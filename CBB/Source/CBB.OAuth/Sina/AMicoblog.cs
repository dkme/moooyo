using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CBB.OAuth.Sina.Common;
using CBB.OAuth.Sina.DataContract;
using CBB.OAuth.Sina.HttpRequests;

namespace CBB.OAuth.Sina
{
    /// <summary>
    /// Provides APIs to access weibo.com.
    /// </summary>
    public static partial class AMicroblog
    {
        private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

        #region OAuth(Login)

        /// <summary>
        /// Retrieves a one-time use request token.
        /// </summary>
        /// <remarks>
        /// See http://open.weibo.com/wiki/Oauth/request_token
        /// </remarks>
        /// <returns>The request token retrieved.</returns>
        public static OAuthRequestToken GetRequestToken()
        {
            var requester = new TokenObtainRequest(APIUri.RequestToken);
            requester.Params.Add(Constants.OAuthCallback, Environment.OAuthCallback);
            
            var response = requester.Request();

            // Processes response.
            var match = Regex.Match(response, Constants.RetrieveRequestTokenPattern, RegexOptions.IgnoreCase);
            if (!match.Success)
                throw new AMicroblogException(LocalErrorCode.UnknowError, "Failed to retrieve request token from the web response.");

            var requestToken = match.Groups[1].Value;
            var requestTokenSecret = match.Groups[2].Value;

            return new OAuthRequestToken() { Token = match.Groups[1].Value, Secret = match.Groups[2].Value };
        }

        /// <summary>
        /// Authorizes the un-authorized request token with a SINA passport(user name and password).
        /// </summary>
        /// <remarks>
        /// See http://open.weibo.com/wiki/Oauth/authorize
        /// </remarks>
        /// <param name="requestToken">The un-authorized request token previously obtained.</param>
        /// <param name="userName">The user name of sina passport.</param>
        /// <param name="password">The password of sina passport.</param>
        /// <returns>The authorization code(aka oauth_verifier).</returns>
        public static string Authorize(string requestToken, string userName, string password)
        {
            ValidateArgument(requestToken, "requestToken");
            ValidateArgument(userName, "userName");
            ValidateArgument(password, "password");

            var requester = new HttpGet(APIUri.Authorize);
            requester.Params.Add(Constants.OAuthToken, requestToken);
            requester.Params.Add("userId", RFC3986Encoder.UrlEncode(userName));
            requester.Params.Add("passwd", RFC3986Encoder.UrlEncode(password));
            requester.Params.Add(Constants.OAuthCallback, "xml");

            try
            {
                var response = requester.Request();
                var match = Regex.Match(response, Constants.RetrieveAuthorizationCodePattern, RegexOptions.CultureInvariant);
                if (match.Success)
                    return match.Groups[1].Value;
                else
                    throw new AMicroblogException(LocalErrorCode.CredentialInvalid, "Invalid user name or password.");
            }
            catch
            {
                throw new AMicroblogException(LocalErrorCode.CredentialInvalid, "Invalid user name or password.");                
            }            
        }

        /// <summary>
        /// Exchanges the request token for an access token.
        /// </summary>
        /// <remarks>
        /// See http://open.weibo.com/wiki/Oauth/access_token
        /// Both Http-Get and Http-Post can work. In this API is implemented with Http-Get.
        /// </remarks>
        /// <param name="requestToken">The request token previously obtained.</param>
        /// <param name="verifier">The authorization code.</param>
        /// <returns></returns>
        public static OAuthAccessToken GetAccessToken(OAuthRequestToken requestToken, string verifier)
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

            var response = requester.Request();

            var accessTokenMatch = Regex.Match(response, Constants.RetrieveAccessTokenPattern, RegexOptions.IgnoreCase);
            if (!accessTokenMatch.Success)
                throw new AMicroblogException(LocalErrorCode.UnknowError, "Failed to retrieve access token.");

            var accessToken = accessTokenMatch.Groups[1].Value;
            var accessTokenSecret = accessTokenMatch.Groups[2].Value;
            var userID = accessTokenMatch.Groups[4].Value;

            return new OAuthAccessToken() { Token = accessToken, Secret = accessTokenSecret, UserID = userID };
        }

        /// <summary>
        /// Performs a OAuth login with the specified <paramref name="userName"/> and <paramref name="password"/>.
        /// </summary>
        /// <remarks>
        /// See http://open.weibo.com/wiki/Oauth
        /// This method is implemented by sequentially calling <see cref="AMicroblog.GetRequestToken"/>, 
        /// <see cref="AMicroblog.Authorize"/> and <see cref="AMicroblog.GetAccessToken"/>
        /// The access token is stored in <see cref="Environment.AccessToken"/> after login.</remarks>
        /// <param name="userName">The user name of a SINA passport.</param>
        /// <param name="password">The password of a SINA passport.</param>
        /// <returns>The access token.</returns>
        public static OAuthAccessToken Login(string userName, string password)
        {
            var requestToken = AMicroblog.GetRequestToken();
            var authorizationCode = AMicroblog.Authorize(requestToken.Token, userName, password);
            var token = AMicroblog.GetAccessToken(requestToken, authorizationCode);

            Environment.AccessToken = token;
            Environment.CurrentUserAccount = userName;

            return token;
        }

        /// <summary>
        /// Performs a local logout. Cleans the Environment data.
        /// </summary>
        public static void Logout()
        {
            Environment.AccessToken = null;
            Environment.CurrentUserAccount = null;
        }
        
        #endregion

        #region Account

        /// <summary>
        /// Verifies whether the current user has SINA microblog service enabled.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Account/verify_credentials </remarks>
        /// <returns>The user info.</returns>
        public static UserInfo VerifyCredential()
        {
            var requester = new OAuthHttpGet(APIUri.VerifyCredential);

            var response = requester.Request();

            var userInfo = XmlSerializationHelper.XmlToObject<UserInfo>(response);

            return userInfo;
        }

        /// <summary>
        /// Terminates the web-login user's session.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Account/end_session </remarks>
        /// <returns></returns>
        public static UserInfo EndSession()
        {
            var requester = new OAuthHttpGet(APIUri.EndSession);

            var response = requester.Request();

            var userInfo = XmlSerializationHelper.XmlToObject<UserInfo>(response);

            return userInfo;
        }

        /// <summary>
        /// Retrieves the app's rate limit. 
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Account/rate_limit_status </remarks>
        /// <returns></returns>
        public static RateLimitStatus GetRateLimitStatus()
        {
            var requester = new OAuthHttpGet(APIUri.GetRateLimitStatus);

            var response = requester.Request();

            response = response.Replace("hash", "rate-limit-status").Replace(" type=\"integer\"", "").Replace(" type=\"datetime\"", "");

            var result = XmlSerializationHelper.XmlToObject<RateLimitStatus>(response);

            return result;
        }

        /// <summary>
        /// Updates the profile image of the current user.
        /// </summary>
        /// <remarks>
        /// Extra permission required to call this API.
        /// Returns error "40070:Error: not enough permission to call the api, pls contact the app developer." if permission insufficient.
        /// See http://open.weibo.com/wiki/Account/update_profile_image
        /// </remarks>
        /// <param name="imageFileLocation">The file location of the image file to upload. Limit as 700KB</param>
        /// <returns>The posted status.</returns>
        public static UserInfo UpdateProfileImage(string imageFileLocation)
        {
            // Validates arguments
            ValidateArgument(imageFileLocation, "imageFileLocation");

            FileInfo picInfo = new FileInfo(imageFileLocation);
            if (picInfo.Length > 700 * 1024) // 700KB limit
            {
                throw new AMicroblogException(LocalErrorCode.ArgumentInvalid, "Profile imgae file too large to upload.");
            }

            var requester = new OAuthMultiPartHttpPost(APIUri.UpdateProfileImage);

            requester.PartFields.Add(new MultiPartField() { Name = "image", FilePath = imageFileLocation });            

            var response = requester.Request();

            var userInfo = XmlSerializationHelper.XmlToObject<UserInfo>(response);

            return userInfo;
        }

        /// <summary>
        /// Updates the profile info of current user.
        /// </summary>
        /// <remarks>
        /// Extra permission required to call this API.
        /// Returns error "40070:Error: not enough permission to call the api, pls contact the app developer." if permission insufficient.
        /// See http://open.weibo.com/wiki/Account/update_profile
        /// </remarks>
        /// <param name="updateProfileInfo">The update profile info.</param>
        /// <returns>The user info.</returns>
        public static UserInfo UpdateProfile(UpdateProfileInfo updateProfileInfo)
        {
            ValidateArgument(updateProfileInfo, "updateProfileInfo");

            var requester = new OAuthHttpPost(APIUri.UpdateProfileImage);

            if(!string.IsNullOrEmpty(updateProfileInfo.ScreenName))
                requester.Params.Add("name", updateProfileInfo.ScreenName);

            if (!string.IsNullOrEmpty(updateProfileInfo.Gender))
                requester.Params.Add("gender", updateProfileInfo.Gender);

            if (!string.IsNullOrEmpty(updateProfileInfo.Description))
                requester.Params.Add("description", updateProfileInfo.Description);

            if (updateProfileInfo.Province.HasValue)
                requester.Params.Add("province", updateProfileInfo.Province.Value.ToString(InvariantCulture));

            if (updateProfileInfo.City.HasValue)
                requester.Params.Add("city", updateProfileInfo.City.Value.ToString(InvariantCulture));

            var response = requester.Request();

            var userInfo = XmlSerializationHelper.XmlToObject<UserInfo>(response);

            return userInfo;
        }

        /// <summary>
        /// Retrieves the privacy info of the current user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Account/get_privacy </remarks>
        public static PrivacyInfo GetPrivacy()
        {
            var requester = new OAuthHttpGet(APIUri.GetPrivacy);

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<PrivacyInfo>(response);

            return result;
        }

        /// <summary>
        /// Updates the privacy settings of the current user.
        /// </summary>
        /// <remarks>
        /// See http://open.weibo.com/wiki/Account/update_privacy 
        /// Extra permission required to call this API.
        /// Returns error "40070:Error: not enough permission to call the api, pls contact the app developer." if permission insufficient.
        /// </remarks>
        /// <param name="commentPrivacy">
        /// The comment privacy, indicating who can comment the statuses of mine. 
        /// 0: All(default), 1: People I am following 
        /// </param>
        /// <param name="messagePrivacy">
        /// The message privacy, indicating who can send private message to me.
        /// 0: All 1:People I am following(default)
        /// </param>
        /// <param name="realNamePrivacy">
        /// The realname privacy, indicating whether others can search me by real name.
        /// 0: Allow 1: Not Allow(deault)
        /// </param>
        /// <param name="geoPrivacy">
        ///  The geo privacy, indicating whether show geo info in my statuses if there is any.
        ///  0: Allow(default) 1: Not Allow
        /// </param>
        /// <param name="badgePrivacy">
        /// The badge privacy, indicating whether display my badge.
        /// 0: Display(default) -1: Hide
        /// </param>
        /// <returns>A boolean value indicating whether the update succeed.</returns>
        public static void UpdatePrivacy(int? commentPrivacy = null, int? messagePrivacy = null, int? realNamePrivacy = null, int? geoPrivacy = null, int? badgePrivacy = null)
        {
            var requester = new OAuthHttpPost(APIUri.UpdatePrivacy);
            if (commentPrivacy.HasValue)
                requester.Params.Add(new ParamPair("comment", commentPrivacy.Value.ToString(InvariantCulture)));
            if (messagePrivacy.HasValue)
                requester.Params.Add(new ParamPair("message", messagePrivacy.Value.ToString(InvariantCulture)));
            if (realNamePrivacy.HasValue)
                requester.Params.Add(new ParamPair("realname", realNamePrivacy.Value.ToString(InvariantCulture)));
            if (geoPrivacy.HasValue)
                requester.Params.Add(new ParamPair("geo", geoPrivacy.Value.ToString(InvariantCulture)));
            if (badgePrivacy.HasValue)
                requester.Params.Add(new ParamPair("badge", badgePrivacy.Value.ToString(InvariantCulture)));

            var response = requester.Request();
        }

        #endregion

        #region Status

        /// <summary>
        /// Retrieves the info of a microblog status by its id.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/show </remarks>
        /// <param name="statusID">The id to identify the status.</param>
        /// <returns>The status info.</returns>
        public static StatusInfo GetStatus(long statusID)
        {
            ValidateArgument(statusID, "statusID");

            var requester = new OAuthHttpGet(string.Format("{0}/{1}.xml", APIUri.ShowStatus, statusID));
            requester.Params.Add(new ParamPair("id", statusID.ToString(InvariantCulture)));
            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<StatusInfo>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the url of a microblog status by its id.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/User/statuses/id 
        /// This is not a remote call. This method just constructs the url and returns it.
        /// </remarks>
        /// <param name="statusID">The id to identify the status.</param>
        /// <returns>The url of the status.</returns>
        public static string GetStatusUrl(long statusID)
        {
            ValidateArgument(statusID, "statusID");

            var userID = Environment.AccessToken.UserID;

            return string.Format(APIUri.GetStatusUrl, userID, statusID);
        }

        /// <summary>
        /// Posts a status to weibo.com.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/update </remarks>
        /// <param name="updateStatusInfo">The status info object.</param>
        /// <returns>The posted status.</returns>
        public static StatusInfo PostStatus(UpdateStatusInfo updateStatusInfo)
        {
            // Validates arguments
            ValidateArgument(updateStatusInfo, "updateStatusInfo");
            ValidateArgument(updateStatusInfo.Status, "updateStatusInfo.Status");

            Collection<ParamPair> customParams = new Collection<ParamPair>();
            customParams.Add(new ParamPair("status", RFC3986Encoder.UrlEncode(updateStatusInfo.Status)));

            if (updateStatusInfo.ReplyTo.HasValue)
            {
                customParams.Add(new ParamPair("in_reply_to_status_id", updateStatusInfo.ReplyTo.Value.ToString()));
            }

            if (updateStatusInfo.Latitude.HasValue && updateStatusInfo.Longitude.HasValue)
            {
                customParams.Add(new ParamPair("lat", updateStatusInfo.Latitude.Value.ToString(InvariantCulture)));
                customParams.Add(new ParamPair("long", updateStatusInfo.Longitude.Value.ToString(InvariantCulture)));
            }

            // The status text must be url-encoded.
            var requester = new OAuthHttpPost(APIUri.UpdateStatus, customParams);

            var response = requester.Request();

            var statusInfo = XmlSerializationHelper.XmlToObject<StatusInfo>(response);

            return statusInfo;
        }

        /// <summary>
        /// Posts a with-pic status to weibo.com.
        /// See <seealso cref="PostStatus"/>
        /// </summary>
        /// <remarks>
        /// 'status' should not be empty, otherwise bad reuqest error 400 returned.
        /// posting duplicate status also causes return 400 error.
        /// See http://open.weibo.com/wiki/Statuses/upload
        ///</remarks>
        /// <param name="updateStatusWithPicInfo">The status info object.</param>
        /// <returns>The posted status.</returns>
        public static StatusInfo PostStatusWithPic(UpdateStatusWithPicInfo updateStatusWithPicInfo)
        {
            // Validates arguments
            ValidateArgument(updateStatusWithPicInfo, "updateStatusWithPicInfo");
            ValidateArgument(updateStatusWithPicInfo.Status, "updateStatusWithPicInfo.Status");
            ValidateArgument(updateStatusWithPicInfo.Pic, "updateStatusWithPicInfo.Pic");

            //新浪带图片微博分享时判断图片的大小，读取本地图片文件
            FileInfo picInfo = new FileInfo(updateStatusWithPicInfo.Pic);
            if (picInfo.Length > 5 * 1021 * 1024) // 5MB limit
            {
                throw new AMicroblogException(LocalErrorCode.ArgumentInvalid, "Pic file too large to post.");
            }

            var uri = APIUri.UpdateStatusWithPic;
            var requester = new OAuthMultiPartHttpPost(uri);

            requester.PartFields.Add(new MultiPartField() { Name = "status", Value = RFC3986Encoder.UrlEncode(updateStatusWithPicInfo.Status) });
            requester.PartFields.Add(new MultiPartField() { Name = "pic", FilePath = updateStatusWithPicInfo.Pic });
            if (updateStatusWithPicInfo.Latitude.HasValue && updateStatusWithPicInfo.Longitude.HasValue)
            {
                requester.PartFields.Add(new MultiPartField() { Name = "lat", FilePath = updateStatusWithPicInfo.Latitude.Value.ToString(InvariantCulture) });
                requester.PartFields.Add(new MultiPartField() { Name = "long", FilePath = updateStatusWithPicInfo.Longitude.Value.ToString(InvariantCulture) });
            }

            var response = requester.Request();
            var statusInfo = XmlSerializationHelper.XmlToObject<StatusInfo>(response);

            return statusInfo;
        }

        /// <summary>
        /// Deletes a microblog status by its id.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/destroy </remarks>
        /// <param name="statusID">The id to identify the status.</param>
        public static StatusInfo DeleteStatus(long statusID)
        {
            ValidateArgument(statusID, "statusID");

            var requester = new OAuthHttpDelete(string.Format("{0}/{1}.xml", APIUri.DeleteStatus, statusID));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<StatusInfo>(response);

            return result;
        }

        /// <summary>
        /// Re-posts a microblog status.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/repost </remarks>
        /// <param name="statusID">The id to identify the status to be reposted.</param>
        /// <param name="repostStatusText">The status text to add in addition to the reposted status.</param>
        /// <param name="commentToAuthor">Indicates whether this status is as a comment to the author of the reposted status.</param>
        /// <param name="commentToOriginalAuthor">Indicates whether this status is as a comment to the author of the original status if there is any.</param>
        /// <returns>The status info.</returns>
        public static StatusInfo Forward(long statusID, string repostStatusText, bool commentToAuthor = false, bool commentToOriginalAuthor = false)
        {
            ValidateArgument(statusID, "statusID");

            var requester = new OAuthHttpPost(APIUri.RepostStatus);
            requester.Params.Add(new ParamPair("id", statusID.ToString(InvariantCulture)));
            requester.Params.Add(new ParamPair("status", RFC3986Encoder.UrlEncode(repostStatusText)));
            var isComment = 0;
            if (commentToAuthor)
                isComment++;
            if (commentToOriginalAuthor)
                isComment++;
            requester.Params.Add(new ParamPair("is_comment", isComment.ToString()));
            var response = requester.Request();

            var statusInfo = XmlSerializationHelper.XmlToObject<StatusInfo>(response);

            return statusInfo;
        }

        /// <summary>
        /// Comments a status.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/comment </remarks>
        /// <param name="statusID">The status id to identify the status.</param>
        /// <param name="comment">The comment text.</param>
        /// <param name="commentToOriginalAuthor">Indicate whether to comment to the original status if the status is a reposted one.</param>
        /// <returns>The comment info.</returns>
        public static CommentInfo Comment(long statusID, string comment, bool commentToOriginalAuthor = false)
        {
            ValidateArgument(statusID, "statusID");
            ValidateArgument(comment, "comment");            

            var requester = new OAuthHttpPost(APIUri.Comment);
            requester.Params.Add(new ParamPair("id", statusID.ToString(InvariantCulture)));
            requester.Params.Add(new ParamPair("comment", RFC3986Encoder.UrlEncode(comment)));            
            if (commentToOriginalAuthor)
                requester.Params.Add(new ParamPair("comment_ori", "1"));

            var response = requester.Request();

            var commentInfo = XmlSerializationHelper.XmlToObject<CommentInfo>(response);

            return commentInfo;
        }

        /// <summary>
        /// Retrieves the comments of a specified status.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/comments </remarks>
        /// <param name="statusID">The ID of the target status</param>
        /// <param name="page">The page index. Default to 1.</param>
        /// <param name="count">The number of statuses to return, if not specified, default to 20. Maxmum as 200.</param>
        /// <returns>The comments.</returns>
        public static Comments GetComments(long statusID,  int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetComments);

            requester.Params.Add("id", statusID.ToString(InvariantCulture));

            if (count.HasValue)
                requester.Params.Add("count", count.Value.ToString(InvariantCulture));
            if (page.HasValue)
                requester.Params.Add("page", page.Value.ToString(InvariantCulture));

            var response = requester.Request();

            var statuses = XmlSerializationHelper.XmlToObject<Comments>(response);

            return statuses;
        }

        /// <summary>
        /// Replies a comment of a status.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/reply </remarks>
        /// <param name="commentID">The comment id to comment to.</param>
        /// <param name="comment">The comment text.</param>
        /// <param name="statusID">The status which owns the comment.</param>
        /// <param name="withoutMention">Indicates whether to include the '@' ref.</param>
        /// <returns>The comment info.</returns>
        public static CommentInfo ReplyComment(long commentID, string comment, long statusID, bool withoutMention = false)
        {
            ValidateArgument(commentID, "commentID");
            ValidateArgument(comment, "comment");

            var requester = new OAuthHttpPost(APIUri.ReplyComment);
            requester.Params.Add(new ParamPair("cid", commentID.ToString(InvariantCulture)));
            requester.Params.Add(new ParamPair("comment", RFC3986Encoder.UrlEncode(comment)));
            requester.Params.Add(new ParamPair("id", statusID.ToString(InvariantCulture)));           
            if (withoutMention)
                requester.Params.Add(new ParamPair("without_mention", "1"));           

            var response = requester.Request();

            var commentInfo = XmlSerializationHelper.XmlToObject<CommentInfo>(response);

            return commentInfo;
        }

        /// <summary>
        /// Deletes a comment identified by the specified <paramref name="commentID"/>.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/comment_destroy </remarks>
        /// <param name="commentID">The comment id.</param>
        /// <returns></returns>
        public static CommentInfo DeleteComment(long commentID)
        {
            ValidateArgument(commentID, "commentID");

            var requester = new OAuthHttpDelete(string.Format("{0}/{1}.xml", APIUri.DeleteComment, commentID));
            var response = requester.Request();

            var commentInfo = XmlSerializationHelper.XmlToObject<CommentInfo>(response);

            return commentInfo;
        }

        /// <summary>
        /// Deletes multiple comments.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/comment/destroy_batch </remarks>
        /// <param name="commentIDs">The comments ids.</param>
        /// <returns></returns>
        public static Comments DeleteComments(long[] commentIDs)
        {
            ValidateArgument(commentIDs, "commentIDs");

            StringBuilder cidBuilder = new StringBuilder();
            foreach (var item in commentIDs)
            {
                cidBuilder.Append(item);
                cidBuilder.Append(",");
            }

            var requester = new OAuthHttpPost(APIUri.DeleteComments);
            requester.Params.Add(new ParamPair("ids", cidBuilder.ToString()));

            var response = requester.Request();

            var comments = XmlSerializationHelper.XmlToObject<Comments>(response);

            return comments;
        }

        /// <summary>
        /// Searches microblog statuses by the specified conditions.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/search 
        /// Extra permission required to call this API.
        /// </remarks>
        /// <param name="keyword">The keyword to search by.</param>
        /// <param name="statusType">The type of statuses to search. 4: forwarded, 5: original, 0: all, default to 0.</param>
        /// <param name="includePic">Indicates whether searching in statues with pic or not with pic. If not specified, search in both.</param>
        /// <param name="authorID">The author ID. If specified, perform the search in the statuses of this author only.</param>
        /// <param name="provice">The provice id.</param>
        /// <param name="city">The city id.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="page">The page index.</param>
        /// <param name="count">The number of statuses to return, if not specified, default to 10.</param>
        /// <param name="returnCounter">Indicates whether to return the counter info or not.</param>
        /// <param name="currentAppOnly">Indicates whether retrieve the statuses the posted from the current app only.</param>
        /// <returns>The statuses found.</returns>
        public static Statuses SearchStatuses(string keyword, int? statusType = null, bool? includePic = null, long? authorID = null,
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

            var response = requester.Request();

            response = response.Replace("<searchResult>", string.Empty);
            response = response.Replace("</searchResult>", string.Empty);

            var result = XmlSerializationHelper.XmlToObject<Statuses>(response);

            return result;
        }

        #endregion

        #region Direct Message

        /// <summary>
        /// Retrieves a certain mumber of latest direct messages of the current user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Direct_messages 
        /// Extra permission required to call this API.
        /// </remarks>
        /// <param name="sinceID">Filter condition, only retrives the direct message after <paramref name="sinceID"/>.</param>
        /// <param name="maxID">Filter condition, only retrives the direct message before <paramref name="maxID"/>.</param> 
        /// <param name="page">The page index.</param>
        /// <param name="count">The number of direct message to return, if not specified, default to 20.</param>
        /// <returns>The direct messages of the current user.</returns>
        public static DirectMessages GetDirectMessages(long? sinceID = null, long? maxID = null, int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetDirectMessages);
         
            ConstructPagedRecordsParams(requester.Params, sinceID, maxID, page, count);

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<DirectMessages>(response);

            return result;
        }

        /// <summary>
        /// Retrieves a certain mumber of the sent direct messages of the current user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Direct_messages/sent 
        /// Extra permission required to call this API.
        /// </remarks>
        /// <param name="sinceID">Filter condition, only retrives the direct message after <paramref name="sinceID"/>.</param>
        /// <param name="maxID">Filter condition, only retrives the direct message before <paramref name="maxID"/>.</param> 
        /// <param name="page">The page index.</param>
        /// <param name="count">The number of direct message to return, if not specified, default to 20.</param>
        /// <returns>The sent direct messages of the current user.</returns>
        public static DirectMessages GetSentDirectMessages(long? sinceID = null, long? maxID = null, int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetSentDirectMessages);

            ConstructPagedRecordsParams(requester.Params, sinceID, maxID, page, count);

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<DirectMessages>(response);

            return result;
        }

        /// <summary>
        /// Sends a direct message to a specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Direct_messages/new 
        /// If the receiver is not a follower of the current user, server returns "40017:Error: can't send direct message to user who is not your follower!".
        /// Extra permission required to call this API.
        /// </remarks>
        /// <param name="receiverID">The id of the receiver.</param>
        /// <param name="message">The message to send.</param>
        /// <returns>The direct messages sent.</returns>
        public static DirectMessageInfo SendDirectMessage(long receiverID, string message)
        {
            ValidateArgument(message, "message");

            var requester = new OAuthHttpPost(APIUri.SendDirectMessage);

            requester.Params.Add("user_id", receiverID.ToString(InvariantCulture));
            requester.Params.Add("text", RFC3986Encoder.UrlEncode(message));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<DirectMessageInfo>(response);

            return result;
        }

        /// <summary>
        /// Sends a direct message to a specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Direct_messages/new 
        /// If the receiver is not a follower of the current user, server returns "40017:Error: can't send direct message to user who is not your follower!".
        /// Extra permission required to call this API.
        /// </remarks>
        /// <param name="receiverScreenName">The id of the receiver.</param>
        /// <param name="message">The message to send.</param>
        /// <returns>The direct messages sent.</returns>
        public static DirectMessageInfo SendDirectMessage(string receiverScreenName, string message)
        {
            ValidateArgument(receiverScreenName, "receiverScreenName");
            ValidateArgument(message, "message");

            var requester = new OAuthHttpPost(APIUri.SendDirectMessage);

            requester.Params.Add("screen_name", RFC3986Encoder.UrlEncode(receiverScreenName));
            requester.Params.Add("text", RFC3986Encoder.UrlEncode(message));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<DirectMessageInfo>(response);

            return result;
        }

        /// <summary>
        /// Deletes a direct message identified by the specified <paramref name="directMessageID"/>.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Direct_messages/destroy 
        /// Extra permission required to call this API.
        /// </remarks>
        /// <param name="directMessageID">The id of the direct message.</param>
        /// <returns>The direct message deleted.</returns>
        public static DirectMessageInfo DeleteDirectMessage(long directMessageID)
        {
            ValidateArgument(directMessageID, "directMessageID");

            var requester = new OAuthHttpDelete(string.Format("{0}/{1}.xml", APIUri.DeleteDirectMessage, directMessageID));
            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<DirectMessageInfo>(response);

            return result;
        }

        /// <summary>
        /// Deletes a direct message identified by the specified <paramref name="directMessageIDs"/>.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Direct_messages/destroy_batch 
        /// Extra permission required to call this API.
        /// </remarks>
        /// <param name="directMessageIDs">The id of the direct messages. At most 20 each call.</param>
        /// <returns>The direct messages deleted.</returns>
        public static DirectMessages DeleteDirectMessages(params long[] directMessageIDs)
        {
            ValidateArgument(directMessageIDs, "directMessageID");

            var count = directMessageIDs.Length;
            if (0 == count)
                throw new AMicroblogException(LocalErrorCode.ArgumentNotProvided, "Direct message IDs not provided.");

            var sBuilder = new StringBuilder();
            for (int i = 0; i < directMessageIDs.Length; i++)
            {
                sBuilder.Append(directMessageIDs[i]);
                if (i < count - 1)
                    sBuilder.Append(",");
            }

            var requester = new OAuthHttpDelete(APIUri.DeleteDirectMessages);
            requester.Params.Add("ids", sBuilder.ToString());

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<DirectMessages>(response);

            return result;
        }

        #endregion

        #region Timeline

        /// <summary>
        /// Retrieves a certain mumber of latest statuses of the specified user or current user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/user_timeline </remarks>
        /// <param name="userID">The user of whoes statues to retrieve, if not specified, default to current user.</param>
        /// <param name="sinceID">Filter condition, only retrives the statuses after <paramref name="sinceID"/>.</param>
        /// <param name="maxID">Filter condition, only retrives the statuses before <paramref name="maxID"/>.</param> 
        /// <param name="page">The page index.</param>
        /// <param name="count">The number of statuses to return, if not specified, default to 20.</param>
        /// <param name="feature">The type of statuses to retrieve.</param>
        /// <param name="currentAppOnly">Indicates whether retrieve the statuses the posted from the current app only.</param>
        /// <returns>The statuses.</returns>
        public static Statuses GetUserStatuses(long? userID = null, long? sinceID = null, long? maxID = null, int? page = null, int? count = null,
            StatusType feature = StatusType.All, bool currentAppOnly = false)
        {
            var requester = new OAuthHttpGet(APIUri.UserTimeline);
            if(userID.HasValue)
                requester.Params.Add(new ParamPair("user_id", userID.Value.ToString(InvariantCulture)));
            
            ConstructPagedRecordsParams(requester.Params, sinceID, maxID, page, count);

            if (feature != StatusType.All)
                requester.Params.Add(new ParamPair("feature", ((int)feature).ToString(InvariantCulture)));
            if(currentAppOnly)
                requester.Params.Add(new ParamPair("base_app", "1"));

            var response = requester.Request();

            var statuses = XmlSerializationHelper.XmlToObject<Statuses>(response);

            return statuses;
        }

        /// <summary>
        /// Retrieves a certain mumber of latest statuses of the public users.
        /// </summary>
        /// <remarks>
        /// Not login required.
        /// See http://open.weibo.com/wiki/Statuses/public_timeline 
        /// </remarks>
        /// <param name="count">The number of statuses to return, if not specified, default to 20.</param>
        /// <param name="currentAppOnly">Indicates whether retrieve the statuses the posted from the current app only.</param>
        /// <returns>The statuses retrieved.</returns>
        public static Statuses GetPublicStatuses(int count = 20, bool currentAppOnly = false)
        {
            var requester = new HttpGet(APIUri.PublicTimeline);
            requester.Params.Add(Constants.Source, Environment.AppKey);            

            if (count != 20)
                requester.Params.Add(new ParamPair("count", count.ToString(InvariantCulture)));            
            if (currentAppOnly)
                requester.Params.Add(new ParamPair("base_app", "1"));

            var response = requester.Request();

            var statuses = XmlSerializationHelper.XmlToObject<Statuses>(response);

            return statuses;
        }

        /// <summary>
        /// Retrieves a certain mumber of latest statuses of the friends current user is following.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/friend_timeline </remarks>
        /// <param name="sinceID">Filter condition, only retrives the statuses after <paramref name="sinceID"/>.</param>
        /// <param name="maxID">Filter condition, only retrives the statuses before <paramref name="maxID"/>.</param>
        /// <param name="page">The page index.</param>
        /// <param name="count">The number of statuses to return, if not specified, default to 20.</param>
        /// <param name="feature">The feature to retrieve.</param>
        /// <param name="currentAppOnly">Indicates whether retrieve the statuses the posted from the current app only.</param>
        /// <returns>The statuses.</returns>
        public static Statuses GetFriendsStatuses(long? sinceID = null, long? maxID = null, int? page = null, int? count = null,
            StatusType feature = StatusType.All, bool currentAppOnly = false)
        {
            var requester = new OAuthHttpGet(APIUri.FriendsTimeline);
            
            ConstructPagedRecordsParams(requester.Params, sinceID, maxID, page, count);
            
            if (feature != StatusType.All)
                requester.Params.Add(new ParamPair("feature", ((int)feature).ToString(InvariantCulture)));
            if (currentAppOnly)
                requester.Params.Add(new ParamPair("base_app", "1"));

            var response = requester.Request();

            var statuses = XmlSerializationHelper.XmlToObject<Statuses>(response);

            return statuses;
        }

        /// <summary>
        /// Retrieves the statuses which mentions the current user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/mentions </remarks>
        /// <param name="sinceID">Filter condition, only retrives the statuses after <paramref name="sinceID"/>.</param>
        /// <param name="maxID">Filter condition, only retrives the statuses before <paramref name="maxID"/>.</param>
        /// <param name="page">The page index.</param>
        /// <param name="count">The number of statuses to return, if not specified, default to 20.</param>
        /// <returns>The statuses.</returns>
        public static Statuses GetMentions(long? sinceID = null, long? maxID = null, int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetMentions);

            ConstructPagedRecordsParams(requester.Params, sinceID, maxID, page, count);

            var response = requester.Request();

            var statuses = XmlSerializationHelper.XmlToObject<Statuses>(response);

            return statuses;
        }

        /// <summary>
        /// Retrieves the comments sent/received by the current user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/comments_timeline </remarks>
        /// <param name="sinceID">Filter condition, only retrives the statuses after <paramref name="sinceID"/>.</param>
        /// <param name="maxID">Filter condition, only retrives the statuses before <paramref name="maxID"/>.</param>
        /// <param name="page">The page index.</param>
        /// <param name="count">The number of statuses to return, if not specified, default to 20.</param>
        /// <returns>The statuses.</returns>
        public static Comments GetCommentsTimeline(long? sinceID = null, long? maxID = null, int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.CommentsTimeline);

            ConstructPagedRecordsParams(requester.Params, sinceID, maxID, page, count);

            var response = requester.Request();

            var statuses = XmlSerializationHelper.XmlToObject<Comments>(response);

            return statuses;
        }

        /// <summary>
        /// Retrieves the comments sent by the current user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/comments_by_me </remarks>
        /// <param name="sinceID">Filter condition, only retrives the statuses after <paramref name="sinceID"/>.</param>
        /// <param name="maxID">Filter condition, only retrives the statuses before <paramref name="maxID"/>.</param>
        /// <param name="page">The page index.</param>
        /// <param name="count">The number of statuses to return, if not specified, default to 20.</param>
        /// <returns>The statuses.</returns>
        public static Comments GetCommentsByMe(long? sinceID = null, long? maxID = null, int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.CommentsByMe);

            ConstructPagedRecordsParams(requester.Params, sinceID, maxID, page, count);

            var response = requester.Request();

            var statuses = XmlSerializationHelper.XmlToObject<Comments>(response);

            return statuses;
        }

        /// <summary>
        /// Retrieves the comments sent to the current user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/comments_to_me </remarks>
        /// <param name="sinceID">Filter condition, only retrives the statuses after <paramref name="sinceID"/>.</param>
        /// <param name="maxID">Filter condition, only retrives the statuses before <paramref name="maxID"/>.</param>
        /// <param name="page">The page index.</param>
        /// <param name="count">The number of statuses to return, if not specified, default to 20.</param>
        /// <returns>The statuses.</returns>
        public static Comments GetCommentsToMe(long? sinceID = null, long? maxID = null, int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.CommentsToMe);

            ConstructPagedRecordsParams(requester.Params, sinceID, maxID, page, count);

            var response = requester.Request();

            var statuses = XmlSerializationHelper.XmlToObject<Comments>(response);

            return statuses;
        }

        /// <summary>
        /// Retrieves the comment counter and repost counter of the specified statuses.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/counts </remarks>
        /// <param name="statusIDs">The ids of the statuses to query. At most 20.</param>
        /// <returns>The counters.</returns>
        public static Counters GetCountersOfCommentNForward(long[] statusIDs)
        {
            ValidateArgument(statusIDs, "statusIDs");

            var requester = new OAuthHttpGet(APIUri.GetCountersOfCommentNForward);

            var statusIDsBuilder = new StringBuilder();
            for (int i = 0; i < statusIDs.Length; i++)
            {
                statusIDsBuilder.Append(statusIDs[i]);
                if (i < statusIDs.Length - 1)
                {
                    statusIDsBuilder.Append(",");
                }
            }
            requester.Params.Add(new ParamPair("ids", statusIDsBuilder.ToString()));

            var response = requester.Request();

            var counters = XmlSerializationHelper.XmlToObject<Counters>(response);

            return counters;
        }

        /// <summary>
        /// Retrieves the unread counters of the current user.
        /// </summary>
        /// <remarks>
        /// If <paramref name="sinceID"/> is specified, <paramref name="withNewStatus"/> must also be specified.
        /// See http://open.weibo.com/wiki/Statuses/unread
        /// </remarks>
        /// <param name="withNewStatus">Indicates whether to include the boolean field "WithNewStatus" in the response.</param>
        /// <param name="sinceID">Further filtering for <paramref name="withNewStatus"/> check.</param>
        /// <returns></returns>
        public static UnreadInfo GetUnreadInfo(bool withNewStatus = false, long? sinceID = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetUnreadInfo);

            if (withNewStatus)
                requester.Params.Add(new ParamPair("with_new_status", "1"));

            if(sinceID.HasValue)
                requester.Params.Add(new ParamPair("since_id", sinceID.Value.ToString(InvariantCulture)));

            var response = requester.Request();

            var unreadInfo = XmlSerializationHelper.XmlToObject<UnreadInfo>(response);

            return unreadInfo;
        }

        /// <summary>
        /// Resets the unread counters of the specified type to zero.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/reset_count </remarks>
        /// <param name="counterType">The type of counter to reset.</param>
        /// <returns>A boolean value indicating whether the counter resets to zero.</returns>
        public static void ResetCounter(CounterType counterType)
        {
            var requester = new OAuthHttpGet(APIUri.ResetCounter);
            requester.Params.Add(new ParamPair("type", ((int)counterType).ToString(InvariantCulture)));

            var response = requester.Request();
        }

        /// <summary>
        /// Retrieves the system defined emotions.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Emotions </remarks>
        /// <param name="emotionType">The type of emotion to retrive.</param>
        /// <param name="language">The language. i.e: cnname, twname</param>
        /// <returns>The emotions retrieved.</returns>
        public static Emotions GetEmotions(EmotionType emotionType = EmotionType.Image, string language = "")
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

            if(!string.IsNullOrEmpty(language))
                requester.Params.Add(new ParamPair("language", language));

            var response = requester.Request();

            response = EncodeXmlCharsPreprocess(response);

            var emotions = XmlSerializationHelper.XmlToObject<Emotions>(response);

            return emotions;
        }

        #endregion

        #region User

        /// <summary>
        /// Retrives the info of the specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Users/show </remarks>
        /// <param name="userID">The user id.</param>
        /// <returns>The user info.</returns>
        public static UserInfo GetUserInfo(long userID)
        {
            var requester = new OAuthHttpGet(string.Format("{0}/{1}.xml",APIUri.GetUserInfo, userID));

            var response = requester.Request();

            var userInfo = XmlSerializationHelper.XmlToObject<UserInfo>(response);

            return userInfo;
        }

        /// <summary>
        /// Retrives the info of the specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Users/show </remarks>
        /// <param name="screenName">The screen name of the user to retrieve.</param>
        /// <returns>The user info retrieved.</returns>
        public static UserInfo GetUserInfo(string screenName)
        {
            ValidateArgument(screenName, "screenName");

            var requester = new OAuthHttpGet(string.Format("{0}.xml", APIUri.GetUserInfo));

            requester.Params.Add(new ParamPair("screen_name", RFC3986Encoder.UrlEncode(screenName)));

            var response = requester.Request();

            var userInfo = XmlSerializationHelper.XmlToObject<UserInfo>(response);

            return userInfo;
        }

        /// <summary>
        /// Retrieves the friends of the specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/friends </remarks>
        /// <param name="userID">The user id. If not provided, defaults to current user.</param>
        /// <param name="cursor">The cursor. -1 means the first page. If provides, next cursor will be returned in the response.</param>
        /// <param name="count">The count of a page. If not provided, defaults to 20.</param>
        /// <returns>The friends retrieved.</returns>
        public static Users GetFriends(long userID, int? cursor = null, int? count = null)
        {
            return GetFriends(userID, null, cursor, count);
        }

        /// <summary>
        /// Retrieves the friends of the specified user.
        /// </summary>
        /// <remarks>
        /// See http://open.weibo.com/wiki/Statuses/friends 
        /// A friend is a microblog user you are following.
        /// </remarks>
        /// <param name="screenName">The screen name of the user. If not provided, defaults to current user.</param>
        /// <param name="cursor">The cursor. -1 means the first page. If provides, next cursor will be returned in the response.</param>
        /// <param name="count">The count of a page. If not provided, defaults to 20.</param>
        /// <returns>The friends retrieved.</returns>
        public static Users GetFriends(string screenName = null, int? cursor = null, int? count = null)
        {
            ValidateArgument(screenName, "screenName");

            return GetFriends(null, screenName, cursor, count);
        }

        /// <summary>
        /// Retrieves the friends of the specified user.
        /// </summary>
        /// <remarks>
        /// See http://open.weibo.com/wiki/Statuses/friends 
        /// One of the <paramref name="userID"/> and <paramref name="screenName"/> should be provided. If not provided, defaults to current user.
        /// </remarks>
        /// <param name="userID">The user id.</param>
        /// <param name="screenName">The screen name of the user.</param>
        /// <param name="cursor">The cursor. -1 means the first page. If provides, next cursor will be returned in the response.</param>
        /// <param name="count">The count of a page. If not provided, defaults to 20.</param>
        /// <returns>The friends retrieved.</returns>
        public static Users GetFriends(long? userID = null, string screenName = null, int? cursor = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetFriends);
            if (userID.HasValue)
                requester.Params.Add(new ParamPair("user_id", userID.Value.ToString(InvariantCulture)));

            if (!string.IsNullOrEmpty(screenName))
                requester.Params.Add(new ParamPair("screen_Name", RFC3986Encoder.UrlEncode(screenName)));

            if (cursor.HasValue)
                requester.Params.Add(new ParamPair("cursor", cursor.Value.ToString(InvariantCulture)));

            if (count.HasValue)
                requester.Params.Add(new ParamPair("count", count.Value.ToString(InvariantCulture)));

            var response = requester.Request();

            var users = XmlSerializationHelper.XmlToObject<Users>(response);

            return users;
        }

        /// <summary>
        /// Retrieves the followers of the specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/followers </remarks>
        /// <param name="userID">The user id.</param>
        /// <param name="cursor">The cursor. -1 means the first page. If provides, next cursor will be returned in the response.</param>
        /// <param name="count">The count of a page. If not provided, defaults to 20.</param>
        /// <returns>The followers retrieved.</returns>
        public static Users GetFollowers(long userID, int? cursor = null, int? count = null)
        {
            return GetFollowers(userID, null, cursor, count);
        }

        /// <summary>
        /// Retrieves the followers of the specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Statuses/followers </remarks>
        /// <param name="screenName">The screen name of the user.</param>
        /// <param name="cursor">The cursor. -1 means the first page. If provides, next cursor will be returned in the response.</param>
        /// <param name="count">The count of a page. If not provided, defaults to 20.</param>
        /// <returns>The followers retrieved.</returns>
        public static Users GetFollowers(string screenName, int? cursor = null, int? count = null)
        {
            ValidateArgument(screenName, "screenName");

            return GetFollowers(null, screenName, cursor, count);
        }

        /// <summary>
        /// Retrieves the followers of the specified user.
        /// </summary>
        /// <remarks>
        /// See http://open.weibo.com/wiki/Statuses/followers 
        /// One of the <paramref name="userID"/> and <paramref name="screenName"/> should be provided. If not provided, defaults to current user.
        /// </remarks>
        /// <param name="userID">The user id.</param>
        /// <param name="screenName">The screen name of the user.</param>
        /// <param name="cursor">The cursor. -1 means the first page. If provides, next cursor will be returned in the response.</param>
        /// <param name="count">The count of a page. If not provided, defaults to 20.</param>
        /// <returns>The followers retrieved.</returns>
        public static Users GetFollowers(long? userID = null, string screenName = null, int? cursor = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetFollowers);
            if (userID.HasValue)
                requester.Params.Add(new ParamPair("user_id", userID.Value.ToString(InvariantCulture)));

            if (!string.IsNullOrEmpty(screenName))
                requester.Params.Add(new ParamPair("screen_Name", RFC3986Encoder.UrlEncode(screenName)));

            if (cursor.HasValue)
                requester.Params.Add(new ParamPair("cursor", cursor.Value.ToString(InvariantCulture)));

            if (count.HasValue)
                requester.Params.Add(new ParamPair("count", count.Value.ToString(InvariantCulture)));

            var response = requester.Request();

            var users = XmlSerializationHelper.XmlToObject<Users>(response);

            return users;
        }

        /// <summary>
        /// Retrieves the hot users of the specified <paramref name="category"/>.
        /// </summary>
        /// <remarks>
        /// default 人气关注
        /// ent 影视名星
        /// hk_famous 港台名人
        /// model 模特
        /// cooking 美食&amp;健康
        /// sport 体育名人
        /// finance 商界名人
        /// tech IT互联网
        /// singer 歌手
        /// writer 作家
        /// moderator 主持人
        /// medium 媒体总编
        /// stockplayer 炒股高手
        /// See http://open.weibo.com/wiki/Users/hot
        /// </remarks>
        /// <param name="category">The user category, if not specified, default to 'default'.</param>
        /// <returns>The users returned.</returns>
        public static Users GetHotUsers(string category = "")
        {
            var requester = new OAuthHttpGet(APIUri.GetHotUsers);

            if (!string.IsNullOrEmpty(category))
                requester.Params.Add(new ParamPair("category", category));

            var response = requester.Request();

            var users = XmlSerializationHelper.XmlToObject<Users>(response);

            return users;
        }

        /// <summary>
        /// Updates the remark of a friend of the current user.
        /// </summary>
        /// <remarks>
        /// See http://open.weibo.com/wiki/User/friends/update_remark
        /// </remarks>
        /// <param name="friendUserID">The friend's user id.</param>
        /// <param name="remark">The remark.</param>
        /// <returns>The friend's info.</returns>
        public static Users UpdateFriendRemark(long friendUserID, string remark)
        {
            var requester = new OAuthHttpPost(APIUri.UpdateRemark);

            requester.Params.Add(new ParamPair("user_id", friendUserID.ToString(InvariantCulture)));
            requester.Params.Add(new ParamPair("remark", RFC3986Encoder.UrlEncode(remark)));

            var response = requester.Request();

            var users = XmlSerializationHelper.XmlToObject<Users>(response);

            return users;
        }

        /// <summary>
        /// Returns a bunch of users the current user may be interested in.
        /// </summary>
        /// <remarks>
        /// See http://open.weibo.com/wiki/Users/suggestions
        /// </remarks>
        /// <returns>The users returned.</returns>
        public static Users GetSuggestedUsers()
        {
            var requester = new OAuthHttpGet(APIUri.GetSuggestedUsers);

            var response = requester.Request();

            var users = XmlSerializationHelper.XmlToObject<Users>(response);

            return users;
        }

        /// <summary>
        /// Retrieves the users by the specified keyword.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Users/search 
        /// Extra permission required to call this API.
        /// </remarks>
        /// <param name="keyword">The keyword to search by.</param>
        /// <param name="page">The page index.</param>
        /// <param name="count">The count of a page. If not provided, defaults to 10.</param>
        /// <param name="currentAppOnly">Indicates whether retrieve the statuses the posted from the current app only.</param>
        /// <returns>The users found.</returns>
        public static Users SearchUsers(string keyword, int? page = null, int? count = null, bool currentAppOnly = false)
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

            var response = requester.Request();

            response = response.Replace("<searchResult>", string.Empty);
            response = response.Replace("</searchResult>", string.Empty);

            var result = XmlSerializationHelper.XmlToObject<Users>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the user suggestions by the specified keyword.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Search/suggestions/at_users </remarks>
        /// <param name="keyword">The keyword to search by.</param>
        /// <param name="type">The type of uses to search. 0: friends, 1: followers</param>
        /// <param name="count">The count of a page. If not provided, defaults to 10.</param>
        /// <param name="range">The range to search in. 0: search in name, 1: search in remark, 2: search both. Default to 2.</param>
        /// <returns>The user suggestions found.</returns>
        public static UserSuggestions GetUserSearchSuggestions(string keyword, int type = 0, int? count = null, int? range = null)
        {
            ValidateArgument("keyword", keyword);

            var requester = new OAuthHttpGet(APIUri.GetUserSearchSuggestions);

            requester.Params.Add("q", RFC3986Encoder.UrlEncode(keyword));
            if (count.HasValue)
                requester.Params.Add("count", count.Value.ToString(InvariantCulture));
            requester.Params.Add("type", type.ToString(InvariantCulture));
            if (range.HasValue)
                requester.Params.Add("range", range.Value.ToString(InvariantCulture));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<UserSuggestions>(response);

            return result;
        }

        #endregion

        #region Friendship

        /// <summary>
        /// Follows a specified user to become a fan of him/her.
        /// </summary>
        /// <remarks>
        /// One of the <paramref name="targetUserID"/> and <paramref name="targetUserScreenName"/> must be provided.
        /// See http://open.weibo.com/wiki/Friendships/create
        /// </remarks>
        /// <param name="targetUserID">The id of the user to follow.</param>
        /// <param name="targetUserScreenName">The screen name of the user to follow.</param>
        /// <returns>The info of the user followed.</returns>
        public static UserInfo Follow(long? targetUserID = null, string targetUserScreenName = null)
        {
            var requester = new OAuthHttpPost(APIUri.CreateFriendship);

            if(targetUserID.HasValue)
                requester.Params.Add(new ParamPair("user_id", targetUserID.Value.ToString(InvariantCulture)));
            if (!string.IsNullOrEmpty(targetUserScreenName))
                requester.Params.Add(new ParamPair("screen_name", targetUserScreenName));

            var response = requester.Request();

            var userInfo = XmlSerializationHelper.XmlToObject<UserInfo>(response);

            return userInfo;
        }

        /// <summary>
        /// Follows a specified user to become a fan of him/her.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Friendships/create </remarks>
        /// <param name="targetUserID">The id of the user to follow.</param>
        /// <returns>The info of the user followed.</returns>
        public static UserInfo Follow(long targetUserID)
        {
            return Follow(targetUserID, null);
        }

        /// <summary>
        /// Follows a specified user to become a fan of him/her.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Friendships/create </remarks>
        /// <param name="targetUserScreenName">The screen name of the user to follow.</param>
        /// <returns>The info of the user followed.</returns>
        public static UserInfo Follow(string targetUserScreenName)
        {
            ValidateArgument(targetUserScreenName, "targetUserScreenName");

            return Follow(null, targetUserScreenName);
        }

        /// <summary>
        /// Unfollows a specified user.
        /// </summary>
        /// <remarks>One of the <paramref name="targetUserID"/> and <paramref name="targetUserScreenName"/> must be provided.
        /// See http://open.weibo.com/wiki/Friendships/destroy
        /// </remarks>
        /// <param name="targetUserID">The id of the user to follow.</param>
        /// <param name="targetUserScreenName">The screen name of the user to follow.</param>
        /// <returns>The info of the user.</returns>
        public static UserInfo Unfollow(long? targetUserID = null, string targetUserScreenName = null)
        {
            var requester = new OAuthHttpPost(APIUri.DeleteFriendship);

            if (targetUserID.HasValue)
                requester.Params.Add(new ParamPair("user_id", targetUserID.Value.ToString(InvariantCulture)));

            if (!string.IsNullOrEmpty(targetUserScreenName))
                requester.Params.Add(new ParamPair("screen_name", targetUserScreenName));

            var response = requester.Request();

            var userInfo = XmlSerializationHelper.XmlToObject<UserInfo>(response);

            return userInfo;
        }

        /// <summary>
        /// Unfollows a specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Friendships/destroy </remarks>
        /// <param name="targetUserID">The id of the user to follow.</param>
        /// <returns>The info of the user.</returns>
        public static UserInfo Unfollow(long targetUserID)
        { 
            return Unfollow(targetUserID, null);
        }

        /// <summary>
        /// Unfollows a specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Friendships/destroy </remarks>
        /// <param name="targetUserScreenName">The screen name of the user to follow.</param>
        /// <returns>The info of the user.</returns>
        public static UserInfo Unfollow(string targetUserScreenName)
        {
            ValidateArgument(targetUserScreenName, "targetUserScreenName");

            return Unfollow(null, targetUserScreenName);
        }

        /// <summary>
        /// Checks whether <paramref name="userAID"/> followed <paramref name="userBID"/>.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Friendships/exists </remarks>
        /// <param name="userAID">The id of the user A.</param>
        /// <param name="userBID">The id of the user B.</param>
        /// <returns>The info of the user.</returns>
        public static bool ExistsFriendship(long userAID, long userBID)
        {
            var requester = new OAuthHttpGet(APIUri.ExistsFriendship);

            requester.Params.Add(new ParamPair("user_a", userAID.ToString(InvariantCulture)));
            requester.Params.Add(new ParamPair("user_b", userBID.ToString(InvariantCulture)));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<ExistsFriendshipResultInfo>(response);

            return result.Value;
        }

        /// <summary>
        /// Retrieves the friendship info of the users identified by <paramref name="targetUserID"/> and <paramref name="sourceUserID"/>.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Friendships/show </remarks>
        /// <param name="targetUserID">The id of the target user.</param>
        /// <param name="sourceUserID">The id of the source user. If not provided, default to current user.</param>
        /// <returns>The info of the friendship.</returns>
        public static RelationshipInfo GetFriendshipInfo(long targetUserID, long? sourceUserID = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetFriendshipInfo);

            if(sourceUserID.HasValue)
                requester.Params.Add(new ParamPair("source_id", sourceUserID.Value.ToString(InvariantCulture)));
            requester.Params.Add(new ParamPair("target_id", targetUserID.ToString(InvariantCulture)));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<RelationshipInfo>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the id of the users that the specified user is following.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Friends/ids </remarks>
        /// <param name="userID">The id of the user. If not provided, defaults to current user.</param>
        /// <param name="cursor">The cursor. -1 means the first page. If provides, next cursor will be returned in the response.</param>
        /// <param name="count">The count of a page. If not provided, defaults to 20.</param>
        /// <returns>The user ids.</returns>
        public static UserIDs GetFollowingUserIDs(long? userID = null, int? cursor = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetFollowingUserIDs);

            if (userID.HasValue)
                requester.Params.Add(new ParamPair("user_id", userID.Value.ToString(InvariantCulture)));
            if (cursor.HasValue)
                requester.Params.Add(new ParamPair("cursor", cursor.Value.ToString(InvariantCulture)));
            if (count.HasValue)
                requester.Params.Add(new ParamPair("count", count.Value.ToString(InvariantCulture)));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<UserIDs>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the id of the users who follows the specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Followers/ids </remarks>
        /// <param name="userID">The id of the user. If not provided, defaults to current user.</param>
        /// <param name="screenName">The screen name of the user. If not provided, defaults to current user.</param>
        /// <param name="cursor">The cursor. -1 means the first page. If provides, next cursor will be returned in the response.</param>
        /// <param name="count">The count of a page. If not provided, defaults to 20.</param>
        /// <returns>The user ids.</returns>
        public static UserIDs GetFollowerUserIDs(long? userID = null, string screenName = null, int? cursor = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetFollowerUserIDs);

            if (userID.HasValue)
                requester.Params.Add(new ParamPair("user_id", userID.Value.ToString(InvariantCulture)));
            if(!string.IsNullOrEmpty(screenName))
                requester.Params.Add(new ParamPair("screen_name", RFC3986Encoder.UrlEncode(screenName)));
            if (cursor.HasValue)
                requester.Params.Add(new ParamPair("cursor", cursor.Value.ToString(InvariantCulture)));
            if (count.HasValue)
                requester.Params.Add(new ParamPair("count", count.Value.ToString(InvariantCulture)));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<UserIDs>(response);

            return result;
        }

        #endregion

        #region Tag

        /// <summary>
        /// Retrieves the tags of the specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Tags </remarks>
        /// <param name="userID">The id of the user.</param>
        /// <param name="count">The count of a page. If not provided, defaults to 20.</param>
        /// <param name="page">The page no.</param>
        /// <returns>The tags returned.</returns>
        public static Tags GetTags(long userID, int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetTags);

            requester.Params.Add("user_id", userID.ToString(InvariantCulture));
            if (page.HasValue)
                requester.Params.Add(new ParamPair("page", page.Value.ToString(InvariantCulture)));
            if (count.HasValue)
                requester.Params.Add(new ParamPair("count", count.Value.ToString(InvariantCulture)));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<Tags>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the tags of the specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Tags/suggestions </remarks>
        /// <param name="count">The count of a page. If not provided, defaults to 10.</param>
        /// <param name="page">The page no.</param>
        /// <returns>The tags returned.</returns>
        public static Tags GetSuggestedTags(int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetSuggestedTags);

            if (page.HasValue)
                requester.Params.Add(new ParamPair("page", page.Value.ToString(InvariantCulture)));
            if (count.HasValue)
                requester.Params.Add(new ParamPair("count", count.Value.ToString(InvariantCulture)));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<Tags>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the tags of the specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Tags/create </remarks>
        /// <param name="tags">The tags. 10 at most for each user.</param>
        /// <returns>The id of the tags returned.</returns>
        public static TagIDs CreateTags(params string[] tags)
        {
            ValidateArgument(tags, "tags");

            var count = tags.Length;
            if (count == 0)
                throw new AMicroblogException(LocalErrorCode.ArgumentNotProvided, "Tags not provided.");

            var tagBuilder = new StringBuilder();
            for (int i = 0; i < tags.Length; i++)
            {
                tagBuilder.Append(tags[i]);
                if( i < count -1)
                    tagBuilder.Append(",");
            }

            var requester = new OAuthHttpPost(APIUri.CreateTags);

            requester.Params.Add("tags", tagBuilder.ToString());

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<TagIDs>(response);

            return result;
        }

        /// <summary>
        /// Deletes a user tag by the <paramref name="tagID"/>.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Tags/destroy </remarks>
        /// <param name="tagID">The id of the tag.</param>
        public static void DeleteTag(long tagID)
        {
            var requester = new OAuthHttpDelete(APIUri.DeleteTag);

            requester.Params.Add("tag_id", tagID.ToString(InvariantCulture));

            requester.Request();
        }

        /// <summary>
        /// Deletes a bunch of user tags by the <paramref name="tagIDs"/>.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Tags/destroy_batch </remarks>
        /// <param name="tagIDs">The ids of the tags to delete. At most 20 each time.</param>
        public static TagIDs DeleteTags(params long[] tagIDs)
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

            var response = requester.Request();

            response = response.Replace("tags>", "tagids>");

            var result = XmlSerializationHelper.XmlToObject<TagIDs>(response);

            return result;
        }

        #endregion

        #region Favorite

        /// <summary>
        /// Retrieves the favorite of the current user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Favorites </remarks>
        /// <param name="page">The page no.</param>
        /// <returns>The statuses in favorite.</returns>
        public static Statuses GetFavorites(int? page = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetFavorites);

            if (page.HasValue)
                requester.Params.Add(new ParamPair("page", page.Value.ToString(InvariantCulture)));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<Statuses>(response);

            return result;
        }

        /// <summary>
        /// Adds the specified status into the favorite of current user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Favorites/create </remarks>
        /// <param name="statusID">The id of the status to add to.</param>
        /// <returns>The status info.</returns>
        public static StatusInfo AddToFavorite(long statusID)
        {
            var requester = new OAuthHttpPost(APIUri.AddToFavorite);

            requester.Params.Add("id", statusID.ToString(InvariantCulture));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<StatusInfo>(response);

            return result;
        }

        /// <summary>
        /// Deletes the specified status from the favorite of current user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Favorites/destroy </remarks>
        /// <param name="statusID">The id of the status to delete from.</param>
        /// <returns>The status info.</returns>
        public static StatusInfo DeleteFromFavorite(long statusID)
        {
            var requester = new OAuthHttpDelete(string.Format("{0}/{1}.xml", APIUri.DeleteFromFavorite, statusID));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<StatusInfo>(response);

            return result;
        }

        /// <summary>
        /// Deletes the specified statuses from the favorite of current user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Favorites/destroy_batch </remarks>
        /// <param name="statusIDs">The ids of the status to delete from.</param>
        /// <returns>The status info.</returns>
        public static Statuses DeleteMultipleFromFavorite(params long[] statusIDs)
        {
            ValidateArgument(statusIDs, "statusIDs");

            var count = statusIDs.Length;
            if (count == 0)
                throw new AMicroblogException(LocalErrorCode.ArgumentNotProvided, "Status IDs not provided.");

            var tagBuilder = new StringBuilder();
            for (int i = 0; i < statusIDs.Length; i++)
            {
                tagBuilder.Append(statusIDs[i]);
                if (i < count - 1)
                    tagBuilder.Append(",");
            }

            var requester = new OAuthHttpDelete(APIUri.DeleteMultipleFromFavorite);

            requester.Params.Add("ids", tagBuilder.ToString());

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<Statuses>(response);

            return result;
        }

        #endregion

        #region Block

        /// <summary>
        /// Retrieves the block list of the specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Blocks/blocking </remarks>
        /// <param name="count">The count of a page. If not provided, defaults to 20.</param>
        /// <param name="page">The page no.</param>
        /// <returns>The blocking users returned.</returns>
        public static Users GetBlockingList(int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetBlockingList);

            if (page.HasValue)
                requester.Params.Add("page", page.Value.ToString(InvariantCulture));
            if (count.HasValue)
                requester.Params.Add("count", count.Value.ToString(InvariantCulture));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<Users>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the user ids of the block list of the specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Blocks/blocking/ids </remarks>
        /// <param name="count">The count of a page. If not provided, defaults to 20.</param>
        /// <param name="page">The page no.</param>
        /// <returns>The blocking users returned.</returns>
        public static UserIDs GetBlockingListIDs(int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetBlockingListIDs);

            if (page.HasValue)
                requester.Params.Add("page", page.Value.ToString(InvariantCulture));
            if (count.HasValue)
                requester.Params.Add("count", count.Value.ToString(InvariantCulture));

            var response = requester.Request();

            response = response.Replace("<ids>", "<id_list><ids>");
            response = response.Replace("</ids>", "</ids></id_list>");

            var result = XmlSerializationHelper.XmlToObject<UserIDs>(response);

            return result;
        }

        /// <summary>
        /// Blocks the specified user for current user.
        /// </summary>
        /// <remarks>
        /// One of the <paramref name="userID"/> and <paramref name="screenName"/> must be specified.
        /// See http://open.weibo.com/wiki/Blocks/create 
        /// </remarks>
        /// <param name="userID">The id of the user to block.</param>
        /// <param name="screenName">The screen name of the user to block.</param>
        /// <returns>The blocking user info.</returns>
        public static UserInfo Block(long? userID = null, string screenName = null)
        {
            var requester = new OAuthHttpPost(APIUri.Block);

            if (userID.HasValue)
                requester.Params.Add("user_id", userID.Value.ToString(InvariantCulture));
            if (!string.IsNullOrEmpty(screenName))
                requester.Params.Add("screen_name", RFC3986Encoder.UrlEncode(screenName));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<UserInfo>(response);

            return result;
        }

        /// <summary>
        /// Unblocks the specified user from the block list of current user.
        /// </summary>
        /// <remarks>
        /// One of the <paramref name="userID"/> and <paramref name="screenName"/> must be specified.
        /// See http://open.weibo.com/wiki/Blocks/destroy 
        /// </remarks>
        /// <param name="userID">The id of the user to unblock.</param>
        /// <param name="screenName">The screen name of the user to unblock.</param>
        /// <returns>The blocking user info.</returns>
        public static UserInfo Unblock(long? userID = null, string screenName = null)
        {
            var requester = new OAuthHttpDelete(APIUri.Unblock);

            if (userID.HasValue)
                requester.Params.Add("user_id", userID.Value.ToString(InvariantCulture));
            if (!string.IsNullOrEmpty(screenName))
                requester.Params.Add("screen_name", RFC3986Encoder.UrlEncode(screenName));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<UserInfo>(response);

            return result;
        }

        /// <summary>
        /// Checks whether the specified user is in the block list of current user.
        /// </summary>
        /// <remarks>
        /// One of the <paramref name="userID"/> and <paramref name="screenName"/> must be specified.
        /// See http://open.weibo.com/wiki/Blocks/exists 
        /// </remarks>
        /// <param name="userID">The id of the user to check.</param>
        /// <param name="screenName">The screen name of the user to check.</param>
        /// <returns>The blocking user info.</returns>
        public static bool IsBlocked(long? userID = null, string screenName = null)
        {
            var requester = new OAuthHttpDelete(APIUri.IsBlocked);

            if (userID.HasValue)
                requester.Params.Add("user_id", userID.Value.ToString(InvariantCulture));
            if (!string.IsNullOrEmpty(screenName))
                requester.Params.Add("screen_name", RFC3986Encoder.UrlEncode(screenName));

            var response = requester.Request();

            var result = response.Contains("true");

            return result;
        }

        #endregion

        #region Trend(Topic)

        /// <summary>
        /// Retrieves the trends(topic) of the specified user.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Trends </remarks>
        /// <param name="userID">The id of the user. </param>
        /// <param name="page">The page no.  If not provided, defaults to 1.</param>
        /// <param name="count">The count of a page. If not provided, defaults to 10.</param>
        /// <returns>The trends returned.</returns>
        public static Trends GetUserTrends(long userID, int? page = null, int? count = null)
        {
            var requester = new OAuthHttpGet(APIUri.GetUserTrends);

            requester.Params.Add("user_id", userID.ToString(InvariantCulture));

            if (page.HasValue)
                requester.Params.Add("page", page.Value.ToString(InvariantCulture));
            if (count.HasValue)
                requester.Params.Add("count", count.Value.ToString(InvariantCulture));

            var response = requester.Request();

            response = response.Replace("trends", "userTrends").Replace("trend", "userTrend");

            var result = XmlSerializationHelper.XmlToObject<Trends>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the trends(topic) of current hour.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Trends/hourly </remarks>
        /// <param name="basedOnCurrentApp">Indicates whether to retrieve based on current app.</param>
        /// <returns>The trends returned.</returns>
        public static PeriodTrends GetHourTrends(bool basedOnCurrentApp = false)
        {
            var requester = new OAuthHttpGet(APIUri.GetHourTrends);

            if (basedOnCurrentApp)
                requester.Params.Add("base_app", "1");
            else
                requester.Params.Add("base_app", "0");
            
            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<PeriodTrends>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the trends(topic) of today.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Trends/daily </remarks>
        /// <param name="basedOnCurrentApp">Indicates whether to retrieve based on current app.</param>
        /// <returns>The trends returned.</returns>
        public static PeriodTrends GetDayTrends(bool basedOnCurrentApp = false)
        {
            var requester = new OAuthHttpGet(APIUri.GetDayTrends);

            if (basedOnCurrentApp)
                requester.Params.Add("base_app", "1");
            else
                requester.Params.Add("base_app", "0");

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<PeriodTrends>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the trends(topic) of current week.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Trends/weekly </remarks>
        /// <param name="basedOnCurrentApp">Indicates whether to retrieve based on current app.</param>
        /// <returns>The trends returned.</returns>
        public static PeriodTrends GetWeekTrends(bool basedOnCurrentApp = false)
        {
            var requester = new OAuthHttpGet(APIUri.GetWeekTrends);

            if (basedOnCurrentApp)
                requester.Params.Add("base_app", "1");
            else
                requester.Params.Add("base_app", "0");

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<PeriodTrends>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the statuses under the specified trend(topic).
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Trends/statuses </remarks>
        /// <param name="trendName">The name(hot word) of the trend.</param>
        /// <returns>The statuses returned.</returns>
        public static Statuses GetTrendStatuses(string trendName)
        {
            ValidateArgument(trendName, "trendName");

            var requester = new OAuthHttpGet(APIUri.GetTrendStatuses);

            requester.Params.Add("trend_name", RFC3986Encoder.UrlEncode(trendName));

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<Statuses>(response);

            return result;
        }

        /// <summary>
        /// Follows the specified trend(topic).
        /// </summary>
        /// <remarks>
        /// If the current user already followed the trend, error 40028 is returned.
        /// See http://open.weibo.com/wiki/Trends/follow </remarks>
        /// <param name="trendName">The name(hot word) of the trend.</param>
        /// <returns>The id of the trend followed.</returns>
        public static long FollowTrend(string trendName)
        {
            ValidateArgument(trendName, "trendName");

            var requester = new OAuthHttpPost(APIUri.FollowTrend);

            requester.Params.Add("trend_name", RFC3986Encoder.UrlEncode(trendName));

            var response = requester.Request();

            var match = Regex.Match(response, @"<topicid>(\d+?)</topicid>", RegexOptions.IgnoreCase);

            var result = long.Parse(match.Groups[1].Value);

            return result;
        }

        /// <summary>
        /// Unfollows the specified trend(topic).
        /// </summary>
        /// <remarks>
        /// If the current user already followed the trend, error 40028 is returned.
        /// See http://open.weibo.com/wiki/Trends/unfollow </remarks>
        /// <param name="trendID">The id of the trend.</param>
        /// <returns>A boolean value indicating whether the operation succeed.</returns>
        public static bool UnfollowTrend(long trendID)
        {
            var requester = new OAuthHttpDelete(APIUri.UnfollowTrend);

            requester.Params.Add("trend_id", trendID.ToString(InvariantCulture));

            var response = requester.Request();

            var match = Regex.Match(response, @"<result>(\w+?)</result>", RegexOptions.IgnoreCase);

            var result = bool.Parse(match.Groups[1].Value);

            return result;
        }

        #endregion

        #region Urls

        /// <summary>
        /// Converts the specified <paramref name="longUrls"/> into short urls.
        /// </summary>
        /// <remarks>
        /// Not login required.
        /// See http://open.weibo.com/wiki/Short_url/shorten 
        /// </remarks>
        /// <param name="longUrls">The long urls. 20 at most each call. Make sure the long urls are url-encoded. i.e: http://open.weibo.com/wiki/API%E6%96%87%E6%A1%A3 </param>
        /// <returns>The url info.</returns>
        public static Urls ConvertToShortUrls(params string[] longUrls)
        {
            ValidateArgument(longUrls, "longUrls");

            var count = longUrls.Length;
            if (count == 0)
                throw new AMicroblogException(LocalErrorCode.ArgumentNotProvided, "Long urls not provided.");

            var requester = new HttpGet(APIUri.ConvertToShortUrls);
            requester.Params.Add(Constants.Source, Environment.AppKey);            

            foreach (var longUrl in longUrls)
            {
                requester.Params.Add("url_long", longUrl);
            }

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<Urls>(response);

            return result;
        }

        /// <summary>
        /// Converts the specified <paramref name="shortUrls"/> back into original long urls.
        /// </summary>
        /// <remarks>
        /// Not login required.
        /// See http://open.weibo.com/wiki/Short_url/expand 
        /// </remarks>
        /// <param name="shortUrls">The long urls. 20 at most each call.</param>
        /// <returns>The url info.</returns>
        public static Urls ConvertToLongUrls(params string[] shortUrls)
        {
            ValidateArgument(shortUrls, "shortUrls");

            var count = shortUrls.Length;
            if (count == 0)
                throw new AMicroblogException(LocalErrorCode.ArgumentNotProvided, "Short urls not provided.");

            var requester = new HttpGet(APIUri.ConvertToLongUrls);
            requester.Params.Add(Constants.Source, Environment.AppKey);            

            foreach (var longUrl in shortUrls)
            {
                requester.Params.Add("url_short", longUrl);
            }

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<Urls>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the shared count of the specified <paramref name="shortUrl"/>.
        /// </summary>
        /// <remarks>
        /// Not login required.
        /// See http://open.weibo.com/wiki/Short_url/share/counts 
        /// </remarks>
        /// <param name="shortUrl">The short url.</param>
        /// <returns>The url info.</returns>
        public static Urls GetShortUrlSharedCount(string shortUrl)
        {
            ValidateArgument(shortUrl, "shortUrl");

            var requester = new HttpGet(APIUri.GetShortUrlSharedCount);
            requester.Params.Add(Constants.Source, Environment.AppKey);
            requester.Params.Add("url_short", shortUrl);

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<Urls>(response);

            return result;
        }      
        
        /// <summary>
        /// Retrieves the statuses in which the specified <paramref name="shortUrl"/> is shared.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Short_url/share/statuses </remarks>
        /// <param name="shortUrl">The short url.</param>
        /// <param name="sinceID">Filter condition, only retrives the statuses after <paramref name="sinceID"/>.</param>
        /// <param name="maxID">Filter condition, only retrives the statuses before <paramref name="maxID"/>.</param> 
        /// <param name="page">The page index.</param>
        /// <param name="count">The number of statuses to return, if not specified, default to 20.</param>
        /// <returns>The statuses.</returns>
        public static Statuses GetShortUrlSharedStatuses(string shortUrl, long? sinceID = null, long? maxID = null, int? page = null, int? count = null)
        {
            ValidateArgument(shortUrl, "shortUrl");

            var requester = new OAuthHttpGet(APIUri.GetShortUrlSharedStatuses);
            requester.Params.Add("url_short", shortUrl);

            ConstructPagedRecordsParams(requester.Params, sinceID, maxID, page, count);

            var response = requester.Request();

            response = response.Replace("<url>", "");
            response = response.Replace("</url>", "");
            response = response.Replace("<share_statuses>", "<statuses>");
            response = response.Replace("</share_statuses>", "</statuses>");

            response = Regex.Replace(response, "<url_short>(.+?)</url_short>", "");

            var result = XmlSerializationHelper.XmlToObject<Statuses>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the comment count of the specified <paramref name="shortUrl"/>.
        /// </summary>
        /// <remarks>
        /// Not login required.
        /// See http://open.weibo.com/wiki/Short_url/comment/counts
        /// </remarks>
        /// <param name="shortUrl">The short url.</param>
        /// <returns>The url info.</returns>
        public static Urls GetShortUrlCommentCount(string shortUrl)
        {
            ValidateArgument(shortUrl, "shortUrl");

            var requester = new HttpGet(APIUri.GetShortUrlCommentCount);
            requester.Params.Add(Constants.Source, Environment.AppKey);
            requester.Params.Add("url_short", shortUrl);

            var response = requester.Request();

            var result = XmlSerializationHelper.XmlToObject<Urls>(response);

            return result;
        }

        /// <summary>
        /// Retrieves the comment in which the specified <paramref name="shortUrl"/> is shared.
        /// </summary>
        /// <remarks>See http://open.weibo.com/wiki/Short_url/comment/comments </remarks>
        /// <param name="shortUrl">The short url.</param>
        /// <param name="sinceID">Filter condition, only retrives the statuses after <paramref name="sinceID"/>.</param>
        /// <param name="maxID">Filter condition, only retrives the statuses before <paramref name="maxID"/>.</param> 
        /// <param name="page">The page index.</param>
        /// <param name="count">The number of statuses to return, if not specified, default to 20.</param>
        /// <returns>The comments.</returns>
        public static Comments GetShortUrlComments(string shortUrl, long? sinceID = null, long? maxID = null, int? page = null, int? count = null)
        {
            ValidateArgument(shortUrl, "shortUrl");

            var requester = new OAuthHttpGet(APIUri.GetShortUrlComments);
            requester.Params.Add("url_short", shortUrl);

            ConstructPagedRecordsParams(requester.Params, sinceID, maxID, page, count);

            var response = requester.Request();

            response = response.Replace("<url>", "");
            response = response.Replace("</url>", "");
            response = response.Replace("<share_comments>", "<comments>");
            response = response.Replace("</share_comments>", "</comments>");

            response = Regex.Replace(response, "<url_short>(.+?)</url_short>", "");

            var result = XmlSerializationHelper.XmlToObject<Comments>(response);

            return result;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Replaces the special characters in the response xml.
        /// </summary>
        /// <example>For example: replaces '&amp;'</example>
        /// <param name="response">The response(in xml)</param>
        /// <returns>Preprocessed xml.</returns>
        private static string EncodeXmlCharsPreprocess(string response)
        {
            return response.Replace("&", "&amp;");
        }

        /// <summary>
        /// Processes async callback response for return-void type methods.
        /// </summary>
        /// <param name="result">The async call result.</param>
        /// <param name="callback">The callback.</param>
        private static void ProcessAsyncCallbackVoidResponse(AsyncCallResult<string> result, VoidAsyncCallback callback)
        {
            var callbackResult = new AsyncCallResult();

            callbackResult.Success = result.Success;
            callbackResult.Exception = result.Exception;

            callback(callbackResult);
        }

        /// <summary>
        /// Processes async callback response by XML de-serialization.
        /// </summary>
        /// <typeparam name="T">The strong type represents the response content.</typeparam>
        /// <param name="result">The async call result.</param>
        /// <param name="preprocess">The func to preprocess the response content before XML de-serialization.</param>
        /// <param name="callback">The callback.</param>
        private static void ProcessAsyncCallbackResponse<T>(AsyncCallResult<string> result, AsyncCallback<T> callback, Func<string, string> preprocess = null) where T : class
        {
            var callbackResult = new AsyncCallResult<T>();
            try
            {
                callbackResult.Success = result.Success;
                callbackResult.Exception = result.Exception;
                if (result.Success)
                {
                    if (null != preprocess)
                        result.Data = preprocess(result.Data);

                    callbackResult.Data = XmlSerializationHelper.XmlToObject<T>(result.Data);
                }
            }
            catch (InvalidOperationException ioex)
            {
                // Contract mismatch.
                throw new AMicroblogException(LocalErrorCode.ParseResponseFailed, "Failed to parse server response. Server probably has updated the API contract.", ioex);
            }

            callback(callbackResult);
        }

        /// <summary>
        /// Processes async callback response by customized logic.
        /// </summary>
        /// <typeparam name="T">The strong type represents the response content.</typeparam>
        /// <param name="result">The async call result.</param>
        /// <param name="customProcess">The func to preprocess the response content.</param>
        /// <param name="callback">The callback.</param>
        private static void ProcessAsyncCallbackResponseCustom<T>(AsyncCallResult<string> result, AsyncCallback<T> callback, Func<string, T> customProcess)
        {
            var callbackResult = new AsyncCallResult<T>();
            try
            {
                callbackResult.Success = result.Success;
                callbackResult.Exception = result.Exception;
                if (result.Success)
                {
                    callbackResult.Data = customProcess(result.Data);
                }
            }
            catch (InvalidOperationException ioex)
            {
                // Contract mismatch.
                throw new AMicroblogException(LocalErrorCode.ParseResponseFailed, "Failed to parse server response. Server probably has updated the API contract.", ioex);
            }

            callback(callbackResult);
        }

        /// <remarks/>
        private static void ConstructPagedRecordsParams(ICollection<ParamPair> additionalParams, long? sinceID = null, long? maxID = null, int? page = null, int? count = null)
        {
            if (sinceID.HasValue)
                additionalParams.Add(new ParamPair("since_id", sinceID.Value.ToString(InvariantCulture)));
            if (maxID.HasValue)
                additionalParams.Add(new ParamPair("max_id", maxID.Value.ToString(InvariantCulture)));
            if (count.HasValue)
                additionalParams.Add(new ParamPair("count", count.Value.ToString(InvariantCulture)));
            if (page.HasValue)
                additionalParams.Add(new ParamPair("page", page.Value.ToString(InvariantCulture)));
        }

        /// <summary>
        /// Validates whether the specified augument is null or not.
        /// </summary>
        /// <param name="arg">The argument to validate.</param>
        /// <param name="argName">The argument name.</param>
        private static void ValidateArgument(object arg, string argName)
        {
            if (null == arg || string.IsNullOrEmpty(arg.ToString()))
                throw new AMicroblogException(LocalErrorCode.ArgumentNotProvided, "Argument '{0}' should not be null.", argName);
        }

        #endregion
    }
}
