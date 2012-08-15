using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;
using Moooyo.BiZ.Member.GlamourCounts;

namespace Moooyo.BiZ.Member.Relation
{
    /// <summary>
    /// 关系提供类
    /// </summary>
    public class RelationProvider
    {
        #region Vistor 访问
        public static CBB.ExceptionHelper.OperationResult AddVistor(String fromMember, String toMember)
        {
            Relation.Visitor vis = new Relation.Visitor();
            vis.FromMember = fromMember;
            vis.ToMember = toMember;
            vis.CreatedTime = DateTime.Now;

            return Relation.RelationProvider.AddRelation(vis, RelationOperationType.OneOperationEachHour);
        }
        public static IList<Visitor> GetVistors(String toMember, int pagesize, int pageno)
        {
            try
            {
                MongoCursor<Visitor> mcvistor = MongoDBHelper.GetCursor<Visitor>(
                    new Visitor().GetCollectionName(),
                    Query.EQ("ToMember", toMember),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);

                List<Visitor> objs = new List<Visitor>();
                objs.AddRange(mcvistor);

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
        public static int GetVistorsCount(String toMember)
        {
            long count = getRelationCount(toMember, "", new Visitor().GetCollectionName(), RelationDirection.ToMe);
            return (int)count;
        }
        #endregion

        #region Favorer 偏爱（关注）
        public static CBB.ExceptionHelper.OperationResult AddFavorer(String fromMember, String toMember)
        {
            Relation.Favorer obj = new Relation.Favorer();
            obj.FromMember = fromMember;
            obj.ToMember = toMember;
            obj.CreatedTime = DateTime.Now;

            return Relation.RelationProvider.AddRelation(obj, RelationOperationType.OnlyOneTimes);
        }
        public static CBB.ExceptionHelper.OperationResult DeleteFavorer(String fromMember, String toMember)
        {
            Relation.Favorer obj = new Relation.Favorer();
            obj.FromMember = fromMember;
            obj.ToMember = toMember;
            obj.CreatedTime = DateTime.Now;

            return Relation.RelationProvider.DeleteRelation(obj);
        }
        public static CBB.ExceptionHelper.OperationResult UpdateFavorer(String fromMember, String toMember, String comment)
        {
            Relation.Favorer obj = new Relation.Favorer();
            obj.FromMember = fromMember;
            obj.ToMember = toMember;
            obj.Comment = comment;
            obj.CreatedTime = DateTime.Now;

            return Relation.RelationProvider.ModifyRelation(obj);
        }
        public static CBB.ExceptionHelper.OperationResult UpdateFavorerVisitCount(String fromMember, String toMember)
        {
            Relation.Favorer obj = new Relation.Favorer();
            obj.FromMember = fromMember;
            obj.ToMember = toMember;
            obj.CreatedTime = DateTime.Now;

            return Relation.RelationProvider.UpdateFavorerVisitCount(obj);
        }
        public static IList<Favorer> GetFavorers(String fromMember, int pagesize, int pageno)
        {
            return GetFavorerList(fromMember, "FromMember", pagesize, pageno);
        }
        public static IList<Favorer> GetFavorersToVisitCount(String fromMember, int pagesize, int pageno)
        {
            try
            {
                MongoCursor<Favorer> mcobjs = MongoDBHelper.GetCursor<Favorer>(
                    new Favorer().GetCollectionName(),
                    Query.EQ("FromMember", fromMember),
                    new SortByDocument("VisitCount", -1),
                    pageno,
                    pagesize);
                List<Favorer> objs = new List<Favorer>();
                objs.AddRange(mcobjs);

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
        public static IList<Favorer> GetFavorers(String[] fromMembers, int pagesize, int pageno)
        {
            try
            {
                MongoCursor<Favorer> mcobjs = MongoDBHelper.GetCursor<Favorer>(
                    new Favorer().GetCollectionName(),
                    Query.In("FromMember", new BsonArray(fromMembers)),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);

                List<Favorer> objs = new List<Favorer>();
                objs.AddRange(mcobjs);

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
        public static int GetFavorersCount(String fromMember)
        {
            long count = getRelationCount(fromMember, "", new Favorer().GetCollectionName(), RelationDirection.FromMe);
            return (int)count;
        }
        public static IList<Favorer> GetListWhoFavoredMe(String toMember, int pagesize, int pageno)
        {
            return GetFavorerList(toMember, "ToMember", pagesize, pageno);
        }
        public static int GetListWhoFavoredMeCount(String toMember)
        {
            long count = getRelationCount(toMember, "", new Favorer().GetCollectionName(), RelationDirection.ToMe);
            return (int)count;
        }
        private static IList<Favorer> GetFavorerList(String member, String fromOrTo, int pagesize, int pageno)
        {
            try
            {
                MongoCursor<Favorer> mcobjs = MongoDBHelper.GetCursor<Favorer>(
                    new Favorer().GetCollectionName(),
                    Query.EQ(fromOrTo, member),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);

                List<Favorer> objs = new List<Favorer>();
                objs.AddRange(mcobjs);

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
        //public static CBB.ExceptionHelper.OperationResult UpdateAllFavorOrFansMyAvatar(string memberId, string avatar)
        //{
        //    try
        //    {
        //        MongoDatabase md = MongoDBHelper.MongoDB;
        //        MongoCollection<Favorer> mc2 = md.GetCollection<Favorer>(new Favorer().GetCollectionName());
        //        mc2.Update(
        //                    Query.Or(
        //                        Query.EQ("FromMember", memberId), 
        //                        Query.EQ("ToMember", memberId)
        //                    ),
        //                    Update.Inc("InterestCount", 1)
        //                );
        //        return new CBB.ExceptionHelper.OperationResult(true);
        //    }
        //    catch (System.Exception err)
        //    {
        //        throw new CBB.ExceptionHelper.OperationException(
        //            CBB.ExceptionHelper.ErrType.SystemErr,
        //            CBB.ExceptionHelper.ErrNo.DBOperationError,
        //            err);
        //    }
        //}
        public static bool IsMemberFavorMe(String me, String you)
        {
            long count = getRelationCount(me, you, new Favorer().GetCollectionName(), RelationDirection.ToMeFromYou);
            if (count > 0) return true;
            else return false;
        }
        public static bool IsInFavor(String me, String you)
        {
            long count = getRelationCount(me, you, new Favorer().GetCollectionName(), RelationDirection.FromMeToYou);
            if (count > 0) return true;
            else return false;
        }
        #endregion

        #region Giftor 礼物
        public static CBB.ExceptionHelper.OperationResult AddGiftor(String fromMember, String toMember,String giftID,String giftName,String comment)
        {
            Relation.Giftor obj = new Relation.Giftor();
            obj.FromMember = fromMember;
            obj.ToMember = toMember;
            obj.Comment = comment;
            obj.SystemGiftID = giftID;
            obj.GiftName = giftName;
            obj.CreatedTime = DateTime.Now;

            return Relation.RelationProvider.AddRelation(obj, RelationOperationType.AlwaysAllowOperation);
        }
        public static CBB.ExceptionHelper.OperationResult DeleteGiftor(String fromMember, String toMember)
        {
            Relation.Giftor obj = new Relation.Giftor();
            obj.FromMember = fromMember;
            obj.ToMember = toMember;

            return Relation.RelationProvider.DeleteRelation(obj);
        }
        public static IList<Giftor> GetGiftors(String toMember, int pagesize, int pageno)
        {
            return GetGiftList(toMember, "ToMember", pagesize, pageno);
        }
        public static IList<Giftor> GetMySendedGifts(String fromMember, int pagesize, int pageno)
        {
            return GetGiftList(fromMember, "FromMember", pagesize, pageno);
        }
        private static IList<Giftor> GetGiftList(String member, String fromOrTo, int pagesize, int pageno)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Giftor> mc = md.GetCollection<Giftor>(new Giftor().GetCollectionName());
                IMongoQuery qc = Query.EQ(fromOrTo, member);
                MongoCursor<Giftor> mcobjs = mc.Find(qc);

                List<Giftor> objs = new List<Giftor>();
                objs.AddRange(mcobjs);

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
        #endregion

        //#region Scorer 评分
        //public static CBB.ExceptionHelper.OperationResult AddScorer(String fromMember, String toMember, int score, String comment)
        //{
        //    Relation.Scorer obj = new Relation.Scorer();
        //    obj.FromMember = fromMember;
        //    obj.ToMember = toMember;
        //    obj.Comment = comment;
        //    obj.Score = score;
        //    obj.CreatedTime = DateTime.Now;

        //    return Relation.RelationProvider.AddRelation(obj, RelationOperationType.OnlyOneTimes);
        //}
        //public static IList<Scorer> GetScorers(String toMember, int pagesize, int pageno)
        //{
        //    return GetScorerList(toMember, "ToMember", pagesize, pageno);
        //}
        //public static IList<Scorer> GetMyScoredMembers(String fromMember, int pagesize, int pageno)
        //{
        //    return GetScorerList(fromMember, "FromMember", pagesize, pageno);
        //}
        //private static IList<Scorer> GetScorerList(String member, String fromOrTo, int pagesize, int pageno)
        //{
        //    try
        //    {
        //        IMongoQuery qc = Query.EQ(fromOrTo, member);
        //        MongoCursor<Scorer> mcobjs = MongoDBHelper.GetCursor<Scorer>(
        //            new Scorer().GetCollectionName(),
        //            qc,
        //            new SortByDocument("CreatedTime", -1),
        //            pageno,
        //            pagesize);

        //        List<Scorer> objs = new List<Scorer>();
        //        objs.AddRange(mcobjs);

        //        return objs;
        //    }
        //    catch (System.Exception err)
        //    {
        //        throw new CBB.ExceptionHelper.OperationException(
        //            CBB.ExceptionHelper.ErrType.SystemErr,
        //            CBB.ExceptionHelper.ErrNo.DBOperationError,
        //            err);
        //    }
        //}
        //public static Scorer GetMemberScoredMe(String frommember, String tomember)
        //{
        //    try
        //    {
        //        MongoDatabase md = MongoDBHelper.MongoDB;
        //        MongoCollection<Scorer> mc = md.GetCollection<Scorer>(new Scorer().GetCollectionName());
        //        IMongoQuery qc = Query.And(Query.EQ("FromMember", frommember), Query.EQ("ToMember", tomember));
        //        Scorer mcobj = mc.FindOne(qc);

        //        return mcobj;
        //    }
        //    catch (System.Exception err)
        //    {
        //        throw new CBB.ExceptionHelper.OperationException(
        //            CBB.ExceptionHelper.ErrType.SystemErr,
        //            CBB.ExceptionHelper.ErrNo.DBOperationError,
        //            err);
        //    }
        //}
        //#endregion

        #region GlamourCounts 魅力值
        //给用户添加魅力值
        public static CBB.ExceptionHelper.OperationResult AddGlamourValue(String fromMember, String toMember, GlamourCountOperate.GlamourCountType glamType, GlamourCountOperate.ModifyGlamourValue modiGlamVal)
        {
            try
            {
                ////判断是否在同一天发送
                //IList<GlamourCounts.GlamourCounts> glamourCountsList = Relation.RelationProvider.GetGlamourCounts(fromMember, toMember, glamType, modiGlamVal);
                //if (glamourCountsList != null)
                //{
                //    foreach (GlamourCounts.GlamourCounts glamourCount in glamourCountsList)
                //    {
                //        if (glamourCount.CreatedTime.Day == DateTime.Now.Day)
                //            return new CBB.ExceptionHelper.OperationResult(false, "你今天已经送过了，明天再送吧");
                //    }
                //}

                GlamourCounts.GlamourCounts glamCount = new GlamourCounts.GlamourCounts();
                float glamVal = (float)modiGlamVal;
                glamCount.FromMember = fromMember;
                glamCount.ToMember = toMember;
                glamCount.CreatedTime = DateTime.Now;
                glamCount.Value = glamVal;
                glamCount.Type = (byte)glamType;

                MongoDatabase mgDb = MongoDBHelper.MongoDB;
                MongoCollection<GlamourCounts.GlamourCounts> mgClect = mgDb.GetCollection<GlamourCounts.GlamourCounts>(GlamourCounts.GlamourCounts.CollectionName());
                mgClect.Insert(glamCount);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError, err);
            }
        }
        //查询魅力值信息
        public static IList<GlamourCounts.GlamourCounts> GetGlamourCounts(String fromMember, String toMember, GlamourCountOperate.GlamourCountType glamType, GlamourCountOperate.ModifyGlamourValue modiGlamVal)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<GlamourCounts.GlamourCounts> mc = md.GetCollection<GlamourCounts.GlamourCounts>(GlamourCounts.GlamourCounts.CollectionName());
                IMongoQuery qc = Query.And(Query.EQ("FromMember", fromMember), Query.EQ("ToMember", toMember), Query.EQ("Type", (byte)glamType), Query.EQ("Value", (float)modiGlamVal));
                MongoCursor<GlamourCounts.GlamourCounts> mgCur = mc.Find(qc);
                List<GlamourCounts.GlamourCounts> objs = new List<GlamourCounts.GlamourCounts>();
                objs.AddRange(mgCur);
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
        #endregion

        //#region Marker 标记
        //public static CBB.ExceptionHelper.OperationResult AddMarker(String fromMember, String toMember, Mark.MarkType type, String comment)
        //{
        //    Relation.Marker obj = new Relation.Marker();
        //    obj.FromMember = fromMember;
        //    obj.ToMember = toMember;
        //    obj.Comment = comment;
        //    obj.MarkType = type;
        //    obj.CreatedTime = DateTime.Now;
            
        //    return Relation.RelationProvider.AddRelation(obj, RelationOperationType.OnlyOneTimes);
        //}
        //public static IList<Marker> GetMarkers(String toMember, int pagesize, int pageno)
        //{
        //    return GetMarkList(toMember, "ToMember",pagesize,pageno);
        //}
        //public static IList<Marker> GetMyMarkedMembers(String fromMember, int pagesize, int pageno)
        //{
        //    return GetMarkList(fromMember, "FromMember", pagesize, pageno);
        //}
        //private static IList<Marker> GetMarkList(String member, String fromOrTo, int pagesize, int pageno)
        //{
        //    try
        //    {
        //        MongoDatabase md = MongoDBHelper.MongoDB;
        //        MongoCollection<Marker> mc = md.GetCollection<Marker>(new Marker().GetCollectionName());
        //        IMongoQuery qc = Query.EQ(fromOrTo, member);
        //        MongoCursor<Marker> mcobjs = mc.Find(qc);

        //        List<Marker> objs = new List<Marker>();
        //        objs.AddRange(mcobjs);

        //        return objs;
        //    }
        //    catch (System.Exception err)
        //    {
        //        throw new CBB.ExceptionHelper.OperationException(
        //            CBB.ExceptionHelper.ErrType.SystemErr,
        //            CBB.ExceptionHelper.ErrNo.DBOperationError,
        //            err);
        //    }
        //}
        //public static Marker GetMemberMarkedMe(String frommember, String tomember)
        //{
        //    try
        //    {
        //        MongoDatabase md = MongoDBHelper.MongoDB;
        //        MongoCollection<Marker> mc = md.GetCollection<Marker>(new Marker().GetCollectionName());
        //        IMongoQuery qc = Query.And(Query.EQ("FromMember", frommember), Query.EQ("ToMember", tomember));
        //        Marker mcobj = mc.FindOne(qc);

        //        return mcobj;
        //    }
        //    catch (System.Exception err)
        //    {
        //        throw new CBB.ExceptionHelper.OperationException(
        //            CBB.ExceptionHelper.ErrType.SystemErr,
        //            CBB.ExceptionHelper.ErrNo.DBOperationError,
        //            err);
        //    }
        //}
        //#endregion

        #region Silentor 无声（静默）
        public static CBB.ExceptionHelper.OperationResult AddSilentor(String fromMember, String toMember)
        {
            Relation.Silentor obj = new Relation.Silentor();
            obj.FromMember = fromMember;
            obj.ToMember = toMember;
            obj.CreatedTime = DateTime.Now;

            //MemberManager.MemberManager.ModifyMarkedTimes(toMember, MemberManager.StatusModifyType.Add);

            return Relation.RelationProvider.AddRelation(obj, RelationOperationType.OnlyOneTimes);
        }
        public static IList<Silentor> GetSilentors(String toMember, int pagesize, int pageno)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Silentor> mc = md.GetCollection<Silentor>(new Silentor().GetCollectionName());
                IMongoQuery qc = Query.EQ("ToMember", toMember);
                MongoCursor<Silentor> mcobjs = mc.Find(qc);

                List<Silentor> objs = new List<Silentor>();
                objs.AddRange(mcobjs);

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
        #endregion

        #region Disabler 禁用（静止）
        public static int GetDisablersCount(String fromMember)
        {
            long count = getRelationCount(fromMember, "", new Disabler().GetCollectionName(), RelationDirection.FromMe);
            return (int)count;
        }
        public static CBB.ExceptionHelper.OperationResult AddDisabler(String fromMember, String toMember)
        {
            Relation.Disabler obj = new Relation.Disabler();
            obj.FromMember = fromMember;
            obj.ToMember = toMember;
            obj.CreatedTime = DateTime.Now;

            return Relation.RelationProvider.AddRelation(obj, RelationOperationType.OnlyOneTimes);
        }
        public static CBB.ExceptionHelper.OperationResult DeleteDisabler(String fromMember, String toMember)
        {
            Relation.Disabler obj = new Relation.Disabler();
            obj.FromMember = fromMember;
            obj.ToMember = toMember;
            obj.CreatedTime = DateTime.Now;

            return Relation.RelationProvider.DeleteRelation(obj);
        }
        public static IList<Disabler> GetDisablers(String fromMember,int pagesize,int pageno)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Disabler> mc = md.GetCollection<Disabler>(new Disabler().GetCollectionName());
                IMongoQuery qc = Query.EQ("FromMember", fromMember);
                MongoCursor<Disabler> mcobjs = mc.Find(qc);

                List<Disabler> objs = new List<Disabler>();
                objs.AddRange(mcobjs);

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
        public static bool IsMemberDisableMe(String me, String you)
        {
            long count = getRelationCount(me, you, new Disabler().GetCollectionName(), RelationDirection.FromMeToYou);
            if (count > 0) return true;
            else return false;
        }
        #endregion

        #region 最后一次聊天
        public static CBB.ExceptionHelper.OperationResult AddLastMsger(String fromMember, String toMember, String comment, Activity.ActivityType type, out bool isFristConnect)
        {
            Relation.LastMsger obj = new Relation.LastMsger();
            obj.FromMember = fromMember;
            obj.FromMemberDeleted = false;
            obj.ToMember = toMember;
            obj.ToMemberDeleted = false;
            obj.Comment = comment;
            obj.Type = type;
            obj.CreatedTime = DateTime.Now;

            return UpdateLastMsger(obj, out isFristConnect);
        }
        public static CBB.ExceptionHelper.OperationResult DeleteLastMsger(String fromMember, String toMember)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Relation.LastMsger> mc = md.GetCollection<Relation.LastMsger>(new Relation.LastMsger().GetCollectionName());
                IMongoQuery qc =
                    Query.And(Query.EQ("FromMember", fromMember), Query.EQ("ToMember", toMember));

                mc.Update(qc,Update.Set("FromMemberDeleted",true));

                IMongoQuery qc2 =
                    Query.And(Query.EQ("ToMember", fromMember), Query.EQ("FromMember", toMember));

                mc.Update(qc2, Update.Set("ToMemberDeleted", true));

                SetMyUnreadMsgCountZero(fromMember, toMember);

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
        public static CBB.ExceptionHelper.OperationResult UpdateLastMsger(Relation.LastMsger obj, out bool isFristConnect)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Relation.LastMsger> mc = md.GetCollection<Relation.LastMsger>(obj.GetCollectionName());
                IMongoQuery qc =
                Query.Or(
                    Query.And(Query.EQ("FromMember", obj.FromMember), Query.EQ("ToMember", obj.ToMember)),
                    Query.And(Query.EQ("FromMember", obj.ToMember), Query.EQ("ToMember", obj.FromMember))
                );

                Relation.LastMsger oObj = mc.FindOne(qc);

                isFristConnect = false;
                //如果第一次联系，则未读信息为1
                if (oObj == null)
                {
                    obj.UnReads = new List<UnRead>();
                    UnRead u = new UnRead();
                    u.SenderMid = obj.FromMember;

                    //如果没有屏蔽，增加未读数量
                    if (obj.Type != Activity.ActivityType.DisabledTalk & obj.Type != Activity.ActivityType.DisabledMsg)
                        u.UnReadCount = 1;

                    //如果是主动发起聊天
                    if (obj.Type == Activity.ActivityType.Talk)
                    {
                        //标记曾经有过主动聊天
                        obj.HasTalk = true;
                        MemberManager.MemberManager.ModifyTodayOutCallsCount(obj.FromMember, MemberManager.StatusModifyType.Add);
                        MemberManager.MemberManager.ModifyTodayInCallsCount(obj.ToMember, MemberManager.StatusModifyType.Add);

                        isFristConnect = true;
                    }
                    obj.UnReads.Add(u);
                }
                else
                {
                    //赋值Key
                    obj._id = oObj._id;
                    obj.UnReads = oObj.UnReads;
                    obj.HasTalk = oObj.HasTalk;

                    bool flag = false;
                    foreach (UnRead u in obj.UnReads)
                    {
                        if (u.SenderMid == obj.FromMember)
                        {
                            //如果没有屏蔽，增加未读数量
                            if (obj.Type != Activity.ActivityType.DisabledTalk & obj.Type != Activity.ActivityType.DisabledMsg)
                                u.UnReadCount++;

                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        UnRead u = new UnRead();
                        u.SenderMid = obj.FromMember;
                        //如果没有屏蔽，增加未读数量
                        if (obj.Type != Activity.ActivityType.DisabledTalk & obj.Type != Activity.ActivityType.DisabledMsg)
                            u.UnReadCount = 1;

                        obj.UnReads.Add(u);
                    }
                    //如果以前没有用户主动聊过
                    if (obj.HasTalk == false)
                    {
                        //如果是主动发起聊天
                        if (obj.Type == Activity.ActivityType.Talk)
                        {
                            //标记曾经有过主动聊天
                            obj.HasTalk = true;
                            MemberManager.MemberManager.ModifyTodayOutCallsCount(obj.FromMember, MemberManager.StatusModifyType.Add);
                            MemberManager.MemberManager.ModifyTodayInCallsCount(obj.ToMember, MemberManager.StatusModifyType.Add);

                            isFristConnect = true;
                        }
                    }
                }
                mc.Save(obj);

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
        public static CBB.ExceptionHelper.OperationResult SetMyUnreadMsgCountZero(String me, String you)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Relation.LastMsger> mc = md.GetCollection<Relation.LastMsger>( new Relation.LastMsger().GetCollectionName());
                IMongoQuery qc =
                Query.Or(
                    Query.And(Query.EQ("FromMember", me), Query.EQ("ToMember", you)),
                    Query.And(Query.EQ("FromMember", you), Query.EQ("ToMember", me))
                );

                Relation.LastMsger oObj = mc.FindOne(qc);

                if (oObj == null)
                {
                    return new CBB.ExceptionHelper.OperationResult(false, "未找到对应记录");
                }
                else
                {
                    foreach (UnRead u in oObj.UnReads)
                    {
                        if (u.SenderMid == you)
                        {
                            u.UnReadCount = 0;
                        }
                    }
                }
                mc.Save(oObj);
                int unreadCount = GetMemberAllUnreadMsgCount(me);
                
                BiZ.MemberManager.MemberManager.ModifyUnReadMsgCount(me, unreadCount);

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
        /// <summary>
        /// 获取所有未读信息数量
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <returns>未读数量</returns>
        private static int GetMemberAllUnreadMsgCount(String mid)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Relation.LastMsger> mc = md.GetCollection<Relation.LastMsger>(new Relation.LastMsger().GetCollectionName());
            IMongoQuery qc =
                Query.Or(
                    Query.EQ("ToMember", mid),
                    Query.EQ("FromMember",mid)
                );

            MongoCursor<Relation.LastMsger> mCur = mc.Find(qc);
            List<Relation.LastMsger> lastMsgers = new List<LastMsger>();
            lastMsgers.AddRange(mCur);
            int unreadCount = 0;
            foreach (LastMsger lastMsger in lastMsgers)
            {
                foreach (UnRead ur in lastMsger.UnReads)
                {
                    if (ur.SenderMid != mid && ur.UnReadCount > 0)
                        unreadCount += ur.UnReadCount;
                }
            }
            return unreadCount;
        }
        public static IList<Relation.LastMsger> GetLastMsgers(String mid, int pagesize, int pageno)
        {
            try
            {
                MongoCursor<Relation.LastMsger> mcvistor = MongoDBHelper.GetCursor<Relation.LastMsger>(
                    new Relation.LastMsger().GetCollectionName(),
                    Query.Or(
                        Query.And(
                            Query.EQ("FromMember", mid), 
                            Query.EQ("FromMemberDeleted", false), 
                            Query.Or(
                                Query.EQ("Type", (int)Activity.ActivityType.Talk),
                                //Query.EQ("Type", (int)Activity.ActivityType.SaiHi),
                                Query.EQ("Type", (int)Activity.ActivityType.AskToDate),
                                Query.EQ("HasTalk", true),
                                Query.EQ("Type", (int)Activity.ActivityType.DisabledTalk),
                                Query.EQ("Type", (int)Activity.ActivityType.CommentBeenReplied)
                            )
                        ), 
                        Query.And(
                            Query.EQ("ToMember", mid), 
                            Query.EQ("ToMemberDeleted", false),
                            Query.Or(
                                Query.LT("Type", (int)Activity.ActivityType.DisabledTalk),
                                Query.GT("Type", (int)Activity.ActivityType.DisabledMsg),
                                Query.EQ("Type", (int)Activity.ActivityType.CommentBeenReplied)
                            )
                        )
                    ),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);

                List<Relation.LastMsger> objs = new List<Relation.LastMsger>();
                objs.AddRange(mcvistor);

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
        public static int GetLastMsgersCount(String mid)
        {
            return (int)MongoDBHelper.GetCount(
                new Relation.LastMsger().GetCollectionName(),
                Query.Or(
                    Query.And(
                        Query.EQ("FromMember", mid), 
                        Query.EQ("Type", (int)Activity.ActivityType.Talk)
                        ), 
                    Query.EQ("ToMember", mid)
                    )
                );
        }
        #endregion

        #region RegInviter 注册邀请
        public static CBB.ExceptionHelper.OperationResult AddRegInviter(String fromMember, String toMember)
        {
            Relation.RegInviter obj = new Relation.RegInviter();
            obj.FromMember = fromMember;
            obj.ToMember = toMember;
            obj.CreatedTime = DateTime.Now;

            return Relation.RelationProvider.AddRelation(obj, RelationOperationType.OnlyOneTimes);
        }
        public static int GetRegInvitersCount(String fromMember)
        {
            long count = getRelationCount(fromMember, "", new RegInviter().GetCollectionName(), RelationDirection.FromMe);
            return (int)count;
        }
        private static IList<RegInviter> GetRegInviteList(String member, String fromOrTo, int pagesize, int pageno)
        {
            try
            {
                MongoCursor<RegInviter> mcobjs = MongoDBHelper.GetCursor<RegInviter>(
                    new Favorer().GetCollectionName(),
                    Query.EQ(fromOrTo, member),
                    new SortByDocument("CreatedTime", -1),
                    pageno,
                    pagesize);

                List<RegInviter> objs = new List<RegInviter>();
                objs.AddRange(mcobjs);

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
        #endregion

        public static CBB.ExceptionHelper.OperationResult AddRelation(RelationMember obj,RelationOperationType t)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<RelationMember> mc = md.GetCollection<RelationMember>(obj.GetCollectionName());
                if (t == RelationOperationType.OnlyOneTimes && mc.Count(Query.And(Query.EQ("FromMember", obj.FromMember), Query.EQ("ToMember", obj.ToMember))) > 0)
                {
                    return new CBB.ExceptionHelper.OperationResult(false, "OverrideOneTimes");
                }
                if (t == RelationOperationType.OneOperationEachHour && mc.Count(Query.And(Query.EQ("FromMember", obj.FromMember), Query.EQ("ToMember", obj.ToMember), Query.GT("CreatedTime", DateTime.Now.AddHours(-1)))) > 0)
                {
                    return new CBB.ExceptionHelper.OperationResult(false, "OverrideOneTimesThisHour");
                }
                if (t == RelationOperationType.OneOperationEachDay && mc.Count(Query.And(Query.EQ("FromMember", obj.FromMember), Query.EQ("ToMember", obj.ToMember), Query.GT("CreatedTime", DateTime.Now.AddDays(-1)))) > 0)
                {
                    return new CBB.ExceptionHelper.OperationResult(false, "OverrideOneTimesThisDay");
                }
                mc.Save(obj);
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
        public static CBB.ExceptionHelper.OperationResult DeleteRelation(RelationMember obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<RelationMember> mc = md.GetCollection<RelationMember>(obj.GetCollectionName());
                IMongoQuery qc = Query.And(Query.EQ("FromMember", obj.FromMember), Query.EQ("ToMember", obj.ToMember));
                mc.Remove(qc);

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
        public static CBB.ExceptionHelper.OperationResult ModifyRelation(RelationMember obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<RelationMember> mc = md.GetCollection<RelationMember>(obj.GetCollectionName());
                IMongoQuery qc = Query.And(Query.EQ("FromMember", obj.FromMember), Query.EQ("ToMember", obj.ToMember));
                IMongoUpdate ud = Update.Combine(
                    Update.Set("Comment", obj.Comment)
                    );
                mc.Update(qc, ud,  UpdateFlags.Upsert);

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
        public static CBB.ExceptionHelper.OperationResult UpdateFavorerVisitCount(RelationMember obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<RelationMember> mc = md.GetCollection<RelationMember>(obj.GetCollectionName());
                IMongoQuery qc = Query.And(Query.EQ("FromMember", obj.FromMember), Query.EQ("ToMember", obj.ToMember));
                IMongoUpdate ud = Update.Combine(
                    Update.Inc("VisitCount", 1)
                    );
                mc.Update(qc, ud, UpdateFlags.Upsert);

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
        public static long getRelationCount(String me, String you, String RelationName, RelationDirection dir)
        {
            try
            {
                IMongoQuery qc=null;
                if (dir == RelationDirection.FromMe) qc = Query.EQ("FromMember", me);
                if (dir == RelationDirection.ToMe) qc = Query.EQ("ToMember", me);
                if (dir == RelationDirection.FromMeToYou) qc =
                    Query.And(Query.EQ("FromMember", me), Query.EQ("ToMember", you));
                if (dir == RelationDirection.ToMeFromYou) qc =
                    Query.And(Query.EQ("FromMember", you), Query.EQ("ToMember", me));
                if (dir == RelationDirection.Both) qc =
                    Query.Or(
                        Query.And(Query.EQ("FromMember", you), Query.EQ("ToMember", me)),
                        Query.And(Query.EQ("FromMember", me), Query.EQ("ToMember", you))
                    );

                long count = MongoDBHelper.GetCount(
                    RelationName,
                    qc);

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
    }
}
