using System;
using System.Collections.Generic;
using CBB.ExceptionHelper;
using CBB.NetworkingHelper.HttpHelper;
using ServiceStack.Text;

namespace Moooyo.App.Core.Api
{
	/// <summary>
	/// Accounts APIs.
	/// </summary>
	public class Accounts
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
                APIReturnData returnData = new SyncHttp().HttpGet(AccountsDefs.CreateStep1, paras);
				CBB.ExceptionHelper.OperationResult result = JsonSerializer.DeserializeFromString<CBB.ExceptionHelper.OperationResult>(returnData.content);

				//保存cookies
				Moooyo.App.Core.Runtime.Env.Cookies = returnData.cookies;

				return result;

			} catch (Exception err) {
				return new OperationResult(false,CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
			}
		}
		/// <summary>
		/// Creates the step2.
		/// </summary>
		/// <returns>
		/// The step2.
		/// </returns>
		/// <param name='year'>
		/// Year.
		/// </param>
		/// <param name='month'>
		/// Month.
		/// </param>
		/// <param name='day'>
		/// Day.
		/// </param>
		/// <param name='sex'>
		/// Sex.
		/// </param>
		public OperationResult CreateStep2 (int year, int month, int day, int sex)
		{
			try {
				//Http请求参数
				List<APIParameter> paras = new List<APIParameter>() { 
                            new APIParameter("year",year.ToString().Trim()),
                            new APIParameter("month",month.ToString().Trim()),
                            new APIParameter("day",day.ToString().Trim()),
							new APIParameter("sex",sex.ToString().Trim())
                        };

				//执行请求
                APIReturnData returnData = new SyncHttp().HttpGet(AccountsDefs.CreateStep2, paras, Runtime.Env.Cookies);
				CBB.ExceptionHelper.OperationResult result = JsonSerializer.DeserializeFromString<CBB.ExceptionHelper.OperationResult>(returnData.content);

				//保存cookies
				Moooyo.App.Core.Runtime.Env.Cookies = returnData.cookies;

				return result;

			} catch (Exception err) {
				return new OperationResult(false,CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
			}
		}
		/// <summary>
		/// Login the specified loginID, password, deviceUID and deviceType.
		/// </summary>
		/// <param name='loginID'>
		/// Login I.
		/// </param>
		/// <param name='password'>
		/// Password.
		/// </param>
		/// <param name='deviceUID'>
		/// Device user interface.
		/// </param>
		/// <param name='deviceType'>
		/// Device type.
		/// </param>
		public OperationResult Login (string loginID, string password, string deviceUID, Core.BiZ.Comm.Device.DeviceType deviceType)
		{
			//参数检查
			if (loginID == null || loginID == String.Empty)
				return new OperationResult (false, "参数不完整");
			if (password == null || password == String.Empty)
				return new OperationResult (false, "参数不完整");
			if (deviceUID == null || deviceUID == String.Empty)
				return new OperationResult (false, "参数不完整");

			try {
				//Http请求参数
				List<APIParameter> paras = new List<APIParameter>() { 
                            new APIParameter("loginID",loginID),
                            new APIParameter("password",password),
                            new APIParameter("deviceUID",deviceUID),
							new APIParameter("deviceType",((int)deviceType).ToString())
                        };

				//执行请求
                APIReturnData returnData = new SyncHttp().HttpGet(AccountsDefs.Login, paras);
				CBB.ExceptionHelper.OperationResult result = JsonSerializer.DeserializeFromString<CBB.ExceptionHelper.OperationResult>(returnData.content);

				//保存cookies
				Moooyo.App.Core.Runtime.Env.Cookies = returnData.cookies;

				return result;

			} catch (Exception err) {
				return new OperationResult(false,CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
			}
		}
		/// <summary>
		/// Devices the auto login.
		/// </summary>
		/// <returns>
		/// The auto login.
		/// </returns>
		/// <param name='loginID'>
		/// Login I.
		/// </param>
		/// <param name='deviceUID'>
		/// Device user interface.
		/// </param>
		/// <param name='deviceType'>
		/// Device type.
		/// </param>
		public OperationResult DeviceAutoLogin (string loginID, string deviceUID, Core.BiZ.Comm.Device.DeviceType deviceType)
		{
			//参数检查
			if (loginID == null || loginID == String.Empty)
				return new OperationResult (false, "参数不完整");
			if (deviceUID == null || deviceUID == String.Empty)
				return new OperationResult (false, "参数不完整");

			try {
				//Http请求参数
				List<APIParameter> paras = new List<APIParameter>() { 
                            new APIParameter("loginID",loginID),
                            new APIParameter("deviceUID",deviceUID),
							new APIParameter("deviceType",((int)deviceType).ToString())
                        };

				//执行请求
                APIReturnData returnData = new SyncHttp().HttpGet(AccountsDefs.Login, paras);
				CBB.ExceptionHelper.OperationResult result = JsonSerializer.DeserializeFromString<CBB.ExceptionHelper.OperationResult>(returnData.content);

				//保存cookies
				Moooyo.App.Core.Runtime.Env.Cookies = returnData.cookies;

				return result;

			} catch (Exception err) {
				return new OperationResult(false,CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
			}
		}
		/// <summary>
		/// Changes the password.
		/// </summary>
		/// <returns>
		/// The password.
		/// </returns>
		/// <param name='oldpwd'>
		/// Oldpwd.
		/// </param>
		/// <param name='newpwd'>
		/// Newpwd.
		/// </param>
		public OperationResult ChangePassword (string oldpwd, string newpwd)
		{
			//参数检查
			if (oldpwd == null || oldpwd == String.Empty)
				return new OperationResult (false, "参数不完整");
			if (newpwd == null || newpwd == String.Empty)
				return new OperationResult (false, "参数不完整");

			try {
				//Http请求参数
				List<APIParameter> paras = new List<APIParameter>() { 
                            new APIParameter("oldpwd",oldpwd.Trim()),
                            new APIParameter("newpwd",newpwd.Trim())
                        };

				//执行请求
                APIReturnData returnData = new SyncHttp().HttpGet(AccountsDefs.ChangePassword, paras, Runtime.Env.Cookies);
				CBB.ExceptionHelper.OperationResult result = JsonSerializer.DeserializeFromString<CBB.ExceptionHelper.OperationResult>(returnData.content);

				//保存cookies
				Moooyo.App.Core.Runtime.Env.Cookies = returnData.cookies;

				return result;

			} catch (Exception err) {
				return new OperationResult(false,CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
			}
		}

		public Accounts()
		{
		}
	}
}

