using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.PhotoCheck
{
    //邱志明
    //2012-02-07
    //待审照片模型
    public class PhotoCheckModel
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

        [System.Web.Script.Serialization.ScriptIgnore]
        private string userId;
        
        /// <summary>
        ///用户ID 
        /// </summary>
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        private string checkImgPath;

        /// <summary>
        /// 待审图片文件名
        /// </summary>
        public string CheckImgPath
        {
            get { return checkImgPath; }
            set { checkImgPath = value; }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        private string adminId;

        /// <summary>
        /// 审核者id
        /// </summary>
        public string AdminId
        {
            get { return adminId; }
            set { adminId = value; }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        private string userHeadName;

        /// <summary>
        /// 用户头像文件名
        /// </summary>
        public string UserHeadName
        {
            get { return userHeadName; }
            set { userHeadName = value; }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        private int checkStatus;

        /// <summary>
        /// 审核状态
        /// </summary>
        public int CheckStatus
        {
            get { return checkStatus; }
            set { checkStatus = value; }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        private DateTime checkTime;

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime CheckTime
        {
            get { return checkTime; }
            set { checkTime = value; }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        private DateTime jionTime;

        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime JionTime
        {
            get { return jionTime; }
            set { jionTime = value; }
        }


    }
}
