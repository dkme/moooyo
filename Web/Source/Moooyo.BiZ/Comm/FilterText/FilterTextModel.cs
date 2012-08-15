using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.FilterText
{
    //邱志明
    //2012-02-23
    //待审核文本模型
    public class FilterTextModel
    {
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;

        public String ID
        {
            get { if (_id != null) return _id.ToString(); else return ""; }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        private int verify_status;
        /// <summary>
        /// 审核状态
        /// </summary>
        public int Verify_status
        {
            get { return verify_status; }
            set { verify_status = value; }
        }
      
        [System.Web.Script.Serialization.ScriptIgnore]
        private string tablename;
        
        /// <summary>
        /// 所属表名
        /// </summary>
        public string Tablename
        {
            get { return tablename; }
            set { tablename = value; }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        private string colname;

        /// <summary>
        /// 所属列名
        /// </summary>
        public string Colname
        {
            get { return colname; }
            set { colname = value; }
        }
        private string memberID;

        /// <summary>
        /// 原始创建用户编号
        /// </summary>
        public string MemberID
        {
            get { return memberID; }
            set { memberID = value; }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        private string colid;

        /// <summary>
        /// 原始id
        /// </summary>
        public string Colid
        {
            get { if (colid != null) return colid.ToString(); else return ""; }
            set { colid = value; }
        }    

        [System.Web.Script.Serialization.ScriptIgnore]
        private string verify_id;

        /// <summary>
        /// 审核人id
        /// </summary>
        public string Verify_id
        {
            get { if (verify_id != null) return verify_id.ToString(); else return ""; }
            set { verify_id = value; }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        private DateTime verify_time;

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime Verify_time
        {
            get { return verify_time; }
            set { verify_time = value; }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        private string verify_text;

        /// <summary>
        /// 审核文本
        /// </summary>
        public string Verify_text
        {
            get { return verify_text; }
            set { verify_text = value; }
        }
        
        [System.Web.Script.Serialization.ScriptIgnore]
        private DateTime jion_time;

        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime Jion_time
        {
            get { return jion_time; }
            set { jion_time = value; }
        }

        public static String GetCollectionName()
        {
            return "FilterText";
        }
    }
}
