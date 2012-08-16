using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Core.InterestCenter
{
    /// <summary>
    /// 兴趣
    /// </summary>
    public class Interest
    {
        /// <summary>
        /// ID 
        /// </summary>
        public String ID
        {
			get;set;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public String Title
        {
            get { return this.title; }
            set { this.title = value; }
        }
        private String title;
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
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return this.createdTime; }
            set { this.createdTime = value; }
        }
        private DateTime createdTime;
        /// <summary>
        /// 创建者
        /// </summary>
        public Creater Creater
        {
            get { return this.creater; }
            set { this.creater = value; }
        }
        private Creater creater;
        // 图片ID
        public String ICONID
        {
            get { return this.iconId; }
            set { this.iconId = value; }
        }
        private String iconId;
        /// <summary>
        /// 图片地址
        /// </summary>
        public String ICONPath
        {
            get { return this.iconPath; }
            set { this.iconPath = value; }
        }
        private String iconPath;
        /// <summary>
        /// 个性图片编号
        /// </summary>
        public String SelfhoodPictureId
        {
            get { return this.selfhoodPictureId; }
            set { this.selfhoodPictureId = value; }
        }
        private String selfhoodPictureId;
        /// <summary>
        /// 个性图片地址
        /// </summary>
        public String SelfhoodPicture
        {
            get { return this.selfhoodPicture; }
            set { this.selfhoodPicture = value; }
        }
        private String selfhoodPicture;
        //粉丝数
        public int FansCount
        {
            get { return this.fansCount; }
            set { this.fansCount = value; }
        }
        private int fansCount;
        /// <summary>
        /// 问问数
        /// </summary>
        public int QuestionCount
        {
            get { return this.questionCount; }
            set { this.questionCount = value; }
        }
        private int questionCount;
        /// <summary>
        /// 分类(逗号分隔的分类Title)
        /// </summary>
        public String Classes
        {
            get { return this.classes; }
            set { this.classes = value; }
        }
        private String classes;
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsAudited
        {
            get { return this.isAudited; }
            set { this.isAudited = value; }
        }
        private bool isAudited;
        /// <summary>
        /// 是否启用（用于关闭兴趣）
        /// </summary>
        public bool IsEnable
        {
            get { return this.isEnable; }
            set { this.isEnable = value; }
        }
        private bool isEnable;

        public static String GetCollectionName()
        {
            return "Interest";
        }
    }
}
