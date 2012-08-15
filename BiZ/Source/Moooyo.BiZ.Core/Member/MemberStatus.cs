using System;
using System.Collections.Generic;

namespace Moooyo.BiZ.Core.Member
{
    /// <summary>
    /// 用户状态信息
    /// </summary>
    public class MemberStatus
    {
        #region 用户状态信息
        /// <summary>
        /// 是否在线
        /// </summary>
        public Boolean IsOnline
        {
            get { return this.isOnline; }
            set { this.isOnline = value; }
        }
        private Boolean isOnline;
        /// <summary>
        /// 使用的设备类别
        /// </summary>
        public Moooyo.BiZ.Core.Comm.Device.DeviceType OnlineDeciveType
        {
            get { return this.onlineDeciveType; }
            set { this.onlineDeciveType = value; }
        }
        private Moooyo.BiZ.Core.Comm.Device.DeviceType onlineDeciveType;
        /// <summary>
        /// 登录次数
        /// </summary>
        public Int32 LoginTimes
        {
            get { return this.loginTimes; }
            set { this.loginTimes = value; }
        }
        private Int32 loginTimes;
        /// <summary>
        /// 积分
        /// </summary>
        public int Points
        {
            get { return this.points; }
            set { this.points = value; }
        }
        private int points;
        /// <summary>
        /// 增加积分的进度值
        /// </summary>
        public int PointsSchedule
        {
            get { return pointsSchedule; }
            set { pointsSchedule = value; }
        }
        private int pointsSchedule;
        /// <summary>
        /// 魅力指数
        /// </summary>
        public int GlamourCount
        {
            get { return this.glamourCount; }
            set { this.glamourCount = value; }
        }
        private int glamourCount;
        /// <summary>
        /// 热度 － 24H内聊天人数
        /// </summary>
        public int Last24HInCallsCount
        {
            get { return this.last24HInCallsCount; }
            set { this.last24HInCallsCount = value; }
        }
        private int last24HInCallsCount;
        /// <summary>
        /// 渴望度 － 24H内发起聊天人数
        /// </summary>
        public int Last24HOutCallsCount
        {
            get { return this.last24HoutCallsCount; }
            set { this.last24HoutCallsCount = value; }
        }
        private int last24HoutCallsCount;
        /// <summary>
        /// 照片数量
        /// </summary>
        public int PhotoCount
        {
            get { return this.photocount; }
            set { this.photocount = value; }
        }
        private int photocount;
        /// <summary>
        /// 收藏人数
        /// </summary>
        public Int32 FavorMemberCount
        {
            get { return this.favorMemberCount; }
            set { this.favorMemberCount = value; }
        }
        private Int32 favorMemberCount;
        /// <summary>
        /// 收藏我的人数
        /// </summary>
        public Int32 MemberFavoredMeCount
        {
            get { return this.memberFavoredMeCount; }
            set { this.memberFavoredMeCount = value; }
        }
        private Int32 memberFavoredMeCount;
        /// <summary>
        /// 被浏览次数
        /// </summary>
        public Int32 BeenViewedTimes
        {
            get { return this.beenViewedTimes; }
            set { this.beenViewedTimes = value; }
        }
        private Int32 beenViewedTimes;
        /// <summary>
        /// 账户余额
        /// </summary>
        public Int32 AccountBalance
        {
            get { return this.accountBalance; }
            set { this.accountBalance = value; }
        }
        private Int32 accountBalance;
        /// <summary>
        /// 未读浏览量
        /// </summary>
        public Int32 UnReadBeenViewedTimes
        {
            get { return this.unReadBeenViewedTimes; }
            set { this.unReadBeenViewedTimes = value; }
        }
        private Int32 unReadBeenViewedTimes;
        /// <summary>
        /// 未读被收藏数量
        /// </summary>
        public Int32 UnReadBeenFavorCount
        {
            get { return this.unReadBeenFavorCount; }
            set { this.unReadBeenFavorCount = value; }
        }
        private Int32 unReadBeenFavorCount;
        /// <summary>
        /// 未读消息数量
        /// </summary>
        public Int32 UnReadMsgCount
        {
            get { return this.unReadMsgCount; }
            set { this.unReadMsgCount = value; }
        }
        private Int32 unReadMsgCount;
        /// <summary>
        /// 未读系统消息数量
        /// </summary>
        public Int32 UnReadSystemMsgCount
        {
            get { return this.unReadSystemMsgCount; }
            set { this.unReadSystemMsgCount = value; }
        }
        private Int32 unReadSystemMsgCount;
        /// <summary>
        /// 未读关于我的动态数量
        /// </summary>
        public Int32 UnReadActivitysAboutMeCount
        {
            get { return this.unReadActivitysAboutMeCount; }
            set { this.unReadActivitysAboutMeCount = value; }
        }
        private Int32 unReadActivitysAboutMeCount;
        /// <summary>
        /// 总发出消息数量
        /// </summary>
        public Int32 TotalMsgCount
        {
            get { return this.totalmsgcount; }
            set { this.totalmsgcount = value; }
        }
        private Int32 totalmsgcount;
        /// <summary>
        /// 最后呼出日期
        /// </summary>
        public DateTime LastCallOutDate
        {
            get { return this.lastCallOutDate; }
            set { this.lastCallOutDate = value; }
        }
        private DateTime lastCallOutDate;
        /// <summary>
        /// 最后呼入日期
        /// </summary>
        public DateTime LastInCallDate
        {
            get { return this.lastInCallDate; }
            set { this.lastInCallDate = value; }
        }
        private DateTime lastInCallDate;
        /// <summary>
        /// 访谈总数
        /// </summary>
        public Int32 InterViewCount
        {
            get { return this.interViewCount; }
            set { this.interViewCount = value; }
        }
        private Int32 interViewCount;
        /// <summary>
        /// 兴趣总数
        /// </summary>
        public Int32 InterestCount
        {
            get { return this.interestCount; }
            set { this.interestCount = value; }
        }
        private Int32 interestCount;
        /// <summary>
        /// 用户徽章列表
        /// </summary>
        private IList<MemberBadge> memberBadge;
        public IList<MemberBadge> MemberBadge
        {
            get { return memberBadge; }
            set { memberBadge = value; }
        }
        #endregion
    }
}

