/*******************************************************************
 * Functional description ：用户皮肤数据提供者
 * Author：Lau Tao
 * Modify the expansion：Lau Tao
 * Modified date：2012/5/31 Thursday 
 * Remarks：
 * ****************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Member.MemberSkin
{
    /// <summary>
    /// 用户皮肤数据提供者
    /// </summary>
    public class MemberSkinProvider
    {
        /// <summary>
        /// 添加用户皮肤
        /// </summary>
        /// <param name="memberId">创建者编号</param>
        /// <param name="userType">用户类型</param>
        /// <param name="personalityPicture">个性图片</param>
        /// <param name="personalityBackgroundPicture">个性背景图片</param>
        /// <returns></returns>
        public static MemberSkin AddMemberSkin(
            String memberId,
            Comm.UserType userType, 
            String personalityPicture,
            String personalityBackgroundPicture)
        {
            //创建用户皮肤
            MemberSkin obj = CreateMemberSkin(memberId, userType, personalityPicture, personalityBackgroundPicture);
            if (obj.ID != "")
            {
                ////增加动态
                //BiZ.Member.Activity.ActivityController.AddActivity(
                //    memberId,
                //    BiZ.Member.Activity.ActivityType.AddInterest,
                //    BiZ.Member.Activity.ActivityController.GetActivityContent_AddInterest_Title(),
                //    BiZ.Member.Activity.ActivityController.GetActivityContent_AddInterest(obj.ID, title, content, iconpath),
                //    false);

                return obj;
            }
            else
            {
                //保存兴趣错误
                return null;
            }
        }
        /// <summary>
        /// 创建用户皮肤
        /// </summary>
        /// <param name="memberId">创建者</param>
        /// <param name="userType">创建者类别</param>
        /// <param name="personalityPicture">个性图片</param>
        /// <param name="personalityBackgroundPicture">个性背景图片</param>
        /// <returns>用户皮肤对象</returns>
        private static MemberSkin CreateMemberSkin(
            String memberId,
            Comm.UserType userType,
            String personalityPicture,
            String personalityBackgroundPicture)
        {
            MemberSkin obj = new MemberSkin();
            obj.Creater = memberId;
            obj.UserType = userType;
            obj.PersonalityPicture = personalityPicture;
            obj.PersonalityBackgroundPicture = personalityBackgroundPicture;
            obj.CreatedTime = DateTime.Now;
            obj.Random = new Random().NextDouble();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberSkin> mc = md.GetCollection<MemberSkin>(MemberSkin.GetCollectionName());
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
        /// <summary>
        /// 删除用户皮肤
        /// </summary>
        /// <param name="memberSkinId">用户皮肤编号</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult DeleteMemberSkin(String memberSkinId)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberSkin> mc = md.GetCollection<MemberSkin>(MemberSkin.GetCollectionName());

                mc.Remove(Query.EQ("_id", ObjectId.Parse(memberSkinId)));

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
        /// 更新用户皮肤
        /// </summary>
        /// <param name="memberSkinId">用户皮肤编号</param>
        /// <param name="memberId">创建者编号</param>
        /// <param name="personalityPicture">个性图片</param>
        /// <param name="personalityBackgroundPicture">个性背景图片</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult UpdateMemberSkin(
            String memberSkinId, 
            String memberId, 
            String personalityPicture,
            String personalityBackgroundPicture)
        {
            MemberSkin ms = new MemberSkin();
            QueryComplete qc = null;
            IMongoUpdate mu = null;
            try
            {
                //用户皮肤编号和创建者编号编号可以为null或空字符串，在为null或空字符串情况下过滤查询条件
                if ((memberSkinId != null && memberSkinId != "") && (memberId != null && memberId != ""))
                {
                    qc = Query.And(
                        Query.EQ("_id", ObjectId.Parse(memberSkinId)), 
                        Query.EQ("Creater", memberId)
                        );
                }
                else if ((memberSkinId != null && memberSkinId != "") && (memberId == null || memberId == ""))
                {
                    qc = Query.EQ("_id", ObjectId.Parse(memberSkinId));
                }
                else if ((memberSkinId == null || memberSkinId == "") && (memberId != null && memberId != ""))
                {
                    qc = Query.EQ("Creater", memberId);
                }

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberSkin> mc = md.GetCollection<MemberSkin>(MemberSkin.GetCollectionName());

                //个性图片和个性背景图片可以为null或空字符串，在为null或空字符串情况下过滤查询条件
                if ((personalityPicture != null && personalityPicture != "") && (personalityBackgroundPicture != null && personalityBackgroundPicture != ""))
                {
                    mu = Update.Combine(
                        Update.Set("PersonalityPicture", personalityPicture), 
                        Update.Set("PersonalityBackgroundPicture", personalityBackgroundPicture)
                        );
                }
                else if ((personalityPicture != null && personalityPicture != "") && (personalityBackgroundPicture == null || personalityBackgroundPicture == ""))
                {
                    mu = Update.Set("PersonalityPicture", personalityPicture);
                }
                else if ((personalityPicture == null || personalityPicture == "") && (personalityBackgroundPicture != null && personalityBackgroundPicture != ""))
                {
                    mu = Update.Set("PersonalityBackgroundPicture", personalityBackgroundPicture);
                }

                mc.Update(qc, mu);

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
        /// 分页获取所有用户皮肤
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>用户皮肤集合</returns>
        public static IList<MemberSkin> GetMemberSkins(int pageSize, int pageNo)
        {
            try
            {
                QueryComplete qc = null;
                MongoCursor<MemberSkin> mc = MongoDBHelper.GetCursor<MemberSkin>(MemberSkin.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1), pageNo, pageSize);
                List<MemberSkin> objs = new List<MemberSkin>();
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
        /// <summary>
        /// 按用户编号分页获取所有用户皮肤
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>用户皮肤集合</returns>
        public static IList<MemberSkin> GetMemberSkins(string memberId, int pageSize, int pageNo)
        {
            try
            {
                QueryComplete qc = null;
                if (memberId != null && memberId != "")
                {
                    qc = Query.Or(
                        Query.EQ("Creater", memberId), 
                        Query.EQ("UserType", Comm.UserType.Administrator)
                        );
                }
                MongoCursor<MemberSkin> mc = MongoDBHelper.GetCursor<MemberSkin>(
                    MemberSkin.GetCollectionName(), 
                    qc, 
                    new SortByDocument("CreatedTime", -1), 
                    pageNo, 
                    pageSize
                    );
                List<MemberSkin> objs = new List<MemberSkin>();
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
        /// <summary>
        /// 按用户编号和图片类型分页获取所有用户皮肤
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>用户皮肤集合</returns>
        public static IList<MemberSkin> GetMemberSkins(string memberId, int pageSize, int pageNo, string pictureType)
        {
            try
            {
                QueryComplete qc = null;
                if (memberId != null && memberId != "")
                {
                    if (pictureType == "PersonalityPicture")
                    {
                        qc = Query.And(
                                Query.Or(
                                    Query.EQ("Creater", memberId),
                                    Query.EQ("UserType", Comm.UserType.Administrator)
                                ),
                                Query.NE("PersonalityPicture", "")
                            );
                    }
                    else if (pictureType == "PersonalityBackgroundPicture")
                    {
                        qc = Query.And(
                                Query.Or(
                                    Query.EQ("Creater", memberId),
                                    Query.EQ("UserType", Comm.UserType.Administrator)
                                ),
                                Query.NE("PersonalityBackgroundPicture", "")
                            );
                    }
                }
                else
                {
                    if (pictureType == "PersonalityPicture")
                    {
                        qc = Query.Or(
                            Query.EQ("UserType", Comm.UserType.Administrator),
                            Query.NE("PersonalityPicture", "")
                        );
                    }
                    else if (pictureType == "PersonalityBackgroundPicture")
                    {
                        qc = Query.Or(
                            Query.EQ("UserType", Comm.UserType.Administrator),
                            Query.NE("PersonalityBackgroundPicture", "")
                        );
                    }
                }
                MongoCursor<MemberSkin> mc = MongoDBHelper.GetCursor<MemberSkin>(
                    MemberSkin.GetCollectionName(),
                    qc,
                    new SortByDocument("CreatedTime", -1),
                    pageNo,
                    pageSize
                    );
                List<MemberSkin> objs = new List<MemberSkin>();
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
        /// <summary>
        /// 获取用户皮肤总数
        /// </summary>
        /// <returns>用户皮肤集合</returns>
        public static long GetMemberSkinCount()
        {
            try
            {
                long memberSkinCount;
                memberSkinCount = MongoDBHelper.GetCount(MemberSkin.GetCollectionName());

                return memberSkinCount;
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
        /// 按用户编号获取用户皮肤总数
        /// </summary>
        /// <returns>用户皮肤集合</returns>
        public static long GetMemberSkinCount(string memberId, string pictureType)
        {
            QueryComplete qc = null;
            try
            {
                if (memberId != null && memberId != "")
                {
                    if (pictureType == "PersonalityPicture")
                    {
                        qc = Query.And(
                                Query.Or(
                                    Query.EQ("Creater", memberId),
                                    Query.EQ("UserType", Comm.UserType.Administrator)
                                    ), 
                                    Query.NE("PersonalityPicture", "")
                                );
                    }
                    else if (pictureType == "PersonalityBackgroundPicture")
                    {
                        qc = Query.And(
                                Query.Or(
                                    Query.EQ("Creater", memberId),
                                    Query.EQ("UserType", Comm.UserType.Administrator)
                                    ),
                                    Query.NE("PersonalityBackgroundPicture", "")
                                );
                    }
                }
                
                long memberSkinCount;
                memberSkinCount = MongoDBHelper.GetCount(MemberSkin.GetCollectionName(), qc);

                return memberSkinCount;
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
        /// 按指定条数随机数升序获取多条用户皮肤
        /// </summary>
        /// <param name="count">指定条数</param>
        /// <returns>用户皮肤列表</returns>
        public static IList<MemberSkin> RandomGetMemberSkins(int count)
        {
            try
            {
                List<MemberSkin> objs = new List<MemberSkin>();
                //随机取count个记录
                double random = new Random(DateTime.Now.Second).NextDouble();
                objs = GetMemberSkins(count, Query.GTE("Random", random));
                if (objs.Count < count)
                {
                    objs.AddRange(GetMemberSkins(count, Query.LTE("Random", random)));
                }
                return objs.Take(count).ToList();
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
        /// 按指定条数和查询对象随机数升序获取多条用户皮肤
        /// </summary>
        /// <param name="count">指定条数</param>
        /// <param name="qc">查询对象</param>
        /// <returns>用户皮肤列表</returns>
        private static List<MemberSkin> GetMemberSkins(int count, QueryConditionList qc)
        {
            MongoCursor<MemberSkin> mc = MongoDBHelper.GetCursor<MemberSkin>(
                MemberSkin.GetCollectionName(),
                qc,
                new SortByDocument("Random", 1),
                1,
                count);

            List<MemberSkin> objs = new List<MemberSkin>();
            objs.AddRange(mc);
            return objs;
        }
        /// <summary>
        /// 在数据库中Member表设置用户皮肤字段
        /// </summary>
        /// <param name="memberSkinId">用户皮肤编号</param>
        /// <param name="setType">设置皮肤类型</param>
        /// <param name="memberId">用户编号</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult SetMemberSkin(String memberSkinId, String setType, String memberId)
        {
            //按用户皮肤编号获取用户皮肤对象
            MemberSkin ms = GetMemberSkin(memberSkinId, null);
            //按用户编号获取用户对象
            Member member = MemberManager.MemberManager.GetMember(memberId);
            if (member.MemberInfomation.MemberSkin == null)
            {
                member.MemberInfomation.MemberSkin = new MemberSkin();
            }
            member.MemberInfomation.MemberSkin._id = ms._id;
            member.MemberInfomation.MemberSkin.Creater = ms.Creater;
            member.MemberInfomation.MemberSkin.UserType = ms.UserType;
            switch(setType) 
            {
                case "PersonalityPicture":
                    member.MemberInfomation.MemberSkin.PersonalityPicture = ms.PersonalityPicture;
                    break;
                case "PersonalityBackgroundPicture":
                    member.MemberInfomation.MemberSkin.PersonalityBackgroundPicture = ms.PersonalityBackgroundPicture;
                    break;
            }
            member.MemberInfomation.MemberSkin.CreatedTime = ms.CreatedTime;
            member.MemberInfomation.MemberSkin.Random = ms.Random;

            //保存用户对象
            MemberManager.MemberManager.SaveMember(member);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        /// <summary>
        /// 按用户皮肤编号或创建者编号获取一条用户皮肤
        /// </summary>
        /// <param name="memberSkinId">用户皮肤编号</param>
        /// <param name="memberId">创建者用户编号</param>
        /// <returns>用户皮肤</returns>
        public static MemberSkin GetMemberSkin(String memberSkinId, String memberId)
        {
            MemberSkin ms = new MemberSkin();
            QueryComplete qc = null;
            try
            {
                //用户皮肤编号和创建者编号编号可以为null或空字符串，在为null或空字符串情况下过滤查询条件
                if ((memberSkinId != null && memberSkinId != "") && (memberId != null && memberId != ""))
                {
                    qc = Query.And(
                        Query.EQ("_id", ObjectId.Parse(memberSkinId)), 
                        Query.EQ("Creater", memberId)
                        );
                }
                else if ((memberSkinId != null && memberSkinId != "") && (memberId == null || memberId == ""))
                {
                    qc = Query.EQ("_id", ObjectId.Parse(memberSkinId));
                }
                else if ((memberSkinId == null || memberSkinId == "") && (memberId != null && memberId != ""))
                {
                    qc = Query.EQ("Creater", memberId);
                }

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberSkin> mc = md.GetCollection<MemberSkin>(MemberSkin.GetCollectionName());
                ms = mc.FindOne(qc);
                return ms;
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
        /// 按用户皮肤编号或创建者编号获取一条个性背景或图片为空的用户皮肤
        /// </summary>
        /// <param name="memberSkinId">用户皮肤编号</param>
        /// <param name="memberId">创建者用户编号</param>
        /// <returns>用户皮肤</returns>
        public static MemberSkin GetEmptyMemberSkin(String memberSkinId, String memberId)
        {
            MemberSkin ms = new MemberSkin();
            QueryComplete qc = null;
            try
            {
                //用户皮肤编号和创建者编号编号可以为null或空字符串，在为null或空字符串情况下过滤查询条件
                if ((memberSkinId != null && memberSkinId != "") && (memberId != null && memberId != ""))
                {
                    qc = Query.And(
                        Query.EQ("_id", ObjectId.Parse(memberSkinId)),
                        Query.EQ("Creater", memberId),
                        Query.Or(
                            Query.EQ("PersonalityPicture", ""),
                            Query.EQ("PersonalityPicture", "null"),
                            Query.EQ("PersonalityBackgroundPicture", ""),
                            Query.EQ("PersonalityBackgroundPicture", "null")
                            )
                        );
                }
                else if ((memberSkinId != null && memberSkinId != "") && (memberId == null || memberId == ""))
                {
                    qc = Query.And(
                            Query.EQ("_id", ObjectId.Parse(memberSkinId)),
                            Query.Or(
                                Query.EQ("PersonalityPicture", ""),
                                Query.EQ("PersonalityPicture", "null"),
                                Query.EQ("PersonalityBackgroundPicture", ""),
                                Query.EQ("PersonalityBackgroundPicture", "null")
                            )
                        );
                }
                else if ((memberSkinId == null || memberSkinId == "") && (memberId != null && memberId != ""))
                {
                    qc = Query.And(
                        Query.EQ("Creater", memberId),
                        Query.Or(
                            Query.EQ("PersonalityPicture", ""),
                            Query.EQ("PersonalityPicture", "null"),
                            Query.EQ("PersonalityBackgroundPicture", ""),
                            Query.EQ("PersonalityBackgroundPicture", "null")
                            )
                        );
                }

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberSkin> mc = md.GetCollection<MemberSkin>(MemberSkin.GetCollectionName());
                ms = mc.FindOne(qc);
                return ms;
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
        /// 按创建者用户编号或个性图片或个性背景获取一条个性背景或图片为空的用户皮肤
        /// </summary>
        /// <param name="memberId">创建者用户编号</param>
        /// <param name="personalityPicture">个性图片</param>
        /// <param name="personalityBackgroundPicture">个性背景</param>
        /// <returns>用户皮肤</returns>
        public static MemberSkin GetEmptyMemberSkin2(String memberId, String personalityPicture, String personalityBackgroundPicture)
        {
            MemberSkin ms = new MemberSkin();
            QueryComplete qc = null;
            try
            {
                //用户皮肤编号和创建者编号编号可以为null或空字符串，在为null或空字符串情况下过滤查询条件
                if ((personalityPicture == null || personalityPicture == "") && (personalityBackgroundPicture == null || personalityBackgroundPicture == ""))
                {
                    qc = Query.And(
                        Query.EQ("Creater", memberId),
                        Query.Or(
                            Query.EQ("PersonalityPicture", ""), Query.EQ("PersonalityPicture", "null")
                        ),
                        Query.Or(
                            Query.EQ("PersonalityBackgroundPicture", ""), Query.EQ("PersonalityBackgroundPicture", "null")
                        )
                        );
                }
                else if ((personalityPicture == null || personalityPicture == "") && (personalityBackgroundPicture != null || personalityBackgroundPicture != ""))
                {
                    qc = Query.And(
                        Query.EQ("Creater", memberId),
                        Query.Or(
                            Query.EQ("PersonalityPicture", ""), Query.EQ("PersonalityPicture", "null")
                        )
                        );
                }
                else if ((personalityPicture != null || personalityPicture != "") && (personalityBackgroundPicture == null || personalityBackgroundPicture == ""))
                {
                    qc = Query.And(
                        Query.EQ("Creater", memberId),
                        Query.Or(
                            Query.EQ("PersonalityBackgroundPicture", ""), Query.EQ("PersonalityBackgroundPicture", "null")
                        )
                        );
                }

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<MemberSkin> mc = md.GetCollection<MemberSkin>(MemberSkin.GetCollectionName());
                ms = mc.FindOne(qc);
                return ms;
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
