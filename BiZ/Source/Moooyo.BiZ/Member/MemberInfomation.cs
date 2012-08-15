using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Moooyo.BiZ.Member
{
    /// <summary>
    /// 用户资料信息
    /// </summary>
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
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
        //public Boolean HideMyAge
        //{
        //    get { return this.hideMyAge; }
        //    set { this.hideMyAge = value; }
        //}
        //private Boolean hideMyAge;
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
        ///// <summary>
        ///// 故乡
        ///// </summary>
        //public String Hometown
        //{
        //    get { if (this.hometown == null) return ""; else return this.hometown; }
        //    set { this.hometown = value; }
        //}
        //private String hometown;
        /// <summary>
        /// 身高
        /// </summary>
        public String Height
        {
            get { if (this.height == null) return ""; else return this.height; }
            set { this.height = value; }
        }
        private String height;
        ///// <summary>
        ///// 体型
        ///// </summary>
        //public String Figure
        //{
        //    get { if (this.figure == null) return ""; else return this.figure; }
        //    set { this.figure = value; }
        //}
        //private String figure;
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
        ///// <summary>
        ///// 收入
        ///// </summary>
        //public String Gainings
        //{
        //    get { if (this.gainings == null) return ""; else return this.gainings; }
        //    set { this.gainings = value; }
        //}
        //private String gainings;
        ///// <summary>
        ///// 居住状态
        ///// </summary>
        //public String LivingStatus
        //{
        //    get { if (this.livingStatus == null) return ""; else return this.livingStatus; }
        //    set { this.livingStatus = value; }
        //}
        //private String livingStatus;
        ///// <summary>
        ///// 有房
        ///// </summary>
        //public String HaveHouse
        //{
        //    get { if (this.haveHouse == null) return ""; else return this.haveHouse; }
        //    set { this.haveHouse = value; }
        //}
        //private String haveHouse;
        ///// <summary>
        ///// 有车
        ///// </summary>
        //public String HaveCar
        //{
        //    get { if (this.haveCar == null) return ""; else return this.haveCar; }
        //    set { this.haveCar = value; }
        //}
        //private String haveCar;
        ///// <summary>
        ///// 车牌子
        ///// </summary>
        //public String CarBand
        //{
        //    get { if (this.carBand == null) return ""; else return this.carBand; }
        //    set { this.carBand = value; }
        //}
        //private String carBand;
        ///// <summary>
        ///// 交友目的
        ///// </summary>
        //public String Goal
        //{
        //    get { if (this.goal == null) return ""; else return this.goal; }
        //    set { this.goal = value; }
        //}
        //private String goal;
        ///// <summary>
        ///// 内心独白
        ///// </summary>
        //public String Soliloquize
        //{
        //    get { if (this.soliloquize == null) return ""; else return this.soliloquize; }
        //    set { this.soliloquize = value; }
        //}
        //private String soliloquize;
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
        /// 用户皮肤
        /// </summary>
        public MemberSkin.MemberSkin MemberSkin
        {
            get { return this.memberSkin; }
            set { this.memberSkin = value; }
        }
        private MemberSkin.MemberSkin memberSkin;
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
        /// <summary>
        /// 访谈
        /// </summary>
        public IList<InterView.InterView> InterViews
        {
            get { return this.interViews; }
            set { this.interViews = value; }
        }
        private IList<InterView.InterView> interViews;
        #endregion

        public MemberInfomation() { }
    }
}
