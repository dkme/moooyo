
using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Moooyo.App.Core.Api;
using CBB.ExceptionHelper;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Moooyo.App.Iphone
{
	public partial class RegController : UIViewController
	{

		private UIImage NABACKIMAGE = new UIImage("UI/Image/Comm/NavigationBackroundImage.png");
		private UIImage NABARBACKIMAGE = new UIImage("UI/Image/Comm/BackButtonImage.png");
		private UIImage NABARCLOSEIMAGE = new UIImage("UI/Image/Comm/CloseButtonImage.png");
		private UIImage AGREEMENTNO = new UIImage("UI/Image/Reg/AgreementNo.png");
		private UIImage AGREEMENTYES = new UIImage("UI/Image/Reg/AgreementYes.png");
		private UIImage SUBMITBACKIMAGE = new UIImage("UI/Image/Reg/SubmitBackImage.png");
		private UIImage SUBMITCLICKBACKIMAGE = new UIImage("UI/Image/Reg/SubmitClickBackImage.png");

		private bool AGREEMENTCHECKED = true;

		private string FONTFAMILY = "STHeitiSC-Medium";

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
			titleLable.Font = UIFont.FromName(FONTFAMILY, 16f);

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

			Console.WriteLine(EmailText.Font.FamilyName);

			LoadTextEventsAndSth();

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

			SubmitButton.SetImage(SUBMITBACKIMAGE, UIControlState.Normal);
			UILabel submitTitle = new UILabel(new RectangleF(90, 0, 100, 40));
			submitTitle.Text = "注册";
			submitTitle.TextColor = UIColor.FromRGB(255, 255, 255);
			submitTitle.TextAlignment = UITextAlignment.Center;
			submitTitle.Font = UIFont.FromName(FONTFAMILY, 16);
			submitTitle.BackgroundColor = UIColor.Clear;
			SubmitButton.Add(submitTitle);
			SubmitButton.TouchUpInside += (sender, e) => 
			{
				SubmitButton.SetImage(SUBMITBACKIMAGE, UIControlState.Normal);
				if(Submit())
				{

				}
			};
			SubmitButton.TouchDown += (sender, e) => 
			{
				SubmitButton.SetImage(SUBMITCLICKBACKIMAGE, UIControlState.Normal);
			};
			SubmitButton.TouchDragInside += (sender, e) => 
			{
				SubmitButton.SetImage(SUBMITBACKIMAGE, UIControlState.Normal);
			};

			base.ViewDidLoad ();
		}

		public void LoadTextEventsAndSth ()
		{
			EmailView.BackgroundColor = PasswordView.BackgroundColor = ConfirmView.BackgroundColor = NickNameView.BackgroundColor = UIColor.Clear;

			EmailView.Image = PasswordView.Image = ConfirmView.Image = NickNameView.Image = TextEventsAndSth.OUTTEXTBORDERIMAGE;

			EmailClose.SetImage(TextEventsAndSth.TEXTCLOSEIMAGE, UIControlState.Normal);
			PasswordClose.SetImage(TextEventsAndSth.TEXTCLOSEIMAGE, UIControlState.Normal);
			ConfirmClose.SetImage(TextEventsAndSth.TEXTCLOSEIMAGE, UIControlState.Normal);
			NickNameClose.SetImage(TextEventsAndSth.TEXTCLOSEIMAGE, UIControlState.Normal);

			List<UIImageView> viewList = new List<UIImageView>{ EmailView, PasswordView, ConfirmView, NickNameView };
			List<UITextField> textList = new List<UITextField>{ EmailText, PasswordText, ConfirmText, NickNameText };
			List<UIButton> closeList = new List<UIButton>{ EmailClose, PasswordClose, ConfirmClose, NickNameClose };
			TextEventsAndSth.LoadTextEventsAndSth(viewList, EmailView, textList, EmailText, closeList, EmailClose, this);
			TextEventsAndSth.LoadTextEventsAndSth(viewList, PasswordView, textList, PasswordText, closeList, PasswordClose, this);
			TextEventsAndSth.LoadTextEventsAndSth(viewList, ConfirmView, textList, ConfirmText, closeList, ConfirmClose, this);
			TextEventsAndSth.LoadTextEventsAndSth(viewList, NickNameView, textList, NickNameText, closeList, NickNameClose, this);
		}

		public bool Submit ()
		{
			string mail = EmailText.Text;
			string password = PasswordText.Text;
			string confirm = ConfirmText.Text;
			string nickname = NickNameText.Text;
			Boolean mailmatch = Regex.IsMatch (mail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
			Boolean passwordmatch = Regex.IsMatch (password, @"^[A-Za-z0-9]+$");
			if (!mailmatch) {
				CustomAlert.ShowCustomAlert (CustomAlertType.Error, "请输入正确的邮箱地址", this);
				return false;
			}
			if (password.Trim ().Length < 6) {
				CustomAlert.ShowCustomAlert (CustomAlertType.Error, "您输入的密码太短了", this);
				return false;
			}
			if (!passwordmatch) {
				CustomAlert.ShowCustomAlert (CustomAlertType.Error, "请输入正确的密码", this);
				return false;
			}
			if (confirm.Trim () != password.Trim ()) {
				CustomAlert.ShowCustomAlert (CustomAlertType.Error, "两次输入的密码不一致", this);
				return false;
			}
			if (nickname.Trim () == "") {
				CustomAlert.ShowCustomAlert (CustomAlertType.Error, "请输入您的昵称", this);
				return false;
			}
			if (!AGREEMENTCHECKED) {
				CustomAlert.ShowCustomAlert (CustomAlertType.Error, "您必须同意使用协议才能注册", this);
				return false;
			}
//			Accounts submitController = new Accounts ();
//			OperationResult submitOperation = submitController.CreateStep1 (mail, nickname, password);
//			if (submitOperation.ok) {
//				return true;
//			} else {
//				CustomAlert.ShowCustomAlert (CustomAlertType.Error, submitOperation.err, this);
//				return false;
//			}
			return true;
		}

		public override void ViewDidUnload ()
		{
			NSNotificationCenter.DefaultCenter.RemoveObserver(TextEventsAndSth.ShowNotification);
			NSNotificationCenter.DefaultCenter.RemoveObserver(TextEventsAndSth.HideNotification);

			base.ViewDidUnload ();
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return false;
		}
	}
}

