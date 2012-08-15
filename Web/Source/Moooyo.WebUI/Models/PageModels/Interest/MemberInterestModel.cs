using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 用于个人兴趣显示页面的对象
    /// View/Member/Interest.aspx
    /// </summary>
    public class MemberInterestModel : MemberPageModel
    {
        public IList<BiZ.InterestCenter.Interest> interestListObj,interestlist;
        public BiZ.InterestCenter.Interest interestObj, memberInterestObj;
        public IList<BiZ.InterestCenter.InterestClass> interestClassListObje;
        public IList<BiZ.InterestCenter.InterestFans> interestFansListObje, interestHtxualFansListObje, interestHotFans;
        public Dictionary<BiZ.WenWen.WenWen, IList<BiZ.WenWen.WenWenAnswer>> wenwens;//问问集合
        public long interestCount, allInterestCount, allInterestFansCount, memberInterestCount, interestClassCount, classInterestCount,interestFansCount, interestHtxualFansCount;
        public bool isFans;
        public String type;
        public IList<BiZ.WenWen.WenWen> wenwenlist;
        public BiZ.WenWen.WenWen wenwenobj;
        public BiZ.WenWen.WenWenAnswer answerobj;
        public IList<BiZ.WenWen.WenWenAnswer> answerlist;
        public int wenwencount,answercount,pagesize,pagecount,pageno;
        public bool ifmembertofans;
        public Boolean iffliter;
        public List<MemberInterestModel> answerlisttomodel;
        public BiZ.Member.Member wwmemberobj;
        public IList<BiZ.Member.Member> memberList;

        public MemberInterestModel() { }
        public MemberInterestModel(
            IList<BiZ.WenWen.WenWen> wenwenlist, 
            IList<BiZ.InterestCenter.Interest> interestlist, 
            long interestCount, 
            int wenwencount, 
            int pagecount, 
            int pageno)
        {
            this.wenwenlist = wenwenlist;
            this.interestlist = interestlist;
            this.interestCount = interestCount;
            this.wenwencount = wenwencount;
            this.pagecount = pagecount;
            this.pageno = pageno;
        }

        public MemberInterestModel(MemberFullDisplayObj memberFullDisplayObj)
        {
            this.Member = memberFullDisplayObj;
        }
        public MemberInterestModel(MemberFullDisplayObj memberFullDisplayObj, IList<BiZ.InterestCenter.Interest> interestListObj)
        {
            this.Member = memberFullDisplayObj;
            this.interestListObj = interestListObj;
        }
        public MemberInterestModel(MemberFullDisplayObj memberFullDisplayObj, IList<BiZ.InterestCenter.Interest> interestListObj, String type)
        {
            this.Member = memberFullDisplayObj;
            this.interestListObj = interestListObj;
            this.type = type;
        }
        public MemberInterestModel(BiZ.InterestCenter.Interest interestObj)
        {
            this.interestObj = interestObj;
        }
        public MemberInterestModel(Dictionary<BiZ.WenWen.WenWen, IList<BiZ.WenWen.WenWenAnswer>> wenwens)
        {
            this.wenwens = wenwens;
        }
        public MemberInterestModel(MemberFullDisplayObj memberFullDisplayObj, BiZ.InterestCenter.Interest interestObj)
        {
            this.Member = memberFullDisplayObj;
            this.interestObj = interestObj;
        }
        public MemberInterestModel(MemberFullDisplayObj memberFullDisplayObj, IList<BiZ.InterestCenter.InterestClass> interestClassListObje)
        {
            this.Member = memberFullDisplayObj;
            this.interestClassListObje = interestClassListObje;
        }
        public MemberInterestModel(MemberFullDisplayObj memberFullDisplayObj, BiZ.InterestCenter.Interest interestObj, IList<BiZ.InterestCenter.InterestFans> interestFansListObje)
        {
            this.Member = memberFullDisplayObj;
            this.interestObj = interestObj;
            this.interestFansListObje = interestFansListObje;
        }
        public MemberInterestModel(
            MemberFullDisplayObj memberFullDisplayObj, 
            BiZ.InterestCenter.Interest interestObj,
            IList<BiZ.InterestCenter.InterestFans> interestHtxualFansListObje,
            IList<BiZ.WenWen.WenWen> wenwenlist)
        {
            this.Member = memberFullDisplayObj;
            this.interestObj = interestObj;
            this.interestHtxualFansListObje = interestHtxualFansListObje;
            this.wenwenlist = wenwenlist;
        }
        public MemberInterestModel(
            MemberFullDisplayObj memberFullDisplayObj,
            IList<BiZ.InterestCenter.Interest> interestListObj,
            IList<BiZ.InterestCenter.InterestClass> interestClassListObje)
        {
            this.Member = memberFullDisplayObj;
            this.interestListObj = interestListObj;
            this.interestClassListObje = interestClassListObje;
        }
        public MemberInterestModel(
            MemberFullDisplayObj memberFullDisplayObj,
            BiZ.InterestCenter.Interest interestObj,
            IList<BiZ.InterestCenter.InterestClass> interestClassListObje)
        {
            this.Member = memberFullDisplayObj;
            this.interestObj = interestObj;
            this.interestClassListObje = interestClassListObje;
        }
        public MemberInterestModel(
            MemberFullDisplayObj memberFullDisplayObj,
            IList<BiZ.InterestCenter.Interest> interestListObj,
            IList<BiZ.InterestCenter.InterestFans> interestFansListObje)
        {
            this.Member = memberFullDisplayObj;
            this.interestListObj = interestListObj;
            this.interestFansListObje = interestFansListObje;
        }
    }
}