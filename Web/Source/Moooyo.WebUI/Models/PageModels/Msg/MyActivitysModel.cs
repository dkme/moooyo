using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 关于我的动态数据对象
    /// </summary>
    public class MyActivitysModel : MemberPageModel
    {
        public IList<BiZ.Member.Activity.ActivityHolderRelatedToMe> activityHolderListObje;
        public IList<Models.RelationDisplayObj> relationObjs;
        public long pageTotal;
        //IList<String> aboutMeActivityStrList;

        public MyActivitysModel(
            MemberFullDisplayObj memberFullDisplayObj,
            IList<BiZ.Member.Activity.ActivityHolderRelatedToMe> activityHolderListObje)
        {
            this.Member = memberFullDisplayObj;
            this.activityHolderListObje = activityHolderListObje;
        }
        public MyActivitysModel(
            MemberFullDisplayObj memberFullDisplayObj, 
            IList<BiZ.Member.Activity.ActivityHolderRelatedToMe> activityHolderListObje,
            IList<Models.RelationDisplayObj> relationObjs)
        {
            this.Member = memberFullDisplayObj;
            this.activityHolderListObje = activityHolderListObje;
            this.relationObjs = relationObjs;
        }
    }
}