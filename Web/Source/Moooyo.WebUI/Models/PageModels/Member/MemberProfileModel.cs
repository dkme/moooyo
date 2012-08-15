using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    //用户页面类
    public class MemberProfileModel : MemberPageModel
    {
        public IList<BiZ.Member.Activity.ActivityHolder> activityHolderListObje;
        public IList<BiZ.InterestCenter.Interest> interestListObje, commonInterestListObje;
        public IList<BiZ.Photo.Photo> photoListObje;
        public IList<BiZ.InterView.InterView> interViewListObje;
        public IList<BiZ.Member.Relation.Visitor> visitorListObje;
        public String MemberUrl, memberForTaContributionGlamourCount, iInTaFansRank, photoType;
        public String interestIds;

        public MemberProfileModel() { }
        public MemberProfileModel(MemberFullDisplayObj memberFullDisplayObj) {
            this.Member = memberFullDisplayObj;
        }
        public MemberProfileModel(
            MemberFullDisplayObj memberFullDisplayObj,
            IList<BiZ.InterestCenter.Interest> interestListObje)
        {
            this.Member = memberFullDisplayObj;
            this.interestListObje = interestListObje;
        }
        public MemberProfileModel(MemberFullDisplayObj memberFullDisplayObj, IList<BiZ.Member.Activity.ActivityHolder> activityHolderListObje, IList<BiZ.Member.Relation.Visitor> visitorListObje)
        {
            this.Member = memberFullDisplayObj;
            this.activityHolderListObje = activityHolderListObje;
            this.visitorListObje = visitorListObje;
        }
        public MemberProfileModel(
            MemberFullDisplayObj memberFullDisplayObj, IList<BiZ.InterestCenter.Interest> interestListObje, IList<BiZ.Photo.Photo> photoListObje, IList<BiZ.InterView.InterView> interViewListObje, IList<BiZ.InterestCenter.Interest> commonInterestListObje)
        {
            this.Member = memberFullDisplayObj;
            this.interestListObje = interestListObje;
            this.photoListObje = photoListObje;
            this.interViewListObje = interViewListObje;
            this.commonInterestListObje = commonInterestListObje;
        }
    }
}