/*******************************************************************
 * Functional description ：自动增加编号集合数据实体
 * Author：Lau Tao
 * Modify the expansion：Lau Tao
 * Modified date：2012/3/21 Wednesday 
 * Remarks：
 * ****************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Comm.IncrementIds
{
    /// <summary>
    /// 自动增加编号数据实体
    /// </summary>
    public class IncrementIds
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public String ID {
            get { if (_id != null)return _id.ToString(); else return ""; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;
        /// <summary>
        /// 自动增加编号的类型
        /// </summary>
        public IdTypes IdTypes {
            get { return this.idTypes; }
            set { this.idTypes = value; }
        }
        private IdTypes idTypes;
        /// <summary>
        /// 自动增加编号
        /// </summary>
        public long IcmetId {
            get { return this.icmetId; }
            set { this.icmetId = value; }
        }
        private long icmetId;
        /// <summary>
        /// 表（集合）名
        /// </summary>
        /// <returns></returns>
        public static String GetCollectionName() {
            return "IncrementIds";
        }
    }
}
