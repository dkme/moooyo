///
/// 功能描述：我的兴趣话题的页面数据模型
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/3/24
/// 附加信息：
///   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    public class MyInterestToWenWenModel:Models.PageModels.PageModelBase
    {
        public IList<BiZ.InterestCenter.Interest> interests;//有话题的兴趣
        public IList<BiZ.InterestCenter.Interest> myinterest;//我加粉的兴趣
        public IList<IList<BiZ.WenWen.WenWen>> wenwens;
        public IList<BiZ.WenWen.WenWen> wenwenlist;
        public int interestcount;
        public int interestcounttowenwens;
        public int interestpagesize;
        public int interestpageno;
        public int interestpagecount;
        public MyInterestToWenWenModel() { }
    }
}