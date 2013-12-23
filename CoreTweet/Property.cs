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

namespace CoreTweet
{
    /// <summary>
    /// Properties of CoreTweet.
    /// </summary>
    public class Property
    {
        static string _apiversion = "1.1";
        /// <summary>
        /// The version of the Twitter API.
        /// To change this value is not recommended but allowed. 
        /// </summary>
        public static string ApiVersion { get { return _apiversion; } set { _apiversion = value; } }
        /// <summary>
        /// The version of CoreTweet.
        /// </summary>
        public static string Version { get { return "0.1 prealpha"; } }
        /// <summary>
        /// The authors.
        /// </summary>
        public static string[] Authors { get { return new []{"lambdalice"}; } }
        /// <summary>
        /// The license of CoreTweet.
        /// </summary>
        public static string License { get { return "The MIT License (MIT)"; } }
        /// <summary>
        /// The license text.
        /// </summary>
        public static string LicenseText 
        {
            get {
                return
@"The MIT License (MIT)

CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
Copyright (c) 2013 lambdalice

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the " + "\"" + "Software" + "\"" + @"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED " + "\"" + "AS IS" + "\"" + @", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.";
            } 
        }
        ///<summary>
        /// The URL you can get helps about CoreTweet.
        /// </summary>
        public static string HelpUrl { get { return "https://twitter.com/lambdalice"; } }
    }
}

