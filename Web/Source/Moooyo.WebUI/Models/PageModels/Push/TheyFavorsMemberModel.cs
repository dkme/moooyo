///
/// 功能描述：喜欢TA的人还喜欢（推人）控件页面数据模型
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/2/21
/// 附加信息：
///   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 喜欢TA的人还喜欢（推人）控件页面数据模型
    /// </summary>
    public class TheyFavorsMemberModel : Models.PageModels.PageModelBase
    {
        public IList<BiZ.Member.Member> memberList;
        public TheyFavorsMemberModel(IList<BiZ.Member.Member> memberList)
        {
            this.memberList = memberList;
        }
    }
}