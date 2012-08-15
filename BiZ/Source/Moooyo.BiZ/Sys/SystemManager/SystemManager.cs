using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Sys.SystemManager
{
    public class SystemManager
    {
        /// <summary>
        /// ID 
        /// </summary>
        public String ID
        {
            get { if (_id != null) return _id.ToString(); else return ""; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;
        /// <summary>
        /// 名称
        /// </summary>
        private String name;
        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        /// <summary>
        /// 等级
        /// </summary>
        private int level;
        public int Level
        {
            get { return this.level; }
            set { this.level = value; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        private String password;
        public String Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        /// <summary>
        /// 是否允许登陆
        /// </summary>
        public Boolean AllowLogin
        {
            get { return this.allowLogin; }
            set { this.allowLogin = value; }
        }
        private Boolean allowLogin;
    }
}
