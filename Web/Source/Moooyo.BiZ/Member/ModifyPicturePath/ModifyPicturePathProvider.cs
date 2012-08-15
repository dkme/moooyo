using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Member.ModifyPicturePath
{
    /// <summary>
    /// 修改图片路径提供者
    /// </summary>
    public class ModifyPicturePathProvider
    {
        public void ModifyMemberAvatarPicPath(Object sender, ModifyPicturePathHandler.ModifyPicPathEventArgs e)
        {
            ModifyPicturePathHandler modiPicPathHand = (ModifyPicturePathHandler)sender;

            CBB.ExceptionHelper.OperationResult result = null;

            //将图片设置为用户头像
            //后台审核删除图片时，PictureId为空
            if (modiPicPathHand.PictureId!="")
                result = BiZ.MemberManager.MemberManager.SetMemberIconPhoto(modiPicPathHand.MemberId, modiPicPathHand.PictureId);
            else
                result = BiZ.MemberManager.MemberManager.SetMemberIconPhotoAuditNotPass(modiPicPathHand.MemberId, modiPicPathHand.OldPicturePath, modiPicPathHand.PicturePath);

            //如果视频认证为通过则改为不通过
            result = MemberManager.MemberManager.SetPhotoIdentNotPass(modiPicPathHand.MemberId);
            //在内容里更新所有我的用户头像
            BiZ.Content.ContentProvider.UpdateMemberIdContentMemberAvatar(modiPicPathHand.MemberId, modiPicPathHand.PicturePath,modiPicPathHand.OldPicturePath);
            //在兴趣粉丝里更新所有我的用户头像
            BiZ.InterestCenter.InterestFactory.UpdateMemberIdInterestFansAvatar(modiPicPathHand.MemberId, modiPicPathHand.PicturePath);
        }

        public void ModifyInterestPicPath(Object sender, ModifyPicturePathHandler.ModifyPicPathEventArgs e)
        {
            ModifyPicturePathHandler modiPicPathHand = (ModifyPicturePathHandler)sender;
        }

        public void ModifyContentPicPath(Object sender, ModifyPicturePathHandler.ModifyPicPathEventArgs e)
        {
            ModifyPicturePathHandler modiPicPathHand = (ModifyPicturePathHandler)sender;
        }
    }
}
