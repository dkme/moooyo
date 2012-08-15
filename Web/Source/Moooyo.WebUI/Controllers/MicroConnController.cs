using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Web.Script.Serialization;

namespace Moooyo.WebUI.Controllers
{
    public class MicroConnController : Controller
    {
        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        #region 视图方法
        public ActionResult ConnectToDouBan(string oauth_verifier, string oauth_token, string isbinding, string isSendingContent, string content)
        {
            Common.Comm.SetMetasVersion(ViewData);
            bool IsBindingPlatform = false;
            if (isbinding != null)
            {
                if (isbinding.ToString().Trim() == "true")
                {
                    //设置绑定标识
                    IsBindingPlatform = true;
                }
            }

            BiZ.Member.Connector.DouBanConnector doubanConnector;

            if (oauth_token == null)
            {
                if (IsBindingPlatform)
                {
                    #region 标记绑定完毕是否需要转发信息
                    bool isSendingContentFlag = false;
                    if (isSendingContent != null)
                    {
                        if (isSendingContent.ToString().Trim() == "true")
                        {
                            //设置绑定标识
                            isSendingContentFlag = true;
                        }
                    }

                    Session["isSendingContentFlag"] = isSendingContentFlag;
                    Session["SendingContent"] = content == null ? "" : content;
                    #endregion
                }

                doubanConnector = new BiZ.Member.Connector.DouBanConnector();
                Session["DouBanConnector"] = doubanConnector;

                string param = IsBindingPlatform ? "?isbinding=true" : "";
                string url = doubanConnector.Connect(param);
                Response.Redirect(url, false);
            }
            else
            {
                if (Session["DouBanConnector"] == null)
                    Session["DouBanConnector"] = new BiZ.Member.Connector.DouBanConnector();
                doubanConnector = Session["DouBanConnector"] as BiZ.Member.Connector.DouBanConnector;

                bool connected = doubanConnector.Connect();
                if (connected)
                {
                    //获取以前保存的平台信息
                    BiZ.Member.Connector.DouBanConnector connectorold = BiZ.Member.Connector.ConnectorProvider.GetConnectorByConnectorID<BiZ.Member.Connector.DouBanConnector>(doubanConnector.ConnectID, doubanConnector.PlatformType);
                    //保存刷新的平台信息
                    if (connectorold != null)
                    {
                        doubanConnector._id = connectorold._id;
                        doubanConnector.MemberID = connectorold.MemberID;
                    }

                    if (IsBindingPlatform)
                    {
                        String memberid = HttpContext.User.Identity.Name;
                        if (memberid != null)
                        {
                            doubanConnector.MemberID = memberid;
                        }
                        //移除本用户其他新浪微博绑定
                        BiZ.Member.Connector.ConnectorProvider.UnBindPlatform(memberid, BiZ.Member.Connector.Platform.Douban);
                    }

                    //保存用户连接
                    BiZ.Member.Connector.ConnectorProvider.SaveConnector(doubanConnector);

                    //区别是用平台ID登录，还是绑定平台
                    if (!IsBindingPlatform)
                    {
                        //如果链接器用户ID为空，则注册新用户
                        if (doubanConnector.MemberID == null)
                        {
                            BiZ.Member.Member mym = loginfromweibo(doubanConnector);
                            //外部平台增加用户动态到后台
                            ExternalPlatformLoginCreateMemberActivity(mym.ID, doubanConnector.PlatformType);
                            Session["UserID"] = mym.ID;
                            //该session用于豆瓣登录选择性别
                            Session["isDoubanLogin"] = true;
                            return RedirectToAction("loginfromweibo", "Account");
                        }
                        //否则用原用户登录
                        else
                        {
                            //外部平台增加用户动态到后台
                            ExternalPlatformLoginCreateMemberActivity(doubanConnector.MemberID, doubanConnector.PlatformType);
                            Session["UserID"] = doubanConnector.MemberID;
                            //该session用于豆瓣登录选择性别
                            Session["isDoubanLogin"] = true;
                            return RedirectToAction("loginfromweibo", "Account");
                        }
                    }
                    else
                    {
                        #region 绑定完毕是否需要转发信息
                        if (Session["isSendingContentFlag"] != null)
                        {
                            ViewData["isSendingContentFlag"] = Session["isSendingContentFlag"] == null ? false : (bool)Session["isSendingContentFlag"];
                            ViewData["SendingContent"] = Session["SendingContent"] == null ? "" : Session["SendingContent"].ToString();
                        }
                        #endregion
                    }
                }
                else
                {
                    ViewData["error"] = true;
                }
            }
            return View();
        }
        public ActionResult ConnectToSinaWeibo(string oauth_verifier, string oauth_token, string isbinding, string isSendingContent, string content)
        {
            Common.Comm.SetMetasVersion(ViewData);

            bool IsBindingPlatform = false;
            if (isbinding != null)
            {
                if (isbinding.ToString().Trim() == "true")
                {
                    //设置绑定标识
                    IsBindingPlatform = true;
                }
            }

            BiZ.Member.Connector.SinaWeiboConnector sinaConnector;

            if (oauth_verifier == null)
            {
                if (IsBindingPlatform)
                {
                    #region 标记绑定完毕是否需要转发信息
                    bool isSendingContentFlag = false;
                    if (isSendingContent != null)
                    {
                        if (isSendingContent.ToString().Trim() == "true")
                        {
                            //设置绑定标识
                            isSendingContentFlag = true;
                        }
                    }

                    Session["isSendingContentFlag"] = isSendingContentFlag;
                    Session["SendingContent"] = content == null ? "" : content;
                    #endregion
                }

                sinaConnector = new BiZ.Member.Connector.SinaWeiboConnector();
                Session["SinaConnector"] = sinaConnector;

                string param = IsBindingPlatform ? "?isbinding=true" : "";
                string url = sinaConnector.Connect(param);
                Response.Redirect(url, false);
            }
            else
            {
                if (Session["SinaConnector"] == null)
                    Session["SinaConnector"] = new BiZ.Member.Connector.SinaWeiboConnector();
                sinaConnector = Session["SinaConnector"] as BiZ.Member.Connector.SinaWeiboConnector;

                bool connected = sinaConnector.Connect(oauth_verifier, oauth_token);
                if (connected)
                {
                    //获取以前保存的平台信息
                    BiZ.Member.Connector.SinaWeiboConnector connectorold = BiZ.Member.Connector.ConnectorProvider.GetConnectorByConnectorID<BiZ.Member.Connector.SinaWeiboConnector>(sinaConnector.ConnectID, sinaConnector.PlatformType);
                    //保存刷新的平台信息
                    if (connectorold != null)
                    {
                        sinaConnector._id = connectorold._id;
                        sinaConnector.MemberID = connectorold.MemberID;
                    }

                    if (IsBindingPlatform)
                    {
                        String memberid = HttpContext.User.Identity.Name;
                        if (memberid != null)
                        {
                            sinaConnector.MemberID = memberid;
                        }
                        //移除本用户其他新浪微博绑定
                        BiZ.Member.Connector.ConnectorProvider.UnBindPlatform(memberid, BiZ.Member.Connector.Platform.SinaWeibo);
                    }

                    //保存用户连接
                    BiZ.Member.Connector.ConnectorProvider.SaveConnector(sinaConnector);

                    //区别是用平台ID登录，还是绑定平台
                    if (!IsBindingPlatform)
                    {
                        //如果链接器用户ID为空，则注册新用户
                        if (sinaConnector.MemberID == null)
                        {
                            BiZ.Member.Member mym = loginfromweibo(sinaConnector);
                            //外部平台增加用户动态到后台
                            ExternalPlatformLoginCreateMemberActivity(mym.ID, sinaConnector.PlatformType);
                            Session["UserID"] = mym.ID;
                            return RedirectToAction("loginfromweibo", "account");
                        }
                        //否则用原用户登录
                        else
                        {
                            //外部平台增加用户动态到后台
                            ExternalPlatformLoginCreateMemberActivity(sinaConnector.MemberID, sinaConnector.PlatformType);
                            Session["UserID"] = sinaConnector.MemberID;
                            return RedirectToAction("loginfromweibo", "account");
                        }
                    }
                    else
                    {
                        #region 绑定完毕是否需要转发信息
                        if (Session["isSendingContentFlag"] != null)
                        {
                            ViewData["isSendingContentFlag"] = Session["isSendingContentFlag"] == null ? false : (bool)Session["isSendingContentFlag"];
                            ViewData["SendingContent"] = Session["SendingContent"] == null ? "" : Session["SendingContent"].ToString();
                        }
                        #endregion
                    }
                }
                else
                {
                    ViewData["error"] = true;
                }
            }

            return View();
        }
        public ActionResult ConnectToTXWeibo(string oauth_verifier, string oauth_token, string isbinding, string isSendingContent, string content)
        {
            Common.Comm.SetMetasVersion(ViewData);

            bool IsBindingPlatform = false;
            if (isbinding != null)
            {
                if (isbinding.ToString().Trim() == "true")
                {
                    //设置绑定标识
                    IsBindingPlatform = true;
                }
            }

            BiZ.Member.Connector.TXWeiboConnector txWeiboConnector;

            if (oauth_verifier == null)
            {
                if (IsBindingPlatform)
                {
                    #region 标记绑定完毕是否需要转发信息
                    bool isSendingContentFlag = false;
                    if (isSendingContent != null)
                    {
                        if (isSendingContent.ToString().Trim() == "true")
                        {
                            //设置绑定标识
                            isSendingContentFlag = true;
                        }
                    }

                    Session["isSendingContentFlag"] = isSendingContentFlag;
                    Session["SendingContent"] = content == null ? "" : content;
                    #endregion
                }

                txWeiboConnector = new BiZ.Member.Connector.TXWeiboConnector();
                Session["txWeiboConnector"] = txWeiboConnector;

                string param = IsBindingPlatform ? "?isbinding=true" : "";
                string url = txWeiboConnector.Connect(param);
                if (url == "err") return Content("<script>window.parent.jBox.tip('抱歉，绑定到腾讯微博功能正在维护中，请稍候使用或直接使用米柚账号登录。');window.parent.jBox.close(true);</script>");
                Response.Redirect(url, false);
            }
            else
            {

                txWeiboConnector = Session["txWeiboConnector"] as BiZ.Member.Connector.TXWeiboConnector;
                //if (txWeiboConnector == null) { 
                //    txWeiboConnector = new BiZ.Member.Connector.TXWeiboConnector(); 
                //}

                bool connected = txWeiboConnector.Connect(oauth_verifier, oauth_token);
                if (connected)
                {
                    //获取以前保存的平台信息
                    BiZ.Member.Connector.TXWeiboConnector connectorold = BiZ.Member.Connector.ConnectorProvider.GetConnectorByConnectorID<BiZ.Member.Connector.TXWeiboConnector>(txWeiboConnector.ConnectID, txWeiboConnector.PlatformType);
                    //保存刷新的平台信息
                    if (connectorold != null)
                    {
                        txWeiboConnector._id = connectorold._id;
                        txWeiboConnector.MemberID = connectorold.MemberID;
                    }

                    if (IsBindingPlatform)
                    {
                        String memberid = HttpContext.User.Identity.Name;
                        if (memberid != null)
                        {
                            txWeiboConnector.MemberID = memberid;
                        }

                        //移除本用户其他腾讯微博绑定
                        BiZ.Member.Connector.ConnectorProvider.UnBindPlatform(memberid, BiZ.Member.Connector.Platform.TencentWeibo);
                    }

                    //保存用户连接
                    BiZ.Member.Connector.ConnectorProvider.SaveConnector(txWeiboConnector);

                    //区别是用平台ID登录，还是绑定平台
                    if (!IsBindingPlatform)
                    {
                        //如果链接器用户ID为空，则注册新用户
                        if (txWeiboConnector.MemberID == null)
                        {
                            BiZ.Member.Member mym = loginfromweibo(txWeiboConnector);
                            //外部平台增加用户动态到后台
                            ExternalPlatformLoginCreateMemberActivity(mym.ID, txWeiboConnector.PlatformType);
                            Session["UserID"] = mym.ID;
                            return RedirectToAction("loginfromweibo", "Account");
                        }
                        //否则用原用户登录
                        else
                        {
                            //外部平台增加用户动态到后台
                            ExternalPlatformLoginCreateMemberActivity(txWeiboConnector.MemberID, txWeiboConnector.PlatformType);
                            Session["UserID"] = txWeiboConnector.MemberID;
                            return RedirectToAction("loginfromweibo", "Account");
                        }
                    }
                    else
                    {
                        #region 绑定完毕是否需要转发信息
                        if (Session["isSendingContentFlag"] != null)
                        {
                            ViewData["isSendingContentFlag"] = Session["isSendingContentFlag"] == null ? false : (bool)Session["isSendingContentFlag"];
                            ViewData["SendingContent"] = Session["SendingContent"] == null ? "" : Session["SendingContent"].ToString();
                        }
                        #endregion
                    }
                }
                else
                {
                    ViewData["error"] = true;
                }
            }

            return View();
        }
        public ActionResult ConnectToRenRen(string code, string isbinding, string isSendingContent, string content)
        {
            Common.Comm.SetMetasVersion(ViewData);

            bool IsBindingPlatform = false;
            if (isbinding != null)
            {
                if (isbinding.ToString().Trim() == "true")
                {
                    //设置绑定标识
                    IsBindingPlatform = true;
                }
            }

            BiZ.Member.Connector.RenRenConnector renrenConnector = null;

            if (code == null)
            {
                if (IsBindingPlatform)
                {
                    #region 标记绑定完毕是否需要转发信息
                    bool isSendingContentFlag = false;
                    if (isSendingContent != null)
                    {
                        if (isSendingContent.ToString().Trim() == "true")
                        {
                            //设置绑定标识
                            isSendingContentFlag = true;
                        }
                    }

                    Session["isSendingContentFlag"] = isSendingContentFlag;
                    Session["SendingContent"] = content == null ? "" : content;
                    #endregion
                }

                renrenConnector = new BiZ.Member.Connector.RenRenConnector();
                Session["renrenConnector"] = renrenConnector;

                string param = IsBindingPlatform ? "?isbinding=true" : "";
                string url = renrenConnector.Connect(param);
                if (url == "err") return Content("<script>window.parent.jBox.tip('抱歉，绑定到腾讯微博功能正在维护中，请稍候使用或直接使用米柚账号登录。');window.parent.jBox.close(true);</script>");
                //return View();
                //Response.Redirect(url, false);
            }
            else
            {
                renrenConnector = Session["renrenConnector"] as BiZ.Member.Connector.RenRenConnector;//本地测试的时候session丢失
                if (renrenConnector == null)
                {
                    renrenConnector = new BiZ.Member.Connector.RenRenConnector();
                    renrenConnector.PlatformType = BiZ.Member.Connector.Platform.RenRen;
                }
                bool connected = renrenConnector.getUserinfo();

                if (connected)
                {
                    //获取以前保存的平台信息
                    BiZ.Member.Connector.RenRenConnector connectorold = BiZ.Member.Connector.ConnectorProvider.GetConnectorByConnectorID<BiZ.Member.Connector.RenRenConnector>(renrenConnector.ConnectID, renrenConnector.PlatformType);
                    //保存刷新的平台信息
                    if (connectorold != null)
                    {
                        renrenConnector._id = connectorold._id;
                        renrenConnector.MemberID = connectorold.MemberID;
                    }

                    if (IsBindingPlatform)
                    {
                        String memberid = HttpContext.User.Identity.Name;
                        if (memberid != null)
                        {
                            renrenConnector.MemberID = memberid;
                        }

                        //移除本用户其他腾讯微博绑定
                        BiZ.Member.Connector.ConnectorProvider.UnBindPlatform(memberid, BiZ.Member.Connector.Platform.RenRen);
                    }

                    //保存用户连接
                    BiZ.Member.Connector.ConnectorProvider.SaveConnector(renrenConnector);

                    //区别是用平台ID登录，还是绑定平台
                    if (!IsBindingPlatform)
                    {
                        //如果链接器用户ID为空，则注册新用户
                        if (renrenConnector.MemberID == null)
                        {
                            BiZ.Member.Member mym = loginfromweibo(renrenConnector);
                            //外部平台增加用户动态到后台
                            ExternalPlatformLoginCreateMemberActivity(mym.ID, renrenConnector.PlatformType);
                            Session["UserID"] = mym.ID;
                            return RedirectToAction("loginfromweibo", "Account");
                        }
                        //否则用原用户登录
                        else
                        {
                            //外部平台增加用户动态到后台
                            ExternalPlatformLoginCreateMemberActivity(renrenConnector.MemberID, renrenConnector.PlatformType);
                            Session["UserID"] = renrenConnector.MemberID;
                            return RedirectToAction("loginfromweibo", "Account");
                        }
                    }
                    else
                    {
                        #region 绑定完毕是否需要转发信息
                        if (Session["isSendingContentFlag"] != null)
                        {
                            ViewData["isSendingContentFlag"] = Session["isSendingContentFlag"] == null ? false : (bool)Session["isSendingContentFlag"];
                            ViewData["SendingContent"] = Session["SendingContent"] == null ? "" : Session["SendingContent"].ToString();
                        }
                        #endregion
                    }
                }
                else
                {
                    ViewData["error"] = true;
                }
            }

            return View();
        }
        #endregion

        #region 数据与业务方法
        public BiZ.Member.Member loginfromweibo<T>(T connector) where T : BiZ.Member.Connector.Connector
        {
            //获取以前保存的平台信息
            BiZ.Member.Connector.Connector connectorold = BiZ.Member.Connector.ConnectorProvider.GetConnectorByConnectorID<T>(connector.ConnectID, connector.PlatformType);

            BiZ.Member.Member mym = null;
            if (connectorold != null)
            {
                if (connectorold.MemberID != null)
                    mym = BiZ.MemberManager.MemberManager.GetMember(connectorold.MemberID);
            }

            if (mym == null)
            {
                mym = BiZ.MemberManager.MemberManager.AddNewPreMemberByEmailRegister(connector.ConnectID + "@" + connector.PlatformType, Int32.Parse(connector.Sex), new CBB.Security.RandomPassword().GetRandomPassword(8));

                BiZ.MemberManager.MemberManager.SaveMember(mym);
                connector.MemberID = mym.ID;
                BiZ.Member.Connector.ConnectorProvider.SaveConnectorMemberID(connector.ConnectID, connector.PlatformType, mym.ID);

                string birthday = ((connector.Year == "" || connector.Year == null) ? "1900" : connector.Year.ToString())
                            + "-" + ((connector.Month == "" || connector.Month == null) ? "1" : connector.Month.ToString())
                            + "-" + ((connector.Day == "" || connector.Day == null) ? "1" : connector.Day.ToString());

                BiZ.MemberManager.MemberManager.SetBaseInfo(mym.ID, new CBB.Security.RandomPassword().GetRandomPassword(8), connector.Name, DateTime.Parse(birthday), connector.Province + "|" + connector.City, true);
                //发消息给用户
                BiZ.Member.Activity.ActivityController.SystemMsgToMember(mym.ID, CBB.ConfigurationHelper.AppSettingHelper.GetConfig("RegisterFinished"));

                //添加邀请表记录
                if (null != Session["Inviter"] && Session["Inviter"].ToString().Trim().Length > 0)
                {
                    Moooyo.BiZ.Member.Relation.RelationProvider.AddRegInviter(Session["Inviter"].ToString(), mym.ID);
                }
            }

            return mym;

        }
        /// <summary>
        /// 外部平台增加用户动态到后台
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <param name="platformType">平台类型</param>
        /// <returns>操作状态</returns>
        internal BiZ.Member.Member ExternalPlatformLoginCreateMemberActivity(
            string memberId, BiZ.Member.Connector.Platform platformType)
        {
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(memberId);
            
            //增加用户动态到后台 begin ——————
            string operateUrl = "";
            if (mym.UniqueNumber != null)
                operateUrl = "/u/" + mym.UniqueNumber.ConvertedID;
            else
                operateUrl = "/Content/TaContent/" + mym.ID;
            BiZ.Sys.MemberActivity.MemberActivityType memberActivityType = new BiZ.Sys.MemberActivity.MemberActivityType();
            switch (platformType)
            {
                case BiZ.Member.Connector.Platform.SinaWeibo:
                    memberActivityType = BiZ.Sys.MemberActivity.MemberActivityType.SinaMicroblogLogin;
                    break;
                case BiZ.Member.Connector.Platform.TencentWeibo:
                    memberActivityType = BiZ.Sys.MemberActivity.MemberActivityType.TencentMicroblogLogin;
                    break;
                case BiZ.Member.Connector.Platform.RenRen:
                    memberActivityType = BiZ.Sys.MemberActivity.MemberActivityType.RenrenLogin;
                    break;
                case BiZ.Member.Connector.Platform.Douban:
                    memberActivityType = BiZ.Sys.MemberActivity.MemberActivityType.DoubanLogin;
                    break;
                default:
                    break;
            }
            //增加用户动态到后台
            BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                mym.ID,
                "",
                memberActivityType,
                operateUrl);

            return mym;
        }
        /// <summary>
        /// 禁用平台（取消绑定）
        /// </summary>
        /// <param name="platformType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveConnectorEnableStatusFalse(String platformType)
        {
            if (platformType == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int platformTypeint = int.Parse(platformType.ToString().Trim());
            String memberid = HttpContext.User.Identity.Name;

            if (memberid != null)
            {
                CBB.ExceptionHelper.OperationResult result = BiZ.Member.Connector.ConnectorProvider.SaveConnectorEnableStatus(memberid, (BiZ.Member.Connector.Platform)platformTypeint, false);
                return Json(new JavaScriptSerializer().Serialize(result));
            }
            else
            {
                return Json(new JavaScriptSerializer().Serialize(new CBB.ExceptionHelper.OperationResult(false)));
            }
        }
        [HttpPost]
        public ActionResult SendInfo(String platformType, String Content, String PicPath, String Url)
        {
            try
            {
                if (platformType == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
                String memberid = User.Identity.Name;
                if (memberid == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

                if (Session["shareWeiBoImage"] != null)
                    PicPath = Session["shareWeiBoImage"].ToString();

                PlatformShareInfo(memberid, platformType, Content, PicPath, Url);

                return Json(new JavaScriptSerializer().Serialize(new CBB.ExceptionHelper.OperationResult(true)));
            }
            catch (System.Exception err)
            {
                return Json(new JavaScriptSerializer().Serialize(new CBB.ExceptionHelper.OperationResult(false, err.Message)));
            }
        }
        internal void PlatformShareInfo(string memberId, string platformTypeStr, string content, string sharedPicture, string url)
        {
            string sendinfo = "";

            if (platformTypeStr.LastIndexOf('|') == platformTypeStr.Length - 1)
                platformTypeStr = platformTypeStr.Substring(0, platformTypeStr.Length - 1);
            string[] platformTypeStrs = platformTypeStr.Split('|');
            byte platformTypeStrsLength = (byte)platformTypeStrs.Length;

            for (byte i = 0; i < platformTypeStrs.Length; i++)
            {
                //新浪微博
                if (Convert.ToInt32(platformTypeStrs[i]) == Convert.ToInt32(BiZ.Member.Connector.Platform.SinaWeibo))
                {
                    BiZ.Member.Connector.Connector conn = BiZ.Member.Connector.ConnectorProvider.GetConnector<BiZ.Member.Connector.SinaWeiboConnector>(memberId, BiZ.Member.Connector.Platform.SinaWeibo);
                    url = url + "?" + Common.Comm.getShareSina();
                    if (sharedPicture != "")
                        sendinfo = conn.SendInfoWithPic(url, content, sharedPicture);
                    else
                        sendinfo = conn.SendInfo(url, content);
                }
                //腾讯微博
                if (Convert.ToInt32(platformTypeStrs[i]) == Convert.ToInt32(BiZ.Member.Connector.Platform.TencentWeibo))
                {
                    BiZ.Member.Connector.Connector conn = BiZ.Member.Connector.ConnectorProvider.GetConnector<BiZ.Member.Connector.TXWeiboConnector>(memberId, BiZ.Member.Connector.Platform.TencentWeibo);
                    url = url + "?" + Common.Comm.getShareTencent();
                    if (sharedPicture != "")
                        sendinfo = conn.SendInfoWithPic(url, content, sharedPicture);
                    else
                        sendinfo = conn.SendInfo(url, content);
                }
                //人人帐号
                if (Convert.ToInt32(platformTypeStrs[i]) == Convert.ToInt32(BiZ.Member.Connector.Platform.RenRen))
                {
                    BiZ.Member.Connector.Connector conn = BiZ.Member.Connector.ConnectorProvider.GetConnector<BiZ.Member.Connector.RenRenConnector>(memberId, BiZ.Member.Connector.Platform.RenRen);
                    url = url + "?" + Common.Comm.getShareRenRen();
                    if (sharedPicture != "")
                        sendinfo = conn.SendInfoWithPic(url, content, sharedPicture);
                    else
                        sendinfo = conn.SendInfo(url, content);
                }
                //豆瓣帐号
                if (Convert.ToInt32(platformTypeStrs[i]) == Convert.ToInt32(BiZ.Member.Connector.Platform.Douban))
                {
                    BiZ.Member.Connector.Connector conn = BiZ.Member.Connector.ConnectorProvider.GetConnector<BiZ.Member.Connector.DouBanConnector>(memberId, BiZ.Member.Connector.Platform.Douban);
                    url = url + "?" + Common.Comm.getShareDouBan();
                    //if (PicPath != "")
                    //{
                    //    sendinfo = conn.SendInfoWithPic(Url, Content, PicPath);
                    //}
                    //else
                    //{
                    sendinfo = conn.SendInfo(url, content);
                    //}
                }
            }
            //清除图片分享的session
            delImageFiles();
        }
        /// <summary>
        /// 清楚图片分享的临时文件
        /// </summary>
        public void delImageFiles()
        {
            if (Session["shareWeiBoImage"] != null)
            {
                //string path = HttpContext.Server.MapPath(HttpContext.Session["myWeiBoImageName"].ToString());
                //Common.FileManager.DeleteFile(path);
                Session.Remove("shareWeiBoImage");
            }
            if (Session["shareUserIconImage"] != null)
            {
                Session.Remove("shareUserIconImage");
            }
        }
        #endregion
    }
}
