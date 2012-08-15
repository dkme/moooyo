using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Member.Relation
{
    /// <summary>
    /// 礼物
    /// </summary>
    public class Giftor : RelationMember
    {
        /// <summary>
        /// 系统礼物ID
        /// </summary>
        public String SystemGiftID
        {
            get { return this.systemGiftID; }
            set { this.systemGiftID = value; }
        }
        private String systemGiftID;
        /// <summary>
        /// 礼物名称
        /// </summary>
        public String GiftName
        {
            get { return this.giftName; }
            set { this.giftName = value; }
        }
        private String giftName;

        public override String GetCollectionName() { return "Giftor"; }
    }
}
