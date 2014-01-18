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
using System.Linq.Expressions;
using System.Collections.Generic;
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    [JsonObject]
    public class Cursored<T> : CoreBase, IEnumerable<T>
    {
        public IEnumerable<T> Result
        {
            get
            {
                if(typeof(T) == typeof(long))
                    return _ids;
                else if(typeof(T) == typeof(User))
                    return _users;
                else if(typeof(T) == typeof(CoreTweet.List))
                    return _lists;
                else
                    throw new InvalidOperationException("This type can't be cursored."); 
            }
        }

        [JsonProperty("next_cursor")]
        public long NextCursor{ get; set; }

        [JsonProperty("previous_cursor")]
        public long PreviousCursor{ get; set; }

        [JsonProperty("users")]
        T[] _users { get; set; }

        [JsonProperty("lists")]
        T[] _lists { get; set; }

        [JsonProperty("ids")]
        T[] _ids { get; set; }


        public IEnumerator<T> GetEnumerator()
        {
            return (Result as IEnumerable<T>).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Result.GetEnumerator();
        }

        internal static IEnumerable<T> Enumerate(Tokens tokens, string apiName, EnumerateMode mode, params Expression<Func<string,object>>[] parameters)
        {
            var p = parameters.ToDictionary(e => e.Parameters[0].Name, e => e.Compile()(""));
            var r = tokens.AccessApi<Cursored<T>>(MethodType.Get, apiName, p);
            while(true)
            {
                foreach(var i in r)
                    yield return i;
                var next = mode == EnumerateMode.Next ? r.NextCursor : r.PreviousCursor; 
                if(next == 0)
                    break;
                p["cursor"] = next;
                r = tokens.AccessApi<Cursored<T>>(MethodType.Get, apiName, p);
            }
        }
    }

    public enum EnumerateMode
    {
        Next, Previous
    }
}

