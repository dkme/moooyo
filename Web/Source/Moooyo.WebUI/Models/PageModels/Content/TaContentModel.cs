using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels.Content
{
    public class TaContentModel : MemberPageModel
    {
        public IList<BiZ.Content.PublicContent> TaContentList;
        public IList<BiZ.InterestCenter.Interest> interestList;
        public Boolean isfans;
        public String userEncodeID;
        public long contentCount;
        public Common.Content.TypeNameAndLikeNameModel namesModel;
        public TaContentModel() { }
    }
}