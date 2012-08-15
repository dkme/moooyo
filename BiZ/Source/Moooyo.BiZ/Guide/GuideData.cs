using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Guide
{
    public class GuideData
    {
        /// <summary>
        /// ID 
        /// </summary>
        public String ID
        {
            get { if (_id != null) return _id.ToString(); else return ""; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;

        //对象ID
        public String GuideID;
        //用户ID
        public String MemberID;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return this.createdTime; }
            set { this.createdTime = value; }
        }
        private DateTime createdTime;

        public GuideData(String GuideID, String MemberID)
        {
            this.GuideID = GuideID;
            this.MemberID = MemberID;
            this.CreatedTime = DateTime.Now;
        }
    }
}
