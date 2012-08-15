using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Sys
{
    /// <summary>
    /// 系统定义的小编问答类
    /// </summary>
    public class SystemInterView
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
        /// 访谈类别
        /// </summary>
        public InterViewType type
        {
            get { return this.t; }
            set { this.t = value; }
        }
        private InterViewType t;
        /// <summary>
        /// 问题
        /// </summary>
        public String Question
        {
            get { return this.question; }
            set { this.question = value; }
        }
        private String question;
        /// <summary>
        /// 范例答案
        /// </summary>
        public String Answer
        {
            get { return this.answer; }
            set { this.answer = value; }
        }
        private String answer;
        /// <summary>
        /// 是否已审核
        /// </summary>
        public bool IsAudited
        {
            get { return this.isaudited; }
            set { this.isaudited = value; }
        }
        private bool isaudited;
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
        /// 使用次数
        /// </summary>
        public long UseCount
        {
            get { return this.usecount; }
            set { this.usecount = value; }
        }
        private long usecount;
        /// <summary>
        /// 随机数（用于取随机记录）
        /// </summary>
        public double Random
        {
            get { return this.random; }
            set { this.random = value; }
        }
        private double random;
        //贡献者ID
        public String Writter;
    }
    public enum InterViewType
    {
        m_normal=11,
        f_normal=21
    }
}
