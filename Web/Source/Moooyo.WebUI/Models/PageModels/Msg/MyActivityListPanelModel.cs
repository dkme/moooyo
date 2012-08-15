using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 与我相关列表数据模型
    /// </summary>
    public class MyActivityListPanelModel : Moooyo.WebUI.Models.PageModels.PageModelBase
    {
        public IList<Models.RelationDisplayObj> relationObjList;
        public IList<BiZ.Member.Activity.ActivityHolderRelatedToMe> activityHolderList;
        public IList<String> aboutMeActivityStrList;

        public MyActivityListPanelModel(
            IList<BiZ.Member.Activity.ActivityHolderRelatedToMe> activityHolderList,
            IList<Models.RelationDisplayObj> relationObjList)
        {
            this.activityHolderList = activityHolderList;
            this.relationObjList = relationObjList;
        }
        public MyActivityListPanelModel(
            IList<BiZ.Member.Activity.ActivityHolderRelatedToMe> activityHolderList,
            IList<Models.RelationDisplayObj> relationObjList,
            IList<String> aboutMeActivityStrList)
        {
            this.activityHolderList = activityHolderList;
            this.relationObjList = relationObjList;
            this.aboutMeActivityStrList = aboutMeActivityStrList;
        }
    }
}