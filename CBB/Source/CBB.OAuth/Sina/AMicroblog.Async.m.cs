using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using CBB.OAuth.Sina.Common;
using CBB.OAuth.Sina.DataContract;
using CBB.OAuth.Sina.HttpRequests;
using System.Text.RegularExpressions;
using System.Threading;

namespace CBB.OAuth.Sina
{
    // Manually implemented async methods
    public static partial class AMicroblog
    {
        #region Manual Async Methods

        /// <summary>
        /// The async implementation of <see cref="Login"/>
        /// </summary>
        public static void LoginAsync(AsyncCallback<OAuthAccessToken> callback, string userName, string password)
        {
            Thread thread = new Thread(delegate()
                {
                    var result = new AsyncCallResult<OAuthAccessToken>();
                    try
                    {
                        var token = AMicroblog.Login(userName, password);
                        result.Success = true;
                        result.Data = token;
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                    }
                    finally
                    {
                        callback(result);
                    }
                });

            thread.Name = "Login";
            thread.Start();
        }

        /// <summary>
        /// The async implementation of <see cref="GetRequestToken"/>
        /// </summary>
        public static void GetRequestTokenAsync(AsyncCallback<OAuthRequestToken> callback)
        {
            var requester = new TokenObtainRequest(APIUri.RequestToken);

            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                var asyncResult = new AsyncCallResult<OAuthRequestToken>();
                asyncResult.Success = result.Success;
                asyncResult.Exception = result.Exception;
                if (result.Success)
                {
                    // Processes response.
                    var match = Regex.Match(result.Data, Constants.RetrieveRequestTokenPattern, RegexOptions.IgnoreCase);
                    if (!match.Success)
                        throw new AMicroblogException(LocalErrorCode.UnknowError, "Failed to retrieve request token from the web response.");

                    var requestToken = match.Groups[1].Value;
                    var requestTokenSecret = match.Groups[2].Value;

                    asyncResult.Data = new OAuthRequestToken() { Token = match.Groups[1].Value, Secret = match.Groups[2].Value };
                }
                else
                    callback(asyncResult);
            });
        }

        /// <summary>
        /// The async implementation of <see cref="Authorize"/>
        /// </summary>
        public static void AuthorizeAsync(AsyncCallback<string> callback, string requestToken, string userName, string password)
        {
            ValidateArgument(requestToken, "requestToken");
            ValidateArgument(userName, "userName");
            ValidateArgument(password, "password");
            var logonAndAuthorizeTemplate = "action=submit&regCallback=http%253A%252F%252Fapi.t.sina.com.cn%252Foauth%252Fauthorize%253Foauth_token%253D{0}%2526oauth_callback%253Doob%2526from%253D%2526with_cookie%253D&oauth_token={0}&oauth_callback=oob&from=&forcelogin=&userId={1}&passwd={2}";
            var postData = string.Format(logonAndAuthorizeTemplate, requestToken, RFC3986Encoder.Encode(userName), RFC3986Encoder.Encode(password));
            var requester = new HttpPost(APIUri.Authorize, postData);
            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                var asyncResult = new AsyncCallResult<string>();
                asyncResult.Success = result.Success;
                asyncResult.Exception = result.Exception;
                if (result.Success)
                {
                    var match = Regex.Match(result.Data, Constants.RetrieveAuthorizationCodePattern, RegexOptions.CultureInvariant);
                    if (match.Success)
                        asyncResult.Data = match.Groups[1].Value;
                    else
                        throw new AMicroblogException(LocalErrorCode.CredentialInvalid, "Invalid user name or password.");
                }
                else
                    callback(asyncResult);
            });
        }

        /// <summary>
        /// The async implementation of <see cref="GetAccessToken"/>
        /// </summary>
        public static void GetAccessTokenAsync(AsyncCallback<OAuthAccessToken> callback, OAuthRequestToken requestToken, string verifier)
        {
            ValidateArgument(requestToken, "requestToken");
            ValidateArgument(requestToken.Token, "requestToken.Token");
            ValidateArgument(requestToken.Secret, "requestToken.Secret");
            ValidateArgument(verifier, "verifier");
            // If the required auth header item is not given (for example: an incorrect name is used.), server will not response.
            var requester = new TokenObtainRequest(APIUri.AccessToken);
            requester.Params.Add(new ParamPair(Constants.OAuthToken, requestToken.Token));
            requester.Params.Add(new ParamPair(Constants.OAuthVerifier, verifier));
            requester.Secret = requestToken.Secret;
            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                var asyncResult = new AsyncCallResult<OAuthAccessToken>();
                asyncResult.Success = result.Success;
                asyncResult.Exception = result.Exception;
                if (result.Success)
                {
                    var accessTokenMatch = Regex.Match(result.Data, Constants.RetrieveAccessTokenPattern, RegexOptions.IgnoreCase);
                    if (!accessTokenMatch.Success)
                        throw new AMicroblogException(LocalErrorCode.UnknowError, "Failed to retrieve access token.");

                    var accessToken = accessTokenMatch.Groups[1].Value;
                    var accessTokenSecret = accessTokenMatch.Groups[2].Value;
                    var userID = accessTokenMatch.Groups[4].Value;

                    asyncResult.Data = new OAuthAccessToken() { Token = accessToken, Secret = accessTokenSecret, UserID = userID };
                }
                else
                    callback(asyncResult);
            });
        }

        /// <summary>
        /// The async implementation of <see cref="GetRateLimitStatus"/>
        /// </summary>
        public static void GetRateLimitStatusAsync(AsyncCallback<RateLimitStatus> callback)
        {
            var requester = new OAuthHttpGet(APIUri.GetRateLimitStatus);
            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                var preprocess = new Func<string, string>(delegate(string response)
                {
                    return response.Replace("hash", "rate-limit-status").Replace(" type=\"integer\"", "").Replace(" type=\"datetime\"", "");
                });

                ProcessAsyncCallbackResponse(result, callback, preprocess);
            });
        }

        /// <summary>
        /// The async implementation of <see cref="GetEmotions"/>
        /// </summary>
        public static void GetEmotionsAsync(AsyncCallback<Emotions> callback, EmotionType emotionType = EmotionType.Image, string language = "")
        {
            var requester = new OAuthHttpGet(APIUri.GetEmotions);
            if (emotionType != EmotionType.Image)
            {
                var eType = string.Empty;
                switch (emotionType)
                {
                    case EmotionType.Image:
                        eType = "face";
                        break;
                    case EmotionType.Magic:
                        eType = "ani";
                        break;
                    case EmotionType.Cartoon:
                        eType = "cartoon";
                        break;
                    default:
                        eType = "face";
                        break;
                }
                requester.Params.Add(new ParamPair("type", eType));
            }

            if (!string.IsNullOrEmpty(language))
                requester.Params.Add(new ParamPair("language", language));

            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                ProcessAsyncCallbackResponse(result, callback, EncodeXmlCharsPreprocess);
            });
        }

        /// <summary>
        /// The async implementation of <see cref="DeleteTags"/>
        /// </summary>
        public static void DeleteTagsAsync(AsyncCallback<TagIDs> callback, params long[] tagIDs)
        {
            ValidateArgument(tagIDs, "tagIDs");
            var count = tagIDs.Length;
            if (count == 0)
                throw new AMicroblogException(LocalErrorCode.ArgumentNotProvided, "Tag IDs not provided.");
            var tagBuilder = new StringBuilder();
            for (int i = 0; i < tagIDs.Length; i++)
            {
                tagBuilder.Append(tagIDs[i]);
                if (i < count - 1)
                    tagBuilder.Append(",");
            }
            var requester = new OAuthHttpDelete(APIUri.DeleteTags);
            requester.Params.Add("ids", tagBuilder.ToString());
            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                var preprocess = new Func<string, string>(delegate(string response) 
                    {
                        return response.Replace("tags>", "tagids>");
                    });

                ProcessAsyncCallbackResponse<TagIDs>(result, callback, preprocess);
            });
        }

        /// <summary>
        /// The async implementation of <see cref="GetBlockingListIDs"/>
        /// </summary>
        public static void GetBlockingListIDsAsync(AsyncCallback<UserIDs> callback, int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetBlockingListIDs);
            if (count.HasValue)
                requester.Params.Add("page", count.Value.ToString(InvariantCulture));
            if (count.HasValue)
                requester.Params.Add("count", page.Value.ToString(InvariantCulture));
            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                ProcessAsyncCallbackResponse(result, callback);
            });
        }

        /// <summary>
        /// The async implementation of <see cref="IsBlocked"/>
        /// </summary>
        public static void IsBlockedAsync(AsyncCallback<bool> callback, long? userID = null, string screenName = null)
        {
            var requester = new OAuthHttpDelete(APIUri.IsBlocked);
            if (userID.HasValue)
                requester.Params.Add("user_id", userID.Value.ToString(InvariantCulture));
            if (!string.IsNullOrEmpty(screenName))
                requester.Params.Add("screen_name", screenName);
            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                ProcessAsyncCallbackResponseCustom(result, callback, response => response.Contains("true"));
            });
        }

        /// <summary>
        /// The async implementation of <see cref="FollowTrend"/>
        /// </summary>
        public static void FollowTrendAsync(AsyncCallback<long> callback, string trendName)
        {
            ValidateArgument(trendName, "trendName");
            var requester = new OAuthHttpPost(APIUri.FollowTrend);
            requester.Params.Add("trend_name", trendName);
            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                Func<string, long> customProcess = delegate(string response)
                    {
                        var match = Regex.Match(response, @"<topicid>(\d+?)</topicid>", RegexOptions.IgnoreCase);

                        var topicID = long.Parse(match.Groups[1].Value);

                        return topicID;
                    };

                ProcessAsyncCallbackResponseCustom(result, callback, customProcess);
            });
        }

        /// <summary>
        /// The async implementation of <see cref="UnfollowTrend"/>
        /// </summary>
        public static void UnfollowTrendAsync(AsyncCallback<bool> callback, long trendID)
        {
            var requester = new OAuthHttpDelete(APIUri.UnfollowTrend);
            requester.Params.Add("trend_id", trendID.ToString(InvariantCulture));
            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                Func<string, bool> customProcess = delegate(string response)
                {
                    var match = Regex.Match(response, @"<result>(\w+?)</result>", RegexOptions.IgnoreCase);

                    var isUnfollowed = bool.Parse(match.Groups[1].Value);

                    return isUnfollowed;
                };

                ProcessAsyncCallbackResponseCustom(result, callback, customProcess);
            });
        }

        /// <summary>
        /// The async implementation of <see cref="GetShortUrlSharedStatuses"/>
        /// </summary>
        public static void GetShortUrlSharedStatusesAsync(AsyncCallback<Statuses> callback, string shortUrl, long? sinceID = null, long? maxID = null, int? page = null, int? count = null)
        {
            ValidateArgument(shortUrl, "shortUrl");
            var requester = new OAuthHttpGet(APIUri.GetShortUrlSharedStatuses);
            requester.Params.Add("url_short", shortUrl);
            ConstructPagedRecordsParams(requester.Params, sinceID, maxID, count, page);
            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                Func<string, string> preprocess = delegate(string response)
                {
                    response = response.Replace("<url>", "");
                    response = response.Replace("</url>", "");
                    response = response.Replace("<share_statuses>", "<statuses>");
                    response = response.Replace("</share_statuses>", "</statuses>");

                    response = Regex.Replace(response, "<url_short>(.+?)</url_short>", "");

                    return response;
                };

                ProcessAsyncCallbackResponse(result, callback, preprocess);
            });
        }

        /// <summary>
        /// The async implementation of <see cref="GetShortUrlComments"/>
        /// </summary>
        public static void GetShortUrlCommentsAsync(AsyncCallback<Comments> callback, string shortUrl, long? sinceID = null, long? maxID = null, int? page = null, int? count = null)
        {
            ValidateArgument(shortUrl, "shortUrl");
            var requester = new OAuthHttpGet(APIUri.GetShortUrlComments);
            requester.Params.Add("url_short", shortUrl);
            ConstructPagedRecordsParams(requester.Params, sinceID, maxID, count, page);
            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                Func<string, string> preprocess = delegate(string response)
                {
                    response = response.Replace("<url>", "");
                    response = response.Replace("</url>", "");
                    response = response.Replace("<share_comments>", "<comments>");
                    response = response.Replace("</share_comments>", "</comments>");

                    response = Regex.Replace(response, "<url_short>(.+?)</url_short>", "");

                    return response;
                };

                ProcessAsyncCallbackResponse(result, callback, preprocess);
            });
        }

        /// <summary>
        /// The async implementation of <see cref="SearchUsers"/>
        /// </summary>
        public static void SearchUsersAsync(AsyncCallback<Users> callback, string keyword, int? page = null, int? count = null, bool currentAppOnly = false)
        {
            ValidateArgument("keyword", keyword);
            var requester = new HttpGet(APIUri.SearchUsers);
            requester.Params.Add(Constants.Source, Environment.AppKey);
            requester.Params.Add("q", RFC3986Encoder.UrlEncode(keyword));
            if (count.HasValue)
                requester.Params.Add("count", count.Value.ToString(InvariantCulture));
            if (page.HasValue)
                requester.Params.Add("page", page.Value.ToString(InvariantCulture));
            if (currentAppOnly)
                requester.Params.Add("base_app ", "1");
            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                Func<string, string> preprocess = delegate(string response)
                {
                    response = response.Replace("<searchResult>", string.Empty);
                    response = response.Replace("</searchResult>", string.Empty);

                    return response;
                };

                ProcessAsyncCallbackResponse(result, callback, preprocess);
            });
        }

        /// <summary>
        /// The async implementation of <see cref="SearchStatuses"/>
        /// </summary>
        public static void SearchStatusesAsync(AsyncCallback<Statuses> callback, string keyword, int? statusType = null, bool? includePic = null, long? authorID = null,
            int? provice = null, int? city = null, long? startTime = null, long? endTime = null,
            int? page = null, int? count = null, bool returnCounter = false, bool currentAppOnly = false)
        {
            ValidateArgument(keyword, "keyword");
            var requester = new HttpGet(APIUri.SearchStatuses);
            requester.Params.Add(Constants.Source, Environment.AppKey);
            requester.Params.Add("q", keyword);
            if (statusType.HasValue)
                requester.Params.Add("filter_ori", statusType.Value.ToString(InvariantCulture));
            if (includePic.HasValue)
                requester.Params.Add("filter_pic", includePic.Value ? "1" : "2");
            if (provice.HasValue)
                requester.Params.Add("provice", provice.Value.ToString(InvariantCulture));
            if (city.HasValue)
                requester.Params.Add("city", city.Value.ToString(InvariantCulture));
            if (startTime.HasValue)
                requester.Params.Add("starttime", startTime.Value.ToString(InvariantCulture));
            if (endTime.HasValue)
                requester.Params.Add("endtime", endTime.Value.ToString(InvariantCulture));
            if (page.HasValue)
                requester.Params.Add("page", page.Value.ToString(InvariantCulture));
            if (count.HasValue)
                requester.Params.Add("count", count.Value.ToString(InvariantCulture));
            if (returnCounter)
                requester.Params.Add("needcount", "true");
            if (currentAppOnly)
                requester.Params.Add("base_app", "1");
            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                Func<string, string> preprocess = delegate(string response)
                {
                    response = response.Replace("<searchResult>", string.Empty);
                    response = response.Replace("</searchResult>", string.Empty);

                    return response;
                };

                ProcessAsyncCallbackResponse(result, callback, preprocess);
            });
        }

        /// <summary>
        /// The async implementation of <see cref="GetUserTrends"/>
        /// </summary>
        public static void GetUserTrendsAsync(AsyncCallback<Trends> callback, long userID, int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetUserTrends);
            requester.Params.Add("user_id", userID.ToString(InvariantCulture));
            if (page.HasValue)
                requester.Params.Add("page", page.Value.ToString(InvariantCulture));
            if (count.HasValue)
                requester.Params.Add("count", count.Value.ToString(InvariantCulture));
            requester.RequestAsync(delegate(AsyncCallResult<string> result)
            {
                Func<string, string> preprocess = delegate(string response)
                {
                    response = response.Replace("trends", "userTrends").Replace("trend", "userTrend");

                    return response;
                };

                ProcessAsyncCallbackResponse(result, callback, preprocess);
            });
        }

        #endregion
    }
}
