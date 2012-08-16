using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Core.Comm.UniqueNumber
{
    /// <summary>
    /// 唯一编号的数据实体
    /// </summary>
    public class UniqueNumber
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public String ID
        {
			get;set;
        }
        /// <summary>
        /// 默认编号
        /// </summary>
        public String DefaultId {
            get { return this.defaultId; }
            set { this.defaultId = value; }
        }
        private String defaultId;
        /// <summary>
        /// 转化后的编号
        /// </summary>
        public long ConvertedID {
            get { return this.convertedID; }
            set { this.convertedID = value; }
        }
        private long convertedID;
        /// <summary>
        /// 域名编号
        /// </summary>
        public String DomainNameID
        {
            get { return this.domainNameID; }
            set { this.domainNameID = value; }
        }
        private String domainNameID;
        /// <summary>
        /// 编号的类型
        /// </summary>
        public IDType IDType
        {
            get { return this.idType; }
            set { this.idType = value; }
        }
        private IDType idType;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime {
            get { return this.createdTime; }
            set { this.createdTime = value; }
        }
        private DateTime createdTime;
        /// <summary>
        /// 表（集合）名
        /// </summary>
        /// <returns></returns>
        public static String GetCollectionName() {
            return "UniqueNumber";
        }

        public UniqueNumber() 
        { 
        }
    }
}
