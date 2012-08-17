using System;

namespace Moooyo.App.Core.BiZ.Fans
{
    public class FansGroupName
    {
        /// <summary>
        /// 默认名称
        /// </summary>
        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        private String name;
        /// <summary>
        /// 第一名
        /// </summary>
        public String FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }
        private String firstName;
        /// <summary>
        /// 第二名
        /// </summary>
        public String Second
        {
            get { return this.second; }
            set { this.second = value; }
        }
        private String second;
        /// <summary>
        /// 第三名
        /// </summary>
        public String TheThird
        {
            get { return this.theThird; }
            set { this.theThird = value; }
        }
        private String theThird;
    }

}

