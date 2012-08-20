
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Moooyo.App.Iphone
{
	public class TextEventsAndSth
	{
		private static UIImage OUTTEXTBORDERIMAGE = new UIImage("UI/Image/CustomTextFile/OutTextToBorder.png");
		private static UIImage INTEXTBORDERIMAGE = new UIImage("UI/Image/CustomTextFile/InTextToBorder.png");
		private static UIImage TEXTCLOSEIMAGE = new UIImage("UI/Image/CustomTextFile/TextClose.png");

		private static float textViewTop;
		private static float textViewHeight;
		private static float keyBoardHeight;

		public static NSObject ShowNotification, HideNotification;
		
		public static void LoadTextEventsAndSth (List<UIImageView> views, UIImageView textView, List<UITextField> texts, UITextField text, List<UIButton> closes, UIButton textClose, UIViewController mainView)
		{
			foreach(UIImageView view in views)
			{
				view.BackgroundColor = UIColor.Clear;
				view.Image = OUTTEXTBORDERIMAGE;
			}
			foreach(UIButton close in closes)
			{
				close.SetImage(TEXTCLOSEIMAGE, UIControlState.Normal);
			}
			ShowNotification = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, delegate (NSNotification n) {
				RectangleF kbdrect = UIKeyboard.BoundsFromNotification(n);
				keyBoardHeight = kbdrect.Height;
				RectangleF frame = mainView.View.Frame;
				float textBottom = frame.Height - keyBoardHeight - textViewHeight;
				if(textViewTop >= textBottom)
				{
					frame.Height += textViewTop - textBottom;
					frame.Y -= textViewTop - textBottom;
					mainView.View.Frame = frame;
				}
			});
			HideNotification = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, delegate (NSNotification n) {
				RectangleF kbdrect = UIKeyboard.BoundsFromNotification(n);
				keyBoardHeight = kbdrect.Height;
				RectangleF frame = mainView.View.Frame;
				frame.Height += frame.Y;
				frame.Y = 0;
				mainView.View.Frame = frame;
			});
			
			text.TouchDown += (sender, e) => 
			{
				textViewTop = textView.Frame.Y;
				textViewHeight = textView.Frame.Height;
				RectangleF frame = mainView.View.Frame;
				float textBottom = frame.Height - keyBoardHeight - textViewHeight;
				if(textViewTop <= textBottom)
				{
					frame.Height += frame.Y;
					frame.Y = 0;
					mainView.View.Frame = frame;
				}
				else
				{
					frame.Height += textViewTop - textBottom;
					frame.Y -= textViewTop - textBottom;
					mainView.View.Frame = frame;
				}
				foreach(UIImageView view in views)
				{
					view.Image = OUTTEXTBORDERIMAGE;
				}
				foreach(UIButton close in closes)
				{
					close.Hidden = true;
				}
				textView.Image = INTEXTBORDERIMAGE;
				if (text.Text.Trim () != "") 
				{
					textClose.Hidden = false;
				}
			};
			text.EditingChanged += (sender, e) => 
			{
				if(text.Text.Trim() != "")
				{
					textClose.Hidden = false;
				}
				else
				{
					textClose.Hidden = true;
				}
			};
			textClose.TouchUpInside += (sender, e) => 
			{
				text.Text = "";
				textClose.Hidden = true;
			};
			text.ShouldReturn = (s) =>
			{
				text.ResignFirstResponder();
				textView.Image = OUTTEXTBORDERIMAGE;
				textClose.Hidden = true;
				return true;
			};
		}

	}
}
