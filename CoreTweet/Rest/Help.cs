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
using System.Linq.Expressions;
using System.Collections.Generic;
using System.IO;
using CoreTweet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Alice.Extensions;

namespace CoreTweet.Rest
{

    /// <summary>GET help</summary>
    public class Help : TokenIncluded
    {
        internal Help(Tokens e) : base(e) { }
            
            
        //GET Methods
            
        /// <summary>
        /// <para>Returns the current configuration used by Twitter including twitter.com slugs which are not usernames, maximum photo resolutions, and t.co URL lengths.</para>
        /// <para>It is recommended applications request this endpoint when they are loaded, but no more than once a day.</para>
        /// <para>Avaliable parameters: Nothing.</para>
        /// </summary>
        /// <returns>Configurations.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Configurations Configuration(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Configurations>(MethodType.Get, "help/configuation", parameters);
        }
            
        /// <summary>
        /// <para>Returns the list of languages supported by Twitter along with their ISO 639-1 code. The ISO 639-1 code is the two letter value to use if you include lang with any of your requests.</para>
        /// <para>Avaliable parameters: Nothing.</para>
        /// </summary>
        /// <returns>Languages.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<Language> Languages(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Language>(MethodType.Get, "help/languages", parameters);
        }
            
        /// <summary>
        /// <para>Returns Twitter's Privacy Policy.</para>
        /// <para>Avaliable parameters: Nothing.</para>
        /// </summary>
        /// <returns>The sentense.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public string Privacy(params Expression<Func<string,object>>[] parameters)
        {
            dynamic j = JObject.Parse(from x in this.Tokens.SendRequest(MethodType.Get, "help/tos", parameters).Use()
                                      from y in new StreamReader(x).Use()
                                      select y.ReadToEnd());
            return j.privacy;
        }
            
        /// <summary>
        /// <para>Returns the Twitter Terms of Service in the requested format. These are not the same as the Developer Rules of the Road.</para>
        /// <para>Avaliable parameters: Nothing.</para>
        /// </summary>
        /// <returns>The sentense.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public string Tos(params Expression<Func<string,object>>[] parameters)
        {
            dynamic j = JObject.Parse(from x in this.Tokens.SendRequest(MethodType.Get, "help/tos", parameters).Use()
                                      from y in new StreamReader(x).Use()
                                      select y.ReadToEnd());
            return j.tos;
        }
            
    }
}
