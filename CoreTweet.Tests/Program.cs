﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using CoreTweet;

namespace CoreTweet.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
#if BENCH
            Debug.WriteLine("Starting benchmarks.");
            DoBenchMarks();
#else
            Debug.WriteLine("Starting tests.");
            ApiTests.SetupTokens();
            ApiTests.StatusesUpdate();
#endif

        }

        static void DoBenchMarks()
        {
            Benchmarks.ExpressionParameters();
            Benchmarks.EscapeDataString();
        }
    }

    static partial class Benchmarks
    {
        static Stopwatch stopwatch = new Stopwatch();

        static double TimeOf(Action action)
        {

            stopwatch.Restart();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }
    }

    public class Data
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessSecret { get; set; }
        public string Bearer { get; set; }
    }

    public static partial class ApiTests
    {
        static string SettingPath = "tokens.xml";
        public static Tokens Tokens { get; set; }
        public static OAuth2Token ApponlyToken { get; set; }

        public static void SetupTokens()
        {
            if (File.Exists(SettingPath))
                LoadSettings();
            else
            {
                Console.Write("Input consumer_key: ");
                var ckey = Console.ReadLine();
                Console.Write("Input consumer_secret: ");
                var csecret = Console.ReadLine();
                Console.WriteLine("Open: ");
                var session = OAuth.Authorize(ckey, csecret);
                Console.WriteLine(session.AuthorizeUri);
                Console.Write("Input PIN: ");
                Tokens = session.GetTokens(Console.ReadLine());
                ApponlyToken = OAuth2.GetToken(ckey, csecret);
                SaveSettings();
            }
        }
        static void LoadSettings()
        {
            var x = new XmlSerializer(typeof(Data));
            var data = (Data)x.Deserialize(File.OpenRead(SettingPath));
            Tokens = Tokens.Create(data.ConsumerKey, data.ConsumerSecret, data.AccessToken, data.AccessSecret);
            ApponlyToken = OAuth2Token.Create(data.ConsumerKey, data.ConsumerSecret, data.Bearer);
        }

        static void SaveSettings()
        {
            var x = new XmlSerializer(typeof(Data));
            x.Serialize(File.OpenWrite(SettingPath), new Data()
            {
                ConsumerKey = Tokens.ConsumerKey,
                ConsumerSecret = Tokens.ConsumerSecret,
                AccessToken = Tokens.AccessToken,
                AccessSecret = Tokens.AccessTokenSecret,
                Bearer = ApponlyToken.BearerToken
            });
        }
    }
}
