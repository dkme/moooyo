using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels.Content
{
    public class IContentModel : MemberPageModel
    {
        public IList<BiZ.Content.PublicContent> MyContentList;
        public long contentCount;
        public Common.Content.TypeNameAndLikeNameModel namesModel;
        public IContentModel() { }
    }
}