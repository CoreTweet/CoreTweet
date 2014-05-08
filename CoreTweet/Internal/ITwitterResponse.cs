using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreTweet.Core
{
    public interface ITwitterResponse
    {
        RateLimit RateLimit { get; set; }
    }

    /*public class TwitterResponse<T> : ITwitterResponse
    {
        public T Value { get; set; }

        public RateLimit RateLimit { get; set; }
    }*/

    public class ListedResponse<T> : ITwitterResponse, IEnumerable<T>
#if NET45
    , IReadOnlyList<T>
#endif
    {
        public ListedResponse(List<T> collection)
        {
            this.innerList = collection;
        }

        public ListedResponse(List<T> collection, RateLimit rateLimit)
            : this(collection)
        {
            this.RateLimit = rateLimit;
        }

        private readonly List<T> innerList;

        public RateLimit RateLimit { get; set; }

        public int Count
        {
            get
            {
                return this.innerList.Count;
            }
        }

        public T this[int index]
        {
            get
            {
                return this.innerList[index];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.innerList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
