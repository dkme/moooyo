///
/// 功能描述：注册页面兴趣列表的页面数据模型
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/3/19
/// 附加信息：
///   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    public class InterestForRegModel:PageModels.PageModelBase
    {
        public IList<BiZ.InterestCenter.Interest> interests;
        public int pageno;
        public IList<BiZ.InterestCenter.InterestClass> interestclass;

        public InterestForRegModel(IList<BiZ.InterestCenter.Interest> interests)
        {
            this.interests = interests;
        }
        public InterestForRegModel(IList<BiZ.InterestCenter.Interest> interests, int pageno, IList<BiZ.InterestCenter.InterestClass> interestclass)
        {
            this.interests = interests;
            this.pageno = pageno;
            this.interestclass = interestclass;
        }
    }
}