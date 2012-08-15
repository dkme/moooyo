/**
 * Functional description ：用户动态数据实体
 * Author：Tao Lau
 * Modify the expansion：Tao Lau
 * Modified date：2012/7/13 Friday 
 * Remarks：
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Sys.MemberActivity
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class MemberActivity
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
        /// <summary>
        /// 从（源）用户
        /// </summary>
        public Creater.Creater FromMember
        {
            get { return this.frommember; }
            set { this.frommember = value; }
        }
        private Creater.Creater frommember;
        /// <summary>
        /// 向（目标）用户
        /// </summary>
        public Creater.Creater ToMember
        {
            get { return this.tomember; }
            set { this.tomember = value; }
        }
        private Creater.Creater tomember;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return this.createdTime; }
            set { this.createdTime = value; }
        }
        private DateTime createdTime;
        /// <summary>
        /// 动态类别
        /// </summary>
        public MemberActivityType ActivityType
        {
            get { return this.activityType; }
            set { this.activityType = value; }
        }
        private MemberActivityType activityType;
        /// <summary>
        /// 操作网址
        /// </summary>
        public String OperateUrl
        {
            get { return this.operateUrl; }
            set { this.operateUrl = value; }
        }
        private String operateUrl;

        public static String GetCollectionName()
        {
            return "MemberActivity";
        }
    }
}
