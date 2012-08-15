/*************************************************
 * Functional description ：魅力值数据提供类
 * Author：Lau Tao
 * Modify the expansion：Lau Tao
 * Modified date：2012/3/24 Saturday   
 * Remarks：
 * *********************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moooyo.BiZ.Member;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Member.GlamourCounts
{
    /// <summary>
    /// 魅力值数据提供类
    /// </summary>
    public class GlamourCountProvider
    {
        /// <summary>
        /// 按用户编号获取所有关注指定用户的用户的用户名和魅力值列表排名（按魅力值降序）
        /// </summary>
        /// <param name="toMember">指定用户的编号</param>
        /// <param name="count">取记录数</param>
        /// <returns>用户编号与相对的魅力值的键值对列表</returns>
        public static Dictionary<String, float> GetFavoreMeMembersGlamourCountRanking(String toMember)
        {
            //按用户编号获取所有关注指定用户的用户的用户名和魅力值列表
            Dictionary<String, float> memberGlamourCounts = GetMembersGlamourCount(toMember);
            //或取降序排序后的用户编号和魅力值键值对列表
            Dictionary<String, float> sortedMemberGlamourCounts = GetSortedMemberGlamourCounts(memberGlamourCounts);
            return sortedMemberGlamourCounts;
        }

        /// <summary>
        /// 获取降序排序后的用户编号和魅力值键值对列表
        /// </summary>
        /// <param name="memberGlamourCounts">用户编号和魅力值键值对列表</param>
        /// <returns>降序排序后的用户编号和魅力值键值对列表</returns>
        public static Dictionary<String, float> GetSortedMemberGlamourCounts(Dictionary<String, float> memberGlamourCounts)
        {
            //按用户的魅力值排序
            var qGlamourCounts = (from glamCount in memberGlamourCounts orderby glamCount.Value descending select glamCount);

            //按键值对取出排序后的用户和魅力值
            Dictionary<String, float> sortedMemberGlamourCounts = new Dictionary<String, float>();
            foreach (var glamCout in qGlamourCounts)
            {
                sortedMemberGlamourCounts.Add(glamCout.Key, glamCout.Value);
            }
            return sortedMemberGlamourCounts;
        }

        /// <summary>
        /// 按用户编号获取所有关注指定用户的用户的用户名和魅力值列表
        /// </summary>
        /// <param name="toMember">指定用户的编号</param>
        /// <returns>所有关注指定用户的用户的魅力值</returns>
        public static Dictionary<String, float> GetMembersGlamourCount(String toMember)
        {
            //获取所有关注指定用户的用户
            IList<Relation.Favorer> favorerList = Relation.RelationProvider.GetListWhoFavoredMe(toMember, 1, 0);
            List<String> fromMemberList = new List<String>();
            foreach (Relation.Favorer favorer in favorerList)
            {
                fromMemberList.Add(favorer.FromMember);
            }

            //获取这些用户的魅力值
            List<GlamourCounts> glamCountList = new List<GlamourCounts>();
            MongoDatabase mgDb = MongoDBHelper.MongoDB;
            MongoCollection<GlamourCounts> mgColect = mgDb.GetCollection<GlamourCounts>(GlamourCounts.CollectionName());
            MongoCursor<GlamourCounts> mgCurs = mgColect.Find(Query.In("ToMember", new BsonArray(fromMemberList.ToArray()))).SetSortOrder(new SortByDocument("Value", -1));
            //获取多条按用户分组累加组的魅力值
            Dictionary<String, float> glamourCountMemberGroupAccumulate = GetGlamourCountToMemberGroupAccumulate(mgCurs);

            //按键值对取出用户和魅力值
            Dictionary<String, float> memberGlamourCounts = new Dictionary<String, float>();
            foreach (var glamCout in glamourCountMemberGroupAccumulate)
            {
                memberGlamourCounts.Add(glamCout.Key, glamCout.Value);
            }
            return memberGlamourCounts;
        }

        /// <summary>
        /// 按用户编号获取一条用户为指定用户贡献了多少分魅力值
        /// </summary>
        /// <param name="fromMember">指定用户的编号</param>
        /// <param name="toMember">对方的用户编号</param>
        /// <returns>为指定用户贡献了多少分魅力值</returns>
        public static float GetMemberForTaContributionGlamourCount(String fromMember, String toMember)
        {
            //获取用户为指定用户贡献了的魅力值列表
            List<GlamourCounts> glamCountList = new List<GlamourCounts>();
            MongoDatabase mgDb = MongoDBHelper.MongoDB;
            MongoCollection<GlamourCounts> mgColect = mgDb.GetCollection<GlamourCounts>(GlamourCounts.CollectionName());
            MongoCursor<GlamourCounts> mgCurs = mgColect.Find(Query.And(Query.EQ("FromMember", fromMember), Query.EQ("ToMember", toMember)));
            //获取多条按用户分组累加组的魅力值
            Dictionary<String, float> glamourCountMemberGroupAccumulate = GetGlamourCountToMemberGroupAccumulate(mgCurs);

            //取出用户的魅力值
            IList<float> memberGlamourCounts = new List<float>();
            foreach (var glamCout in glamourCountMemberGroupAccumulate.Values)
            {
                memberGlamourCounts.Add(glamCout);
            }
            if (memberGlamourCounts == null || memberGlamourCounts.Count <= 0) { return 0; }
            return memberGlamourCounts[0];
        }

        /// <summary>
        /// 按用户编号获取所有关注指定用户的用户为指定用户贡献了的魅力值列表排名（按魅力值降序）
        /// </summary>
        /// <param name="toMember">指定用户的用户编号</param>
        /// <returns>指定用户的粉丝的用户编号和对应编号的键值对列表</returns>
        public static Dictionary<String, float> GetMembersForTaContributionGlamourCount(String toMember)
        {
            //获取所有关注指定用户的用户
            IList<Relation.Favorer> favorerList = Relation.RelationProvider.GetListWhoFavoredMe(toMember, 1, 0);
            List<String> fromMemberList = new List<String>();
            foreach (Relation.Favorer favorer in favorerList)
            {
                fromMemberList.Add(favorer.FromMember);
            }

            //获取这些用户的魅力值
            List<GlamourCounts> glamCountList = new List<GlamourCounts>();
            MongoDatabase mgDb = MongoDBHelper.MongoDB;
            MongoCollection<GlamourCounts> mgColect = mgDb.GetCollection<GlamourCounts>(GlamourCounts.CollectionName());
            MongoCursor<GlamourCounts> mgCurs = mgColect.Find(Query.And(Query.In("FromMember", new BsonArray(fromMemberList.ToArray())), Query.EQ("ToMember", toMember))).SetSortOrder(new SortByDocument("Value", -1)); //降序
            //获取多条按用户分组累加组的魅力值
            Dictionary<String, float> glamourCountMemberGroupAccumulate = GetGlamourCountFromMemberGroupAccumulate(mgCurs);

            //获取降序排序后的用户编号和魅力值键值对列表
            Dictionary<String, float> sortedMemberGlamourCounts = GetSortedMemberGlamourCounts(glamourCountMemberGroupAccumulate);
            return sortedMemberGlamourCounts;
        }

        /// <summary>
        /// 获取多条按To用户分组累加组的魅力值
        /// </summary>
        /// <param name="mgCurs">Mongo数据集游标</param>
        /// <returns>用户编号和魅力值键值对列表</returns>
        public static Dictionary<String, float> GetGlamourCountToMemberGroupAccumulate(MongoCursor<GlamourCounts> mgCurs)
        {
            //按用户分组累加组的魅力值
            var qGlamourCounts = from glamCount in mgCurs
                group glamCount by glamCount.ToMember into toMemberList
                select new
                {
                    toMemberList.Key,
                    memberGlamourCount = toMemberList.Sum(glamCount => glamCount.Value)
                };
            //按键值对取出用户和魅力值
            Dictionary<String, float> memberGlamourCounts = new Dictionary<String, float>();
            foreach (var glamCout in qGlamourCounts)
            {
                memberGlamourCounts.Add(glamCout.Key, glamCout.memberGlamourCount);
            }
            return memberGlamourCounts;
        }

        /// <summary>
        /// 获取多条按From用户分组累加组的魅力值
        /// </summary>
        /// <param name="mgCurs">Mongo数据集游标</param>
        /// <returns>用户编号和魅力值键值对列表</returns>
        public static Dictionary<String, float> GetGlamourCountFromMemberGroupAccumulate(MongoCursor<GlamourCounts> mgCurs)
        {
            //按用户分组累加组的魅力值
            var qGlamourCounts = from glamCount in mgCurs
                                 group glamCount by glamCount.FromMember into fromMemberList
                                 select new
                                 {
                                     fromMemberList.Key,
                                     memberGlamourCount = fromMemberList.Sum(glamCount => glamCount.Value)
                                 };
            //按键值对取出用户和魅力值
            Dictionary<String, float> memberGlamourCounts = new Dictionary<String, float>();
            foreach (var glamCout in qGlamourCounts)
            {
                memberGlamourCounts.Add(glamCout.Key, glamCout.memberGlamourCount);
            }
            return memberGlamourCounts;
        }

        /// <summary>
        /// 获取用户在指定用户的粉丝中排名(为指定用户贡献的魅力值降序)
        /// </summary>
        /// <param name="toMember">关注指定用户的用户</param>
        /// <param name="fromMember">指定用户</param>
        /// <returns>在TA的粉丝中排名</returns>
        public static long GetIInTaFansRank(String toMember, String fromMember)
        { 
            // 按用户编号获取所有关注指定用户的用户的用户名和魅力值列表排名（按魅力值降序）
            Dictionary<String, float> favoreMeMembersGlamourCountRanking = GetMembersForTaContributionGlamourCount(toMember);

            //按用户的魅力值排序
            long rankFlag = 0;
            foreach (String member in favoreMeMembersGlamourCountRanking.Keys)
            {
                rankFlag++;
                if (member == fromMember) return rankFlag;
            }

            return rankFlag;
        }
    }
}
