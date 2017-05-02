using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace RestApisGen
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 1 && args[0] == "debug")
            {
                var api = ApiParent.Parse(args[1]);
                foreach(var e in api.Endpoints)
                    if(e.JsonMap != null && e.JsonMap.Length > 0)
                    {
                        var dic = new Dictionary<string, object>();
                        dic.Add("recipient_id", 1234);
                        dic.Add("quick_reply_type", "test");
                        var jm =
                            e.JsonMap
                             .Select(x => 
                                {
                                    if(!x.Contains("$"))
                                        return x;
                                    var s = x;
                                    foreach(var i in dic)
                                        s = s.Replace("$" + i.Key, "\"" + i.Value + "\"");
                                    return s;
                                }
                             )
                             .Where(x => !x.Contains("$"))
                             .JoinToString(Environment.NewLine);
                        
                        var jt = JToken.Parse(jm);
                        
                        Console.WriteLine(jt.RemoveEmptyObjects(true));
                    }
                //RestApisCs.Generate(new []{ api }, new StreamWriter(Console.OpenStandardOutput()));
                return;
            }
            Console.WriteLine("Reading API templates");
            var apis = Directory.GetFiles("ApiTemplates").Where(x => !x.Contains("test.api"))
                .Select(ApiParent.Parse);

            Console.WriteLine("Generating RestApis.cs");
            using (var writer = new StreamWriter(Path.Combine("CoreTweet.Shared", "RestApis.cs")))
            {
                RestApisCs.Generate(apis, writer);
            }
        }
    }
}
