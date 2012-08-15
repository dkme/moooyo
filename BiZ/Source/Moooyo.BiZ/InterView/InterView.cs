using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.InterView
{
    /// <summary>
    /// 访谈
    /// </summary>
    public class InterView : Comment.ICanBeenComment
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
        /// 用户ID
        /// </summary>
        public String MemberID
        {
            get { return this.memberid; }
            set { this.memberid = value; }
        }
        private String memberid;
        /// <summary>
        /// 系统问题ID
        /// </summary>
        public String SystemQuestionID
        {
            get { return this.systemQuestionID; }
            set { this.systemQuestionID = value; }
        }
        private String systemQuestionID;
        /// <summary>
        /// 问题
        /// </summary>
        public String Question
        {
            get { return this.question; }
            set { this.question = value; }
        }
        private String question;
        /// <summary>
        /// 答案
        /// </summary>
        public String Answer
        {
            get { return this.answer; }
            set { this.answer = value; }
        }
        private String answer;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return this.createdTime; }
            set { this.createdTime = value; }
        }
        private DateTime createdTime;
        //喜欢的数量
        private long likeCount;
        public long LikeCount
        {
            get { return likeCount; }
            set { likeCount = value; }
        }
        //回复的数量
        private long answerCount;
        public long AnswerCount
        {
            get { return answerCount; }
            set { answerCount = value; }
        }
        //回复的集合
        private List<Comment.Comment> answerList;
        public List<Comment.Comment> AnswerList
        {
            get { return answerList; }
            set { answerList = value; }
        }
        /// <summary>
        /// 更新访谈喜欢数量
        /// </summary>
        /// <param name="interviewID">访谈编号</param>
        /// <returns></returns>
        public Boolean UpdateLikeCount(String interviewID)
        {
            try
            {
                InterView obj = new InterView();
                obj = InterViewProvider.GetInterView(interviewID);
                if (obj != null)
                {
                    obj.LikeCount = Like.LikeDataFactory.GetLike<Like.LikeData>(null, interviewID, Like.LikeType.InterView, Like.LikeData.GetCollectionName()).Count;
                    obj.Save(obj);
                }
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
        /// <summary>
        /// 更新内容的回复显示集合，工厂方法
        /// </summary>
        /// <param name="ID">回复对象</param>
        /// <returns>操作是否成功</returns>
        public Boolean UpdateCommentList(Comment.Comment comment)
        {
            try
            {
                InterView obj = new InterView();
                obj = InterViewProvider.GetInterView(comment.CommentToID);
                if (obj.AnswerList != null)
                {
                    if (obj.AnswerList.Count >= 6)
                    {
                        obj.AnswerList.Insert(0, comment);
                        obj.AnswerList.RemoveAt(obj.AnswerList.Count - 1);
                    }
                    else
                    {
                        obj.AnswerList.Insert(0, comment);
                    }
                }
                else
                {
                    List<Comment.Comment> commentlist = new List<Comment.Comment>();
                    commentlist.Add(comment);
                    obj.AnswerList = commentlist;
                }
                obj.AnswerCount = Comment.CommentProvider.findForTypeCount(comment.CommentToID, Comment.CommentType.InterView, Comm.DeletedFlag.No);
                obj.Save(obj);
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
        public InterView Save(InterView obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterView> mc = md.GetCollection<InterView>(InterView.GetCollectionName());
                mc.Save(obj);
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

        public static String GetCollectionName()
        {
            return "InterView";
        }
    }
}
