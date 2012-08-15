///
/// 功能描述：应用管理类
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/2/22
/// 附加信息：
///   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.Sys.Applications
{
    public class ApplicationsFactory
    {
        /// <summary>
        /// 查询所有应用
        /// </summary>
        /// <returns></returns>
        public static IList<Application> GetApp()
        {
            List<Application> list = new List<Application>();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Application> mc = md.GetCollection<Application>("Applications");
                MongoCursor<Application> mcr = mc.FindAll();
                list.AddRange(mcr);
                return list;
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }
        /// <summary>
        /// 添加应用
        /// </summary>
        /// <param name="obj">应用对象</param>
        public static CBB.ExceptionHelper.OperationResult AddApp(String imgpath, String description,String url)
        {
            try
            {
                Application appobj = new Application();
                appobj.ImagePath1 = imgpath;
                appobj.Description1 = description;
                appobj.Url = url;
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Application> mc = md.GetCollection<Application>("Applications");
                mc.Insert(appobj);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }
        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="id">应用id</param>
        public static CBB.ExceptionHelper.OperationResult DeleteApp(String id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<Application> mc = md.GetCollection<Application>("Applications");
                IMongoQuery qc = Query.And(Query.EQ("_id", ObjectId.Parse(id)));
                mc.Remove(qc);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (Exception es)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   es);
            }
        }
    }
}
