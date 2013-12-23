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

namespace CoreTweet.Core
{
    public abstract class CoreBase : TokenIncluded
    {
        public CoreBase() : base() { }
        
        public CoreBase(Tokens tokens) : base(tokens) { }
        
        /// <summary>
        /// Convert dynamic object to specified type.
        /// </summary>
        /// <param name='tokens'>
        /// OAuth tokens.
        /// </param>
        /// <param name='e'>
        /// Dynamic object.
        /// </param>
        /// <typeparam name='T'>
        /// The 1st type parameter.
        /// </typeparam>
        public static T Convert<T>(Tokens tokens, dynamic e)
            where T : CoreBase
        {
            var i = (T)typeof(T).InvokeMember(null, System.Reflection.BindingFlags.CreateInstance, null, null, new []{tokens});
            i.ConvertBase(e);
            return i;
        }
        
        /// <summary>
        /// Convert dynamic object to an array of specified type.
        /// </summary>
        /// <param name='tokens'>
        /// OAuth tokens.
        /// </param>
        /// <param name='e'>
        /// Dynamic object.
        /// </param>
        /// <typeparam name='T'>
        /// The 1st type parameter.
        /// </typeparam>
        public static IEnumerable<T> ConvertArray<T>(Tokens tokens, dynamic e)
            where T : CoreBase
        {
            if(e == null || !e.IsArray)
                return null;
            T[] ts = new T[((dynamic[])e).Length];
            for(int i = 0; i < ((dynamic[])e).Length; i++)
                ts[i] = Convert<T>(tokens, ((dynamic[])e)[i]);
            return ts;
        }

        /// <summary>
        /// Implementation for CoreBase.Convert.
        /// </summary>
        internal abstract void ConvertBase(dynamic e);
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
        protected internal Tokens Tokens { get; set; }

		public Tokens IncludedTokens
		{
			get
			{
				return this.Tokens;
			}
		}
        
        public TokenIncluded() : this(null) { }
        
        public TokenIncluded(Tokens tokens)
        {
            Tokens = tokens;
        }
    }
}