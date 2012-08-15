using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Sys.Wants
{
    public class SystemWants
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
        /// 
        /// </summary>
        public WantType type
        {
            get { return this.t; }
            set { this.t = value; }
        }
        private WantType t;
        /// <summary>
        /// 我想
        /// </summary>
        public String IWantStr
        {
            get { return this.iwantstr; }
            set { this.iwantstr = value; }
        }
        private String iwantstr;
        /// <summary>
        /// 
        /// </summary>
        public String Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
        private String content;
        /// <summary>
        /// 
        /// </summary>
        public long UseCount
        {
            get { return this.usecount; }
            set { this.usecount = value; }
        }
        private long usecount;
        public double Random
        {
            get { return this.random; }
            set { this.random = value; }
        }
        private double random;
        //是否审核通过
        public bool IsAudited;
        //贡献者ID
        public String Writter;
        public static String GetCollectionName()
        {
            return "SystemWants";
        }

    }
    public enum WantType
    {
        all_normal = 0,
        m_normal = 11,
        m_humor = 12,
        m_stange = 13,
        m_praise = 14,
        m_cool = 15,
        f_normal = 21,
        f_humor = 22,
        f_stange = 23,
        f_praise = 24,
        f_cool = 25
    }

}
