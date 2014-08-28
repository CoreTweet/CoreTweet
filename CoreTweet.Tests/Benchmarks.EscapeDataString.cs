using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CoreTweet.Tests
{
    static partial class Benchmarks
    {
        public static void EscapeDataString()
        {
            Console.WriteLine("Benchmark #2: EscapeDataString");
            Console.WriteLine("------------------------------");
            var count = 10000;
            var tweets = new[]
            {
                "あいうえおか()きくけこ!*L>plujefqnef'",
                "あいうえおか()きくけこ!*L>plujefqnef'",
                "あいうえおか()きくけこ!*L>plujefqnef'",
                "あいうえおか()きくけこ!*L>plujefqnef'",
                "あいうえおか()きくけこ!*L>plujefqnef'",
                "あいうえおか()きくけこ!*L>plujefqnef'",
                "あいうえおか()きくけこ!*L>plujefqnef'",
                "あいうえおか()きくけこ!*L>plujefqnef'",
                "あいうえおか()きくけこ!*L>plujefqnef'",
                "あいうえおか()きくけこ!*L>plujefqnef'",
            };
            double esc_1 = 0, esc_2 = 0, esc_3 = 0, esc_4 = 0;

            foreach (var _ in Enumerable.Range(0, count))
            {
                esc_1 += TimeOf(() =>
                {
                    foreach (var s in tweets)
                        EscapeDataString1(s);
                });
                esc_2 += TimeOf(() =>
                {
                    foreach (var s in tweets)
                        EscapeDataString2(s);
                });
                esc_3 += TimeOf(() =>
                {
                    foreach (var s in tweets)
                        EscapeDataString3(s);
                });
                esc_4 += TimeOf(() =>
                {
                    foreach (var s in tweets)
                        EscapeDataString4(s);
                });
            }

            Console.WriteLine("esc_1: {0}", esc_1 / count);
            Console.WriteLine("esc_2: {0}", esc_2 / count);
            Console.WriteLine("esc_3: {0}", esc_3 / count);
            Console.WriteLine("esc_4: {0}", esc_4 / count);
            Console.WriteLine();
        }

        static readonly string[] reserved = new []{ "(", ")", "*", "!", "'" };

        static string EscapeDataString1(string text)
        {
            text = Uri.EscapeDataString(text);
            var sb = new StringBuilder(text);
            for (var i = 0; i < reserved.Length; i++)
            {
                sb.Replace(reserved[i], Uri.HexEscape(reserved[i][0]));
            }
            return sb.ToString();
        }

        static string EscapeDataString2(string text)
        {
            text = Uri.EscapeDataString(text);
            for (var i = 0; i < reserved.Length; i++)
            {
                text.Replace(reserved[i], Uri.HexEscape(reserved[i][0]));
            }
            return text;
        }

        static readonly string reserved_str = "()*!'";

        static string EscapeDataString3(string text)
        {
            text = Uri.EscapeDataString(text);
            var sb = new StringBuilder(text);
            foreach(var s in reserved_str)
            {
                sb.Replace(s.ToString(), Uri.HexEscape(s));
            }
            return sb.ToString();
        }
        static string EscapeDataString4(string text)
        {
            text = Uri.EscapeDataString(text);
            foreach(var s in reserved_str)
            {
                text.Replace(s.ToString(), Uri.HexEscape(s));
            }
            return text;
        }
    }
}
