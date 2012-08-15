using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models
{
    public class MemberFullDisplayObj : MemberDisplayObj
    {
        #region 资料信息
        public DateTime Birthday;
        public String EducationalBackground;
        public String PropertySituation; //置业状况
        public String PersonalIntroduction; //个人介绍
        public BiZ.Member.MemberSkin.MemberSkin MemberSkin; //用户皮肤
        //public string PersonalityPicture; //用户皮肤个性图片
        //public string PersonalityBackgroundPicture; //用户皮肤个性背景图片
        #endregion

        #region 状态表
        public int BeenViewedTimes;
        public int FavorMemberCount;
        public int MemberFavoredMeCount;
        public int TotalMsgCount;
        public int UnReadBeenViewedTimes;
        public int UnReadMsgCount;
        public int UnReadBeenFavorCount;
        public int UnReadSystemMsgCount;
        public int UnReadActivitysAboutMeCount;
        #endregion

        #region 更多计数器
        public String InterViewCount;
        public int Last24HOutCallsCount;
        #endregion
    }
}