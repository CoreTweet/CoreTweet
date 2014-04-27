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

        static bool _ignoreCertificates = false;

        /// <summary>
        /// <para>Determine whether CoreTweet ignores any SSL certificates or don't.</para>
        /// </summary>
        /// <remarks>
        /// <para>The default value is false. This property is for Mono user who don't have a right to import trusted root certificates.</para>
        /// <para>Use mozroots to import certificates if you can. If you wish to set this to true, do it at your own risk.</para>
        /// </remarks>
        /// <see cref="http://mono-project.com/FAQ:_Security"/>
        /// <value><c>true</c> if ignore certificates; otherwise, <c>false</c>.</value>
        public static bool IgnoreCertificates { get { return _ignoreCertificates; } set { _ignoreCertificates = value; } }
    }
}

