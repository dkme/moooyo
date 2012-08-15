using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 用于兴趣推送列表显示控件的页面数据模型
    /// </summary>
    public class InterestListControlModel : Moooyo.WebUI.Models.PageModels.PageModelBase
    {
        public IList<BiZ.InterestCenter.Interest> interestListObj;

        public InterestListControlModel(IList<BiZ.InterestCenter.Interest> interestListObj)
        {
            this.interestListObj = interestListObj;
        }
    }
}