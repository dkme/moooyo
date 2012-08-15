using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.IO;
using System.Web.Script.Serialization;
using System.Drawing;

namespace Moooyo.WebUI.Models
{
    public class DisplayObjProvider
    {
        public static string getMembersJson(string mid, string province, int sex, int agetype, int searchtype, int pagesize, int pageno)
        {
            IList<BiZ.Member.Member> list = BiZ.MemberManager.MemberManager.GetMembers(province, sex, (BiZ.Member.AgeType)agetype, (BiZ.Member.SearchType)searchtype, BiZ.Member.HasPhotoType.True, pagesize, pageno);
            List<Models.RelationDisplayObj> objs = new List<Models.RelationDisplayObj>();
            foreach (BiZ.Member.Member mem in list)
            {
                Models.RelationDisplayObj obj = getRelationDisplayObj(mid, mem);
                if (obj != null) objs.Add(obj);
            }
            return new JavaScriptSerializer().Serialize(objs);
        }
        //public static string getScoreMembersJson(string mid, string province, int sex, int count)
        //{
        //    IList<BiZ.Score.ScoreMember> list = BiZ.MemberManager.MemberManager.GetScoreMembersByProvince(province, sex, count);

        //    return new JavaScriptSerializer().Serialize(list);
        //}
        //public static string getUnRegScoreMembersJson(int sex, int count)
        //{
        //    IList<BiZ.Score.ScoreMember> list = BiZ.MemberManager.MemberManager.GetUnRegScoreMembers("全部",-1,5);

        //    return new JavaScriptSerializer().Serialize(list);
        //}
        public static string getVisitorListJson(string mid, int pagesize, int pageno)
        {
            return new JavaScriptSerializer().Serialize(getVisitorList(mid,pagesize,pageno));
        }
        public static List<Models.RelationDisplayObj> getVisitorList(string mid, int pagesize, int pageno)
        {
            IList<BiZ.Member.Relation.Visitor> list = BiZ.Member.Relation.RelationProvider.GetVistors(mid, pagesize, pageno);
            List<Models.RelationDisplayObj> objs = new List<Models.RelationDisplayObj>();
            foreach (BiZ.Member.RelationMember vis in list)
            {
                Models.RelationDisplayObj obj = getRelationDisplayObj(mid, vis);
                if (obj != null) objs.Add(obj);
            }
            return objs;
        }
        public static string getDisablesListJson(string mid, int pagesize, int pageno)
        {
            return new JavaScriptSerializer().Serialize(getDisablesList(mid,pagesize,pageno));
        }
        public static IList<Models.RelationDisplayObj> getDisablesList(string mid, int pagesize, int pageno)
        {
            IList<BiZ.Member.Relation.Disabler> list = BiZ.Member.Relation.RelationProvider.GetDisablers(mid, pagesize, pageno);
            List<Models.RelationDisplayObj> objs = new List<Models.RelationDisplayObj>();
            foreach (BiZ.Member.RelationMember vis in list)
            {
                Models.RelationDisplayObj obj = getRelationDisplayObj(mid, vis);
                if (obj != null) objs.Add(obj);
            }
            return objs;
        }
        public static string getFavorMeListJson(string mid, int pagesize, int pageno)
        {
            return new JavaScriptSerializer().Serialize(getFavorMeList(mid, pagesize, pageno));
        }
        public static IList<Models.RelationDisplayObj> getFavorMeList(string mid, int pagesize, int pageno)
        {
            IList<BiZ.Member.Relation.Favorer> list = BiZ.Member.Relation.RelationProvider.GetListWhoFavoredMe(mid, pagesize, pageno);
            List<Models.RelationDisplayObj> objs = new List<Models.RelationDisplayObj>();
            foreach (BiZ.Member.RelationMember vis in list)
            {
                Models.RelationDisplayObj obj = getRelationDisplayObj(mid, vis);
                if (obj != null) objs.Add(obj);
            }
            return objs;
        }
        public static int getFavorMeCount(String toMember)
        {
            return BiZ.Member.Relation.RelationProvider.GetListWhoFavoredMeCount(toMember);
        }
        //public static IList<Models.RelationDisplayObj> GetAboutMeActivityList(string mid, int pagesize, int pageno)
        //{
        //    IList<BiZ.Member.Activity.ActivityHolderRelatedToMe> amaList = BiZ.Member.Activity.ActivityController.GetMemberRelationActivitys(mid, 0, 0);
        //    IList<BiZ.Member.Relation.Visitor> vistitList = BiZ.Member.Relation.RelationProvider.GetVistors(mid, 0, 0);
        //    List<BiZ.Member.Activity.AboutMeAndVisitActivity> amava = new List<BiZ.Member.Activity.AboutMeAndVisitActivity>();

        //    foreach (BiZ.Member.Activity.ActivityHolderRelatedToMe ama in amaList)
        //    {
        //        BiZ.Member.Activity.AboutMeAndVisitActivity aboutMeVisitActive = new BiZ.Member.Activity.AboutMeAndVisitActivity(ama);
        //        amava.Add(aboutMeVisitActive);
        //    }
        //    foreach (BiZ.Member.Relation.Visitor visit in vistitList)
        //    {
        //        BiZ.Member.Activity.AboutMeAndVisitActivity aboutMeVisitActive = new BiZ.Member.Activity.AboutMeAndVisitActivity(visit);
        //        amava.Add(aboutMeVisitActive);
        //    }

        //    var qAboutMeVisitList = (
        //        from aboutMeVisit in amava orderby aboutMeVisit.LastOperationTime, aboutMeVisit.CreatedTime descending select aboutMeVisit);

        //    List<BiZ.Member.Activity.AboutMeAndVisitActivity> amava2 = new List<BiZ.Member.Activity.AboutMeAndVisitActivity>();
        //    amava2.AddRange(qAboutMeVisitList);

        //    List<Models.RelationDisplayObj> objs = new List<Models.RelationDisplayObj>();
        //    foreach (BiZ.Member.Activity.AboutMeAndVisitActivity aboutMeVisit in amava2)
        //    {
        //        BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(aboutMeVisit.FromMember);
        //        Models.RelationDisplayObj obj = getRelationDisplayObj(mid, member);
        //        if (obj != null) objs.Add(obj);
        //    }

        //    //分页
        //    pageno = (pageno == 0) ? 1 : pageno;
        //    if (pagesize >= 1) objs = objs.Skip((pageno - 1) * pagesize).Take(pagesize).ToList();

        //    return objs;
        //}
        public static IList<Models.RelationDisplayObj> GetAboutMeActivityList(string mid, int pagesize, int pageno)
        {
            IList<BiZ.Member.Activity.ActivityHolderRelatedToMe> amaList = BiZ.Member.Activity.ActivityController.GetMemberRelationActivitys(mid, pagesize, pageno);
            List<Models.RelationDisplayObj> objs = new List<Models.RelationDisplayObj>();
            foreach (BiZ.Member.Activity.ActivityHolderRelatedToMe aboutMeVisit in amaList)
            {
                BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(aboutMeVisit.FromMemberID);
                Models.RelationDisplayObj obj = getRelationDisplayObj(mid, member);
                if (obj != null) objs.Add(obj);
            }
            return objs;
        }
        public static string getIFavoredListJson(string mid, int pagesize, int pageno)
        {
            return new JavaScriptSerializer().Serialize(getIFavoredList(mid,pagesize,pageno));
        }
        public static IList<Models.RelationDisplayObj> getIFavoredList(string mid, int pagesize, int pageno)
        {
            IList<BiZ.Member.Relation.Favorer> list = BiZ.Member.Relation.RelationProvider.GetFavorers(mid, pagesize, pageno);
            List<Models.RelationDisplayObj> objs = new List<Models.RelationDisplayObj>();
            foreach (BiZ.Member.RelationMember vis in list)
            {
                Models.RelationDisplayObj obj = getRelationDisplayObj(mid, vis);
                if (obj != null) objs.Add(obj);
            }
            return objs;
        }
        public static int getIFavoredCount(String fromMember)
        {
            return BiZ.Member.Relation.RelationProvider.GetFavorersCount(fromMember);
        }

        //public static string getLastMsgersJson(string mid, int pagesize, int pageno)
        //{
        //    IList<BiZ.Member.Relation.LastMsger> list = BiZ.Member.Relation.RelationProvider.GetLastMsgers(mid, pagesize, pageno);
        //    List<Models.MsgerDisplayObj> objs = new List<Models.MsgerDisplayObj>();
        //    foreach (BiZ.Member.Relation.LastMsger vis in list)
        //    {
        //        Models.MsgerDisplayObj mobj = getMsgerDisplayObj(mid, vis);
        //        if (mobj != null) objs.Add(mobj);
        //    }
        //    return new JavaScriptSerializer().Serialize(objs);
        //}
        public static string GetPrivateAndSysMesgesJson(string mid, int pagesize, int pageno)
        {
            IList<BiZ.Member.Relation.LastMsger> lMsgsList = BiZ.Member.Relation.RelationProvider.GetLastMsgers(mid, 0, 0);
            IList<BiZ.Sys.SystemMsg.SystemMsg> sMsgsList = BiZ.Sys.SystemMsg.SystemMsgProvider.GetMsgs(mid, 0, 0);
            List<BiZ.Member.Relation.PrivateAndSysMesge> privSysMsgList = new List<BiZ.Member.Relation.PrivateAndSysMesge>();

            foreach (BiZ.Member.Relation.LastMsger lm in lMsgsList)
            {
                BiZ.Member.Relation.PrivateAndSysMesge psm = new BiZ.Member.Relation.PrivateAndSysMesge(lm);
                privSysMsgList.Add(psm);
            }

            foreach (BiZ.Sys.SystemMsg.SystemMsg sm in sMsgsList)
            {
                BiZ.Member.Relation.PrivateAndSysMesge psm = new BiZ.Member.Relation.PrivateAndSysMesge(sm);
                privSysMsgList.Add(psm);
            }

            var qPrivSysMsgList = (
                from privSysMsg in privSysMsgList orderby privSysMsg.CreatedTime descending select privSysMsg);

            List<BiZ.Member.Relation.PrivateAndSysMesge> privSysMsgList2 = new List<BiZ.Member.Relation.PrivateAndSysMesge>();
            privSysMsgList2.AddRange(qPrivSysMsgList);

            List<Models.MsgerDisplayObj> objs = new List<Models.MsgerDisplayObj>();
            foreach (BiZ.Member.Relation.PrivateAndSysMesge psm in privSysMsgList2)
            {
                Models.MsgerDisplayObj mobj = getMsgerDisplayObj(mid, psm);
                if (mobj != null) 
                    objs.Add(mobj);
            }

            //分页
            pageno = (pageno == 0) ? 1 : pageno;
            if (pagesize >= 1) objs = objs.Skip((pageno - 1) * pagesize).Take(pagesize).ToList();

            return new JavaScriptSerializer().Serialize(objs);
        }
        public static string getMsgerJson(string me, string you)
        {
            return new JavaScriptSerializer().Serialize(getMsgerDisplayObj(me, you));
        }
        public static Models.MsgerDisplayObj getMsgerDisplayObj(string mid, BiZ.Member.Relation.PrivateAndSysMesge psm)
        {
            Models.MsgerDisplayObj obj = new Models.MsgerDisplayObj();
            obj.FromMember = psm.FromMember;
            obj.ToMember = psm.ToMember;
            obj.Comment = psm.Comment;
            obj.CreatedTime = psm.CreatedTime;

            obj.UnReads = psm.UnReads;

            String fromMember = obj.FromMember;
            BiZ.Member.Member mym = null;
            BiZ.Member.Member tomember = null;
            if (obj.FromMember != "" && obj.FromMember != null)
            {
                mym = BiZ.MemberManager.MemberManager.GetMember(obj.FromMember);
            }
            if (obj.ToMember != "" && obj.ToMember != null)
            {
                tomember = BiZ.MemberManager.MemberManager.GetMember(obj.ToMember);
            }

            if (tomember == null)
            {
                return null;
            }

            //获取源用户信息
            if (mid == psm.ToMember)
            {
                obj.DisplayFromOrTo = "from";
                getMemberDisplayObj(obj, mym);

                //获取关注状态
                obj.InFavor = BiZ.Member.Relation.RelationProvider.IsInFavor(obj.FromMember, obj.ToMember);
            }

            //获取目标用户信息
            if (fromMember != "")
            {
                if (mid == obj.FromMember)
                {
                    obj.DisplayFromOrTo = "to";
                    getMemberDisplayObj(obj, tomember);

                    //获取关注状态
                    obj.InFavor = BiZ.Member.Relation.RelationProvider.IsInFavor(obj.ToMember, obj.FromMember);
                }
            }
            
            //获取消息数量
            if (fromMember != "")
                obj.MsgBetweenMeCount = BiZ.Member.Link.MsgProvider.GetMsgCount(obj.FromMember, obj.ToMember);
            else
                obj.MsgBetweenMeCount = 0;

            //获取距离

            if (fromMember != "")
            {
                if (mym.MemberInfomation.Lat != 0 & mym.MemberInfomation.Lng != 0 & tomember.MemberInfomation.Lat != 0 & tomember.MemberInfomation.Lng != 0)
                {
                    obj.Distance = CBB.LocationFunctionHelper.DistanceAndAroundCalculator.getDistanceStr(
                            tomember.MemberInfomation.Lng,
                            tomember.MemberInfomation.Lat,
                            mym.MemberInfomation.Lng,
                            mym.MemberInfomation.Lat);
                }
            }
            else
                obj.Distance = "";

            return obj;
        }
        public static Models.MsgerDisplayObj getMsgerDisplayObj(string me, string you)
        {
            Models.MsgerDisplayObj obj = new Models.MsgerDisplayObj();
            obj.FromMember = me;
            obj.ToMember = you;

            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(obj.FromMember);
            BiZ.Member.Member tomember = BiZ.MemberManager.MemberManager.GetMember(obj.ToMember);

            obj.DisplayFromOrTo = "to";
            getMemberDisplayObj(obj, tomember);

            //获取关注状态
            obj.InFavor = BiZ.Member.Relation.RelationProvider.IsInFavor(me, you);
            //获取消息数量
            obj.MsgBetweenMeCount = BiZ.Member.Link.MsgProvider.GetMsgCount(me, you);
            //获取距离
            if (mym.MemberInfomation.Lat != 0 & mym.MemberInfomation.Lng != 0 & tomember.MemberInfomation.Lat != 0 & tomember.MemberInfomation.Lng != 0)
            {
                obj.Distance = CBB.LocationFunctionHelper.DistanceAndAroundCalculator.getDistanceStr(
                        tomember.MemberInfomation.Lng,
                        tomember.MemberInfomation.Lat,
                        mym.MemberInfomation.Lng,
                        mym.MemberInfomation.Lat);
            }
            else
                obj.Distance = "";

            return obj;
        }
        //获取用户显示数据，mid-显示的用户id
        public static Models.MemberFullDisplayObj getMemberFullDisplayObj(string mid)
        {
            Models.MemberFullDisplayObj obj = new MemberFullDisplayObj();
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(mid);

            getMemberFullDisplayObj(obj, mym);

            return obj;
        }
        //获取登录用户显示数据,mid-登录用户的ID
        public static Models.UserDisplayObj getUserDisplayObj(string mid)
        {
            Models.UserDisplayObj obj = new UserDisplayObj();
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(mid);

            getUserDisplayObj(obj, mym);

            return obj;
        }
        public static string GetWeDistance(string meId, String heId)
        {
            bool isGeoset = false;
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(heId);
            if (mym != null)
            {
                if (mym.MemberInfomation == null)
                {
                    isGeoset = false;
                }
                else
                {
                    if (mym.MemberInfomation.Lat != 0 & mym.MemberInfomation.Lng != 0)
                        isGeoset = true;
                }
            }
            String distance = "? 米";
            //计算距离
            if (isGeoset)
            {
                if (meId != null & meId != "")
                {
                    BiZ.Member.Member me = BiZ.MemberManager.MemberManager.GetMember(meId);

                    if (me.MemberInfomation.Lat != 0 & me.MemberInfomation.Lng != 0)
                    {
                        distance = CBB.LocationFunctionHelper.DistanceAndAroundCalculator.getDistanceStr(
                        me.MemberInfomation.Lng,
                        me.MemberInfomation.Lat,
                        mym.MemberInfomation.Lng,
                        mym.MemberInfomation.Lat);
                    }
                }
            }
            return distance;
        }
        public static bool IsInFavor(string meId, String taId)
        {
            bool isInFavor = BiZ.Member.Relation.RelationProvider.IsInFavor(meId, taId);
            return isInFavor;
        }
        public static string getGetOnlineStr(string memberid)
        {
            return BiZ.MemberManager.MemberManager.GetMember(memberid).OnlineStr;
        }
        //用户注册并选择兴趣上传图片后，创建一张用户展示图片，分享到用户版定的第三方平台
        public static string getUserIconToShare(string memberid)
        {
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(memberid);
            //创建背景
            Image coverback = Image.FromFile(HttpContext.Current.Server.MapPath("/pics/Register_UserIcon/upIcon_background.jpg"));
            Graphics g;
            //建立背景画布
            g = System.Drawing.Graphics.FromImage(coverback);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //画用户头像
            Image membericon = null;
            int userIconX = 132;
            int userIconY = 87;
            int userIconWidth = 100;
            int userIconHeight = 100;
            if (mym.MemberInfomation == null || mym.MemberInfomation.IconPath == null || mym.MemberInfomation.IconPath.Trim() == "")
            {
                membericon = Image.FromFile(HttpContext.Current.Server.MapPath("/pics/noicon.jpg"));
            }
            else
            {
                byte[] stream = new CBB.ImageHelper.ImageLoader().loadimage(mym.MemberInfomation.IconPath.Split('.')[0] + "_p.jpg");
                Stream st = new MemoryStream(stream);
                membericon = Image.FromStream(st);
            }
            membericon = new Bitmap(membericon, userIconWidth, userIconHeight);
            g.DrawImage(membericon, new Rectangle(userIconX, userIconY, userIconWidth, userIconHeight), 0, 0, userIconWidth, userIconHeight, GraphicsUnit.Pixel);
            //画用户昵称
            String userNickName = mym.MemberInfomation.NickName;
            float nickNameX = coverback.Width / 2 - (userNickName.Length * 18) / 2 - 2;
            float nickNameY = userIconY + userIconHeight + 15;
            float nickNameSize = 18;
            g.DrawString(userNickName, new Font("微软雅黑", nickNameSize, GraphicsUnit.Pixel), new SolidBrush(Color.FromArgb(68, 68, 68)), new PointF(nickNameX, nickNameY));

            //获取兴趣集合
            IList<BiZ.InterestCenter.Interest> interests = BiZ.InterestCenter.InterestFactory.GetMemberInterest(memberid, 0, 0);
            IList<BiZ.InterestCenter.Interest> interestList = new List<BiZ.InterestCenter.Interest>();
            //最多只能有三个兴趣
            foreach (var obj in interests)
            {
                if (interestList.Count < 3)
                    interestList.Add(obj);
                else
                    break;
            }
            //创建画兴趣的相关参数
            int interestWidth = 40;
            int interestHeight = 40;
            List<Font> fontList = new List<Font>() { 
                new Font("微软雅黑", 25, GraphicsUnit.Pixel), 
                new Font("微软雅黑", 18, GraphicsUnit.Pixel), 
                new Font("微软雅黑", 25, GraphicsUnit.Pixel) };
            List<SolidBrush> solidBrushList = new List<SolidBrush>() {  
                new SolidBrush(Color.FromArgb(100, 160, 180)),
                new SolidBrush(Color.FromArgb(158, 1, 2)), 
                new SolidBrush(Color.FromArgb(220, 140, 20)) };
            List<PointF> pointFList = new List<PointF>() { 
                new PointF(64, 284),
                new PointF(159, 319),
                new PointF(99, 344)};
            List<Rectangle> rectangleList = new List<Rectangle>() {
                new Rectangle(104, 234, interestWidth, interestHeight),
                new Rectangle(159, 234, interestWidth, interestHeight),
                new Rectangle(214, 234, interestWidth, interestHeight)};
            //读取兴趣外框
            string border = HttpContext.Current.Server.MapPath("/pics/Register_UserIcon/upIcon_interest_boder.png");
            System.IO.FileStream borderFileStream = new System.IO.FileStream(border, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader borderBinaryReader = new System.IO.BinaryReader(borderFileStream);
            long borderTotalBytes = new System.IO.FileInfo(border).Length;
            byte[] borderBytes = borderBinaryReader.ReadBytes((Int32)borderTotalBytes);
            Stream borderStream = new MemoryStream(borderBytes);
            Image interestBorder = Image.FromStream(borderStream);
            //加入兴趣
            for (int i = 0; i < interestList.Count; i++)
            {
                //获取当前兴趣对象
                var obj = interestList[i];
                //获取兴趣的图标
                string interestIconPath = obj.ICONPath;
                byte[] interestBytes = new CBB.ImageHelper.ImageLoader().loadimage(interestIconPath.Split('.')[0] + "_i.jpg");
                Stream interestStream = new MemoryStream(interestBytes);
                Image interestIcon = Image.FromStream(interestStream);
                interestIcon = new Bitmap(interestIcon, interestWidth, interestHeight);
                //画兴趣图标
                g.DrawImage(interestIcon, rectangleList[i], 0, 0, interestWidth, interestHeight, GraphicsUnit.Pixel);
                //画外框
                g.DrawImage(interestBorder, rectangleList[i], 0, 0, interestWidth, interestHeight, GraphicsUnit.Pixel);
                //获取兴趣名称
                string interestTitle = obj.Title;
                //画兴趣名称
                g.DrawString(interestTitle, fontList[i], solidBrushList[i], pointFList[i]);
            }
            //释放画板
            g.Dispose();
            //保存画好的图片的到临时文件夹
            string filename = "/temp_up_file/" + memberid + (DateTime.Now.ToString("yyyyMMddhhmmss") + DateTime.Now.Millisecond.ToString()) + ".jpg";
            coverback.Save(HttpContext.Current.Server.MapPath(filename), System.Drawing.Imaging.ImageFormat.Jpeg);
            //释放图片对象
            coverback.Dispose();
            //放回图片地址
            return filename;
        }
        //私有方法
        public static string getMsgsJson(string me, string you, int pagesize, int pageno)
        {
            IList<BiZ.Member.Link.Msg> list = BiZ.Member.Link.MsgProvider.GetMsgs(me, you, pagesize, pageno);
            return new JavaScriptSerializer().Serialize(list);
        }
        private static Models.RelationDisplayObj getRelationDisplayObj(String mid, BiZ.Member.RelationMember vis)
        {
            Models.RelationDisplayObj obj = new Models.RelationDisplayObj();
            obj.FromMember = vis.FromMember;
            obj.ToMember = vis.ToMember;
            obj.ID = vis.ID;
            obj.CreatedTime = vis.CreatedTime;
            obj.TimeSpan = Common.Comm.getTimeSpan(obj.CreatedTime);
            obj.Comment = vis.Comment;

            try
            {
                BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(obj.FromMember);
                BiZ.Member.Member tomember = BiZ.MemberManager.MemberManager.GetMember(obj.ToMember);

                if (mym == null || tomember == null) return null;

                //获取源用户信息
                if (mid == obj.ToMember)
                {
                    obj.DisplayFromOrTo = "from";
                    getMemberDisplayObj(obj, mym);
                }

                //获取目标用户信息
                if (mid == obj.FromMember)
                {
                    obj.DisplayFromOrTo = "to";
                    getMemberDisplayObj(obj, tomember);
                }

                //获取距离
                if (mym.MemberInfomation != null & mym.MemberInfomation.Lat != 0 & mym.MemberInfomation.Lng != 0 & tomember.MemberInfomation.Lat != 0 & tomember.MemberInfomation.Lng != 0)
                {
                    obj.Distance = CBB.LocationFunctionHelper.DistanceAndAroundCalculator.getDistanceStr(
                            tomember.MemberInfomation.Lng,
                            tomember.MemberInfomation.Lat,
                            mym.MemberInfomation.Lng,
                            mym.MemberInfomation.Lat);
                }
                else
                    obj.Distance = "";
            }
            catch { }

            return obj;
        }
        public static Models.RelationDisplayObj getRelationDisplayObj(String me, BiZ.Member.Member you)
        {
            if (you == null) return null;
            Models.RelationDisplayObj obj = new Models.RelationDisplayObj();
            obj.FromMember = me;
            obj.ToMember = you.ID;
            obj.ID = you.ID;
            obj.CreatedTime = you.CreatedTime;
            obj.Comment = "";

            obj.DisplayFromOrTo = "to";
            getMemberDisplayObj(obj, you);
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(obj.FromMember);

            if (mym == null) return null;
            //获取距离
            if (mym.MemberInfomation.Lat != 0 & mym.MemberInfomation.Lng != 0 & you.MemberInfomation.Lat != 0 & you.MemberInfomation.Lng != 0)
            {
                obj.Distance = CBB.LocationFunctionHelper.DistanceAndAroundCalculator.getDistanceStr(
                        you.MemberInfomation.Lng,
                        you.MemberInfomation.Lat,
                        mym.MemberInfomation.Lng,
                        mym.MemberInfomation.Lat);
            }
            else
                obj.Distance = "";

            //会员级别
            obj.MemberType = (int)mym.MemberType;

            return obj;
        }
        private static void getUserDisplayObj(Models.UserDisplayObj obj, BiZ.Member.Member mym)
        {
            if (mym == null) return;

            getMemberDisplayObj(obj, mym);
            obj.Email = mym.Email;
            obj.EmailIsVaild = mym.EmailIsVaild;
            obj.BindedPlatforms = MemberBindingPlatform(mym.ID);
        }
        internal static string MemberBindingPlatform(string memberId)
        {
            BiZ.Member.Connector.Connector sinaConn = BiZ.Member.Connector.ConnectorProvider.GetConnector<BiZ.Member.Connector.SinaWeiboConnector>(memberId, BiZ.Member.Connector.Platform.SinaWeibo);
            BiZ.Member.Connector.Connector txConn = BiZ.Member.Connector.ConnectorProvider.GetConnector<BiZ.Member.Connector.TXWeiboConnector>(memberId, BiZ.Member.Connector.Platform.TencentWeibo);
            BiZ.Member.Connector.Connector rrConn = BiZ.Member.Connector.ConnectorProvider.GetConnector<BiZ.Member.Connector.RenRenConnector>(memberId, BiZ.Member.Connector.Platform.RenRen);
            BiZ.Member.Connector.Connector dbConn = BiZ.Member.Connector.ConnectorProvider.GetConnector<BiZ.Member.Connector.DouBanConnector>(memberId, BiZ.Member.Connector.Platform.Douban);

            string bindedPlatforms = "";

            if (sinaConn != null && sinaConn.Enable)
            {
                bindedPlatforms += (int)sinaConn.PlatformType + "|";
            }
            if (txConn != null && txConn.Enable)
            {
                bindedPlatforms += (int)txConn.PlatformType + "|";
            }
            if (rrConn != null && rrConn.Enable)
            {
                bindedPlatforms += (int)rrConn.PlatformType + "|";
            }
            if (dbConn != null && dbConn.Enable)
            {
                bindedPlatforms += (int)dbConn.PlatformType + "|";
            }
            return bindedPlatforms;
        }
        private static void getMemberFullDisplayObj(Models.MemberFullDisplayObj obj, BiZ.Member.Member mym)
        {
            if (mym == null) return;

            getMemberDisplayObj(obj, mym);
            if (mym.MemberInfomation != null)
            {
                obj.Birthday = mym.MemberInfomation.Birthday;
                obj.PropertySituation = mym.MemberInfomation.PropertySituation;
                obj.PersonalIntroduction = mym.MemberInfomation.PersonalIntroduction;
                //obj.Figure = (mym.MemberInfomation.Figure == "") ? "问我" : mym.MemberInfomation.Figure;
                obj.Star = (mym.MemberInfomation.Star == "") ? "问我" : mym.MemberInfomation.Star;

                //String hometown = mym.MemberInfomation.Hometown.Replace('|', ' ');
                //String[] arrHometown = hometown.Split(' ');
                //if (arrHometown.Length == 2)
                //{
                //    if (arrHometown[0] == arrHometown[1]) hometown = arrHometown[0];
                //}


                //obj.Hometown = (hometown == "") ? "问我" : hometown;
                //obj.Hometown = (mym.MemberInfomation.Hometown == "") ? "问我" : mym.MemberInfomation.Hometown.Replace('|', ' ');
                obj.EducationalBackground = (mym.MemberInfomation.EducationalBackground == "") ? "问我" : mym.MemberInfomation.EducationalBackground;
                if (mym.MemberInfomation.MemberSkin != null)
                    obj.MemberSkin = mym.MemberInfomation.MemberSkin;

            }

            #region 计数器
            if (mym.Status != null)
            {
                obj.InterViewCount = mym.Status.InterViewCount.ToString();
                obj.Last24HOutCallsCount = mym.Status.Last24HOutCallsCount;
                obj.FavorMemberCount = mym.Status.FavorMemberCount;
                obj.MemberFavoredMeCount = mym.Status.MemberFavoredMeCount;
                obj.UnReadBeenFavorCount = mym.Status.UnReadBeenFavorCount;
                obj.UnReadBeenViewedTimes = mym.Status.UnReadBeenViewedTimes;
                obj.UnReadMsgCount = mym.Status.UnReadMsgCount;
                obj.UnReadSystemMsgCount = mym.Status.UnReadSystemMsgCount;
                obj.UnReadActivitysAboutMeCount = mym.Status.UnReadActivitysAboutMeCount;
                obj.loginCount = mym.Status.LoginTimes;
            }
            else
            {
                obj.InterViewCount = "0";
                //obj.SkillCount = "0";
            }
            #endregion
        }
        private static void getMemberDisplayObj(Models.MemberDisplayObj obj, BiZ.Member.Member mym)
        {
            if (mym == null) return;
            obj.ID = mym.ID;

            #region 增强的唯一编号
            obj.UniqueNumber = BiZ.Comm.UniqueNumber.UniqueNumberProvider.GetConvertedMemberID(mym.ID, BiZ.Comm.UniqueNumber.IDType.MemberID);
            #endregion

            #region 用户魅力封号
            int Glamour = mym.Status.GlamourCount;

            string[,] title = new string[,] { { "勇气小男生", "可爱小女生" }, { "魅力型男", "多情少女" }, { "花心帅哥", "半妆美人" }, { "俊秀公子", "江南名媛" }, { "玉面郎君", "玉面佳人" }, { "天慕王子", "惊鸿贵人" } };
            int count = title.GetLength(0);
            int tempnumber = 0;
            for (int i = 0; i < count;i++ )
            {
                tempnumber = tempnumber * (i+1) + 10;
                if (Glamour > tempnumber)
                {
                    if (i + 1 >= count) 
                    {
                        obj.MemberTitle = mym.Sex == 1 ? "绝世魅力天王" : "倾国倾城天后";
                        break;
                    }
                    continue;
                }
                else 
                {
                    obj.MemberTitle = mym.Sex == 1 ? title[i,0] : title[i,1];
                    break;
                }
            }
            #endregion

            #region 用户头像
            if ((mym.MemberPhoto == null) || (mym.MemberPhoto.IconID == null) || (mym.MemberPhoto.IconID == ""))
            {
                obj.ICONPath = "/pics/noicon.jpg";
                obj.BigImg = "/pics/nobigimg.jpg";
                obj.MinICON = "/pics/noicon.jpg";
            }
            else
            {
                if (mym.MemberInfomation != null & mym.MemberInfomation.IconPath != "")
                {
                    obj.ICONPath = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath") + "/" + mym.MemberInfomation.IconPath.Replace("\\", "/").Split('.')[0] + "_p" + ".jpg";
                    obj.BigImg = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath") + "/" + mym.MemberInfomation.IconPath;
                    obj.MinICON = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("uploadPath") + "/" + mym.MemberInfomation.IconPath.Replace("\\", "/").Split('.')[0] + "_i" + ".jpg";
                }
                else
                {
                    obj.ICONPath = "/pics/noicon.jpg";
                    obj.BigImg = "/pics/nobigimg.jpg";
                    obj.MinICON = "/pics/noicon.jpg";
                }
            }
            #endregion

            #region 基本信息
            if (mym.MemberInfomation != null)
            {
                obj.Name = mym.MemberInfomation.NickName;
                obj.Email = mym.Email;
                obj.Want = mym.MemberInfomation.IWant;
                obj.Career = (mym.MemberInfomation.Career == "") ? "问我" : mym.MemberInfomation.Career;
                //String city = mym.MemberInfomation.City.Replace('|', ' ');
                //String[] arrCity = city.Split(' ');
                //if(arrCity[0] == arrCity[1]) city = arrCity[0];
                //obj.City = city;
                obj.City = (mym.MemberInfomation.City == "") ? "问我" : mym.MemberInfomation.City.Replace('|', '@');
                obj.Lat = mym.MemberInfomation.Lat;
                obj.Lng = mym.MemberInfomation.Lng;
                obj.Age = (mym.MemberInfomation.Age == "") ? "问我" : mym.MemberInfomation.Age;
                obj.Height = (mym.MemberInfomation.Height == "") ? "问我" : mym.MemberInfomation.Height;
            }
            #endregion

            #region 设置信息
            if (mym.Settings != null)
            {
                if (mym.Settings.FansGroupName != null)
                {
                    obj.FansGroupName = new BiZ.Member.FansGroupName();
                    obj.FansGroupName.Name = mym.Settings.FansGroupName.Name;
                    obj.FansGroupName.FirstName = mym.Settings.FansGroupName.FirstName;
                    obj.FansGroupName.Second = mym.Settings.FansGroupName.Second;
                    obj.FansGroupName.TheThird = mym.Settings.FansGroupName.TheThird;
                }
                obj.HiddenMyLoc = mym.Settings.HiddenMyLoc;
            }
            #endregion

            #region 性别
            obj.Sex = mym.Sex;
            if (obj.Sex == 1) obj.WantSex = 2;
            if (mym.Sex == 1)
            {
                obj.SexStr = "男";
                obj.WantSexStr = "女";
            }
            else if (mym.Sex == 2)
            {
                obj.SexStr = "女";
                obj.WantSexStr = "男";
            }
            else
            {
                obj.SexStr = "问我";
                obj.WantSexStr = "问我";
            }
            #endregion

            #region 状态
            obj.OnlineStr = mym.OnlineStr;
            if (mym.Status != null)
            {
                obj.Points = mym.Status.Points;
                obj.GlamourCount = mym.Status.GlamourCount;
                obj.PointsSchedule = mym.Status.PointsSchedule;

                if (mym.Status.Last24HInCallsCount < 6)
                    obj.Hot = "normal";
                if (mym.Status.Last24HInCallsCount >= 6 & mym.Status.Last24HInCallsCount < 10)
                    obj.Hot = "hot1";
                if (mym.Status.Last24HInCallsCount >= 10 & mym.Status.Last24HInCallsCount < 20)
                    obj.Hot = "hot2";
                if (mym.Status.Last24HInCallsCount >= 20)
                    obj.Hot = "hot3";
            }
            obj.AllowLogin = mym.AllowLogin;
            #endregion

            #region 计数器
            if (mym.Status != null)
            {
                obj.PhotoCount = mym.Status.PhotoCount.ToString();
                obj.InterestCount = mym.Status.InterestCount.ToString();
            }
            else
            {
                obj.PhotoCount = "0";
                obj.InterestCount = "0";
            }
            #endregion

            #region 用户兴趣
            if (mym.Status != null && mym.Status.InterestCount > 0)
            {
                obj.InterestList = BiZ.InterestCenter.InterestFactory.GetMemberInterest(obj.ID, 10, 1);
            }
            #endregion

            #region 级别和认证
            //会员级别
            obj.MemberType = (int)mym.MemberType;
            if (mym.MemberType == BiZ.Member.MemberType.Level0)
                obj.MemberLevel = "普通会员";
            if (mym.MemberType == BiZ.Member.MemberType.Level1)
                obj.MemberLevel = "高级会员";
            if (mym.MemberType == BiZ.Member.MemberType.Level2)
                obj.MemberLevel = "VIP";
            //头像认证
            //视频认证
            if (mym.MemberPhoto != null)
                obj.IsRealPhotoIdentification = mym.MemberPhoto.IsRealPhotoIdentification;
            //身份证认证
            //Email验证
            obj.EmailIsVaild = mym.EmailIsVaild;

            #endregion

            #region 用户徽章
                obj.Badgelist = mym.Status.MemberBadge;
            #endregion
        }
    }
}
