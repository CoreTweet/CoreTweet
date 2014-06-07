using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreTweet.Core
{
    /// <summary>
    /// Represents a response that has rate limit.
    /// </summary>
    public interface ITwitterResponse
    {
        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        RateLimit RateLimit { get; set; }
    }

    /*public class TwitterResponse<T> : ITwitterResponse
    {
        public T Value { get; set; }

        public RateLimit RateLimit { get; set; }
    }*/

    /// <summary>
    /// The collection of response.
    /// </summary>
    public class ListedResponse<T> : ITwitterResponse, IEnumerable<T>
#if NET45
    , IReadOnlyList<T>
#endif
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.Core.ListedResponse&lt;T&gt;"/> class with a specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new <see cref="ListedResponse&lt;T&gt;"/>.</param>
        public ListedResponse(List<T> collection)
        {
            this.innerList = collection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.Core.ListedResponse&lt;T&gt;"/> class with a specified collection and rate limit.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new <see cref="ListedResponse&lt;T&gt;"/>.</param>
        /// <param name="rateLimit">The rate limit.</param>
        public ListedResponse(List<T> collection, RateLimit rateLimit)
            : this(collection)
        {
            this.RateLimit = rateLimit;
        }

        private readonly List<T> innerList;

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets the number of elements actually contained in the <see cref="ListedResponse&lt;T&gt;"/>.
        /// </summary>
        public int Count
        {
            get
            {
                return this.innerList.Count;
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public T this[int index]
        {
            get
            {
                return this.innerList[index];
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
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
