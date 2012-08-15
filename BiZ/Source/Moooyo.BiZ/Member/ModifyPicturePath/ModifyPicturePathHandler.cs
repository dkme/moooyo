using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Member.ModifyPicturePath
{
    /// <summary>
    /// 修改图片路径提供者
    /// </summary>
    public class ModifyPicturePathHandler
    {
        public string MemberId { get; set; } //用户编号
        public string PictureId { get; set; } //图片编号
        public string PicturePath { get; set; } //图片路径
        public string OldPicturePath { get; set; } //是否删除所有图片地址

        //声明委托
        public delegate void ModifyPicPathEventHandler(Object sender, ModifyPicPathEventArgs e);
        //声明修改头像事件
        public event ModifyPicPathEventHandler ModifyPicPathEvent; 

        /// <summary>
        /// 传递给Observer所感兴趣的信息
        /// </summary>
        public class ModifyPicPathEventArgs : EventArgs
        {
            public ModifyPicPathEventArgs()
            {
            }
        }

        protected virtual void OnModifyPicPath(ModifyPicPathEventArgs e)
        {
            if (ModifyPicPathEvent != null) //如果有对象注册
            {
                ModifyPicPathEvent(this, e); //调用所有注册对象的方法
            }
        }

        /// <summary>
        /// 修改图片路径
        /// </summary>
        /// <param name="pictureType"></param>
        /// <param name="picturePathStr"></param>
        public void ModifyPicPath()
        {
            //代码...//
            //建立ModifyPicPathEventArgs对象
            ModifyPicPathEventArgs e = new ModifyPicPathEventArgs();
            OnModifyPicPath(e); //调用OnModifyPicPath方法
        }
    }
}
