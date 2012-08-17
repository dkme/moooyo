using System;
using System.Collections.Generic;
using System.Linq;

namespace Moooyo.App.Core.BiZ.DisplayObjs
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