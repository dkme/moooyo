using System;

namespace Moooyo.App.Core.BiZ.Member
{
    public enum BadegType
    {
         superman=1 
    }
    /*邱志明
     *2012-04-10
     * 用户徽章列表实体类
     */
    public class MemberBadge
    {
        private int badegType;

        /// <summary>
        /// 徽章类型
        /// </summary>
        public int BadegType
        {
            get { return badegType; }
            set { badegType = value; }
        }
        private int badegStatus;

        /// <summary>
        /// 徽章状态 0禁用 1可用 2待审
        /// </summary>
        public int BadegStatus
        {
            get { return badegStatus; }
            set { badegStatus = value; }
        }
        private DateTime jionTime;

        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime JionTime
        {
            get { return jionTime; }
            set { jionTime = value; }
        }
    }
}

