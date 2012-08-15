using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Builders;

namespace CBB.MongoDB
{
    public class GridFSHelper
    {
        private static string ConnectionString
        {
            get
            {
                if (connectionString == null) connectionString = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("MongoGridFSDBConnectionString");
                return connectionString;
            }
        }
        private static string connectionString;

        private static string DBName
        {
            get
            {
                if (dbName == null) dbName = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("MongoGridFSDBName");
                return dbName;
            }
        }
        private static string dbName;

        private static MongoServer GetServer
        {
            get
            {
                if (server == null)
                {
                    MongoServer ms = MongoServer.Create(ConnectionString);
                    return ms;
                }
                return server;
            }
        }
        private static MongoServer server;

        public static MongoDatabase MongoDB
        {
            get
            {
                if (mongoDB == null) mongoDB = GetServer.GetDatabase(DBName);
                return mongoDB;
            }
        }
        private static MongoDatabase mongoDB;

        public static void UploadFile(Stream file,String filename)
        {
            file.Seek(0, SeekOrigin.Begin);
            MongoDB.GridFS.Upload(file, filename.ToLower());
        }
        public byte[] GetFile(String filename)
        {
            var file = MongoDB.GridFS.FindOne(Query.EQ("filename", filename.ToLower()));
            
            if (file != null)
            {
                MongoGridFSStream stream = file.OpenRead();
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                return bytes;
            }
            else
                return null;

        }
        public static void DelFile(String filename)
        {
            MongoDB.GridFS.Delete(filename.ToLower());
        }
    }
}
