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
using System.Collections.Generic;
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
            var b = BitConverter.GetBytes(value);
            if(BitConverter.IsLittleEndian)
                Array.Reverse(b);
            return b;
        }

        private static byte[] GetBytes(uint value)
        {
            var b = BitConverter.GetBytes(value);
            if(BitConverter.IsLittleEndian)
                Array.Reverse(b);
            return b;
        }

        private static uint ToUInt32(byte[] value, int startIndex)
        {
            if(BitConverter.IsLittleEndian)
            {
                var b = new byte[4];
                Array.Copy(value, startIndex, b, 0, 4);
                Array.Reverse(b);
                return BitConverter.ToUInt32(b, 0);
            }
            else
            {
                return BitConverter.ToUInt32(value, startIndex);
            }
        }

        private static uint LeftRotate(uint x, int bits)
        {
            return x << bits | x >> (32 - bits);
        }

        internal static byte[] Sha1(IEnumerable<byte> message)
        {
            var msgList = message.ToList();
            var ml = msgList.Count * 8L;
            msgList.Add(0x80);
            int bytesToAdd = 64 - (msgList.Count % 64) - 8;
            if (bytesToAdd < 0)
                bytesToAdd += 64;
            msgList.AddRange(Enumerable.Repeat((byte)0, bytesToAdd));
            msgList.AddRange(GetBytes(ml));
            var msg = msgList.ToArray();

            var h = new uint[] { 0x67452301, 0xEFCDAB89, 0x98BADCFE, 0x10325476, 0xC3D2E1F0 };

            const int chunkSize = 64;
            var block = new byte[chunkSize];
            var w = new uint[80];
            for(var i = 0; i < msg.Length; i += chunkSize)
            {
                Array.Copy(msg, i, block, 0, chunkSize);

                for(var t = 0; t < 16; t++)
                    w[t] = ToUInt32(block, t * 4);

                for(var t = 16; t < 80; t++)
                    w[t] = LeftRotate(w[t - 3] ^ w[t - 8] ^ w[t - 14] ^ w[t - 16], 1);

                var a = h[0];
                var b = h[1];
                var c = h[2];
                var d = h[3];
                var e = h[4];

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

                h[0] += a;
                h[1] += b;
                h[2] += c;
                h[3] += d;
                h[4] += e;
            }

            return h.SelectMany(GetBytes).ToArray();
        }
#endif

        internal static byte[] HmacSha1(IEnumerable<byte> key, IEnumerable<byte> message)
        {
#if PCL
            var k = key.ToList();
            if(k.Count > 64)
                k = Sha1(k).ToList();
            k.AddRange(Enumerable.Repeat((byte)0, 64 - k.Count));

            var ipad = Enumerable.Repeat((byte)0x36, 64);
            var opad = Enumerable.Repeat((byte)0x5C, 64);

            var inner = Sha1(k.Zip(ipad, (x, y) => (byte)(x ^ y)).Concat(message ?? Enumerable.Empty<byte>()));
            return Sha1(k.Zip(opad, (x, y) => (byte)(x ^ y)).Concat(inner));
#else
            var keyArray = key as byte[];
            if(keyArray == null) keyArray = key.ToArray();
            var messageArray = message as byte[];
                if(messageArray == null)
                    messageArray = message != null
                        ? message.ToArray()
                        : new byte[] { };
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
