// Copyright (c) 2013-2016 CoreTweet Development Team
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

#if ASYNC
using System.Threading;
using System.Threading.Tasks;
#endif

namespace CoreTweet
{
    internal static class EnumerableExtensions
    {
        internal static IEnumerable<string> EnumerateLines(this StreamReader streamReader)
        {
            while(!streamReader.EndOfStream)
                yield return streamReader.ReadLine();
        }

        internal static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach(T item in source)
                action(item);
        }

        internal static string JoinToString<T>(this IEnumerable<T> source)
        {
#if !NET35
            return string.Concat(source);
#else
            return string.Concat(source.Cast<object>().ToArray());
#endif
        }

        internal static string JoinToString<T>(this IEnumerable<T> source, string separator)
        {
#if !NET35
            return string.Join(separator, source);
#else
            return string.Join(separator, source.Select(x => x.ToString()).ToArray());
#endif
        }

        internal static IEnumerable<T> EndWith<T>(this IEnumerable<T> source, params T[] second)
        {
            return source.Concat(second);
        }

        internal static TResult[] ConvertAll<TSource, TResult>(this TSource[] source, Func<TSource, TResult> selector)
        {
#if NETCORE
            var result = new TResult[source.Length];
            for (var i = 0; i < source.Length; i++)
                result[i] = selector(source[i]);
            return result;
#else
            var converter = new Converter<TSource, TResult>(selector);
            return Array.ConvertAll(source, converter);
#endif
        }
    }

    internal static class DisposableExtensions
    {
        internal class Using<T>
            where T : IDisposable
        {
            internal T Source { get; }

            internal Using(T source)
            {
                Source = source;
            }

        }

        internal static Using<T> Use<T>(this T source)
            where T : IDisposable
        {
            return new Using<T>(source);
        }

        internal static TResult SelectMany<T, TSecond, TResult>
            (this Using<T> source, Func<T, Using<TSecond>> second, Func<T, TSecond, TResult> selector)
            where T : IDisposable
            where TSecond : IDisposable
        {
            using(source.Source)
            using(var s = second(source.Source).Source)
                return selector(source.Source, s);
        }

        internal static TResult Select<T, TResult>(this Using<T> source, Func<T, TResult> selector)
            where T : IDisposable
        {
            using(source.Source)
                return selector(source.Source);
        }
    }

#if SYNC
    internal static class StreamExtensions
    {
        internal static void WriteString(this Stream stream, string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
    }
#endif

#if NETCORE && !NETCOREAPP1_0
    internal static class TypeInfoExtensions
    {
        internal static IEnumerable<Type> GetInterfaces(this TypeInfo source)
        {
            return source.ImplementedInterfaces;
        }

        internal static PropertyInfo GetProperty(this TypeInfo source, string name)
        {
            return source.GetDeclaredProperty(name);
        }

        internal static MethodInfo GetGetMethod(this PropertyInfo source)
        {
            return source.GetMethod;
        }
    }
#endif


#if ASYNC
    internal struct Unit
    {
        internal static readonly Unit Default = new Unit();
    }

    internal static class TaskExtensions
    {
        internal static Task<TResult> Done<TSource, TResult>(this Task<TSource> source, Func<TSource, TResult> action, CancellationToken cancellationToken, TaskContinuationOptions options = TaskContinuationOptions.ExecuteSynchronously)
        {
            var tcs = new TaskCompletionSource<TResult>();
            source.ContinueWith(t =>
            {
                if (t.IsCanceled || cancellationToken.IsCancellationRequested)
                {
                    tcs.TrySetCanceled();
                    return;
                }

                if (t.Exception != null)
                {
                    tcs.TrySetException(t.Exception.InnerExceptions.Count == 1 ? t.Exception.InnerException : t.Exception);
                    return;
                }

                try
                {
                    tcs.TrySetResult(action(t.Result));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, options);
            return tcs.Task;
        }

        internal static Task Done<TSource>(this Task<TSource> source, Action<TSource> action, CancellationToken cancellationToken, TaskContinuationOptions options = TaskContinuationOptions.ExecuteSynchronously)
        {
            return source.Done(x =>
            {
                action(x);
                return Unit.Default;
            }, cancellationToken, options);
        }

        internal static Task<TResult> Done<TResult>(this Task source, Func<TResult> action, CancellationToken cancellationToken, TaskContinuationOptions options = TaskContinuationOptions.ExecuteSynchronously)
        {
            var tcs = new TaskCompletionSource<TResult>();
            source.ContinueWith(t =>
            {
                if (t.IsCanceled || cancellationToken.IsCancellationRequested)
                {
                    tcs.TrySetCanceled();
                    return;
                }

                if (t.Exception != null)
                {
                    tcs.TrySetException(t.Exception.InnerExceptions.Count == 1 ? t.Exception.InnerException : t.Exception);
                    return;
                }

                try
                {
                    tcs.TrySetResult(action());
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, options);
            return tcs.Task;
        }

        internal static Task Done(this Task source, Action action, CancellationToken cancellationToken, TaskContinuationOptions options = TaskContinuationOptions.ExecuteSynchronously)
        {
            return source.Done(() =>
            {
                action();
                return Unit.Default;
            }, cancellationToken, options);
        }
    }
#endif
}
