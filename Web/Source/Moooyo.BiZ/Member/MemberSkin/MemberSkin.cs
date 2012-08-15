/*******************************************************************
 * Functional description ：用户皮肤数据实体
 * Author：Lau Tao
 * Modify the expansion：Lau Tao
 * Modified date：2012/5/31 Thursday 
 * Remarks：
 * ****************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Member.MemberSkin
{
    /// <summary>
    /// 用户皮肤
    /// </summary>
    public class MemberSkin
    {
        /// <summary>
        /// ID 
        /// </summary>
        public String ID
        {
            get { 
                if (_id != null) return _id.ToString(); 
                else return ""; 
            }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;
        /// <summary>
        /// 创建者
        /// </summary>
        public String Creater
        {
            get { return this.creater; }
            set { this.creater = value; }
        }
        private String creater;
        /// <summary>
        /// 创建者类别
        /// </summary>
        public Comm.UserType UserType
        {
            get { return this.userType; }
            set { this.userType = value; }
        }
        private Comm.UserType userType;
        /// <summary>
        /// 个性图片
        /// </summary>
        public String PersonalityPicture
        {
            get { return this.personalityPicture; }
            set { this.personalityPicture = value; }
        }
        private String personalityPicture;
        /// <summary>
        /// 个性背景图片
        /// </summary>
        public String PersonalityBackgroundPicture
        {
            get { return this.personalityBackgroundPicture; }
            set { this.personalityBackgroundPicture = value; }
        }
        private String personalityBackgroundPicture;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return this.createTime; }
            set { this.createTime = value; }
        }
        private DateTime createTime;
        /// <summary>
        /// 随机数
        /// </summary>
        public double Random
        {
            get { return this.random; }
            set { this.random = value; }
        }
        private double random;

        public static String GetCollectionName()
        {
            return "MemberSkins";
        }
    }
}
