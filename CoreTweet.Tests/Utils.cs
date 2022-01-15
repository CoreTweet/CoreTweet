using System;

namespace CoreTweet.Tests
{
    static class Debug
    {
        static string GetVersion()
        {
            #if NETCOREAPP
            return "netcore:" + Environment.Version.ToString();
            #elif NET45
            return "netframework:45";
            #elif NET461
            return "netframework:461";
            #else
            return "unknown";
            #endif
        }
        public static void WriteLine(string format, params Object[] args)
        {
            Console.WriteLine(String.Format("[{0}] ", GetVersion()) + format, args);
        }

        public static void WriteLine()
        {
            Console.WriteLine();
        }
    }
}
