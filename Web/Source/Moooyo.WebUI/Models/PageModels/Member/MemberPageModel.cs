using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    //用户页面基础类
    public class MemberPageModel : PageModelBase
    {
        public MemberFullDisplayObj Member;

        public MemberPageModel() { }
        public MemberPageModel(MemberFullDisplayObj memberFullDisplayObj)
        {
            this.Member = memberFullDisplayObj;
        }
    }
}