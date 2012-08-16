// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Moooyo.App.Iphone
{
	[Register ("RegController")]
	partial class RegController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField EmailText { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField PasswordText { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField ConfirmPasswordText { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField NickeNameText { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton SubmitBtton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (EmailText != null) {
				EmailText.Dispose ();
				EmailText = null;
			}

			if (PasswordText != null) {
				PasswordText.Dispose ();
				PasswordText = null;
			}

			if (ConfirmPasswordText != null) {
				ConfirmPasswordText.Dispose ();
				ConfirmPasswordText = null;
			}

			if (NickeNameText != null) {
				NickeNameText.Dispose ();
				NickeNameText = null;
			}

			if (SubmitBtton != null) {
				SubmitBtton.Dispose ();
				SubmitBtton = null;
			}
		}
	}
}
