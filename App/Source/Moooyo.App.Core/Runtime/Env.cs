using System;
using System.Net;

namespace Moooyo.App.Core.Runtime
{
	public class Env
	{
		/// <summary>
		/// The runner device.
		/// </summary>
		public static Moooyo.App.Core.Defs.Device.DeviceDef RunnerDevice;
		public static Moooyo.App.Core.Defs.EnvSetting RunnerEnvSetting;
		/// <summary>
		/// The cookies.
		/// </summary>
		public static CookieCollection Cookies;
	}
}

