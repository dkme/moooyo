///
/// 功能描述：图片对象
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
    public class Image
    {
        private String imageUrl;//图片地址
        public String ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }
        public Image() { }
        public Image(String ImageUrl)
        {
            this.ImageUrl = ImageUrl;
        }
    }
}
