using System;

namespace Moooyo.App.Core.BiZ.Member.Activity
{
    /// <summary>
    /// 和我相关的动态
    /// </summary>
    public class ActivityHolderRelatedToMe : ActivityHolderBase
    {
        /// <summary>
        /// 相关用户IDID
        /// </summary>
        public String FromMemberID
        {
            get { return this.fromMemberid; }
            set { this.fromMemberid = value; }
        }
        private String fromMemberid;

        /// <summary>
        /// 相关用户资料信息
        /// </summary>
        public MemberInfomation FromMemberInfomation
        {
            get
            {
                return this.infomation;
            }
            set { this.infomation = value; }
        }
        private MemberInfomation infomation;

        public static String GetCollectionName() { return "ActivityRelatedToMe"; }
    }
}

