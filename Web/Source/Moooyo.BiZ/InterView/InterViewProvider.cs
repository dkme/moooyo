using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.InterView
{
    public class InterViewProvider
    {
        public static IList<InterView> GetInterViews(String me, int pagesize, int pageno)
        {
            try
            {
                MongoCursor<InterView> mc = MongoDBHelper.GetCursor<InterView>(
                    "InterView",
                    Query.EQ("MemberID", me),
                    new SortByDocument("CreatedTime", 1),
                    pageno,
                    pagesize);

                List<InterView> objs = new List<InterView>();
                objs.AddRange(mc);

                return objs;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static IList<InterView> GetInterViews(String[] ids)
        {
            try
            {
                MongoCursor<InterView> mc = MongoDBHelper.GetCursor<InterView>(
                    "InterView",
                    Query.EQ("_id", new BsonArray(ids.ToArray())),
                    new SortByDocument("CreatedTime", 1),
                    0,
                    0);

                List<InterView> objs = new List<InterView>();
                objs.AddRange(mc);

                return objs;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static InterView GetInterView(String id)
        {
            InterView iv = new InterView();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterView> mc = md.GetCollection<InterView>("InterView");
                iv = mc.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
                return iv;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static InterView AddInterView(String mid, String systemQuestionID, String question, String answer)
        {
            InterView iv = new InterView();
            iv.MemberID = mid;
            iv.SystemQuestionID = systemQuestionID;
            iv.Question = question;
            iv.Answer = answer;
            iv.CreatedTime = DateTime.Now;

            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterView> mc = md.GetCollection<InterView>("InterView");
                mc.Insert(iv);
                //审核关键字
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(answer, InterView.GetCollectionName(), iv.ID, "Answer", iv.MemberID);
                //增加用户计数
                BiZ.MemberManager.MemberManager.ModifyInterViewCount(mid, MemberManager.StatusModifyType.Add);

                //增加动态
                BiZ.Member.Activity.ActivityController.AddActivity(
                    mid,
                    Member.Activity.ActivityType.UpdateInterView,
                    BiZ.Member.Activity.ActivityController.GetActivityContent_UpdateInterView_Title(),
                    BiZ.Member.Activity.ActivityController.GetActivityContent_UpdateInterView(iv.Question, iv.Answer),
                    false);

                return iv;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static CBB.ExceptionHelper.OperationResult UpdateInterView(String id,String mid, String answer)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterView> mc = md.GetCollection<InterView>("InterView");
                mc.Update(
                    Query.And(Query.EQ("_id", ObjectId.Parse(id)),Query.EQ("MemberID",mid)),
                    Update.Set("Answer", answer));
                //审核关键字
                new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(answer, InterView.GetCollectionName(), id, "Answer", mid);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static CBB.ExceptionHelper.OperationResult RemoveInterView(String id,String mid)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InterView> mc = md.GetCollection<InterView>("InterView");
                mc.Remove(Query.And(Query.EQ("_id", ObjectId.Parse(id)), Query.EQ("MemberID", mid)));

                //减少用户计数
                BiZ.MemberManager.MemberManager.ModifyInterViewCount(mid, MemberManager.StatusModifyType.Decrease);

                return new CBB.ExceptionHelper.OperationResult(true);
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
