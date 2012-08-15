/**
 * Functional description ：用户动态类别数据
 * Author：Tao Lau
 * Modify the expansion：Tao Lau
 * Modified date：2012/7/13 Friday 
 * Remarks：
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Sys.MemberActivity
{
    public enum MemberActivityType
    {
        UploadAvatar = 0, //上传头像
        UploadPicture = 1, //发布图片
        Comment = 2, //评论
        SinaMicroblogLogin = 3, //新浪微博登录
        TencentMicroblogLogin = 18, //腾讯微博登录
        DoubanLogin = 4, //豆瓣登录
        RenrenLogin = 19, //人人登录
        VideoCertificate = 5, //视频认证
        RemoveContent = 6, //删除内容
        PrivateLetter = 7, //私信
        PersonalInfoChange = 8, //资料修改
        AddInterest = 9, //加入兴趣
        AddInterview = 10, //访谈
        NewTalkAbout = 11, //新说说
        LikeOther = 12, //mo别人的内容
        FavorOther = 13, //关注别人
        SubmitOpinion = 14, //提交建议
        Logout = 15, //登出
        Login = 16, //登录
        CreateInterest = 17, //创建兴趣
        Unknown = 20, //未知
        Register = 21, //注册
        AllowLogin = 22 //允许登录，通过邀请码 
    }
}
