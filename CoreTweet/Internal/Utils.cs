// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013 lambdalice
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
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Alice.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace CoreTweet.Core
{
    internal static class InternalUtils
    {
        internal static IDictionary<string, object> ResolveObject<T>(T t, BindingFlags flags = BindingFlags.Default)
        {
            var type = typeof(T);
            if(t is IEnumerable<KeyValuePair<string, object>>)
                return (t as IEnumerable<KeyValuePair<string, object>>).ToDictionary(x => x.Key, x => x.Value);

            else
            {
                if(type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any())
                    return AnnoToDictionary(t);

                var flag = BindingFlags.Instance | BindingFlags.Public | flags;

                if(type.GetCustomAttributes(typeof(TwitterParametersAttribute), false).Any())
                {
                    var d = new Dictionary<string, object>();

                    foreach(var f in type.GetFields(flag))
                    {
                        var attr = f.GetCustomAttributes(true).FirstOrDefault(y => y is TwitterParameterAttribute);
                        if(attr != null)
                        {
                            var name = (attr as TwitterParameterAttribute).Name;
                            d.Add(name != null ? name : f.Name, f.GetValue(t));
                        }
                    }

                    foreach(var p in type.GetProperties(flag).Where(x => x.CanRead))
                    {
                        var attr = p.GetCustomAttributes(true).FirstOrDefault(y => y is TwitterParameterAttribute);
                        if(attr != null)
                        {
                            var name = (attr as TwitterParameterAttribute).Name;
                            d.Add(name != null ? name : p.Name, p.GetValue(t, null));
                        }
                    }

                    return d;
                }

                throw new InvalidDataException("the object " + t.ToString() + " can not be used as parameters.");
            }
        }

        internal static IDictionary<string, object> AnnoToDictionary<T>(T f)
        {
            return typeof(T).GetProperties()
                .Where(x => x.CanRead && x.GetIndexParameters().Length == 0)
                .ToDictionary(x => x.Name, x => x.GetValue(f, null));
        }

        internal static object GetExpressionValue(Expression<Func<string, object>> expr)
        {
            var constExpr = expr.Body as ConstantExpression;
            return constExpr != null ? constExpr.Value : expr.Compile()("");
        }

        internal static IDictionary<string, object> ExpressionsToDictionary(IEnumerable<Expression<Func<string, object>>> exprs)
        {
            return exprs.ToDictionary(x => x.Parameters[0].Name, GetExpressionValue);
        }

        /// <summary>
        /// Gets the url of the specified api's name.
        /// </summary>
        /// <returns>The url.</returns>
        internal static string GetUrl(string apiName)
        {
            return string.Format("https://api.twitter.com/{0}/{1}.json", Property.ApiVersion, apiName);
        }
    } 
}

