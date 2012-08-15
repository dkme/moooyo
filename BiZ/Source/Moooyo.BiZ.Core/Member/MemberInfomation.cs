using System;

namespace Moooyo.BiZ.Core.Member
{
    public class MemberInfomation
    {
        /// <summary>
        /// 用户
        /// </summary>
        public Member Owener
        {
            get { return this.owener; }
            set { this.owener = value; }
        }
        private Member owener;

        #region 用户资料信息
        /// <summary>
        /// 昵称
        /// </summary>
        public String NickName
        {
            get { if (this.nickName == null) return ""; else return this.nickName; }
            set { this.nickName = value; }
        }
        private String nickName;
        /// <summary>
        /// 真实姓名
        /// </summary>
        public String RealName
        {
            get { if (this.realName == null) return ""; else return this.realName; }
            set { this.realName = value; }
        }
        private String realName;
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
        /// 生日
        /// </summary>
        public DateTime Birthday
        {
            get { return this.birthday; }
            set { this.birthday = value; }
        }
        private DateTime birthday;
        /// <summary>
        /// 年龄
        /// </summary>
        public String Age
        {
            get {
                TimeSpan ts = DateTime.Now - Birthday;
                double year = Math.Floor(ts.TotalDays / 365);
                if (year < 100) return year.ToString("#0");
                else
                 return "?";
            }
        }
        /// <summary>
        /// 星座
        /// </summary>
        public String Star
        {
            get { if (this.star == null) return ""; else return this.star; }
            set { this.star = value; }
        }
        private String star;
        /// <summary>
        /// 所在省
        /// </summary>
        public String Province
        {
            get { if (this.city == null) return ""; else return this.city.Split('|')[0]; }
        }
        /// <summary>
        /// 所在地
        /// </summary>
        public String City
        {
            get { if (this.city == null) return ""; else return this.city; }
            set { this.city = value; }
        }
        private String city;
        /// <summary>
        /// 身高
        /// </summary>
        public String Height
        {
            get { if (this.height == null) return ""; else return this.height; }
            set { this.height = value; }
        }
        private String height;
        /// <summary>
        /// 学历
        /// </summary>
        public String EducationalBackground
        {
            get { if (this.educationalBackground == null) return ""; else return this.educationalBackground; }
            set { this.educationalBackground = value; }
        }
        private String educationalBackground;
        /// <summary>
        /// 职业
        /// </summary>
        public String Career
        {
            get { if (this.career == null) return ""; else return this.career; }
            set { this.career = value; }
        }
        private String career;
        /// <summary>
        /// 置业状况
        /// </summary>
        private String propertySituation;
        public String PropertySituation {
            get { return this.propertySituation; }
            set { this.propertySituation = value; }
        }
        /// <summary>
        /// 我想
        /// </summary>
        public String IWant
        {
            get { if (this.iWant == null) return ""; else return this.iWant; }
            set { this.iWant = value; }
        }
        private String iWant;
        /// <summary>
        /// 心情
        /// </summary>
        public String Mood
        {
            get { if (this.mood == null) return ""; else return this.mood; }
            set { this.mood = value; }
        }
        private String mood;
        /// <summary>
        /// 经度
        /// </summary>
        public Double Lat
        {
            get { return this.lat; }
            set { this.lat = value; }
        }
        private Double lat;
        /// <summary>
        /// 纬度
        /// </summary>
        public Double Lng
        {
            get { return this.lng; }
            set { this.lng = value; }
        }
        private Double lng;
        /// <summary>
        /// 地理矩阵
        /// </summary>
        public String Matrix
        {
            get { if (this.matrix == null) return ""; else return this.matrix; }
            set { this.matrix = value; }
        }
        private String matrix;
        /// <summary>
        /// 头像路径
        /// </summary>
        public String IconPath
        {
            get
            {
                if (iconpath == null) return "";
                return this.iconpath;
            }
            set { this.iconpath = value; }
        }
        private String iconpath;
        /// <summary>
        /// QQ
        /// </summary>
        public String QQ
        {
            get
            {
                if (qq == null) return "";
                return this.qq;
            }
            set { this.qq = value; }
        }
        private String qq;
        /// <summary>
        /// MSN
        /// </summary>
        public String MSN
        {
            get
            {
                if (msn == null) return "";
                return this.msn;
            }
            set { this.msn = value; }
        }
        private String msn;
        /// <summary>
        /// Tel
        /// </summary>
        public String Tel
        {
            get
            {
                if (tel == null) return "";
                return this.tel;
            }
            set { this.tel = value; }
        }
        private String tel;
        /// <summary>
        /// 个人介绍
        /// </summary>
        public String PersonalIntroduction
        {
            get { return this.personalIntroduction; }
            set { this.personalIntroduction = value; }
        }
        private String personalIntroduction;
        /// <summary>
        /// Other
        /// </summary>
        public String Other
        {
            get
            {
                if (qq == null) return "";
                return this.qq;
            }
            set { this.qq = value; }
        }
        private String other;
        #endregion

        public MemberInfomation() { }
    }

}

