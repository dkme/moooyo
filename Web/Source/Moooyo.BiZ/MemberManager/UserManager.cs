using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace WoXi.BiZ.UserManager
{
    /// <summary>
    /// 用户管理类
    /// </summary>
    public class MemberManager
    {
        #region 用户查询
        public static IList<Member.Member> GetActivMember(DateTime lastoperationtime)
        {
            try
            {
                MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
               "Members",
               Query.And(Query.EQ("Au", 2),Query.GTE("LastOperationTime",lastoperationtime)),
               SortBy.Descending("LastOperationTime"),
               0,
               0);

                List<Member.Member> objs = new List<Member.Member>();
                objs.AddRange(mc);

                return objs;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        #endregion
        #region 打分用户
        public static IList<Score.ScoreMember> GetScoreMembersByMemberID(String mid, int count)
        {
            if (mid != "")
            {
                Member.Member mym = UserManager.MemberManager.GetMember(mid);
                if (mym == null) return new List<Score.ScoreMember>();
                int wantsex = 1;
                if (mym.Sex == 1) wantsex = 2;

                return GetScoreMembersByProvince(mym.MemberInfomation.City.Split('|')[0], wantsex, count);
            }
            else
            {
                return new List<Score.ScoreMember>();
            }
        }
        public static IList<Score.ScoreMember> GetScoreMembersByProvince(String province,int wantsex, int count)
        {
            List<Score.ScoreMember> returnlist = new List<Score.ScoreMember>();

            IList<Member.Member> ml = GetMembers(province ,wantsex, Member.AgeType.All, Member.SearchType.All, true, Member.HasPhotoType.True, count, 100, 1);

            //如果本地区数量不足，则获取所有地区
            if (ml.Count < count)
                ml = GetMembers("全部", wantsex, Member.AgeType.All, Member.SearchType.All, true, Member.HasPhotoType.True, count, 100, 1);

            foreach (Member.Member obj in ml)
            {
                Score.ScoreMember sm = new Score.ScoreMember();
                sm.ID = obj.ID;
                sm.OnlineStr = obj.OnlineStr;

                if (obj.MemberInfomation == null) continue;

                sm.Age = obj.MemberInfomation.Age.ToString();
                sm.Sex = obj.Sex;
                sm.IWant = obj.MemberInfomation.IWant;
                sm.NickName = obj.MemberInfomation.NickName;
                sm.City = obj.MemberInfomation.City;
                sm.Career = obj.MemberInfomation.Career;
                sm.IconPath = obj.MemberInfomation.IconPath;
                sm.Soliloquize = obj.MemberInfomation.Soliloquize;

                if (obj.MemberPhoto != null)
                {
                    sm.IconID = obj.MemberPhoto.IconID;
                    if (sm.IconID != "")
                    {
                        Photo.Photo ph = Photo.PhotoManager.GetPhoto(sm.IconID);
                        sm.IconCommentsCount = ph.CommentCount;
                    }
                }
                if (obj.Status != null)
                {
                    sm.PhotoCount = obj.Status.PhotoCount;
                    sm.ScoreAvg = obj.Status.ScoreAvg.ToString("#0.0");
                }

                returnlist.Add(sm);
            }
            

            return returnlist;
        }
        public static IList<Score.ScoreMember> GetUnRegScoreMembers(String province, int wantsex, int count)
        {
            List<Score.ScoreMember> returnlist = new List<Score.ScoreMember>();

            IList<Member.Member> ml = GetGoodMembers(province, wantsex, true, count, 100, 1);

            foreach (Member.Member obj in ml)
            {
                Score.ScoreMember sm = new Score.ScoreMember();
                sm.ID = obj.ID;
                sm.OnlineStr = obj.OnlineStr;

                if (obj.MemberInfomation == null) continue;

                sm.Age = obj.MemberInfomation.Age.ToString();
                sm.Sex = obj.Sex;
                sm.IWant = obj.MemberInfomation.IWant;
                sm.NickName = obj.MemberInfomation.NickName;
                sm.City = obj.MemberInfomation.City;
                sm.Career = obj.MemberInfomation.Career;
                sm.IconPath = obj.MemberInfomation.IconPath;

                if (obj.MemberPhoto != null)
                {
                    sm.IconID = obj.MemberPhoto.IconID;
                    if (sm.IconID != "")
                    {
                        Photo.Photo ph = Photo.PhotoManager.GetPhoto(sm.IconID);
                        sm.IconCommentsCount = ph.CommentCount;
                    }
                }
                if (obj.Status != null)
                {
                    sm.PhotoCount = obj.Status.PhotoCount;
                    sm.ScoreAvg = obj.Status.ScoreAvg.ToString("#0.0");
                }

                returnlist.Add(sm);
            }


            return returnlist;
        }
        #endregion
        #region 注册操作
        /// <summary>
        /// 通过填写Email预注册用户
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Member.Member AddNewPreMemberByEmailRegister(String email, int sex, String pwd)
        {
            try
            {
                Member.Member mym = AddMember(email, pwd, "", sex);
                return mym;
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        public static Member.Member AddNewPreMemberByUploadPhoto(String photoID)
        {
            try
            {
                Member.Member mym = AddMember("", "", "", 0);
                return mym;
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        public static Member.Member GetPreMemberByEmail(String email, String emailpwd)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<BiZ.Member.Member> mc = md.GetCollection<BiZ.Member.Member>("Members");
            IMongoQuery qc = Query.And(Query.EQ("Email", email), Query.EQ("EmailPwd", emailpwd));
            BiZ.Member.Member member = mc.FindOne(qc);
            return member;
        }
        public static Member.Member GetPreMemberByID(String id)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<BiZ.Member.Member> mc = md.GetCollection<BiZ.Member.Member>("Members");
            IMongoQuery qc = Query.EQ("_id", ObjectId.Parse(id));
            BiZ.Member.Member member = mc.FindOne(qc);
            return member;
        }
        public static IList<Member.Member> GetNewUsersToAudit(int pagesize,int pageno)
        {
            MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
            "Members",
            Query.EQ("Au",0),
            SortBy.Ascending("CreatedTime"),
            pageno,
            pagesize);

            List<Member.Member> objs = new List<Member.Member>();
            objs.AddRange(mc);

            return objs;
        }
        public static IList<Member.Member> GetNewUsersToMark(int sex,int pagesize, int pageno)
        {
            MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
            "Members",
            Query.And(Query.EQ("Sex", sex), Query.EQ("Au", 1), Query.GT("CreatedTime", DateTime.Now.AddMinutes(-5))),
            SortBy.Ascending("CreatedTime"),
            0,
            0);

            List<Member.Member> objs = new List<Member.Member>();
            objs.AddRange(mc);

            if (objs.Count == 0)
            {
                objs = GetMembers("全部", sex, Member.AgeType.All, Member.SearchType.All, true,Member.HasPhotoType.True, 1, 100, 1).ToList<Member.Member>(); ;
            }
            else
            {
                Random rnd = new Random(DateTime.Now.Millisecond);
                IEnumerable<Member.Member> rndlist = objs.OrderBy(x => rnd.Next()).Take(pagesize);
                List<Member.Member> randomobjs = rndlist.ToList();
                objs = randomobjs;
            }

            return objs;
        }
        public static CBB.ExceptionHelper.OperationResult SetAuditResult(String mid,int sex, int auditresult)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(Query.EQ("_id", ObjectId.Parse(mid)),
                Update.Combine(Update.Set("Au", auditresult), Update.Set("Sex", sex), Update.Set("MemberInfomation.Sex", sex), Update.Set("MemberInfomation.NickName", sex==1?"他":"她"))
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        /// <summary>
        ///  激活Email
        /// </summary>
        /// <param name="preID"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool UpdateEmailIsVaild(String email, String pwd)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<BiZ.Member.Member> mc = md.GetCollection<BiZ.Member.Member>("Members");
            IMongoQuery qc = Query.And(Query.EQ("Email", email), Query.EQ("Password", pwd));
            IMongoSortBy sb = SortBy.Ascending(new string[]{"Email"});
            IMongoUpdate up = Update.Set("EmailIsVaild",true);
            FindAndModifyResult result = mc.FindAndModify(qc, sb, up);
            return result.Ok;
        }
        public static bool GetIsEmailUsed(String email)
        {
            // 预定成功返回值
            long emailcount = 0;

            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection mc = md.GetCollection("Members");
                IMongoQuery qc = Query.EQ("Email", email);
                emailcount = mc.Count(qc);
                if (emailcount > 0) return false;
                return true;
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }

        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="preRegisterID"></param>
        /// <param name="email"></param>
        /// <param name="nickName"></param>
        /// <param name="birthday"></param>
        /// <param name="pwd"></param>
        /// <param name="iconNo"></param>
        /// <param name="sex"></param>
        /// <returns>返回用户ID</returns>
        public static Member.Member AddMember(String email, String pwd, String iconID, int sex)
        {
            try
            {
                Member.Member mym = new Member.Member();
                mym.MemberInfomation = new Member.MemberInfomation();
                mym.MemberPhoto = new Member.MemberPhoto();
                mym.Status = new Member.MemberStatus();
                mym.Settings = new Member.MemberSetting();
                mym.AllowLogin = true;

                mym.MemberPhoto.IconID = iconID;
                if (iconID != "")
                {
                    Photo.Photo myp = Photo.PhotoManager.GetPhoto(iconID);
                    mym.MemberInfomation.IconPath = myp.FileName;
                }

                mym.Settings.AutoAddOutCallingToMyFavorList = true;
                mym.Settings.HiddenMyPhoto = false;
                mym.Settings.StopMyAccount = false;

                mym.Email = email;
                mym.Password = CBB.Security.MD5Helper.getMd5Hash(pwd);
                mym.CreatedTime = DateTime.Now;
                mym.Sex = sex;
                mym.MemberInfomation.Sex = sex;

                mym.FinishedReg = false;
                mym.EmailIsVaild = false;

                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
                mc.Insert(mym);

                return mym;

            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        public static CBB.ExceptionHelper.OperationResult SetEmailAndSex(String id,String email,int sex)
        {
            BiZ.Member.Member mym = BiZ.UserManager.MemberManager.GetMember(id);
            mym.Sex = sex;
            mym.MemberInfomation.Sex = sex;
            mym.Email = email;
            SaveMember(mym);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetBaseInfo(String id, String pwd, String nickName, DateTime birthday, String city, bool finishedReg)
        {
            BiZ.Member.Member mym = BiZ.UserManager.MemberManager.GetMember(id);

            mym.MemberInfomation.NickName = nickName;
            mym.MemberInfomation.Birthday = birthday;
            mym.MemberInfomation.City = city;

            mym.Password = CBB.Security.MD5Helper.getMd5Hash(pwd);
            SaveMember(mym);

            if (finishedReg) SetRegFinished(mym);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetRegFinished(String id)
        {
            BiZ.Member.Member mym = BiZ.UserManager.MemberManager.GetMember(id);
            return SetRegFinished(mym);
        }
        private static CBB.ExceptionHelper.OperationResult SetRegFinished(BiZ.Member.Member mym)
        {
            mym.FinishedReg = true;
            SaveMember(mym);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static String MemberLogin(String email, String Password,String ipaddr)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                //"select top 1 Email,Password,AllowLogin from Members where Email="+email+",Password="+Password+",AllowLogin="+true
                MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
                IMongoQuery qc = Query.And(Query.EQ("Email", email),Query.EQ("Password",CBB.Security.MD5Helper.getMd5Hash(Password)),Query.EQ("AllowLogin",true));
                BiZ.Member.Member m  = mc.FindOne(qc);
                if (m != null)
                {
                    m.LastOperationTime = DateTime.Now;
                    m.LastLoginIP = ipaddr;
                    m.Status.LoginTimes++;

                    BiZ.UserManager.MemberManager.SaveMember(m);

                    return m._id.ToString();
                }
                else
                    return "";
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        public static Member.Member GetMember(String id)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            IMongoQuery qc = Query.EQ("_id", ObjectId.Parse(id));
            return mc.FindOne(qc);
        }
        public static Member.Member GetMemberBySinaWeiboID(String id)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            IMongoQuery qc = Query.EQ("ConnSinaWeiboID", id);
            return mc.FindOne(qc);
        }
        public static Member.Member GetMemberByQQID(String id)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            IMongoQuery qc = Query.EQ("ConnQQID", id);
            return mc.FindOne(qc);
        }
        public static Member.Member GetMemberByTXWeiboID(String id)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            IMongoQuery qc = Query.EQ("ConnTXWeiboID", id);
            return mc.FindOne(qc);
        }
        public static Member.Member GetMemberByDoubanID(String id)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            IMongoQuery qc = Query.EQ("ConnDoubanID", id);
            return mc.FindOne(qc);
        }
        public static Member.Member GetAuditingMember(String id)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            IMongoQuery qc = Query.And(Query.EQ("_id", ObjectId.Parse(id)), Query.EQ("FinishedReg", false));
            return mc.FindOne(qc);
        }
        public static IList<Member.Member> GetGoodMembers(string Province, int Sex, bool isRandom, int randomcount, int pagesize, int pageno)
        {
            QueryComplete qcsex = null;
            if (Sex!=-1) qcsex = Query.EQ("Sex",Sex);
            QueryComplete qccity = null;
            if (Province != "全部") qccity = Query.Matches("MemberInfomation.City","^"+Province);

            MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
            "Members",
            Query.And(Query.EQ("Au",2),qcsex,qccity),
            SortBy.Descending("CreatedTime"),
            pageno,
            pagesize);

            List<Member.Member> objs = new List<Member.Member>();
            objs.AddRange(mc);

            if (isRandom)
            {
                Random rnd = new Random();
                IEnumerable<Member.Member> rndlist = objs.OrderBy(x => rnd.Next()).Take(randomcount);
                List<Member.Member> randomobjs = rndlist.ToList();
                objs = randomobjs;
            }

            return objs;
        }
        public static IList<Member.Member> GetMembers(string Province, int Sex, Member.AgeType age, Member.SearchType type, bool isRandom,Member.HasPhotoType hasiconphoto, int randomcount, int pagesize, int pageno)
        {
            Member.MemberSearch ms = new Member.MemberSearch();
            ms.Province = Province;
            ms.Sex = Sex;
            ms.Type = type;
            ms.Age = age;
            ms.HasPhoto = hasiconphoto;

            MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
            "Members",
            ms.GetSearchQueryObj(),
            ms.GetOrderByObj(),
            pageno,
            pagesize);

            List<Member.Member> objs = new List<Member.Member>();
            objs.AddRange(mc);

            if (isRandom)
            {
                Random rnd = new Random(DateTime.Now.Millisecond);
                IEnumerable<Member.Member> rndlist = objs.OrderBy(x => rnd.Next()).Take(randomcount);
                List<Member.Member> randomobjs = rndlist.ToList();
                objs = randomobjs;
            }

            return objs;
        }
        public static IList<Member.Member> GetMembers(string Province, int Sex, Member.AgeType age, Member.SearchType type, Member.HasPhotoType hasiconphoto, int pagesize, int pageno)
        {
            return GetMembers(Province, Sex, age, type, false, hasiconphoto, -1, pagesize, pageno);
        }
        public static IList<Member.Member> SearchMembers(
            int BeenBand,
            int FinishedReg,
            int HasPhoto,
            string Province,
            string City,
            int Sex,
            string ID,
            int pagesize,
            int pageno
            )
        {
            QueryComplete qcband = null;
            if (BeenBand == 1)
                qcband = Query.EQ("AllowLogin", false);
            if (BeenBand == 0)
                qcband = Query.EQ("AllowLogin", true);
            QueryComplete finishedreg = null;
            if (FinishedReg == 1)
                finishedreg = Query.EQ("FinishedReg", true);
            if (FinishedReg == 0)
                finishedreg = Query.EQ("FinishedReg", false);
            QueryComplete hasphoto = null;
            if (HasPhoto == 1)
                hasphoto = Query.GT("Status.PhotoCount", 0);
            if (HasPhoto == 0)
                hasphoto = Query.EQ("Status.PhotoCount", 0);
            QueryComplete province = null;
            if (Province != "全部" & Province != "")
            {
                province = Query.Matches("MemberInfomation.City", "^" + Province);
                if (City != "全部" & City != "")
                    province = Query.EQ("MemberInfomation.City", Province + "|" + City);
            }
            QueryComplete sex = null;
            if (Sex!=0)
                sex = Query.EQ("Sex", Sex);
            QueryComplete id = null;
            if (ID!="")
                id = Query.EQ("_id", ObjectId.Parse(ID));

            QueryComplete searchobj = Query.And(qcband, finishedreg, hasphoto, province, sex, id);

            MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
                       "Members",
                       searchobj,
                       SortBy.Descending("LastOperationTime"),
                       pageno,
                       pagesize);

            List<Member.Member> objs = new List<Member.Member>();
            objs.AddRange(mc);

            return objs;
        }
        public static long SearchMembersCount(
            int BeenBand,
            int FinishedReg,
            int HasPhoto,
            string Province,
            string City,
            int Sex,
            string ID
            )
        {
            QueryComplete qcband = null;
            if (BeenBand == 1)
                qcband = Query.EQ("AllowLogin", false);
            if (BeenBand == 0)
                qcband = Query.EQ("AllowLogin", true);
            QueryComplete finishedreg = null;
            if (FinishedReg == 1)
                finishedreg = Query.EQ("FinishedReg", true);
            if (FinishedReg == 0)
                finishedreg = Query.EQ("FinishedReg", false);
            QueryComplete hasphoto = null;
            if (HasPhoto == 1)
                hasphoto = Query.GT("Status.PhotoCount", 0);
            if (HasPhoto == 0)
                hasphoto = Query.EQ("Status.PhotoCount", 0);
            QueryComplete province = null;
            if (Province != "全部" & Province!="")
            {
                province = Query.Matches("MemberInfomation.City", "^" + Province);
                if (City != "全部" & Province != "")
                    province = Query.EQ("MemberInfomation.City", Province + "|" + City);
            }
            QueryComplete sex = null;
            if (Sex != 0)
                sex = Query.EQ("Sex", Sex);
            QueryComplete id = null;
            if (ID != "")
                id = Query.EQ("_id", ObjectId.Parse(ID));

            QueryComplete searchobj = Query.And(qcband, finishedreg, hasphoto, province, sex, id);

            long count = MongoDBHelper.GetCount(
                       "Members",
                       searchobj);
            return count;
        }
        public static long GetMembersCount(string Province, int Sex, Member.AgeType age, Member.SearchType type,Member.HasPhotoType hasiconphoto)
        {
            Member.MemberSearch ms = new Member.MemberSearch();
            ms.Province = Province;
            ms.Sex = Sex;
            ms.Type = type;
            ms.Age = age;
            ms.HasPhoto = hasiconphoto;

            long mc = MongoDBHelper.GetCount(
            "Members",
            ms.GetSearchQueryObj());

            return mc;
        }
        public static CBB.ExceptionHelper.OperationResult SaveMember(Member.Member member)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Save(member);

            return new CBB.ExceptionHelper.OperationResult(true, "");
        }
        #endregion
        #region Member操作
        public static CBB.ExceptionHelper.OperationResult SetAllowLogin(String mid,bool allowflag)
        {
            String name = "已屏蔽";
            if (allowflag == true) name = "无名";
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(Query.EQ("_id", ObjectId.Parse(mid)),
                Update.Combine(Update.Set("AllowLogin", allowflag),Update.Set("MemberInfomation.NickName",name))
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetOnline(String mid)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(Query.EQ("_id", ObjectId.Parse(mid)),
                Update.Combine(Update.Set("LastOperationTime", DateTime.Now), Update.Set("Status.IsOnline", true))
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetNewPwd(String mid,String oldpwd,String newpwd)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(
                Query.And(
                    Query.EQ("_id", ObjectId.Parse(mid)),
                    Query.EQ("Password", CBB.Security.MD5Helper.getMd5Hash(oldpwd))
                    ),
                Update.Set("Password", CBB.Security.MD5Helper.getMd5Hash(newpwd))
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetNewPwd(String email,String newpwd)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(
                    Query.EQ("Email", email),
                    Update.Set("Password", CBB.Security.MD5Helper.getMd5Hash(newpwd))
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static bool OldPwdRight(String mid, String oldpwd)
        {
            // 预定成功返回值
            long count = 0;

            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection mc = md.GetCollection("Members");
                IMongoQuery qc = 
                    Query.And(
                        Query.EQ("_id", ObjectId.Parse(mid)),
                        Query.EQ("Password",CBB.Security.MD5Helper.getMd5Hash(oldpwd)));
                count = mc.Count(qc);
                if (count > 0) return true;
                return false;
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        public static CBB.ExceptionHelper.OperationResult SetContact(String mid, String qq, String msn, String tel, String other)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(
                    Query.EQ("_id", ObjectId.Parse(mid)),
                    Update.Combine(
                        Update.Set("MemberInfomation.QQ", qq),
                        Update.Set("MemberInfomation.MSN", msn),
                        Update.Set("MemberInfomation.Tel", tel),
                        Update.Set("MemberInfomation.Other", other)
                    )
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetAutoAddFavor(String mid, bool autoAddFavor)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(
                    Query.EQ("_id", ObjectId.Parse(mid)),
                    Update.Set("Settings.AutoAddOutCallingToMyFavorList", autoAddFavor)
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetMemberType(String mid, Member.MemberType type)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(
                    Query.EQ("_id", ObjectId.Parse(mid)),
                    Update.Set("MemberType", (int)type)
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        #endregion
        #region MemberStatus 操作
        public static bool IsMemberLastCallOutDateToday(String mid)
        {
            Member.Member mym = GetMember(mid);
            //如果最后呼出日期不是今天
            if (mym.Status.LastCallOutDate.Date != DateTime.Now.Date)
            {
                mym.Status.Last24HOutCallsCount = 0;
                mym.Status.LastCallOutDate = DateTime.Now.Date;
                SaveMember(mym);

                return false;
            }

            return true;
        }
        public static bool IsMemberLastInCallDateToday(String mid)
        {
            Member.Member mym = GetMember(mid);
            //如果最后呼出日期不是今天
            if (mym.Status.LastInCallDate.Date != DateTime.Now.Date)
            {
                mym.Status.Last24HInCallsCount = 0;
                mym.Status.LastInCallDate = DateTime.Now.Date;
                SaveMember(mym);

                return false;
            }

            return true;
        }
        private static CBB.ExceptionHelper.OperationResult ModifyStatusValue(String mid, String statusName, int value)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");

                mc.Update(
                    Query.EQ("_id", ObjectId.Parse(mid)),
                    Update.Inc(statusName, value),
                    UpdateFlags.Upsert,SafeMode.True
                    );

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        private static CBB.ExceptionHelper.OperationResult SetStatusValueZero(String mid, String statusName)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
                mc.Update(
                    Query.EQ("_id", ObjectId.Parse(mid)),
                    Update.Set(statusName, 0),SafeMode.True
                    );

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static CBB.ExceptionHelper.OperationResult ModifyBeenViewedTimes(String mid,StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.BeenViewedTimes", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyTotalMsgCount(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.TotalMsgCount", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyMemberFavoredMeCount(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.MemberFavoredMeCount", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyFavorMemberCount(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.FavorMemberCount", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyGlamourCount(String mid, StatusModifyType type, int value)
        {
            return ModifyStatusValue(mid, "Status.GlamourCount", value * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyTodayInCallsCount(String mid, StatusModifyType type)
        {
            IsMemberLastInCallDateToday(mid);
            return ModifyStatusValue(mid, "Status.Last24HInCallsCount", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyTodayOutCallsCount(String mid, StatusModifyType type)
        {
            IsMemberLastCallOutDateToday(mid);
            return ModifyStatusValue(mid, "Status.Last24HOutCallsCount", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyPhotoCount(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.PhotoCount", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyInterViewCount(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.InterViewCount", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifySkillCount(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.SkillCount", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyInterestCountCount(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.InterestCount", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyMarkedTimes(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.MarkedTimes", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyScoredTimes(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.ScoredTimes", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyUnReadBeenViewedTimes(String mid, StatusModifyType type, int value)
        {
            return ModifyStatusValue(mid, "Status.UnReadBeenViewedTimes", value * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult SetUnReadBeenViewedTimesZero(String mid)
        {
            return SetStatusValueZero(mid, "Status.UnReadBeenViewedTimes");
        }
        public static CBB.ExceptionHelper.OperationResult ModifyUnReadMsgCount(String mid, StatusModifyType type, int value)
        {
            return ModifyStatusValue(mid, "Status.UnReadMsgCount", value * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult SetUnReadMsgCountZero(String mid)
        {
            return SetStatusValueZero(mid, "Status.UnReadMsgCount");
        }
        public static CBB.ExceptionHelper.OperationResult ModifyUnReadBeenFavorCountCount(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.UnReadBeenFavorCount", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult SetUnReadBeenFavorCountZero(String mid)
        {
            return SetStatusValueZero(mid, "Status.UnReadBeenFavorCount");
        }
        public static CBB.ExceptionHelper.OperationResult ModifyUnReadMarkCount(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.UnReadMarkCount", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult SetUnReadMarkCountZero(String mid)
        {
            return SetStatusValueZero(mid, "Status.UnReadMarkCount");
        }
        public static CBB.ExceptionHelper.OperationResult ModifyUnReadScoreCount(String mid, StatusModifyType type, int value)
        {
            return ModifyStatusValue(mid, "Status.UnReadScoreCount", value * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult SetUnReadScoreCountZero(String mid)
        {
            return SetStatusValueZero(mid, "Status.UnReadScoreCount");
        }
        public static int GetMemberUnReadCount(String mid)
        {
            BiZ.Member.Member mym = BiZ.UserManager.MemberManager.GetMember(mid);
            if (mym == null) return 0;

            int count = 0;
            if (mym.Status != null)
                count = mym.Status.UnReadBeenFavorCount
                    + mym.Status.UnReadBeenViewedTimes
                    //+ mym.Status.UnReadMarkCount
                    + mym.Status.UnReadMsgCount;
                    //+ mym.Status.UnReadScoreCount;

            return count;
        }
        #endregion
        #region memberInfomation操作
        public static CBB.ExceptionHelper.OperationResult SetLatLng(String mid,bool hiddenmyloc,double lat, double lng)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(Query.EQ("_id", ObjectId.Parse(mid)),
                Update.Combine(Update.Set("Settings.HiddenMyLoc", hiddenmyloc), Update.Set("MemberInfomation.Lat", lat), Update.Set("MemberInfomation.Lng", lng))
                , UpdateFlags.Upsert);

            //增加动态
            BiZ.Member.Activity.ActivityController.AddActivity(
                mid,
                Member.Activity.ActivityType.UpdateLocation,
                BiZ.Member.Activity.ActivityController.GetActivityContent_SetLocation_Title(),
                "",
                true);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetIWant(String mid, String iwant)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(Query.EQ("_id", ObjectId.Parse(mid)),
                Update.Set("MemberInfomation.IWant", iwant)
                );

            //增加动态
            BiZ.Member.Activity.ActivityController.AddActivity(
                mid,
                Member.Activity.ActivityType.UpdateIWant,
                BiZ.Member.Activity.ActivityController.GetActivityContent_UpdateIWant_Title(),
                BiZ.Member.Activity.ActivityController.GetActivityContent_UpdateIWant(iwant),
                true);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetSoliloquize(String mid, String soliloquize)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(Query.EQ("_id", ObjectId.Parse(mid)),
                Update.Set("MemberInfomation.Soliloquize", soliloquize)
                );

            //增加动态
            BiZ.Member.Activity.ActivityController.AddActivity(
                mid,
                Member.Activity.ActivityType.UpdateSoliloquize,
                BiZ.Member.Activity.ActivityController.GetActivityContent_UpdateSoliloquize_Title(),
                BiZ.Member.Activity.ActivityController.GetActivityContent_UpdateSoliloquize(soliloquize),
                true);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static Member.Member GetOneSoliloquizedMember(int sex,String province)
        {
            MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
                        "Members",
                        Query.And( Query.EQ("Sex", sex), Query.Matches("MemberInfomation.City", "^" + province), Query.NE("MemberInfomation.Soliloquize", "")),
                        SortBy.Descending("LastOperationTime"),
                        1,
                        10);

            List<Member.Member> objs = new List<Member.Member>();
            objs.AddRange(mc);

            Random rnd = new Random();
            IEnumerable<Member.Member> rndlist = objs.OrderBy(x => rnd.Next()).Take(1);
            List<Member.Member> randomobjs = rndlist.ToList();
            if (randomobjs.Count > 0) return randomobjs[0];
            else
                return null;
        }
        #endregion
        #region MemberPhoto操作
        public static CBB.ExceptionHelper.OperationResult SetMemberIconPhoto(String mid, String photoid)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            Member.Member me = GetMember(mid);
            if (me.MemberPhoto == null) me.MemberPhoto = new Member.MemberPhoto();
            me.MemberPhoto.IconID = photoid;
            Photo.Photo p = Photo.PhotoManager.GetPhoto(photoid);
            me.MemberInfomation.IconPath = p.FileName;
            mc.Save(me);

            //增加动态
            BiZ.Member.Activity.ActivityController.AddActivity(
                me,
                Member.Activity.ActivityType.SetICON,
                BiZ.Member.Activity.ActivityController.GetActivityContent_SetICON_Title(),
                BiZ.Member.Activity.ActivityController.GetActivityContent_SetICON(me),
                true);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        #endregion
        #region memberRelations操作
        //用户标签
        public static CBB.ExceptionHelper.OperationResult AddMark(String mid,String content)
        {
            Member.Member mym = UserManager.MemberManager.GetMember(mid);
            Mark.Mark mknew = new Mark.Mark();
            mknew.Content = content;
            mknew.ContentCove = BiZ.Sys.Marks.SystemMarksFactory.GetSystemMarkContentCoveByContent(content);
            mknew.Times = 1;

            if (mym.MemberRelations == null)
            {
                mym.MemberRelations = new Member.MemberRelations();
                mym.MemberRelations.Marks = new List<Mark.Mark>();
                mym.MemberRelations.Marks.Add(mknew);
            }
            else
            {
                bool havecontent = false;
                foreach (Mark.Mark mk in mym.MemberRelations.Marks)
                {
                    if (mk.Content == content)
                    {
                        mk.Times++;
                        havecontent = true;
                    }
                }
                if (!havecontent)
                {
                    mym.MemberRelations.Marks.Add(mknew);
                }
            }
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Save(mym);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        //用户评分
        public static CBB.ExceptionHelper.OperationResult AddScore(String mid, int score)
        {
            Member.Member mym = UserManager.MemberManager.GetMember(mid);
            if (mym.Status == null)
            {
                mym.Status = new Member.MemberStatus();
                mym.Status.ScoreAvg = (float)score;
            }
            else
            {
                IList<Member.Relation.Scorer> ls = BiZ.Member.Relation.RelationProvider.GetScorers(mid, 0, 0);

                int totalscore = score;
                foreach (Member.Relation.Scorer sc in ls)
                {
                    totalscore += sc.Score;
                }
                mym.Status.ScoreAvg = (float)totalscore / (ls.Count + 1);
            }
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Save(mym);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static IList<Mark.Mark> GetMark(String mid)
        {
            Member.Member mym = UserManager.MemberManager.GetMember(mid);
            if (mym.MemberRelations == null) return new List<Mark.Mark>();
            if (mym.MemberRelations.Marks == null) return new List<Mark.Mark>();
            if (mym.MemberRelations.Marks.Count == 0) return new List<Mark.Mark>();

            return mym.MemberRelations.Marks;
        }

        #endregion
    }
}
