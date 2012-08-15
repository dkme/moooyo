///
/// 功能描述：与我兴趣相同的控件的页面数据模型
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/2/20
/// 附加信息：
///   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 与我兴趣相同的控件的页面数据模型
    /// </summary>
    public class InterestOverMeMemberModel : Moooyo.WebUI.Models.PageModels.PageModelBase
    {
        public IList<BiZ.Member.Relation.Favorer> funslist;
        public Dictionary<BiZ.Member.Member,IList<BiZ.InterestCenter.Interest>> memberList;//与我兴趣相同的人
        //public IList<BiZ.InterestCenter.Interest> interestList;//我喜欢的兴趣
        public InterestOverMeMemberModel(Dictionary<BiZ.Member.Member, IList<BiZ.InterestCenter.Interest>> memberList, IList<BiZ.Member.Relation.Favorer> funslist)
        {
            this.memberList = memberList;
            this.funslist = funslist;
        }
    }
}