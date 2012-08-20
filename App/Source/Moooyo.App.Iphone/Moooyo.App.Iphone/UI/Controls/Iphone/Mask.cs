using System;
using System.Drawing;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Moooyo.App.Iphone
{
	public class Mask
	{
		public Mask ()
		{
		}

		public static UIView GetMask(float mainviewwidth, float mainviewheight)
		{
			UIView alertBackView = new UIView();
			alertBackView.Alpha = 0.5f;
			alertBackView.Frame = new RectangleF(0, 0, mainviewwidth, mainviewheight);
			alertBackView.BackgroundColor = UIColor.FromRGB(0, 0, 0);
			return alertBackView;
		}
	}
}

