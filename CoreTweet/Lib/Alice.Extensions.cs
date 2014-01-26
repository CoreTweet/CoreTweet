using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Alice.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> And<T>(this IEnumerable<T> source, IEnumerable<T> target)
        {
            return
                from x in source
                from y in target
                where x.Equals(y)
                select x;
        }

        public static IEnumerable<Tuple<T1, T2>> Conbinate<T1, T2>(this IEnumerable<T1> source, IEnumerable<T2> target)
        {
            return
                from x in source
                from y in target
                select Tuple.Create(x, y);
        }

        public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach(T item in source)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<string> EnumerateLines(this StreamReader streamReader)
        {
            while(!streamReader.EndOfStream)
                yield return streamReader.ReadLine();
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach(T item in source)
                action(item);
        }

        public static string JoinToString<T>(this IEnumerable<T> source)
        {
            return string.Concat<T>(source);
        }

        public static string JoinToString<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join<T>(separator, source);
        }

        public static IEnumerable<T> Or<T>(this IEnumerable<T> source, IEnumerable<T> target)
        {
            return source.Union(target);
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            IEnumerable<T> x = new T[0];
            var randomize = new Random();
            while(source.Any())
            {
                var rand = randomize.Next(source.Count());
                x = x.Concat(new[] { source.Skip(rand).First() });
                source = source.Skip(rand, 1);
            }
            return x;
        }

        public static IEnumerable<T> SkipWhile<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
        {
            var cnt = 0;
            foreach(var x in source)
            {
                if (!predicate(x, cnt))
                    yield return x;
                cnt++;
            }
        }

        public static IEnumerable<T> Skip<T>
            (this IEnumerable<T> source, int start, int range)
        {
            return SkipWhile(source, (_, x) => x >= start && x < start + range);
        }

        public static IEnumerable<IEnumerable<T>> Split<T>
            (this IEnumerable<T> source, params T[] separator)
        {
            return Split(source, separator.Contains);
        }

        public static IEnumerable<IEnumerable<T>> Split<T>
            (this IEnumerable<T> source, T[] separator, StringSplitOptions options)
        {
            return Split(source, separator.Contains)
                  .Where(x => options == StringSplitOptions.RemoveEmptyEntries ? x.Any() : true);
        }

        public static IEnumerable<IEnumerable<T>> Split<T>
            (this IEnumerable<T> source, Func<T, bool> separator)
        {
            IEnumerable<T> x = new T[0];
            var count = 0;
            foreach(var t in source)
            {
                count++;
                if (separator(t))
                {
                    yield return x;
                    x = new T[0];
                }
                else
                {
                    x = x.Concat(new T[] { t });
                    if (!source.Skip(count).Any())
                        yield return x;
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Split<T>
            (this IEnumerable<T> source, Func<T, bool> separator, StringSplitOptions options)
        {
            return Split(source, separator)
                  .Where(x => options == StringSplitOptions.RemoveEmptyEntries ? x.Any() : true);
        }


        public static IEnumerable<T> Xor<T>(this IEnumerable<T> source, IEnumerable<T> target)
        {
            return source.Where(x => !target.Any(y => y.Equals(x)))
                    .Concat(target.Where(x => !source.Any(y => y.Equals(x))));
        }

    }

    public static class StringExtension
    {

        public static IEnumerable<string> Split(this string source, params Regex[] separator)
        {
            return Split(source, separator, StringSplitOptions.None);
        }

        public static IEnumerable<string> Split
            (this string source, IEnumerable<Regex> separator, StringSplitOptions options)
        {
            return
                from x in separator
                from y in ToEnumerable(x.Matches(source))
                select y.Value;
        }

        static IEnumerable<Match> ToEnumerable(this MatchCollection source)
        {
            foreach(var x in source)
                yield return (Match)x;
        }
    }

    public static class DisposableExtensions
    {
        public class Using<T>
            where T : IDisposable
        {
            public T Source { get; set; }

            public Using(T source)
            {
                Source = source;
            }

        }

        public static Using<T> Use<T>(this T source)
            where T : IDisposable
        {
            return new Using<T>(source);
        }

        public static TResult SelectMany<T, TSecond, TResult>
            (this Using<T> source, Func<T, Using<TSecond>> second, Func<T, TSecond, TResult> selector)
            where T : IDisposable
            where TSecond : IDisposable
        {
            using(source.Source)
            using(var s = second(source.Source).Source)
                return selector(source.Source, s);
        }

        public static TResult Select<T, TResult>(this Using<T> source, Func<T, TResult> selector)
            where T : IDisposable
        {
            using(source.Source)
                return selector(source.Source);
        }
    }
}

