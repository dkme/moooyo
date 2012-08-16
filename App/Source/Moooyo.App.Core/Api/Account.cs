using System;
using System.Collections.Generic;
using CBB.ExceptionHelper;
using CBB.NetworkingHelper.HttpHelper;
using System.Json;

namespace Moooyo.App.Core.Api
{
	public class Account
	{
		/// <summary>
		/// Creates the step1.
		/// </summary>
		/// <returns>
		/// The step1 for Creating Account.
		/// </returns>
		public OperationResult CreateStep1 (string loginID, string nickname, string pwd)
		{
			//参数检查
			if (loginID == null || loginID == String.Empty)
				return new OperationResult (false, "参数不完整");
			if (nickname == null || nickname == String.Empty)
				return new OperationResult (false, "参数不完整");
			if (pwd == null || pwd == String.Empty)
				return new OperationResult (false, "参数不完整");

			try {
				//Http请求参数
				List<APIParameter> paras = new List<APIParameter>() { 
                            new APIParameter("loginID",loginID),
                            new APIParameter("nickname",nickname),
                            new APIParameter("pwd",pwd)
                        };

				//执行请求
                APIReturnData returnData = new SyncHttp().HttpPost(AccountsDefs.CreateStep1, paras);
				var jcontent = JsonValue.Parse(returnData.content);

				//保存cookies
				Moooyo.App.Core.Runtime.Env.Cookies = returnData.cookies;

				return new OperationResult((bool)jcontent["ok"],jcontent["err"].ToString());

			} catch (Exception err) {
				return new OperationResult(false,CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
			}
		}
		public Account ()
		{
		}
	}
}

