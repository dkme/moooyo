using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Moooyo.App.Iphone
{
	public class CustomAlert
	{
		public static UIImage ALERTMESSAGEBACK = new UIImage("UI/Image/Comm/AlertMessageBack.png");
		public static UIImage ALERTMESSAGEERROR = new UIImage("UI/Image/Comm/AlertMessageError.png");

		public static string FONTFAMILY = "STHeitiSC-Medium";

		public static void ShowCustomAlert (CustomAlertType type, string message, UIViewController mainView)
		{
			float width = mainView.View.Frame.Width;
			float height = mainView.View.Frame.Height + mainView.NavigationController.NavigationBar.Frame.Height + 20;
			List<UIView> alertViews = CustomAlerts (width, height, type, message);
			foreach (UIView view in alertViews) 
			{
				mainView.NavigationController.View.Add(view);
			}
		}

		public static List<UIView> CustomAlerts (float viewWidth, float viewHeight, CustomAlertType type, string message)
		{
			List<UIView> uiviews = new List<UIView>();

			UIView alertBackView = new UIView();
			alertBackView.Alpha = 0.5f;
			alertBackView.Frame = new RectangleF(0, 0, viewWidth, viewHeight);
			alertBackView.BackgroundColor = UIColor.FromRGB(0, 0, 0);

			float width = 300f;
			float height = 60f;
			float x = (viewWidth - width) / 2;
			float y = (viewHeight - height * 1.5f) / 2;
			UIButton alertButton = new UIButton ();
			alertButton.Frame = new RectangleF (x, y, width, height);
			alertButton.BackgroundColor = UIColor.Clear;
			alertButton.SetImage(ALERTMESSAGEBACK, UIControlState.Normal);
			alertButton.AdjustsImageWhenHighlighted = false;
			alertButton.TouchUpInside += (sender, e) => 
			{
				alertBackView.RemoveFromSuperview();
				alertButton.RemoveFromSuperview();
			};
			UIImageView imageView = new UIImageView(new RectangleF(20, 20, 20, 20));
			switch (type) 
			{
				case CustomAlertType.Error: imageView.Image = ALERTMESSAGEERROR; break;
				case CustomAlertType.OK:break;
			}
			UILabel messageLable = new UILabel();
			messageLable.Text = message;
			messageLable.Font = UIFont.FromName(FONTFAMILY, 15);
			messageLable.TextColor = UIColor.FromRGB(100, 100, 100);
			messageLable.Frame = new RectangleF(50, 10, width - 80, height - 20);
			messageLable.TextAlignment = UITextAlignment.Left;
			messageLable.BackgroundColor = UIColor.Clear;
			alertButton.Add(imageView);
			alertButton.Add(messageLable);
			uiviews.Add(alertBackView);
			uiviews.Add(alertButton);
			return uiviews;
		}
	}
	public enum CustomAlertType
	{
		OK = 0,
		Error = 1
	}
}

