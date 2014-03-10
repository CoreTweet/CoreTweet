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
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using Alice.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace CoreTweet.Core
{
    /// <summary>
    /// The base class of twitter objects.
    /// </summary>
    public abstract class CoreBase : TokenIncluded
    {
        public CoreBase(TokensBase t) : base(t) { }
        public CoreBase() : base() {}
        /// <summary>
        /// Convert the json to a twitter object of the specified type.
        /// </summary>
        /// <remarks>
        /// This method is used internally in CoreTweet.
        /// You can use this method for debugging.
        /// </remarks>
        /// <param name='tokens'>
        /// OAuth tokens.
        /// </param>
        /// <param name='json'>
        /// The json message.
        /// </param>
        /// <typeparam name='T'>
        /// The type of a twitter object.
        /// </typeparam>
        /// <returns>
        /// The twitter object.
        /// </returns>
        public static T Convert<T>(TokensBase tokens, string json)
            where T : CoreBase
        {
            var r = ConvertBase<T>(tokens, json);
            r.Tokens = tokens;
            return r;
        }

        /// <summary>
        /// Convert the json to a twitter object of the specified type.
        /// </summary>
        /// <remarks>
        /// This method is used internally in CoreTweet.
        /// You can use this method for debugging.
        /// </remarks>
        /// <param name='tokens'>
        /// OAuth tokens.
        /// </param>
        /// <param name='json'>
        /// The json message.
        /// </param>
        /// <typeparam name='T'>
        /// The type of a twitter object.
        /// </typeparam>
        /// <returns>
        /// The twitter object.
        /// </returns>
        public static T ConvertBase<T>(TokensBase tokens, string json)
        {
            try
            {
                var js = new JsonSerializer();
                var cr = new DefaultContractResolver();
                cr.DefaultMembersSearchFlags = cr.DefaultMembersSearchFlags | BindingFlags.NonPublic;
                js.ContractResolver = cr;
                var r = from x in new StringReader(json).Use()
                        from y in new JsonTextReader(x).Use()
                        select js.Deserialize<T>(y);
                return r;
            }
            catch(Exception ex)
            {
                throw new ParsingException("on a REST api, cannot parse the json", JToken.Parse(json).ToString(Formatting.Indented), ex);
            }
        }

        /// <summary>
        /// Convert the json to a twitter object of the specified type.
        /// This is used for APIs that return an array.
        /// </summary>
        /// <remarks>
        /// This method is used internally in CoreTweet.
        /// You can use this method for debugging.
        /// </remarks>
        /// <param name='tokens'>
        /// OAuth tokens.
        /// </param>
        /// <param name='json'>
        /// The json message.
        /// </param>
        /// <typeparam name='T'>
        /// The type of a twitter object.
        /// </typeparam>
        /// <returns>
        /// Twitter objects.
        /// </returns>
        public static IEnumerable<T> ConvertArray<T>(TokensBase tokens, string json)
            where T : CoreBase
        {
            var r = ConvertBase<IEnumerable<T>>(tokens, json);
            foreach(var x in r)
                x.Tokens = tokens;
            return r;
        }
    }
    
    /// <summary>
    /// The token included class.
    /// </summary>
    public abstract class TokenIncluded
    {
        /// <summary>
        /// Gets or sets the oauth tokens.
        /// </summary>
        /// <value>
        /// The tokens.
        /// </value>
        protected internal TokensBase Tokens { get; set; }

        public TokensBase IncludedTokens
        {
            get
            {
                return this.Tokens;
            }
        }
        
        public TokenIncluded() : this(null)
        {
        }
        
        public TokenIncluded(TokensBase tokens)
        {
            Tokens = tokens;
        }
    }

    internal static class InternalUtil
    {
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
        internal static string Url(string apiName)
        {
            return string.Format("https://api.twitter.com/{0}/{1}.json", Property.ApiVersion, apiName);
        }
    }
}