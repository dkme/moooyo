///
/// 功能描述：问问发布加粉兴趣列表的页面数据模型
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/2/27
/// 附加信息：
///   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    public class InterestWenWenModel:Models.PageModels.PageModelBase
    {
        public int wenwencount;//用户的问问数
        public int wenwenanswercount;//用户回复的问问数
        public BiZ.InterestCenter.Interest interest;//用户加粉的单个兴趣
        public BiZ.WenWen.WenWen wenwen;//问问
        public IList<BiZ.InterestCenter.Interest> interests;//用户加粉的兴趣集合
        public IList<BiZ.WenWen.WenWenAnswer> answerlist;//问问答案集合
        public IList<BiZ.WenWen.WenWen> wenwens;//问问集合
        public IList<BiZ.InterestCenter.Interest> interestTowenwen;//问问对应的兴趣对象集合
        public IList<BiZ.WenWen.WenWen> wenwenlist;
        public IList<int> wenwenanswerlist,wenwencounts;
        public IList<BiZ.InterestCenter.Interest> wenwenlistinterest;
        public int wenwenpagecount;
        public int answercount;
        public int answerpagecount;
        public int answernextpageno;
        public Boolean iffliter;
        public InterestWenWenModel() { }
        public InterestWenWenModel(IList<BiZ.WenWen.WenWen> wenwens)
        {
            this.wenwens = wenwens;
        }

        public InterestWenWenModel(IList<BiZ.InterestCenter.Interest> interestTowenwen)
        {
            this.interestTowenwen = interestTowenwen;
        }

        public InterestWenWenModel(BiZ.WenWen.WenWen wenwen, BiZ.InterestCenter.Interest interest)
        {
            this.wenwen = wenwen;
            this.interest = interest;
        }

        public InterestWenWenModel(
            IList<BiZ.WenWen.WenWen> wenwens,
            IList<BiZ.InterestCenter.Interest> interestTowenwen,
            IList<BiZ.WenWen.WenWen> wenwenlist,
            IList<int> wenwenanswerlist,
            IList<BiZ.InterestCenter.Interest> wenwenlistinterest)
        {
            this.wenwens = wenwens;
            this.interestTowenwen = interestTowenwen;
            this.wenwenlist = wenwenlist;
            this.wenwenanswerlist = wenwenanswerlist;
            this.wenwenlistinterest = wenwenlistinterest;
        }

        public InterestWenWenModel(
            IList<BiZ.WenWen.WenWen> wenwenlist,
            IList<int> wenwenanswerlist,
            IList<BiZ.InterestCenter.Interest> wenwenlistinterest)
        {
            this.wenwenlist = wenwenlist;
            this.wenwenanswerlist = wenwenanswerlist;
            this.wenwenlistinterest = wenwenlistinterest;
        }

        public InterestWenWenModel(
            IList<BiZ.WenWen.WenWen> wenwens,
            IList<BiZ.InterestCenter.Interest> interestTowenwen)
        {
            this.wenwens = wenwens;
            this.interestTowenwen = interestTowenwen;
        }

        public InterestWenWenModel(
            IList<BiZ.WenWen.WenWen> wenwens,
            IList<BiZ.InterestCenter.Interest> interestTowenwen,
            int wenwenpagecount)
        {
            this.wenwens = wenwens;
            this.interestTowenwen = interestTowenwen;
            this.wenwenpagecount=wenwenpagecount;
        }

        public InterestWenWenModel(
            int wenwencount,
            int wenwenanswercount,
            IList<BiZ.InterestCenter.Interest> interests)
        {
            this.wenwencount = wenwencount;
            this.wenwenanswercount = wenwenanswercount;
            this.interests = interests;
        }

        public InterestWenWenModel(
            int wenwencount,
            int wenwenanswercount,
            BiZ.InterestCenter.Interest interest)
        {
            this.wenwencount = wenwencount;
            this.wenwenanswercount = wenwenanswercount;
            this.interest = interest;
        }

        public InterestWenWenModel(
            IList<BiZ.WenWen.WenWenAnswer> answerlist)
        {
            this.answerlist = answerlist;
        }
        public InterestWenWenModel(
            int answercount,
            int answerpagecount,
            int answernextpageno,
            IList<BiZ.WenWen.WenWenAnswer> answerlist)
        {
            this.answercount = answercount;
            this.answerpagecount = answerpagecount;
            this.answernextpageno = answernextpageno;
            this.answerlist = answerlist;
        }
    }
}