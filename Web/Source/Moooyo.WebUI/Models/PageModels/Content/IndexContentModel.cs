using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels.Content
{
    public class IndexContentModel : MemberPageModel
    {
        public IList<BiZ.Content.PublicContent> ContentList;
        public IList<BiZ.InterestCenter.Interest> interestList;
        public IList<BiZ.Content.PublicContent> imageContentList;
        public IList<string> ActivityCityList;
        public BiZ.TopImagePush.ImagePush imagePush;
        public string[] cityList;
        public long contentCount;
        public int smailPagesize;
        public int smailPageno;
        public int bigPagesize;
        public int bigPageno;
        public Common.Content.TypeNameAndLikeNameModel namesModel;
        public IndexContentModel() { }
    }
}
