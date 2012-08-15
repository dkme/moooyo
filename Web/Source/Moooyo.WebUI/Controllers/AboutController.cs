using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moooyo.WebUI.Controllers
{
    public class AboutController : Controller
    {
        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        public ActionResult Agreement()
        {
            #region metas version
            SetMetasVersion();
            #endregion

            return View();
        }
        public ActionResult Guideline()
        {
            #region metas version
            SetMetasVersion();
            #endregion

            return View();
        }
        public ActionResult Privacy()
        {
            #region metas version
            SetMetasVersion();
            #endregion

            return View();
        }
        public ActionResult Disclaimer()
        {
            #region metas version
            SetMetasVersion();
            #endregion

            return View();
        }
    }
}
