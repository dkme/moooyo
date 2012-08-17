using System;

namespace Moooyo.App.Core.BiZ.Creater
{
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
        public Creater(String id)
        {
        }
    }

}

