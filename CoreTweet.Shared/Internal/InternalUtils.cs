// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2018 CoreTweet Development Team
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CoreTweet.Rest;

#if ASYNC
using System.Threading;
using System.Threading.Tasks;
#endif

namespace CoreTweet.Core
{
    internal static class InternalUtils
    {
        internal static IEnumerable<KeyValuePair<string, object>> ResolveObject(object t)
        {
            if(t == null)
                return new Dictionary<string, object>();
            var ie = t as IEnumerable<KeyValuePair<string, object>>;
            if(ie != null) return ie;

#if NETCORE
            var type = t.GetType().GetTypeInfo();
#else
            var type = t.GetType();
#endif

            if(type.GetCustomAttributes(typeof(TwitterParametersAttribute), false).Any())
            {
                var d = new Dictionary<string, object>();

#if NETCORE
                foreach(var f in type.DeclaredFields.Where(x => x.IsPublic && !x.IsStatic))
#else
                foreach(var f in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
#endif
                {
                    var attr = f.GetCustomAttributes(true).OfType<TwitterParameterAttribute>().FirstOrDefault();
                    var value = f.GetValue(t);
                    if(attr != null && value != null)
                    {
                        var defaultValue = attr.DefaultValue ?? GetDefaultValue(t.GetType());
                        if(!value.Equals(defaultValue))
                            d.Add(attr.Name ?? f.Name, value);
                    }
                }

#if NETCORE
                foreach(var p in type.DeclaredProperties.Where(x => x.CanRead && x.GetMethod.IsPublic && !x.GetMethod.IsStatic))
#else
                foreach(var p in type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanRead))
#endif
                {
                    var attr = p.GetCustomAttributes(true).OfType<TwitterParameterAttribute>().FirstOrDefault();
                    var value = p.GetValue(t, null);
                    if(attr != null && value != null)
                    {
                        var defaultValue = attr.DefaultValue ?? GetDefaultValue(t.GetType());
                        if(!value.Equals(defaultValue))
                            d.Add(attr.Name ?? p.Name, value);
                    }
                }

                return d;
            }

            // IEnumerable<KeyVakuePair<string, Any>> or IEnumerable<Tuple<string, Any>>
            var ienumerable = t as System.Collections.IEnumerable;
            if(ienumerable != null)
            {
                var elements = ienumerable.Cast<object>();
                var ieElementTypes =
                    type.GetInterfaces()
#if NETCORE
                    .Select(IntrospectionExtensions.GetTypeInfo)
#endif
                    .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>))
#if NETCORE
                    .Select(x => x.GenericTypeArguments[0].GetTypeInfo())
                    .Where(x => x.IsGenericType && x.GenericTypeArguments[0] == typeof(string));
#else
                    .Select(x => x.GetGenericArguments()[0])
                    .Where(x => x.IsGenericType && x.GetGenericArguments()[0] == typeof(string));
#endif
                foreach(var genericElement in ieElementTypes)
                {
                    var genericTypeDefinition = genericElement.GetGenericTypeDefinition();
                    if(genericTypeDefinition == typeof(KeyValuePair<,>))
                    {
                        var getKey = genericElement.GetProperty("Key").GetGetMethod();
                        var getValue = genericElement.GetProperty("Value").GetGetMethod();
                        return elements.Select(x => new KeyValuePair<string, object>(
                            (string)getKey.Invoke(x, null),
                            getValue.Invoke(x, null)
                        ));
                    }
#if !NET35
                    if(genericTypeDefinition == typeof(Tuple<,>))
                    {
                        var getItem1 = genericElement.GetProperty("Item1").GetGetMethod();
                        var getItem2 = genericElement.GetProperty("Item2").GetGetMethod();
                        return elements.Select(x => new KeyValuePair<string, object>(
                            (string)getItem1.Invoke(x, null),
                            getItem2.Invoke(x, null)
                        ));
                    }
#endif
                }
            }

#if !NET35
            // Tuple<Tuple<string, Any>, Tuple<string, Any>, ...>
            if (type.FullName.StartsWith("System.Tuple`", StringComparison.Ordinal))
            {
                var items = EnumerateTupleItems(t).ToArray();
                try
                {
                    return items.ConvertAll(x =>
                    {
                        var xtype = x.GetType();
                        return new KeyValuePair<string, object>(
#if NETCORE
                            (string)xtype.GetRuntimeProperty("Item1").GetValue(x),
                            xtype.GetRuntimeProperty("Item2").GetValue(x)
#else
                            (string)xtype.GetProperty("Item1").GetValue(x, null),
                            xtype.GetProperty("Item2").GetValue(x, null)
#endif
                        );
                    });
                }
                catch
                {
                    return ResolveObject(items);
                }
            }
#endif

            return AnnoToDictionary(t);
        }

        private static IDictionary<string,object> AnnoToDictionary(object f)
        {
#if NETCORE
            return f.GetType().GetRuntimeProperties()
                .Where(x => x.CanRead && x.GetIndexParameters().Length == 0)
                .Select(x => Tuple.Create(x.Name, x.GetMethod))
                .Where(x => x.Item2.IsPublic && !x.Item2.IsStatic)
                .ToDictionary(x => x.Item1, x => x.Item2.Invoke(f, null));
#else
            return f.GetType().GetProperties()
                .Where(x => x.CanRead && x.GetIndexParameters().Length == 0)
                .ToDictionary(x => x.Name, x => x.GetValue(f, null));
#endif
        }

#if !NET35
        private static IEnumerable<object> EnumerateTupleItems(object tuple)
        {
            while(true)
            {
#if NETCORE
                var type = tuple.GetType().GetTypeInfo();
                var props = type.DeclaredProperties;
#else
                var type = tuple.GetType();
                var props = type.GetProperties();
#endif

                foreach(var p in props.Where(x => x.Name.StartsWith("Item", StringComparison.Ordinal)).OrderBy(x => x.Name))
                    yield return p.GetValue(tuple, null);

                if(type.GetGenericTypeDefinition() == typeof(Tuple<,,,,,,,>))
                    tuple = type.GetProperty("Rest").GetValue(tuple, null);
                else
                    break;
            }
        }
#endif

        private static object GetExpressionValue(Expression<Func<string,object>> expr)
        {
            var constExpr = expr.Body as ConstantExpression;
            return constExpr != null ? constExpr.Value : expr.Compile()("");
        }

        private static object GetDefaultValue(Type type)
        {
            return type
#if NETCORE
                .GetTypeInfo()
#endif
                .IsValueType ? Activator.CreateInstance(type) : null;
        }

        internal static IEnumerable<KeyValuePair<string, object>> ExpressionsToDictionary(IEnumerable<Expression<Func<string,object>>> exprs)
        {
            return exprs.Select(x => new KeyValuePair<string, object>(x.Parameters[0].Name, GetExpressionValue(x)));
        }

        internal static byte[] MapDictToJson(IEnumerable<KeyValuePair<string, object>> parameters, string[] jsonmap)
        {
            var dic = parameters == null
                ? new Dictionary<string, object>()
                : (parameters as IDictionary<string, object>)
                    ?? parameters.ToDictionary(x => x.Key, x => x.Value); // Check key duplication

            var jm = jsonmap
                 .Select(x =>
                    {
                        if(x.IndexOf('$') < 0)
                            return x;

                        // Only zero or one $placeholder exists in a line.
                        foreach(var i in dic)
                        {
                            var placeholder = "$" + i.Key;
                            var placeholderIndex = x.IndexOf(placeholder);
                            if(placeholderIndex >= 0)
                            {
                                var placeholderEndIndex = placeholderIndex + placeholder.Length;
                                if(placeholderEndIndex == x.Length)
                                    return x.Remove(placeholderIndex) + FormatValueForJson(i.Value);

                                var nextChar = x[placeholderEndIndex];
                                if(nextChar != '_' && !char.IsLetterOrDigit(nextChar))
                                    return x.Remove(placeholderIndex) + FormatValueForJson(i.Value) + x.Substring(placeholderEndIndex);
                            }
                        }

                        return "";
                    }
                 )
                 .JoinToString();

            var jt = JToken.Parse(jm);
            var jsonStr = jt.RemoveEmptyObjects(true).ToString();
            return Encoding.UTF8.GetBytes(jsonStr);
        }

        private static string FormatValueForJson(object value)
        {
            if (value == null) return "null";

            var type = value.GetType();
            if (type.Name == "FSharpOption`1")
            {
                return FormatValueForJson(
#if NETCORE
                    type.GetRuntimeProperty("Value").GetValue(value)
#else
                    type.GetProperty("Value").GetValue(value, null)
#endif
                );
            }

            return JsonConvert.SerializeObject(value);
        }

        internal static JToken RemoveEmptyObjects(this JToken t, bool recursive = false)
        {
            if (t.Type != JTokenType.Object)
                return t;
            var cp = new JObject();
            foreach (var x in t.Children<JProperty>())
            {
                var c = x.Value;
                if (recursive && c.HasValues)
                    c = c.RemoveEmptyObjects(true);
                if (c.Type != JTokenType.Object || c.HasValues)
                    cp.Add(x.Name, c);
            }
            return cp;
        }

        private static object FormatObjectForParameter(object x)
        {
            if (x is string) return x;
            if (x is int)
                return ((int)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is long)
                return ((long)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is double)
            {
                var s = ((double)x).ToString("F99", CultureInfo.InvariantCulture).TrimEnd('0');
                if (s[s.Length - 1] == '.') s += '0';
                return s;
            }
            if (x is float)
            {
                var s = ((float)x).ToString("F99", CultureInfo.InvariantCulture).TrimEnd('0');
                if (s[s.Length - 1] == '.') s += '0';
                return s;
            }
            if (x is uint)
                return ((uint)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is ulong)
                return ((ulong)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is short)
                return ((short)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is ushort)
                return ((ushort)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is decimal)
                return ((decimal)x).ToString(CultureInfo.InvariantCulture);
            if (x is byte)
                return ((byte)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is sbyte)
                return ((sbyte)x).ToString("D", CultureInfo.InvariantCulture);

            if (x is DateTimeOffset)
                return ((DateTimeOffset)x).ToUniversalTime().ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture);

            if (x is TweetMode || x is Bucket)
                return x.ToString().ToLowerInvariant();

            if (x is V2.TweetExpansions)
                return V2.TweetExpansionsExtensions.ToQueryString((V2.TweetExpansions)x);
            if (x is V2.UserExpansions)
                return V2.UserExpansionsExtensions.ToQueryString((V2.UserExpansions)x);

            if (x is V2.MediaFields)
                return V2.MediaFieldsExtensions.ToQueryString((V2.MediaFields)x);
            if (x is V2.PlaceFields)
                return V2.PlaceFieldsExtensions.ToQueryString((V2.PlaceFields)x);
            if (x is V2.PollFields)
                return V2.PollFieldsExtensions.ToQueryString((V2.PollFields)x);
            if (x is V2.TweetFields)
                return V2.TweetFieldsExtensions.ToQueryString((V2.TweetFields)x);
            if (x is V2.UserFields)
                return V2.UserFieldsExtensions.ToQueryString((V2.UserFields)x);

            if (x is UploadMediaType)
                return Media.GetMediaTypeString((UploadMediaType)x);

            if (x is IEnumerable<string>
                || x is IEnumerable<int>
                || x is IEnumerable<long>
                || x is IEnumerable<double>
                || x is IEnumerable<float>
                || x is IEnumerable<uint>
                || x is IEnumerable<ulong>
                || x is IEnumerable<short>
                || x is IEnumerable<ushort>
                || x is IEnumerable<decimal>)
            {
                return ((System.Collections.IEnumerable)x).Cast<object>().Select(FormatObjectForParameter).JoinToString(",");
            }

            var type = x.GetType();
            if (type.Name == "FSharpOption`1")
            {
                return FormatObjectForParameter(
#if NETCORE
                    type.GetRuntimeProperty("Value").GetValue(x)
#else
                    type.GetProperty("Value").GetValue(x, null)
#endif
                );
            }

            return x;
        }

        internal static KeyValuePair<string, object>[] FormatParameters(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return parameters != null
                ? parameters.Where(kvp => kvp.Key != null && kvp.Value != null)
                    .Select(kvp => new KeyValuePair<string, object>(kvp.Key, FormatObjectForParameter(kvp.Value)))
                    .ToArray()
                : new KeyValuePair<string, object>[0];
        }

        internal static string GetUrl(ConnectionOptions options, string baseUrl, bool needsVersion, string rest)
        {
            var result = new StringBuilder(baseUrl.TrimEnd('/'));
            if (needsVersion)
            {
                result.Append('/');
                result.Append((options ?? ConnectionOptions.Default).UrlPrefix);
            }
            result.Append('/');
            result.Append(rest);
            return result.ToString();
        }

        internal static string GetUrl(ConnectionOptions options, string apiName)
        {
            if (options == null) options = ConnectionOptions.Default;
            return GetUrl(options, options.ApiUrl, true, apiName + options.UrlSuffix);
        }

        internal static readonly DateTimeOffset unixEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);

        internal static DateTimeOffset GetUnixTime(long seconds)
        {
            return unixEpoch.AddTicks(checked(seconds * 10000000));
        }

        internal static DateTimeOffset GetUnixTimeMs(long milliseconds)
        {
            return unixEpoch.AddTicks(checked(milliseconds * 10000));
        }

        private const string XRateLimitLimit = "x-rate-limit-limit";
        private const string XRateLimitRemaining = "x-rate-limit-remaining";
        private const string XRateLimitReset = "x-rate-limit-reset";

#if SYNC
        internal static RateLimit ReadRateLimit(HttpWebResponse response)
        {
            var limit = response.Headers[XRateLimitLimit];
            var remaining = response.Headers[XRateLimitRemaining];
            var reset = response.Headers[XRateLimitReset];
            return limit != null && remaining != null && reset != null
                ? new RateLimit()
                {
                    Limit = int.Parse(limit, NumberFormatInfo.InvariantInfo),
                    Remaining = int.Parse(remaining, NumberFormatInfo.InvariantInfo),
                    Reset = GetUnixTime(long.Parse(reset, NumberFormatInfo.InvariantInfo))
                }
                : null;
        }
#endif

#if ASYNC
        internal static RateLimit ReadRateLimit(AsyncResponse response)
        {
            if(!new[] { XRateLimitLimit, XRateLimitRemaining, XRateLimitReset }
                .All(x => response.Headers.ContainsKey(x)))
                return null;

            var limit = response.Headers[XRateLimitLimit];
            var remaining = response.Headers[XRateLimitRemaining];
            var reset = response.Headers[XRateLimitReset];
            return new RateLimit()
            {
                Limit = int.Parse(limit, NumberFormatInfo.InvariantInfo),
                Remaining = int.Parse(remaining, NumberFormatInfo.InvariantInfo),
                Reset = GetUnixTime(long.Parse(reset, NumberFormatInfo.InvariantInfo))
            };
        }
#endif

        private static KeyValuePair<string, object> GetReservedParameter(List<KeyValuePair<string, object>> parameters, string reserved)
        {
            return parameters.Single(kvp => kvp.Key == reserved);
        }

#if SYNC
        internal static T ReadResponse<T>(HttpWebResponse response, string jsonPath)
        {
            using(var sr = new StreamReader(response.GetResponseStream()))
            {
                var json = sr.ReadToEnd();
                var result = CoreBase.Convert<T>(json, jsonPath);
                var twitterResponse = result as ITwitterResponse;
                if(twitterResponse != null)
                {
                    twitterResponse.RateLimit = ReadRateLimit(response);
                    twitterResponse.Json = json;
                }
                return result;
            }
        }

        internal static void AccessParameterReservedApiNoResponse(this TokensBase t, MethodType m, string uri, IEnumerable<string> reserveds, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix = null, string urlSuffix = null)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            var list = parameters.ToList();
            var replaced = reserveds.Select(reserved =>
            {
                var kvp = GetReservedParameter(list, reserved);
                list.Remove(kvp);
                return kvp;
            }).Aggregate(uri, (acc, kvp) => acc.Replace(string.Format("{{{0}}}", kvp.Key), FormatObjectForParameter(kvp.Value).ToString()));
            t.AccessApiNoResponseImpl(m, replaced, list, urlPrefix, urlSuffix);
        }

        /// <summary>
        /// id, slug, etc
        /// </summary>
        internal static T AccessParameterReservedApi<T>(this TokensBase t, MethodType m, string uri, IEnumerable<string> reserveds, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix = null, string urlSuffix = null)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            var list = parameters.ToList();
            var replaced = reserveds.Select(reserved =>
            {
                var kvp = GetReservedParameter(list, reserved);
                list.Remove(kvp);
                return kvp;
            }).Aggregate(uri, (acc, kvp) => acc.Replace(string.Format("{{{0}}}", kvp.Key), FormatObjectForParameter(kvp.Value).ToString()));
            return t.AccessApiImpl<T>(m, replaced, list, "", urlPrefix, urlSuffix);
        }

        internal static ListedResponse<T> AccessParameterReservedApiArray<T>(this TokensBase t, MethodType m, string uri, IEnumerable<string> reserveds, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix = null, string urlSuffix = null)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            var list = parameters.ToList();
            var replaced = reserveds.Select(reserved =>
            {
                var kvp = GetReservedParameter(list, reserved);
                list.Remove(kvp);
                return kvp;
            }).Aggregate(uri, (acc, kvp) => acc.Replace(string.Format("{{{0}}}", kvp.Key), FormatObjectForParameter(kvp.Value).ToString()));
            return t.AccessApiArrayImpl<T>(m, replaced, list, "", urlPrefix, urlSuffix);
        }
#endif

#if ASYNC
        internal static Task AccessParameterReservedApiNoResponseAsync(this TokensBase t, MethodType m, string uri, IEnumerable<string> reserveds, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix = null, string urlSuffix = null)
        {
            if(parameters == null) throw new ArgumentNullException(nameof(parameters));
            var list = parameters.ToList();
            var replaced = reserveds.Select(reserved =>
            {
                var kvp = GetReservedParameter(list, reserved);
                list.Remove(kvp);
                return kvp;
            }).Aggregate(uri, (acc, kvp) => acc.Replace(string.Format("{{{0}}}", kvp.Key), FormatObjectForParameter(kvp.Value).ToString()));
            return t.AccessApiNoResponseAsyncImpl(m, replaced, list, cancellationToken, urlPrefix, urlSuffix);
        }

        internal static Task<T> AccessParameterReservedApiAsync<T>(this TokensBase t, MethodType m, string uri, IEnumerable<string> reserveds, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix = null, string urlSuffix = null)
        {
            if(parameters == null) throw new ArgumentNullException(nameof(parameters));
            var list = parameters.ToList();
            var replaced = reserveds.Select(reserved =>
            {
                var kvp = GetReservedParameter(list, reserved);
                list.Remove(kvp);
                return kvp;
            }).Aggregate(uri, (acc, kvp) => acc.Replace(string.Format("{{{0}}}", kvp.Key), FormatObjectForParameter(kvp.Value).ToString()));
            return t.AccessApiAsyncImpl<T>(m, replaced, list, cancellationToken, "", urlPrefix, urlSuffix);
        }

        internal static Task<ListedResponse<T>> AccessParameterReservedApiArrayAsync<T>(this TokensBase t, MethodType m, string uri, IEnumerable<string> reserveds, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix = null, string urlSuffix = null)
        {
            if(parameters == null) throw new ArgumentNullException(nameof(parameters));
            var list = parameters.ToList();
            var replaced = reserveds.Select(reserved =>
            {
                var kvp = GetReservedParameter(list, reserved);
                list.Remove(kvp);
                return kvp;
            }).Aggregate(uri, (acc, kvp) => acc.Replace(string.Format("{{{0}}}", kvp.Key), FormatObjectForParameter(kvp.Value).ToString()));
            return t.AccessApiArrayAsyncImpl<T>(m, replaced, list, cancellationToken, "", urlPrefix, urlSuffix);
        }

        internal static Task<AsyncResponse> ResponseCallback(this Task<AsyncResponse> task, CancellationToken cancellationToken)
        {
            return task.Done(async res =>
            {
                if(!res.Source.IsSuccessStatusCode)
                {
                    var tex = await TwitterException.Create(res).ConfigureAwait(false);
                    if(tex != null) throw tex;
                    res.Source.EnsureSuccessStatusCode();
                }

                return res;
            }, cancellationToken).Unwrap();
        }

        internal static async Task<T> ReadResponse<T>(this Task<AsyncResponse> t, Func<string, T> parse, CancellationToken cancellationToken)
        {
            using (var res = await t.ConfigureAwait(false))
            {
                // Check here to make sure dispose `res` if the cancellation was requested
                cancellationToken.ThrowIfCancellationRequested();

                using (var sr = new StreamReader(await res.GetResponseStreamAsync().ConfigureAwait(false)))
                {
                    var json = await sr.ReadToEndAsync().ConfigureAwait(false);
                    var result = parse(json);
                    var twitterResponse = result as ITwitterResponse;
                    if (twitterResponse != null)
                    {
                        twitterResponse.RateLimit = ReadRateLimit(res);
                        twitterResponse.Json = json;
                    }
                    return result;
                }
            }
        }
#endif
    }
}
