using System;

namespace CoreTweet
{
    /// <summary>
    /// Properties of CoreTweet.
    /// </summary>
    public class Property
    {
        static string _apiversion = "1.1";

        /// <summary>
        /// <para>Gets or sets the version of the Twitter API.</para>
        /// <para>To change this value is not recommended but allowed.</para>
        /// </summary>
        public static string ApiVersion
        {
            get { return _apiversion; }
            set { _apiversion = value; }
        }
    }
}

