using System;

namespace Moooyo.BiZ.Core.Member
{
    public class MemberPhoto
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
        /// <summary>
        /// 头像ID
        /// </summary>
        public int IconNo
        {
            get { return this.iconNo; }
            set { this.iconNo = value; }
        }
        private int iconNo;
        /// <summary>
        /// 头像ID
        /// </summary>
        public String IconID
        {
            get {
                if (iconid == null) return "";
                return this.iconid; 
            }
            set { this.iconid = value; }
        }
        private String iconid;
        /// <summary>
        /// 是否通过了视频认证
        /// </summary>
        public Boolean IsRealPhotoIdentification
        {
            get { return this.isRealPhotoIdentification; }
            set { this.isRealPhotoIdentification = value; }
        }
        private Boolean isRealPhotoIdentification;

        public MemberPhoto() { }
       
    }

}

