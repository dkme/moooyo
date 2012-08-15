///
/// 功能描述：活动列表的页面数据模型
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/4/9
/// 附加信息：
///   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    public class ActivesModel : PageModelBase
    {
        public IList<Active.ActiveReflection> actives;
        public ActivesModel(IList<Active.ActiveReflection> actives)
        {
            this.actives = actives;
        }
    }
}