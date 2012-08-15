using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Gift
{
    /// <summary>
    /// 系统礼物定义
    /// </summary>
    public class GiftDefs
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
        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        private String name;
        public ValueType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        private ValueType type;
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private int value;
        public int Price
        {
            get { return this.price; }
            set { this.price = value; }
        }
        private int price;
        public String ImageID
        {
            get { return imageID;}
            set { this.imageID = value; }
        }
        public String imageID;
        public String IconID
        {
            get { return iconID; }
            set { this.iconID = value; }
        }
        public String iconID;
        public String Comment
        {
            get { return comment; }
            set { this.comment = value; }
        }
        public String comment;
        public int SellCount;
        /// <summary>
        /// 用户建立时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return this.createdTime; }
            set { this.createdTime = value; }
        }
        private DateTime createdTime;
    }
    public enum ValueType
    {
        Hot
    }
}
