using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;

namespace Moooyo.BiZ.Member
{
    /// <summary>
    /// 用户类
    /// </summary>
    //使MongoDB查询表时，可以忽略额外的元素 
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class Member
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
        [System.Web.Script.Serialization.ScriptIgnore]

        /// <summary>
        /// 增强的相关唯一编号
        /// </summary>
        public Comm.UniqueNumber.UniqueNumber UniqueNumber
        {
            get { return this.uniqueNumber; }
            set { this.uniqueNumber = value; }
        }
        private Comm.UniqueNumber.UniqueNumber uniqueNumber;
        /// <summary>
        /// 审核结果
        /// </summary>
        public int Au
        {
            get { return this.au; }
            set { this.au = value; }
        }
        private int au;
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
        /// Email是否已验证
        /// </summary>
        public Boolean EmailIsVaild
        {
            get { return this.emailIsVaild; }
            set { this.emailIsVaild = value; }
        }
        private Boolean emailIsVaild;
        /// <summary>
        /// 密码
        /// </summary>
        private String password;
        public String Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
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
        /// 用户建立时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return this.createdTime; }
            set { this.createdTime = value; }
        }
        private DateTime createdTime;
        /// <summary>
        /// 是否允许登陆
        /// </summary>
        public Boolean AllowLogin
        {
            get { return this.allowLogin; }
            set { this.allowLogin = value; }
        }
        private Boolean allowLogin;
        /// <summary>
        /// 在线状态
        /// </summary>
        public String OnlineStr
        {
            get { return GetOnlineStr(LastOperationTime); }
        }
        /// <summary>
        /// 会员类别
        /// </summary>
        public MemberType MemberType
        {
            get { return this.memberType; }
            set { this.memberType = value; }
        }
        private MemberType memberType;
        /// <summary>
        /// 手机号码
        /// </summary>
        public String MobileNo
        {
            get { return this.mobileNo; }
            set { this.mobileNo = value; }
        }
        private String mobileNo;
        /// <summary>
        /// 是否绑定手机号码
        /// </summary>
        public Boolean IsBindingMobileNo
        {
            get { return this.isBindingMobileNo; }
            set { this.isBindingMobileNo = value; }
        }
        private Boolean isBindingMobileNo;
        /// <summary>
        /// 最后操作时间
        /// </summary>
        public DateTime LastOperationTime
        {
            get { return this.lastOperationTime; }
            set { this.lastOperationTime = value.ToLocalTime(); }
        }
        private DateTime lastOperationTime;
        /// <summary>
        /// 最后登录IP
        /// </summary>
        public String LastLoginIP
        {
            get { return this.lastLoginIP; }
            set { this.lastLoginIP = value; }
        }
        private String lastLoginIP;
        /// <summary>
        /// 用户资料信息
        /// </summary>
        public MemberInfomation MemberInfomation
        {
            get {
                if (this.infomation!=null)
                    this.infomation.Sex = Sex; 
                return this.infomation;
            }
            set { this.infomation = value; }
        }
        private MemberInfomation infomation;
        /// <summary>
        /// 用户照片
        /// </summary>
        public MemberPhoto MemberPhoto
        {
            get
            {
                return this.memberPhoto;
            }
            set { this.memberPhoto = value; }
        }
       
        private MemberPhoto memberPhoto;
        /// <summary>
        /// 用户状态计数信息
        /// </summary>
        public MemberStatus Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        private MemberStatus status;
        /// <summary>
        /// 用户设置
        /// </summary>
        public MemberSetting Settings
        {
            get { return this.settings; }
            set { this.settings = value; }
        }
        private MemberSetting settings;
        /// <summary>
        /// 用户关系信息
        /// </summary>
        public MemberRelations MemberRelations
        {
            get { return this.relations; }
            set { this.relations = value; }
        }
        private MemberRelations relations;
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
        /// 用户邀请的人的数据对象集合
        /// </summary>
        public IList<Member> ToRegInviterMember
        {
            get { return toRegInviterMember; }
            set { toRegInviterMember = value; }
        }
        private IList<Member> toRegInviterMember;


        private String GetOnlineStr(DateTime lastOperationTime)
        {
            String str = "";
            TimeSpan sp = DateTime.Now - lastOperationTime;

            int rightnow = 300;
            int onehour = 3600;
            int oneweek = 604800;
            double spsec = sp.TotalSeconds;

            if (spsec < rightnow)
            {
                str = "在线";
                return str;
            }
            if (spsec > rightnow & spsec < onehour)
            {
                str = "刚刚在线";
                return str;
            }
            if (spsec > onehour & lastOperationTime.Date == DateTime.Now.Date)
            {
                str = "今天来过";
                return str;
            }
            else
                if (spsec < oneweek)
                {
                    str = "最近来过";
                    return str;
                }

            return "";
        }

        public static String GetCollectionName()
        {
            return "Members";
        }

        //#region 技能
        ///// <summary>
        ///// 我想学的技能列表
        ///// </summary>
        //public IList<Skills.Skill> SkillsWhatIWantToKnow
        //{
        //    get { return this.skillsWhatIWantToKnow; }
        //    set { this.skillsWhatIWantToKnow = value; }
        //}
        //private IList<Skills.Skill> skillsWhatIWantToKnow;
        ///// <summary>
        ///// 我懂的技能列表
        ///// </summary>
        //public IList<Skills.Skill> SkillsWhatIKnowsWell
        //{
        //    get { return this.skillsWhatIKnowsWell; }
        //    set { this.skillsWhatIKnowsWell = value; }
        //}
        //private IList<Skills.Skill> skillsWhatIKnowsWell;
        //#endregion 
        
        #region 构造函数
        public Member() { }
        #endregion
    }
}
