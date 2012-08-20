// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Moooyo.App.Iphone
{
	[Register ("Reg2Controller")]
	partial class Reg2Controller
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView SexTextView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView TheDayTextView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton ManRadioButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton NvRadioButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton TheDayButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel XZButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton SubmitButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SexTextView != null) {
				SexTextView.Dispose ();
				SexTextView = null;
			}

			if (TheDayTextView != null) {
				TheDayTextView.Dispose ();
				TheDayTextView = null;
			}

			if (ManRadioButton != null) {
				ManRadioButton.Dispose ();
				ManRadioButton = null;
			}

			if (NvRadioButton != null) {
				NvRadioButton.Dispose ();
				NvRadioButton = null;
			}

			if (TheDayButton != null) {
				TheDayButton.Dispose ();
				TheDayButton = null;
			}

			if (XZButton != null) {
				XZButton.Dispose ();
				XZButton = null;
			}

			if (SubmitButton != null) {
				SubmitButton.Dispose ();
				SubmitButton = null;
			}
		}
	}
}
