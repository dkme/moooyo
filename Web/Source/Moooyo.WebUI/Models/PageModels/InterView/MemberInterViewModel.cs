using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 用于访谈的页面数据对象
    /// </summary>
    public class MemberInterViewModel : MemberPageModel
    {
        public IList<BiZ.InterView.InterView> interviewlist;
        public IList<BiZ.Sys.SystemInterView> systeminterviewlist;

        public MemberInterViewModel(MemberFullDisplayObj memberFullDisplayObj, 
            IList<BiZ.InterView.InterView> interviewlist,
            IList<BiZ.Sys.SystemInterView> systeminterviewlist)
        {
            this.Member = memberFullDisplayObj;
            this.interviewlist = interviewlist;
            this.systeminterviewlist = systeminterviewlist;
        }
    }
}