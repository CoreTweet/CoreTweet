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
using System.Net;
using CoreTweet.Core;

namespace CoreTweet
{
    public class Embed : CoreBase
    {   
        public string Html { get; set; }

        public string AuthorName { get; set; }
        
        public string AuthorUrl { get; set; }

        public string ProviderUrl{ get; set; }
  
        public string ProviderName{ get; set; }
        
        public Uri Url{ get; set; }

        public string Version { get; set; }

        public string Type{ get; set; }

        public int? Height{ get; set; }

        public int? Width{ get; set; }
        
        public string CacheAge{ get; set; }
        
        public Embed(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Html = e.html;
            AuthorName = e.author_name;
            AuthorUrl = e.author_url;
            ProviderName = e.provider_name;
            ProviderUrl = e.provider_url;
            Url = new Uri(e.url);
            Version = e.version;
            Type = e.type;
            Height = (int?)e.height;
            Width = (int?)e.Width;
            CacheAge = e.cache_age;
        }
    }
}

