// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2016 CoreTweet Development Team
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
using System.Diagnostics;
using System.Linq;

#if WIN_RT
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
#elif !PCL
using System.Security.Cryptography;
#endif

namespace CoreTweet.Core
{
    internal static class SecurityUtils
    {
#if PCL
        private static byte[] GetBytes(long value)
        {
            return new[]
            {
                (byte)(value >> 56),
                (byte)(value >> 48),
                (byte)(value >> 40),
                (byte)(value >> 32),
                (byte)(value >> 24),
                (byte)(value >> 16),
                (byte)(value >> 8),
                (byte)value
            };
        }

        private static byte[] GetBytes(uint value)
        {
            return new[]
            {
                (byte)(value >> 24),
                (byte)(value >> 16),
                (byte)(value >> 8),
                (byte)value
            };
        }

        private static uint ToUInt32(byte[] value, int startIndex)
        {
            return ((uint)value[startIndex] << 24)
                | ((uint)value[startIndex + 1] << 16)
                | ((uint)value[startIndex + 2] << 8)
                | value[startIndex + 3];
        }

        private static uint LeftRotate(uint x, int bits)
        {
            return x << bits | x >> (32 - bits);
        }

        internal static byte[] Sha1(IEnumerable<byte> message)
        {
            var msg = message.ToList();
            var ml = msg.Count * 8L;
            msg.Add(0x80);
            int bytesToAdd = 64 - (msg.Count % 64) - 8;
            if (bytesToAdd < 0)
                bytesToAdd += 64;
            msg.AddRange(new byte[bytesToAdd]);
            msg.AddRange(GetBytes(ml));

            uint h0 = 0x67452301, h1 = 0xEFCDAB89, h2 = 0x98BADCFE, h3 = 0x10325476, h4 = 0xC3D2E1F0;

            const int chunkSize = 64;
            var block = new byte[chunkSize];
            var w = new uint[80];
            var msglen = msg.Count;
            for(var i = 0; i < msglen; i += chunkSize)
            {
                msg.CopyTo(i, block, 0, chunkSize);

                for(var t = 0; t < 16; t++)
                    w[t] = ToUInt32(block, t * 4);

                for(var t = 16; t < 80; t++)
                    w[t] = LeftRotate(w[t - 3] ^ w[t - 8] ^ w[t - 14] ^ w[t - 16], 1);

                var a = h0;
                var b = h1;
                var c = h2;
                var d = h3;
                var e = h4;

                for(var t = 0; t < 80; t++)
                {
                    uint f;
                    if(t < 20)
                        f = ((b & c) | ((~b) & d)) + 0x5A827999;
                    else if(t < 40)
                        f = (b ^ c ^ d) + 0x6ED9EBA1;
                    else if(t < 60)
                        f = ((b & c) | (b & d) | (c & d)) + 0x8F1BBCDC;
                    else
                        f = (b ^ c ^ d) + 0xCA62C1D6;

                    var temp = LeftRotate(a, 5) + f + e + w[t];
                    e = d;
                    d = c;
                    c = LeftRotate(b, 30);
                    b = a;
                    a = temp;
                }

                h0 += a;
                h1 += b;
                h2 += c;
                h3 += d;
                h4 += e;
            }

            var result = new byte[20];
            Buffer.BlockCopy(GetBytes(h0), 0, result, 0, 4);
            Buffer.BlockCopy(GetBytes(h1), 0, result, 4, 4);
            Buffer.BlockCopy(GetBytes(h2), 0, result, 8, 4);
            Buffer.BlockCopy(GetBytes(h3), 0, result, 12, 4);
            Buffer.BlockCopy(GetBytes(h4), 0, result, 16, 4);
            return result;
        }
#endif

        internal static byte[] HmacSha1(IEnumerable<byte> key, IEnumerable<byte> message)
        {
#if PCL
            var k = key.ToArray();
            if(k.Length > 64)
                k = Sha1(k);
            if(k.Length != 64)
            {
                Debug.Assert(k.Length < 64);
                var tmp = new byte[64];
                Buffer.BlockCopy(k, 0, tmp, 0, k.Length);
                k = tmp;
            }

            var k_ipad = new byte[64];
            var k_opad = new byte[64];
            for(var i = 0; i < 64; i++)
            {
                k_ipad[i] = (byte)(k[i] ^ 0x36);
                k_opad[i] = (byte)(k[i] ^ 0x5C);
            }

            var inner = Sha1(message == null ? k_ipad : k_ipad.Concat(message));
            var x = new byte[64 + 20];
            Buffer.BlockCopy(k_opad, 0, x, 0, 64);
            Buffer.BlockCopy(inner, 0, x, 64, 20);
            return Sha1(x);
#else
            var keyArray = key as byte[] ?? key.ToArray();
            var messageArray = message as byte[] ?? (message?.ToArray() ?? new byte[] { });
#if WIN_RT
            var prov = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha1);
            var buffer = CryptographicEngine.Sign(
                prov.CreateKey(CryptographicBuffer.CreateFromByteArray(keyArray)),
                CryptographicBuffer.CreateFromByteArray(messageArray)
            );
            byte[] result;
            CryptographicBuffer.CopyToByteArray(buffer, out result);
            return result;
#else
            using (var hs1 = new HMACSHA1(keyArray))
                return hs1.ComputeHash(messageArray);
#endif
#endif
        }
    }
}
