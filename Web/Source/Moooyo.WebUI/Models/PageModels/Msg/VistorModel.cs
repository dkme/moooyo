using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 用于来访者页面的数据对象
    /// </summary>
    public class VistorModel : MemberPageModel
    {
        public IList<Models.RelationDisplayObj> vistorlist;

        public VistorModel(MemberFullDisplayObj memberFullDisplayObj,
         IList<Models.RelationDisplayObj> vistorlist) 
        {
            this.Member = memberFullDisplayObj;
            this.vistorlist = vistorlist;
        }
    }
}