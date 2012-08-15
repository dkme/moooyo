using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace CBB.OAuth.Sina.Common
{
    /// <summary>
    /// Provides helper methods for OAuth purpose.
    /// </summary>
    public static class OAuthHelper
    {
        /// <summary>
        /// Gets a collection of OAuth basic parameters(key/value pair).
        /// <remarks>Not include 'oauth_token' and'oauth_verifier' parameters.</remarks>
        /// </summary>
        /// <returns>The collection contains OAuth basic parameters.</returns>
        public static ParamCollection GetOAuthBasicParams()
        {
            var parameters = new ParamCollection();

            parameters.Add(Constants.OAuthConsumerKey, Environment.AppKey);

            parameters.Add(Constants.OAuthSignatureMethod, Environment.OAuthSignatureMethod);

            var timestamp = RFC3986Encoder.ToUnixTime(DateTime.Now);
            parameters.Add(Constants.OAuthTimestamp, timestamp.ToString(CultureInfo.InvariantCulture));

            var nonce = GenerateNonce();
            parameters.Add(Constants.OAuthNonce, nonce);

            parameters.Add(Constants.OAuthVersion, Environment.OAuthVersion);

            return parameters;
        }

        /// <summary>
        /// Constructs the OAuth authorization header with the specified parameters. See RFC5849 section 3.4.1.3.2.
        /// <remarks>
        /// This method does the following:
        /// 1. Creates signature-base-string with the specified <paramref name="uri"/>, <paramref name="parameters"/> and the <paramref name="httpMethod"/>.
        /// 2. Creates signature upon signature-base-string with sign-key (by <see cref="Environment.AppKey"/> and <paramref name="tokenSecret"/>).
        /// 3. Constructs OAuth Authentication header.
        /// </remarks>
        /// </summary>
        /// <param name="parameters">The parameters to used to construct OAuth authorization header(including OAuth protocol params and request-specific params).</param>
        /// <param name="httpMethod">The HTTP methods, like POST, GET etc.</param>
        /// <param name="uri">The uri.</param>
        /// <param name="tokenSecret">The token secret. (Could be a request token secret or access token secret.)</param>
        /// <returns>The OAuth authorization header string.</returns>
        public static string ConstructOAuthHeader(IEnumerable<ParamPair> parameters, string httpMethod, string uri, string tokenSecret)
        {
            var sortedParamsBuilder = new StringBuilder();
            var headerBuilder = new StringBuilder();
            headerBuilder.Append(Constants.OAuthHeaderPrefix);

            var encodedParams = new List<ParamPair>();
            foreach (var item in parameters)
            {
                var name = RFC3986Encoder.Encode(item.Name);
                var val = RFC3986Encoder.Encode(item.Value);
                encodedParams.Add(new ParamPair(name, val));
            }

            var sortedResult = encodedParams.OrderBy((item) => item.Name + item.Value);

            foreach (var pair in sortedResult)
            {
                sortedParamsBuilder.Append(string.Format("{0}={1}&", pair.Name, pair.Value));

                headerBuilder.Append(string.Format("{0}=\"{1}\", ", pair.Name, pair.Value));
            }

            var sortedParamsString = sortedParamsBuilder.ToString().TrimEnd('&');

            var signatureBaseString = string.Format("{0}&{1}&{2}", httpMethod, RFC3986Encoder.Encode(uri), RFC3986Encoder.Encode(sortedParamsString));

            var signature = Sign(signatureBaseString, Environment.AppSecret + "&" + tokenSecret);

            headerBuilder.Append(string.Format("{0}=\"{1}\"", Constants.OAuthSignature, RFC3986Encoder.Encode(signature)));

            return headerBuilder.ToString();
        }

        /// <summary>
        /// Constructs the OAuth authorization query string. 
        /// </summary>
        /// <param name="parameters">The parameters to used to construct OAuth authorization header(including OAuth protocol params and request-specific params).</param>
        /// <param name="httpMethod">The HTTP methods, like POST, GET etc.</param>
        /// <param name="uri">The uri.</param>
        /// <param name="tokenSecret">The token secret. (Could be a request token secret or access token secret.)</param>
        /// <returns>The OAuth authorization query string.</returns>
        public static string ConstructOAuthQueryString(IEnumerable<ParamPair> parameters, string httpMethod, string uri, string tokenSecret)
        {    
            var sortedParamsBuilder = new StringBuilder();
            var oAuthQueryStringBuilder = new StringBuilder();

            var encodedParams = new List<ParamPair>();
            foreach (var item in parameters)
            {
                var name = RFC3986Encoder.Encode(item.Name);
                var val = RFC3986Encoder.Encode(item.Value);
                encodedParams.Add(new ParamPair(name, val));
            }

            var sortedResult = encodedParams.OrderBy((item) => item.Name + item.Value);

            foreach (var pair in sortedResult)
            {
                sortedParamsBuilder.Append(string.Format("{0}={1}&", pair.Name, pair.Value));

                oAuthQueryStringBuilder.Append(string.Format("{0}={1}&", pair.Name, pair.Value));
            }

            var sortedParamsString = sortedParamsBuilder.ToString().TrimEnd('&');

            var signatureBaseString = string.Format("{0}&{1}&{2}", httpMethod, RFC3986Encoder.Encode(uri), RFC3986Encoder.Encode(sortedParamsString));

            var signature = Sign(signatureBaseString, Environment.AppSecret + "&" + tokenSecret);

            oAuthQueryStringBuilder.Append(string.Format("{0}={1}", Constants.OAuthSignature, RFC3986Encoder.Encode(signature)));

            return oAuthQueryStringBuilder.ToString();
        }

        /// <summary>
        /// Prepares a post body string for an access-token-required request.
        /// </summary>
        /// <param name="uri">The uri to identify the resource.</param>
        /// <param name="customPostParams">Additional parameters (in addition to the OAuth parameters) to be included in the post body.</param>
        /// <returns>The url-encoded post body string.</returns>
        public static string PreparePostBody(string uri, IEnumerable<ParamPair> customPostParams)
        {
            var parameters = GetOAuthBasicParams();
            OAuthAccessToken accessToken = Environment.AccessToken;

            parameters.Add(Constants.OAuthToken, accessToken.Token);

            if (null != customPostParams)
            {
                foreach (var item in customPostParams)
                {
                    parameters.Add(item.Name, item.Value);
                }
            }

            var postBody = ConstructPostBody(parameters, uri, accessToken.Secret);

            return postBody;
        }

        /// <summary>
        /// Constructs the OAuth string for the Http-Post request's body.
        /// </summary>
        /// <param name="parameters">The parameters to used to construct OAuth authorization header(including OAuth protocol params and request-specific params).</param>
        /// <param name="uri">The uri to identify the resource.</param>
        /// <param name="accessTokenSecret">The access token secret.</param>
        /// <returns>The OAuth string for a HTTP-Post body.</returns>
        private static string ConstructPostBody(IEnumerable<ParamPair> parameters, string uri, string accessTokenSecret)
        {
            var sortedParamsBuilder = new StringBuilder();
            var encodedParams = new List<ParamPair>();
            foreach (var item in parameters)
            {
                var name = RFC3986Encoder.Encode(item.Name);
                var val = RFC3986Encoder.Encode(item.Value);
                encodedParams.Add(new ParamPair(name, val));
            }

            var sortedResult = encodedParams.OrderBy((item) => item.Name + item.Value);

            foreach (var pair in sortedResult)
            {
                sortedParamsBuilder.Append(string.Format("{0}={1}&", pair.Name, pair.Value));
            }

            // Constructs base string
            var sortedParamsString = sortedParamsBuilder.ToString().TrimEnd('&');
            var signatureBaseString = string.Format("{0}&{1}&{2}", HttpMethod.Post, RFC3986Encoder.Encode(uri), RFC3986Encoder.Encode(sortedParamsString));
            var signature = Sign(signatureBaseString, Environment.AppSecret + "&" + accessTokenSecret);

            var bodyBuilder = new StringBuilder();
            bodyBuilder.Append(sortedParamsBuilder.ToString());

            bodyBuilder.Append(string.Format("&{0}={1}", "oauth_signature", RFC3986Encoder.Encode(signature)));

            return bodyBuilder.ToString();
        }

        /// <summary>
        /// Generates a random nonce string.
        /// </summary>
        /// <returns>A random nonce string.</returns>
        private static string GenerateNonce()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Signs the specified data (in string format) with HMAC-SHA1 algorithm and the specified <paramref name="signKey"/>.
        /// </summary>
        /// <param name="data">The data to be signed.</param>
        /// <param name="signKey">The sign key.</param>
        /// <returns>The base64 format signature string.</returns>
        private static string Sign(string data, string signKey)
        {
            var dataBytes = Encoding.ASCII.GetBytes(data);
            var signKeyBytes = Encoding.ASCII.GetBytes(signKey);

            var algorithm = new System.Security.Cryptography.HMACSHA1(signKeyBytes);
            var result = Convert.ToBase64String(algorithm.ComputeHash(dataBytes));

            return result;
        }
    }
}
