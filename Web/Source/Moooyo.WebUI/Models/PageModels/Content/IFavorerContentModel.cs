using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels.Content
{
    public class IFavorerContentModel : MemberPageModel
    {
        public IList<BiZ.Content.PublicContent> ContentList;
        public IList<BiZ.Creater.Creater> favorerMemberList;
        public long contentCount;
        public Common.Content.TypeNameAndLikeNameModel namesModel;
        public IFavorerContentModel() { }
    }
}