
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

		private UIImage AGREEMENTNO = new UIImage("UI/Image/Agreement/AgreementNo.png");
		private UIImage AGREEMENTYES = new UIImage("UI/Image/Agreement/AgreementYes.png");
		private UIImage SUBMITIMAGEOUT = new UIImage("UI/Image/Reg/SubmitImageOut.png");
		private UIImage SUBMITIMAGEIN = new UIImage("UI/Image/Reg/SubmitImageIn.png");

		private bool AGREEMENTCHECKED = true;

		private string FONTFAMILY = "STHeitiSC-Medium";

		public RegController () : base ("RegController", null)
		{
		}
		
		public override void ViewDidLoad ()
		{
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

			ShowAgreement.TouchUpInside += (sender, e) => 
			{
				UsageAgreement.ShowAgreement(this);
			};

			SubmitButton.SetImage(SUBMITIMAGEOUT, UIControlState.Normal);
			UILabel submitTitle = new UILabel(new RectangleF(95, 0, 100, 45));
			submitTitle.Text = "注册";
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
					CustomAlert.ShowCustomAlert(CustomAlertType.Wait, "正在提交注册信息", this);
					Reg2Controller reg2 = new Reg2Controller();
					reg2.NavigationItem.SetHidesBackButton(true, true);
					NavigationController.PushViewController(reg2, true);
					CustomAlert.CloseCustomAlert();
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

		public void LoadTextEventsAndSth ()
		{
			EmailText.Text = "john@gmail.com";
			PasswordText.Text = "123456";
			ConfirmText.Text = "123456";
			NickNameText.Text = "John";

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
				CustomAlert.ShowCustomAlert (CustomAlertType.Warning, "请输入正确的邮箱地址", this);
				return false;
			}
			if (password.Trim ().Length < 6) {
				CustomAlert.ShowCustomAlert (CustomAlertType.Warning, "您输入的密码太短了", this);
				return false;
			}
			if (!passwordmatch) {
				CustomAlert.ShowCustomAlert (CustomAlertType.Warning, "请输入正确的密码", this);
				return false;
			}
			if (confirm.Trim () != password.Trim ()) {
				CustomAlert.ShowCustomAlert (CustomAlertType.Warning, "两次输入的密码不一致", this);
				return false;
			}
			if (nickname.Trim () == "") {
				CustomAlert.ShowCustomAlert (CustomAlertType.Warning, "请输入您的昵称", this);
				return false;
			}
			if (!AGREEMENTCHECKED) {
				CustomAlert.ShowCustomAlert (CustomAlertType.Warning, "您必须同意使用协议才能注册", this);
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

		public override void ViewWillAppear (bool animated)
		{
			LoadNavigation.LoadNa(this, "加入米柚");
			base.ViewWillAppear (animated);
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

