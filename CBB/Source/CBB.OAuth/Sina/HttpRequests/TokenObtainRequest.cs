using System.Collections.Generic;
using System.Net;
using CBB.OAuth.Sina.Common;

namespace CBB.OAuth.Sina.HttpRequests
{
    /// <summary>
    /// Performs a OAuth token-attached HTTP-Get request to the resource identified by the uri with the parameters specifed in ExtraOAuthParams.  
    /// </summary>
    /// <remarks>
    /// The signature is created upon the specified ExtraOAuthParams which is expected to include the OAuth parameters.
    /// This token secret could be a request token secret or access token secret.
    /// In this class, OAuth authorization info is conveyed in in HTTP header.
    /// </remarks>
    /// <returns>The server response string(UTF8 decoded).</returns>
    public class TokenObtainRequest : HttpGet
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TokenObtainRequest"/> with the specified <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">The uri to identify a resource in the remote server.</param>
        public TokenObtainRequest(string uri)
            : base(uri)
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="TokenObtainRequest"/> with the specified <paramref name="uri"/> amd <paramref name="secret"/>.
        /// </summary>
        /// <param name="uri">The uri to identify a resource in the remote server.</param>
        /// <param name="secret">The token secret.</param>
        public TokenObtainRequest(string uri, string secret)
            : this(uri)
        {
            this.Secret = secret;
        }

        /// <summary>
        /// Gets or sets the secret to create signature.
        /// <remarks>If set, it is request token secret; otherwise, leave empty.</remarks>
        /// </summary>
        public string Secret
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets extra OAuth parameters to be used in the request.
        /// </summary>
        public override ParamCollection Params
        {
            get
            {
                return base.Params;
            }
        }

        /// <summary>
        /// Collects all the parameters for the request.
        /// </summary>
        /// <returns>The parameters.</returns>
        private IEnumerable<ParamPair> CollectAllParams()
        {
            var parameters = OAuthHelper.GetOAuthBasicParams();

            if (null != Params)
            {
                foreach (var item in Params)
                {
                    parameters.Add(item.Name, item.Value);
                }
            }

            return parameters;
        }

        /// <summary>
        /// Returns only the raw uri. Not appending any query params.
        /// </summary>
        /// <returns></returns>
        protected override string ConstructUri()
        {
            return Uri;
        }

        /// <summary>
        /// Appends the OAuth authorization header.
        /// </summary>
        /// <param name="headers">The web header collection object.</param>
        protected override void AppendHeaders(WebHeaderCollection headers)
        {
            var oAuthHeader = OAuthHelper.ConstructOAuthHeader(CollectAllParams(), HttpMethod.Get, Uri, Secret);

            headers.Add(HttpRequestHeader.Authorization, oAuthHeader);
        }
    }
}
