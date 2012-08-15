using System;

namespace Moooyo.App.Core.Defs.App
{
	/// <summary>
	/// App Runned Over History.
	/// </summary>
	public class History
	{
		/// <summary>
		/// Gets or sets the install time.
		/// </summary>
		/// <value>
		/// The install time.
		/// </value>
		public DateTime InstallTime{ get; set; }
		/// <summary>
		/// Gets or sets the updated times.
		/// </summary>
		/// <value>
		/// The updated times.
		/// </value>
		public int UpdatedTimes{ get; set; }
		/// <summary>
		/// Gets or sets the started at version.
		/// </summary>
		/// <value>
		/// The started at version.
		/// </value>
		public Version StartedAtVersion{ get; set; }
		/// <summary>
		/// Gets or sets the now version.
		/// </summary>
		/// <value>
		/// The now version.
		/// </value>
		public Version NowVersion{ get; set; }
		/// <summary>
		/// Gets or sets the runed times.
		/// </summary>
		/// <value>
		/// The runed times.
		/// </value>
		public int RunnedTimes{ get; set; }
		/// <summary>
		/// Gets or sets the last runed time.
		/// </summary>
		/// <value>
		/// The last runed time.
		/// </value>
		public DateTime LastRunnedTime{ get; set; }
		public History ()
		{
		}
	}
}

