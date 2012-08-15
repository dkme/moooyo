using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using CBB.MongoDB;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace Moooyo.BiZ.FilterText
{
    //邱志明
    //2012-02-23
    //脏词过滤管理类
    public enum VerifyStatus
    {
        /// <summary>
        /// 待审
        /// </summary>
        waitaudit = 0,
        /// <summary>
        /// 通过
        /// </summary>
        auditpass = 1,
        /// <summary>
        /// 删除
        /// </summary>
        audidel = 2,
        /// <summary>
        /// 修改
        /// </summary>
        audimod = 3
    }

    public class FilterTextOperation
    {
        const string msg = "内容未能通过审核已被删除！";
        /// <summary>
        /// 添加待审文本
        /// </summary>
        /// <param name="ftm">待审文本模型</param>
        public void AddFilterText(FilterTextModel ftm)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterTextModel> mc = md.GetCollection<FilterTextModel>("FilterText");
                mc.Insert(ftm);
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }

        /// <summary>
        /// 依据审核状态批量查询待审文本
        /// </summary>
        /// <param name="verifyStatus">待审状态枚举</param>
        /// <returns></returns>
        public List<FilterTextModel> GetFilterTexts(VerifyStatus verifyStatus, int pageno, int pagesize)
        {
            List<FilterTextModel> list = new List<FilterTextModel>();
            try
            {
                MongoCursor<FilterTextModel> mc = MongoDBHelper.GetCursor<FilterTextModel>(
                "FilterText",
                Query.EQ("Verify_status", Convert.ToInt32(verifyStatus)),
                new SortByDocument("Jion_time", -1),
                pageno,
                pagesize
                );
                List<FilterTextModel> objs = new List<FilterTextModel>();
                objs.AddRange(mc);
                return objs;
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <param name="verifyStatus">类型枚举</param>
        /// <returns></returns>
        public long GetCount(VerifyStatus verifyStatus)
        {
            long count;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterTextModel> mc = md.GetCollection<FilterTextModel>("FilterText");
                count = mc.Count(Query.EQ("Verify_status", Convert.ToInt32(verifyStatus)));
                return count;
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
        /// 批量修改待审状态
        /// </summary>
        /// <param name="_id">处理id集合</param>
        /// <param name="verify_status">审核状态</param>
        /// <param name="Verify_id">审核人id</param>
        /// <param name="texts">修改后的文本，其他状态传Null</param>
        public void UpdateFilterTexts(List<ObjectId> _id, VerifyStatus verify_status, string Verify_id, List<string> texts)
        {
            if (_id.Count <= 0)
                return;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterTextModel> mc = md.GetCollection<FilterTextModel>("FilterText");
                if (verify_status == VerifyStatus.audidel)
                {

                    QueryConditionList qcl = Query.In("_id", new BsonArray(_id.ToArray()));
                    mc.Update(qcl, Update.Set("Verify_status", Convert.ToInt32(verify_status)).Set("Verify_id", Verify_id).Set("Verify_time", DateTime.Now), UpdateFlags.Multi);
                    ////逐行删除
                    //foreach (ObjectId id in _id)
                    //{
                    //    FilterTextModel ftm = GetFilterText(id);
                    //    MongoCollection mc2 = md.GetCollection(ftm.Tablename);
                    //    QueryComplete qcl2 = Query.EQ("_id", ObjectId.Parse(ftm.Colid));
                    //    mc2.Remove(qcl2);
                    //}
                    //删除时替换成“已经删除”
                    for (int i = 0; i < _id.Count; i++)
                    {

                        FilterTextModel ftm = GetFilterText(_id[i]);
                        MongoCollection mc2 = md.GetCollection(ftm.Tablename);
                        if (null == mc2)
                            return;
                        QueryComplete qcl2 = Query.EQ("_id", ObjectId.Parse(ftm.Colid));
                        mc2.Update(qcl2, Update.Set(ftm.Colname, msg));
                        UpdateContent(ftm, msg);
                    }

                }
                else if (verify_status == VerifyStatus.auditpass) //通过
                {
                    if (texts.Count <= 0)
                        return;
                    QueryConditionList qcl = Query.In("_id", new BsonArray(_id.ToArray()));
                    mc.Update(qcl, Update.Set("Verify_status", Convert.ToInt32(verify_status)).Set("Verify_id", Verify_id).Set("Verify_time", DateTime.Now), UpdateFlags.Multi);
                    //逐行修改
                    for (int i = 0; i < _id.Count; i++)
                    {

                        FilterTextModel ftm = GetFilterText(_id[i]);
                        MongoCollection mc2 = md.GetCollection(ftm.Tablename);
                        if (null == mc2)
                            return;
                        QueryComplete qcl2 = Query.EQ("_id", ObjectId.Parse(ftm.Colid));
                        mc2.Update(qcl2, Update.Set(ftm.Colname, texts[i]));
                        UpdateContent(ftm, texts[i]);
                    }
                }
                else if (verify_status == VerifyStatus.audimod) //修改
                {
                    if (texts.Count <= 0)
                        return;
                    QueryConditionList qcl = Query.In("_id", new BsonArray(_id.ToArray()));
                    mc.Update(qcl, Update.Set("Verify_status", Convert.ToInt32(verify_status)).Set("Verify_id", Verify_id).Set("Verify_time", DateTime.Now), UpdateFlags.Multi);
                    //逐行修改
                    for (int i = 0; i < _id.Count; i++)
                    {

                        FilterTextModel ftm = GetFilterText(_id[i]);
                        MongoCollection mc2 = md.GetCollection(ftm.Tablename);
                        if (null == mc2)
                            return;
                        QueryComplete qcl2 = Query.EQ("_id", ObjectId.Parse(ftm.Colid));
                        mc2.Update(qcl2, Update.Set(ftm.Colname, texts[i]));
                        UpdateContent(ftm, texts[i]);
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }

        /// <summary>
        /// 获取单个待审文本模型
        /// </summary>
        /// <param name="_id">id</param>
        /// <returns></returns>
        public FilterTextModel GetFilterText(ObjectId _id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterTextModel> mc = md.GetCollection<FilterTextModel>("FilterText");
                IMongoQuery qc = Query.EQ("_id", _id);
                return mc.FindOne(qc);

            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }

        /// <summary>
        /// 修改单个待审文本的审核状态
        /// </summary>
        /// <param name="_id">id</param>
        /// <param name="Verify_status">待审状态枚举</param>
        /// <param name="text">修改后的文本(删除和通过的状态该值传"")</param>
        public void UpdateFilterText(ObjectId _id, VerifyStatus Verify_status, string Verify_id, string text)
        {
            try
            {
                if (Verify_status == VerifyStatus.waitaudit)
                    return;
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterTextModel> mc = md.GetCollection<FilterTextModel>("FilterText");

                if (Verify_status == VerifyStatus.audimod && text.Trim() != "")
                {
                    mc.Update(Query.EQ("_id", _id),
                        Update.Set("Verify_status", Convert.ToInt32(Verify_status))
                        .Set("Verify_id", Verify_id)
                        .Set("Verify_time", DateTime.Now)
                     );
                    FilterTextModel ftm = GetFilterText(_id);
                    MongoCollection mc2 = md.GetCollection(ftm.Tablename);
                    QueryComplete qcl2 = Query.EQ("_id", ObjectId.Parse(ftm.Colid));
                    mc2.Update(qcl2, Update.Set(ftm.Colname, text));
                    UpdateContent(ftm, text);
                }
                else if (Verify_status == VerifyStatus.audidel)
                {
                    mc.Update(Query.EQ("_id", _id),
                        Update.Set("Verify_status", Convert.ToInt32(Verify_status))
                        .Set("Verify_id", Verify_id)
                        .Set("Verify_time", DateTime.Now)
                     );
                    FilterTextModel ftm = GetFilterText(_id);
                    MongoCollection mc2 = md.GetCollection(ftm.Tablename);
                    //删除原始表中的信息
                    //if (null == mc2)
                    //    return;
                    //QueryComplete qcl2 = Query.EQ("_id", ObjectId.Parse(ftm.Colid));
                    //mc.Remove(qcl2);
                    //修改原始表中的信息为“已经删除”
                    QueryComplete qcl2 = Query.EQ("_id", ObjectId.Parse(ftm.Colid));
                    mc2.Update(qcl2, Update.Set(ftm.Colname, msg));
                    UpdateContent(ftm, msg);
                }
                else if (Verify_status == VerifyStatus.auditpass)
                {
                    //mc.Update(Query.EQ("_id", _id),
                    //       Update.Set("Verify_status", Convert.ToInt32(Verify_status))
                    //       .Set("Verify_id", Verify_id)
                    //       .Set("Verify_time", DateTime.Now)
                    //    );
                    mc.Update(Query.EQ("_id", _id),
                        Update.Set("Verify_status", Convert.ToInt32(Verify_status))
                        .Set("Verify_id", Verify_id)
                        .Set("Verify_time", DateTime.Now)
                     );
                    FilterTextModel ftm = GetFilterText(_id);
                    MongoCollection mc2 = md.GetCollection(ftm.Tablename);
                    QueryComplete qcl2 = Query.EQ("_id", ObjectId.Parse(ftm.Colid));
                    mc2.Update(qcl2, Update.Set(ftm.Colname, text));
                    UpdateContent(ftm, text);
                }
                else
                {
                    return;
                }

            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }

        public void UpdateContent(FilterTextModel ftm, string text)
        {
            if (ftm.Tablename == InterView.InterView.GetCollectionName())
            {
                Content.InterViewContent interviewobj = Content.ContentProvider.getInterviewContent(ftm.Colid, ftm.MemberID);
                foreach (var interview in interviewobj.InterviewList)
                {
                    if (interview.ID == ftm.Colid)
                    {
                        interview.Answer = text;
                        break;
                    }
                }
                new Content.PublicContent().savePublicContent(interviewobj);
            }
            else if (ftm.Tablename == Comment.Comment.GetCollectionName())
            {
                Content.PublicContent contentobj = Content.ContentProvider.getContentToComment(ftm.Colid);
                foreach (var comment in contentobj.AnswerList)
                {
                    if (comment.ID == ftm.Colid)
                    {
                        comment.Content = text;
                        break;
                    }
                }
                new Content.PublicContent().savePublicContent(contentobj);
            }
        }

        public void DeleteFilterTexts(List<ObjectId> objids)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterTextModel> mc = md.GetCollection<FilterTextModel>("FilterText");
                QueryConditionList qcl = Query.In("_id", new BsonArray(objids.ToArray()));
                mc.Remove(qcl);
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }
        public bool FindFliter(String tablename, String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterTextModel> mc = md.GetCollection<FilterTextModel>("FilterText");
                FilterTextModel obj = mc.FindOne(Query.And(Query.EQ("Tablename", tablename), Query.EQ("Colid", id), Query.EQ("Verify_status", 0)));
                if (obj == null)
                {
                    return true;
                }
                else
                {
                    if (obj.Verify_status == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }
    }
}
