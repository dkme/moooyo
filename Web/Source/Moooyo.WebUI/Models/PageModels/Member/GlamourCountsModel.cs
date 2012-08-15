using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    //魅力值相关实体
    public class GlamourCountsModel : PageModels.MemberPageModel
    {
        public IList<String> memberGlamourCountListObj;
        public Dictionary<BiZ.Member.Member, IList<BiZ.InterestCenter.Interest>> memberInterestListObj;
        public String skin;

        public GlamourCountsModel(Models.MemberFullDisplayObj memberDisplayObj, String skin, IList<String> memberGlamourCountListObj, Dictionary<BiZ.Member.Member, IList<BiZ.InterestCenter.Interest>> memberInterestListObj)
        {
            this.Member = memberDisplayObj;
            this.skin = skin;
            this.memberGlamourCountListObj = memberGlamourCountListObj;
            this.memberInterestListObj = memberInterestListObj;
        }
    }
}