///
/// 功能描述：内容回复表数据结构
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/5/19
/// 附加信息：
///   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Comment
{
    /// <summary>
    /// 内容回复的实体类
    /// </summary>
    public class Comment
    {
        public ObjectId _id;//ID
        public String ID
        {
            get { if (_id != null) return _id.ToString(); else return ""; }
        }
        private String memberID;//创建者编号
        public String MemberID
        {
            get { return memberID; }
            set { memberID = value; }
        }
        private Moooyo.BiZ.Creater.Creater creater;//创建者
        public Moooyo.BiZ.Creater.Creater Creater
        {
            get { return creater; }
            set { creater = value; }
        }
        private String commentToID;//回复对象编号
        public String CommentToID
        {
            get { return commentToID; }
            set { commentToID = value; }
        }
        private String content;//回复内容
        public String Content
        {
            get { return content; }
            set { content = value; }
        }
        private DateTime createdTime;//创建时间
        public DateTime CreatedTime
        {
            get { return createdTime; }
            set { createdTime = value; }
        }
        private DateTime updateTime;//更新时间
        public DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }
        private CommentType commentType;//回复类型
        public CommentType CommentType
        {
            get { return commentType; }
            set { commentType = value; }
        }
        private Comm.DeletedFlag deleteFlag;//是否删除
        public Comm.DeletedFlag DeleteFlag
        {
            get { return deleteFlag; }
            set { deleteFlag = value; }
        }

        public Comment() { }
        public Comment(String contentID)
        {
            try
            {
                Comment obj = new Comment();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Comment> mc = md.GetCollection<Comment>(Comment.GetCollectionName());
                obj = mc.FindOne(Query.EQ("_id", ObjectId.Parse(contentID)));
                this._id = obj._id;
                this.MemberID = obj.MemberID;
                this.Creater = new Moooyo.BiZ.Creater.Creater(obj.MemberID);
                this.CommentToID = obj.CommentToID;
                this.Content = obj.Content;
                this.CreatedTime = obj.CreatedTime;
                this.UpdateTime = obj.UpdateTime;
                this.CommentType = obj.CommentType;
                this.DeleteFlag = obj.DeleteFlag;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public Comment(String memberID, String commentToID, String content,CommentType type)
        {
            this.MemberID = memberID;
            this.Creater = new BiZ.Creater.Creater(memberID);
            this.CommentToID = commentToID;
            this.Content = content;
            this.CreatedTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
            this.CommentType = type;
            this.DeleteFlag = Comm.DeletedFlag.No;
        }

        public static String GetCollectionName()
        {
            return "Comment";
        }
        /// <summary>
        /// 内容回复的添加或更新
        /// </summary>
        /// <param name="obj">回复对象</param>
        /// <returns>添加或更新操作是否成功</returns>
        public Comment Save(Comment obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Comment> mc = md.GetCollection<Comment>(Comment.GetCollectionName());
                mc.Save(obj);
                //审核关键字
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(obj.Content, Comment.GetCollectionName(), obj.ID, "Content", obj.MemberID);
                return obj;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        /// <summary>
        /// 回复删除
        /// </summary>
        /// <param name="ID">回复编号</param>
        /// <returns>删除是否成功</returns>
        public Boolean Remove(String ID)
        {
            try
            {
                Comment comment = new Comment(ID);
                comment.DeleteFlag = Comm.DeletedFlag.No;
                comment.Save(comment);
                return true;
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
