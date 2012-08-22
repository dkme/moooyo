using System;
using System.Drawing;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Moooyo.App.Iphone
{
	public class TimePicker
	{
		private static UIImage TIMEBACKIMAGE = new UIImage("UI/Image/TimePicker/TimeBackImage.png");
		private static UIImage BACKBUTTONIMAGEIN = new UIImage("UI/Image/TimePicker/BackButtonImageIn.png");
		private static UIImage BACKBUTTONIMAGEOUT = new UIImage("UI/Image/TimePicker/BackButtonImageOut.png");
		private static UIImage TRUEBUTTONIMAGEIN = new UIImage("UI/Image/TimePicker/TrueButtonImageIn.png");
		private static UIImage TRUEBUTTONIMAGEOUT = new UIImage("UI/Image/TimePicker/TrueButtonImageOut.png");

		private static string FONTFAMILY = "STHeitiSC-Medium";
		private static float TIMEWIDTH = 290;
		private static float TIMEHEIGHT = 165;
		private static float TIMEITEMFRISTWIDTH = 80;
		private static float TIMETIEMOTHERWIDTH = (280 - 80) / 2;
		private static float TIMEBUTTONHEIGHT = 50;
		private static float TIMETEXTHEIGHT = 50;
		private static float BUTTONWIDTH = 160;
		private static float BUTTONHEIGHT = 45;

		private static UIView maskView;
		private static UIView datePicker;
		private static UIView backButton;
		private static UIView trueButton;

		public static float year;
		public static float month;
		public static float day;

		public TimePicker ()
		{
		}

		public static void ShowTimePicker (UIViewController mainView)
		{
			float width = mainView.View.Frame.Width;
			float height = mainView.View.Frame.Height + mainView.NavigationController.NavigationBar.Frame.Height + 20;
			maskView = Mask.GetMask(width, height);
			datePicker = GetTimePicker(width, height);
			backButton = GetBackButton(width, height);
			trueButton = GetTrueButton(width, height);
			mainView.NavigationController.Add(maskView);
			mainView.NavigationController.Add(datePicker);
			mainView.NavigationController.Add(backButton);
			mainView.NavigationController.Add(trueButton);
		}

		public static void CloseTimePiker ()
		{
			maskView.RemoveFromSuperview();
			datePicker.RemoveFromSuperview();
			backButton.RemoveFromSuperview();
			trueButton.RemoveFromSuperview();
		}

		private static UIView GetTimePicker (float viewWidth, float viewHeight)
		{
			year = DateTime.Now.Year;
			month = DateTime.Now.Month;
			day = DateTime.Now.Day;

			UIView view = new UIView(new RectangleF((viewWidth - TIMEWIDTH) / 2, (viewHeight - BUTTONHEIGHT - TIMEHEIGHT) / 2, TIMEWIDTH, TIMEHEIGHT));
			view.BackgroundColor = UIColor.Clear;

			UIImageView imageView = new UIImageView(new RectangleF(0, 0, TIMEWIDTH, TIMEHEIGHT));
			imageView.Image = TIMEBACKIMAGE;

			UIButton yearIncrease = new UIButton(new RectangleF(0, 0, TIMEITEMFRISTWIDTH, TIMEBUTTONHEIGHT));
			yearIncrease.BackgroundColor = UIColor.Clear;
			UIButton yeatText = new UIButton(new RectangleF(0, TIMEBUTTONHEIGHT, TIMEITEMFRISTWIDTH, TIMETEXTHEIGHT));
			yeatText.BackgroundColor = UIColor.Clear;
			yeatText.SetTitle(year.ToString(), UIControlState.Normal);
			yeatText.Font = UIFont.FromName(FONTFAMILY, 20);
			UIButton yearReduced = new UIButton(new RectangleF(0, TIMEBUTTONHEIGHT + TIMETEXTHEIGHT, TIMEITEMFRISTWIDTH, TIMEBUTTONHEIGHT));
			yearReduced.BackgroundColor = UIColor.Clear;

			UIButton monthIncrease = new UIButton(new RectangleF(TIMEITEMFRISTWIDTH, 0, TIMETIEMOTHERWIDTH, TIMEBUTTONHEIGHT));
			monthIncrease.BackgroundColor = UIColor.Clear;
			UIButton monthText = new UIButton(new RectangleF(TIMEITEMFRISTWIDTH, TIMEBUTTONHEIGHT, TIMETIEMOTHERWIDTH, TIMETEXTHEIGHT));
			monthText.BackgroundColor = UIColor.Clear;
			monthText.SetTitle(month.ToString(), UIControlState.Normal);
			monthText.Font = UIFont.FromName(FONTFAMILY, 20);
			UIButton monthReduced = new UIButton(new RectangleF(TIMEITEMFRISTWIDTH, TIMEBUTTONHEIGHT + TIMETEXTHEIGHT, TIMETIEMOTHERWIDTH, TIMEBUTTONHEIGHT));
			monthReduced.BackgroundColor = UIColor.Clear;

			UIButton dayIncrease = new UIButton(new RectangleF(TIMEITEMFRISTWIDTH + TIMETIEMOTHERWIDTH, 0, TIMETIEMOTHERWIDTH, TIMEBUTTONHEIGHT));
			dayIncrease.BackgroundColor = UIColor.Clear;
			UIButton dayText = new UIButton(new RectangleF(TIMEITEMFRISTWIDTH + TIMETIEMOTHERWIDTH, TIMEBUTTONHEIGHT, TIMETIEMOTHERWIDTH, TIMETEXTHEIGHT));
			dayText.BackgroundColor = UIColor.Clear;
			dayText.SetTitle(day.ToString(), UIControlState.Normal);
			dayText.Font = UIFont.FromName(FONTFAMILY, 20);
			UIButton dayReduced = new UIButton(new RectangleF(TIMEITEMFRISTWIDTH + TIMETIEMOTHERWIDTH, TIMEBUTTONHEIGHT + TIMETEXTHEIGHT, TIMETIEMOTHERWIDTH, TIMEBUTTONHEIGHT));
			dayReduced.BackgroundColor = UIColor.Clear;

			imageView.Add(yearIncrease);
			imageView.Add(yearReduced);
			imageView.Add(yeatText);
			imageView.Add(monthIncrease);
			imageView.Add(monthReduced);
			imageView.Add(monthText);
			imageView.Add(dayIncrease);
			imageView.Add(dayReduced);
			imageView.Add(dayText);

			view.Add(imageView);
			return view;
		}

		private static UIView GetBackButton (float viewWidth, float viewHeight)
		{
			UIView view = new UIView(new RectangleF(0, viewHeight - BUTTONHEIGHT, BUTTONWIDTH, BUTTONHEIGHT));
			view.BackgroundColor = UIColor.Clear;
			UIButton button = new UIButton(new RectangleF(0, 0, BUTTONWIDTH, BUTTONHEIGHT));
			button.SetImage(BACKBUTTONIMAGEOUT, UIControlState.Normal);
			button.AdjustsImageWhenDisabled = false;
			button.AdjustsImageWhenHighlighted = false;
			UILabel backTitle = new UILabel(new RectangleF(50, 10, 60, 25));
			backTitle.Text = "返回";
			backTitle.TextColor = UIColor.FromRGB(255, 255, 255);
			backTitle.Font = UIFont.FromName(FONTFAMILY, 18);
			backTitle.TextAlignment = UITextAlignment.Center;
			backTitle.BackgroundColor = UIColor.Clear;
			button.Add(backTitle);
			button.TouchUpInside += (sender, e) => 
			{
				button.SetImage(BACKBUTTONIMAGEOUT, UIControlState.Normal);
				CloseTimePiker();
			};
			button.TouchDown += (sender, e) => 
			{
				button.SetImage(BACKBUTTONIMAGEIN, UIControlState.Normal);
			};
			button.TouchDragInside += (sender, e) => 
			{
				button.SetImage(BACKBUTTONIMAGEOUT, UIControlState.Normal);
			};
			view.Add(button);
			return view;
		}

		private static UIView GetTrueButton (float viewWidth, float viewHeight)
		{
			UIView view = new UIView(new RectangleF(BUTTONWIDTH, viewHeight - BUTTONHEIGHT, BUTTONWIDTH, BUTTONHEIGHT));
			view.BackgroundColor = UIColor.Clear;
			UIButton button = new UIButton(new RectangleF(0, 0, BUTTONWIDTH, BUTTONHEIGHT));
			button.SetImage(TRUEBUTTONIMAGEOUT, UIControlState.Normal);
			button.AdjustsImageWhenDisabled = false;
			button.AdjustsImageWhenHighlighted = false;
			UILabel trueTitle = new UILabel(new RectangleF(50, 10, 60, 25));
			trueTitle.Text = "完成";
			trueTitle.TextColor = UIColor.FromRGB(255, 255, 255);
			trueTitle.Font = UIFont.FromName(FONTFAMILY, 18);
			trueTitle.TextAlignment = UITextAlignment.Center;
			trueTitle.BackgroundColor = UIColor.Clear;
			button.Add(trueTitle);
			button.TouchUpInside += (sender, e) => 
			{
				button.SetImage(TRUEBUTTONIMAGEOUT, UIControlState.Normal);
				CloseTimePiker();
			};
			button.TouchDown += (sender, e) => 
			{
				button.SetImage(TRUEBUTTONIMAGEIN, UIControlState.Normal);
			};
			button.TouchDragInside += (sender, e) => 
			{
				button.SetImage(TRUEBUTTONIMAGEOUT, UIControlState.Normal);
			};
			view.Add(button);
			return view;
		}
	}
}

