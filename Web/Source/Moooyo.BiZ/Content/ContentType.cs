///
/// 功能描述：内容类型，枚举
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/5/19
/// 附加信息：
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Content
{
    public enum ContentType
    {
        Image = 0,//图片内容
        SuiSuiNian = 1,//碎碎念内容
        IWant = 2,//我想内容
        Mood = 3,//心情内容
        InterView = 4,//访谈内容
        CallFor = 5,//号召内容
        Interest = 6,//兴趣操作内容
        Member = 7//用户操作内容
    }
}
