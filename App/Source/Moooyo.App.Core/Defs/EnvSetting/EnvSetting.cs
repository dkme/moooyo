using System;

namespace Moooyo.App.Core.Defs
{
	public class EnvSetting
	{
		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>
		/// The version.
		/// </value>
		public Core.Defs.App.Version Version{ get; set; }
		/// <summary>
		/// Gets or sets the account I.
		/// </summary>
		/// <value>
		/// The account I.
		/// </value>
		public string AccountID{ get; set; }
		/// <summary>
		/// Gets or sets the member I.
		/// </summary>
		/// <value>
		/// The member I.
		/// </value>
		public string MemberID{ get; set; }
		/// <summary>
		/// Gets or sets the last operation time.
		/// </summary>
		/// <value>
		/// The last operation time.
		/// </value>
		public DateTime LastOperationTime{ get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Moooyo.App.Core.Environment"/> auto login.
		/// </summary>
		/// <value>
		/// <c>true</c> if auto login; otherwise, <c>false</c>.
		/// </value>
		public bool AutoLogin{ get; set; }

		public EnvSetting ()
		{
		}
	}
}

