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
        internal static byte[] Sha1(IEnumerable<byte> message)
        {
            Func<uint, uint, uint, uint> f00 = (b, c, d) => (b & c) | ((~b) & d);
            Func<uint, uint, uint, uint> f20 = (b, c, d) => b ^ c ^ d;
            Func<uint, uint, uint, uint> f40 = (b, c, d) => (b & c) | (b & d) | (c & d);
            var f60 = f20;

            var K = new uint[]
            {
                0x5A827999,   
                0x6ED9EBA1,   
                0x8F1BBCDC,   
                0xCA62C1D6
            };

            uint H0 = 0x67452301;
            uint H1 = 0xEFCDAB89;
            uint H2 = 0x98BADCFE;
            uint H3 = 0x10325476;
            uint H4 = 0xC3D2E1F0;

            uint A, B, C, D, E;
            var W = new uint[80];
            uint TEMP;

            var msg = message.ToList();
            var ml = msg.Count * 8L;
            msg.Add(0x80);
            msg.AddRange(Enumerable.Repeat((byte)0, 64 - (msg.Count % 64) - 8));
            msg.AddRange(BitConverter.GetBytes(ml).Reverse());

            for(var i = 0; i < msg.Count; i += 64)
            {
                var block = msg.Skip(i).Take(64).ToArray();

                for(var t = 0; t < 16; t++)
                    W[t] = BitConverter.ToUInt32(new[] { block[t * 4 + 3], block[t * 4 + 2], block[t * 4 + 1], block[t * 4] }, 0);

                for(var t = 16; t < 80; t++)
                {
                    var x = W[t - 3] ^ W[t - 8] ^ W[t - 14] ^ W[t - 16];
                    W[t] = x << 1 | x >> 31;
                }

                A = H0;
                B = H1;
                C = H2;
                D = H3;
                E = H4;

                for(var t = 0; t < 20; t++)
                {
                    TEMP = (A << 5 | A >> 27) + f00(B, C, D) + E + W[t] + K[0];
                    E = D;
                    D = C;
                    C = B << 30 | B >> 2;
                    B = A;
                    A = TEMP;
                }

                for(var t = 20; t < 40; t++)
                {
                    TEMP = (A << 5 | A >> 27) + f20(B, C, D) + E + W[t] + K[1];
                    E = D;
                    D = C;
                    C = B << 30 | B >> 2;
                    B = A;
                    A = TEMP;
                }

                for(var t = 40; t < 60; t++)
                {
                    TEMP = (A << 5 | A >> 27) + f40(B, C, D) + E + W[t] + K[2];
                    E = D;
                    D = C;
                    C = B << 30 | B >> 2;
                    B = A;
                    A = TEMP;
                }

                for(var t = 60; t < 80; t++)
                {
                    TEMP = (A << 5 | A >> 27) + f60(B, C, D) + E + W[t] + K[3];
                    E = D;
                    D = C;
                    C = B << 30 | B >> 2;
                    B = A;
                    A = TEMP;
                }

                H0 += A;
                H1 += B;
                H2 += C;
                H3 += D;
                H4 += E;
            }

            return new[] { H0, H1, H2, H3, H4 }.SelectMany(x => BitConverter.GetBytes(x).Reverse()).ToArray();
        }

        //internal static string Sha1Hex(IEnumerable<byte> message)
        //{
        //    return string.Concat(Sha1(message)
        //        .Select(b => b.ToString("x"))
        //        .Select(x => x.Length == 1 ? ("0" + x) : x)
        //    );
        //}
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
