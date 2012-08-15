///
/// 功能描述：我的兴趣自定义控件的页面数据对象
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/3/20
/// 附加信息：
///  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    public class MyInterestModel : PageModels.PageModelBase
    {
        public IList<BiZ.InterestCenter.Interest> interests;
        public long interestcount;
        public MyInterestModel(IList<BiZ.InterestCenter.Interest> interests, long interestcount)
        {
            this.interests = interests;
            this.interestcount = interestcount;
        }
    }
}