
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
		public static UIImage OUTTEXTBORDERIMAGE = new UIImage("UI/Image/Reg/OutTextToBorder.png");
		public static UIImage INTEXTBORDERIMAGE = new UIImage("UI/Image/Reg/InTextToBorder.png");
		public static UIImage TEXTCLOSEIMAGE = new UIImage("UI/Image/Reg/TextClose.png");

		public static float textViewTop;
		public static float textViewHeight;
		public static float keyBoardHeight;

		public static NSObject ShowNotification, HideNotification;
		
		public static void LoadTextEventsAndSth (List<UIImageView> views, UIImageView textView, List<UITextField> texts, UITextField text, List<UIButton> closes, UIButton textClose, UIViewController mainView)
		{
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
				textView.Image = TextEventsAndSth.INTEXTBORDERIMAGE;
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
