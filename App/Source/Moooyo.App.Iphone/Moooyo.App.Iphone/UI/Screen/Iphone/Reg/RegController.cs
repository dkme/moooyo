
using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Moooyo.App.Iphone
{
	public partial class RegController : UIViewController
	{

		private UIImage NABACKIMAGE = new UIImage("UI/Image/Reg/NavigationBackroundImage.png");
		private UIImage NABARBACKIMAGE = new UIImage("UI/Image/Reg/BackButtonImage.png");
		private UIImage NABARCLOSEIMAGE = new UIImage("UI/Image/Reg/CloseButtonImage.png");
		private UIImage OUTTEXTBORDERIMAGE = new UIImage("UI/Image/Reg/OutTextToBorder.png");
		private UIImage INTEXTBORDERIMAGE = new UIImage("UI/Image/Reg/InTextToBorder.png");
		private UIImage TEXTCLOSEIMAGE = new UIImage("UI/Image/Reg/TextClose.png");
		private UIImage AGREEMENTNO = new UIImage("UI/Image/Reg/AgreementNo.png");
		private UIImage AGREEMENTYES = new UIImage("UI/Image/Reg/AgreementYes.png");
		private UIImage SUBMITBACKIMAGE = new UIImage("UI/Image/Reg/SubmitBackImage.png");

		private bool AGREEMENTCHECKED = true;

		private string FONTFAMILY = "Helvetica";

		private float textViewOffsetTop;
		private float textViewOffsetHeight;

		public RegController () : base ("RegController", null)
		{
		}
		
		public override void ViewDidLoad ()
		{
			this.Title = "加入米柚";
			this.View.BackgroundColor = UIColor.FromRGB(240, 240, 240);

			UIImageView imageView = new UIImageView(NABACKIMAGE);

			UILabel titleLable = new UILabel(new RectangleF(110, 0, 100, 44));
			titleLable.Text = "加入米柚";
			titleLable.BackgroundColor = UIColor.Clear;
			titleLable.TextAlignment = UITextAlignment.Center;
			titleLable.TextColor = UIColor.FromRGB(255, 255, 255);
			titleLable.Font = UIFont.FromName(FONTFAMILY, 18);

			UIButton backButton = new UIButton(new RectangleF(0, 0, 44, 44));
			backButton.SetBackgroundImage(NABARBACKIMAGE, UIControlState.Normal);
			backButton.BackgroundColor = UIColor.Clear;

			UIButton closeButton = new UIButton(new RectangleF(276, 0, 44, 44));
			closeButton.SetBackgroundImage(NABARCLOSEIMAGE, UIControlState.Normal);
			closeButton.BackgroundColor = UIColor.Clear;

			NavigationController.NavigationBar.AddSubview(imageView);
			NavigationController.NavigationBar.Add(titleLable);
			NavigationController.NavigationBar.Add(backButton);
			NavigationController.NavigationBar.Add(closeButton);

			EmailView.BackgroundColor = PasswordView.BackgroundColor = ConfirmView.BackgroundColor = NickNameView.BackgroundColor = UIColor.Clear;

			EmailView.Image = PasswordView.Image = ConfirmView.Image = NickNameView.Image = OUTTEXTBORDERIMAGE;

			EmailClose.SetImage(TEXTCLOSEIMAGE, UIControlState.Normal);
			PasswordClose.SetImage(TEXTCLOSEIMAGE, UIControlState.Normal);
			ConfirmClose.SetImage(TEXTCLOSEIMAGE, UIControlState.Normal);
			NickNameClose.SetImage(TEXTCLOSEIMAGE, UIControlState.Normal);

			CheckButton.SetImage(AGREEMENTYES, UIControlState.Normal);
			CheckButton.TouchUpInside += (sender, e) => 
			{
				if(AGREEMENTCHECKED)
				{
					AGREEMENTCHECKED = false;
					CheckButton.SetImage(AGREEMENTNO, UIControlState.Normal);
				}
				else
				{
					AGREEMENTCHECKED = true;
					CheckButton.SetImage(AGREEMENTYES, UIControlState.Normal);
				}
			};

			SubminButton.SetImage(SUBMITBACKIMAGE, UIControlState.Normal);
			UILabel submitTitle = new UILabel(new RectangleF(90, 0, 100, 40));
			submitTitle.Text = "注册";
			submitTitle.TextColor = UIColor.FromRGB(255, 255, 255);
			submitTitle.TextAlignment = UITextAlignment.Center;
			submitTitle.Font = UIFont.FromName(FONTFAMILY, 16);
			submitTitle.BackgroundColor = UIColor.Clear;
			SubminButton.Add(submitTitle);

			LoadTextEventsAndSth();

			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, delegate (NSNotification n) {
				RectangleF kbdrect = UIKeyboard.BoundsFromNotification(n);
				RectangleF frame = this.View.Frame;
				if(textViewOffsetTop >= frame.Height - kbdrect.Height - textViewOffsetHeight){
					frame.Y -= textViewOffsetTop - (frame.Height - kbdrect.Height - textViewOffsetHeight);
					frame.Height += textViewOffsetTop - (frame.Height - kbdrect.Height - textViewOffsetHeight);
					this.View.Frame = frame;
				}
			});
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, delegate (NSNotification n) {
				RectangleF kbdrect = UIKeyboard.BoundsFromNotification(n);
				RectangleF frame = this.View.Frame;
				if(frame.Y < 0){
					frame.Height += frame.Y;
					frame.Y = 0;
					this.View.Frame = frame;
				}
			});

			SubminButton.TouchUpInside += (sender, e) => 
			{
				if(Submit())
				{

				}
			};

			base.ViewDidLoad ();
		}

		public void LoadTextEventsAndSth ()
		{
			TextEventsAndSth(EmailView, EmailText, EmailClose);
			TextEventsAndSth(PasswordView, PasswordText, PasswordClose);
			TextEventsAndSth(ConfirmView, ConfirmText, ConfirmClose);
			TextEventsAndSth(NickNameView, NickNameText, NickNameClose);
		}

		public void TextEventsAndSth (UIImageView textView, UITextField text, UIButton textClose)
		{
			text.TouchDown += (sender, e) => 
			{
				textViewOffsetTop = textView.Frame.Y;
				textViewOffsetHeight = textView.Frame.Height;
				TextFocuse(textView, text, textClose);
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
				return true;
			};
		}

		public void TextFocuse (UIImageView focusText, UITextField text, UIButton textClose)
		{
			EmailView.Image = OUTTEXTBORDERIMAGE;
			PasswordView.Image = OUTTEXTBORDERIMAGE;
			ConfirmView.Image = OUTTEXTBORDERIMAGE;
			NickNameView.Image = OUTTEXTBORDERIMAGE;
			focusText.Image = INTEXTBORDERIMAGE;
			EmailClose.Hidden = true;
			PasswordClose.Hidden = true;
			ConfirmClose.Hidden = true;
			NickNameClose.Hidden = true;
			if (text.Text.Trim () != "") {
				textClose.Hidden = false;
			}
		}

		public bool Submit ()
		{
			string mail = EmailText.Text;
			string password = PasswordText.Text;
			string confirm = ConfirmText.Text;
			string nickname = NickNameText.Text;
			Boolean mailmatch = Regex.IsMatch (mail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
			Boolean passwordmatch = Regex.IsMatch (password, @"^[A-Za-z0-9]+$");
			if (mail.Trim () == "") {
				return false;
			}
			if (!mailmatch) {
				return false;
			}
			if (password.Trim () == "") {
				return false;
			}
			if (!passwordmatch) {
				return false;
			}
			if (confirm.Trim () != password.Trim ()) {
				return false;
			}
			if (nickname.Trim () == "") {
				return false;
			}
			if (!AGREEMENTCHECKED) {
				return false;
			}
			return true;
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

