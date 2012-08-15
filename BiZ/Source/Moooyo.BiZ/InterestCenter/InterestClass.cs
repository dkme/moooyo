using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.InterestCenter
{
    /// <summary>
    /// 兴趣分类
    /// </summary>
    public class InterestClass
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
        /// 图片地址
        /// </summary>
        public String ICONPath
        {
            get { return this.iconpath; }
            set { this.iconpath = value; }
        }
        private String iconpath;
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
        /// 兴趣数量
        /// </summary>
        public int InterestCount
        {
            get { return this.interestCount; }
            set { this.interestCount = value; }
        }
        private int interestCount;
        /// <summary>
        /// 包含的兴趣列表
        /// </summary>
        public IList<Interest> Interesters
        {
            get { return this.interesters; }
            set { this.interesters = value; }
        }
        private IList<Interest> interesters;
        /// <summary>
        /// 排序
        /// </summary>
        public int Order
        {
            get { return this.order; }
            set { this.order = value; }
        }
        private int order;

        public static String GetCollectionName()
        {
            return "InterestClass";
        }
    }
}
