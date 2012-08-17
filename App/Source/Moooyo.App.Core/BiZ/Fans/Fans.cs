using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.App.Core.BiZ.Fans
{
    public class Fans
    {
        /// <summary>
        /// ID 
        /// </summary>
        public String ID
        {
			get;set;
        }

        //对象ID
        public String ObjectID;
        /// <summary>
        /// 创建者
        /// </summary>
        public Creater.Creater Creater
        {
            get { return this.creater; }
            set { this.creater = value; }
        }
        private Creater.Creater creater;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return this.createdTime; }
            set { this.createdTime = value; }
        }
        private DateTime createdTime;

    }
}
