using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Security.Principal;

namespace Moooyo.WebUI.Common
{
    public class Comm
    {
        #region 定义用户兴趣达到一定数量后，再次添加所需的米果数
        private static Dictionary<int, int> InterestAddFansToPoints = null;
        public static Dictionary<int, int> getInterestAddFansToPoints()
        {
            if (InterestAddFansToPoints == null)
            {
                string str = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("InterestAddFansToPoints");
                string[] strs = str.Split('|');
                InterestAddFansToPoints = new Dictionary<int, int>();
                foreach (string item in strs)
                {
                    string[] items = item.Split(',');
                    InterestAddFansToPoints.Add(int.Parse(items[0]), int.Parse(items[1]));
                }
            }
            return InterestAddFansToPoints;
        }
        #endregion

        #region 定义用户创建兴趣所需的米果数
        private static Dictionary<int, int> InterestInsertToPoints = null;
        public static Dictionary<int, int> getInterestInsertToPoints()
        {
            if (InterestInsertToPoints == null)
            {
                string str = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("InterestInsertToPoints");
                string[] strs = str.Split('|');
                InterestInsertToPoints = new Dictionary<int, int>();
                foreach (string item in strs)
                {
                    string[] items = item.Split(',');
                    InterestInsertToPoints.Add(int.Parse(items[0]), int.Parse(items[1]));
                }
            }
            return InterestInsertToPoints;
        }
        #endregion

        //发布号召所需的米果数
        private static int AddCallForContentToPoints = 0;
        public static int getAddCallForContentToPoints()
        {
            if (AddCallForContentToPoints == 0) AddCallForContentToPoints = int.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("AddCallForContentToPoints"));
            return AddCallForContentToPoints;
        }

        //积分增长的进度值上限
        private static int MaxPointsSchedule = 0;
        public static int getMaxPointsSchedule() {
            if (MaxPointsSchedule == 0) MaxPointsSchedule = int.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("MaxPointsSchedule"));
            return MaxPointsSchedule;
        }

        //完成邀请赠送的米果数
        private static int InvitationCodeToPoints = 0;
        public static int getInvitationCodeToPoints()
        {
            if (InvitationCodeToPoints == 0) InvitationCodeToPoints = int.Parse(CBB.ConfigurationHelper.AppSettingHelper.GetConfig("InvitationCodeToPoints"));
            return InvitationCodeToPoints;
        }

        #region 外部平台分享连接识别字段
        private static string shareSina = "";
        public static string getShareSina()
        {
            if (shareSina == "") shareSina = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("shareSina");
            return shareSina;
        }
        private static string shareTencent = "";
        public static string getShareTencent()
        {
            if (shareTencent == "") shareTencent = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("shareTencent");
            return shareTencent;
        }
        private static string shareRenRen = "";
        public static string getShareRenRen()
        {
            if (shareRenRen == "") shareRenRen = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("shareRenRen");
            return shareRenRen;
        }
        private static string shareDouBan = "";
        public static string getShareDouBan()
        {
            if (shareDouBan == "") shareDouBan = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("shareDouBan");
            return shareDouBan;
        }
        #endregion

        public static void SetMetasVersion(ViewDataDictionary vdd)
        {
            vdd["jsversion"] = BiZ.Sys.RunStatus.JsVersion;
            vdd["cssversion"] = BiZ.Sys.RunStatus.CSSVersion;
            vdd["imageversion"] = BiZ.Sys.RunStatus.ImageVersion;
            vdd["uploadpath"] = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");
        }
        private static string activityCitys;
        public static string GetActivityCitys()
        {
            if (activityCitys == null) activityCitys = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("ActivityCity");
            return activityCitys;
        }
        public static bool isalreadylogin(string role, IPrincipal contextUser)
        {
            var id = contextUser.Identity as FormsIdentity;
            if (id != null && id.IsAuthenticated)
            {
                var roles = id.Ticket.UserData.Split(',');
                if (roles[0] == role)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        //得到距离当前时间
        public static String getTimeSpan(DateTime dt)
        {
            TimeSpan sp = DateTime.Now - dt.ToLocalTime();
            double df = sp.TotalSeconds;

            string time = "";
            if (df < 0) return dt.ToString("yyyy-mm-dd hh:mm:ss");
            if (df >= 0 & df < 60) time = Math.Floor(df) + "秒前";
            if (df >= 60 & df < 3600) time = Math.Floor(df / 60) + "分钟前";
            if (df >= 3600 & df < 86400) time = Math.Floor(df / 3600) + "小时前";
            if (df >= 86400 & df < 2592000) time = Math.Floor(df / 86400) + "天前";
            if (df >= 2592000) time = Math.Floor(df / 2592000) + "个月前";
            return time;
        }
        //得到指定大小的图片地址
        public static String getImagePath(String filename, ImageType type)
        {
            if (filename == null || filename == "" || filename.IndexOf("noicon") >= 0)
            { return "/pics/noicon.jpg"; }

            String uploadpath = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("UploadPath");

            int fileNameLastDot = filename.LastIndexOf('.') < 0 ? filename.Length - 1 : filename.LastIndexOf('.');
            switch (type)
            {
                case ImageType.Original:
                    return uploadpath + "/" + filename.Substring(0, fileNameLastDot) + ".jpg";
                case ImageType.Icon:
                    return uploadpath + "/" + filename.Substring(0, fileNameLastDot) + "_i.jpg";
                case ImageType.Small:
                    return uploadpath + "/" + filename.Substring(0, fileNameLastDot) + "_s.jpg";
                case ImageType.Middle:
                    return uploadpath + "/" + filename.Substring(0, fileNameLastDot) + "_p.jpg";
            }

            return uploadpath + "/" + filename;
        }
        //注册邮箱验证邮件内容
        [ValidateInput(true)]
        public static String GetRegisterEmailContent(String email, String pwd)
        {
            String domainName = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("Domain");

            String str = @"<html><body>欢迎注册[米柚]，请点击链接<a href='http://" + domainName + "/register/RegStep2ActiveEmail?email=" + email + "&pwd=" + pwd + @"' target='_blank'>http://" + domainName + "/register/RegStep2ActiveEmail?email=" + email + "&pwd=" + pwd + @"</a>激活您的注册。
                </body></html>
                       ";
            return str;
        }
        //延迟邮箱验证邮件内容
        [ValidateInput(true)]
        public static String GetVerifyEmailContent(String email, String pwd)
        {
            String domainName = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("Domain");

            String str = @"<html><body>欢迎来到[米柚]，请点击链接<a href='http://" + domainName + "/setting/ActiveEmail?email=" + email + "&pwd=" + pwd + @"' target='_blank'>http://" + domainName + "/setting/ActiveEmail?email=" + email + "&pwd=" + pwd + @"</a>完成您的邮箱验证。
                </body></html>
                       ";
            return str;
        }
        //替换头像
        public static String getExpression(String oldstr)
        {
            String keys = "[anj],[aom],[baiy],[bis],[biz],[chah],[ciy],[dak],[dang],[dao],[dey],[fad],[fank],[fend],[fendou],[fenn],[gang],[gouy],[guz],[haix],[hanx],[haq],[huaix],[jie],[jingk],[jingy],[kea],[kel],[koubs],[ku],[kuaikl],[laoh],[lengh],[liul],[nang],[no],[out],[peif],[piez],[qinqin],[qoudl],[se],[shuai],[shui],[tiaop],[toux],[weiq],[weix],[wos],[xia],[xianh],[xianhdx],[xin],[xins],[yinx],[yiw],[youhh],[yun],[zaij],[zan],[zhem],[zhoum],[zhuak],[zoun],[zuohh]";
            String[] keylist = keys.Split(',');
            foreach (char str in oldstr.ToCharArray())
            {
                bool ifindex = false;
                foreach (string key in keylist)
                {
                    if (oldstr.IndexOf(key) >= 0)
                    {
                        ifindex = true;
                        string keystr = key.Substring(1, key.Length - 2);
                        oldstr = oldstr.Replace(key, "<img src=\"/pics/Expression/" + keystr + ".gif\"/>");
                    }
                }
                if (!ifindex)
                {
                    break;
                }
            }
            return oldstr;
        }
        //获取cookie
        public static String GetCookie(String name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(name);
            if (cookie != null)
            {
                return cookie.Value;
            }
            return null;
        }
        //删除cookie
        public static Boolean RemoveCookie(String name)
        {
            HttpContext.Current.Request.Cookies.Remove(name);
            return true;
        }
        //添加cookie
        public static void SetCookie(String name, String value, CookieOrSessionExpiresTime expiresTimeType)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(name);
            if (cookie != null)
            {
                HttpContext.Current.Request.Cookies.Remove(name);
            }
            HttpCookie cookietoname = new HttpCookie(name, value);
            cookietoname.Expires = GetCookieOrSessionExpiresTime(expiresTimeType);
            HttpContext.Current.Response.Cookies.Add(cookietoname);
        }

        public static DateTime GetCookieOrSessionExpiresTime(CookieOrSessionExpiresTime expiresTimeType)
        {
            switch (expiresTimeType)
            {
                case CookieOrSessionExpiresTime.OneDay: return DateTime.Now.AddDays(1);
                case CookieOrSessionExpiresTime.OneWeek: return DateTime.Now.AddDays(7);
                case CookieOrSessionExpiresTime.OneMonth: return DateTime.Now.AddMonths(1);
                case CookieOrSessionExpiresTime.OneYear: return DateTime.Now.AddYears(1);
                default: return DateTime.Now;
            }
        }
        //加载话题在的图片
        public static String getImageToTopic(String imgids, int showcount)
        {
            try
            {
                string[] imgs = imgids.Split(',');
                string imgstr = string.Empty;
                int indexcount = showcount > imgs.Length - 1 ? imgs.Length - 1 : showcount;
                for (int i = 0; i < indexcount; i++)
                {
                    imgstr += "<img src=\"" + getImagePath(imgs[i], ImageType.Original) + "\" title=\"点击放大\" data-topicimgid=\"" + imgs[i] + "\" onload=\"ImageZoom(this)\" width=\"10\" height=\"10\" />";
                }
                return imgstr;
            }
            catch
            {
                return imgids;
            }
        }

        //public static List<string> timestrlist = new List<string> { 
        //    "5秒前", "10秒前", "15秒前", "20秒前", "25秒前", "30秒前", "35秒前", "40秒前", "45秒前", "50秒前", "55秒前",
        //    "5分钟前", "10分钟前", "15分钟前", "20分钟前", "25分钟前", "30分钟前", "35分钟前", "40分钟前", "45分钟前", "50分钟前", "55分钟前",
        //    "1小时前", "4小时前", "8小时前", "12小时前", "16小时前", "20小时前",
        //    "1天前", "2天前", "3天前", "4天前", "5天前", "6天前", "一周前"};
        //public static List<long> timetimelist = new List<long> { 
        //    5, 10, 15, 20, 25, 30, 35, 40, 45, 50,55,
        //    5 * 60, 10 * 60, 15 * 60, 20 * 60, 25 * 60, 30 * 60, 35 * 60, 40 * 60, 45 * 60, 50 * 60, 55*60,
        //    1 * 60 * 60, 4 * 60 * 60, 8 * 60 * 60, 12 * 60 * 60, 16 * 60 * 60, 20 * 60 * 60,
        //    1 * 60 * 60 * 24, 2 * 60 * 60 * 24, 3 * 60 * 60 * 24, 4 * 60 * 60 * 24, 5 * 60 * 60 * 24, 6 * 60 * 60 * 24, 7 * 60 * 60 * 24};
        public static List<string> timestrlist = new List<string> { "刚刚", "今天", "昨天", "前天" };
        public static List<long> timetimelist = new List<long> { 60 * 60, 1 * 60 * 60 * 24, 2 * 60 * 60 * 24, 3 * 60 * 60 * 24 };
        public static String getTimeline(DateTime puttime)
        {
            if (HttpContext.Current.Session["timelist"] == null)
            {
                HttpContext.Current.Session["timelist"] = new List<long>();
            }
            List<long> timelist = HttpContext.Current.Session["timelist"] as List<long>;
            puttime = puttime.AddHours(8);//加八个小时
            DateTime nowtime = DateTime.Now;
            bool ifExceed = false;
            DateTime showtime = DateTime.Parse(puttime.ToString("yyyy-MM-dd HH:mm:ss"));
            TimeSpan span = (TimeSpan)(nowtime - showtime);
            long timeca = (long)span.TotalMilliseconds / 1000;
            if (timetimelist[timetimelist.Count - 1] > timeca)
            {
                if (timelist.Count <= 0 || (timelist.Count > 0 && (timelist[timelist.Count - 1] < timeca)))
                {
                    for (var j = 0; j < timetimelist.Count; j++)
                    {
                        if (timetimelist[j] > timeca)
                        {
                            timelist.Add(timetimelist[j]);
                            ifExceed = false;
                            HttpContext.Current.Session["timelist"] = timelist;
                            return timestrlist[j];
                        }
                        else { ifExceed = true; }
                    }
                }
                else
                {
                    return "";
                }
            }
            else
            {
                ifExceed = true;
            }
            if (ifExceed)
            {
                TimeSpan showtimespan = (DateTime.Parse(showtime.ToString("yyyy-MM-dd")+" 00:00:00") - DateTime.Parse("1970-1-1 00:00:00"));
                if ((timelist.Count > 0 && timelist[timelist.Count - 1] <= timetimelist[timetimelist.Count - 1]) || timelist.Count <= 0)
                {
                    timelist.Add(showtimespan.Ticks / 1000 / 1000 * 100);
                    HttpContext.Current.Session["timelist"] = timelist;
                    return showtime.ToString("yyyy-MM-dd");
                }
                else
                {
                    if (timelist[timelist.Count - 1] > ((showtimespan).Ticks / 1000 / 1000 * 100))
                    {
                        timelist.Add(showtimespan.Ticks / 1000 / 1000 * 100);
                        HttpContext.Current.Session["timelist"] = timelist;
                        return showtime.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            return "";
        }
        //替换回车
        public static String replaceToN(String content)
        {
            for (var i = 0; i < content.Length; i++)
            {
                if (content.IndexOf("\n") >= 0)
                {
                    content = content.Replace("\n", "<br/>");
                }
                else
                {
                    break;
                }
            }
            return content;
        }
        //截取指定长度的字符串
        public static String getSubStringToIndex(string content, int index)
        {
            return content = content.Length > index ? content.Substring(0, index) + ".." : content;
        }

        //随机排序数组
        public static T[] ArrayRandomSort<T>(T[] array)
        {
            int len = array.Length;
            List<int> list = new List<int>();
            T[] ret = new T[len];
            Random rand = new Random();
            int i = 0;
            while (list.Count < len)
            {
                int iter = rand.Next(0, len);
                if (!list.Contains(iter))
                {
                    list.Add(iter);
                    ret[i] = array[iter];
                    i++;
                }

            }
            return ret;
        } 
    }

    public enum ImageType
    {
        Original,
        Icon,
        Small,
        Middle
    }
    public enum CookieOrSessionExpiresTime
    {
        OneDay, OneWeek, OneMonth, OneYear
    }
}
