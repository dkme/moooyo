using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CBB.OAuth.Sina.Common
{
    /// <summary>
    /// Defines constants used in CBB.OAuth.Sina.
    /// </summary>
    internal static class Constants
    {
        internal const string OAuthHeaderPrefix = "OAuth ";

        internal const string PostContentType = "application/x-www-form-urlencoded";

        internal const string PostMultiPartContentType = "multipart/form-data; boundary={0}";

        internal const string RetrieveRequestTokenPattern = "^oauth_token=(.+?)&oauth_token_secret=(.+?)$";

        internal const string RetrieveAccessTokenPattern = "^oauth_token=(.+?)&oauth_token_secret=(.+?)(&user_id=(.+?)){0,1}(&screen_name=(.+?)){0,1}$";

        internal const string RetrieveAuthorizationCodePattern = "<oauth_verifier>(.+?)</oauth_verifier>";

        internal const string XmlHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

        internal const string Source = "source";

        internal const string OAuthConsumerKey = "oauth_consumer_key";
        internal const string OAuthSignatureMethod = "oauth_signature_method";
        internal const string OAuthTimestamp = "oauth_timestamp";
        internal const string OAuthNonce = "oauth_nonce";
        internal const string OAuthVersion = "oauth_version";
        public static string OAuthCallback = "oauth_callback";
        internal const string OAuthSignature = "oauth_signature";
        internal const string OAuthToken = "oauth_token";
        internal const string OAuthVerifier = "oauth_verifier";
    }
}
