using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CBB.MongoDB
{
    /// <summary>
    /// MongoDB帮助类
    /// </summary>
    public class MongoDBHelper
    {
        private static string ConnectionString 
        {
            get {
                if (connectionString == null) connectionString = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("MongoDBConnectionString");
                return connectionString;
            }
        }
        private static string connectionString;

        private static string DBName
        {
            get
            {
                if (dbName == null) dbName = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("MongoDBName");
                return dbName;
            }
        }
        private static string dbName;

        private static MongoServer GetServer
        {
            get{
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

        public static MongoCursor<BsonDocument> GetCursor(String collectionName, IMongoQuery query = null
    , IMongoSortBy sort = null, Int32 page = 0, Int32 itemCount = 0)
        {
            MongoCollection<BsonDocument> collection = MongoDB.GetCollection<BsonDocument>(collectionName);
            sort = sort ?? new SortByDocument { };
            itemCount = (itemCount == 0) ? 1 : itemCount;
            try
            {
                if (page < 1)
                    return ((query == null) ? collection.FindAll() : collection.Find(query)).SetSortOrder(sort);
                else
                    return ((query == null) ? collection.FindAll() : collection.Find(query))
                        .SetSortOrder(sort).SetSkip((page - 1) * itemCount).SetLimit(itemCount);
            }
            finally
            {
                server.Disconnect();
            }
        }

        public static MongoCursor<T> GetCursor<T>(String collectionName, IMongoQuery query = null
, IMongoSortBy sort = null, Int32 page = 0, Int32 itemCount = 0)
        {
            MongoCollection<T> collection = MongoDB.GetCollection<T>(collectionName);
            sort = sort ?? new SortByDocument { };
            itemCount = (itemCount == 0) ? 1 : itemCount;
            try
            {
                if (page < 1)
                    return ((query == null) ? collection.FindAll() : collection.Find(query)).SetSortOrder(sort);
                else
                    return ((query == null) ? collection.FindAll() : collection.Find(query))
                        .SetSortOrder(sort).SetSkip((page - 1) * itemCount).SetLimit(itemCount);
            }
            finally
            {
                //server.Disconnect();
            }
        }

        public static long GetCount(String collectionName, IMongoQuery query = null)
        {
            MongoCollection collection = MongoDB.GetCollection(collectionName);
            try
            {
                return collection.Count(query);
            }
            finally
            { }
        }
    }

}
