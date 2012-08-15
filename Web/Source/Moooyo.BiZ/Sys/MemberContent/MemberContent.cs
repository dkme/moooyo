using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;

namespace Moooyo.BiZ.Sys.MemberContent
{
    public class MemberContent
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
        /// 类别
        /// </summary>
        public MemberContentType type
        {
            get { return this.t; }
            set { this.t = value; }
        }
        private MemberContentType t;
        /// <summary>
        /// 被投诉人ID
        /// </summary>
        public String MemberID
        {
            get { return this.memberid; }
            set { this.memberid = value; }
        }
        private String memberid;
        /// <summary>
        /// 内容
        /// </summary>
        public String Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
        private String content;
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
        /// 范例答案
        /// </summary>
        public String Result
        {
            get { return this.result; }
            set { this.result = value; }
        }
        private String result;
        //贡献者ID
        public String Writter;
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
    public enum MemberContentType
    {
        jb_01 = 11,
        jb_02 = 12,
        jb_03 = 13,
        jb_04 = 14,
        jb_05 = 15,
        jb_06 = 16,
        jb_07 = 17,
        jb_08 = 18,
        jb_09 = 19,
        //给小编的建议
        jy_01 = 21,
        //bug提交，意见，建议
        jy_02 = 22,
        jy_03 = 23,
        jy_04 = 24,
        jy_05 = 25  
    }
}
