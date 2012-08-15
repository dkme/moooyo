using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Creater
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class Creater
    {
        /// <summary>
        /// MemberID 
        /// </summary>
        public String MemberID
        {
            get { return this.memberID; }
            set { this.memberID = value; }
        }
        private String memberID;
        /// <summary>
        /// 昵称 
        /// </summary>
        public String NickName
        {
            get { return this.nickname; }
            set { this.nickname = value; }
        }
        private String nickname;
        /// <summary>
        /// 头像地址
        /// </summary>
        public String ICONPath
        {
            get { return this.iconpath; }
            set { this.iconpath = value; }
        }
        private String iconpath;
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
        /// 年龄
        /// </summary>
        public String Age
        {
            get { return this.age; }
            set { this.age = value; }
        }
        private String age;
        /// <summary>
        /// 身高
        /// </summary>
        public String Height
        {
            get { return this.height; }
            set { this.height = value; }
        }
        private String height;
        /// <summary>
        /// 我想
        /// </summary>
        public String IWant
        {
            get { return this.iwant; }
            set { this.iwant = value; }
        }
        private String iwant;
        /// <summary>
        /// 所在地
        /// </summary>
        public String City
        {
            get { return this.city; }
            set { this.city = value; }
        }
        private String city;
        /// <summary>
        /// 职业
        /// </summary>
        public String Career
        {
            get { return this.career; }
            set { this.career = value; }
        }
        private String career;
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
        /// 会员类别
        /// </summary>
        public Member.MemberType MemberType
        {
            get { return this.memberType; }
            set { this.memberType = value; }
        }
        private Member.MemberType memberType;
        /// <summary>
        /// 用户照片
        /// </summary>
        public Member.MemberPhoto MemberPhoto
        {
            get { return this.memberPhoto; }
            set { this.memberPhoto = value; }
        }
        private Member.MemberPhoto memberPhoto;
        ///// <summary>
        ///// 最后操作时间
        ///// </summary>
        //public DateTime LastOperationTime
        //{
        //    get { return this.lastOperationTime; }
        //    set { this.lastOperationTime = value.ToLocalTime(); }
        //}
        //private DateTime lastOperationTime;
        ///// <summary>
        ///// 在线状态
        ///// </summary>
        //public String OnlineStr
        //{
        //    get { return GetOnlineStr(LastOperationTime); }
        //}
        //private String GetOnlineStr(DateTime lastOperationTime)
        //{
        //    String str = "";
        //    if (lastOperationTime != null)
        //    {
        //        TimeSpan sp = DateTime.Now - lastOperationTime;

        //        int rightnow = 300;
        //        int onehour = 3600;
        //        int oneweek = 604800;
        //        double spsec = sp.TotalSeconds;

        //        if (spsec < rightnow)
        //        {
        //            str = "在线";
        //            return str;
        //        }
        //        if (spsec > rightnow & spsec < onehour)
        //        {
        //            str = "刚刚在线";
        //            return str;
        //        }
        //        if (spsec > onehour & lastOperationTime.Date == DateTime.Now.Date)
        //        {
        //            str = "今天来过";
        //            return str;
        //        }
        //        else
        //        {
        //            if (spsec < oneweek)
        //            {
        //                str = "最近来过";
        //                return str;
        //            }
        //        }
        //    }
        //    return str;
        //}
        public Creater(String id)
        {
            try
            {
                Member.Member obj = new Member.Member();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>(Member.Member.GetCollectionName());
                obj = mc.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
                this.MemberID = obj.ID;
                this.ICONPath = obj.MemberInfomation.IconPath;
                this.NickName = obj.MemberInfomation.NickName;
                this.Sex = obj.Sex;
                this.Height = obj.MemberInfomation.Height;
                this.IWant = obj.MemberInfomation.IWant;
                this.Age = obj.MemberInfomation.Age;
                this.City = obj.MemberInfomation.City;
                this.Career = obj.MemberInfomation.Career;
                if (obj.UniqueNumber != null)
                {
                    this.UniqueNumber = obj.UniqueNumber;
                }
                this.MemberType = obj.MemberType;
                this.MemberPhoto = obj.MemberPhoto;
                //this.LastOperationTime = obj.LastOperationTime;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
    }
}
