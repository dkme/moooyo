using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Photo
{
    /// <summary>
    /// 照片类别
    /// </summary>
    public enum PhotoType
    {
        #region 用户照片类别，0到10，（比如，生活照、艺术照，等等）
        /// <summary>
        /// 用户图片
        /// </summary>
        MemberPhoto = 0,
        /// <summary>
        /// 用户头像
        /// </summary>
        MemberAvatar = 1,
        #endregion

        /// <summary>
        /// 用户皮肤个性图片
        /// </summary>
        MemberSkinPersonalityPicture = 11, //用户皮肤个性图片
        /// <summary>
        /// 用户皮肤个性背景图片
        /// </summary>
        MemberSkinPersonalityBackgroundPicture = 12, //用户皮肤个性背景图片
        /// <summary>
        /// 兴趣图标
        /// </summary>
        InterestICON = 30,
        InterestSelfhoodPicture = 31, //兴趣个性图片
        /// <summary>
        /// 认证图片
        /// </summary>
        IdentificationPhoto = 98,
        /// <summary>
        /// 注册图片
        /// </summary>
        RegisterPhoto = 99,
        /// <summary>
        /// 话题图片
        /// </summary>
        TopicImage = 100,
        /// <summary>
        /// 其他图片
        /// </summary>
        OtherPhoto = 200,
        #region 内容图片
        ImageContentPhoto = 201,
        SuoSuoContentPhoto = 202,
        CallForContentPhoto = 203,
        IWantContentPhoto = 204,
        /// <summary>
        /// 网站首页图片
        /// </summary>
        WebsiteHome = 205
        #endregion
    }
}
