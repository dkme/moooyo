using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 我关注的人页面数据对象
    /// </summary>
    public class MemberRelationsModel : PageModels.MemberProfileModel
    {
        public IList<Models.RelationDisplayObj> relationObjs;
        public bool isFavor = false;

        public MemberRelationsModel() { }
        public MemberRelationsModel(
            IList<Models.RelationDisplayObj> relationObjs)
        {
            this.relationObjs = relationObjs;
        }
        public MemberRelationsModel(MemberFullDisplayObj memberFullDisplayObj,
            IList<Models.RelationDisplayObj> relationObjs)
        {
            this.Member = memberFullDisplayObj;
            this.relationObjs = relationObjs;
        }
    }
}