using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RestApisGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reading API templates");

            var apis = Directory.EnumerateDirectories("ApiTemplates")
                .SelectMany(path => Directory.EnumerateFiles(path).Select(fileName => ApiParent.Parse(fileName, $"\"{path.Split(Path.DirectorySeparatorChar).LastOrDefault()}\"")));

            Console.WriteLine("Generating RestApis.cs");
            using (var writer = new StreamWriter(Path.Combine("CoreTweet.Shared", "RestApis.cs")))
            {
                RestApisCs.Generate(apis, writer);
            }
        }
    }
}
