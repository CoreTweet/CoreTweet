using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreTweet.Tests
{
    static partial class ApiTests
    {
        public static void StatusesUpdate()
        {
            var text =

                "[CoreTweet.Test::net45]"
#else
                ""

                + " これは実際 API のテストです。ごあんしんください ()!*'" + new Random().Next().ToString("X") + " [based on RFC3986]";
            Tokens.Statuses.Update(status => text);
        }
    }
}
