// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2014 lambdalice
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
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CoreTweet.Core
{
    internal static class InternalUtils
    {
        /// <summary>
        /// Object to Dictionary
        /// </summary>
        internal static IEnumerable<KeyValuePair<string, object>> ResolveObject<T>(T t, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public)
        {
            var type = typeof(T);
            if(t is IEnumerable<KeyValuePair<string,object>>)
                return (t as IEnumerable<KeyValuePair<string,object>>);
            else
            {
                if(type.GetCustomAttributes(typeof(TwitterParametersAttribute), false).Any())
                {
                    var d = new Dictionary<string,object>();

                    foreach(var f in type.GetFields(flags))
                    {
                        var attr = (TwitterParameterAttribute)f.GetCustomAttributes(true).FirstOrDefault(y => y is TwitterParameterAttribute);
                        var value = f.GetValue(t);
                        if(attr.DefaultValue == null)
                            attr.DefaultValue = GetDefaultValue(t.GetType());

                        if(attr != null && value != null && !value.Equals(attr.DefaultValue))
                        {
                            var name = attr.Name;
                            d.Add(name != null ? name : f.Name, value);
                        }
                    }

                    foreach(var p in type.GetProperties(flags).Where(x => x.CanRead))
                    {
                        var attr = (TwitterParameterAttribute)p.GetCustomAttributes(true).FirstOrDefault(y => y is TwitterParameterAttribute);
                        var value = p.GetValue(t, null);
                        if(attr.DefaultValue == null)
                            attr.DefaultValue = GetDefaultValue(t.GetType());

                        if(attr != null && value != null && !value.Equals(attr.DefaultValue))
                        {
                            var name = attr.Name;
                            d.Add(name != null ? name : p.Name, value);
                        }
                    }

                    return d;
                }

                else
                    return AnnoToDictionary(t);

                // throw new InvalidDataException("the object " + t.ToString() + " can not be used as parameters.");
            }
        }

        /// <summary>
        /// Anonymous type object to Dictionary
        /// </summary>
        private static IDictionary<string,object> AnnoToDictionary<T>(T f)
        {
            return typeof(T).GetProperties()
                .Where(x => x.CanRead && x.GetIndexParameters().Length == 0)
                .ToDictionary(x => x.Name, x => x.GetValue(f, null));
        }

        /// <summary>
        /// Gets the expression value.
        /// </summary>
        private static object GetExpressionValue(Expression<Func<string,object>> expr)
        {
            var constExpr = expr.Body as ConstantExpression;
            return constExpr != null ? constExpr.Value : expr.Compile()("");
        }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        private static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        /// Expressions to dictionary.
        /// </summary>
        internal static IEnumerable<KeyValuePair<string, object>> ExpressionsToDictionary(IEnumerable<Expression<Func<string,object>>> exprs)
        {
            return exprs.Select(x => new KeyValuePair<string, object>(x.Parameters[0].Name, GetExpressionValue(x)));
        }

        /// <summary>
        /// Gets the url of the specified api's name.
        /// </summary>
        /// <returns>The url.</returns>
        internal static string GetUrl(string apiName)
        {
            return string.Format("https://api.twitter.com/{0}/{1}.json", Property.ApiVersion, apiName);
        }

#if !PCL
        /// <summary>
        /// id, slug, etc
        /// </summary>
        internal static T AccessParameterReservedApi<T>(this TokensBase t, MethodType m, string uri, string reserved, IDictionary<string, object> parameters)
        {
            var r = parameters[reserved];
            parameters.Remove(reserved);
            return t.AccessApi<T>(m, uri.Replace(string.Format("{{{0}}}", reserved), r.ToString()), parameters);
        }

        /// <summary>
        /// id, slug, etc
        /// </summary>
        internal static IEnumerable<T> AccessParameterReservedApiArray<T>(this TokensBase t, MethodType m, string uri, string reserved, IDictionary<string, object> parameters)
        {
            var r = parameters[reserved];
            parameters.Remove(reserved);
            return t.AccessApiArray<T>(m, uri.Replace(string.Format("{{{0}}}", reserved), r.ToString()), parameters);
        }
#endif
    }
}

