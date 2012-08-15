using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using Moooyo.BiZ.FilterText;

namespace Moooyo.BiZ.Sys.FilterText
{
    //邱志明
    //2012-02-23
    //待审文本后台控制器
    public class SystemFilterTextFactory
    {
        /// <summary>
        /// 依据状态类型批量获取待审文本
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        public static List<Moooyo.BiZ.FilterText.FilterTextModel> GetAllFilterText(Moooyo.BiZ.FilterText.VerifyStatus vs,int pageno, int pagesize) 
        {
            try
            {
                return new Moooyo.BiZ.FilterText.FilterTextOperation().GetFilterTexts(vs,pageno,pagesize);
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
        ///  批量更新待审文本状态
        /// </summary>
        /// <param name="idlist">id集合</param>
        /// <param name="vs">更新状态枚举</param>
        /// <param name="adminId">管理员id</param>
        /// <param name="uptext">修改后的文本</param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult UpdateFilterTexts(List<ObjectId> idlist, Moooyo.BiZ.FilterText.VerifyStatus vs, string adminId, List<string> uptext) 
        {
            try{
                new Moooyo.BiZ.FilterText.FilterTextOperation().UpdateFilterTexts(idlist, vs, adminId, uptext);
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
        /// 更新一个待审文本状态
        /// </summary>
        /// <param name="_id">id</param>
        /// <param name="vs">更新状态枚举</param>
        /// <param name="admin_id">管理员id</param>
        /// <param name="text">修改后的文本</param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult UpdateFilterText(ObjectId _id, VerifyStatus vs, string admin_id,string text) 
        {
            try{
                new Moooyo.BiZ.FilterText.FilterTextOperation().UpdateFilterText(_id, vs, admin_id, text);
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
        /// 返回总条数
        /// </summary>
        /// <param name="verifyStatus"></param>
        /// <returns></returns>
        public static long GetCount(VerifyStatus verifyStatus) 
        {
            return new FilterTextOperation().GetCount(verifyStatus);
            
        }
        /// <summary>
        /// 删除待审文本(支持批量)
        /// </summary>
        /// <param name="id">id集合</param>
        /// <returns></returns>
        public static CBB.ExceptionHelper.OperationResult DeleteFilterText(List<ObjectId> id)
        {
            try
            {
                new Moooyo.BiZ.FilterText.FilterTextOperation().DeleteFilterTexts(id);
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
    }
}
