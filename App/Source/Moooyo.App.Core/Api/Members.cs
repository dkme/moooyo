using System;
using System.Collections.Generic;
using CBB.ExceptionHelper;
using CBB.NetworkingHelper.HttpHelper;
using ServiceStack.Text;

namespace Moooyo.App.Core.Api
{
	/// <summary>
	/// Members APIs.
	/// </summary>
	public class Members
	{
		/// <summary>
		/// Gets the member.
		/// </summary>
		/// <returns>
		/// The member.
		/// </returns>
		/// <param name='mid'>
		/// Middle.
		/// </param>
		public BiZ.Core.Member.Member GetMember (string mID)
		{
			try {
				//参数检查
				if (mID == null || mID == String.Empty)
					return new BiZ.Core.Member.Member();

				//Http请求参数
				List<APIParameter> paras = new List<APIParameter>() { 
                            new APIParameter("mID",mID.Trim())
                        };

				//执行请求
                APIReturnData returnData = new SyncHttp().HttpGet(MembersDefs.GetMember, paras, Runtime.Env.Cookies);

				//保存cookies
				Moooyo.App.Core.Runtime.Env.Cookies = returnData.cookies;

				BiZ.Core.Member.Member mobj = JsonSerializer.DeserializeFromString<BiZ.Core.Member.Member>(returnData.content);
				return mobj;

			} catch (Exception err) {
				throw new CBB.ExceptionHelper.OperationException(
					CBB.ExceptionHelper.ErrType.SystemErr,
					CBB.ExceptionHelper.ErrNo.DBOperationError,
					err);
			}
		}

		public BiZ.Core.DisplayObjs.MemberFullDisplayObj GetFullDisplayMember(string mID)
		{
			try {
				//参数检查
				if (mID == null || mID == String.Empty)
					return new BiZ.Core.DisplayObjs.MemberFullDisplayObj();

				//Http请求参数
				List<APIParameter> paras = new List<APIParameter>() { 
                            new APIParameter("mID",mID.Trim())
                        };

				//执行请求
                APIReturnData returnData = new SyncHttp().HttpGet(MembersDefs.GetFullDisplayMember, paras, Runtime.Env.Cookies);

				//保存cookies
				Moooyo.App.Core.Runtime.Env.Cookies = returnData.cookies;

				BiZ.Core.DisplayObjs.MemberFullDisplayObj mobj = JsonSerializer.DeserializeFromString<BiZ.Core.DisplayObjs.MemberFullDisplayObj>(returnData.content);
				return mobj;

			} catch (Exception err) {
				throw new CBB.ExceptionHelper.OperationException(
					CBB.ExceptionHelper.ErrType.SystemErr,
					CBB.ExceptionHelper.ErrNo.DBOperationError,
					err);
			}
		}

		public Members ()
		{
		}
	}
}

