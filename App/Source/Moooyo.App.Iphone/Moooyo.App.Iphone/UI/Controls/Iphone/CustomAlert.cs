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
		private static UIImage ALERTMESSAGEBACK = new UIImage("UI/Image/CustomAlert/AlertMessageBack.png");
		private static UIImage ALERTMESSAGEERROR = new UIImage("UI/Image/CustomAlert/AlertMessageError.png");
		private static UIImage ALERTMESSAGEWARNING = new UIImage("UI/Image/CustomAlert/AlertMessageWarning.png");
		private static UIImage ALERTMESSAGEWAIT = new UIImage("UI/Image/CustomAlert/AlertMessageWait.png");

		private static string FONTFAMILY = "STHeitiSC-Medium";

		private static UIView maskView;
		private static UIView alertButton;

		public static void ShowCustomAlert (CustomAlertType type, string message, UIViewController mainView)
		{
			float width = mainView.View.Frame.Width;
			float height = mainView.View.Frame.Height + mainView.NavigationController.NavigationBar.Frame.Height + 20;
			maskView = Mask.GetMask(width, height);
			alertButton = GetCustomButton(width, height, type, message);
			mainView.NavigationController.View.Add(maskView);
			mainView.NavigationController.View.Add(alertButton);
		}

		public static void CloseCustomAlert ()
		{
			maskView.RemoveFromSuperview();
			alertButton.RemoveFromSuperview();
		}

		public static UIView GetCustomButton (float viewWidth, float viewHeight, CustomAlertType type, string message)
		{
			float width = 300f;
			float height = 60f;
			float x = (viewWidth - width) / 2;
			float y = (viewHeight - height * 1.5f) / 2;

			UIView buttonView = new UIView(new RectangleF (x, y, width, height));
			buttonView.BackgroundColor = UIColor.Clear;

			UIButton alertButton = new UIButton (new RectangleF(0, 0, width, height));
			alertButton.BackgroundColor = UIColor.Clear;
			alertButton.SetImage(ALERTMESSAGEBACK, UIControlState.Normal);
			alertButton.AdjustsImageWhenHighlighted = false;
			alertButton.TouchUpInside += (sender, e) => 
			{
				CloseCustomAlert();
			};
			UIImageView imageView = new UIImageView(new RectangleF(20, 20, 20, 20));
			switch (type) 
			{
				case CustomAlertType.Error: imageView.Image = ALERTMESSAGEERROR; break;
				case CustomAlertType.OK:break;
				case CustomAlertType.Warning:imageView.Image = ALERTMESSAGEWARNING; break;
				case CustomAlertType.Wait:imageView.Image = ALERTMESSAGEWAIT; break;
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

			buttonView.Add(alertButton);

			return buttonView;
		}
	}
	public enum CustomAlertType
	{
		OK = 0,
		Error = 1,
		Warning = 2,
		Wait = 3
	}
}

