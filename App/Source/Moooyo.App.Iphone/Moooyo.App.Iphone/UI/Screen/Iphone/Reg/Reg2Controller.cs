
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Moooyo.App.Iphone
{
	public partial class Reg2Controller : UIViewController
	{
		private UIImage OUTTEXTBORDERIMAGE = new UIImage("UI/Image/CustomTextFile/OutTextToBorder.png");
		private UIImage INTEXTBORDERIMAGE = new UIImage("UI/Image/CustomTextFile/InTextToBorder.png");
		private UIImage INRADIO = new UIImage("UI/Image/Reg/Reg2/InRadio.png");
		private UIImage OUTRADIO = new UIImage("UI/Image/Reg/Reg2/OutRadio.png");
		private UIImage SUBMITIMAGEOUT = new UIImage("UI/Image/Reg/Reg2/SubmitImageOut.png");
		private UIImage SUBMITIMAGEIN = new UIImage("UI/Image/Reg/Reg2/SubmitImageIn.png");

		private string FONTFAMILY = "STHeitiSC-Medium";

		private int sex = 0;

		public Reg2Controller () : base ("Reg2Controller", null)
		{
		}

		public override void ViewDidLoad ()
		{
			SexTextView.Image = TheDayTextView.Image = OUTTEXTBORDERIMAGE;

			ManRadioButton.SetImage(OUTRADIO, UIControlState.Normal);
			NvRadioButton.SetImage(OUTRADIO, UIControlState.Normal);
			ManRadioButton.TouchUpInside += (sender, e) => 
			{
				SexClick(1);
			};
			NvRadioButton.TouchUpInside += (sender, e) => 
			{
				SexClick(2);
			};

			TheDayButton.TouchUpInside += (sender, e) => 
			{
				SexTextView.Image = OUTTEXTBORDERIMAGE;
				TheDayTextView.Image = INTEXTBORDERIMAGE;
				TimePicker.ShowTimePicker(this);
			};

			SubmitButton.SetImage(SUBMITIMAGEOUT, UIControlState.Normal);
			UILabel submitTitle = new UILabel(new RectangleF(95, 0, 100, 45));
			submitTitle.Text = "继续";
			submitTitle.TextColor = UIColor.FromRGB(255, 255, 255);
			submitTitle.TextAlignment = UITextAlignment.Center;
			submitTitle.Font = UIFont.FromName(FONTFAMILY, 18);
			submitTitle.BackgroundColor = UIColor.Clear;
			SubmitButton.Add(submitTitle);
			SubmitButton.TouchUpInside += (sender, e) => 
			{
				SubmitButton.SetImage(SUBMITIMAGEOUT, UIControlState.Normal);
				if(Submit())
				{
//					CustomAlert.ShowCustomAlert(CustomAlertType.Wait, "正在提交注册信息", this);
//					Reg2Controller reg2 = new Reg2Controller();
//					reg2.NavigationItem.SetHidesBackButton(true, true);
//					NavigationController.PushViewController(reg2, true);
//					CustomAlert.CloseCustomAlert();
				}
			};
			SubmitButton.TouchDown += (sender, e) => 
			{
				SubmitButton.SetImage(SUBMITIMAGEIN, UIControlState.Normal);
			};
			SubmitButton.TouchDragInside += (sender, e) => 
			{
				SubmitButton.SetImage(SUBMITIMAGEOUT, UIControlState.Normal);
			};

			base.ViewDidLoad ();
		}

		public bool Submit ()
		{
			if (sex == 0) {
				CustomAlert.ShowCustomAlert(CustomAlertType.Warning, "您好像忘记选择性别了", this);
				return false;		
			}
			return true;
		}
		
		public void SexClick (int index)
		{
			if (sex == 0) {
				CustomAlert.ShowCustomAlert (CustomAlertType.Warning, "注册成功后性别将不可更改", this);
			}
			SexTextView.Image = INTEXTBORDERIMAGE;
			TheDayTextView.Image = OUTTEXTBORDERIMAGE;
			switch (index) {
			case 1:
				ManRadioButton.SetImage(INRADIO, UIControlState.Normal);
				NvRadioButton.SetImage(OUTRADIO, UIControlState.Normal);
				sex = 1;
				break;
			case 2:
				ManRadioButton.SetImage(OUTRADIO, UIControlState.Normal);
				NvRadioButton.SetImage(INRADIO, UIControlState.Normal);
				sex = 2;
				break;
			}
		}
		
		public override void ViewWillAppear (bool animated)
		{
			LoadNavigation.LoadNa(this, "基本信息");
			base.ViewWillAppear (animated);
		}

		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return false;
		}
	}
}

