using System;
using System.Web;
using System.Web.Mvc;

namespace Moooyo.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        #region 视图方法
        //按编号提示错误信息
        public ActionResult Error(String errorno, string errorinfo)
        {
            SetMetasVersion();

            String errorInfo = "";
            ViewData["errorNo"] = errorno == null ? "" : errorno;
            switch (errorno) {
                case "0": errorInfo = "需要提供完整参数。";
                    break;
                case "1": errorInfo = "哦哦，出错了。";
                    break;
                case "2": errorInfo = "用户激活失败。";
                    break;
                case "3": errorInfo = "邮件发送失败。";
                    break;
                case "404": errorInfo = "这次运气差了一点点哦，你要访问的页面不存在。";
                    break;
                case "1000": //1000是自定义错误提示信息
                    if (errorinfo != null && errorinfo != "") 
                        errorInfo = errorinfo;
                    else 
                        errorInfo = "哦哦，出错了。";
                    break;
                default: 
                    break;
            }
            ViewData["errorInfo"] = errorInfo == null ? "" : errorInfo;
            return View();
        }

        #endregion

        #region 数据与业务方法

        #endregion
    }
}
