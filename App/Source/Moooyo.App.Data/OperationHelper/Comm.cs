using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace Moooyo.App.Data
{
    public static class Comm
    {
        private static string _connectionStr = "";

        /// <summary>
        /// 获得数据库连接串
        /// </summary>
        public static string ConnectionStr
        {
            get
            {
                if (_connectionStr == "")
                {
                    //数据库连接字符串
                    string strConn = string.Empty;
                    //连接Sql Server数据库路径
                    strConn = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("ConnectionString");

                    //判断是连接字符串是否存在
                    if (strConn.Trim() == "")
                    {
                        throw new Exception("未设置ConnectionStr变量，不能开启数据库连接");
                    }
                    else
                    {
                        _connectionStr = strConn;
                        IDbConnection dcon = IDBOperationHelper.GetIDbConnection(_connectionStr);
                        _database = dcon.Database;
                    }
                }
                return _connectionStr;
            }
            set { 
                _connectionStr = value;
                IDbConnection dcon = IDBOperationHelper.GetIDbConnection(_connectionStr);
                _database = dcon.Database;
            }
        }
        private static string _database = "";
        public static string Database
        {
            get 
            {
                if (_database == "")
                {
                    IDbConnection dcon = IDBOperationHelper.GetIDbConnection(ConnectionStr);
                    _database = dcon.Database;
                }
                return _database;
            }
        }
    }
}
