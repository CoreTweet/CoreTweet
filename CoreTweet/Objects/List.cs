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
    public class List : CoreBase
    { 
        public string Slug{ get; set; }

        public string Name{ get; set; }

        public DateTimeOffset CreatedAt{ get; set; }

        public Uri Uri{ get; set; }

        public int SubscriberCount { get; set; }

        public int MemberCount{ get; set; }

        public long Id{ get; set; }
        
        public string Mode{ get; set; }

        public string FullName{ get; set; }
        
        public string Description{ get; set; }

        public User User{ get; set; }

        public bool Following{ get; set; }
        
        public List(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Slug = e.slug;
            Name = e.name;
            CreatedAt = DateTimeOffset.ParseExact(e.created_at, "ddd MMM dd HH:mm:ss K yyyy",
                                                  System.Globalization.DateTimeFormatInfo.InvariantInfo, 
                                                  System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            Uri = new Uri(e.uri);
            SubscriberCount = e.subsuriber_count;
            MemberCount = e.member_count;
            Id = (long)e.id;
            Mode = e.mode;
            FullName = e.full_name;
            Description = e.description;
            User = CoreBase.Convert<User>(this.Tokens, e.user);
            Following = e.following;
        }
    }
}
