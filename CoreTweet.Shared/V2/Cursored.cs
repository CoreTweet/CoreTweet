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
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet.V2
{
    public class CursoredMeta : CoreBase
    {
        /// <summary>
        /// The number of results returned in the response.
        /// </summary>
        [JsonProperty("result_count")]
        public int ResultCount { get; set; }
        // MEMO: the document is wrong (actual key is `result_count`, not `count`)

        /// <summary>
        /// A value that encodes the next 'page' of results that can be requested, via the <c>pagination_token</c> request parameter.
        /// </summary>
        [JsonProperty("next_token")]
        public string NextToken { get; set; }

        /// <summary>
        /// A value that encodes the previous 'page' of results that can be requested, via the <c>pagination_token</c> request parameter.
        /// </summary>
        [JsonProperty("previous_token")]
        public string PreviousToken { get; set; }
    }

    public class CursoredItem<TData, TIncludes, TMeta> : CoreBase
        where TData : CoreBase
        where TIncludes : CoreBase
        where TMeta : CursoredMeta
    {
        public TData Data { get; set; }

        public TIncludes Includes { get; set; }

        public TMeta Meta { get; set; }
    }

    public class Cursored<TData, TIncludes, TMeta> : ResponseBase, ITwitterResponse
        where TData : CoreBase
        where TIncludes : CoreBase
        where TMeta : CursoredMeta
    {
        [JsonProperty("data")]
        public TData[] Data { get; set; }

        [JsonProperty("includes")]
        public TIncludes Includes { get; set; }

        [JsonProperty("meta")]
        public TMeta Meta { get; set; }
    }

    #if SYNC
    internal static class Cursored
    {
        internal static IEnumerable<CursoredItem<TData, TIncludes, TMeta>> Enumerate<TData, TIncludes, TMeta>(TokensBase tokens, string apiName, string cursorKey, EnumerateMode mode, Expression<Func<string,object>>[] parameters, string urlPrefix = null, string urlSuffix = null)
            where TData : CoreBase
            where TIncludes : CoreBase
            where TMeta : CursoredMeta
        {
            var p = InternalUtils.ExpressionsToDictionary(parameters);
            return EnumerateImpl<TData, TIncludes, TMeta>(tokens, apiName, cursorKey, mode, p, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<CursoredItem<TData, TIncludes, TMeta>> Enumerate<TData, TIncludes, TMeta>(TokensBase tokens, string apiName, string cursorKey, EnumerateMode mode, IDictionary<string, object> parameters, string urlPrefix = null, string urlSuffix = null)
            where TData : CoreBase
            where TIncludes : CoreBase
            where TMeta : CursoredMeta
        {
            return EnumerateImpl<TData, TIncludes, TMeta>(tokens, apiName, cursorKey, mode, parameters, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<CursoredItem<TData, TIncludes, TMeta>> Enumerate<TData, TIncludes, TMeta>(TokensBase tokens, string apiName, string cursorKey, EnumerateMode mode, object parameters, string urlPrefix = null, string urlSuffix = null)
            where TData : CoreBase
            where TIncludes : CoreBase
            where TMeta : CursoredMeta
        {
            var p = InternalUtils.ResolveObject(parameters);
            return EnumerateImpl<TData, TIncludes, TMeta>(tokens, apiName, cursorKey, mode, p, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<CursoredItem<TData, TIncludes, TMeta>> EnumerateImpl<TData, TIncludes, TMeta>(TokensBase tokens, string apiName, string cursorKey, EnumerateMode mode, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
            where TData : CoreBase
            where TIncludes : CoreBase
            where TMeta : CursoredMeta
        {
            if(mode == EnumerateMode.Next)
                return EnumerateForwardImpl<TData, TIncludes, TMeta>(tokens, apiName, cursorKey, parameters, urlPrefix, urlSuffix);
            else
                return EnumerateBackwardImpl<TData, TIncludes, TMeta>(tokens, apiName, cursorKey, parameters, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<CursoredItem<TData, TIncludes, TMeta>> EnumerateForward<TData, TIncludes, TMeta>(TokensBase tokens, string apiName, string cursorKey, Expression<Func<string, object>>[] parameters, string urlPrefix = null, string urlSuffix = null)
            where TData : CoreBase
            where TIncludes : CoreBase
            where TMeta : CursoredMeta
        {
            var p = InternalUtils.ExpressionsToDictionary(parameters);
            return EnumerateForwardImpl<TData, TIncludes, TMeta>(tokens, apiName, cursorKey, p, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<CursoredItem<TData, TIncludes, TMeta>> EnumerateForward<TData, TIncludes, TMeta>(TokensBase tokens, string apiName, string cursorKey, IDictionary<string, object> parameters, string urlPrefix = null, string urlSuffix = null)
            where TData : CoreBase
            where TIncludes : CoreBase
            where TMeta : CursoredMeta
        {
            return EnumerateForwardImpl<TData, TIncludes, TMeta>(tokens, apiName, cursorKey, parameters, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<CursoredItem<TData, TIncludes, TMeta>> EnumerateForward<TData, TIncludes, TMeta>(TokensBase tokens, string apiName, string cursorKey, object parameters, string urlPrefix = null, string urlSuffix = null)
            where TData : CoreBase
            where TIncludes : CoreBase
            where TMeta : CursoredMeta
        {
            var p = InternalUtils.ResolveObject(parameters);
            return EnumerateForwardImpl<TData, TIncludes, TMeta>(tokens, apiName, cursorKey, p, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<CursoredItem<TData, TIncludes, TMeta>> EnumerateForwardImpl<TData, TIncludes, TMeta>(TokensBase tokens, string apiName, string cursorKey, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
            where TData : CoreBase
            where TIncludes : CoreBase
            where TMeta : CursoredMeta
        {
            var prmList = parameters.ToList();
            while(true)
            {
                var r = tokens.AccessApiImpl<Cursored<TData, TIncludes, TMeta>>(MethodType.Get, apiName, prmList, "", urlPrefix, urlSuffix);
                foreach(var i in r.Data)
                    yield return new CursoredItem<TData, TIncludes, TMeta>()
                    {
                        Data = i,
                        Includes = r.Includes,
                        Meta = r.Meta,
                    };
                var next = r.Meta.NextToken;
                if(string.IsNullOrEmpty(next))
                    break;
                prmList.RemoveAll(kvp => kvp.Key == cursorKey);
                prmList.Add(new KeyValuePair<string, object>(cursorKey, next));
            }
        }

        internal static IEnumerable<CursoredItem<TData, TIncludes, TMeta>> EnumerateBackwardImpl<TData, TIncludes, TMeta>(TokensBase tokens, string apiName, string cursorKey, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
            where TData : CoreBase
            where TIncludes : CoreBase
            where TMeta : CursoredMeta
        {
            var prmList = parameters.ToList();
            while(true)
            {
                var r = tokens.AccessApiImpl<Cursored<TData, TIncludes, TMeta>>(MethodType.Get, apiName, prmList, "", urlPrefix, urlSuffix);
                foreach(var i in r.Data)
                    yield return new CursoredItem<TData, TIncludes, TMeta>()
                    {
                        Data = i,
                        Includes = r.Includes,
                        Meta = r.Meta,
                    };
                var next = r.Meta.PreviousToken;
                if(string.IsNullOrEmpty(next))
                    break;
                prmList.RemoveAll(kvp => kvp.Key == cursorKey);
                prmList.Add(new KeyValuePair<string, object>(cursorKey, next));
            }
        }
    }
    #endif

    /// <summary>
    /// Provides a mode of enumeration.
    /// </summary>
    public enum EnumerateMode
    {
        /// <summary>
        /// The enumeration mode is next.
        /// </summary>
        Next,
        /// <summary>
        /// The enumeration mode is previous.
        /// </summary>
        Previous
    }
}
