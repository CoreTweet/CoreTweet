using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace CoreTweet.Tests
{
    static partial class Benchmarks
    {
        public static void ExpressionParameters()
        {
            Console.WriteLine("Benchmark #1: ExpressionParameters");
            Console.WriteLine("----------------------------------");

            var count = 1000;
            double A_direct = 0, A_local = 0, B_direct = 0, B_local = 0;

            foreach(var _ in Enumerable.Range(0, count))
            {
                A_direct += TimeOf(() => ExpressionsToDictionaryA(str => "abcdefghijk", val => 1234567890, call => TestFunc()).ToArray());
                B_direct += TimeOf(() => ExpressionsToDictionaryB(str => "abcdefghijk", val => 1234567890, call => TestFunc()).ToArray());

                var _str = "avcdefghijk";
                var _val = 1234567890;
                var _call = TestFunc();

                A_local += TimeOf(() => ExpressionsToDictionaryA(str => _str, val => _val, call => _call).ToArray());
                B_local += TimeOf(() => ExpressionsToDictionaryB(str => _str, val => _val, call => _call).ToArray());
            }

            Console.WriteLine("A_direct: {0}", A_direct / count);
            Console.WriteLine("B_direct: {0}", B_direct / count);
            Console.WriteLine("A_local : {0}", A_local / count);
            Console.WriteLine("B_local:  {0}", B_local / count);
            Console.WriteLine();
        }

        static string TestFunc()
        {
            return "qawsedrftgyhujikolp";
        }

        static object GetExpressionValue(Expression<Func<string, object>> expr)
        {
            var constExpr = expr.Body as ConstantExpression;
            return constExpr != null ? constExpr.Value : expr.Compile()("");
        }

        static IEnumerable<KeyValuePair<string, object>> ExpressionsToDictionaryA(params Expression<Func<string, object>>[] exprs)
        {
            return exprs.Select(x => new KeyValuePair<string, object>(x.Parameters[0].Name, GetExpressionValue(x)));
        }

        static IEnumerable<KeyValuePair<string, object>> ExpressionsToDictionaryB(params Expression<Func<string, object>>[] exprs)
        {
            return exprs.Select(x => new KeyValuePair<string, object>(x.Parameters[0].Name, x.Compile()("")));
        }
    }
}
