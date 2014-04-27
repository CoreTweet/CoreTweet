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
        /// The version of the Twitter API.
        /// To change this value is not recommended but allowed. 
        /// </summary>
        public static string ApiVersion { get { return _apiversion; } set { _apiversion = value; } }
    }
}

