using System;
using System.Diagnostics;

namespace CBB.OAuth.Sina.Common
{
    /// <summary>
    /// Represents the OAuth requst token.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Token:{Token},Secret:{Secret}")]
    public class OAuthRequestToken
    {
        /// <summary>
        /// Gets or sets the token field.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the token secret field.
        /// </summary>
        public string Secret { get; set; }
    }

    /// <summary>
    /// Represents the OAuth access token.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("UserID:{UserID}")]
    public class OAuthAccessToken : OAuthRequestToken
    {
        /// <summary>
        /// Gets or sets the user id field.
        /// </summary>
        public string UserID { get; set; }
    }
}
