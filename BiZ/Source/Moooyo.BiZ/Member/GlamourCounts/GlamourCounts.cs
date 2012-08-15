using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Member.GlamourCounts
{
    /// <summary>
    /// 用户魅力值信息
    /// </summary>
    public class GlamourCounts
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public String ID
        {
            get { if (_id != null) return _id.ToString(); else return ""; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;
        /// <summary>
        /// 来自谁
        /// </summary>
        public String FromMember
        {
            get { return this.fromMember; }
            set { this.fromMember = value; }
        }
        private String fromMember;
        /// <summary>
        /// 给谁
        /// </summary>
        public String ToMember
        {
            get { return this.toMember; }
            set { this.toMember = value; }
        }
        private String toMember;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return this.createTime; }
            set { this.createTime = value; }
        }
        private DateTime createTime;
        /// <summary>
        /// 魅力值
        /// </summary>
        public float Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private float value;
        /// <summary>
        /// 操作类型
        /// </summary>
        public byte Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        private byte type;
        public static String CollectionName()
        {
            return "GlamourCounts";
        }
    }
}
