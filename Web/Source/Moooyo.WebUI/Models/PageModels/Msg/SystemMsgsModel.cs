using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 系统消息页面数据对象
    /// </summary>
    public class SystemMsgsModel : MemberPageModel
    {
        public IList<BiZ.Sys.SystemMsg.SystemMsg> systemMsglist;

        public SystemMsgsModel(MemberFullDisplayObj memberFullDisplayObj,
            IList<BiZ.Sys.SystemMsg.SystemMsg> systemMsglist)
        {
            this.Member = memberFullDisplayObj;
            this.systemMsglist = systemMsglist;
        }

    }
}