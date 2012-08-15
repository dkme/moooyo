///
/// 功能描述：内容访问权限，枚举
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/5/23
/// 附加信息：
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Content
{
    public enum ContentPermissions
    {
        AllOpen = 0,//对所有人公开
        MyFriend = 1,//仅对好友公开
        AllClose = 2//对所有人不公开
    }
}
