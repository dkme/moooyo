using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.BiZ.Core.DisplayObjs
{
    public class MemberDisplayObj
    {
        public String ID;
        public DateTime CreatedTime;
        public String TimeSpan;
        public String MemberTitle; // 用户封号
        public BiZ.Core.Comm.UniqueNumber.UniqueNumber UniqueNumber; //增强的唯一编号

        #region 基本信息
        public String Name;
        public String Email;
        public int Sex;
        public String SexStr;
        public int WantSex;
        public String WantSexStr;
        public String ICONPath;
        public String MinICON;
        public String BigImg;
        public String Want;
        public String City;
        public String Career;
        public String Age;
        public String Height;
        public String Star; //星座
        public double Lng;
        public double Lat;
        #endregion

        #region 基本状态信息
        public String OnlineStr;
        public String Hot;
        public int Points; //积分
        public int GlamourCount;
        public String PhotoCount;
        public String InterestCount;
        public long loginCount;
        public int PointsSchedule; //积分培育
        public bool AllowLogin; //是否允许登录
        #endregion

        public String Distance;

        #region 基本认证信息
        //头像认证
        public bool IsHaveIcon;
        //视频认证
        public bool IsRealPhotoIdentification;
        //身份证认证
        public bool IsRealPerson;
        public bool EmailIsVaild; //Email验证
        #endregion

        #region  用户徽章
        public IList<BiZ.Core.Member.MemberBadge> Badgelist;
        #endregion

        #region 会员级别
        public int MemberType;
        public String MemberLevel;
        #endregion

        #region 用户兴趣列表
        public IList<BiZ.Core.InterestCenter.Interest> InterestList;
        #endregion

        #region 设置信息
        public BiZ.Core.Fans.FansGroupName FansGroupName;
        public Boolean HiddenMyLoc;
        #endregion
    }
}