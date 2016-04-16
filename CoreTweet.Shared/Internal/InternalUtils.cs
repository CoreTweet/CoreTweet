// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2016 CoreTweet Development Team
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
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;

#if !NET35
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

#if WIN_RT || PCL
            var type = t.GetType().GetTypeInfo();
#else
            var type = t.GetType();
#endif

            if(type.GetCustomAttributes(typeof(TwitterParametersAttribute), false).Any())
            {
                var d = new Dictionary<string, object>();

#if WIN_RT || PCL
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

#if WIN_RT || PCL
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
                    .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>))
#if WIN_RT || PCL
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
            if (type.Namespace == "System" && type.Name.StartsWith("Tuple`", StringComparison.Ordinal))
            {
                var items = EnumerateTupleItems(t).ToArray();
                try
                {
                    return items.Select(x =>
                    {
                        var xtype = x.GetType();
                        return new KeyValuePair<string, object>(
#if WIN_RT || PCL
                            (string)xtype.GetRuntimeProperty("Item1").GetValue(x),
                            xtype.GetRuntimeProperty("Item2").GetValue(x)
#else
                            (string)xtype.GetProperty("Item1").GetValue(x, null),
                            xtype.GetProperty("Item2").GetValue(x, null)
#endif
                        );
                    }).ToArray();
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
#if WIN_RT || PCL
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
#if WIN_RT || PCL
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
#if WIN_RT || PCL
                .GetTypeInfo()
#endif
                .IsValueType ? Activator.CreateInstance(type) : null;
        }

        internal static IEnumerable<KeyValuePair<string, object>> ExpressionsToDictionary(IEnumerable<Expression<Func<string,object>>> exprs)
        {
            return exprs.Select(x => new KeyValuePair<string, object>(x.Parameters[0].Name, GetExpressionValue(x)));
        }

        internal static string GetUrl(ConnectionOptions options, string baseUrl, bool needsVersion, string rest)
        {
            var result = new StringBuilder(baseUrl.TrimEnd('/'));
            if (needsVersion)
            {
                result.Append('/');
                result.Append((options ?? new ConnectionOptions()).ApiVersion);
            }
            result.Append('/');
            result.Append(rest);
            return result.ToString();
        }

        internal static string GetUrl(ConnectionOptions options, string apiName)
        {
            if (options == null) options = new ConnectionOptions();
            return GetUrl(options, options.ApiUrl, true, apiName + ".json");
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

        internal static RateLimit ReadRateLimit(HttpWebResponse response)
        {
            var limit = response.Headers[XRateLimitLimit];
            var remaining = response.Headers[XRateLimitRemaining];
            var reset = response.Headers[XRateLimitReset];
            return limit != null && remaining != null && reset != null
                ? new RateLimit()
                {
                    Limit = int.Parse(limit),
                    Remaining = int.Parse(remaining),
                    Reset = GetUnixTime(long.Parse(reset))
                }
                : null;
        }

#if !NET35
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
                Limit = int.Parse(limit),
                Remaining = int.Parse(remaining),
                Reset = GetUnixTime(long.Parse(reset))
            };
        }
#endif

        private static KeyValuePair<string, object> GetReservedParameter(List<KeyValuePair<string, object>> parameters, string reserved)
        {
            return parameters.Single(kvp => kvp.Key == reserved);
        }

#if !ASYNC_ONLY
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

        /// <summary>
        /// id, slug, etc
        /// </summary>
        internal static T AccessParameterReservedApi<T>(this TokensBase t, MethodType m, string uri, string reserved, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if(parameters == null) throw new ArgumentNullException(nameof(parameters));
            var list = parameters.ToList();
            var kvp = GetReservedParameter(list, reserved);
            list.Remove(kvp);
            return t.AccessApiImpl<T>(m, uri.Replace(string.Format("{{{0}}}", reserved), kvp.Value.ToString()), list, "");
        }

        internal static ListedResponse<T> AccessParameterReservedApiArray<T>(this TokensBase t, MethodType m, string uri, string reserved, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if(parameters == null) throw new ArgumentNullException(nameof(parameters));
            var list = parameters.ToList();
            var kvp = GetReservedParameter(list, reserved);
            list.Remove(kvp);
            return t.AccessApiArrayImpl<T>(m, uri.Replace(string.Format("{{{0}}}", reserved), kvp.Value.ToString()), list, "");
        }
#endif

#if !NET35
        internal static Task<T> AccessParameterReservedApiAsync<T>(this TokensBase t, MethodType m, string uri, string reserved, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            if(parameters == null) throw new ArgumentNullException(nameof(parameters));
            var list = parameters.ToList();
            var kvp = GetReservedParameter(list, reserved);
            list.Remove(kvp);
            return t.AccessApiAsyncImpl<T>(m, uri.Replace(string.Format("{{{0}}}", reserved), kvp.Value.ToString()), list, cancellationToken, "");
        }

        internal static Task<ListedResponse<T>> AccessParameterReservedApiArrayAsync<T>(this TokensBase t, MethodType m, string uri, string reserved, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            if(parameters == null) throw new ArgumentNullException(nameof(parameters));
            var list = parameters.ToList();
            var kvp = GetReservedParameter(list, reserved);
            list.Remove(kvp);
            return t.AccessApiArrayAsyncImpl<T>(m, uri.Replace(string.Format("{{{0}}}", reserved), kvp.Value.ToString()), list, cancellationToken, "");
        }


        internal static Task<AsyncResponse> ResponseCallback(this Task<AsyncResponse> task, CancellationToken cancellationToken)
        {
#if WIN_RT || PCL
            return task.Done(async res =>
            {
                if(!res.Source.IsSuccessStatusCode)
                {
                    var tex = await TwitterException.Create(res).ConfigureAwait(false);
                    if(tex != null)
                        throw tex;
                    res.Source.EnsureSuccessStatusCode();
                }

                return res;
            }, cancellationToken).Unwrap();
#else
            return task.ContinueWith(t =>
            {
                if(t.IsFaulted)
                {
                    var wex = t.Exception.InnerException as WebException;
                    if(wex != null)
                    {
                        var tex = TwitterException.Create(wex);
                        if(tex != null)
                            throw tex;
                    }
                    t.Exception.InnerException.Rethrow();
                }

                return t;
            }, cancellationToken).Unwrap();
#endif
        }

        internal static Task<T> ReadResponse<T>(this Task<AsyncResponse> t, Func<string, T> parse, CancellationToken cancellationToken)
        {
            return t.Done(res =>
            {
                var reg = cancellationToken.Register(res.Dispose);
                return res.GetResponseStreamAsync()
                    .Done(stream =>
                    {
                        try
                        {
                            using(var sr = new StreamReader(stream))
                            {
                                var json = sr.ReadToEnd();
                                var result = parse(json);
                                var twitterResponse = result as ITwitterResponse;
                                if(twitterResponse != null)
                                {
                                    twitterResponse.RateLimit = ReadRateLimit(res);
                                    twitterResponse.Json = json;
                                }
                                return result;
                            }
                        }
                        finally
                        {
                            reg.Dispose();
                            cancellationToken.ThrowIfCancellationRequested();
                        }
                    }, cancellationToken);
            }, cancellationToken).Unwrap();
        }
#endif
    }
}
