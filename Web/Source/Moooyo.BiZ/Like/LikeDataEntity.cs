/*************************************************
 * Functional description ：喜欢数据实体
 * Author：Tao Lau
 * Modify the expansion：Tao Lau
 * Modified date：2012/4/19 Thursday   
 * Remarks：
 * *********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Like
{
    public class LikeDataEntity
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
        //被喜欢的ID
        private String fromId;

        public String FromId
        {
            get { return fromId; }
            set { fromId = value; }
        }
        //喜欢的ID
        private String toId;

        public String ToId
        {
            get { return toId; }
            set { toId = value; }
        }
        //喜欢的数据类型
        private LikeType likeType;

        public LikeType LikeType
        {
            get { return likeType; }
            set { likeType = value; }
        }
        //创建时间
        private DateTime createdTime;
        public DateTime CreatedTime
        {
            get { return createdTime; }
            set { createdTime = value; }
        }
    }
}
