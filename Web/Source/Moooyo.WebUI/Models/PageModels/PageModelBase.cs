using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    /// <summary>
    /// 页面数据模型的基类
    /// </summary>
    public class PageModelBase
    {
        //基本参数
        public String JsVersion { get{ return BiZ.Sys.RunStatus.JsVersion;  } }
        public String CssVersion { get { return BiZ.Sys.RunStatus.CSSVersion; } }
        public String ImageVersion { get { return BiZ.Sys.RunStatus.ImageVersion; } }
        public String UploadPath { get { return CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath"); } }
        public int WenWenCount { get { return BiZ.WenWen.WenWenProvider.GetWenWenCount(null); } }

        //访问者是否已登录
        public bool AlreadyLogon;
        //访问用户的ID
        public String UserID;
        //页面用户的ID
        public String MemberID;
        //是否浏览自己的页面
        public bool IsOwner { get { return MemberID == UserID ? true : false; } }
        //分页对象
        public Models.PaggerObj Pagger;
        //登录用户显示对象
        public Models.UserDisplayObj User
        {
            get
            {
                if (_user == null)
                {
                    if (UserID != null)
                        _user = Models.DisplayObjProvider.getUserDisplayObj(UserID);
                }
                return _user;
            }
        }
        private Models.UserDisplayObj _user;
    }
}