using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels.Content
{
    public class ContentDetailModel : MemberPageModel
    {
        public BiZ.Content.PublicContent contentobj;
        public String contenttype;
        public String typename;
        public String likename;
        public Boolean ifshowmember;
        public Boolean ifmy;
        public ImageLayOutTypeModel imageLayOutModel;
        public ContentDetailModel() { }
    }
}