using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.OAuth.Sina.Common
{
    /// <summary>
    /// Defines the SINA micro-blog API's uris (in xml format if applicable).
    /// </summary>
    public static class APIUri
    {
        /// <remarks/>
        public const string WeiboUriPrefix = "http://api.t.sina.com.cn/";

        #region OAuth(Login)

        /// <remarks/>
        public static readonly string RequestToken = string.Format("{0}oauth/request_token", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string Authorize = string.Format("{0}oauth/authorize", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string AccessToken = string.Format("{0}oauth/access_token", WeiboUriPrefix);

        #endregion

        #region Account

        /// <remarks/>
        public static readonly string VerifyCredential = string.Format("{0}account/verify_credentials.xml", WeiboUriPrefix);
        
        /// <remarks/>
        public static readonly string EndSession = string.Format("{0}account/end_session.xml", WeiboUriPrefix);
        
        /// <remarks/>
        public static readonly string GetRateLimitStatus = string.Format("{0}account/rate_limit_status.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string UpdateProfileImage = string.Format("{0}account/update_profile_image.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string UpdateProfile = string.Format("{0}account/update_profile.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string UpdatePrivacy = string.Format("{0}account/update_privacy.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetPrivacy = string.Format("{0}account/get_privacy.xml", WeiboUriPrefix);

        #endregion

        #region Direct Message

        /// <remarks/>
        public static readonly string GetDirectMessages = string.Format("{0}direct_messages.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetSentDirectMessages = string.Format("{0}direct_messages/sent.xml", WeiboUriPrefix);
        
        /// <remarks/>
        public static readonly string SendDirectMessage = string.Format("{0}direct_messages/new.xml", WeiboUriPrefix);
        
        /// <remarks/>
        public static readonly string DeleteDirectMessage = string.Format("{0}direct_messages/destroy", WeiboUriPrefix);
        
        /// <remarks/>
        public static readonly string DeleteDirectMessages = string.Format("{0}direct_messages/destroy_batch", WeiboUriPrefix);
        
        #endregion

        #region Status

        /// <remarks/>
        public static readonly string ShowStatus = string.Format("{0}statuses/show", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetStatusUrl = WeiboUriPrefix + "{0}/statuses/{1}.xml";
        
        /// <remarks/>
        public static readonly string UpdateStatus = string.Format("{0}statuses/update.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string UpdateStatusWithPic = string.Format("{0}statuses/upload.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string UpdateStatusAndPicUrl = string.Format("{0}statuses/upload_url_text.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string DeleteStatus = string.Format("{0}statuses/destroy", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string RepostStatus = string.Format("{0}statuses/repost.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string Comment = string.Format("{0}statuses/comment.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetComments = string.Format("{0}statuses/comments.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string ReplyComment = string.Format("{0}statuses/reply.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string DeleteComment = string.Format("{0}statuses/comment_destroy", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string DeleteComments = string.Format("{0}statuses/comment/destroy_batch.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string SearchStatuses = string.Format("{0}statuses/search.xml", WeiboUriPrefix);

        #endregion

        #region Timeline

        /// <remarks/>
        public static readonly string PublicTimeline = string.Format("{0}statuses/public_timeline.xml", WeiboUriPrefix);
        
        /// <remarks/>
        public static readonly string FriendsTimeline = string.Format("{0}statuses/friends_timeline.xml", WeiboUriPrefix);
        
        /// <remarks/>
        public static readonly string UserTimeline = string.Format("{0}statuses/user_timeline.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetMentions = string.Format("{0}statuses/mentions.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string CommentsTimeline = string.Format("{0}statuses/comments_timeline.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string CommentsByMe = string.Format("{0}statuses/comments_by_me.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string CommentsToMe = string.Format("{0}statuses/comments_to_me.xml", WeiboUriPrefix);
        
        /// <remarks/>
        public static readonly string GetCountersOfCommentNForward = string.Format("{0}statuses/counts.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetUnreadInfo = string.Format("{0}statuses/unread.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string ResetCounter = string.Format("{0}statuses/reset_count.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetEmotions = string.Format("{0}emotions.xml", WeiboUriPrefix);

        #endregion

        #region User

        /// <remarks/>
        public static readonly string GetUserInfo = string.Format("{0}users/show", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetFriends = string.Format("{0}statuses/friends.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetFollowers = string.Format("{0}statuses/followers.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetHotUsers = string.Format("{0}users/hot.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string UpdateRemark = string.Format("{0}users/update_remark.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetSuggestedUsers = string.Format("{0}users/suggestions.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string SearchUsers = string.Format("{0}users/search.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetUserSearchSuggestions = string.Format("{0}search/suggestions/at_users.xml", WeiboUriPrefix);

        #endregion

        #region Friendship

        /// <remarks/>
        public static readonly string CreateFriendship = string.Format("{0}friendships/create.xml", WeiboUriPrefix);
        
        /// <remarks/>
        public static readonly string DeleteFriendship = string.Format("{0}friendships/destroy.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string ExistsFriendship = string.Format("{0}friendships/exists.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetFriendshipInfo = string.Format("{0}friendships/show.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetFollowingUserIDs = string.Format("{0}friends/ids.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetFollowerUserIDs = string.Format("{0}followers/ids.xml", WeiboUriPrefix);

        #endregion

        #region Tag

        /// <remarks/>
        public static readonly string GetTags = string.Format("{0}tags.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string CreateTags = string.Format("{0}tags/create.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string DeleteTag = string.Format("{0}tags/destroy.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string DeleteTags = string.Format("{0}tags/destroy_batch.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetSuggestedTags = string.Format("{0}tags/suggestions.xml", WeiboUriPrefix);

        #endregion

        #region Favorite
        
        /// <remarks/>
        public static readonly string GetFavorites = string.Format("{0}favorites.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string AddToFavorite = string.Format("{0}favorites/create.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string DeleteFromFavorite = string.Format("{0}favorites/destroy", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string DeleteMultipleFromFavorite = string.Format("{0}favorites/destroy_batch.xml", WeiboUriPrefix);

        #endregion

        #region Block
        
        /// <remarks/>
        public static readonly string GetBlockingList = string.Format("{0}blocks/blocking.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetBlockingListIDs = string.Format("{0}blocks/blocking/ids.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string Block = string.Format("{0}blocks/create.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string Unblock = string.Format("{0}blocks/destroy.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string IsBlocked = string.Format("{0}blocks/exists.xml", WeiboUriPrefix);

        #endregion

        #region Trends(Topics)

        /// <remarks/>
        public static readonly string GetUserTrends = string.Format("{0}trends.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetHourTrends = string.Format("{0}trends/hourly.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetDayTrends = string.Format("{0}trends/daily.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetWeekTrends = string.Format("{0}trends/weekly.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetTrendStatuses = string.Format("{0}trends/statuses.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string FollowTrend = string.Format("{0}trends/follow.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string UnfollowTrend = string.Format("{0}trends/destroy.xml", WeiboUriPrefix);

        #endregion

        #region Short Uri(Topics)

        /// <remarks/>
        public static readonly string ConvertToShortUrls = string.Format("{0}short_url/shorten.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string ConvertToLongUrls = string.Format("{0}short_url/expand.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetShortUrlSharedCount = string.Format("{0}short_url/share/counts.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetShortUrlSharedStatuses = string.Format("{0}short_url/share/statuses.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetShortUrlCommentCount = string.Format("{0}short_url/comment/counts.xml", WeiboUriPrefix);

        /// <remarks/>
        public static readonly string GetShortUrlComments = string.Format("{0}short_url/comment/comments.xml", WeiboUriPrefix);

        #endregion
    }
}
