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
using Codeplex.Data;
using CoreTweet.Core;

namespace CoreTweet
{
    public class Configurations : CoreBase
    {
        public int CharactersReservedPerMedia { get; set; }

        public int MaxMediaPerUpload{ get; set; }
        
        public string[] NonUsernamePaths{ get; set; }
        
        public int PhotoSizeLimit{ get; set; }
        
        public int ShortUrlLength{ get; set; }
        
        public int ShortUrlLengthHttps{ get; set; }
        
        public Sizes PhotoSizes{ get; set; }
        
        public Configurations(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            CharactersReservedPerMedia = e.characters_reserved_per_media;
            MaxMediaPerUpload = e.max_media_per_upload;
            NonUsernamePaths = e.non_username_paths;
            PhotoSizeLimit = e.photo_size_limit;
            ShortUrlLength = e.short_url_length;
            ShortUrlLengthHttps = e.short_url_length_https;
            PhotoSizes = CoreBase.Convert<Sizes>(this.Tokens, e.photo_sizes);
        }
    }
    
    public class Language : CoreBase
    {
        public string Code{ get; set; }

        public string Name{ get; set; }

        public string Status{ get; set; }
        
        public Language(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Code = e.code;
            Name = e.name;
            Status = e.status;
        }
    }
}

