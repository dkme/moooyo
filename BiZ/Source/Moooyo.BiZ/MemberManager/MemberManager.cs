using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CBB.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Moooyo.BiZ.MemberManager
{
    /// <summary>
    /// 查询新用户的方式
    /// </summary>
    public enum NewMembersSelectType
    {
        New = 0,
        NewToInterest = 1
    }
    /// <summary>
    /// 用户管理类
    /// </summary>
    public class MemberManager
    {
        #region 用户查询
        public static long GetAllMemberCount()
        {
            try
            {
                return MongoDBHelper.GetCount(Member.Member.GetCollectionName(), null);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static IList<Member.Member> GetActivMember(DateTime lastoperationtime)
        {
            try
            {
                MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
               "Members",
               Query.And(Query.EQ("Au", 2),Query.GT("LastOperationTime",lastoperationtime)),
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
        public static Member.Member GetMember(String id)
        {
            if (id == null || id == "") return null;
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            IMongoQuery qc = Query.EQ("_id", ObjectId.Parse(id));
            return mc.FindOne(qc);
        }
        public static Member.Member GetMemberByEmail(String email)
        {
            //转换成小写再比对
            email = email.ToLower();
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            IMongoQuery qc = Query.EQ("Email", email);
            return mc.FindOne(qc);
        }
        public static Member.MemberInfomation GetMemberInfomation(String id)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            IMongoQuery qc = Query.EQ("_id", ObjectId.Parse(id));
            Member.Member member = mc.FindOne(qc);
            if (null != member && null != member.MemberInfomation)
            {
                return member.MemberInfomation;
            }
            else
                return null;
        }
        //按ID数组获取用户
        public static IList<Member.Member> GetMember(String[] ids)
        {
            try
            {
                List<ObjectId> iIds = new List<ObjectId>();
                List<Member.Member> objs = new List<Member.Member>();
                foreach (String id in ids)
                {
                    ObjectId objid;
                    bool parseFlag = ObjectId.TryParse(id, out objid);
                    if (parseFlag)
                        iIds.Add(objid);
                    else
                        return objs;
                }
                MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
                    Member.Member.GetCollectionName(),
                    Query.In("_id", new BsonArray(iIds.ToArray())),
                    null,
                    0,
                    0);

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
        //按ID数组获取用户
        public static IList<Member.Member> GetMember(String[] ids, int pageno, int pagesize)
        {
            try
            {
                List<ObjectId> iIds = new List<ObjectId>();
                List<Member.Member> objs = new List<Member.Member>();
                foreach (String id in ids)
                {
                    ObjectId objid;
                    bool parseFlag = ObjectId.TryParse(id, out objid);
                    if (parseFlag)
                        iIds.Add(objid);
                }
                MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
                    Member.Member.GetCollectionName(),
                    Query.And(
                    Query.In("_id", new BsonArray(iIds.ToArray())),
                    Query.Exists("MemberInfomation.IconPath", true),
                    Query.NE("MemberInfomation.IconPath", "")
                    ),
                    new SortByDocument("LastOperationTime", -1),
                    pageno,
                    pagesize);
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
        //按ID数组获取用户
        public static long GetMemberCount(String[] ids)
        {
            try
            {
                List<ObjectId> iIds = new List<ObjectId>();
                List<Member.Member> objs = new List<Member.Member>();
                foreach (String id in ids)
                {
                    ObjectId objid;
                    bool parseFlag = ObjectId.TryParse(id, out objid);
                    if (parseFlag)
                        iIds.Add(objid);
                }
                return MongoDBHelper.GetCount(Member.Member.GetCollectionName(),Query.In("_id", new BsonArray(iIds.ToArray())));
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public static long GetMemberCountToIcon(String[] ids)
        {
            try
            {
                List<ObjectId> iIds = new List<ObjectId>();
                List<Member.Member> objs = new List<Member.Member>();
                foreach (String id in ids)
                {
                    ObjectId objid;
                    bool parseFlag = ObjectId.TryParse(id, out objid);
                    if (parseFlag)
                        iIds.Add(objid);
                }
                return MongoDBHelper.GetCount(Member.Member.GetCollectionName(), Query.And(
                    Query.In("_id", new BsonArray(iIds.ToArray())),
                    Query.Exists("MemberInfomation.IconPath", true),
                    Query.NE("MemberInfomation.IconPath", "")
                    ));
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
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
            if (Sex != -1) qcsex = Query.EQ("Sex", Sex);
            QueryComplete qccity = null;
            if (Province != "全部") qccity = Query.Matches("MemberInfomation.City", "^" + Province);

            MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
            "Members",
            Query.And(Query.EQ("Au", 2), qcsex, qccity),
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
        public static IList<Member.Member> GetMembers(string Province, int Sex, Member.AgeType age, Member.SearchType type, bool isRandom, Member.HasPhotoType hasiconphoto, int randomcount, int pagesize, int pageno)
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
            if (Sex != 0)
                sex = Query.EQ("Sex", Sex);
            QueryComplete id = null;
            if (ID != "")
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
            if (Province != "全部" & Province != "")
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
        public static long GetMembersCount(string Province, int Sex, Member.AgeType age, Member.SearchType type, Member.HasPhotoType hasiconphoto)
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
        #endregion
        
        #region 注册操作
        /// <summary>
        /// 构造用户增强的唯一编号的值
        /// </summary>
        /// <param name="uniqueNumber">唯一编号对象</param>
        /// <param name="member">用户对象</param>
        /// <returns>操作状态</returns>
        public static Member.Member SetMemberUniqueNumber(
            BiZ.Comm.UniqueNumber.UniqueNumber uniqueNumber,
            Member.Member member)
        {
            if (member.UniqueNumber == null) 
                member.UniqueNumber = new Comm.UniqueNumber.UniqueNumber();
            member.UniqueNumber.DefaultId = uniqueNumber.DefaultId;
            member.UniqueNumber.ConvertedID = uniqueNumber.ConvertedID;
            member.UniqueNumber.DomainNameID = uniqueNumber.DomainNameID;
            member.UniqueNumber.IDType = uniqueNumber.IDType;
            member.UniqueNumber.CreatedTime = uniqueNumber.CreatedTime;

            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>(Member.Member.GetCollectionName());
            mc.Save(member);
            MongoCollection<BiZ.Comm.UniqueNumber.UniqueNumber> mcUn = md.GetCollection<BiZ.Comm.UniqueNumber.UniqueNumber>(BiZ.Comm.UniqueNumber.UniqueNumber.GetCollectionName());
            mcUn.Save(uniqueNumber);

            return member;
        }
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
                //保存用户编码
                BiZ.Comm.UniqueNumber.UniqueNumber uniqueNumber = BiZ.Comm.UniqueNumber.UniqueNumberProvider.ConvertMemberID(mym.ID, BiZ.Comm.UniqueNumber.IDType.MemberID);
                //构造用户增强的唯一编号的值
                mym = SetMemberUniqueNumber(uniqueNumber, mym);

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
                //保存用户编码
                BiZ.Comm.UniqueNumber.UniqueNumber uniqueNumber = BiZ.Comm.UniqueNumber.UniqueNumberProvider.ConvertMemberID(mym.ID, BiZ.Comm.UniqueNumber.IDType.MemberID);
                //构造用户增强的唯一编号的值
                SetMemberUniqueNumber(uniqueNumber, mym);

                return mym;
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        public static Member.Member GetNotActivationEmailMember(String email)
        {
            //转换成小写再比对
            email = email.ToLower();
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<BiZ.Member.Member> mc = md.GetCollection<BiZ.Member.Member>("Members");
            IMongoQuery qc = Query.And(Query.EQ("Email", email), Query.EQ("EmailIsVaild", false));
            BiZ.Member.Member member = mc.FindOne(qc);
            return member;
        }
        public static Member.Member GetPreMemberByEmail(String email)
        {
            //转换成小写再比对
            email = email.ToLower();
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<BiZ.Member.Member> mc = md.GetCollection<BiZ.Member.Member>("Members");
            IMongoQuery qc = Query.And(Query.EQ("Email", email), Query.EQ("FinishedReg", false));
            BiZ.Member.Member member = mc.FindOne(qc);
            return member;
        }
        public static Member.Member GetPreMemberByEmail(String email, String emailpwd)
        {
            //转换成小写再比对
            email = email.ToLower();
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<BiZ.Member.Member> mc = md.GetCollection<BiZ.Member.Member>("Members");
            IMongoQuery qc = Query.And(Query.EQ("Email", email), Query.EQ("Password", emailpwd),Query.EQ("FinishedReg",false));
            BiZ.Member.Member member = mc.FindOne(qc);
            return member;
        }
        public static Member.Member GetPreMemberByID(String id)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<BiZ.Member.Member> mc = md.GetCollection<BiZ.Member.Member>("Members");
            IMongoQuery qc = Query.And(Query.EQ("_id", ObjectId.Parse(id)),Query.EQ("FinishedReg",false));
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
        public static IList<Member.Member> GetNewUsersToMark(int sex, int pagesize, int pageno)
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
                Update.Combine(Update.Set("Au", auditresult), Update.Set("Sex", sex), Update.Set("MemberInfomation.Sex", sex))
                //Update.Combine(Update.Set("Au", auditresult), Update.Set("Sex", sex), Update.Set("MemberInfomation.Sex", sex), Update.Set("MemberInfomation.NickName", sex==1?"他":"她"))
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
            //转换成小写再比对
            email = email.ToLower();
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
            return GetIsEmailUnFinishedReg(email, true);
        }
        public static bool GetIsEmailUnFinishedReg(String email,bool finishedReg)
        {
            // 预定成功返回值
            long emailcount = 0;

            try
            {
                //转换成小写再比对
                email = email.ToLower();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection mc = md.GetCollection("Members");
                IMongoQuery qc = Query.And(Query.EQ("Email", email), Query.EQ("FinishedReg", finishedReg));
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
                mym.AllowLogin = false;

                mym.MemberPhoto.IconID = iconID;
                if (iconID != "")
                {
                    Photo.Photo myp = Photo.PhotoManager.GetPhoto(iconID);
                    mym.MemberInfomation.IconPath = myp.FileName;
                }

                mym.Settings.AutoAddOutCallingToMyFavorList = true;
                mym.Settings.HiddenMyPhoto = false;
                mym.Settings.StopMyAccount = false;

                //转换成小写再插入
                email = email.ToLower();
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
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(id);
            mym.Sex = sex;
            mym.MemberInfomation.Sex = sex;
            mym.Email = email;
            SaveMember(mym);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetBaseInfo(String id, String pwd, String nickName, DateTime birthday, String city, bool finishedReg)
        {
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(id);

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
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(id);
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
        /// <param name="email">用户邮箱</param>
        /// <param name="Password">用户密码</param>
        /// <param name="Password">用户IP地址 </param>
        /// <returns>用户编号</returns>
        public static String MemberLogin(String email, String Password,String ipaddr)
        {
            try
            {
                String memberId = PasswordMemberLogin(email, Password, ipaddr, false);
                return memberId;
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 已经加密密码用户登录
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <param name="Password">用户密码</param>
        /// <param name="Password">用户IP地址 </param>
        /// <returns>用户编号</returns>
        public static String MemberPasswordLogin(String email, String Password, String ipaddr)
        {
            try
            {
                String memberId = PasswordMemberLogin(email, Password, ipaddr, true);
                return memberId;
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 密码登陆
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <param name="Password">用户密码</param>
        /// <param name="ipaddr">用户IP地址</param>
        /// <param name="passwordEncryption">密码是否已经加密</param>
        /// <returns>用户编号</returns>
        public static String PasswordMemberLogin(String email, String Password, String ipaddr, Boolean passwordEncryption)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            //"select top 1 Email,Password,AllowLogin from Members where Email="+email+",Password="+Password+",AllowLogin="+true
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");

            IMongoQuery qc = null;

            //转换成小写再比对
            email = email.ToLower();

            //如果是加密密码
            if (!passwordEncryption)
                qc = Query.And(Query.EQ("Email", email), Query.EQ("Password", CBB.Security.MD5Helper.getMd5Hash(Password)));
            else
                qc = Query.And(Query.EQ("Email", email), Query.EQ("Password", Password));

            BiZ.Member.Member m = mc.FindOne(qc);
            if (m != null)
            {
                m.LastOperationTime = DateTime.Now;
                m.LastLoginIP = ipaddr;
                m.Status.LoginTimes++;
                ////更新兴趣粉丝中用户的最后登录时间
                //BiZ.InterestCenter.InterestFactory.UpdateFansLastOperationTime(m.ID, m.LastOperationTime);

                BiZ.MemberManager.MemberManager.SaveMember(m);

                return m._id.ToString();
            }
            else
                return "";
        }

        //获取用户登录次数
        public static long GetMemberLoginTimes(string memeberid) 
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
                IMongoQuery qc = Query.And(Query.EQ("_id", ObjectId.Parse(memeberid) ));
                BiZ.Member.Member m = mc.FindOne(qc);
                if (m != null)
                {
                    return m.Status.LoginTimes;
                }
                else
                    return 0;
            }
            catch (CBB.ExceptionHelper.OperationException err) 
            {
                throw err;
            }
        }
        
        public static CBB.ExceptionHelper.OperationResult SaveMember(Member.Member member)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Save(member);

            #region 用户相关信息过滤
            new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(member.MemberInfomation.NickName, Member.Member.GetCollectionName(), member.ID, "MemberInfomation.NickName", member.ID);

            new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(member.MemberInfomation.NickName, Member.Member.GetCollectionName(), member.ID, "MemberInfomation.NickName", member.ID);
            #endregion


            return new CBB.ExceptionHelper.OperationResult(true, "");
        }
        #endregion

        #region Member操作

        /// <summary>
        /// 微博账号设置自己的米柚账号
        /// </summary>
        /// <param name="mid">当前登陆的用户编号</param>
        /// <param name="userword">当前微博用户设置的米柚邮箱账号</param>
        /// <param name="password">当前微博用户设置的密码</param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult UpdateWeiBoUser(String mid, String userword, String password)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(
                Query.EQ("_id", ObjectId.Parse(mid)), 
                Update.Combine(
                Update.Set("Password", CBB.Security.MD5Helper.getMd5Hash(password)), 
                Update.Set("Email", userword),
                Update.Set("EmailIsVaild", false)));
            return new CBB.ExceptionHelper.OperationResult(true);
        }
        /// <summary>
        /// 视频认证是否通过
        /// </summary>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult CheckRealPhoto(List<ObjectId> useridlist,bool isreal) 
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(Query.In("_id",new BsonArray(useridlist.ToArray())),
                Update.Set("MemberPhoto.IsRealPhotoIdentification", isreal));
            return new CBB.ExceptionHelper.OperationResult(true);
        }
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
        /// <summary>
        /// 批量更新用户是否允许登录
        /// </summary>
        /// <param name="idlist">id集合</param>
        /// <param name="allowlogin">是否允许登录</param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult UpdateMemberAllowLogin(List<ObjectId> idlist,bool allowlogin )
        {
            try{
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
                QueryConditionList qcl = Query.In("_id", new BsonArray(idlist.ToArray()));
                mc.Update(qcl, Update.Set("AllowLogin", allowlogin), UpdateFlags.Multi);
                return new CBB.ExceptionHelper.OperationResult(true);
            }catch(Exception es){
                
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
            
        }
        /// <summary>
        /// 依据允许登录状态批量获取用户
        /// </summary>
        /// <param name="type">是否允许登录</param>
        /// <param name="pageno"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static List<Member.Member> GetMemberByAllowLogin(bool type, int usersel, int stype, string scontent, int pageno, int pagesize)
        {
            try
            {
                string emailstr = string.Empty;
                switch (usersel)
                {
                    case 1: emailstr = "^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$"; break;
                    case 2: emailstr = "TencentWeibo"; break;
                    case 3: emailstr = "SinaWeibo"; break;
                    case 4: emailstr = "RenRen"; break;
                    case 5: emailstr = "Douban"; break;
                }
                List<Member.Member> objs = new List<Member.Member>();
                if (scontent != "")
                {
                    switch (stype)
                    {
                        case 1:
                            MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
                                Member.Member.GetCollectionName(), 
                                Query.Matches("Email", scontent), 
                                SortBy.Descending("CreatedTime"), 
                                pageno, 
                                pagesize
                                );
                            objs.AddRange(mc);
                            foreach (var obj in objs) 
                            { 
                                obj.ToRegInviterMember = GetRegInviterMember(obj.ID); 
                            }
                            break;
                        case 2:
                            MongoCursor<Comm.UniqueNumber.UniqueNumber> mcs = MongoDBHelper.GetCursor<Comm.UniqueNumber.UniqueNumber>(
                                Comm.UniqueNumber.UniqueNumber.GetCollectionName(), 
                                Query.EQ("ConvertedID", 
                                long.Parse(scontent)), 
                                SortBy.Descending("CreatedTime"), 
                                0, 
                                0
                                );
                            List<Comm.UniqueNumber.UniqueNumber> unlist = new List<Comm.UniqueNumber.UniqueNumber>();
                            unlist.AddRange(mcs);

                            List<ObjectId> ids = new List<ObjectId>();
                            foreach (var tranobj in unlist) { ids.Add(ObjectId.Parse(tranobj.DefaultId)); }

                            MongoCursor<Member.Member> mcstypetwo = MongoDBHelper.GetCursor<Member.Member>(
                                Member.Member.GetCollectionName(), 
                                Query.In("_id", new BsonArray(ids.ToArray())), 
                                SortBy.Descending("CreatedTime"), 
                                0, 
                                0
                                );
                            objs.AddRange(mcstypetwo);
                            foreach (var obj in objs) 
                            { 
                                obj.ToRegInviterMember = GetRegInviterMember(obj.ID); 
                            }
                            break;
                    }
                }
                else
                {
                    QueryComplete qc = null;

                    Regex regex;
                    regex = new Regex(emailstr, RegexOptions.IgnoreCase);
                    if(usersel != 0)
                        qc = Query.And(
                            Query.EQ("AllowLogin", type),
                            Query.Matches("Email", regex)
                            );
                    else
                        qc = Query.EQ("AllowLogin", type);

                    MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
                    Member.Member.GetCollectionName(),
                    qc,
                    SortBy.Descending("CreatedTime"), pageno, pagesize);
                    objs.AddRange(mc);
                    foreach (var obj in objs) { obj.ToRegInviterMember = GetRegInviterMember(obj.ID); }
                }
                return objs;
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }
        /// <summary>
        /// 根据用户id查询邀请的用户集合
        /// </summary>
        /// <param name="memberID">用户id</param>
        /// <returns></returns>
        public static IList<Member.Member> GetRegInviterMember(String memberID)
        {
            try
            {
                MongoCursor<Member.Relation.RegInviter> mc = MongoDBHelper.GetCursor<Member.Relation.RegInviter>(
                "RegisterInviter",
                Query.EQ("FromMember", memberID),
                SortBy.Descending("CreatedTime"), 0, 0);
                List<Member.Relation.RegInviter> objs = new List<Member.Relation.RegInviter>();
                objs.AddRange(mc);
                IList<Member.Member> memberlist = new List<Member.Member>();
                string[] ids = new string[objs.Count];
                for (int i = 0; i < objs.Count; i++) { ids[i] = objs[i].ToMember; }
                memberlist = GetMember(ids);
                return memberlist;
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }
        /// <summary>
        /// 依据是否允许登录状态获取总数
        /// </summary>
        /// <param name="type">是否允许登录</param>
        /// <returns></returns>
        public static long GetAllowLoginMemberCount(bool type, int usersel, int stype, string scontent)
        {
            long count = 0;
            try
            {
                string emailstr = string.Empty;
                switch (usersel)
                {
                    case 1: emailstr = "^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$"; break;
                    case 2: emailstr = "TencentWeibo"; break;
                    case 3: emailstr = "SinaWeibo"; break;
                    case 4: emailstr = "RenRen"; break;
                }
                List<Member.Member> objs = new List<Member.Member>();
                if (scontent != "")
                {
                    switch (stype)
                    {
                        case 1:
                            MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(Member.Member.GetCollectionName(), Query.Matches("Email", scontent), SortBy.Descending("CreatedTime"), 0, 0);
                            objs.AddRange(mc);
                            count = objs.Count;
                            break;
                        case 2:
                            MongoCursor<Comm.UniqueNumber.UniqueNumber> mcs = MongoDBHelper.GetCursor<Comm.UniqueNumber.UniqueNumber>(Comm.UniqueNumber.UniqueNumber.GetCollectionName(), Query.EQ("ConvertedID", long.Parse(scontent)), SortBy.Descending("CreatedTime"), 0, 0);
                            List<Comm.UniqueNumber.UniqueNumber> unlist = new List<Comm.UniqueNumber.UniqueNumber>();
                            unlist.AddRange(mcs);

                            List<ObjectId> ids = new List<ObjectId>();
                            foreach (var tranobj in unlist) { ids.Add(ObjectId.Parse(tranobj.DefaultId)); }

                            MongoCursor<Member.Member> mcstypetwo = MongoDBHelper.GetCursor<Member.Member>(Member.Member.GetCollectionName(), Query.In("_id", new BsonArray(ids.ToArray())), SortBy.Descending("CreatedTime"), 0, 0);
                            objs.AddRange(mcstypetwo);
                            count = objs.Count;
                            break;
                    }
                }
                else
                {
                    Regex regex;
                    regex = new Regex(emailstr, RegexOptions.IgnoreCase);
                    MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
                    Member.Member.GetCollectionName(),
                    Query.And(
                    Query.EQ("AllowLogin", type),
                    Query.Matches("Email", regex)
                    ),
                    SortBy.Descending("CreatedTime"), 0, 0);
                    objs.AddRange(mc);
                    count = objs.Count;
                }
                return count;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
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
            //转换成小写再比对
            email = email.ToLower();
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
        public static CBB.ExceptionHelper.OperationResult SetEmail(String mid, String oldpwd, String newemail)
        {
            //转换成小写再比对或更新
            newemail = newemail.ToLower();

            //判断email是否已占用
            Member.Member mym = GetMemberByEmail(newemail);
            if (mym != null) return new CBB.ExceptionHelper.OperationResult(false,"新Email已经被使用");
            mym = GetMember(mid);
            if (mym.Password != CBB.Security.MD5Helper.getMd5Hash(oldpwd)) return new CBB.ExceptionHelper.OperationResult(false,"用户密码不正确");

            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(
                Query.And(
                    Query.EQ("_id", ObjectId.Parse(mid)),
                    Query.EQ("Password", CBB.Security.MD5Helper.getMd5Hash(oldpwd))
                    ),
                    Update.Combine(Update.Set("Email", newemail),Update.Set("EmailIsVaild",false))
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetEmail(String mid, String newemail)
        {
            //转换成小写再比对或更新
            newemail = newemail.ToLower();

            //判断email是否已占用
            Member.Member mym = GetMemberByEmail(newemail);
            if (mym != null) return new CBB.ExceptionHelper.OperationResult(false, "新Email已经被使用");
            mym = GetMember(mid);

            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(
                    Query.EQ("_id", ObjectId.Parse(mid)),
                    Update.Combine(Update.Set("Email", newemail), Update.Set("EmailIsVaild", false))
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetEmailPasswd(String mid, String newemail, string newPasswd)
        {
            //转换成小写再比对或更新
            newemail = newemail.ToLower();

            //判断email是否已占用
            Member.Member mym = GetMemberByEmail(newemail);
            if (mym != null) return new CBB.ExceptionHelper.OperationResult(false, "新Email已经被使用");
            mym = GetMember(mid);

            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(
                    Query.EQ("_id", ObjectId.Parse(mid)),
                    Update.Combine(
                        Update.Set("Email", newemail), 
                        Update.Set("EmailIsVaild", false),
                        Update.Set("Password", CBB.Security.MD5Helper.getMd5Hash(newPasswd))
                    )
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetPrivacy(
            String memberId, bool autoAddOutCallingToMyFavorList, bool onlySeniorMemberCanTalkSaiHiMe, bool onlyVIPMemberCanTalkSaiHiMe)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(
                    Query.EQ("_id", ObjectId.Parse(memberId)),
                    Update.Combine(
                        Update.Set("Settings.AutoAddOutCallingToMyFavorList", autoAddOutCallingToMyFavorList),
                        Update.Set("Settings.OnlySeniorMemberCanTalkSaiHiMe", onlySeniorMemberCanTalkSaiHiMe),
                        Update.Set("Settings.OnlyVIPMemberCanTalkSaiHiMe", onlyVIPMemberCanTalkSaiHiMe)
                    )
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult AccessSet(String memberId, bool stopMyAccount)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(
                    Query.EQ("_id", ObjectId.Parse(memberId)),
                    Update.Set("Settings.StopMyAccount", stopMyAccount)
                );

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        //更新粉丝团名称
        public static CBB.ExceptionHelper.OperationResult UpdateFansGroupName(String memberID, String name, String firstName, String second, String theThird)
        {
            try
            {
                Member.Member memObj = GetMember(memberID);
                if (memObj == null) return new CBB.ExceptionHelper.OperationResult(false, "未找到对象");
                if (memObj.Settings.FansGroupName == null) memObj.Settings.FansGroupName = new Member.FansGroupName();
                memObj.Settings.FansGroupName.Name = name;
                memObj.Settings.FansGroupName.FirstName = firstName;
                memObj.Settings.FansGroupName.Second = second;
                memObj.Settings.FansGroupName.TheThird = theThird;

                MongoDatabase mgDd = MongoDBHelper.MongoDB;
                MongoCollection<Member.Member> mgClet = mgDd.GetCollection<Member.Member>(Member.Member.GetCollectionName());
                mgClet.Save(memObj);

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
        //获取粉丝团名称
        public static Member.FansGroupName GetFansGroupName(String memberID)
        {
            try
            {
                Member.Member memObj = GetMember(memberID);

                return memObj.Settings.FansGroupName;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
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
        /// <summary>
        /// 设置粉丝团名称
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newpwd"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult SetMemberFansGroupName(String mid, String fansGroupName)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            mc.Update(
                    Query.EQ("_id", ObjectId.Parse(mid)),
                    Update.Set("Setting.FansGroupName", fansGroupName)
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

        /// <summary>
        /// 添加用户徽章
        /// </summary>
        /// <param name="memberid">用户id</param>
        /// <param name="BadegType">徽章类型</param>
        /// <param name="BadegStatus">状态</param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult AddBadgeList(String memberid, Member.BadegType BadegType, int BadegStatus)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");

                Member.Member member = mc.FindOne(Query.EQ("_id",ObjectId.Parse(memberid)));
                Member.MemberStatus ms = member.Status;
                bool is_have = true; //可以添加
                if (ms.MemberBadge == null) { 
                    ms.MemberBadge = new List<Member.MemberBadge>(); 
                }
                else {
                    foreach (Member.MemberBadge mb in ms.MemberBadge) 
                    {
                        if (mb.BadegType == Convert.ToInt32(BadegType)) 
                        {
                            is_have = false; //如果用户已经拥有徽章列表，判断是否拥有即将添加的徽章
                        }
                    }
                }
                if (is_have)
                {
                    Member.MemberBadge memberBadge = new Member.MemberBadge();
                    memberBadge.BadegStatus = BadegStatus;
                    memberBadge.BadegType = Convert.ToInt32(BadegType);
                    memberBadge.JionTime = DateTime.Now;
                    ms.MemberBadge.Add(memberBadge);
                    mc.Save(member);
                    return new CBB.ExceptionHelper.OperationResult(true);
                }
                else 
                {
                    return new CBB.ExceptionHelper.OperationResult(true);
                }
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 删除用户徽章
        /// </summary>
        /// <param name="memberid">用户id</param>
        /// <param name="badegtype">徽章类型</param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult DelBadge(String memberid, Member.BadegType badegtype)
        {
            try
            {
                Member.Member member = GetMember(memberid);
                Member.MemberStatus ms = member.Status;
                int d = -1;
                for (int i = 0; i < ms.MemberBadge.Count; i++)
                {
                   
                    if (ms.MemberBadge[i].BadegType == Convert.ToInt32(badegtype))
                    {
                        d = i;
                    }
                }
                if (d != -1)
                {
                    ms.MemberBadge.RemoveAt(d);
                }
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>(Member.Member.GetCollectionName());
                mc.Save(member);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (CBB.ExceptionHelper.OperationException err)
            {
                throw err;
            }
        }

        // 结束

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
        /// <summary>
        /// 设置状态值
        /// </summary>
        /// <param name="mid">用户编号</param>
        /// <param name="statusName">状态名</param>
        /// <param name="count">数量</param>
        /// <returns>操作状态</returns>
        private static CBB.ExceptionHelper.OperationResult SetStatusValue(String mid, String statusName, int count)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
                mc.Update(
                    Query.EQ("_id", ObjectId.Parse(mid)),
                    Update.Set(statusName, count), 
                    UpdateFlags.Upsert,
                    SafeMode.True
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
        public static CBB.ExceptionHelper.OperationResult ModifyPoints(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.Points", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyPoints(String mid, StatusModifyType type, int value)
        {
            return ModifyStatusValue(mid, "Status.Points", value * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult ModifyPointsSchedule(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.PointsSchedule", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult SetPointsScheduleZero(String mid)
        {
            return SetStatusValueZero(mid, "Status.PointsSchedule");
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
        public static CBB.ExceptionHelper.OperationResult ModifyInterestCountCount(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.InterestCount", 1 * (int)type);
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
        public static CBB.ExceptionHelper.OperationResult ModifyUnReadMsgCount(String mid, int unreadCount)
        {
            return SetStatusValue(mid, "Status.UnReadMsgCount", unreadCount);
        }
        public static CBB.ExceptionHelper.OperationResult SetUnReadMsgCountZero(String mid)
        {
            return SetStatusValueZero(mid, "Status.UnReadMsgCount");
        }
        public static CBB.ExceptionHelper.OperationResult ModifyUnReadSystemMsgCount(String mid, StatusModifyType type, int value)
        {
            return ModifyStatusValue(mid, "Status.UnReadSystemMsgCount", value * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult SetUnReadSystemMsgCountZero(String mid)
        {
            return SetStatusValueZero(mid, "Status.UnReadSystemMsgCount");
        }
        public static CBB.ExceptionHelper.OperationResult ModifyUnReadActivitysAboutMeCount(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.UnReadActivitysAboutMeCount", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult SetUnReadActivitysAboutMeCountZero(String mid)
        {
            return SetStatusValueZero(mid, "Status.UnReadActivitysAboutMeCount");
        }
        public static CBB.ExceptionHelper.OperationResult ModifyUnReadBeenFavorCountCount(String mid, StatusModifyType type)
        {
            return ModifyStatusValue(mid, "Status.UnReadBeenFavorCount", 1 * (int)type);
        }
        public static CBB.ExceptionHelper.OperationResult SetUnReadBeenFavorCountZero(String mid)
        {
            return SetStatusValueZero(mid, "Status.UnReadBeenFavorCount");
        }
        public static int GetMemberUnReadCount(String mid)
        {
            BiZ.Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(mid);
            if (mym == null) return 0;

            int count = 0;
            if (mym.Status != null)
                count = mym.Status.UnReadActivitysAboutMeCount
                    + mym.Status.UnReadMsgCount
                    + mym.Status.UnReadSystemMsgCount;
                    //+ mym.Status.UnReadScoreCount;
                    //+ mym.Status.UnReadBeenFavorCount
                    //+ mym.Status.UnReadBeenViewedTimes;
                    //+ mym.Status.UnReadMarkCount

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
            //审核关键字
            new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(iwant, Member.Member.GetCollectionName(), mid, "MemberInfomation.IWant", mid);
            //增加动态
            BiZ.Member.Activity.ActivityController.AddActivity(
                mid,
                Member.Activity.ActivityType.UpdateIWant,
                BiZ.Member.Activity.ActivityController.GetActivityContent_UpdateIWant_Title(),
                BiZ.Member.Activity.ActivityController.GetActivityContent_UpdateIWant(iwant),
                true);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        //public static CBB.ExceptionHelper.OperationResult SetSoliloquize(String mid, String soliloquize)
        //{
        //    MongoDatabase md = MongoDBHelper.MongoDB;
        //    MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
        //    mc.Update(Query.EQ("_id", ObjectId.Parse(mid)),
        //        Update.Set("MemberInfomation.Soliloquize", soliloquize)
        //        );

        //    //增加动态
        //    BiZ.Member.Activity.ActivityController.AddActivity(
        //        mid,
        //        Member.Activity.ActivityType.UpdateSoliloquize,
        //        BiZ.Member.Activity.ActivityController.GetActivityContent_UpdateSoliloquize_Title(),
        //        BiZ.Member.Activity.ActivityController.GetActivityContent_UpdateSoliloquize(soliloquize),
        //        true);

        //    return new CBB.ExceptionHelper.OperationResult(true);
        //}
        //public static Member.Member GetOneSoliloquizedMember(int sex,String province)
        //{
        //    MongoCursor<Member.Member> mc = MongoDBHelper.GetCursor<Member.Member>(
        //                "Members",
        //                Query.And( Query.EQ("Sex", sex), Query.Matches("MemberInfomation.City", "^" + province), Query.NE("MemberInfomation.Soliloquize", "")),
        //                SortBy.Descending("LastOperationTime"),
        //                1,
        //                10);

        //    List<Member.Member> objs = new List<Member.Member>();
        //    objs.AddRange(mc);

        //    Random rnd = new Random();
        //    IEnumerable<Member.Member> rndlist = objs.OrderBy(x => rnd.Next()).Take(1);
        //    List<Member.Member> randomobjs = rndlist.ToList();
        //    if (randomobjs.Count > 0) return randomobjs[0];
        //    else
        //        return null;
        //}
        #endregion

        #region MemberPhoto操作
        public static CBB.ExceptionHelper.OperationResult SetMemberIconPhotoAuditNotPass(String mid, String oldphotofilename, String FileName)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            Member.Member me = GetMember(mid);
            if (me.MemberPhoto == null) me.MemberPhoto = new Member.MemberPhoto();
            if (me.MemberInfomation.IconPath != oldphotofilename) return new CBB.ExceptionHelper.OperationResult(false, "头像已改变");
            me.MemberInfomation.IconPath = FileName;
            mc.Save(me);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetMemberIconPhoto(String mid, String photoid, String FileName)
        {
            MongoDatabase md = MongoDBHelper.MongoDB;
            MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
            Member.Member me = GetMember(mid);
            if (me.MemberPhoto == null) me.MemberPhoto = new Member.MemberPhoto();
            me.MemberPhoto.IconID = photoid;
            me.MemberInfomation.IconPath = FileName;
            mc.Save(me);

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        public static CBB.ExceptionHelper.OperationResult SetMemberIconPhoto(String mid, String photoid)
        {
            Photo.Photo p = Photo.PhotoManager.GetPhoto(photoid);
            string filename = "";
            if (p != null) filename = p.FileName;
            return SetMemberIconPhoto(mid, photoid, filename);
        }
        /// <summary>
        /// 如果视频认证为通过则改为不通过
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns>操作状态</returns>
        public static CBB.ExceptionHelper.OperationResult SetPhotoIdentNotPass(String userid)
        {
            //如果视频认证为通过则改为不通过
            BiZ.Member.Member member = BiZ.MemberManager.MemberManager.GetMember(userid);
            if (member.MemberPhoto.IsRealPhotoIdentification)
            {
                member.MemberPhoto.IsRealPhotoIdentification = false;
                List<ObjectId> obj = new List<ObjectId>();
                obj.Add(ObjectId.Parse(userid));
                //修改认证为不通过
                BiZ.MemberManager.MemberManager.CheckRealPhoto(obj, false);
                //删除以前的认证照片（如果存在）
                Moooyo.BiZ.Sys.PhotoCheck.PhotoCheckStatusFactory.RemoveCheckPhotosByusrid(userid);
                //发送站内信
                BiZ.Member.Activity.ActivityController.SystemMsgToMember(userid, CBB.ConfigurationHelper.AppSettingHelper.GetConfig("Videocertificationagain"));
            }

            return new CBB.ExceptionHelper.OperationResult(true);
        }
        #endregion

        #region memberRelations操作
        ////用户标签
        //public static CBB.ExceptionHelper.OperationResult AddMark(String mid,String content)
        //{
        //    Member.Member mym = MemberManager.GetMember(mid);
        //    Mark.Mark mknew = new Mark.Mark();
        //    mknew.Content = content;
        //    mknew.ContentCove = BiZ.Sys.Marks.SystemMarksFactory.GetSystemMarkContentCoveByContent(content);
        //    mknew.Times = 1;

        //    if (mym.MemberRelations == null)
        //    {
        //        mym.MemberRelations = new Member.MemberRelations();
        //        mym.MemberRelations.Marks = new List<Mark.Mark>();
        //        mym.MemberRelations.Marks.Add(mknew);
        //    }
        //    else
        //    {
        //        bool havecontent = false;
        //        foreach (Mark.Mark mk in mym.MemberRelations.Marks)
        //        {
        //            if (mk.Content == content)
        //            {
        //                mk.Times++;
        //                havecontent = true;
        //            }
        //        }
        //        if (!havecontent)
        //        {
        //            mym.MemberRelations.Marks.Add(mknew);
        //        }
        //    }
        //    MongoDatabase md = MongoDBHelper.MongoDB;
        //    MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
        //    mc.Save(mym);

        //    return new CBB.ExceptionHelper.OperationResult(true);
        //}
        ////用户评分
        //public static CBB.ExceptionHelper.OperationResult AddScore(String mid, int score)
        //{
        //    Member.Member mym = MemberManager.GetMember(mid);
        //    if (mym.Status == null)
        //    {
        //        mym.Status = new Member.MemberStatus();
        //        mym.Status.ScoreAvg = (float)score;
        //    }
        //    else
        //    {
        //        IList<Member.Relation.Scorer> ls = BiZ.Member.Relation.RelationProvider.GetScorers(mid, 0, 0);

        //        int totalscore = score;
        //        foreach (Member.Relation.Scorer sc in ls)
        //        {
        //            totalscore += sc.Score;
        //        }
        //        mym.Status.ScoreAvg = (float)totalscore / (ls.Count + 1);
        //    }
        //    MongoDatabase md = MongoDBHelper.MongoDB;
        //    MongoCollection<Member.Member> mc = md.GetCollection<Member.Member>("Members");
        //    mc.Save(mym);

        //    return new CBB.ExceptionHelper.OperationResult(true);
        //}
        //public static IList<Mark.Mark> GetMark(String mid)
        //{
        //    Member.Member mym = MemberManager.GetMember(mid);
        //    if (mym.MemberRelations == null) return new List<Mark.Mark>();
        //    if (mym.MemberRelations.Marks == null) return new List<Mark.Mark>();
        //    if (mym.MemberRelations.Marks.Count == 0) return new List<Mark.Mark>();

        //    return mym.MemberRelations.Marks;
        //}

        #endregion

        #region MemberToNew操作
        /// <summary>
        /// 根据用户编号查询新用户
        /// </summary>
        /// <param name="MemberID">用户编号</param>
        /// <returns></returns>
        public static Member.MemberToNew getMemberToNew(String MemberID)
        {
            try
            {
                Member.MemberToNew obj = new Member.MemberToNew();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Member.MemberToNew> mc = md.GetCollection<Member.MemberToNew>(Member.MemberToNew.GetCollectionName());
                obj = mc.FindOne(Query.EQ("MemberID", MemberID));
                return obj;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        /// <summary>
        /// 根据指定条件查询新用户
        /// </summary>
        /// <param name="type">查询类型</param>
        /// <param name="ids">用户编号集合</param>
        /// <param name="createdTime">创建时间范围</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageno">当前页数</param>
        /// <returns></returns>
        public static List<Member.MemberToNew> getMemberToNews(NewMembersSelectType type, String[] ids, String createdTime, int pagesize, int pageno)
        {
            List<Member.MemberToNew> memberList = new List<Member.MemberToNew>();
            if (type == NewMembersSelectType.New)
            {
                QueryComplete qc = null;
                if (createdTime != null && createdTime != "")
                {
                    qc = Query.And(Query.GT("CreatedTime", new BsonDateTime(DateTime.Parse(createdTime))));
                }
                else
                {
                    qc = null;
                }
                MongoCursor<Member.MemberToNew> mc = MongoDBHelper.GetCursor<Member.MemberToNew>(Member.MemberToNew.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1), pageno, pagesize);
                memberList.AddRange(mc);
            }
            if (type == NewMembersSelectType.NewToInterest)
            {
                List<ObjectId> iIds = new List<ObjectId>();
                foreach (String id in ids)
                {
                    ObjectId objid;
                    bool parseFlag = ObjectId.TryParse(id, out objid);
                    if (parseFlag)
                        iIds.Add(objid);
                }
                QueryComplete qc = null;
                if (createdTime != null && createdTime != "")
                {
                    qc = Query.And(Query.In("MemberID", new BsonArray(iIds.ToArray())), Query.GT("CreatedTime", new BsonDateTime(DateTime.Parse(createdTime))));
                }
                else
                {
                    qc = Query.And(Query.In("MemberID", new BsonArray(iIds.ToArray())));
                }
                MongoCursor<Member.MemberToNew> mc = MongoDBHelper.GetCursor<Member.MemberToNew>(Member.MemberToNew.GetCollectionName(), qc, new SortByDocument("CreatedTime", -1), pageno, pagesize);
                memberList.AddRange(mc);
            }
            return memberList;
        }
        /// <summary>
        /// 删除新用户记录
        /// </summary>
        /// <param name="MemberID">用户id</param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult RemoveMemberToNew(String MemberID)
        {
            try
            {
                Member.MemberToNew obj = new Member.MemberToNew();
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Member.MemberToNew> mc = md.GetCollection<Member.MemberToNew>(Member.MemberToNew.GetCollectionName());
                mc.Remove(Query.EQ("MemberID", MemberID));
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
        #endregion
    }
}
