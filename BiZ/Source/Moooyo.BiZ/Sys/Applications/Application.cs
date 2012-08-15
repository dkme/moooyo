using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Sys.Applications
{
    public class Application
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
        /// 应用图片路径
        /// </summary>
        private String ImagePath;

        public String ImagePath1
        {
            get { return ImagePath; }
            set { ImagePath = value; }
        }
        /// <summary>
        /// 应用描述
        /// </summary>
        private String Description;

        public String Description1
        {
            get { return Description; }
            set { Description = value; }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        private String url;

        public String Url
        {
            get { return url; }
            set { url = value; }
        }
    }
}
