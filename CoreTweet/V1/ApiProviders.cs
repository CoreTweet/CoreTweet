// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2018 CoreTweet Development Team
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this.Tokens software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this.Tokens permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using CoreTweet.Core;
using CoreTweet.V1.Streaming;
using CoreTweet.V1.AccountActivity;

namespace CoreTweet.V1
{
    public class V1Api : ApiProviderBase
    {
        internal V1Api(TokensBase e) : base(e) {}

        /// <summary>
        /// Gets the wrapper of account.
        /// </summary>
        public Account Account => new Account(this.Tokens);

        /// <summary>
        /// Gets the wrapper of application.
        /// </summary>
        public Application Application => new Application(this.Tokens);

        /// <summary>
        /// Gets the wrapper of blocks.
        /// </summary>
        public Blocks Blocks => new Blocks(this.Tokens);

        /// <summary>
        /// Gets the wrapper of collections.
        /// </summary>
        public Collections Collections => new Collections(this.Tokens);

        /// <summary>
        /// Gets the wrapper of direct_messages.
        /// </summary>
        public DirectMessages DirectMessages => new DirectMessages(this.Tokens);

        /// <summary>
        /// Gets the wrapper of favorites.
        /// </summary>
        public Favorites Favorites => new Favorites(this.Tokens);

        /// <summary>
        /// Gets the wrapper of friends.
        /// </summary>
        public Friends Friends => new Friends(this.Tokens);

        /// <summary>
        /// Gets the wrapper of followers.
        /// </summary>
        public Followers Followers => new Followers(this.Tokens);

        /// <summary>
        /// Gets the wrapper of friendships.
        /// </summary>
        public Friendships Friendships => new Friendships(this.Tokens);

        /// <summary>
        /// Gets the wrapper of geo.
        /// </summary>
        public Geo Geo => new Geo(this.Tokens);

        /// <summary>
        /// Gets the wrapper of help.
        /// </summary>
        public Help Help => new Help(this.Tokens);

        /// <summary>
        /// Gets the wrapper of lists.
        /// </summary>
        public Lists Lists => new Lists(this.Tokens);

        /// <summary>
        /// Gets the wrapper of media.
        /// </summary>
        public Media Media => new Media(this.Tokens);

        /// <summary>
        /// Gets the wrapper of mutes.
        /// </summary>
        public Mutes Mutes => new Mutes(this.Tokens);

        /// <summary>
        /// Gets the wrapper of search.
        /// </summary>
        public Search Search => new Search(this.Tokens);

        /// <summary>
        /// Gets the wrapper of saved_searches.
        /// </summary>
        public SavedSearches SavedSearches => new SavedSearches(this.Tokens);

        /// <summary>
        /// Gets the wrapper of statuses.
        /// </summary>
        public Statuses Statuses => new Statuses(this.Tokens);

        /// <summary>
        /// Gets the wrapper of trends.
        /// </summary>
        public Trends Trends => new Trends(this.Tokens);

        /// <summary>
        /// Gets the wrapper of tweets.
        /// </summary>
        public Tweets Tweets => new Tweets(this.Tokens);

        /// <summary>
        /// Gets the wrapper of users.
        /// </summary>
        public Users Users => new Users(this.Tokens);

        /// <summary>
        /// Gets the wrapper of the Streaming API.
        /// </summary>
        public StreamingApi Streaming => new StreamingApi(this.Tokens);

        /// <summary>
        /// Gets the wrapper of the Account Activity API.
        /// </summary>
        public AccountActivityApi AccountActivity => new AccountActivityApi(this.Tokens);
    }
}