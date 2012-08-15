using System;

namespace Moooyo.BiZ.Core.Member
{
    /// <summary>
    /// 用户设置
    /// </summary>
    public class MemberSetting
    {
        /// <summary>
        /// 隐藏我的具体位置
        /// </summary>
        public Boolean HiddenMyLoc
        {
            get { return this.hiddenmyloc; }
            set { this.hiddenmyloc = value; }
        }
        private Boolean hiddenmyloc;
        /// <summary>
        /// 隐藏照片
        /// </summary>
        public Boolean HiddenMyPhoto
        {
            get { return this.hiddenMyPhoto; }
            set { this.hiddenMyPhoto = value; }
        }
        private Boolean hiddenMyPhoto;
        /// <summary>
        /// 停用帐号
        /// </summary>
        public Boolean StopMyAccount
        {
            get { return this.stopMyAccount; }
            set { this.stopMyAccount = value; }
        }
        private Boolean stopMyAccount;
        /// <summary>
        /// 收藏设定 － 自动将主动联系的用户加入收藏
        /// </summary>
        public Boolean AutoAddOutCallingToMyFavorList
        {
            get { return this.autoAddOutCallingToMyFavorList; }
            set { this.autoAddOutCallingToMyFavorList = value; }
        }
        private Boolean autoAddOutCallingToMyFavorList;
        /// <summary>
        /// 私信和打招呼过滤 － 高级会员才能给我私信和打招呼（高级、VIP会员特权 ）
        /// </summary>
        public Boolean OnlySeniorMemberCanTalkSaiHiMe
        {
            get { return this.onlySeniorMemberCanTalkSaiHiMe; }
            set { this.onlySeniorMemberCanTalkSaiHiMe = value; }
        }
        private Boolean onlySeniorMemberCanTalkSaiHiMe;
        /// <summary>
        /// 私信和打招呼过滤 － VIP会员才能给我私信和打招呼 （VIP会员特权）
        /// </summary>
        public Boolean OnlyVIPMemberCanTalkSaiHiMe
        {
            get { return this.onlyVIPMemberCanTalkSaiHiMe; }
            set { this.onlyVIPMemberCanTalkSaiHiMe = value; }
        }
        private Boolean onlyVIPMemberCanTalkSaiHiMe;
        /// <summary>
        /// 粉丝团名称
        /// </summary>
        public Fans.FansGroupName FansGroupName
        {
            get { return this.fansGroupName; }
            set { this.fansGroupName = value; }
        }
        private Fans.FansGroupName fansGroupName;
    }
}

