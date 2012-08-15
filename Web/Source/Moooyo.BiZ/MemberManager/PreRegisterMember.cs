using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Moooyo.BiZ.MemberManager
{
    /// <summary>
    /// 预注册用户
    /// </summary>
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class PreRegisterMember
    {
        /// <summary>
        /// 预注册用户ID
        /// </summary>
        public String ID
        {
            get { if (_id != null) return _id.ToString(); else return ""; }
        }
        public MongoDB.Bson.ObjectId _id;
        /// <summary>
        /// Email
        /// </summary>
        public String Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
        private String email;
        /// <summary>
        /// Email验证密码
        /// </summary>
        public String EmailPwd
        {
            get { return this.emailPwd; }
            set { this.emailPwd = value; }
        }
        private String emailPwd;
        /// <summary>
        /// Email是否已验证
        /// </summary>
        public Boolean EmailIsVaild
        {
            get { return this.emailIsVaild; }
            set { this.emailIsVaild = value; }
        }
        private Boolean emailIsVaild;
        /// <summary>
        /// 审核结果
        /// </summary>
        public int AuditResult
        {
            get { return this.auditResult; }
            set { this.auditResult = value; }
        }
        private int auditResult;
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex
        {
            get { return this.sex; }
            set { this.sex = value; }
        }
        private int sex;
        /// <summary>
        /// 照片ID
        /// </summary>
        public String PhotoID
        {
            get { return this.photoID; }
            set { this.photoID = value; }
        }
        private String photoID;
        ///// <summary>
        ///// 内部人员评价
        ///// </summary>
        //public String InnerScores
        //{
        //    get { return this.innerScores; }
        //    set { this.innerScores = value; }
        //}
        //private String innerScores;
        ///// <summary>
        ///// 用户评价
        ///// </summary>
        //public String Scores
        //{
        //    get { return this.scores; }
        //    set { this.scores = value; }
        //}
        //private String scores;
        ///// <summary>
        ///// 内部人员标签
        ///// </summary>
        //public String InnerMarks
        //{
        //    get { return this.innerMarks; }
        //    set { this.innerMarks = value; }
        //}
        //private String innerMarks;
        ///// <summary>
        ///// 用户标签
        ///// </summary>
        //public String Marks
        //{
        //    get { return this.marks; }
        //    set { this.marks = value; }
        //}
        //private String marks;
        /// <summary>
        /// 最终生成用户ID
        /// </summary>
        public String MemberID
        {
            get { return this.memberID; }
            set { this.memberID = value; }
        }
        private String memberID;
        /// <summary>
        /// 是否完成了注册
        /// </summary>
        public bool FinishedReg
        {
            get { return this.finishedReg; }
            set { this.finishedReg = value; }
        }
        private bool finishedReg;
        /// <summary>
        /// 用户建立时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return this.createdTime; }
            set { this.createdTime = value; }
        }
        private DateTime createdTime;

        public PreRegisterMember() { }
    }
}
