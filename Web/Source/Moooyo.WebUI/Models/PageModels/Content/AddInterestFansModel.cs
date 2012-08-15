using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels.Content
{
    public class AddInterestFansModel : MemberPageModel
    {
        public IList<BiZ.InterestCenter.Interest> interestList;
        public IList<IList<BiZ.InterestCenter.Interest>> interestLists;
        public IList<BiZ.InterestCenter.Interest> interestListTo;
        public AddInterestFansModel() { }
    }
}