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
using System.Linq;

#if WIN_RT
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
#elif !OWN_HMAC
using System.Security.Cryptography;
#endif

namespace CoreTweet.Core
{
    internal static class SecurityUtils
    {
#if OWN_HMAC
        private static void SetBytes(long value, byte[] buffer, int index)
        {
            buffer[index] = (byte)(value >> 56);
            buffer[index + 1] = (byte)(value >> 48);
            buffer[index + 2] = (byte)(value >> 40);
            buffer[index + 3] = (byte)(value >> 32);
            buffer[index + 4] = (byte)(value >> 24);
            buffer[index + 5] = (byte)(value >> 16);
            buffer[index + 6] = (byte)(value >> 8);
            buffer[index + 7] = (byte)value;
        }

        private static void SetBytes(uint value, byte[] buffer, int index)
        {
            buffer[index] = (byte)(value >> 24);
            buffer[index + 1] = (byte)(value >> 16);
            buffer[index + 2] = (byte)(value >> 8);
            buffer[index + 3] = (byte)value;
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

        private static int ComputeBufferSize(int messageSize)
        {
            messageSize += 9; // 0x80 + ml
            var paddingSize = 64 - (messageSize % 64);
            if (paddingSize == 64) paddingSize = 0;
            return messageSize + paddingSize;
        }

        private static byte[] PrivateSha1(byte[] buffer, int messageSize)
        {
#if DEBUG
            if (buffer.Length != ComputeBufferSize(messageSize))
                throw new ArgumentException();
#endif

            buffer[messageSize] = 0x80;
            SetBytes(messageSize * 8L, buffer, buffer.Length - 8);

            uint h0 = 0x67452301, h1 = 0xEFCDAB89, h2 = 0x98BADCFE, h3 = 0x10325476, h4 = 0xC3D2E1F0;

            const int chunkSize = 64;
            var w = new uint[80];
            for(var i = 0; i < buffer.Length; i += chunkSize)
            {
                for(var t = 0; t < 16; t++)
                    w[t] = ToUInt32(buffer, i + t * 4);

                for(var t = 16; t < 80; t++)
                    w[t] = LeftRotate(w[t - 3] ^ w[t - 8] ^ w[t - 14] ^ w[t - 16], 1);

                uint a = h0, b = h1, c = h2, d = h3, e = h4;

                for(var t = 0; t < 80; t++)
                {
                    var f = t < 20 ? ((b & c) | ((~b) & d)) + 0x5A827999
                        : t < 40 ? (b ^ c ^ d) + 0x6ED9EBA1
                        : t < 60 ? ((b & c) | (b & d) | (c & d)) + 0x8F1BBCDC
                        : (b ^ c ^ d) + 0xCA62C1D6;

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
            SetBytes(h0, result, 0);
            SetBytes(h1, result, 4);
            SetBytes(h2, result, 8);
            SetBytes(h3, result, 12);
            SetBytes(h4, result, 16);
            return result;
        }
#endif

        internal static byte[] HmacSha1(byte[] key, byte[] message)
        {
#if OWN_HMAC
            if(key.Length > 64)
            {
                var tmp = new byte[ComputeBufferSize(key.Length)];
                Buffer.BlockCopy(key, 0, tmp, 0, key.Length);
                key = PrivateSha1(tmp, key.Length);
            }
            if(key.Length != 64)
            {
                var tmp = new byte[64];
                Buffer.BlockCopy(key, 0, tmp, 0, key.Length);
                key = tmp;
            }

            var innerLength = 64 + message.Length;
            var inner = new byte[ComputeBufferSize(innerLength)];

            const int outerLength = 64 + 20;
            var outer = new byte[ComputeBufferSize(outerLength)];

            for(var i = 0; i < 64; i++)
            {
                inner[i] = (byte)(key[i] ^ 0x36);
                outer[i] = (byte)(key[i] ^ 0x5C);
            }

            Buffer.BlockCopy(message, 0, inner, 64, message.Length);
            var innerHash = PrivateSha1(inner, innerLength);

            Buffer.BlockCopy(innerHash, 0, outer, 64, 20);
            return PrivateSha1(outer, outerLength);
#elif WIN_RT
            var prov = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha1);
            var buffer = CryptographicEngine.Sign(
                prov.CreateKey(CryptographicBuffer.CreateFromByteArray(key)),
                CryptographicBuffer.CreateFromByteArray(message)
            );
            byte[] result;
            CryptographicBuffer.CopyToByteArray(buffer, out result);
            return result;
#else
            using (var hs1 = new HMACSHA1(key))
                return hs1.ComputeHash(message);
#endif
        }
    }
}
