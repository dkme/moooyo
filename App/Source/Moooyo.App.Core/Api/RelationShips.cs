using System;
using System.Collections.Generic;
using CBB.ExceptionHelper;
using CBB.NetworkingHelper.HttpHelper;
using ServiceStack.Text;
using Moooyo.App.Core.BiZ.Member.Relation;

namespace Moooyo.App.Core.Api
{
	/// <summary>
	/// Relation ships.
	/// </summary>
	public class RelationShips
	{
		/// <summary>
		/// Gets the favorers.
		/// </summary>
		/// <returns>
		/// The favorers.
		/// </returns>
		/// <param name='pageSize'>
		/// Page size.
		/// </param>
		/// <param name='pageNo'>
		/// Page no.
		/// </param>
		public IList<Favorer> GetFavorers (int pageSize,int pageNo)
		{
			try {
				//Http请求参数
				List<APIParameter> paras = new List<APIParameter>() { 
                            new APIParameter("pageSize",pageSize.ToString().Trim()),
                            new APIParameter("pageNo",pageNo.ToString().Trim())
                        };

				//执行请求
                APIReturnData returnData = new SyncHttp().HttpGet(RelationShipsDefs.GetFavorers, paras, Runtime.Env.Cookies);
				List<Favorer> list = JsonSerializer.DeserializeFromString<List<Favorer>>(returnData.content);

				//保存cookies
				Moooyo.App.Core.Runtime.Env.Cookies = returnData.cookies;

				return list;

			} catch (Exception err) {
				throw new CBB.ExceptionHelper.OperationException(
					CBB.ExceptionHelper.ErrType.SystemErr,
					CBB.ExceptionHelper.ErrNo.DBOperationError,
					err);
			}
		}
		/// <summary>
		/// Gets the favored list.
		/// </summary>
		/// <returns>
		/// The favored list.
		/// </returns>
		/// <param name='pageSize'>
		/// Page size.
		/// </param>
		/// <param name='pageNo'>
		/// Page no.
		/// </param>
		public IList<Favorer> GetFavoredList (int pageSize, int pageNo)
		{
			try {
				//Http请求参数
				List<APIParameter> paras = new List<APIParameter>() { 
                            new APIParameter("pageSize",pageSize.ToString().Trim()),
                            new APIParameter("pageNo",pageNo.ToString().Trim())
                        };

				//执行请求
                APIReturnData returnData = new SyncHttp().HttpGet(RelationShipsDefs.GetFavoredList, paras, Runtime.Env.Cookies);
				List<Favorer> list = JsonSerializer.DeserializeFromString<List<Favorer>>(returnData.content);

				//保存cookies
				Moooyo.App.Core.Runtime.Env.Cookies = returnData.cookies;

				return list;

			} catch (Exception err) {
				throw new CBB.ExceptionHelper.OperationException(
					CBB.ExceptionHelper.ErrType.SystemErr,
					CBB.ExceptionHelper.ErrNo.DBOperationError,
					err);
			}
		}
		/// <summary>
		/// Gets the vistors.
		/// </summary>
		/// <returns>
		/// The vistors.
		/// </returns>
		/// <param name='pageSize'>
		/// Page size.
		/// </param>
		/// <param name='pageNo'>
		/// Page no.
		/// </param>
		public IList<Visitor> GetVistors (int pageSize, int pageNo)
		{
			try {
				//Http请求参数
				List<APIParameter> paras = new List<APIParameter>() { 
                            new APIParameter("pageSize",pageSize.ToString().Trim()),
                            new APIParameter("pageNo",pageNo.ToString().Trim())
                        };

				//执行请求
                APIReturnData returnData = new SyncHttp().HttpGet(RelationShipsDefs.GetFavoredList, paras, Runtime.Env.Cookies);
				List<Visitor> list = JsonSerializer.DeserializeFromString<List<Visitor>>(returnData.content);

				//保存cookies
				Moooyo.App.Core.Runtime.Env.Cookies = returnData.cookies;

				return list;

			} catch (Exception err) {
				throw new CBB.ExceptionHelper.OperationException(
					CBB.ExceptionHelper.ErrType.SystemErr,
					CBB.ExceptionHelper.ErrNo.DBOperationError,
					err);
			}
		}

		public RelationShips ()
		{
		}
	}
}

