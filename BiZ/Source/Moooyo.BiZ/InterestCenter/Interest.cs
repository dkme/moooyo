using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.InterestCenter
{
    /// <summary>
    /// 兴趣
    /// </summary>
    public class Interest : CBB.RankingHelper.IRankingAble
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
        public Creater.Creater Creater
        {
            get { return this.creater; }
            set { this.creater = value; }
        }
        private Creater.Creater creater;
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
        //兴趣话题集合
        public IList<WenWen.WenWen> WenWens
        {
            get { return this.wenWens; }
            set { this.wenWens = value; }
        }
        private IList<WenWen.WenWen> wenWens;
        /// <summary>
        /// 兴趣话题集合(男)
        /// </summary>
        public IList<WenWen.WenWen> WenWensToBoy
        {
            get { return this.wenWensToBoy; }
            set { this.wenWensToBoy = value; }
        }
        private IList<WenWen.WenWen> wenWensToBoy;
        /// <summary>
        /// 兴趣话题集合(女)
        /// </summary>
        public IList<WenWen.WenWen> WenWensToGirl
        {
            get { return this.wenWensToGirl; }
            set { this.wenWensToGirl = value; }
        }
        private IList<WenWen.WenWen> wenWensToGirl;
        /// <summary>
        /// 兴趣话题更新时间
        /// </summary>
        public DateTime UpdateTimeToWenWen
        {
            get { return this.updateTimeToWenWen; }
            set { this.updateTimeToWenWen = value; }
        }
        private DateTime updateTimeToWenWen;

        public static String GetCollectionName()
        {
            return "Interest";
        }

        //获取ID
        public String GetObjID() { return ID ;}
        //获取排序表名
        public String GetPerMonthRankingTableName() { return "RankMonthInterest"; }
        public String GetWeeklyRankingTableName() { return "RankWeeklyInterest"; }
        public String GetEachYearRankingTableName() { return "RankYearInterest"; }
        public String GetDailyRankingTableName() { return "RankDailyInterest"; }
        //获取排名结果表名
        public String GetPerMonthRankResultTableName() { return "RankResultMonthInterest"; }
        public String GetWeeklyRankResultTableName() { return "RankResultWeeklyInterest"; }
        public String GetEachYearRankResultTableName() { return "RankResultYearInterest"; }
        public String GetDailyRankResultTableName() { return "RankResultDailyInterest"; }
    }
}
