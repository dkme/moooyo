/*******************************************************************
 * Functional description ：邀请码数据提供类
 * Author：Lau Tao
 * Modify the expansion：Tao Lau 
 * Modified date：2012/7/3 Tuesday 
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

namespace Moooyo.BiZ.Sys.InvitationCode
{
    public class InvitationCodeProvider
    {
        /// <summary>
        /// 按生成数和参考数生成不重复的数值数组
        /// </summary>
        /// <param name="median">生成数</param>
        /// <param name="reference">参考数</param>
        /// <returns>不重复的数值数组</returns>
        public static int[] GenerateNumber3(int median, int reference)
        {
            //用于存放1到33这33个数  
            int[] container = new int[reference];
            //用于保存返回结果  
            int[] result = new int[median];
            Random random = new Random();
            for (int i = 1; i <= reference; i++)
            {
                container[i - 1] = i;
            }
            int index = 0;
            int value = 0;
            for (int i = 0; i < median; i++)
            {
                //从[1,container.Count + 1)中取一个随机值，保证这个值不会超过container的元素个数  
                index = random.Next(1, container.Length - 1 - i);
                //以随机生成的值作为索引取container中的值  
                value = container[index];
                //将随机取得值的放到结果集合中  
                result[i] = value;
                //将刚刚使用到的从容器集合中移到末尾去  
                container[index] = container[container.Length - i - 1];
                //将队列对应的值移到队列中  
                container[container.Length - i - 1] = value;
            }
            //result.Sort();排序  
            return result;
        }  
        /// <summary>
        /// 生成邀请码
        /// </summary>
        /// <param name="count">生成数量</param>
        /// <param name="generatedMember">操作用户</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult GenerateInviteCodes(short count, string generatedMember, string generatedMemberId)
        {
            try
            {
                //int rndint = 0;
                //long tick = 0;

                if(count > 100) return new CBB.ExceptionHelper.OperationResult(false, "最多只能生成100个邀请码");
          
                //for(byte i = 0; i < count; i++)
                //{
                    //tick = DateTime.Now.Ticks;
                    //Random rnd = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                    //rndint = rnd.Next(0, 999999);

                    int[] arrInviteCodes = GenerateNumber3(count, 999999);

                    for (short i = 0; i < arrInviteCodes.Length; i++)
                    {

                        InvitationCode inviteCode = new InvitationCode(arrInviteCodes[i], null, generatedMember, generatedMemberId, Comm.UsedFlag.No);
                        Add(inviteCode);
                    }
                //}

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
        /// 用户生成邀请码
        /// </summary>
        /// <param name="generatedMember">用户编号</param>
        /// <returns></returns>
        public static InvitationCode GenerateInviteCode(string generatedMember)
        {
            try
            {
                int[] arrInviteCodes = GenerateNumber3(1, 999999);
                InvitationCode inviteCode = new InvitationCode(arrInviteCodes[0], null, generatedMember, generatedMember, Comm.UsedFlag.No);
                return Save(inviteCode);
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
        /// 获取多条邀请码
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>邀请码对象列表</returns>
        public static IList<InvitationCode> GetInviteCodes(int pageSize, int pageNo, Comm.UsedFlag usedFlag = Comm.UsedFlag.Unknown)
        {
            QueryComplete qc = null;
            try
            {
                if (usedFlag != Comm.UsedFlag.Unknown)
                    qc = Query.EQ("UsedFlag", usedFlag);
                MongoCursor<InvitationCode> mc = MongoDBHelper.GetCursor<InvitationCode>(
                    InvitationCode.GetCollectionName(),
                    qc,
                    new SortByDocument("CreatedTime", -1),
                    pageNo,
                    pageSize);
                List<InvitationCode> objs = new List<InvitationCode>();
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
        /// 根据生成者的编号获取最新的邀请码对象
        /// </summary>
        /// <param name="GeneratedMemberID">生成者的编号</param>
        /// <returns></returns>
        public static InvitationCode GetInviteCodeToGeneratedMember(String GeneratedMemberID)
        {
            try
            {
                MongoCursor<InvitationCode> mc = MongoDBHelper.GetCursor<InvitationCode>(
                    InvitationCode.GetCollectionName(),
                    Query.EQ("GeneratedMember.MemberID", GeneratedMemberID),
                    new SortByDocument("CreatedTime", -1), 0, 0);
                List<InvitationCode> objs = new List<InvitationCode>();
                objs.AddRange(mc);
                return objs.Count > 0 ? objs[0] : null;
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
        /// 获取所有邀请码总数
        /// </summary>
        /// <returns>邀请码总数</returns>
        public static int GetInviteCodesCount(Comm.UsedFlag usedFlag = Comm.UsedFlag.Unknown)
        {
            QueryComplete qc = null;
            try
            {
                if (usedFlag != Comm.UsedFlag.Unknown)
                    qc = Query.EQ("UsedFlag", usedFlag);
                MongoDatabase md = MongoDBHelper.MongoDB;
                int count = (int)MongoDBHelper.GetCount(InvitationCode.GetCollectionName(), qc);
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
        /// 按邀请码获取一条邀请码对象
        /// </summary>
        /// <param name="inviteCode">邀请码</param>
        /// <returns>邀请码对象</returns>
        public static InvitationCode GetInviteCode(int inviteCode)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InvitationCode> mc = md.GetCollection<InvitationCode>(InvitationCode.GetCollectionName());
                InvitationCode obj = mc.FindOne(Query.EQ("InviteCode", inviteCode));
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
        /// 按邀请码和使用状态获取一条邀请码对象
        /// </summary>
        /// <param name="inviteCode">邀请码</param>
        /// <returns>邀请码对象</returns>
        public static InvitationCode GetInviteCode(int inviteCode, Comm.UsedFlag usedFlag)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InvitationCode> mc = md.GetCollection<InvitationCode>(InvitationCode.GetCollectionName());
                InvitationCode obj = mc.FindOne(Query.And(Query.EQ("InviteCode", inviteCode), Query.EQ("UsedFlag", usedFlag)));
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
        /// 按用户动态编号和使用标记分页获取多条邀请码
        /// </summary>
        /// <param name="generatedMemberId">用户动态编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNo">当前页</param>
        /// <returns>邀请码对象列表</returns>
        public static IList<InvitationCode> GetInviteCodes(
            string generatedMemberId, 
            int pageSize, 
            int pageNo, 
            Comm.UsedFlag usedFlag = Comm.UsedFlag.Unknown)
        {
            QueryComplete qc = null;
            try
            {
                qc = Query.EQ("GeneratedMemberId", generatedMemberId);
                if (usedFlag != Comm.UsedFlag.Unknown)
                {
                    qc = Query.And(
                        Query.EQ("GeneratedMemberId", generatedMemberId),
                        Query.EQ("UsedFlag", usedFlag)
                        );
                }
                MongoCursor<InvitationCode> mc = MongoDBHelper.GetCursor<InvitationCode>(
                    InvitationCode.GetCollectionName(),
                    qc,
                    new SortByDocument("CreatedTime", -1),
                    pageNo,
                    pageSize);
                List<InvitationCode> objs = new List<InvitationCode>();
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
        /// 更新邀请码
        /// </summary>
        /// <param name="obj">邀请码对象</param>
        /// <returns>邀请码对象</returns>
        public static InvitationCode Save(InvitationCode obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InvitationCode> mc = md.GetCollection<InvitationCode>(InvitationCode.GetCollectionName());
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
        /// 添加邀请码
        /// </summary>
        /// <param name="obj">邀请码对象</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult Add(InvitationCode obj)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<InvitationCode> mc = md.GetCollection<InvitationCode>(InvitationCode.GetCollectionName());
                mc.Insert(obj);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (Exception excep)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   excep);
            }
        }
    }
}
