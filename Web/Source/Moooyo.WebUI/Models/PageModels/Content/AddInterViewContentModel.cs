using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels.Content
{
    public class AddInterViewContentModel:MemberPageModel
    {
        public IList<BiZ.InterestCenter.Interest> interestList;
        public IList<BiZ.InterView.InterView> myinterviewList;
        public IList<BiZ.Sys.SystemInterView> sysinterviewList;
        public AddInterViewContentModel() { }
    }
}