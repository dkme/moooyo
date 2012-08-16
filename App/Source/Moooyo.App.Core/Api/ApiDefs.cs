using System;

namespace Moooyo.App.Core.Api
{
	public class CommDefs
	{
		public static string RootUri{get{
				return "http://AppApiV1.moooyo.com";
			}}
	}
	/// <summary>
	/// Accounts API defs.
	/// </summary>
	public class AccountsDefs
	{
		/// <summary>
		/// Gets the create step1.
		/// </summary>
		/// <value>
		/// The create step1.
		/// </value>
		public static string CreateStep1{get{
				return CommDefs.RootUri+"/accounts/createStep1";
			}}
		/// <summary>
		/// Gets the create step2.
		/// </summary>
		/// <value>
		/// The create step2.
		/// </value>
		public static string CreateStep2 {get {
				return CommDefs.RootUri + "/accounts/createStep2";
			}}
		/// <summary>
		/// Gets the login.
		/// </summary>
		/// <value>
		/// The login.
		/// </value>
		public static string Login {get {
				return CommDefs.RootUri + "/accounts/login";
			}}
		/// <summary>
		/// Gets the change password.
		/// </summary>
		/// <value>
		/// The change password.
		/// </value>
		public static string ChangePassword {get {
				return CommDefs.RootUri + "/accounts/changePassword";
			}}


		public AccountsDefs ()
		{
		}
	}

	/// <summary>
	/// Members API defs.
	/// </summary>
	public class MembersDefs{
		/// <summary>
		/// Gets the get member.
		/// </summary>
		/// <value>
		/// The get member.
		/// </value>
		public static string GetMember {get {
				return CommDefs.RootUri + "/members/getMember";
			}}
		/// <summary>
		/// Gets the get full display member.
		/// </summary>
		/// <value>
		/// The get full display member.
		/// </value>
		public static string GetFullDisplayMember {get {
				return CommDefs.RootUri + "/members/getFullDisplayMember";
			}}
	}

	/// <summary>
	/// Relationships API defs.
	/// </summary>
	public class RelationShipsDefs{
		/// <summary>
		/// Gets the get favorers.
		/// </summary>
		/// <value>
		/// The get favorers.
		/// </value>
		public static string GetFavorers {get {
				return CommDefs.RootUri + "/relationships/getFavorers";
			}}
		/// <summary>
		/// Gets the get favored list.
		/// </summary>
		/// <value>
		/// The get favored list.
		/// </value>
		public static string GetFavoredList {get {
				return CommDefs.RootUri + "/relationships/getFavoredList";
			}}
		/// <summary>
		/// Gets the get vistors.
		/// </summary>
		/// <value>
		/// The get vistors.
		/// </value>
		public static string GetVistors {get {
				return CommDefs.RootUri + "/relationships/getVistors";
			}}
	}
}

