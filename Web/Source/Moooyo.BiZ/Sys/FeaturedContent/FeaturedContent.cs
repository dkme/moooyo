/*******************************************************************
 * Functional description ：精选内容数据实体
 * Author：Lau Tao
 * Modify the expansion：Tao Lau 
 * Modified date：2012/7/4 Wednesday 
 * Remarks：
 * ****************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Sys.FeaturedContent
{
    /// <summary>
    /// 精选内容数据实体
    /// </summary>
    public class FeaturedContent
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public String ID
        {
            get { if (_id != null)return _id.ToString(); else return ""; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;
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
        /// 图片
        /// </summary>
        public String Image
        {
            get { return this.image; }
            set { this.image = value; }
        }
        private String image;
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
        /// 创建者
        /// </summary>
        public Creater.Creater Creator
        {
            get { return this.creator; }
            set { this.creator = value; }
        }
        private Creater.Creater creator;
        /// <summary>
        /// 是否使用
        /// </summary>
        public Comm.UsedFlag UsedFlag
        {
            get { return this.usedFlag; }
            set { this.usedFlag = value; }
        }
        private Comm.UsedFlag usedFlag;
        /// <summary>
        /// 显示次数
        /// </summary>
        public int ShowedCount
        {
            get { return this.showedCount; }
            set { this.showedCount = value; }
        }
        private int showedCount;
        
        /// <summary>
        /// 表（集合）名
        /// </summary>
        /// <returns></returns>
        public static String GetCollectionName()
        {
            return "FeaturedContent";
        }

        public FeaturedContent(String image, String content, String creator, Comm.UsedFlag usedFlag)
        {
            this.CreatedTime = DateTime.Now;
            this.Image = image;
            this.Content = content;
            if (creator != null && creator != "" && creator != "4eb0fde42101b0824e2b018f") //不等于管理员
                this.Creator = new Creater.Creater(creator);
            else
                this.Creator = null;
            this.UsedFlag = usedFlag;
        }
    }
}
