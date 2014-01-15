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
using CoreTweet.Core;

namespace CoreTweet
{
    public class Cursored<T> : CoreBase, IEnumerable<T>
    {
        public IEnumerable<T> Result{ get; set; }

        public long NextCursor{ get; set; }

        public long PreviousCursor{ get; set; }
        
        public Cursored(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Result = ParamByType<T>(this.Tokens, e);
            NextCursor = (long)e.next_cursor;
            PreviousCursor = (long)e.previous_cursor;
        }

        public static T2[] ParamByType<T2>(Tokens tokens, dynamic e)
        {
            if(typeof(T2) == typeof(long))
                return e.ids;
            else if(typeof(T2) == typeof(User))
                return CoreBase.ConvertArray<User>(tokens, e.users) ;
            else if(typeof(T2) == typeof(CoreTweet.List))
                return CoreBase.ConvertArray<CoreTweet.List>(tokens, e.lists);
            else
                throw new InvalidOperationException("This type can't be cursored.");
        }
        
        public System.Collections.IEnumerator GetEnumerator()
        {
            return Result.GetEnumerator();
        }
        
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (Result as IEnumerable<T>).GetEnumerator();
        }

    }
}

