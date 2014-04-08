// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2014 lambdalice
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
    /// Twitter parameter attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class TwitterParameterAttribute : Attribute
    {
        /// <summary>
        /// Name of the parameter binding for.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Default value of the parameter.
        /// </summary>
        /// <value>The default value.</value>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.TwitterParameterAttribute"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="defaultValue">Default value.</param>
        public TwitterParameterAttribute(string name = null, object defaultValue = null)
        {
            Name = name;
            DefaultValue = defaultValue;
        }
    }

    /// <summary>
    /// Twitter parameters attribute.
    /// This is used for a class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TwitterParametersAttribute : Attribute {}
}

