using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.WenWen
{
    //问问回答
    public class WenWenAnswer
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
        //问问ID
        public String WenWenID;
        //内容
        public String Content;
        //创建时间
        public DateTime CreatedTime;
        //创建者
        public Creater.Creater Creater;
        //最佳答案
        public bool IsBestAnswer;

        public static String GetCollectionName()
        {
            return "WenWenAnswer";
        }
    }
}
