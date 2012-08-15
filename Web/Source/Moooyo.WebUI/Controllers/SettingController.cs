using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Moooyo.WebUI.Controllers
{
    public class SettingController : Controller
    {
        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        #region 视图方法
        [Authorize]
        public ActionResult PersonInfo()
        {
            SetMetasVersion();
            String userid = HttpContext.User.Identity.Name;
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(userid);
            #region 内测期跳转检测
            if (!mym.AllowLogin)
            {
                return RedirectToAction("Welcome", "Account");
            }
            #endregion
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel(memberDisplayObj);
            memberModel.UserID = userid;
            memberModel.MemberID = userid;
            memberModel.AlreadyLogon = true;

            return View(memberModel);
        }
        [Authorize]
        public ActionResult VerifyEmail()
        {
            SetMetasVersion();
            String memberID = HttpContext.User.Identity.Name;
            Models.PageModels.PageModelBase pm = new Models.PageModels.PageModelBase();
            pm.MemberID = memberID;
            pm.UserID = memberID;
            pm.AlreadyLogon = true;

            ViewData["emailurl"] = "mail." + pm.User.Email.Split('@')[1];
            if (pm.User.EmailIsVaild) return View(pm);

            //发送验证邮件
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(memberID);
            if (!CBB.NetworkingHelper.EmailHelper.SendMail(mym.Email, "[米柚]用户验证邮件", Common.Comm.GetVerifyEmailContent(mym.Email, mym.Password)))
                return RedirectToAction("Error", "Error", new { errNo = "邮件发送失败。" });

            return View(pm);
        }
        [Authorize]
        public ActionResult SetLocation()
        {
            SetMetasVersion();
            String memberID = HttpContext.User.Identity.Name;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(memberID);
            IList<BiZ.InterestCenter.Interest> interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(memberID, 0, 0);
            Models.PageModels.MemberProfileModel memberModel = new Models.PageModels.MemberProfileModel(memberDisplayObj);
            String interestIds = "";
            if (interestList != null)
            {
                foreach (BiZ.InterestCenter.Interest interest in interestList)
                {
                    interestIds += interest.ID + ",";
                }
            }
            memberModel.interestIds = interestIds;
            memberModel.UserID = memberID;
            memberModel.MemberID = memberID;
            memberModel.AlreadyLogon = true;


            return View(memberModel);
        }
        [Authorize]
        public ActionResult SetEmail()
        {
            SetMetasVersion();
            String memberID = HttpContext.User.Identity.Name;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(memberID);
            Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel(memberDisplayObj);
            memberModel.UserID = memberID;
            memberModel.MemberID = memberID;
            memberModel.AlreadyLogon = true;

            return View(memberModel);
        }
        [Authorize]
        public ActionResult Authentica()
        {
            SetMetasVersion();
            String memberID = HttpContext.User.Identity.Name;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(memberID);
            Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel(memberDisplayObj);
            memberModel.UserID = memberID;
            memberModel.MemberID = memberID;
            memberModel.AlreadyLogon = true;
            if (memberModel.Member.IsRealPhotoIdentification)
            {
                ViewData["isAuth"] = "pass";
            }
            else
            {
                BiZ.PhotoCheck.PhotoCheckModel pcm = Moooyo.BiZ.Sys.PhotoCheck.PhotoCheckStatusFactory.GetCheckPhotoByUserId(memberID);
                if (null == pcm)
                {
                    string temppath = memberModel.Member.ICONPath;
                    if (null == temppath || temppath.Trim().Length <= 0 || temppath.Equals("/pics/noicon.jpg"))
                    {
                        ViewData["isAuth"] = "notIcon";
                    }
                    else
                    {
                        ViewData["isAuth"] = "Auth";
                    }
                }
                else
                {
                    if (pcm.CheckStatus == Convert.ToInt32(BiZ.PhotoCheck.CheckPhotoStatus.waitaudit))
                    {
                        ViewData["isAuth"] = "wait";
                    }
                    else
                    {
                        pcm = Moooyo.BiZ.Sys.PhotoCheck.PhotoCheckStatusFactory.GetCheckPhotoByUserId(memberID, BiZ.PhotoCheck.CheckPhotoStatus.waitaudit);
                        if (null != pcm)
                            ViewData["isAuth"] = "wait";
                        else
                            ViewData["isAuth"] = "lose";
                    }
                }
            }

            return View(memberModel);
        }
        [Authorize]
        public ActionResult AcctUpgrade()
        {
            SetMetasVersion();
            String memberID = HttpContext.User.Identity.Name;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(memberID);
            Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel(memberDisplayObj);
            memberModel.UserID = memberID;
            memberModel.MemberID = memberID;
            memberModel.AlreadyLogon = true;

            return View(memberModel);
        }
        [Authorize]
        public ActionResult Contact()
        {
            SetMetasVersion();
            String memberID = HttpContext.User.Identity.Name;
            BiZ.Member.Member memberObj = BiZ.MemberManager.MemberManager.GetMember(memberID);
            ViewData["nickName"] = memberObj.MemberInfomation.NickName;
            ViewData["mobileNo"] = memberObj.MobileNo;
            ViewData["qq"] = memberObj.MemberInfomation.QQ;

            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(memberID);
            Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel(memberDisplayObj);
            memberModel.UserID = memberID;
            memberModel.MemberID = memberID;
            memberModel.AlreadyLogon = true;
            return View(memberModel);
        }

        [Authorize]
        public ActionResult AccountBind()
        {
            SetMetasVersion();

            String memberID = HttpContext.User.Identity.Name;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(memberID);

            IList<BiZ.Member.Connector.Connector> connectors = new List<BiZ.Member.Connector.Connector>();
            connectors.Add(BiZ.Member.Connector.ConnectorProvider.GetConnector<BiZ.Member.Connector.SinaWeiboConnector>(memberID, BiZ.Member.Connector.Platform.SinaWeibo));
            connectors.Add(BiZ.Member.Connector.ConnectorProvider.GetConnector<BiZ.Member.Connector.TXWeiboConnector>(memberID, BiZ.Member.Connector.Platform.TencentWeibo));
            connectors.Add(BiZ.Member.Connector.ConnectorProvider.GetConnector<BiZ.Member.Connector.RenRenConnector>(memberID, BiZ.Member.Connector.Platform.RenRen));
            connectors.Add(BiZ.Member.Connector.ConnectorProvider.GetConnector<BiZ.Member.Connector.DouBanConnector>(memberID, BiZ.Member.Connector.Platform.Douban));

            Models.PageModels.MemberBindingPlatformModel memberModel = new Models.PageModels.MemberBindingPlatformModel(
                memberDisplayObj,
                connectors);

            memberModel.UserID = memberID;
            memberModel.MemberID = memberID;
            memberModel.AlreadyLogon = true;

            return View(memberModel);
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            SetMetasVersion();
            String memberID = HttpContext.User.Identity.Name;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(memberID);
            Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel(memberDisplayObj);
            memberModel.UserID = memberID;
            memberModel.MemberID = memberID;
            memberModel.AlreadyLogon = true;

            return View(memberModel);
        }
        [Authorize]
        public ActionResult Privacy()
        {
            SetMetasVersion();
            String memberID = HttpContext.User.Identity.Name;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(memberID);
            Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel(memberDisplayObj);
            memberModel.UserID = memberID;
            memberModel.MemberID = memberID;
            memberModel.AlreadyLogon = true;

            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(memberID);
            ViewData["AutoAddOutCallingToMyFavorList"] = mym.Settings.AutoAddOutCallingToMyFavorList;
            ViewData["OnlySeniorMemberCanTalkSaiHiMe"] = mym.Settings.OnlySeniorMemberCanTalkSaiHiMe;
            ViewData["OnlyVIPMemberCanTalkSaiHiMe"] = mym.Settings.OnlyVIPMemberCanTalkSaiHiMe;

            return View(memberModel);
        }
        [Authorize]
        public ActionResult AccessSet()
        {
            SetMetasVersion();
            String memberID = HttpContext.User.Identity.Name;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(memberID);
            Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel(memberDisplayObj);
            memberModel.UserID = memberID;
            memberModel.MemberID = memberID;
            memberModel.AlreadyLogon = true;

            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(memberID);
            ViewData["isAllowAccessMe"] = mym.Settings.StopMyAccount;

            return View(memberModel);
        }
        [Authorize]
        public ActionResult UploadFace()
        {
            SetMetasVersion();
            String userid = HttpContext.User.Identity.Name;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            IList<BiZ.InterestCenter.Interest> interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userid, 0, 0);
            Models.PageModels.MemberProfileModel memberModel = new Models.PageModels.MemberProfileModel(memberDisplayObj);
            String interestIds = "";
            if (interestList != null)
            {
                foreach (BiZ.InterestCenter.Interest interest in interestList)
                {
                    interestIds += interest.ID + ",";
                }
            }
            memberModel.interestIds = interestIds;
            memberModel.UserID = userid;
            memberModel.MemberID = userid;
            memberModel.AlreadyLogon = true;

            return View(memberModel);
        }
        [Authorize]
        public ActionResult Invite()
        {
            SetMetasVersion();
            String userid = HttpContext.User.Identity.Name;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel(memberDisplayObj);
            memberModel.UserID = userid;
            memberModel.MemberID = userid;
            memberModel.AlreadyLogon = true;

            return View(memberModel);
        }
        [Authorize]
        public ActionResult Skin()
        {
            SetMetasVersion();
            String userid = HttpContext.User.Identity.Name;
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);
            Models.PageModels.MemberPageModel memberModel = new Models.PageModels.MemberPageModel(memberDisplayObj);
            memberModel.UserID = userid;
            memberModel.MemberID = userid;
            memberModel.AlreadyLogon = true;

            return View(memberModel);
        }
        [Authorize]
        public ActionResult SkinBackground()
        {
            SetMetasVersion();
            String userid = HttpContext.User.Identity.Name;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);

            IList<BiZ.Member.MemberSkin.MemberSkin> memberSkinList = BiZ.Member.MemberSkin.MemberSkinProvider.GetMemberSkins(userid, 6, 1, "PersonalityBackgroundPicture");
            double memberSkinCount = BiZ.Member.MemberSkin.MemberSkinProvider.GetMemberSkinCount(userid, "PersonalityBackgroundPicture");

            Models.PageModels.Setting.MemberSkinModel memberSkinModel = new Models.PageModels.Setting.MemberSkinModel(
                memberDisplayObj, 
                memberSkinList
                );
            memberSkinModel.UserID = userid;
            memberSkinModel.MemberID = userid;
            memberSkinModel.AlreadyLogon = true;
            memberSkinModel.Pagger = new Models.PaggerObj();
            int pageCount = (int)Math.Ceiling(memberSkinCount / 6);
            memberSkinModel.Pagger.PageCount = pageCount;
            #endregion

            return View(memberSkinModel);
        }
        [Authorize]
        public ActionResult SkinPicture()
        {
            SetMetasVersion();
            String userid = HttpContext.User.Identity.Name;

            #region 构造页面数据对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userid);

            IList<BiZ.Member.MemberSkin.MemberSkin> memberSkinList = BiZ.Member.MemberSkin.MemberSkinProvider.GetMemberSkins(userid, 8, 1, "PersonalityPicture");
            double memberSkinCount = BiZ.Member.MemberSkin.MemberSkinProvider.GetMemberSkinCount(userid, "PersonalityPicture");

            Models.PageModels.Setting.MemberSkinModel memberSkinModel = new Models.PageModels.Setting.MemberSkinModel(
                memberDisplayObj,
                memberSkinList
                );
            memberSkinModel.UserID = userid;
            memberSkinModel.MemberID = userid;
            memberSkinModel.AlreadyLogon = true;
            memberSkinModel.Pagger = new Models.PaggerObj();
            int pageCount = (int)Math.Ceiling(memberSkinCount / 8);
            memberSkinModel.Pagger.PageCount = pageCount;
            #endregion

            return View(memberSkinModel);
        }
        //[Authorize]
        //public ActionResult Setting(String type)
        //{
        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        //    #endregion

        //    ViewData["type"] = type;

        //    return View();
        //}
        //[Authorize]
        //public ActionResult SetMyLocation(string type)
        //{
        //    #region metas version
        //    ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
        //    ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
        //    ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
        //    #endregion

        //    String userid = HttpContext.User.Identity.Name;
        //    BiZ.Member.Member me = BiZ.MemberManager.MemberManager.GetMember(userid);
        //    ViewData["city"] = me.MemberInfomation.City.Split('|')[1].ToString();
        //    ViewData["lng"] = me.MemberInfomation.Lng;
        //    ViewData["lat"] = me.MemberInfomation.Lat;
        //    ViewData["type"] = type;
        //    return View();
        //}
        #endregion

        #region 数据与业务方法
        [ValidateInput(true)]
        [Authorize]
        public ActionResult ActiveEmail(String email, String Pwd)
        {
            if (email == null || Pwd == null)
                return RedirectToAction("Error", "Error", new { errorno = "0" });

            //更新邮件验证状态
            bool result = BiZ.MemberManager.MemberManager.UpdateEmailIsVaild(email, Pwd);
            if (result)
            {
                //BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMemberByEmail(email);
                //if (mym != null)
                //{
                //    //发消息给用户
                //    BiZ.Member.Activity.ActivityController.SystemMsgToMember(mym.ID, CBB.ConfigurationHelper.AppSettingHelper.GetConfig("EmailVerifySuccessed"));
                //}
            }

            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult SetEmailProc(String pwd, String email)
        {
            if (pwd == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (email == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String mid = HttpContext.User.Identity.Name;
            CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SetEmail(mid, pwd, email);

            return Json(new JavaScriptSerializer().Serialize(result));
        }
        [ValidateInput(true)]
        [HttpPost]
        public ActionResult ModifyEmail(String email)
        {
            if (email == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String mid = HttpContext.User.Identity.Name;

            CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SetEmail(mid, email);

            return Json(new JavaScriptSerializer().Serialize(result));
        }
        [HttpPost]
        [ValidateInput(true)]
        [Authorize]
        public ActionResult ModifyEmailPasswd(String email, string password)
        {
            if (email == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String mid = HttpContext.User.Identity.Name;

            CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SetEmailPasswd(mid, email, password);

            return Json(new JavaScriptSerializer().Serialize(result));
        }
        [HttpPost]
        [ValidateInput(true)]
        [Authorize]
        public ActionResult UpdateMemberContact(String mobileNo, String qq)
        {
            if (mobileNo == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (qq == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (HttpContext.User.Identity.Name == null)
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            String memberId = HttpContext.User.Identity.Name;
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(HttpContext.User.Identity.Name);
            mym.MobileNo = mobileNo;
            mym.MemberInfomation.QQ = qq;
            CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SaveMember(mym);
            return Json(new JavaScriptSerializer().Serialize(result));
        }
        [HttpPost]
        public ActionResult UpdateFansGroupName(String memberID, String name, String firstName, String second, String theThird)
        {
            if (memberID == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (name == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (firstName == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (second == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (theThird == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            String userID = HttpContext.User.Identity.Name;
            if (userID == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            memberID = memberID == "" ? userID : memberID;
            CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.UpdateFansGroupName(memberID, name, firstName, second, theThird);
            return Json(new JavaScriptSerializer().Serialize(result));
        }
        [HttpPost]
        public ActionResult GetFansGroupName(String memberID)
        {
            if (memberID == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String userID = HttpContext.User.Identity.Name;
            if (userID == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            memberID = memberID == "" ? userID : memberID;
            BiZ.Member.FansGroupName fansGroupName = BiZ.MemberManager.MemberManager.GetFansGroupName(memberID);
            return Json(new JavaScriptSerializer().Serialize(fansGroupName));
        }
        [Authorize]
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult IsOldPwdRight(String oldpwd)
        {
            if (oldpwd == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
            String userid = User.Identity.Name;
            bool returnvalue = BiZ.MemberManager.MemberManager.OldPwdRight(userid, oldpwd);
            return Json(returnvalue, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult SetNewPwd(String oldpwd, String newpwd)
        {
            if (oldpwd == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (newpwd == "") return RedirectToAction("Error", "Error", new { errorno = "0" });

            String userid = User.Identity.Name;
            CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SetNewPwd(userid, oldpwd, newpwd);

            return Json(new JavaScriptSerializer().Serialize(result));
        }
        [Authorize]
        [HttpPost]
        public ActionResult SetLatLng(bool hiddenmyloc, String lat, String lng)
        {
            if (lat == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (lng == null) return RedirectToAction("Error", "Error", new { errorno = "0" });

            String userid = HttpContext.User.Identity.Name;
            BiZ.MemberManager.MemberManager.SetLatLng(userid, hiddenmyloc, double.Parse(lat), double.Parse(lng));

            return Json(new CBB.ExceptionHelper.OperationResult(true));
        }
        [Authorize]
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult SetContactInfo(String qq, String msn, String tel, String other)
        {
            String userid = User.Identity.Name;
            CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SetContact(userid, qq, msn, tel, other);

            return Json(new JavaScriptSerializer().Serialize(result));
        }
        [Authorize]
        [HttpPost]
        public ActionResult SetAutoAddFavor(bool autoaddfavor)
        {
            String userid = User.Identity.Name;
            CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SetAutoAddFavor(userid, autoaddfavor);

            return Json(new JavaScriptSerializer().Serialize(result));
        }
        [Authorize]
        [HttpPost]
        public ActionResult SetPrivacy(bool flagAutoAddToFavor, bool flagOnlySeniorMemberTS, bool flagOnlyVIPMemberTS)
        {
            String userId = User.Identity.Name;
            CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SetPrivacy(
                userId, flagAutoAddToFavor, flagOnlySeniorMemberTS, flagOnlyVIPMemberTS);

            return Json(new JavaScriptSerializer().Serialize(result));
        }
        [Authorize]
        [HttpPost]
        public ActionResult AccessSet(bool isAllowAccessMe)
        {
            String userId = User.Identity.Name;
            CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.AccessSet(
                userId, isAllowAccessMe);

            return Json(new JavaScriptSerializer().Serialize(result));
        }
        [Authorize]
        [HttpPost]
        public ActionResult updateProfile(
            String nickName,
            String year,
            String month,
            String day,
            //string sex,
            //String hideMyAge,
            String star,
            String province,
            String city,
            //String gprov,
            //String gcity,
            String height,
            //String figure,
            String educationalBackground,
            String career,
            //String propertySituation,
            String domainNameID,
            String personalIntroduction
            //String gainings,
            //String livingStatus,
            //String haveHouse,
            //String haveCar,
            //String carBand,
            //String goal
            )
        {
            if (HttpContext.User.Identity.Name == null)
                return RedirectToAction("Error", "Error", new { errorno = "0" });
            //byte btSex = 1;
            //if(!byte.TryParse(sex, out btSex))
            //    return RedirectToAction("Error", "Error", new { errorno = "0" });

            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(HttpContext.User.Identity.Name);
            BiZ.Member.MemberInfomation myinfo = mym.MemberInfomation;
            myinfo.NickName = nickName;
            if ((year != null && year != "") && (month != null && month != "") && (day != null && day != ""))
            {
                myinfo.Birthday = DateTime.Parse(year + "/" + month + "/" + day);
            }
            //myinfo.Sex = btSex;
            //if (hideMyAge == "on")
            //    myinfo.HideMyAge = true;
            //else
            //    myinfo.HideMyAge = false;
            myinfo.Star = star;
            myinfo.City = province + "|" + city;
            //myinfo.Hometown = gprov + "|" + gcity;
            myinfo.Height = height;
            //myinfo.Figure = figure;
            myinfo.EducationalBackground = educationalBackground;
            myinfo.Career = career;
            //myinfo.PropertySituation = propertySituation;

            BiZ.Comm.UniqueNumber.UniqueNumber uniqueNum;
            if (mym.UniqueNumber == null)
            {
                //按原始编号和编号类型获取转换后的编号
                
                uniqueNum = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetConvertedMemberID(mym.ID, BiZ.Comm.UniqueNumber.IDType.MemberID);
                if (uniqueNum == null)
                {
                    //保存用户编码
                    uniqueNum = BiZ.Comm.UniqueNumber.UniqueNumberProvider.ConvertMemberID(mym.ID, BiZ.Comm.UniqueNumber.IDType.MemberID);
                    //构造用户增强的唯一编号的值
                    BiZ.MemberManager.MemberManager.SetMemberUniqueNumber(uniqueNum, mym);
                }
                mym.UniqueNumber = uniqueNum;
            }
            
            mym.UniqueNumber.DomainNameID = domainNameID;
            if (mym.UniqueNumber.DomainNameID != null)
            {
                BiZ.MemberManager.MemberManager.SetMemberUniqueNumber(mym.UniqueNumber, mym);
            }

            myinfo.PersonalIntroduction = personalIntroduction;
            //myinfo.Gainings = gainings;
            //myinfo.LivingStatus = livingStatus;
            //myinfo.HaveHouse = haveHouse;
            //myinfo.HaveCar = haveCar;
            //myinfo.CarBand = carBand;
            //myinfo.Goal = goal;

            CBB.ExceptionHelper.OperationResult result = BiZ.MemberManager.MemberManager.SaveMember(mym);

            //增加用户动态到后台
            string operateUrl = "";
            if (mym.UniqueNumber != null)
                operateUrl = "/u/" + mym.UniqueNumber.ConvertedID;
            else
                operateUrl = "/Content/TaContent/" + mym.ID;
            BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                mym.ID,
                "",
                BiZ.Sys.MemberActivity.MemberActivityType.PersonalInfoChange,
                operateUrl);

            return Json(new JavaScriptSerializer().Serialize(result));
        }
        [ValidateInput(true)]
        [HttpPost]
        public ActionResult IsDomainNameIDUsed(String domainNameID)
        {
            if (domainNameID == "" || domainNameID == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            String memberId = HttpContext.User.Identity.Name;
            bool returnvalue = BiZ.Comm.UniqueNumber.UniqueNumberProvider.IsDomainNameIDUsed(domainNameID, memberId);
            return Json(returnvalue, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetRandomMemberSkins(int count)
        {
            IList<BiZ.Member.MemberSkin.MemberSkin> memberSkinList = BiZ.Member.MemberSkin.MemberSkinProvider.RandomGetMemberSkins(count);
            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            return Json(jsSerial.Serialize(memberSkinList));
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetMemberSkins(int pageSize, int pageNo)
        {
            String userId = HttpContext.User.Identity.Name;
            IList<BiZ.Member.MemberSkin.MemberSkin> memberSkinList = BiZ.Member.MemberSkin.MemberSkinProvider.GetMemberSkins(userId, pageSize, pageNo);
            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            return Json(jsSerial.Serialize(memberSkinList));
        }
        [HttpPost]
        [Authorize]
        public ActionResult GetMemberSkins2(int pageSize, int pageNo, string pictureType)
        {
            String userId = HttpContext.User.Identity.Name;
            IList<BiZ.Member.MemberSkin.MemberSkin> memberSkinList = BiZ.Member.MemberSkin.MemberSkinProvider.GetMemberSkins(userId, pageSize, pageNo, pictureType);
            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            return Json(jsSerial.Serialize(memberSkinList));
        }
        [HttpPost]
        [Authorize]
        public ActionResult SetMemberSkin(String memberSkinId, String setType)
        {
            String memberId = HttpContext.User.Identity.Name;
            CBB.ExceptionHelper.OperationResult result = BiZ.Member.MemberSkin.MemberSkinProvider.SetMemberSkin(memberSkinId, setType, memberId);
            JavaScriptSerializer jsSerial = new JavaScriptSerializer();
            return Json(jsSerial.Serialize(result));
        }
        //[HttpPost]
        //public ActionResult SetMemberSkinPicture(FormCollection frmColect, String pictureType)
        //{
        //    string memberID = HttpContext.User.Identity.Name;
        //    CBB.ExceptionHelper.OperationResult result;

        //    if (memberID == null)
        //        return Content("-1");

        //    if (Request.Files == null || Request.Files.Count == 0)
        //    {
        //        //上传文件为空，返回
        //        return Content("-1");
        //    }

        //    UpController uc = new UpController();
        //    HttpPostedFileBase httpFile = Request.Files[0];

        //    Int32 fileSize = 5000;    //文件大小限制，单位5KB
        //    if (fileSize * 1024 < httpFile.ContentLength)
        //    {
        //        return Content("{\"ok\":\"false\",\"info\":\"文件大小不能超过5M哦！\"}");
        //    }

        //    int holderNo = 0;
        //    switch (pictureType)
        //    {
        //        case "PersonalityPicture": holderNo = 11;
        //            break;
        //        case "PersonalityBackgroundPicture": holderNo = 12;
        //            break;
        //        default: break;
        //    }

        //    BiZ.Photo.Photo photo = uc.SavePhoto(memberID, holderNo, httpFile.InputStream, httpFile.FileName);

        //    BiZ.Member.MemberSkin.MemberSkin ms = BiZ.Member.MemberSkin.MemberSkinProvider.GetMemberSkin(null, memberID);
        //    if (ms != null)
        //    {
        //        String personalityPicture = null;
        //        String personalityBackgroundPicture = null;

        //        switch (pictureType)
        //        {
        //            case "PersonalityPicture":
        //                personalityPicture = photo.FileName;

        //                break;
        //            case "PersonalityBackgroundPicture":
        //                personalityBackgroundPicture = photo.FileName;
        //                break;
        //            default: break;
        //        }

        //        result = BiZ.Member.MemberSkin.MemberSkinProvider.UpdateMemberSkin(
        //            ms.ID,
        //            memberID,
        //            personalityPicture,
        //            personalityBackgroundPicture);

        //        result = BiZ.Member.MemberSkin.MemberSkinProvider.SetMemberSkin(
        //            ms.ID,
        //            (personalityPicture != null ? "PersonalityPicture" : "PersonalityBackgroundPicture"),
        //            memberID);
        //    }
        //    else
        //    {
        //        String personalityPicture = "";
        //        String personalityBackgroundPicture = "";

        //        switch (pictureType)
        //        {
        //            case "PersonalityPicture":
        //                personalityPicture = photo.FileName;
        //                break;
        //            case "PersonalityBackgroundPicture":
        //                personalityBackgroundPicture = photo.FileName;
        //                break;
        //            default: break;
        //        }

        //        result = BiZ.Member.MemberSkin.MemberSkinProvider.AddMemberSkin(
        //            memberID,
        //            BiZ.Comm.UserType.Member,
        //            personalityPicture,
        //            personalityBackgroundPicture);

        //        BiZ.Member.MemberSkin.MemberSkin ms2 = BiZ.Member.MemberSkin.MemberSkinProvider.GetMemberSkin(null, memberID);
        //        result = BiZ.Member.MemberSkin.MemberSkinProvider.SetMemberSkin(
        //            ms2.ID,
        //            (personalityPicture != "" ? "PersonalityPicture" : "PersonalityBackgroundPicture"),
        //            memberID);
        //    }

        //    if (result.ok)
        //        return Content("{\"ok\":\"true\"}");
        //    else
        //        return Content("{\"ok\":\"false\",\"info\":\"系统维护中，给您带来了不便，请谅解！\"}");
        //}
        /// <summary>
        /// 设置用户皮肤个性图片
        /// </summary>
        /// <param name="memberId">用户编号</param>
        /// <param name="photoPath">图片路径</param>
        /// <param name="skinType">皮肤类型</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult SetMemberSkinPicture(
            String memberId, 
            String photoPath, 
            BiZ.Member.MemberSkin.MemberSkinType skinType)
        {
            CBB.ExceptionHelper.OperationResult result = null;

            String personalityPicture = "";
            String personalityBackgroundPicture = "";

            switch (skinType)
            {
                case BiZ.Member.MemberSkin.MemberSkinType.PersonalityPicture:
                    personalityPicture = photoPath;
                    break;
                case BiZ.Member.MemberSkin.MemberSkinType.PersonalityBackgroundPicture:
                    personalityBackgroundPicture = photoPath;
                    break;
                default: break;
            }
            //按用户编号获取用户皮肤
            BiZ.Member.MemberSkin.MemberSkin ms = BiZ.Member.MemberSkin.MemberSkinProvider.GetEmptyMemberSkin(null, memberId);
            ////如果用户皮肤存在
            //if (ms != null)
            //{
            //    if ((ms.PersonalityPicture == null || ms.PersonalityPicture == "") && (ms.PersonalityBackgroundPicture != null || ms.PersonalityBackgroundPicture != "") && (personalityPicture != null && personalityPicture != "") && (personalityBackgroundPicture == null && personalityBackgroundPicture == ""))
            //    {
            //        //更新用户皮肤
            //        result = BiZ.Member.MemberSkin.MemberSkinProvider.UpdateMemberSkin(
            //            ms.ID,
            //            memberId,
            //            personalityPicture,
            //            personalityBackgroundPicture);
            //        //设置用户皮肤
            //        result = BiZ.Member.MemberSkin.MemberSkinProvider.SetMemberSkin(
            //            ms.ID,
            //            (personalityPicture != null ? "PersonalityPicture" : "PersonalityBackgroundPicture"),
            //            memberId);
            //    }
            //    else if ((ms.PersonalityPicture != null || ms.PersonalityPicture != "") && (ms.PersonalityBackgroundPicture == null || ms.PersonalityBackgroundPicture == "") && (personalityPicture == null && personalityPicture == "") && (personalityBackgroundPicture != null && personalityBackgroundPicture != ""))
            //    {
            //        //更新用户皮肤
            //        result = BiZ.Member.MemberSkin.MemberSkinProvider.UpdateMemberSkin(
            //            ms.ID,
            //            memberId,
            //            personalityPicture,
            //            personalityBackgroundPicture);
            //        //设置用户皮肤
            //        result = BiZ.Member.MemberSkin.MemberSkinProvider.SetMemberSkin(
            //            ms.ID,
            //            (personalityPicture != null ? "PersonalityPicture" : "PersonalityBackgroundPicture"),
            //            memberId);
            //    }
            //    else
            //    {
            //        //添加用户皮肤
            //        result = BiZ.Member.MemberSkin.MemberSkinProvider.AddMemberSkin(
            //            memberId,
            //            BiZ.Comm.UserType.Member,
            //            personalityPicture,
            //            personalityBackgroundPicture);
            //        //按用户编号获取用户皮肤
            //        BiZ.Member.MemberSkin.MemberSkin ms2 = BiZ.Member.MemberSkin.MemberSkinProvider.GetMemberSkin(null, memberId);
            //        //设置用户皮肤
            //        result = BiZ.Member.MemberSkin.MemberSkinProvider.SetMemberSkin(
            //            ms2.ID,
            //            (personalityPicture != null ? "PersonalityPicture" : "PersonalityBackgroundPicture"),
            //            memberId);
            //    }
            //}
            //else
            //{

            ////添加用户皮肤
            //BiZ.Member.MemberSkin.MemberSkin ms = BiZ.Member.MemberSkin.MemberSkinProvider.AddMemberSkin(
            //    memberId,
            //    BiZ.Comm.UserType.Member,
            //    personalityPicture,
            //    personalityBackgroundPicture);
            //if (ms != null)
            //    result = new CBB.ExceptionHelper.OperationResult(true);
            //else
            //    result = new CBB.ExceptionHelper.OperationResult(false);

            //按用户编号获取用户皮肤
            BiZ.Member.MemberSkin.MemberSkin ms2 = BiZ.Member.MemberSkin.MemberSkinProvider.GetMemberSkin(ms.ID, memberId);
            //设置用户皮肤
            result = BiZ.Member.MemberSkin.MemberSkinProvider.SetMemberSkin(
                ms2.ID,
                ((personalityPicture != null && personalityPicture != "") ? "PersonalityPicture" : "PersonalityBackgroundPicture"),
                memberId);

            //}
            return result;
        }
        #endregion
    } 
}
