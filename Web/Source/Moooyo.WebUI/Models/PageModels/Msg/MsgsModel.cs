using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 消息页面的数据对象
    /// </summary>
    public class MsgsModel : MemberPageModel
    {
        //对方
        public MemberFullDisplayObj You;
        //普通用户最大每天主动联系数量
        public int NomalLevelMaxSendMsgLimit;
        public long pageTotal;

        public MsgsModel(MemberFullDisplayObj memberFullDisplayObj,
            MemberFullDisplayObj you)
        {
            this.Member = memberFullDisplayObj;
            this.You = you;
            this.NomalLevelMaxSendMsgLimit = 0;
            Int32.TryParse(
                CBB.ConfigurationHelper.AppSettingHelper.GetConfig("NomalLevelMaxSendMsgLimit"),
                out this.NomalLevelMaxSendMsgLimit);
        }
    }
}