///
/// 功能描述：顶部图片推送的显示次数和点击次数的封装类
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/5/19
/// 附加信息：
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.TopImagePush
{
    public class ImagePushCount
    {
        private long showCount;//显示次数
        public long ShowCount
        {
            get { return showCount; }
            set { showCount = value; }
        }
        private long clickCount;//点击次数
        public long ClickCount
        {
            get { return clickCount; }
            set { clickCount = value; }
        }
        public ImagePushCount() { }
        public ImagePushCount(long ShowCount, long ClickCount)
        {
            this.showCount = ShowCount;
            this.ClickCount = ClickCount;
        }
    }
}
