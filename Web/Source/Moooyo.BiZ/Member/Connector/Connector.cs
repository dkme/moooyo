using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MongoDB.Bson;

namespace Moooyo.BiZ.Member.Connector
{
    /// <summary>
    /// 外部平台连接基类
    /// </summary>
    public abstract class Connector
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
        //用户ID
        public String MemberID;
        //平台定义
        public Platform PlatformType;
        //用户连接ID
        public String ConnectID;
        public String OAuth_Token;
        public String OAuth_Token_Secret;
        //平台OAuth版本
        public OAuthType OAuthType;
        //过期时间
        public DateTime ExpireDate;
        //用户是否启用
        public bool Enable;
        //用户信息
        public String Name;
        public String ICONPath;
        public String Sex;
        public String SexStr;
        public String ProvinceCode;
        public String Province;
        public String CityCode;
        public String City;
        public String Age;
        public String Year;
        public String Month;
        public String Day;

        /// <summary>
        /// 获取连接状态
        /// </summary>
        /// <returns></returns>
        public abstract ConnectStatus GetConnectStatus();
        /// <summary>
        /// 连接外部平台
        /// </summary>
        /// <returns></returns>
        public abstract String Connect(String addparams);
        /// <summary>
        /// 设置外部平台连接禁用
        /// </summary>
        /// <returns></returns>
        public abstract CBB.ExceptionHelper.OperationResult SetDisable();
        /// <summary>
        /// 设置外部平台连接启用
        /// </summary>
        /// <returns></returns>
        public abstract CBB.ExceptionHelper.OperationResult SetEnable();
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public abstract String SendInfo(string url, string info);
        /// <summary>
        /// 发送带图片的消息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="pic"></param>
        /// <returns></returns>
        public abstract string SendInfoWithPic(string url, string info, string pic);
        public static String GetCollectionName() { return "PlatformConnector"; }
    }
    /// <summary>
    /// 连接状态
    /// </summary>
    public enum ConnectStatus
    {
        //可用
        Available=1,
        //未验证
        Unauthorized=7,
        //已过期
        Expired=8,
        //未知
        Unknow=9
    }
}
