///
/// 功能描述：内容数据提供类
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

namespace Moooyo.BiZ.Content
{
    public class ContentProvider
    {
        /// <summary>
        /// 返回所有内容的集合
        /// </summary>
        /// <param name="deleteflag">删除状态</param>
        /// <param name="pageno">当前页数</param>
        /// <param name="pagesize">每页条数</param>
        /// <returns>内容数量</returns>
        public static IList<PublicContent> findAll(Comm.DeletedFlag deleteFlag, int pageno, int pagesize)
        {
            return getContentList(Query.EQ("DeleteFlag", deleteFlag), new SortByDocument("UpdateTime", -1), pagesize, pageno);
        }
        /// <summary>
        /// 按内容编号和创建者编号或取多条内容对象
        /// </summary>
        /// <param name="contentId">内容编号</param>
        /// <param name="memberId">创建者编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>内容对象列表</returns>
        public static IList<PublicContent> GetIDMemberContent(String contentId, String memberId, int pageSize, int pageNo)
        {
            QueryComplete qc = Query.And(Query.EQ("_id", ObjectId.Parse(contentId)), Query.EQ("MemberID", memberId));
            return getContentList(qc, new SortByDocument("UpdateTime", -1), pageSize, pageNo);
        }
        /// <summary>
        /// 返回所有内容的数量
        /// </summary>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns>内容数量</returns>
        public static long findAllCount(Comm.DeletedFlag deleteFlag)
        {
            return getContentListCount(Query.EQ("DeleteFlag", deleteFlag));
        }

        /// <summary>
        /// 按内容类型返回内容集合
        /// </summary>
        /// <param name="memberID">当前登录的用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <param name="pageno">当前页数</param>
        /// <param name="pagesize">每页条数</param>
        /// <returns>内容数量</returns>
        public static IList<PublicContent> findForType(String nowMemberID, String contenttype, Comm.DeletedFlag deleteFlag, int pageno, int pagesize)
        {
            return getContentList(findForTypeQC(nowMemberID, contenttype, deleteFlag), new SortByDocument("UpdateTime", -1), pagesize, pageno);
        }
        /// <summary>
        /// 按内容类型返回内容集合
        /// </summary>
        /// <param name="memberID">当前登录的用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <param name="pageno">当前页数</param>
        /// <param name="pagesize">每页条数</param>
        /// <returns>内容数量</returns>
        public static IList<PublicContent> findCreatedTimeForType(String nowMemberID, String contenttype, Comm.DeletedFlag deleteFlag, int pageno, int pagesize)
        {
            return getContentList(findForTypeQC(nowMemberID, contenttype, deleteFlag), new SortByDocument("CreatedTime", -1), pagesize, pageno);
        }
        /// <summary>
        /// 按内容类型返回内容集合数量
        /// </summary>
        /// <param name="memberID">当前登录的用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns>内容数量</returns>
        public static long findForTypeCount(String nowMemberID, String contenttype, Comm.DeletedFlag deleteFlag)
        {
            return getContentListCount(findForTypeQC(nowMemberID, contenttype, deleteFlag));
        }
        /// <summary>
        /// 按内容类型返回内容集合的查询条件
        /// </summary>
        /// <param name="memberID">当前登录的用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns></returns>
        public static QueryComplete findForTypeQC(String nowMemberID, String contenttype, Comm.DeletedFlag deleteFlag)
        {
            QueryComplete qc = Query.And(GetFindConditionToPermissions(nowMemberID, contenttype, deleteFlag));
            return qc;
        }

        /// <summary>
        /// 按用户编号返回内容集合
        /// </summary>
        /// <param name="nowMemberID">当前登录用户编号</param>
        /// <param name="toMemberID">被查看的用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <param name="pageno">当前页数</param>
        /// <param name="pagesize">每页条数</param>
        /// <returns>内容集合</returns>
        public static IList<PublicContent> findForMember(String nowMemberID, String toMemberID, String contenttype, Comm.DeletedFlag deleteFlag, int pageno, int pagesize)
        {
            return getContentList(findForMemberQC(nowMemberID, toMemberID, contenttype, deleteFlag), new SortByDocument("CreatedTime", -1), pagesize, pageno);
        }
        /// <summary>
        /// 按用户编号返回内容集合的数量
        /// </summary>
        /// <param name="nowMemberID">当前登录用户编号</param>
        /// <param name="toMemberID">被查看的用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns>集合数量</returns>
        public static long findForMemberCount(String nowMemberID, String toMemberID, String contenttype, Comm.DeletedFlag deleteFlag)
        {
            return getContentListCount(findForMemberQC(nowMemberID, toMemberID, contenttype, deleteFlag));
        }
        /// <summary>
        /// 按用户编号返回内容集合的查询条件
        /// </summary>
        /// <param name="nowMemberID">当前登录用户编号</param>
        /// <param name="toMemberID">被查看的用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns></returns>
        public static QueryComplete findForMemberQC(String nowMemberID, String toMemberID, String contenttype, Comm.DeletedFlag deleteFlag)
        {
            QueryComplete qc = null;
            if (nowMemberID == null || nowMemberID == "")
            {
                if (contenttype != null && contenttype != "")
                {
                    qc = Query.And(
                            Query.EQ("MemberID", toMemberID),
                            Query.EQ("ContentType", int.Parse(contenttype)),
                            Query.EQ("DeleteFlag", deleteFlag),
                            Query.Or(
                                Query.EQ("ContentPermissions", ContentPermissions.AllOpen)
                                )
                            );
                }
                if (contenttype == null || contenttype == "")
                {
                    qc = Query.And(
                            Query.EQ("MemberID", toMemberID),
                            Query.EQ("DeleteFlag", deleteFlag),
                            Query.Or(
                                Query.EQ("ContentPermissions", ContentPermissions.AllOpen)
                                )
                            );
                }
            }
            else
            {
                if (nowMemberID == toMemberID && (contenttype != null && contenttype != ""))
                {
                    qc = Query.And(
                            Query.EQ("MemberID", nowMemberID),
                            Query.EQ("ContentType", int.Parse(contenttype)),
                            Query.EQ("DeleteFlag", deleteFlag)
                            );
                }
                if (nowMemberID == toMemberID && (contenttype == null || contenttype == ""))
                {
                    qc = Query.And(
                            Query.EQ("MemberID", nowMemberID),
                            Query.EQ("DeleteFlag", deleteFlag)
                            );
                }
                if (nowMemberID != toMemberID && (contenttype != null && contenttype != ""))
                {
                    qc = Query.And(
                            Query.EQ("MemberID", toMemberID),
                            Query.EQ("ContentType", int.Parse(contenttype)),
                            Query.EQ("DeleteFlag", deleteFlag),
                            Query.Or(
                                Query.EQ("ContentPermissions", ContentPermissions.AllOpen),
                                Query.And(
                                    Query.EQ("ContentPermissions", ContentPermissions.MyFriend),
                                    Query.In("MyFriends", nowMemberID)
                                    )
                                )
                            );
                }
                if (nowMemberID != toMemberID && (contenttype == null || contenttype == ""))
                {
                    qc = Query.And(
                            Query.EQ("MemberID", toMemberID),
                            Query.EQ("DeleteFlag", deleteFlag),
                            Query.Or(
                                Query.EQ("ContentPermissions", ContentPermissions.AllOpen),
                                Query.And(
                                    Query.EQ("ContentPermissions", ContentPermissions.MyFriend),
                                    Query.In("MyFriends", nowMemberID)
                                    )
                                )
                            );
                }
            }
            return qc;
        }

        /// <summary>
        /// 按用户编号返回用户关注的人的内容集合
        /// </summary>
        /// <param name="nowMemberID">用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <param name="pageno">当前页数</param>
        /// <param name="pagesize">每页条数</param>
        /// <returns>内容集合</returns>
        public static IList<PublicContent> findForMenberToFavorer(String nowMemberID, String contenttype, Comm.DeletedFlag deleteFlag, int pageno, int pagesize)
        {
            return getContentList(findForMenberToFavorerQC(nowMemberID, contenttype, deleteFlag), new SortByDocument("UpdateTime", -1), pagesize, pageno);
        }
        /// <summary>
        /// 按用户编号返回用户关注的人的内容集合数量
        /// </summary>
        /// <param name="nowMemberID">用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns>集合数量</returns>
        public static long findForMenberToFavorerCount(String nowMemberID, String contenttype, Comm.DeletedFlag deleteFlag)
        {
            return getContentListCount(findForMenberToFavorerQC(nowMemberID, contenttype, deleteFlag));
        }
        /// <summary>
        /// 按用户编号返回用户关注的人的内容的查询条件
        /// </summary>
        /// <param name="nowMemberID">用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns></returns>
        public static QueryComplete findForMenberToFavorerQC(String nowMemberID, String contenttype, Comm.DeletedFlag deleteFlag)
        {
            //获取我关注的人
            IList<BiZ.Member.Relation.Favorer> favorerList = BiZ.Member.Relation.RelationProvider.GetFavorers(nowMemberID, 0, 0);
            List<string> favorerIDs = new List<string>();
            foreach (BiZ.Member.Relation.Favorer favorer in favorerList)
            {
                favorerIDs.Add(favorer.ToMember);
            }
            QueryComplete qc = null;
            if (contenttype != null && contenttype != "")
            {
                qc = Query.And(
                        Query.In("MemberID", new BsonArray(favorerIDs.ToArray())),
                        Query.EQ("ContentType", int.Parse(contenttype)),
                        Query.EQ("DeleteFlag", deleteFlag),
                        Query.Or(
                            Query.EQ("ContentPermissions", ContentPermissions.AllOpen),
                            Query.And(
                                Query.EQ("ContentPermissions", ContentPermissions.MyFriend),
                                Query.In("MyFriends", nowMemberID)
                                )
                            )
                        );
            }
            else
            {
                qc = Query.And(
                        Query.In("MemberID", new BsonArray(favorerIDs.ToArray())),
                        Query.EQ("DeleteFlag", deleteFlag),
                        Query.Or(
                            Query.EQ("ContentPermissions", ContentPermissions.AllOpen),
                            Query.And(
                                Query.EQ("ContentPermissions", ContentPermissions.MyFriend),
                                Query.In("MyFriends", nowMemberID)
                                )
                            )
                        );
            }
            return qc;
        }

        /// <summary>
        /// 根据条件获取我和我关注的人的内容集合
        /// </summary>
        /// <param name="nowMemberID">当前登录的用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">是否删除</param>
        /// <param name="pageno">页数</param>
        /// <param name="pagesize">每页条数</param>
        /// <returns></returns>
        public static IList<PublicContent> findContentForMyAndFavorer(String nowMemberID, String contenttype, Comm.DeletedFlag deleteFlag, int pageno, int pagesize)
        {
            return getContentList(findContentForMyAndFavorerQC(nowMemberID, contenttype, deleteFlag), new SortByDocument("UpdateTime", -1), pagesize, pageno);
        }
        /// <summary>
        /// 根据条件获取我和我关注的人的内容集合的数量
        /// </summary>
        /// <param name="nowMemberID">当前登录的用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">是否删除</param>
        /// <returns></returns>
        public static long findContentForMyAndFavorerCount(String nowMemberID, String contenttype, Comm.DeletedFlag deleteFlag)
        {
            return getContentListCount(findContentForMyAndFavorerQC(nowMemberID, contenttype, deleteFlag));
        }
        /// <summary>
        /// 根据条件获取我和我关注的人的内容集合的查询条件
        /// </summary>
        /// <param name="nowMemberID">当前登录的用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">是否删除</param>
        /// <returns></returns>
        public static QueryComplete findContentForMyAndFavorerQC(String nowMemberID, String contenttype, Comm.DeletedFlag deleteFlag)
        {
            //获取我关注的人
            IList<BiZ.Member.Relation.Favorer> favorerList = BiZ.Member.Relation.RelationProvider.GetFavorers(nowMemberID, 0, 0);
            List<string> favorerIDs = new List<string>();
            foreach (BiZ.Member.Relation.Favorer favorer in favorerList)
            {
                favorerIDs.Add(favorer.ToMember);
            }
            List<string> memberIds = new List<string>();
            memberIds.AddRange(favorerIDs);
            memberIds.Add(nowMemberID);
            QueryComplete qc = null;
            qc = Query.And(
                    Query.In("MemberID", new BsonArray(memberIds.ToArray())),
                    GetFindConditionToPermissions(nowMemberID, contenttype, deleteFlag)
                    );
            return qc;
        }

        /// <summary>
        /// 按某些条件返回内容集合
        /// </summary>
        /// <param name="memberID">当前登录的用户编号</param>
        /// <param name="interestID">兴趣编号</param>
        /// <param name="city">创建者城市</param>
        /// <param name="sex">创建者性别</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <param name="pageno">当前页数</param>
        /// <param name="pagesize">每页条数</param>
        /// <returns>内容集合</returns>
        public static IList<PublicContent> findForSomeThing(String nowMemberID, String interestID, String city, String sex, Comm.DeletedFlag deleteFlag, int pageno, int pagesize)
        {
            return getContentList(findForSomeThingQC(nowMemberID, interestID, city, sex, deleteFlag), new SortByDocument("UpdateTime", -1), pagesize, pageno);
        }
        /// <summary>
        /// 按某些条件放回内容集合的数量
        /// </summary>
        /// <param name="memberID">当前登录的用户编号</param>
        /// <param name="interestID">兴趣编号</param>
        /// <param name="city">创建者城市</param>
        /// <param name="sex">创建者性别</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns>内容集合的数量</returns>
        public static long findForSomeThingCount(String nowMemberID, String interestID, String city, String sex, Comm.DeletedFlag deleteFlag)
        {
            return getContentListCount(findForSomeThingQC(nowMemberID, interestID, city, sex, deleteFlag));
        }
        /// <summary>
        /// 按某些条件返回内容集合的查询条件
        /// </summary>
        /// <param name="memberID">当前登录的用户编号</param>
        /// <param name="interestID">兴趣编号</param>
        /// <param name="city">创建者城市</param>
        /// <param name="sex">创建者性别</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns></returns>
        public static QueryComplete findForSomeThingQC(String nowMemberID, String interestID, String city, String sex, Comm.DeletedFlag deleteFlag)
        {
            QueryComplete qc = null;
            if ((sex == null || sex == "") && (city == null || city == ""))
            {
                qc = Query.And(
                        Query.In("InterestID", interestID),
                        GetFindConditionToPermissions(nowMemberID, null, deleteFlag)
                        );
            }
            else if ((sex != null && sex != "") && (city == null || city == ""))
            {
                qc = Query.And(
                        Query.In("InterestID", interestID),
                        Query.EQ("Sex", int.Parse(sex)),
                        GetFindConditionToPermissions(nowMemberID, null, deleteFlag)
                        );
            }
            else if ((sex == null || sex == "") && (city != null && city != ""))
            {
                qc = Query.And(
                        Query.In("InterestID", interestID),
                        Query.EQ("City", city),
                        GetFindConditionToPermissions(nowMemberID, null, deleteFlag)
                        );
            }
            else
            {
                qc = Query.And(
                    Query.In("InterestID", interestID),
                    Query.EQ("City", city),
                    Query.EQ("Sex", int.Parse(sex)),
                    GetFindConditionToPermissions(nowMemberID, null, deleteFlag)
                    );
            }
            return qc;
        }

        /// <summary>
        /// 获取系统推荐的类容
        /// </summary>
        /// <param name="ids">编号组</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <param name="pageno">页数</param>
        /// <param name="pagesize">每页条数</param>
        /// <returns></returns>
        public static IList<PublicContent> findContentForSysLike(string[] ids, String contenttype, Comm.DeletedFlag deleteFlag, int pageno, int pagesize)
        {
            return getContentList(findContentForSysLikeQC(ids, contenttype, deleteFlag), new SortByDocument("UpdateTime", -1), pagesize, pageno);
        }
        /// <summary>
        /// 获取系统推荐的类容的数量
        /// </summary>
        /// <param name="ids">编号组</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns></returns>
        public static long findContentForSysLikeCount(string[] ids, String contenttype, Comm.DeletedFlag deleteFlag)
        {
            return getContentListCount(findContentForSysLikeQC(ids, contenttype, deleteFlag));
        }
        /// <summary>
        /// 获取系统推荐的类容的查询条件
        /// </summary>
        /// <param name="ids">编号组</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="deleteFlag">删除状态</param>
        /// <returns></returns>
        public static QueryComplete findContentForSysLikeQC(string[] ids, String contenttype, Comm.DeletedFlag deleteFlag)
        {
            List<ObjectId> IDs = new List<ObjectId>();
            foreach (string str in ids)
            {
                IDs.Add(ObjectId.Parse(str));
            }
            QueryComplete qc = Query.And(GetFindConditionToPermissions(null, contenttype, deleteFlag), Query.In("_id", new BsonArray(IDs.ToArray())));
            return qc;
        }

        /// <summary>
        /// 解析内容数据返回内容公共类集合
        /// </summary>
        /// <param name="bds">基础数据集</param>
        /// <returns>内容公共类集合</returns>
        public static IList<PublicContent> ContentResolve(MongoCursor<BsonDocument> bds)
        {
            IList<PublicContent> objlist = new List<PublicContent>();
            foreach (BsonDocument bd in bds)
            {
                ContentType contentType = (ContentType)Enum.Parse(typeof(ContentType), bd["ContentType"].AsInt32.ToString());
                switch (contentType)
                {
                    case ContentType.Interest:
                        InterestContent interest = new InterestContent();
                        interest = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<InterestContent>(bd.ToJson());
                        objlist.Add((PublicContent)interest);
                        break;
                    case ContentType.Member:
                        MemberContent member = new MemberContent();
                        member = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<MemberContent>(bd.ToJson());
                        objlist.Add((PublicContent)member);
                        break;
                    case ContentType.CallFor:
                        CallForContent callfar = new CallForContent();
                        callfar = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<CallForContent>(bd.ToJson());
                        objlist.Add((PublicContent)callfar);
                        break;
                    case ContentType.Image:
                        ImageContent image = new ImageContent();
                        image = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<ImageContent>(bd.ToJson());
                        objlist.Add((PublicContent)image);
                        break;
                    case ContentType.InterView:
                        InterViewContent interview = new InterViewContent();
                        interview = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<InterViewContent>(bd.ToJson());
                        objlist.Add((PublicContent)interview);
                        break;
                    case ContentType.IWant:
                        IWantContent iwant = new IWantContent();
                        iwant = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<IWantContent>(bd.ToJson());
                        objlist.Add((PublicContent)iwant);
                        break;
                    case ContentType.Mood:
                        MoodContent mood = new MoodContent();
                        mood = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<MoodContent>(bd.ToJson());
                        objlist.Add((PublicContent)mood);
                        break;
                    case ContentType.SuiSuiNian:
                        SuiSuiNianContent suisuinian = new SuiSuiNianContent();
                        suisuinian = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<SuiSuiNianContent>(bd.ToJson());
                        objlist.Add((PublicContent)suisuinian);
                        break;
                }
            }
            return objlist;
        }
        /// <summary>
        /// 根据内容编号获取内容的类型
        /// </summary>
        /// <param name="contentID">内容编号</param>
        /// <returns>内容类型</returns>
        public static ContentType GetContentType(String contentID)
        {
            try
            {
                QueryComplete qc = Query.EQ("_id", ObjectId.Parse(contentID));
                MongoCursor<BsonDocument> mc = MongoDBHelper.GetCursor<BsonDocument>(PublicContent.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1));
                ContentType type = 0;
                foreach (BsonDocument bd in mc)
                {
                    type = (ContentType)Enum.Parse(typeof(ContentType), bd["ContentType"].AsInt32.ToString());
                }
                return type;
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
        /// 获取权限查询的条件语句
        /// </summary>
        /// <param name="memberID">用户编号</param>
        /// <returns></returns>
        public static QueryComplete GetFindConditionToPermissions(String nowMemberID, String contenttype, Comm.DeletedFlag deleteFlag)
        {
            QueryComplete qc = null;
            if ((nowMemberID == null || nowMemberID == "") && (contenttype == null || contenttype == ""))
            {
                qc = Query.And(
                        Query.EQ("DeleteFlag", deleteFlag.GetHashCode()),
                        Query.EQ("ContentPermissions", ContentPermissions.AllOpen.GetHashCode())
                        );
            }
            else if ((nowMemberID != null && nowMemberID != "") && (contenttype == null || contenttype == ""))
            {
                qc = Query.And(
                        Query.EQ("DeleteFlag", deleteFlag.GetHashCode()),
                        Query.Or(
                            Query.EQ("ContentPermissions", ContentPermissions.AllOpen.GetHashCode()),
                            Query.EQ("MemberID", nowMemberID),
                            Query.And(
                                Query.EQ("ContentPermissions", ContentPermissions.MyFriend.GetHashCode()),
                                Query.In("MyFriends", nowMemberID)
                                )
                            )
                        );
            }
            else if ((nowMemberID == null || nowMemberID == "") && (contenttype != null && contenttype != ""))
            {
                qc = Query.And(
                        Query.EQ("ContentType", int.Parse(contenttype)),
                        Query.EQ("DeleteFlag", deleteFlag.GetHashCode()),
                        Query.EQ("ContentPermissions", ContentPermissions.AllOpen.GetHashCode())
                        );
            }
            else
            {
                qc = Query.And(
                        Query.EQ("ContentType", int.Parse(contenttype)),
                        Query.EQ("DeleteFlag", deleteFlag.GetHashCode()),
                        Query.Or(
                            Query.EQ("ContentPermissions", ContentPermissions.AllOpen.GetHashCode()),
                            Query.EQ("MemberID", nowMemberID),
                            Query.And(
                                Query.EQ("ContentPermissions", ContentPermissions.MyFriend.GetHashCode()),
                                Query.In("MyFriends", nowMemberID)
                                )
                            )
                        );
            }
            return qc;
        }

        /// <summary>
        /// 根据条件获取内容集合的公共方法
        /// </summary>
        /// <param name="qc">查询条件</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">页数</param>
        /// <returns></returns>
        public static IList<PublicContent> getContentList(QueryComplete qc, SortByDocument order, int pagesize, int pageno)
        {
            try
            {
                MongoCursor<BsonDocument> mc = MongoDBHelper.GetCursor<BsonDocument>(PublicContent.GetCollectionName(), qc, order, pageno, pagesize);
                new SortByDocument("UpdateTime", -1);
                return ContentResolve(mc);
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
        /// 根据条件获取内容数量的公共方法
        /// </summary>
        /// <param name="qc"></param>
        /// <returns></returns>
        public static long getContentListCount(QueryComplete qc)
        {
            try
            {
                return MongoDBHelper.GetCount(PublicContent.GetCollectionName(), qc);
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
        /// 根据访谈编号获取访谈内容
        /// </summary>
        /// <param name="interviewID">访谈</param>
        /// <param name="memberID">用户编号</param>
        /// <returns></returns>
        public static InterViewContent getInterviewContent(String interviewID, String memberID)
        {
            try
            {
                List<InterViewContent> ic = new List<InterViewContent>();
                QueryComplete qc = Query.And(Query.EQ("InterviewList._id", ObjectId.Parse(interviewID)), Query.EQ("InterviewList.MemberID", memberID), Query.EQ("DeleteFlag", Comm.DeletedFlag.No), Query.EQ("ContentType", ContentType.InterView));
                MongoCursor<InterViewContent> mc = MongoDBHelper.GetCursor<InterViewContent>(InterViewContent.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1), 0, 0);
                ic.AddRange(mc);
                return ic != null && ic.Count > 0 ? ic[0] : null;
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
        /// 根据评论编号获取内容
        /// </summary>
        /// <param name="commentID">评论编号</param>
        /// <param name="memberID">用户编号</param>
        /// <returns></returns>
        public static PublicContent getContentToComment(String commentID)
        {
            try
            {
                QueryComplete qc = Query.EQ("AnswerList._id", ObjectId.Parse(commentID));
                MongoCursor<BsonDocument> mc = MongoDBHelper.GetCursor<BsonDocument>(PublicContent.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1));
                string contentid = "";
                foreach (BsonDocument bd in mc)
                {
                    contentid = bd["_id"].AsObjectId.ToString();
                }
                return new PublicContent().getPublicContent(contentid);
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
        /// 获取用户最后发布的图片内容
        /// </summary>
        /// <param name="nowMemberID">用户编号</param>
        /// <returns></returns>
        public static ImageContent getLastImageForMember(String nowMemberID)
        {
            try
            {
                List<ImageContent> ic = new List<ImageContent>();
                QueryComplete qc = Query.And(Query.EQ("MemberID", nowMemberID), Query.EQ("DeleteFlag", Comm.DeletedFlag.No), Query.EQ("ContentType", ContentType.Image));
                MongoCursor<ImageContent> mc = MongoDBHelper.GetCursor<ImageContent>(ImageContent.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1), 0, 0);
                ic.AddRange(mc);
                return ic != null && ic.Count > 0 ? ic[0] : null;
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
        /// 获取用户最后发布的头像更新内容
        /// </summary>
        /// <param name="memberID">用户编号</param>
        /// <returns></returns>
        public static MemberContent getLastMemberForMember(String memberID)
        {
            try
            {
                List<MemberContent> ic = new List<MemberContent>();
                QueryComplete qc = Query.And(Query.EQ("MemberID", memberID), Query.EQ("DeleteFlag", Comm.DeletedFlag.No), Query.EQ("ContentType", ContentType.Member), Query.EQ("Type", CBB.ConfigurationHelper.AppSettingHelper.GetConfig("MemberType").Split('|')[1].Split(',')[1]));
                MongoCursor<MemberContent> mc = MongoDBHelper.GetCursor<MemberContent>(MemberContent.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1), 0, 0);
                ic.AddRange(mc);
                return ic != null && ic.Count > 0 ? ic[0] : null;
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
        /// 获取用户最后发布的访谈内容
        /// </summary>
        /// <param name="nowMemberID">用户编号</param>
        /// <returns></returns>
        public static InterViewContent getLastInterViewForMember(String nowMemberID)
        {
            try
            {
                List<InterViewContent> ic = new List<InterViewContent>();
                QueryComplete qc = Query.And(Query.EQ("MemberID", nowMemberID), Query.EQ("DeleteFlag", Comm.DeletedFlag.No), Query.EQ("ContentType", ContentType.InterView));
                MongoCursor<InterViewContent> mc = MongoDBHelper.GetCursor<InterViewContent>(InterViewContent.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1), 0, 0);
                ic.AddRange(mc);
                return ic != null && ic.Count > 0 ? ic[0] : null;
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
        /// 返回指定用户关注的人的编号集合
        /// </summary>
        /// <param name="memberID">用户的编号</param>
        /// <returns>好友编号集合</returns>
        public static IList<String> getMyFriends(String nowMemberID)
        {
            List<String> myfriends = new List<String>();
            //获取我关注的人
            IList<BiZ.Member.Relation.Favorer> favorerList = BiZ.Member.Relation.RelationProvider.GetFavorers(nowMemberID, 0, 0);
            //获取关注我的人
            //IList<BiZ.Member.Relation.Favorer> whofavorerList = BiZ.Member.Relation.RelationProvider.GetFavorers(nowMemberID, 0, 0);
            foreach (BiZ.Member.Relation.Favorer favorer1 in favorerList)
            {
                //foreach (BiZ.Member.Relation.Favorer favorer2 in whofavorerList)
                //{
                //    if (favorer1.ToMember == favorer2.FromMember)
                //    {
                        myfriends.Add(favorer1.ToMember);
                    //}
                //}
            }
            return myfriends;
        }
        /// <summary>
        /// 按用户编号和头像地址更新多条用户内容
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <param name="avatar">头像地址</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult UpdateMemberIdContentMemberAvatar(string memberId, string avatar, string oldavatar)
        {
            try
            {
                if (oldavatar == null) oldavatar = "";

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberContent> mc = md.GetCollection<MemberContent>(MemberContent.GetCollectionName());
                mc.Update(
                            Query.And(
                                Query.EQ("MemberID", memberId),
                                Query.EQ("Creater.ICONPath", oldavatar),
                                Query.EQ("ContentType", BiZ.Content.ContentType.Member)
                            ),
                            Update.Combine(
                                Update.Set("Creater.ICONPath", avatar),
                                Update.Set("MemberAvatar", avatar)
                            ),
                            UpdateFlags.Multi,
                            SafeMode.True
                        );
                mc.Update(
                            Query.And(
                                Query.EQ("MemberID", memberId),
                                Query.EQ("Creater.ICONPath", oldavatar)
                            ),
                            Update.Set("Creater.ICONPath", avatar),
                            UpdateFlags.Multi,
                            SafeMode.True
                        );
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
        public static CBB.ExceptionHelper.OperationResult RemoveContentWhereHasImage(string memberId, string oldimgpath)
        {
            try
            {
                if (oldimgpath == null) oldimgpath = "";

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberContent> mc = md.GetCollection<MemberContent>(MemberContent.GetCollectionName());
                mc.Remove(
                            Query.And(
                                Query.EQ("MemberID", memberId),
                                Query.EQ("ImageList.ImageUrl", oldimgpath),
                                Query.In("ContentType", new BsonValue[] { BiZ.Content.ContentType.Image, BiZ.Content.ContentType.SuiSuiNian, BiZ.Content.ContentType.CallFor })
                            )
                        );
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
