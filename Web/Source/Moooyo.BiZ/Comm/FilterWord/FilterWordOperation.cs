using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using CBB.MongoDB;

namespace Moooyo.BiZ.FilterWord
{
    // 邱志明
    // 2012-02-21
    // 过滤字符管理类
    public class FilterWordOperation
    {
        /// <summary>
        /// 批量插入过滤词
        /// </summary>
        /// <param name="wordlist">过滤词集合</param>
        /// <param name="wt">过滤词类型</param>
        public void AddWordList(List<string> wordlist, CBB.CheckHelper.FilterWord.word_type wt) 
        {
            List<FilterWordMoldel> list = new List<FilterWordMoldel>();
            foreach(string tempword in wordlist)
            {
                FilterWordMoldel fwm = new FilterWordMoldel();
                fwm.Is_enable = true;
                fwm.Type = Convert.ToInt32(wt);
                fwm.Word = tempword;
                list.Add(fwm);
            }
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterWordMoldel> mc = md.GetCollection<FilterWordMoldel>("FilterWord");
                mc.InsertBatch(typeof(FilterWordMoldel), list);
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
        /// 插入单个过滤词
        /// </summary>
        /// <param name="fwm">过滤词对象</param>
        public void AddFilterWord(FilterWordMoldel fwm) 
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterWordMoldel> mc = md.GetCollection<FilterWordMoldel>("FilterWord");
                mc.Insert(fwm);
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
        /// 更新过滤词类型
        /// </summary>
        /// <param name="mid">_id</param>
        /// <param name="type">类型（对应的枚举值）</param>
        public void UpdateWord(string mid,int type) 
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterWordMoldel> mc = md.GetCollection<FilterWordMoldel>("FilterWord");
                mc.Update(Query.EQ("_id", ObjectId.Parse(mid)),
                    Update.Set("type", type)
                    );             
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
        /// 更新过滤词状态
        /// </summary>
        /// <param name="mid">_id</param>
        /// <param name="type"></param>
        public void UpdateWord(string mid, bool Is_enable)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterWordMoldel> mc = md.GetCollection<FilterWordMoldel>("FilterWord");
                mc.Update(Query.EQ("_id", ObjectId.Parse(mid)),
                    Update.Set("Is_enable", Is_enable)
                    );
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
        /// 依据id批量更新过滤词状态
        /// </summary>
        /// <param name="mid"></param>
        public void UpdateWord(List<ObjectId> _id, bool Is_enable)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterWordMoldel> mc = md.GetCollection<FilterWordMoldel>("FilterWord");
                QueryConditionList qcl =  Query.In("_id", new BsonArray(_id.ToArray()));
                mc.Update(qcl,Update.Set("Is_enable", Is_enable),UpdateFlags.Multi);
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
        /// 查询指定类型的过滤词
        /// </summary>
        /// <param name="wt">过滤词类型枚举</param>
        /// <returns>过滤词集合</returns>
        public List<string> GetWord(CBB.CheckHelper.FilterWord.word_type wt) 
        {
            List<FilterWordMoldel> list = new List<FilterWordMoldel>();
            List<string> wordlist = null;
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterWordMoldel> mc = md.GetCollection<FilterWordMoldel>("FilterWord");
                IMongoQuery qc = Query.And(Query.EQ("Type", Convert.ToInt32(wt)));
                MongoCursor<FilterWordMoldel> mcr =  mc.Find(qc);
                list.AddRange(mcr);
                if (null != list) 
                {
                    wordlist = new List<string>();
                    foreach (FilterWordMoldel fwm in list) 
                    {
                        wordlist.Add(fwm.Word.Trim());
                    }
                }
                return wordlist;                
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
        /// 查询指定类型的过滤词模型
        /// </summary>
        /// <param name="wt">过滤词类型枚举</param>
        /// <returns>过滤词模型集合</returns>
        public List<FilterWordMoldel> GetFilterWordModels(CBB.CheckHelper.FilterWord.word_type wt)
        {
            List<FilterWordMoldel> list = new List<FilterWordMoldel>();
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterWordMoldel> mc = md.GetCollection<FilterWordMoldel>("FilterWord");
                IMongoQuery qc = Query.And(Query.EQ("Type", Convert.ToInt32(wt)));
                MongoCursor<FilterWordMoldel> mcr = mc.Find(qc);
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
        /// 依据id删除过滤词
        /// </summary>
        /// <param name="mid"></param>
        public void DeleteWord(string mid) 
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterWordMoldel> mc = md.GetCollection<FilterWordMoldel>("FilterWord");
                IMongoQuery qc = Query.And(Query.EQ("_id", ObjectId.Parse(mid)));
                mc.Remove(qc);
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
        /// 依据id批量删除过滤词
        /// </summary>
        /// <param name="mid"></param>
        public void DeleteWord(List<ObjectId> _id)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterWordMoldel> mc = md.GetCollection<FilterWordMoldel>("FilterWord");
                IMongoQuery qc = Query.In("_id",new BsonArray(_id.ToArray()));
                mc.Remove(qc);
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
        /// 依据类型删除过滤词
        /// </summary>
        /// <param name="wt">过滤词类型枚举值</param>
        public void DeleteWord(CBB.CheckHelper.FilterWord.word_type wt)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterWordMoldel> mc = md.GetCollection<FilterWordMoldel>("FilterWord");
                IMongoQuery qc = Query.And(Query.EQ("Type", Convert.ToInt32(wt)));
                mc.Remove(qc);
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
        /// 依据启用状态删除过滤词
        /// </summary>
        /// <param name="is_enable">是否启用</param>
        public void DeleteWord(bool is_enable)
        {
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterWordMoldel> mc = md.GetCollection<FilterWordMoldel>("FilterWord");
                IMongoQuery qc = Query.And(Query.EQ("Is_enable", is_enable));
                mc.Remove(qc);
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
        /// 查询所有过滤词模型
        /// </summary>
        /// <returns></returns>
        public List<FilterWordMoldel> GetWord() 
        {
            List<FilterWordMoldel> list = new List<FilterWordMoldel>();
            
            try
            {
                MongoDatabase md = MongoDBHelper.MongoDB;
                MongoCollection<FilterWordMoldel> mc = md.GetCollection<FilterWordMoldel>("FilterWord");
                MongoCursor<FilterWordMoldel> mcr = mc.FindAll();
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


    }
}
