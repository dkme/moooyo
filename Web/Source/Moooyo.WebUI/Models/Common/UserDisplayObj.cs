using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models
{
    public class UserDisplayObj : MemberDisplayObj
    {
        #region 微博绑定信息
        public String BindedPlatforms;
        #endregion

        #region 邮箱验证信息
        public String Email;
        public bool EmailIsVaild;
        #endregion
    }
}