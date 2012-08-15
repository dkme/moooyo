using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.FilterWord
{
    /// <summary>
    /// 过滤字符实体类
    /// </summary>
    public class FilterWordMoldel
    {
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;

        public String ID
        {
            get { if (_id != null) return _id.ToString(); else return ""; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        private Boolean is_enable;

        /// <summary>
        /// 是否启用
        /// </summary>
        public Boolean Is_enable
        {
            get { return is_enable; }
            set { is_enable = value; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        private int type;

        /// <summary>
        /// 过滤字类型：删除、屏蔽、待审
        /// </summary>
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        private string word;
        
        /// <summary>
        /// 过滤字符
        /// </summary>
        public string Word
        {
            get { return word; }
            set { word = value; }
        }

        public static String GetCollectionName()
        {
            return "FilterWord";
        }
    }
}
