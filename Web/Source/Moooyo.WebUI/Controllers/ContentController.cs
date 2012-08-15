using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System.Configuration;
using System.Web.Security;


namespace Moooyo.WebUI.Controllers
{
    public class ContentController : Controller
    {
        public void SetMetasVersion()
        {
            ViewData["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            ViewData["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            ViewData["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            ViewData["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }

        //获取内容对象的模型
        public Models.PageModels.Content.ContentModel getContentModel(BiZ.Content.PublicContent contentobj)
        {
            Common.Content.TypeNameAndLikeNameModel typeNameModel = Common.Content.ContentNamesDefs.GetDefs();
            string typename = "";
            string likename = "";
            if (contentobj.ContentType == BiZ.Content.ContentType.Image)
            {
                String type = ((Moooyo.BiZ.Content.ImageContent)contentobj).Type;
                foreach (var typeobj in typeNameModel.imageTypes.ToList())
                {
                    if (typeobj.Split(',')[0] == type || typeobj.Split(',')[1] == type)
                    {
                        typename = typeobj.Split(',')[1];
                        likename = typeobj.Split(',')[2];
                    }
                }
                if (typename == "") { typename = type; likename = "喜欢"; }
            }
            if (contentobj.ContentType == BiZ.Content.ContentType.SuiSuiNian)
            {
                String type = ((Moooyo.BiZ.Content.SuiSuiNianContent)contentobj).Type;
                foreach (var typeobj in typeNameModel.suisuinianTypes.ToList())
                {
                    if (typeobj.Split(',')[0] == type || typeobj.Split(',')[1] == type)
                    {
                        typename = typeobj.Split(',')[1];
                        likename = typeobj.Split(',')[2];
                    }
                }
                if (typename == "") { typename = type; likename = "喜欢"; }
            }
            if (contentobj.ContentType == BiZ.Content.ContentType.CallFor)
            {
                String type = ((Moooyo.BiZ.Content.CallForContent)contentobj).Type;
                foreach (var typeobj in typeNameModel.callforTypes.ToList())
                {
                    if (typeobj.Split(',')[0] == type || typeobj.Split(',')[1] == type)
                    {
                        typename = typeobj.Split(',')[1];
                        likename = typeobj.Split(',')[2];
                    }
                }
                if (typename == "") { typename = type; likename = "喜欢"; }
            }
            if (contentobj.ContentType == BiZ.Content.ContentType.Interest)
            {
                String type = ((Moooyo.BiZ.Content.InterestContent)contentobj).Type;
                foreach (var typeobj in typeNameModel.interestTypes.ToList())
                {
                    if (typeobj.Split(',')[0] == type || typeobj.Split(',')[1] == type)
                    {
                        typename = typeobj.Split(',')[1];
                        likename = typeobj.Split(',')[2];
                    }
                }
                if (typename == "") { typename = type; likename = "喜欢"; }
            }
            if (contentobj.ContentType == BiZ.Content.ContentType.Member)
            {
                String type = ((Moooyo.BiZ.Content.MemberContent)contentobj).Type;
                foreach (var typeobj in typeNameModel.memberTypes.ToList())
                {
                    if (typeobj.Split(',')[0] == type || typeobj.Split(',')[1] == type)
                    {
                        typename = typeobj.Split(',')[1];
                        likename = typeobj.Split(',')[2];
                    }
                }
                if (typename == "") { typename = type; likename = "喜欢"; }
            }
            if (contentobj.ContentType == BiZ.Content.ContentType.InterView)
            {
                typename = "访谈";
                likename = "访谈";
            }
            Models.PageModels.Content.ContentModel model = new Models.PageModels.Content.ContentModel();
            model.contentobj = contentobj;
            model.contenttype = contentobj.ContentType.ToString();
            model.typename = typename;
            model.likename = likename;
            return model;
        }

        //获取图片显示的布局
        public Models.PageModels.Content.ImageLayOutTypeModel getImageLayOutTypeModel()
        {
            Models.PageModels.Content.ImageLayOutTypeModel model = new Models.PageModels.Content.ImageLayOutTypeModel();
            Dictionary<BiZ.Content.ImageLayoutType, List<string>> layOutTypeList = new Dictionary<BiZ.Content.ImageLayoutType, List<string>>();
            List<string> one_one = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("one_one").Split('|').ToList();
            List<string> two_one = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("two_one").Split('|').ToList();
            List<string> two_two = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("two_two").Split('|').ToList();
            List<string> three_one = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("three_one").Split('|').ToList();
            List<string> three_two = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("three_two").Split('|').ToList();
            List<string> four_one = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("four_one").Split('|').ToList();
            List<string> four_two = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("four_two").Split('|').ToList();
            List<string> four_three = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("four_three").Split('|').ToList();
            List<string> four_four = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("four_four").Split('|').ToList();
            List<string> five_one = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("five_one").Split('|').ToList();
            List<string> five_two = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("five_two").Split('|').ToList();
            List<string> five_three = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("five_three").Split('|').ToList();
            layOutTypeList.Add(BiZ.Content.ImageLayoutType.one_one, one_one);
            layOutTypeList.Add(BiZ.Content.ImageLayoutType.two_one, two_one);
            layOutTypeList.Add(BiZ.Content.ImageLayoutType.two_two, two_two);
            layOutTypeList.Add(BiZ.Content.ImageLayoutType.three_one, three_one);
            layOutTypeList.Add(BiZ.Content.ImageLayoutType.three_two, three_two);
            layOutTypeList.Add(BiZ.Content.ImageLayoutType.four_one, four_one);
            layOutTypeList.Add(BiZ.Content.ImageLayoutType.four_two, four_two);
            layOutTypeList.Add(BiZ.Content.ImageLayoutType.four_three, four_three);
            layOutTypeList.Add(BiZ.Content.ImageLayoutType.four_four, four_four);
            layOutTypeList.Add(BiZ.Content.ImageLayoutType.five_one, five_one);
            layOutTypeList.Add(BiZ.Content.ImageLayoutType.five_two, five_two);
            layOutTypeList.Add(BiZ.Content.ImageLayoutType.five_three, five_three);
            model.layOutTypeList = layOutTypeList;
            return model;
        }

        //获取用户转换后的编号：memberID-用户编号
        public String getMemberConvertedID(String memberID)
        {
            BiZ.Comm.UniqueNumber.UniqueNumber uNumberID2 = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetConvertedMemberID(memberID, BiZ.Comm.UniqueNumber.IDType.MemberID);
            if (uNumberID2 != null)
            {
                return uNumberID2.ConvertedID.ToString();
            }
            else
            {
                return "";
            }
        }

        //根据内容类型获取内容类别：contentType-内容类型
        public List<string> getContentType(BiZ.Content.ContentType contentType)
        {
            Common.Content.TypeNameAndLikeNameModel typeNameModel = Common.Content.ContentNamesDefs.GetDefs();
            List<String> types = new List<string>();
            switch (contentType)
            {
                case BiZ.Content.ContentType.Interest: types = typeNameModel.interestTypes; break;
                case BiZ.Content.ContentType.Member: types = typeNameModel.memberTypes; break;
                case BiZ.Content.ContentType.Image: types = typeNameModel.imageTypes; break;
                case BiZ.Content.ContentType.SuiSuiNian: types = typeNameModel.suisuinianTypes; break;
                case BiZ.Content.ContentType.CallFor: types = typeNameModel.callforTypes; break;
            }
            return types;
        }

        //内容详细页：内容ID
        public ActionResult ContentDetail(string id)
        {

            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;

            //获取内容
            BiZ.Content.PublicContent content = new BiZ.Content.PublicContent().getPublicContent(id);
            string memberID = "";
            if (content != null)
                memberID = content.Creater.MemberID;

            if (content == null) RedirectToAction("Err", "Err", new { errorno = "404" });

            if (content.DeleteFlag == BiZ.Comm.DeletedFlag.Yes)
                return RedirectToAction("TaContent/" + memberID + "/all/1", "Content");

            if (userID != null && userID != "" && memberID != null && memberID != "")
            {
                #region 是否浏览自己的主页
                bool isme = memberID == userID;
                if (!isme)
                {
                    //增加来访
                    BiZ.Member.Activity.ActivityController.VisitMember(userID, memberID);
                }
                #endregion
            }

            //获取当前登录的用户对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(content.Creater.MemberID);

            Models.PageModels.Content.ContentModel model = new Models.PageModels.Content.ContentModel();
            model.UserID = userID;
            model.MemberID = content.Creater.MemberID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = memberDisplayObj;
            model.contenttype = content.ContentType.ToString();
            model.contentobj = content;

            return View(model);

        }

        #region 我自己的内容
        /// <summary>
        /// 我自己的页面
        /// </summary>
        /// <param name="contenttype">内容类型</param>
        /// <param name="pn">页数</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult IContent(string contenttype, string pn)
        {
            //清空时间轴集合
            if (Session["timelist"] != null)
                Session.Remove("timelist");
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(userID);
            #region 内测期跳转检测
            if (!mym.AllowLogin)
            {
                return RedirectToAction("Welcome", "Account");
            }
            #endregion
            userID = userID == "" ? null : userID;
            //用户必须登录才能打开该页面
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });

            contenttype = contenttype == null || contenttype == "" || contenttype == "all" ? "" : contenttype;
            int bigpagesize = 30;
            int smailpagesize = 10;
            int pageno = pn == null || int.Parse(pn) == 0 ? 1 : int.Parse(pn);
            int pagesize = pageno == 1 ? smailpagesize : bigpagesize;
            Models.PageModels.Content.IContentModel model = getIContentModel(userID, contenttype, pagesize, pageno);

            model.Pagger = new Models.PaggerObj();
            int pages = (int)Math.Ceiling((double)model.contentCount / bigpagesize);
            model.Pagger.PageCount = pages;
            model.Pagger.PageSize = bigpagesize;
            model.Pagger.PageNo = pageno;
            model.Pagger.PageUrl = "/Content/IContent" + (contenttype == "" ? "/all" : "/" + contenttype);
            ViewData["contenttype"] = contenttype;
            ViewData["memberUrl"] = getMemberConvertedID(userID);
            return View(model);
        }
        /// <summary>
        /// 我自己的页面小分页的Ajax内容获取
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="pageno">小分页的页数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IContentToAjax(int pageno, string contenttype)
        {
            String userID = User.Identity.Name;
            contenttype = contenttype == null || contenttype == "" || contenttype == "all" ? "" : contenttype;
            Models.PageModels.Content.IContentModel model = getIContentModel(userID, contenttype, 10, pageno);
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        /// <summary>
        /// 我自己的页面的数据模型获取
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页数</param>
        /// <returns></returns>
        public Models.PageModels.Content.IContentModel getIContentModel(string userID, string contenttype, int pagesize, int pageno)
        {
            //获取当前登录的用户对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            //获取内容集合
            IList<BiZ.Content.PublicContent> MyContentList = BiZ.Content.ContentProvider.findForMember(userID, userID, contenttype, BiZ.Comm.DeletedFlag.No, pageno, pagesize);

            Models.PageModels.Content.IContentModel model = new Models.PageModels.Content.IContentModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = memberDisplayObj;
            model.MyContentList = MyContentList;
            model.contentCount = BiZ.Content.ContentProvider.findForMemberCount(userID, userID, contenttype, BiZ.Comm.DeletedFlag.No);
            model.namesModel = Common.Content.ContentNamesDefs.GetDefs();
            return model;
        }
        #endregion

        #region 我关注的人的内容
        /// <summary>
        /// 我关注的人的内容
        /// </summary>
        /// <param name="contenttype">内容类型</param>
        /// <param name="pn">页数</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult IFavorerContent(string contenttype, string pn)
        {
            //清空时间轴集合
            if (Session["timelist"] != null)
                Session.Remove("timelist");
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(userID);
            #region 内测期跳转检测
            if (!mym.AllowLogin)
            {
                return RedirectToAction("Welcome", "Account");
            }
            #endregion
            userID = userID == "" ? null : userID;
            //用户必须登录才能打开该页面
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });

            contenttype = contenttype == null || contenttype == "" || contenttype == "all" ? "" : contenttype;
            int bigpagesize = 30;
            int smailpagesize = 10;
            int pageno = pn == null || int.Parse(pn) == 0 ? 1 : int.Parse(pn);
            int pagesize = pageno == 1 ? smailpagesize : bigpagesize;
            Models.PageModels.Content.IFavorerContentModel model = getIFavorerContentModel(userID, contenttype, pagesize, pageno);

            model.Pagger = new Models.PaggerObj();
            int pages = (int)Math.Ceiling((double)model.contentCount / bigpagesize);
            model.Pagger.PageCount = pages;
            model.Pagger.PageSize = bigpagesize;
            model.Pagger.PageNo = pageno;
            model.Pagger.PageUrl = "/Content/IFavorerContent" + (contenttype == "" ? "/all" : "/" + contenttype);
            ViewData["contenttype"] = contenttype;

            //用户引导
            ViewData["guideShowed"] = BiZ.Guide.GuideDataProvider.CheckGuide(new BiZ.Guide.GuideData("IFavorerContentV1", userID));

            return View(model);
        }
        /// <summary>
        /// 我关注的页面小分页的Ajax内容获取
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="pageno">小分页的页数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IFavorerContentToAjax(int pageno, string contenttype)
        {
            String userID = User.Identity.Name;
            contenttype = contenttype == null || contenttype == "" || contenttype == "all" ? "" : contenttype;
            Models.PageModels.Content.IFavorerContentModel model = getIFavorerContentModel(userID, contenttype, 10, pageno);
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        /// <summary>
        /// 我关注的人的内容的数据模型获取
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页数</param>
        /// <returns></returns>
        public Models.PageModels.Content.IFavorerContentModel getIFavorerContentModel(string userID, string contenttype, int pagesize, int pageno)
        {
            //获取当前登录的用户对象
            Models.MemberFullDisplayObj memberDisplayObj = Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            //获取我关注的人的内容集合
            IList<BiZ.Content.PublicContent> MyContentList = BiZ.Content.ContentProvider.findForMenberToFavorer(userID, contenttype, BiZ.Comm.DeletedFlag.No, pageno, pagesize);
            //获取我关注的人的集合(按我对他们的访问数量排序)
            IList<BiZ.Member.Relation.Favorer> favorerList = BiZ.Member.Relation.RelationProvider.GetFavorersToVisitCount(userID, 0, 0);
            //将我关注的人的集合转换成可用的集合
            IList<BiZ.Creater.Creater> favorerMemberList = new List<BiZ.Creater.Creater>();
            foreach (var obj in favorerList)
                favorerMemberList.Add(new BiZ.Creater.Creater(obj.ToMember));

            Models.PageModels.Content.IFavorerContentModel model = new Models.PageModels.Content.IFavorerContentModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = memberDisplayObj;
            model.ContentList = MyContentList;
            model.favorerMemberList = favorerMemberList;
            model.contentCount = BiZ.Content.ContentProvider.findForMenberToFavorerCount(userID, contenttype, BiZ.Comm.DeletedFlag.No);
            model.namesModel = Common.Content.ContentNamesDefs.GetDefs();
            return model;
        }
        #endregion

        #region TA的内容
        /// <summary>
        /// TA的页面
        /// </summary>
        /// <param name="memberID">浏览的用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="pn">页数</param>
        /// <returns></returns>
        public ActionResult TaContent(string memberID, string contenttype, string pn)
        {
            //清空时间轴集合
            if (Session["timelist"] != null)
                Session.Remove("timelist");
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;

            ////按转换后的编号和编号类型获取原始的编号
            //BiZ.Comm.UniqueNumber.UniqueNumber trfmId = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetDefaultId(memberID, BiZ.Comm.UniqueNumber.IDType.MemberID);
            ////如果存在该转换后的编号，就将对应的默认编号赋给用户编号。
            //if (trfmId != null) memberID = trfmId.DefaultId;
            if (userID != null && userID != "" && memberID != null && memberID != "")
            {
                #region 是否浏览自己的主页
                bool isme = memberID == userID;
                if (!isme)
                {
                    //增加来访
                    BiZ.Member.Activity.ActivityController.VisitMember(userID, memberID);
                }
                #endregion
            }
            if (memberID == null || memberID == "")
                return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });

            //如果点击的是自己，则跳转到我自己的页面
            if (userID == memberID)
                return RedirectToAction("IContent/all/1", "Content");

            if (userID != null && userID != "" && memberID != null && memberID != "")
            {
                //如果他是我关注的人，增加我对他的访问数量
                if (BiZ.Member.Relation.RelationProvider.IsInFavor(userID, memberID))
                {
                    BiZ.Member.Relation.RelationProvider.UpdateFavorerVisitCount(userID, memberID);
                }
            }

            contenttype = contenttype == null || contenttype == "" || contenttype == "all" ? "" : contenttype;
            int bigpagesize = 30;
            int smailpagesize = 10;
            int pageno = pn == null || int.Parse(pn) == 0 ? 1 : int.Parse(pn);
            int pagesize = pageno == 1 ? smailpagesize : bigpagesize;
            Models.PageModels.Content.TaContentModel model = getTaContentModel(userID, memberID, contenttype, pagesize, pageno);

            model.Pagger = new Models.PaggerObj();
            double pages = model.contentCount % bigpagesize == 0 ? model.contentCount / bigpagesize : model.contentCount / bigpagesize + 1;
            model.Pagger.PageCount = int.Parse(pages.ToString());
            model.Pagger.PageSize = bigpagesize;
            model.Pagger.PageNo = pageno;
            model.Pagger.PageUrl = "/Content/TaContent/" + memberID + (contenttype == "" ? "/all" : "/" + contenttype);
            ViewData["contenttype"] = contenttype;
            ViewData["memberUrl"] = getMemberConvertedID(memberID);
            ViewData["memberId"] = memberID;
            return View(model);
        }
        /// <summary>
        /// TA的页面小分页的Ajax内容获取
        /// </summary>
        /// <param name="pageno">页数</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="memberID">Ta的编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TaContentToAjax(int pageno, string contenttype, string memberID)
        {
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            contenttype = contenttype == null || contenttype == "" || contenttype == "all" ? "" : contenttype;
            Models.PageModels.Content.TaContentModel model = getTaContentModel(userID, memberID, contenttype, 10, pageno);
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        /// <summary>
        /// TA的页面的数据模型获取
        /// </summary>
        /// <param name="userID">当前登录的用户编号</param>
        /// <param name="memberID">被浏览的用户编号</param>
        /// <param name="contenttype">内容类型</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页数</param>
        /// <returns></returns>
        public Models.PageModels.Content.TaContentModel getTaContentModel(string userID, string memberID, string contenttype, int pagesize, int pageno)
        {
            //判断当前登录的用户是不是被浏览的用户的粉丝
            Boolean isfans = BiZ.Member.Relation.RelationProvider.IsInFavor(userID, memberID);
            //获取被浏览的对象的内容集合
            IList<BiZ.Content.PublicContent> TaContentList = BiZ.Content.ContentProvider.findForMember(userID, memberID, contenttype, BiZ.Comm.DeletedFlag.No, pageno, pagesize);
            //获取被浏览的用户的兴趣集合
            IList<BiZ.InterestCenter.Interest> interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(memberID, 6, 1);
            //按原始编号和编号类型获取转换后的编号
            long ConvertedID = 0;
            BiZ.Comm.UniqueNumber.UniqueNumber uniqueNumber = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetConvertedMemberID(memberID, BiZ.Comm.UniqueNumber.IDType.MemberID);
            if (uniqueNumber != null)
                ConvertedID = uniqueNumber.ConvertedID;

            Models.PageModels.Content.TaContentModel model = new Models.PageModels.Content.TaContentModel();
            model.UserID = userID;
            model.MemberID = memberID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = Models.DisplayObjProvider.getMemberFullDisplayObj(memberID);
            model.TaContentList = TaContentList;
            model.interestList = interestList;
            model.contentCount = BiZ.Content.ContentProvider.findForMemberCount(userID, memberID, contenttype, BiZ.Comm.DeletedFlag.No);
            model.isfans = isfans;
            model.namesModel = Common.Content.ContentNamesDefs.GetDefs();
            return model;
        }
        #endregion

        #region 登录前的首页
        /// <summary>
        /// 登录前的首页(不登陆也可以访问)
        /// </summary>
        /// <param name="interestID">兴趣编号</param>
        /// <param name="city">城市</param>
        /// <param name="sex">性别</param>
        /// <param name="pn">页数</param>
        /// <returns></returns>
        public ActionResult LoginIndexContent(string contenttype, string pn)
        {
            //清空时间轴集合
            if (Session["timelist"] != null)
                Session.Remove("timelist");
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;

            contenttype = contenttype == null || contenttype == "" || contenttype == "all" ? "" : contenttype;
            int bigpagesize = 30;
            int pageno = pn == null || int.Parse(pn) == 0 ? 1 : int.Parse(pn);
            Models.PageModels.Content.IndexContentModel model = getLoginIndexContentModel(userID, contenttype, bigpagesize, pageno);
            model.Pagger = new Models.PaggerObj();
            double pages = model.contentCount % bigpagesize == 0 ? model.contentCount / bigpagesize : model.contentCount / bigpagesize + 1;
            model.Pagger.PageCount = int.Parse(pages.ToString());
            model.Pagger.PageSize = bigpagesize;
            model.Pagger.PageNo = pageno;
            model.Pagger.PageUrl = "/Content/LoginIndexContent/" + (contenttype == "" ? "/all" : "/" + contenttype);
            ViewData["contenttype"] = contenttype;
            return View(model);
        }
        /// <summary>
        /// 登录前的首页的数据模型获取
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页数</param>
        /// <returns></returns>
        public Models.PageModels.Content.IndexContentModel getLoginIndexContentModel(string userID, string contenttype, int pagesize, int pageno)
        {
            IList<BiZ.Like.AdminLikeData> syslikelist = BiZ.Like.LikeDataFactory.GetLike<BiZ.Like.AdminLikeData>(null, null, BiZ.Like.LikeType.Content, BiZ.Like.AdminLikeData.GetCollectionName());
            var groupobjs = (from obj in syslikelist group obj by new { obj.ToId } into g select new { id = g.Key.ToId });
            string[] ids = null;
            if (syslikelist != null && groupobjs.Count() > 0)
            {
                ids = new string[groupobjs.Count()];
                int i = 0;
                foreach (var obj in groupobjs)
                {
                    ids[i] = obj.id;
                    i++;
                }
            }
            //获取内容集合
            IList<BiZ.Content.PublicContent> ContentList = BiZ.Content.ContentProvider.findContentForSysLike(ids, contenttype, BiZ.Comm.DeletedFlag.No, pageno, pagesize);
            //获取图片内容集合
            IList<BiZ.Content.PublicContent> imageContentList = BiZ.Content.ContentProvider.findContentForSysLike(ids, BiZ.Content.ContentType.Image.GetHashCode().ToString(), BiZ.Comm.DeletedFlag.No, 0, 0);
            //获取顶部推送内容集合
            List<BiZ.TopImagePush.ImagePush> imagePushList = BiZ.TopImagePush.ImagePushProvider.findAll(BiZ.Comm.DeletedFlag.No, 0, 0);
            //随机从顶部推送内容集合中取一个内容对象
            BiZ.TopImagePush.ImagePush imagePush = null;
            if (imagePushList.Count > 0)
            {
                Random pushrann = new Random();
                imagePush = imagePushList[pushrann.Next(imagePushList.Count - 1)];
            }

            Models.PageModels.Content.IndexContentModel model = new Models.PageModels.Content.IndexContentModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = userID != null && userID != "" ? Models.DisplayObjProvider.getMemberFullDisplayObj(userID) : null;
            model.ContentList = ContentList;
            model.imageContentList = imageContentList;
            model.imagePush = imagePush;
            model.contentCount = BiZ.Content.ContentProvider.findContentForSysLikeCount(ids, contenttype, BiZ.Comm.DeletedFlag.No);
            return model;
        }

        #endregion

        #region 登录后的首页
        /// <summary>
        /// 登录后的首页(不登陆也可以访问)
        /// </summary>
        /// <param name="interestID">兴趣编号</param>
        /// <param name="city">城市</param>
        /// <param name="sex">性别</param>
        /// <param name="pn">页数</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult IndexContent(string interestID, string city, string sex, string pn, String pushimg)
        {
            ViewData["pushImage"] = pushimg;

            //清空时间轴集合
            if (Session["timelist"] != null)
                Session.Remove("timelist");
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            //未登陆状态用户id未空
            userID = userID == "" ? null : userID;
            if (userID != null && userID != "")
            {
                BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(userID);
                #region 内测期跳转检测
                if (!mym.AllowLogin)
                    return RedirectToAction("Welcome", "Account");
                #endregion
            }

            interestID = interestID == null || interestID == "" || interestID == "all" ? "" : interestID;
            city = city == null || city == "" || city == "all" ? "" : city;
            sex = sex == null || sex == "" || sex == "all" ? "" : sex;

            String john_memberid = Common.Comm.GetCookie("john_memberid");
            if (john_memberid != null && john_memberid != "" && john_memberid != userID)
            {
                Common.Comm.RemoveCookie("john_memberid");
                Common.Comm.RemoveCookie("john_interestID");
                Common.Comm.RemoveCookie("john_city");
                Common.Comm.RemoveCookie("john_sex");
            }
            if (interestID == "")
            {
                String interestIDCookie = Common.Comm.GetCookie("john_interestID");
                if (interestIDCookie != null && interestIDCookie != "")
                {
                    interestID = interestIDCookie;
                    Common.Comm.RemoveCookie("john_interestID");
                }
            }
            if (city == "")
            {
                String cityCookie = Common.Comm.GetCookie("john_city");
                if (cityCookie != null && cityCookie != "")
                {
                    city = cityCookie;
                    Common.Comm.RemoveCookie("john_city");
                }
            }
            if (sex == "")
            {
                String sexCookie = Common.Comm.GetCookie("john_sex");
                if (sexCookie != null && sexCookie != "")
                {
                    sex = sexCookie;
                    Common.Comm.RemoveCookie("john_sex");
                }
            }

            int bigpagesize = 30;
            int smailpagesize = 10;
            int pageno = pn == null || int.Parse(pn) == 0 ? 1 : int.Parse(pn);
            int pagesize = pageno == 1 ? smailpagesize : bigpagesize;
            Models.PageModels.Content.IndexContentModel model = getIndexContentModel(userID, interestID, city, sex, pagesize, pageno);
            if (model.interestList.Count <= 0)
                return RedirectToAction("RegAddInterest", "Register");

            if (ViewData["interestID"] != null)
            {
                interestID = ViewData["interestID"].ToString();
                Common.Comm.SetCookie("john_interestID", interestID, Common.CookieOrSessionExpiresTime.OneYear);
            }
            if (ViewData["city"] != null)
            {
                city = ViewData["city"].ToString();
                Common.Comm.SetCookie("john_city", city, Common.CookieOrSessionExpiresTime.OneYear);
            }
            if (ViewData["sex"] != null)
            {
                sex = ViewData["sex"].ToString();
                Common.Comm.SetCookie("john_sex", sex, Common.CookieOrSessionExpiresTime.OneYear);
            }
            Common.Comm.SetCookie("john_memberid", userID, Common.CookieOrSessionExpiresTime.OneYear);

            model.Pagger = new Models.PaggerObj();
            int pages = (int)Math.Ceiling((double)model.contentCount / bigpagesize);
            model.Pagger.PageCount = pages;
            model.Pagger.PageSize = bigpagesize;
            model.Pagger.PageNo = pageno;
            model.Pagger.PageUrl = "/Content/IndexContent" + (interestID == "" ? "/all" : "/" + interestID) + (city == "" ? "/all" : "/" + city) + (sex == "" ? "/all" : "/" + sex);

            //用户引导
            ViewData["guideShowed"] = BiZ.Guide.GuideDataProvider.CheckGuide(new BiZ.Guide.GuideData("IndexContentV1", userID));

            return View(model);
        }
        /// <summary>
        /// 登录后的首页的页面小分页的Ajax内容获取
        /// </summary>
        /// <param name="pageno">页数</param>
        /// <param name="interestID">兴趣编号</param>
        /// <param name="city">城市</param>
        /// <param name="sex">性别</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IndexContentToAjax(string pageno, string interestID, string city, string sex)
        {
            if (pageno == null || pageno == "") return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (interestID == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (city == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (sex == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int pNo = 0;
            if (!int.TryParse(pageno, out pNo)) return RedirectToAction("Error", "Error", new { errorno = "0" });

            String userID = User.Identity.Name;
            //未登陆状态用户id未空
            userID = userID == "" ? null : userID;
            interestID = interestID == null || interestID == "" || interestID == "all" ? "" : interestID;
            city = city == null || city == "" || city == "all" ? "" : city;
            sex = sex == null || sex == "" || sex == "all" ? "" : sex;
            Models.PageModels.Content.IndexContentModel model = getIndexContentModel(userID, interestID, city, sex, 10, pNo);
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        /// <summary>
        /// 登录后的首页的数据模型获取
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页数</param>
        /// <returns></returns>
        public Models.PageModels.Content.IndexContentModel getIndexContentModel(string userID, string interestID, string city, string sex, int pagesize, int pageno)
        {
            //获取用户兴趣集合
            IList<BiZ.InterestCenter.Interest> interestList = GetUserInterests(userID, interestID);

            if ((interestID == null || interestID == "") && interestList.Count > 0) { interestID = interestList[0].ID; }

            //获取内容集合
            IList<BiZ.Content.PublicContent> ContentList = BiZ.Content.ContentProvider.findForSomeThing(userID, interestID, city, sex, BiZ.Comm.DeletedFlag.No, pageno, pagesize);

            //获取顶部推送内容集合
            List<BiZ.TopImagePush.ImagePush> imagePushList = BiZ.TopImagePush.ImagePushProvider.findAll(BiZ.Comm.DeletedFlag.No, 0, 0);

            //随机从顶部推送内容集合中取一个内容对象
            BiZ.TopImagePush.ImagePush imagePush = null;
            if (imagePushList.Count > 0)
            {
                Random pushrann = new Random();
                imagePush = imagePushList[pushrann.Next(imagePushList.Count - 1)];
            }

            //获取开启的城市
            string[] activityCityStr = Common.Comm.GetActivityCitys().Split(',');
            List<string> ActivityCitys = new List<string>();
            foreach (string citystr in activityCityStr)
            {
                if (citystr == "") continue;

                ActivityCitys.Add(citystr);
            }

            Models.PageModels.Content.IndexContentModel model = new Models.PageModels.Content.IndexContentModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = userID != null && userID != "" ? Models.DisplayObjProvider.getMemberFullDisplayObj(userID) : null;
            model.ContentList = ContentList;
            model.interestList = interestList;
            model.imagePush = imagePush;
            model.contentCount = BiZ.Content.ContentProvider.findForSomeThingCount(userID, interestID, city, sex, BiZ.Comm.DeletedFlag.No);
            model.ActivityCityList = ActivityCitys;
            model.namesModel = Common.Content.ContentNamesDefs.GetDefs();

            ViewData["interestID"] = interestID;
            ViewData["city"] = city;
            ViewData["sex"] = sex;

            //判断当前登录的用户是否填写城市信息，如果没有则保存当前用户所选择的城市为他的所在城市信息
            if ((userID != null && userID != "") && (city != null && city != "") && (model.Member.City == null || model.Member.City == ""))
            {
                string Province = BiZ.Sys.ZoneFactory.GetCityByName(city).Province;
                BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(userID);
                mym.MemberInfomation.City = Province + "|" + city;
                BiZ.MemberManager.MemberManager.SaveMember(mym);
            }
            return model;
        }
        /// <summary>
        /// 排序内容
        /// </summary>
        /// <param name="orginalList">内容集合</param>
        /// <returns></returns>
        public IList<BiZ.Content.PublicContent> GetOrderedPublicContent(IList<BiZ.Content.PublicContent> orginalList)
        {
            //构建用于获取内容对象的字典
            Dictionary<string, BiZ.Content.PublicContent> contentDict = new Dictionary<string, BiZ.Content.PublicContent>();
            //排序类
            Common.HolderOrder holderOrder = new Common.HolderOrder();
            foreach (BiZ.Content.PublicContent content in orginalList)
            {
                contentDict.Add(content.ID, content);
                int spaceWidth = 2;
                if (content.ContentType == BiZ.Content.ContentType.InterView)
                {
                    Moooyo.BiZ.Content.InterViewContent interview = (Moooyo.BiZ.Content.InterViewContent)content;
                    if (interview.AnswerCount > 2) spaceWidth = 3;
                }
                else if (content.ContentType == BiZ.Content.ContentType.Member)
                {
                    spaceWidth = 1;
                }
                holderOrder.AddContent(content.ID, spaceWidth);
            }
            //排序后的list
            List<BiZ.Content.PublicContent> newlist = new List<BiZ.Content.PublicContent>();
            IList<string> orderedIDs = holderOrder.GetOrderedContentIDs();
            foreach (string id in orderedIDs)
            {
                newlist.Add(contentDict[id]);
            }
            return newlist;
        }
        /// <summary>
        /// 获取用户兴趣集合,未登陆时用户ID传入null，则返回系统兴趣编号
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="interestID">兴趣编号</param>
        /// <returns></returns>
        private static IList<BiZ.InterestCenter.Interest> GetUserInterests(string userID, string interestID)
        {
            IList<BiZ.InterestCenter.Interest> interestList;
            //获取兴趣集合
            ////如果用户未登陆
            ////获取系统自定义的兴趣编号集合
            //if (userID == null || userID == "")
            //{
            //    interestList = BiZ.InterestCenter.InterestFactory.GetInterest(GetSysInterests().Split(','));
            //}
            //else
            //{
            interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userID, 0, 0);
            ////如果未获取到用户兴趣列表，则取系统定义的兴趣
            //if (interestList.Count == 0)
            //    interestList = BiZ.InterestCenter.InterestFactory.GetInterest(GetSysInterests().Split(','));
            //}
            if (interestList.Count > 0)
            {
                //如果未指定兴趣，则设置第一个兴趣编号未默认兴趣
                if (interestID == null || interestID == "")
                {
                    if (interestList.Count > 0)
                        interestID = interestList[0].ID;
                }
                //排列兴趣顺序，将指定的兴趣排在最前
                BiZ.InterestCenter.Interest interestobj = null;
                for (int i = 0; i < interestList.Count; i++)
                {
                    if (interestList[i].ID == interestID)
                    {
                        interestobj = interestList[i];
                        interestList.RemoveAt(i);
                        break;
                    }
                }
                interestList.Insert(0, interestobj);
            }
            return interestList;
        }
        /// <summary>
        /// 获取系统定义的未登陆用户使用的兴趣列表
        /// </summary>
        /// <returns></returns>
        private static String GetSysInterests()
        {
            return CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SysTemPushInterestIDs");
        }
        #endregion

        //内容控件To登录后：contentobj-内容对象；ifshowname-是否显示用户昵称；ifshowICONPath-是否显示用户头像
        public ActionResult ContentItem(BiZ.Content.PublicContent contentobj, Boolean ifshowmember, Boolean ifmy)
        {
            SetMetasVersion();
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            Models.PageModels.Content.ContentModel model = getContentModel(contentobj);
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = userID != null && userID != "" ? Models.DisplayObjProvider.getMemberFullDisplayObj(userID) : null;
            model.ifshowmember = ifshowmember;
            model.ifmy = ifmy;
            model.imageLayOutModel = getImageLayOutTypeModel();
            return View(model);
        }

        //内容控件To登录前：contentobj-内容对象；ifshowname-是否显示用户昵称；ifshowICONPath-是否显示用户头像
        public ActionResult ContentItemToIndex(BiZ.Content.PublicContent contentobj, Boolean ifshowmember, Boolean ifmy)
        {
            SetMetasVersion();
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            Models.PageModels.Content.ContentModel model = getContentModel(contentobj);
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID != null && userID != "" ? true : false;
            model.Member = userID != null && userID != "" ? Models.DisplayObjProvider.getMemberFullDisplayObj(userID) : null;
            model.ifshowmember = ifshowmember;
            model.ifmy = ifmy;
            return View(model);
        }

        #region 内容发布
        /// <summary>
        /// 发布图片内容
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddImageContent(String contentID)
        {
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            //用户必须登录才能打开该页面
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });

            Models.PageModels.Content.AddContentModel model = new Models.PageModels.Content.AddContentModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID == null || userID == "" ? false : true;
            model.Member = userID == null || userID == "" ? null : Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            if (contentID != null && contentID != "")
            {
                model.contentObj = new BiZ.Content.PublicContent().getPublicContent(contentID);
            }
            //图片上传时要用到的图片类型
            ViewData["phototype"] = BiZ.Photo.PhotoType.ImageContentPhoto.GetHashCode();
            return View(model);
        }
        /// <summary>
        /// 发布碎碎念内容
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddSuiSuiNianContent(String contentID)
        {
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            //用户必须登录才能打开该页面
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });

            Models.PageModels.Content.AddContentModel model = new Models.PageModels.Content.AddContentModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID == null || userID == "" ? false : true;
            model.Member = userID == null || userID == "" ? null : Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            if (contentID != null && contentID != "")
            {
                model.contentObj = new BiZ.Content.PublicContent().getPublicContent(contentID);
            }
            //图片上传时要用到的图片类型
            ViewData["phototype"] = BiZ.Photo.PhotoType.SuoSuoContentPhoto.GetHashCode();
            return View(model);
        }
        /// <summary>
        /// 发布访谈内容
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddInterViewContent()
        {
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            //用户必须登录才能打开该页面
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });
            //获取当前登录用户的访谈集合
            IList<BiZ.InterView.InterView> interviewList = BiZ.InterView.InterViewProvider.GetInterViews(userID, 0, 0);

            Models.PageModels.Content.AddInterViewContentModel model = new Models.PageModels.Content.AddInterViewContentModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID == null || userID == "" ? false : true;
            model.Member = userID == null || userID == "" ? null : Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            model.myinterviewList = interviewList;
            model.sysinterviewList = getInterView(userID);
            return View(model);
        }
        /// <summary>
        /// 刷新访谈
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult reloadInterView()
        {
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            return Json(new JavaScriptSerializer().Serialize(getInterView(userID)));
        }
        /// <summary>
        /// 根据用户编号随机获取系统访谈集合
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns></returns>
        public IList<BiZ.Sys.SystemInterView> getInterView(String userID)
        {
            //获取当前登录用户的访谈集合
            IList<BiZ.InterView.InterView> interviewList = BiZ.InterView.InterViewProvider.GetInterViews(userID, 0, 0);
            //根据访谈集合创建访谈对应的系统编号的集合
            string[] alreadyanswered = new string[interviewList.Count];
            for (int i = 0; i < interviewList.Count; i++)
            {
                alreadyanswered[i] = interviewList[i].SystemQuestionID;
            }
            //获取系统访谈，除去当前用户回答过的访谈
            IList<BiZ.Sys.SystemInterView> systeminterviews = new SystemFuncController().GetSystemInterViewList("", alreadyanswered);
            return systeminterviews;
        }
        /// <summary>
        /// 发布号召内容
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddCallForContent(String contentID)
        {
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            //用户必须登录才能打开该页面
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });

            Models.PageModels.Content.AddContentModel model = new Models.PageModels.Content.AddContentModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID == null || userID == "" ? false : true;
            model.Member = userID == null || userID == "" ? null : Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            //图片上传时要用到的图片类型
            ViewData["phototype"] = BiZ.Photo.PhotoType.CallForContentPhoto.GetHashCode();
            if (contentID != null && contentID != "")
            {
                model.contentObj = new BiZ.Content.PublicContent().getPublicContent(contentID);
            }
            return View(model);
        }
        /// <summary>
        /// 发布照片内容
        /// </summary>
        /// <param name="permissions">访问权限</param>
        /// <param name="interestids">兴趣编号集合</param>
        /// <param name="Content">内容</param>
        /// <param name="imageIDs">图片编号集合</param>
        /// <param name="type">照片念类别</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertImageContent(string permissions, string interestids, string Content, string imageIDs, string layOutType, string type, string contentid)
        {
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            if (imageIDs.Split(',').Length > 1 && (layOutType == null || layOutType == ""))
            {
                return Json(new JavaScriptSerializer().Serialize(false));
            }
            if (userID != "" && permissions != "" && type != "")
            {
                //根据图片编号集合创建图片对象集合
                List<BiZ.Content.Image> imageList = new List<BiZ.Content.Image>();
                foreach (String photoid in imageIDs.Split(','))
                {
                    BiZ.Photo.Photo obj = BiZ.Photo.PhotoManager.GetPhoto(photoid);
                    if (obj != null)
                        imageList.Add(new BiZ.Content.Image(obj.FileName));
                }
                //创建图片内容对象
                BiZ.Content.ImageContent image = null;
                //修改
                if (contentid != null && contentid != "" && contentid != "null")
                {
                    image = new BiZ.Content.ImageContent(contentid);
                    image.ContentPermissions = getContentPerMissions(permissions);
                    image.InterestID = interestids.Split(',').ToList();
                    image.Content = Content;
                    image.ImageList = imageList;
                    image.LayOutType = getImageLayOutType(layOutType);
                    image.Type = type;
                    image.UpdateTime = DateTime.Now;
                }
                //添加
                else
                {
                    image = new BiZ.Content.ImageContent(userID, getContentPerMissions(permissions), interestids.Split(',').ToList(), Content, imageList, getImageLayOutType(layOutType), type);
                }
                //保存图片内容对象
                image = image.Save(image);
                //更新用户在兴趣中的活跃度
                updateInterestHot(interestids, userID);

                //增加用户动态到后台
                BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                    userID,
                    "",
                    BiZ.Sys.MemberActivity.MemberActivityType.UploadAvatar,
                    "/Content/ContentDetail/" + image.ID);

                return Json(new JavaScriptSerializer().Serialize(image));
            }
            else
                return Json(new JavaScriptSerializer().Serialize(false));
        }
        /// <summary>
        /// 发布碎碎念内容
        /// </summary>
        /// <param name="permissions">访问权限</param>
        /// <param name="interestids">兴趣编号集合</param>
        /// <param name="Content">内容</param>
        /// <param name="imageIDs">图片编号集合</param>
        /// <param name="type">碎碎念类别</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertSuiSuiNianContent(string permissions, string interestids, string Content, string imageIDs, string layOutType, string type, string contentid)
        {
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            if (imageIDs.Split(',').Length > 1 && (layOutType == null || layOutType == ""))
            {
                return Json(new JavaScriptSerializer().Serialize(false));
            }
            if (userID != "" && permissions != "" && type != "")
            {
                //根据图片编号集合创建图片对象集合
                List<BiZ.Content.Image> imageList = new List<BiZ.Content.Image>();
                foreach (String photoid in imageIDs.Split(','))
                {
                    BiZ.Photo.Photo obj = BiZ.Photo.PhotoManager.GetPhoto(photoid);
                    if (obj != null)
                        imageList.Add(new BiZ.Content.Image(obj.FileName));
                }
                //创建碎碎念内容对象
                BiZ.Content.SuiSuiNianContent suisuinian = null;
                if (contentid != null && contentid != "" && contentid != "null")
                {
                    suisuinian = new BiZ.Content.SuiSuiNianContent(contentid);
                    suisuinian.ContentPermissions = getContentPerMissions(permissions);
                    suisuinian.InterestID = interestids.Split(',').ToList();
                    suisuinian.Content = Content;
                    suisuinian.ImageList = imageList;
                    suisuinian.LayOutType = getImageLayOutType(layOutType);
                    suisuinian.Type = type;
                    suisuinian.UpdateTime = DateTime.Now;
                }
                else
                {
                    suisuinian = new BiZ.Content.SuiSuiNianContent(userID, getContentPerMissions(permissions), interestids.Split(',').ToList(), Content, imageList, getImageLayOutType(layOutType), type);
                }
                //保存碎碎念对象
                suisuinian = suisuinian.Save(suisuinian);
                //更新用户在兴趣中的活跃度
                updateInterestHot(interestids, userID);

                //增加用户动态到后台
                BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                    userID,
                    "",
                    BiZ.Sys.MemberActivity.MemberActivityType.NewTalkAbout,
                    "/Content/ContentDetail/" + suisuinian.ID);

                return Json(new JavaScriptSerializer().Serialize(suisuinian));
            }
            else
                return Json(new JavaScriptSerializer().Serialize(false));
        }
        /// <summary>
        /// 发布访谈内容
        /// </summary>
        /// <param name="permissions">访问权限</param>
        /// <param name="interestids">兴趣编号集合</param>
        /// <param name="interviewid">访谈编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertInterViewContent(string permissions, string interestids, string interviewid, Boolean ifdelete)
        {
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            if (userID != "" && permissions != "" && interestids != "" && interviewid != "")
            {
                //创建访谈内容对象
                BiZ.Content.InterViewContent interviewContent = null;
                //根据访谈编号获取访谈对象
                BiZ.InterView.InterView interView = BiZ.InterView.InterViewProvider.GetInterView(interviewid);
                //创建访谈对象集合用于保存访谈内容
                IList<BiZ.InterView.InterView> interViewList = new List<BiZ.InterView.InterView>();
                interViewList.Add(interView);
                if (ifdelete)
                {
                    interviewContent = BiZ.Content.ContentProvider.getInterviewContent(interviewid, userID);
                    interViewList = interviewContent.InterviewList;
                    int index = -1;
                    for (int i = 0; i < interViewList.Count; i++)
                    {
                        if (interViewList[i].ID == interviewid) { index = i; break; }
                    }
                    interviewContent.InterviewList.RemoveAt(index);
                    if (interviewContent.InterviewList.Count <= 0)
                    {
                        interviewContent.DeleteFlag = BiZ.Comm.DeletedFlag.Yes;
                    }
                }
                else
                {
                    //根据访谈编号和用户编号查询访谈内容
                    interviewContent = BiZ.Content.ContentProvider.getInterviewContent(interviewid, userID);
                    //访谈内容是否存在
                    if (interviewContent != null)
                    {
                        interViewList = interviewContent.InterviewList;
                        int index = -1;
                        for (int i = 0; i < interViewList.Count; i++)
                        {
                            if (interViewList[i].ID == interviewid) { index = i; break; }
                        }
                        interviewContent.InterviewList.RemoveAt(index);
                        interviewContent.InterviewList.Insert(0, interView);
                        if (interviewContent.InterviewList.Count <= 0)
                        {
                            interviewContent.DeleteFlag = BiZ.Comm.DeletedFlag.Yes;
                        }
                    }
                    else
                    {
                        //获取当前登录用户最后发布的访谈内容
                        BiZ.Content.InterViewContent lastinterview = BiZ.Content.ContentProvider.getLastInterViewForMember(userID);
                        //判断用户最后发布的访谈内容是否在今天
                        if (lastinterview != null && lastinterview.UpdateTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            lastinterview.InterviewList.Insert(0, interView);
                            interviewContent = lastinterview;
                        }
                        else
                        {
                            interviewContent = new BiZ.Content.InterViewContent(userID, getContentPerMissions(permissions), interestids.Split(',').ToList(), interViewList);
                        }
                    }
                }
                interviewContent.ContentPermissions = (BiZ.Content.ContentPermissions)Enum.Parse(typeof(BiZ.Content.ContentPermissions), permissions);
                interviewContent.InterestID = interestids.Split(',').ToList();
                interviewContent.UpdateTime = DateTime.Now;
                //保存访谈内容对象
                interviewContent = interviewContent.Save(interviewContent);
                //更新用户在兴趣中的活跃度
                updateInterestHot(interestids, userID);

                //增加用户动态到后台
                BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                    userID,
                    "",
                    BiZ.Sys.MemberActivity.MemberActivityType.AddInterview,
                    "/Content/ContentDetail/" + interviewContent.ID);

                return Json(new JavaScriptSerializer().Serialize(interviewContent));
            }
            else
                return Json(new JavaScriptSerializer().Serialize(false));
        }
        /// <summary>
        /// 发布号召内容
        /// </summary>
        /// <param name="permissions">访问权限</param>
        /// <param name="interestids">兴趣编号集合</param>
        /// <param name="Content">内容</param>
        /// <param name="imageIDs">图片编号集合</param>
        /// <param name="type">号召类别</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertCallForContent(string permissions, string interestids, string Content, string imageIDs, string layOutType, string type, string contentid)
        {
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            if (imageIDs.Split(',').Length > 1 && (layOutType == null || layOutType == ""))
            {
                return Json(new JavaScriptSerializer().Serialize(false));
            }
            if (userID != "" && permissions != "" && type != "")
            {
                //根据图片编号集合创建图片集合
                List<BiZ.Content.Image> imageList = new List<BiZ.Content.Image>();
                foreach (String photoid in imageIDs.Split(','))
                {
                    BiZ.Photo.Photo obj = BiZ.Photo.PhotoManager.GetPhoto(photoid);
                    if (obj != null)
                        imageList.Add(new BiZ.Content.Image(obj.FileName));
                }
                //创建号召内容对象
                BiZ.Content.CallForContent callForContent = null;
                if (contentid != null && contentid != "" && contentid != "null")
                {
                    callForContent = new BiZ.Content.CallForContent(contentid);
                    callForContent.ContentPermissions = getContentPerMissions(permissions);
                    callForContent.InterestID = interestids.Split(',').ToList();
                    callForContent.Content = Content;
                    callForContent.ImageList = imageList;
                    callForContent.LayOutType = getImageLayOutType(layOutType);
                    callForContent.Type = type;
                    callForContent.UpdateTime = DateTime.Now;
                }
                else
                {
                    callForContent = new BiZ.Content.CallForContent(userID, getContentPerMissions(permissions), interestids.Split(',').ToList(), Content, imageList, getImageLayOutType(layOutType), type);
                }
                //保存号召内容对象
                callForContent = callForContent.Save(callForContent);
                //更新用户在兴趣中的活跃度
                updateInterestHot(interestids, userID);
                if (contentid == null && contentid == "" && contentid == "null")
                {
                    //扣除米果
                    int callfortopoints = Common.Comm.getAddCallForContentToPoints();
                    BiZ.MemberManager.MemberManager.ModifyPoints(userID, BiZ.MemberManager.StatusModifyType.Decrease, callfortopoints);
                }
                return Json(new JavaScriptSerializer().Serialize(callForContent));
            }
            else
                return Json(new JavaScriptSerializer().Serialize(false));
        }
        /// <summary>
        /// 发布兴趣操作内容
        /// </summary>
        /// <param name="permissions">访问权限</param>
        /// <param name="interestids">兴趣编号集合</param>
        /// <param name="interstid">兴趣编号</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertInterestContent(string permissions, string interestids, string content, string interstid, string type)
        {
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            if (userID != "" && permissions != "" && interestids != "" && content != "" && interstid != "" && type != "")
            {
                //创建兴趣操作内容对象
                BiZ.Content.InterestContent imageContent = new BiZ.Content.InterestContent(userID, getContentPerMissions(permissions), interestids.Split(',').ToList(), content, BiZ.InterestCenter.InterestFactory.GetInterest(interstid), type);
                //保存兴趣操作内容对象
                imageContent = imageContent.Save(imageContent);
                return Json(new JavaScriptSerializer().Serialize(true));
            }
            else
                return Json(new JavaScriptSerializer().Serialize(false));
        }
        /// <summary>
        /// 发布用户操作内容
        /// </summary>
        /// <param name="permissions">访问权限</param>
        /// <param name="interestids">兴趣编号集合</param>
        /// <param name="lat">经度</param>
        /// <param name="lng">纬度</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertMemberContent(string permissions, string interestids, string lat, string lng, string type)
        {
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            if (userID != "" && permissions != "" && interestids != "" && lat != "" && lng != "" && type != "")
            {
                //创建兴趣操作内容对象
                BiZ.Content.MemberContent imageContent = new BiZ.Content.MemberContent(userID, getContentPerMissions(permissions), interestids.Split(',').ToList(), Double.Parse(lat), Double.Parse(lng), type);
                //保存兴趣操作内容对象
                imageContent = imageContent.Save(imageContent);
                return Json(new JavaScriptSerializer().Serialize(true));
            }
            else
                return Json(new JavaScriptSerializer().Serialize(false));
        }
        /// <summary>
        /// 发布内容时的类别选择
        /// </summary>
        /// <param name="type">类别集合</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddContentType(BiZ.Content.ContentType contentType, BiZ.Content.PublicContent contentObj)
        {
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            //用户必须登录才能打开该页面
            if (userID == null || userID == "")
                return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });
            Models.PageModels.Content.AddContentModel model = new Models.PageModels.Content.AddContentModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = true;
            model.type = getContentType(contentType);
            if (contentObj != null)
            {
                model.contentObj = contentObj;
            }
            return View(model);
        }
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ImageUploadascx(Boolean ifShowUp, String contentTitle, String photoType, BiZ.Content.PublicContent contentObj)
        {
            SetMetasVersion();
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            Models.PageModels.Content.AddContentModel model = new Models.PageModels.Content.AddContentModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID == null || userID == "" ? false : true;
            if (contentObj != null)
            {
                model.contentObj = contentObj;
            }
            ViewData["ifShowUp"] = ifShowUp;
            ViewData["contentTitle"] = contentTitle;
            ViewData["phototype"] = photoType;
            return View(model);
        }
        /// <summary>
        /// 右边的公共控件
        /// </summary>
        /// <param name="interestList">当前用户的兴趣集合</param>
        /// <returns></returns>
        public ActionResult AddRightPanel(BiZ.Content.PublicContent contentObj)
        {
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            ////用户必须登录才能打开该页面
            //if (userID == null || userID == "")
            //    return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });
            Models.PageModels.Content.AddContentModel model = new Models.PageModels.Content.AddContentModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.AlreadyLogon = userID == null || userID == "" ? false : true;
            model.interestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(userID, 0, 0);
            if (contentObj != null)
            {
                model.contentObj = contentObj;
            }
            else
            {
                model.contentObj = null;
            }
            return View(model);
        }
        /// <summary>
        /// 将访问权限转换为枚举类型
        /// </summary>
        /// <param name="permissions">访问权限</param>
        /// <returns></returns>
        public static BiZ.Content.ContentPermissions getContentPerMissions(string permissions)
        {
            return (BiZ.Content.ContentPermissions)Enum.Parse(typeof(BiZ.Content.ContentPermissions), permissions);
        }
        /// <summary>
        /// 将图片布局类型转换为枚举
        /// </summary>
        /// <param name="layOutType">图片布局类型</param>
        /// <returns></returns>
        public BiZ.Content.ImageLayoutType getImageLayOutType(string layOutType)
        {
            return (BiZ.Content.ImageLayoutType)Enum.Parse(typeof(BiZ.Content.ImageLayoutType), layOutType);
        }
        /// <summary>
        /// 更新用户在兴趣中的活跃度
        /// </summary>
        /// <param name="iids">兴趣编号集合</param>
        /// <param name="mid">用户编号</param>
        public void updateInterestHot(string iids, string mid)
        {
            foreach (string iid in iids.Split(',')) { InterestCenterController.updateInterestHot(iid, mid); }
        }
        #endregion

        //删除内容(更改内容删除状态)：contentid-内容编号
        public ActionResult DeleteContent(string contentid)
        {
            BiZ.Content.PublicContent contentobj = new BiZ.Content.PublicContent().getPublicContent(contentid);
            contentobj.DeleteFlag = BiZ.Comm.DeletedFlag.Yes;
            contentobj.UpdateTime = DateTime.Now;
            contentobj.savePublicContent(contentobj);
            if (contentobj != null)
            {
                string memberId = HttpContext.User.Identity.Name;
                //增加用户动态到后台
                BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                    memberId,
                    "",
                    BiZ.Sys.MemberActivity.MemberActivityType.RemoveContent,
                    "/Content/ContentDetail/" + contentobj.ID);

                return Json(new JavaScriptSerializer().Serialize(true));
            }
            return Json(new JavaScriptSerializer().Serialize(false));
        }

        #region 内容回复、喜欢
        /// <summary>
        /// 加载回复
        /// </summary>
        /// <param name="contentID">内容编号</param>
        /// <param name="pageno">页数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShowComment(String contentID, string pageno, string pagesize)
        {
            if (contentID == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pageno == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (pagesize == null) return RedirectToAction("Error", "Error", new { errorno = "0" });
            int pgNo = 0, pgSize = 0;
            if (!int.TryParse(pageno, out pgNo)) return RedirectToAction("Error", "Error", new { errorno = "0" });
            if (!int.TryParse(pagesize, out pgSize)) return RedirectToAction("Error", "Error", new { errorno = "0" });

            IList<BiZ.Comment.Comment> commentList = BiZ.Comment.CommentProvider.findForContent(contentID, BiZ.Comm.DeletedFlag.No, pgNo, pgSize);
            Models.PageModels.Comment.CommentModel model = new Models.PageModels.Comment.CommentModel();
            model.commentList = commentList;
            model.contentObject = new BiZ.Content.PublicContent().getPublicContent(contentID);
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        /// <summary>
        /// 添加回复
        /// </summary>
        /// <param name="contentID">内容编号</param>
        /// <param name="content">回复内容</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddComment(String contentID, String content, String toMember, String commentId)
        {
            String userID = User.Identity.Name;
            CBB.ExceptionHelper.OperationResult result = null;

            BiZ.Comment.Comment comment = AddCommentFunc(userID, contentID, content);

            Models.PageModels.Comment.CommentModel model = new Models.PageModels.Comment.CommentModel();
            model.contentObject = new BiZ.Content.PublicContent().getPublicContent(contentID);
            //增加用户关联动态
            ActivityController.addActivityToContent(model.contentObject, comment, userID, "addComment");

            //评论被评论
            if (toMember != "" && toMember != null)
            {
                IList<BiZ.Content.PublicContent> pcs = BiZ.Content.ContentProvider.GetIDMemberContent(contentID, toMember, 0, 0);
                string strContentType = "",
                    imagesStr = "";
                if (pcs.Count < 1)
                {
                    BiZ.Comment.Comment comment2 = BiZ.Comment.CommentProvider.GetComment(commentId);

                    switch (model.contentObject.ContentType)
                    {
                        case BiZ.Content.ContentType.Image: 
                            strContentType = "图片";
                            imagesStr = ActivityController.GetMemberActivityContentImages(model.contentObject);
                            break;
                        case BiZ.Content.ContentType.SuiSuiNian:
                            imagesStr = ActivityController.GetMemberActivityContentImages(model.contentObject);
                            strContentType = "说说";
                            break;
                        case BiZ.Content.ContentType.IWant:
                            strContentType = "我想";
                            imagesStr = ActivityController.GetMemberActivityContentImages(model.contentObject);
                            break;
                        case BiZ.Content.ContentType.Mood:
                            strContentType = "心情";
                            string statusMood = ((BiZ.Content.MoodContent)model.contentObject).Type;
                            switch (statusMood)
                            {
                                case "0": strContentType = "好心情";
                                    break;
                                case "1": strContentType = "坏心情";
                                    break;
                                default: break;
                            }
                            break;
                        case BiZ.Content.ContentType.InterView:
                            strContentType = "访谈";
                            break;
                        case BiZ.Content.ContentType.CallFor:
                            imagesStr = ActivityController.GetMemberActivityContentImages(model.contentObject);
                            strContentType = "号召";
                            break;
                        case BiZ.Content.ContentType.Interest:
                            strContentType = "兴趣";
                            break;
                        case BiZ.Content.ContentType.Member:
                            strContentType = "用户动态";
                            string statusMember = ((BiZ.Content.MemberContent)model.contentObject).Type;
                            switch (statusMember)
                            {
                                case "0":
                                    strContentType = "新位置";
                                    break;
                                case "1":
                                    strContentType = "新头像";
                                    break;
                                default:
                                    break;
                            }

                            break;
                        default:
                            break;
                    }

                    ////增加用户关联动态
                    //result = BiZ.Member.Activity.ActivityController.AddActivityRelatedToMe(
                    //    toMember,
                    //    userID,
                    //    BiZ.Member.Activity.ActivityType.CommentBeenReplied,
                    //    BiZ.Member.Activity.ActivityController.GetActivityContent_CommentBeenReplied_Title(),
                    //    BiZ.Member.Activity.ActivityController.GetActivityContent_CommentBeenReplied(
                    //        model.contentObject.ID, 
                    //        comment.Content, 
                    //        comment2.Content,
                    //        strContentType,
                    //        imagesStr
                    //        ),
                    //    false
                    //    );

                    string replyStr = comment.Content;
                    int replyIndexOfChar = replyStr.IndexOf('：');
                    string replyStr2 = replyStr.Substring(replyIndexOfChar + 1, replyStr.Length - replyIndexOfChar - 1);
                    BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(toMember);

                    string msgStr = "|" + member.MemberInfomation.NickName + "：" + comment2.Content + "\n" + replyStr2 + "";
                    BiZ.Member.Activity.ActivityController.MsgToMember(
                        userID,
                        toMember,
                        msgStr,
                        BiZ.Member.Activity.ActivityType.CommentBeenReplied);
                    
                }
            }

            //增加用户动态到后台
            BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                userID,
                toMember,
                BiZ.Sys.MemberActivity.MemberActivityType.Comment,
                "/Content/ContentDetail/" + model.contentObject.ID);

            return Json(new JavaScriptSerializer().Serialize(model));
        }
        
        /// <summary>
        /// 添加回复
        /// </summary>
        /// <param name="contentID">内容编号</param>
        /// <param name="content">回复内容</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMemberLabelComment(String contentID, String content, String memberLabel)
        {
            String userID = User.Identity.Name;

            BiZ.Comment.Comment comment = AddCommentFunc(userID, contentID, content);

            Models.PageModels.Comment.CommentModel model = new Models.PageModels.Comment.CommentModel();
            model.commentList = BiZ.Comment.CommentProvider.GetMemberNameContentIDComment(contentID, "回应" + memberLabel + "：", BiZ.Comm.DeletedFlag.No, 0, 0);
            model.commentCount = model.commentList.Count;
            model.contentObject = new BiZ.Content.PublicContent().getPublicContent(contentID);

            //增加用户关联动态
            ActivityController.addActivityToContent(model.contentObject, comment, userID, "addComment");
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        [HttpPost]
        public ActionResult GetMemberLabelComment(String contentID, String memberLabel)
        {
            Models.PageModels.Comment.CommentModel model = new Models.PageModels.Comment.CommentModel();
            model.commentList = BiZ.Comment.CommentProvider.GetMemberNameContentIDComment(contentID, "回应" + memberLabel + "：", BiZ.Comm.DeletedFlag.No, 0, 0);
            model.commentCount = model.commentList.Count;
            return Json(new JavaScriptSerializer().Serialize(model));
        }
        //添加回复方法
        private BiZ.Comment.Comment AddCommentFunc(String userID, String contentID, String content)
        {
            userID = userID == "" ? null : userID;
            BiZ.Comment.Comment comment = new BiZ.Comment.Comment(userID, contentID, content, BiZ.Comment.CommentType.JustComment);
            comment = comment.Save(comment);
            new BiZ.Comment.Factory().getICanBeenComment(BiZ.Comm.ContentType.PublicContent).UpdateCommentList(new BiZ.Comment.Comment(comment.ID));
            return comment;
        }
        /// <summary>
        /// 添加内容喜欢
        /// </summary>
        /// <param name="contentID">内容编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddContentLike(String contentID, String likeContentType)
        {
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            Boolean ifLiked = BiZ.Like.LikeDataFactory.IfLiked<BiZ.Like.LikeData>(userID, contentID, BiZ.Like.LikeType.Content, BiZ.Like.LikeData.GetCollectionName());
            if (!ifLiked)
            {
                BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(userID);
                BiZ.Like.LikeDataFactory.AddLikeData(userID, contentID, BiZ.Like.LikeType.Content);
                BiZ.Content.PublicContent.UpdateLikeCount(contentID);
                //追加喜欢的集合
                BiZ.Content.PublicContent obj = new BiZ.Content.PublicContent().getPublicContent(contentID);
                obj.LikeList.Add(
                    new BiZ.Like.LikeMember(userID, mym.MemberInfomation.NickName, mym.MemberInfomation.IconPath)
                    );
                //保存更改
                obj.savePublicContent(obj);
                //增加魅力值
                BiZ.Member.Activity.ActivityController.AddGlamourValue(
                    userID, 
                    obj.MemberID, 
                    BiZ.Member.GlamourCounts.GlamourCountOperate.GlamourCountType.ContentLike, 
                    BiZ.Member.GlamourCounts.GlamourCountOperate.ModifyGlamourValue.One, 
                    likeContentType
                    );
                //增加积分增长的进度值
                BiZ.Member.Member memberobj = BiZ.MemberManager.MemberManager.GetMember(obj.MemberID);
                int MaxPointsSchedule = Common.Comm.getMaxPointsSchedule();
                if (memberobj.Status.PointsSchedule < MaxPointsSchedule)
                {
                    //增加一点进度值
                    BiZ.MemberManager.MemberManager.ModifyPointsSchedule(obj.MemberID, BiZ.MemberManager.StatusModifyType.Add);
                }
                //增加用户关联动态
                ActivityController.addActivityToContent(obj, null, userID, "addLike");

                //增加用户动态到后台
                BiZ.Sys.MemberActivity.MemberActivityProvider.CreateMemberActivity(
                    userID,
                    obj.MemberID,
                    BiZ.Sys.MemberActivity.MemberActivityType.LikeOther,
                    "/Content/ContentDetail/" + obj.ID);

                Models.PageModels.Content.ContentModel model = new Models.PageModels.Content.ContentModel();
                model.contentobj = obj;
                return Json(new JavaScriptSerializer().Serialize(model));
            }
            return Json(new JavaScriptSerializer().Serialize(false));
        }
        /*
        /// <summary>
        /// 添加内容喜欢回复
        /// </summary>
        /// <param name="contentID">内容编号</param>
        /// <param name="content">回复内容</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddContentLikeComment(String contentID, String content)
        {
        String userID = User.Identity.Name;
        userID = userID == "" ? null : userID;
        BiZ.Comment.Comment comment = new BiZ.Comment.Comment(userID, contentID, content, BiZ.Comment.CommentType.ToLike);
        comment = comment.Save(comment);
        BiZ.Content.PublicContent.UpdateLikeList(comment);
        new BiZ.Comment.Factory().getICanBeenComment(BiZ.Comm.ContentType.PublicContent).UpdateCommentList(comment);
        Models.PageModels.Comment.CommentModel model = new Models.PageModels.Comment.CommentModel();
        model.contentObject = new BiZ.Content.PublicContent().getPublicContent(contentID);
        //增加用户关联动态
        addActivityToContent(model.contentObject, comment, userID, "addLikeToComment");
        return Json(new JavaScriptSerializer().Serialize(model));
        }*/
        
        #endregion

        #region 访谈回复、喜欢
        ///// <summary>
        ///// 加载访谈回复
        ///// </summary>
        ///// <param name="interviewID">访谈编号</param>
        ///// <param name="pageno">页数</param>
        ///// <returns></returns>
        //public ActionResult ShowInterViewComment(String interviewID, int pageno)
        //{
        //    int pagesize = 6;
        //    IList<BiZ.Comment.Comment> commentList = BiZ.Comment.CommentProvider.findForType(interviewID, BiZ.Comment.CommentType.InterView, BiZ.Comm.DeletedFlag.No, pageno, pagesize);
        //    Models.PageModels.Comment.InterViewCommentModel model = new Models.PageModels.Comment.InterViewCommentModel();
        //    model.commentList = commentList;
        //    model.interviewObject = BiZ.InterView.InterViewProvider.GetInterView(interviewID);
        //    return Json(new JavaScriptSerializer().Serialize(model));
        //}
        ///// <summary>
        ///// 添加访谈回复
        ///// </summary>
        ///// <param name="interviewID">访谈编号</param>
        ///// <param name="content">页数</param>
        ///// <returns></returns>
        //public ActionResult AddInterViewComment(String interviewID, String contentID, String content)
        //{
        //    String userID = User.Identity.Name;
        //    userID = userID == "" ? null : userID;
        //    BiZ.Comment.Comment comment = new BiZ.Comment.Comment(userID, interviewID, content, BiZ.Comment.CommentType.InterView);
        //    comment = comment.Save(comment);
        //    new BiZ.Comment.Factory().getICanBeenComment(BiZ.Comm.ContentType.InterView).UpdateCommentList(comment);
        //    BiZ.Content.PublicContent.UpdateInterViewList(contentID, interviewID);
        //    Models.PageModels.Comment.InterViewCommentModel model = new Models.PageModels.Comment.InterViewCommentModel();
        //    model.interviewObject = BiZ.InterView.InterViewProvider.GetInterView(interviewID);
        //    //增加用户关联动态
        //    addActivityToContent(new BiZ.Content.PublicContent().getPublicContent(contentID), comment, userID, "addInterViewComment");
        //    return Json(new JavaScriptSerializer().Serialize(model));
        //}
        ///// <summary>
        ///// 添加访谈喜欢
        ///// </summary>
        ///// <param name="interviewID">访谈编号</param>
        ///// <returns></returns>
        //public ActionResult AddInterViewLike(String interviewID, String contentID)
        //{
        //    String userID = User.Identity.Name;
        //    userID = userID == "" ? null : userID;
        //    Boolean ifLiked = BiZ.Like.LikeDataFactory.IfLiked<BiZ.Like.LikeData>(userID, interviewID, BiZ.Like.LikeType.InterView, BiZ.Like.LikeData.GetCollectionName());
        //    if (!ifLiked)
        //    {
        //        BiZ.Like.LikeDataFactory.AddLikeData(userID, interviewID, BiZ.Like.LikeType.InterView);
        //        new BiZ.InterView.InterView().UpdateLikeCount(interviewID);
        //        BiZ.Content.PublicContent.UpdateInterViewList(contentID, interviewID);
        //        Models.PageModels.Comment.InterViewCommentModel model = new Models.PageModels.Comment.InterViewCommentModel();
        //        model.interviewObject = BiZ.InterView.InterViewProvider.GetInterView(interviewID);
        //        //增加用户关联动态
        //        addActivityToContent(new BiZ.Content.PublicContent().getPublicContent(contentID), null, userID, "addInterViewLike");
        //        return Json(new JavaScriptSerializer().Serialize(model));
        //    }
        //    return Json(new JavaScriptSerializer().Serialize(false));
        //}
        #endregion

        #region 更新顶部推送内容的计数器
        /// <summary>
        /// 更新顶部推送内容的显示次数
        /// </summary>
        /// <param name="id">顶部推送内容编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePushShowCount(string id)
        {
            if (id == null || id == "")
                return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });
            BiZ.TopImagePush.ImagePush imagePush = new BiZ.TopImagePush.ImagePush(id);
            BiZ.TopImagePush.ImagePushCount imagePushCount = new BiZ.TopImagePush.ImagePushCount(imagePush.ImagePushCount.ShowCount + 1, imagePush.ImagePushCount.ClickCount);
            imagePush.ImagePushCount = imagePushCount;
            imagePush.Save(imagePush);
            return Json(new JavaScriptSerializer().Serialize(true));
        }
        /// <summary>
        /// 更新顶部推送内容的点击次数
        /// </summary>
        /// <param name="id">顶部推送内容编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePushClickCount(string id)
        {
            if (id == null || id == "")
                return RedirectToAction("Error", "Error", new { errNo = "参数不正确。" });
            BiZ.TopImagePush.ImagePush imagePush = new BiZ.TopImagePush.ImagePush(id);
            BiZ.TopImagePush.ImagePushCount imagePushCount = new BiZ.TopImagePush.ImagePushCount(imagePush.ImagePushCount.ShowCount, imagePush.ImagePushCount.ClickCount + 1);
            imagePush.ImagePushCount = imagePushCount;
            imagePush.Save(imagePush);
            return Json(new JavaScriptSerializer().Serialize(true));
        }
        #endregion

        //根据内容类型查找内容集合：contenttype-类型；pagesize-每页条数；pageno-页数
        [HttpPost]
        public ActionResult getContentToType(string contenttype, int pagesize, int pageno)
        {
            IList<BiZ.Content.PublicContent> contentlist = BiZ.Content.ContentProvider.findCreatedTimeForType("", contenttype, BiZ.Comm.DeletedFlag.No, pageno, pagesize);
            Models.PageModels.Content.IndexContentModel model = new Models.PageModels.Content.IndexContentModel();
            model.ContentList = contentlist;
            model.contentCount = BiZ.Content.ContentProvider.findForTypeCount("", contenttype, BiZ.Comm.DeletedFlag.No);
            return Json(new JavaScriptSerializer().Serialize(model));
        }

        //关于赚取米果
        [Authorize]
        public ActionResult AboutEarnPoints()
        {
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            Models.PageModels.MemberPageModel model = new Models.PageModels.MemberPageModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.Member = userID == null ? null : Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            return View(model);
        }

        //关于米果的用途
        [Authorize]
        public ActionResult AboutUsePoints()
        {
            //获取并设置项目版本号
            SetMetasVersion();
            //获取当前登录的用户编号
            String userID = User.Identity.Name;
            userID = userID == "" ? null : userID;
            Models.PageModels.MemberPageModel model = new Models.PageModels.MemberPageModel();
            model.UserID = userID;
            model.MemberID = userID;
            model.Member = userID == null ? null : Models.DisplayObjProvider.getMemberFullDisplayObj(userID);
            return View(model);
        }

        //显示最新的用户
        [HttpPost]
        public ActionResult ShowNewMember(String interestID, String createdTime)
        {
            IList<BiZ.Member.MemberToNew> memberList = new List<BiZ.Member.MemberToNew>();
            if (interestID != null && interestID != "")
            {
                IList<BiZ.InterestCenter.InterestFans> interestFanss = BiZ.InterestCenter.InterestFactory.GetInterestFans(interestID, 8, 1);
                string[] iids = new string[interestFanss.Count];
                for (int i = 0; i < interestFanss.Count; i++)
                {
                    if (interestFanss[i].Creater != null)
                    {
                        iids[i] = interestFanss[i].Creater.MemberID;
                    }
                }
                memberList = BiZ.MemberManager.MemberManager.getMemberToNews(BiZ.MemberManager.NewMembersSelectType.NewToInterest, iids, createdTime, 0, 0);
            }
            else
            {
                memberList = BiZ.MemberManager.MemberManager.getMemberToNews(BiZ.MemberManager.NewMembersSelectType.New, new string[0], createdTime, 8, 1);
            }
            return Json(new JavaScriptSerializer().Serialize(memberList));
        }

        //获取指定用户最后设置的头像内容
        [HttpPost]
        public ActionResult getLastMemberContent(String toObjectID, String type)
        {
            BiZ.Member.MemberToNewType types = (BiZ.Member.MemberToNewType)Enum.Parse(typeof(BiZ.Member.MemberToNewType), type);
            if (types == BiZ.Member.MemberToNewType.ImageContent)
            {
                BiZ.Content.MemberContent memberobj = new BiZ.Content.MemberContent(toObjectID);
                return Json(new JavaScriptSerializer().Serialize(memberobj));
            }
            else
            {
                return Json(new JavaScriptSerializer().Serialize(null));
            }
        }
    }
}
