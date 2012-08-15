using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Member.Activity
{
    /// <summary>
    /// 用户的动态
    /// </summary>
    public class ActivityHolder : ActivityHolderBase
    {
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
        /// 会员类别
        /// </summary>
        public MemberType MemberType
        {
            get { return this.memberType; }
            set { this.memberType = value; }
        }
        private MemberType memberType;
        /// <summary>
        /// 用户资料信息
        /// </summary>
        public MemberInfomation MemberInfomation
        {
            get
            {
                return this.infomation;
            }
            set { this.infomation = value; }
        }
        private MemberInfomation infomation;
        /// <summary>
        /// 是否认证头像
        /// </summary>
        public Boolean IsRealPhotoIdentification
        {
            get { return isRealPhotoIdentification; }
            set { isRealPhotoIdentification = value; }
        }
        private Boolean isRealPhotoIdentification;
        public static String GetCollectionName() { return "Activity"; }
    }
}
