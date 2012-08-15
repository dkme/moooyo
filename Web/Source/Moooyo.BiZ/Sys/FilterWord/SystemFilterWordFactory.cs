using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Sys.FilterWord
{
    //邱志明
    //2012-02-22
    //过滤词处理器
    public class SystemFilterWordFactory
    {
        /// <summary>
        /// 按类型查询
        /// </summary>
        /// <param name="wt"></param>
        /// <returns></returns>
        public static List<Moooyo.BiZ.FilterWord.FilterWordMoldel> GetAllSystemFilterWord(CBB.CheckHelper.FilterWord.word_type wt) 
        {
            try
            {
                Moooyo.BiZ.FilterWord.FilterWordOperation fwo = new BiZ.FilterWord.FilterWordOperation();
                return fwo.GetFilterWordModels(wt);
            }
            catch (Exception ex) 
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }
        /// <summary>
        /// 查询所有的
        /// </summary>
        /// <returns></returns>
        public static List<Moooyo.BiZ.FilterWord.FilterWordMoldel> GetAllSystemFilterWord()
        {
            try
            {
                Moooyo.BiZ.FilterWord.FilterWordOperation fwo = new BiZ.FilterWord.FilterWordOperation();
                return fwo.GetWord();
            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

        /// <summary>
        /// 添加一个过滤词
        /// </summary>
        /// <param name="wordname">名称</param>
        /// <param name="word_is_enable">状态</param>
        /// <param name="wordtype">类型</param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult AddFilterWord(string wordname, bool word_is_enable, int wordtype) 
        {
            try
            {
                Moooyo.BiZ.FilterWord.FilterWordMoldel fwm = new BiZ.FilterWord.FilterWordMoldel();
                fwm.Is_enable = word_is_enable;
                fwm.Type = wordtype;
                fwm.Word = wordname;
                Moooyo.BiZ.FilterWord.FilterWordOperation fwo = new BiZ.FilterWord.FilterWordOperation();
                fwo.AddFilterWord(fwm);
                return new CBB.ExceptionHelper.OperationResult(true);

            }
            catch (Exception ex) 
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

        /// <summary>
        /// 依据id批量删除
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult DeleteFilterWords(List<ObjectId> _id)
        {
            try
            {
                Moooyo.BiZ.FilterWord.FilterWordOperation fwo = new BiZ.FilterWord.FilterWordOperation();
                fwo.DeleteWord(_id);
                return new CBB.ExceptionHelper.OperationResult(true);

            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

        /// <summary>
        /// 依据id批量更新状态
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult ChangeFilterWords(List<ObjectId> _id, bool is_enable)
        {
            try
            {
                Moooyo.BiZ.FilterWord.FilterWordOperation fwo = new BiZ.FilterWord.FilterWordOperation();
                fwo.UpdateWord(_id,is_enable);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="_id">过滤词集合</param>
        /// <param name="wt">类型枚举</param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult UploadFilterWords(List<string> _id, CBB.CheckHelper.FilterWord.word_type wt)
        {
            try
            {
                Moooyo.BiZ.FilterWord.FilterWordOperation fwo = new BiZ.FilterWord.FilterWordOperation();
                fwo.AddWordList(_id, wt);
                return new CBB.ExceptionHelper.OperationResult(true);
            }
            catch (Exception ex)
            {
                throw new CBB.ExceptionHelper.OperationException(
                   CBB.ExceptionHelper.ErrType.SystemErr,
                   CBB.ExceptionHelper.ErrNo.DBOperationError,
                   ex);
            }
        }
        /// <summary>
        /// 检测关键字
        /// </summary>
        /// <param name="text">原始文本</param>
        /// <param name="wt">检测类型</param>
        /// <returns></returns>
        public static List<string> CheckFilterWord(ref string text,CBB.CheckHelper.FilterWord.word_type wt) 
        {
            Moooyo.BiZ.FilterWord.FilterWordController.GetNewFilterWordController();
            return new Moooyo.BiZ.FilterWord.FilterWordController().FilterText(ref text, wt);
        }
    }
}
