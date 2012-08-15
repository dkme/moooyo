///
/// 功能描述：兴趣粉丝自定义控件的页面数据对象
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
    public class InterestFansModel : PageModels.PageModelBase
    {
        public IList<BiZ.InterestCenter.InterestFans> fans;
        public long fanscount;
        public InterestFansModel(IList<BiZ.InterestCenter.InterestFans> fans, long fanscount)
        {
            this.fans = fans;
            this.fanscount = fanscount;
        }
    }
}