using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Moooyo.App.Iphone
{
	public class LoadNavigation
	{
		private static UIImage NABACKIMAGE = new UIImage("UI/Image/Navigation/NavigationBackroundImage.png");
		private static UIImage NABARBACKIMAGE = new UIImage("UI/Image/Navigation/BackButtonImage.png");
		private static UIImage NABARCLOSEIMAGE = new UIImage("UI/Image/Navigation/CloseButtonImage.png");
		private static string FONTFAMILY = "STHeitiSC-Medium";

		public LoadNavigation ()
		{
		}

		public static void LoadNa(UIViewController mainview, string titlestr)
		{
			mainview.View.BackgroundColor = UIColor.FromRGB(240, 240, 240);
			
			UIImageView imageView = new UIImageView(NABACKIMAGE);

			UILabel titleLable = new UILabel(new RectangleF(110, 0, 100, 44));
			titleLable.Text = titlestr;
			titleLable.BackgroundColor = UIColor.Clear;
			titleLable.TextAlignment = UITextAlignment.Center;
			titleLable.TextColor = UIColor.FromRGB(255, 255, 255);
			titleLable.Font = UIFont.FromName(FONTFAMILY, 16f);

			UIButton backButton = new UIButton(new RectangleF(0, 0, 44, 44));
			backButton.SetBackgroundImage(NABARBACKIMAGE, UIControlState.Normal);
			backButton.BackgroundColor = UIColor.Clear;
			backButton.TouchUpInside += (sender, e) => 
			{
				mainview.NavigationController.PopViewControllerAnimated(true);
			};

			UIButton closeButton = new UIButton(new RectangleF(276, 0, 44, 44));
			closeButton.SetBackgroundImage(NABARCLOSEIMAGE, UIControlState.Normal);
			closeButton.BackgroundColor = UIColor.Clear;

			mainview.NavigationController.NavigationBar.Add(imageView);
			mainview.NavigationController.NavigationBar.Add(titleLable);
			mainview.NavigationController.NavigationBar.Add(backButton);
			mainview.NavigationController.NavigationBar.Add(closeButton);
		}
	}
}

