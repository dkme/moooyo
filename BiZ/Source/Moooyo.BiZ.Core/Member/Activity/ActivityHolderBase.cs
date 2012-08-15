using System;
using System.Collections.Generic;

namespace Moooyo.BiZ.Core.Member.Activity
{
    /// <summary>
    /// 动态基类
    /// </summary>
    public class ActivityHolderBase
    {
        /// <summary>
        /// ID 
        /// </summary>
        public String ID
        {
			get;set;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public String MemberID
        {
            get { return this.memberid; }
            set { this.memberid = value; }
        }
        private String memberid;
        /// <summary>
        /// 创建时间
        /// </summary>
        public String Date
        {
            get { return this.date; }
            set { this.date = value; }
        }
        private String date;
        /// <summary>
        /// 用户当日动态列表
        /// </summary>
        public IList<Activity> Activitys
        {
            get
            {
                return this.activitys;
            }
            set { this.activitys = value; }
        }
        private IList<Activity> activitys;
        /// <summary>
        /// 最后操作时间
        /// </summary>
        public DateTime LastOperationTime
        {
            get { return this.lastOperationTime; }
            set { this.lastOperationTime = value; }
        }
        private DateTime lastOperationTime;
    }

}

