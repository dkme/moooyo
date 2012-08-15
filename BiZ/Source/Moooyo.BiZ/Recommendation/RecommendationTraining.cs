using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace WoXi.BiZ.Recommendation
{
    /// <summary>
    /// 训练数据基类
    /// </summary>
    public abstract class RecommendationTraining
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
        public String MemberID;
        public String ObjectID;
        public float Value;

        public abstract String GetCollectionName();
    }
}
