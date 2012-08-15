using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Member.Connector
{
    /// <summary>
    /// 用户平台连接数据库服务类
    /// </summary>
    public class ConnectorProvider
    {
        //用户平台连接
        public static IList<Connector> GetConnectors(String mid)
        {
            try
            {
                MongoCursor<Connector> mc = MongoDBHelper.GetCursor<Connector>(
                    Connector.GetCollectionName(),
                    Query.EQ("MemberID", mid),
                    null,
                    0,
                    0);

                List<Connector> objs = new List<Connector>();
                objs.AddRange(mc);

                return objs;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }

        }
        //获取指定用户平台连接
        public static T GetConnector<T>(String mid, Platform platformType)
        {
            T iv ;
            //try
            //{
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(Connector.GetCollectionName());
                iv = mc.FindOne(Query.And(Query.EQ("MemberID", mid), Query.EQ("PlatformType", platformType)));
                return iv;
            //}
            //catch (System.Exception err)
            //{
            //    throw new CBB.ExceptionHelper.OperationException(
            //        CBB.ExceptionHelper.ErrType.SystemErr,
            //        CBB.ExceptionHelper.ErrNo.DBOperationError,
            //        err);
            //}
        }
        //按平台ID获取平台连接
        public static T GetConnectorByConnectorID<T>(String cid, Platform platformType) where T: Connector
        {
            T iv;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<T> mc = md.GetCollection<T>(Connector.GetCollectionName());
                iv = mc.FindOne(Query.And(Query.EQ("ConnectID", cid), Query.EQ("PlatformType", platformType)));
                return iv;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //保存用户平台连接
        public static CBB.ExceptionHelper.OperationResult SaveConnector(Connector connector)
        {
            if (connector == null) return new CBB.ExceptionHelper.OperationResult(false, "保存平台连接失败：平台连接器为null");
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Connector> mc = md.GetCollection<Connector>(Connector.GetCollectionName());
                mc.Save(connector);

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //保存平台连接中的用户ID
        public static CBB.ExceptionHelper.OperationResult SaveConnectorMemberID(String cid, Platform platformType, String mid)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Connector> mc = md.GetCollection<Connector>(Connector.GetCollectionName());
                mc.Update(
                    Query.And(Query.EQ("ConnectID", cid), Query.EQ("PlatformType", platformType)),
                    Update.Set("MemberID", mid), SafeMode.True);

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //设置用户平台可用状态
        //取消绑定时，本状态为false
        public static CBB.ExceptionHelper.OperationResult SaveConnectorEnableStatus(String mid, Platform platformType,bool enable)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Connector> mc = md.GetCollection<Connector>(Connector.GetCollectionName());
                mc.Update(
                    Query.And(Query.EQ("MemberID", mid), Query.EQ("PlatformType", platformType)),
                    Update.Set("Enable", enable), SafeMode.True);

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        //移除绑定
        public static CBB.ExceptionHelper.OperationResult UnBindPlatform(String mid, Platform platformType)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Connector> mc = md.GetCollection<Connector>(Connector.GetCollectionName());
                mc.Remove(
                    Query.And(Query.EQ("MemberID", mid), Query.EQ("PlatformType", platformType)), SafeMode.True);

                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
    }
}
