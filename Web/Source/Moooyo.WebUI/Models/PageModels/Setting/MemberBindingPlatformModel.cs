using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 用户设置－微博绑定
    /// </summary>
    public class MemberBindingPlatformModel : MemberPageModel
    {
        public IList<BiZ.Member.Connector.Connector> connectors;
        public MemberBindingPlatformModel(MemberFullDisplayObj memberFullDisplayObj,
            IList<BiZ.Member.Connector.Connector> connectors)
        {
            this.Member = memberFullDisplayObj;
            this.connectors = connectors;
        }

        public bool IsBinded(BiZ.Member.Connector.Platform platform)
        {
            foreach (BiZ.Member.Connector.Connector obj in connectors)
            {
                if (obj == null) continue;
                if (obj.PlatformType == platform && obj.Enable)
                    return true;
            }

            return false;
        }
    }
}