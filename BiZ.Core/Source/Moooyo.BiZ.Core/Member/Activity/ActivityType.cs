using System;

namespace Moooyo.BiZ.Core.Member.Activity
{
	public enum ActivityType
    {
        /// <summary>
        /// 管理员操作
        /// </summary>
        SystemAdminDelAction = 11,
        SystemAdminAttention = 12,

        /// <summary>
        /// 关系操作
        /// </summary>
        View = 110,
        Favor = 111,
        RemoveFavor = 112,
        Like = 113,

        /// <summary>
        /// 权限操作
        /// </summary>
        Disable = 120,
        Silent = 121,
        DisabledTalk = 122,
        DisabledMsg = 123,

        /// <summary>
        /// 用户联系
        /// </summary>
        Talk = 130,
        SendGift = 131,
        AskGift = 132,
        AskToDate = 133,
        //SaiHi=134,
        Action = 135,
        //Mark = 136,
        //WantToLearnFromYou=137,
        //WantToTeachYou=138,
        //BeenMarked = 139,
        //BeenScored = 140,

        /// <summary>
        /// 用户资料操作
        /// </summary>
        UploadPhoto = 210,
        UploadVideo = 211,
        FinishProfile = 212,
        FinishInterview = 213,
        //FinishSoliloquize=214,
        //FinishSkill=224,
        FinishIWant = 223,
        SetICON = 215,
        UpdateLocation = 216,
        UpdatePhotoComment = 217,
        UpdateIWant = 218,
        //UpdateSoliloquize=219,
        //UpdateIKnow=220,
        //UpdateWantLearn=221,
        UpdateInterView = 222,
        Authentica = 223,

        /// <summary>
        /// 应用操作
        /// </summary>
        //AddRequest=301,
        AddInterest = 302,

        /// <summary>
        /// 关联用户对我的操作（隐私）
        /// </summary>
        //被加粉
        BeenFavored = 701,
        //被删粉
        BeenRemovedFavor = 702,
        //发表的信息被喜欢
        BeenLiked = 703,
        //照片被评论
        PhotoBeenCommented = 704,
        //加入我创建的兴趣的粉丝团
        JoinToMyInterestFansGroup = 705,
        //在我创建的兴趣下添加问问
        AddQuestToMyInterest = 706,
        //用户给我贡献魅力值
        PresentGlamourValue = 707,
        //添加问问答案
        AddAskAnswer = 708,
        //被访问
        BeenVisit = 709,
        //号召被喜欢
        CallForBeenLiked = 711,
        //号召被评论
        CallForBeenCommented = 712,
        //号召被喜欢并评论
        CallForBeenLikeAndCommented = 713,
        //图片被喜欢
        ImageBeenLiked = 714,
        //图片被评论
        ImageBeenCommented = 715,
        //图片被喜欢并评论
        ImageBeenLikeAndCommented = 716,
        //兴趣被喜欢
        InterestBeenLiked = 717,
        //兴趣被评论
        InterestBeenCommented = 718,
        //兴趣被喜欢并评论
        InterestBeenLikeAndCommented = 719,
        //我想被喜欢
        IWantBeenLiked = 720,
        //我想被评论
        IWantBeenCommented = 721,
        //我想被喜欢并评论
        IWantBeenLikeAndCommented = 722,
        //心情被喜欢
        MoodBeenLiked = 723,
        //心情被评论
        MoodBeenCommented = 724,
        //心情被喜欢并评论
        MoodBeenLikeAndCommented = 725,
        //坏心情被喜欢
        BadMoodBeenLiked = 734,
        //坏心情被评论
        BadMoodBeenCommented = 735,
        //坏心情被喜欢并评论
        BadMoodBeenLikeAndCommented = 736,

        //说说被喜欢
        TalkAboutBeenLiked = 726,
        //说说被评论
        TalkAboutBeenCommented = 727,
        //说说被喜欢并评论
        TalkAboutBeenLikeAndCommented = 728,
        //访谈被喜欢
        InterViewBeenLiked = 729,
        //访谈被评论
        InterViewBeenCommented = 730,
        //用户设置位置被喜欢
        MembeSetLocationBeenLiked = 731,
        //用户设置位置被评论
        MemberSetLocationBeenCommented = 732,
        //用户设置位置被喜欢并评论
        MemberSetLocationBeenLikeAndCommented = 733,
        //用户设置位置被喜欢
        MembeSetAvatarBeenLiked = 737,
        //用户设置位置被评论
        MemberSetAvatarBeenCommented = 738,
        //用户设置位置被喜欢并评论
        MemberSetAvatarBeenLikeAndCommented = 739,
        //评论被回复
        CommentBeenReplied = 740
    }
}

