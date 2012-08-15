using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels.Content
{
    public class AddContentModel:MemberPageModel
    {
        public IList<BiZ.InterestCenter.Interest> interestList;
        public BiZ.Content.PublicContent contentObj;
        public List<String> type;
        public AddContentModel() { }
    }
}