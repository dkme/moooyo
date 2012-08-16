
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Moooyo.App.Iphone
{
	public partial class RegController : UIViewController
	{
		public RegController () : base ("RegController", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}
		
		public override void ViewDidLoad ()
		{
			this.Title = "加入米柚";
			NavigationController.NavigationBar.TintColor = UIColor.FromRGB(189, 17, 25);
			this.View.BackgroundColor = UIColor.FromRGB(240, 240, 240);
			UIColor textBackgroundColor = new UIColor(250, 250, 250, 1);
			EmailText.BackgroundColor = textBackgroundColor;
			PasswordText.BackgroundColor = textBackgroundColor;
			ConfirmPasswordText.BackgroundColor = textBackgroundColor;
			NickeNameText.BackgroundColor = textBackgroundColor;
			EmailText.TouchDown += (sender, e) => 
			{
				if(EmailText.Text.Trim() == "邮箱")
				{
					EmailText.Text = "";
				}
			};
			EmailText.ShouldReturn = (s) =>
			{
				EmailText.ResignFirstResponder();
				return true;
			};
			PasswordText.TouchDown += (sender, e) => 
			{
				if(PasswordText.Text.Trim() == "密码")
				{
					PasswordText.Text = "";
				}
			};
			PasswordText.ShouldReturn = (s) =>
			{
				PasswordText.ResignFirstResponder();
				return true;
			};
			ConfirmPasswordText.TouchDown += (sender, e) => 
			{
				if(ConfirmPasswordText.Text.Trim() == "确认密码")
				{
					ConfirmPasswordText.Text = "";
				}
			};
			ConfirmPasswordText.ShouldReturn = (s) =>
			{
				ConfirmPasswordText.ResignFirstResponder();
				return true;
			};
			NickeNameText.TouchDown += (sender, e) => 
			{
				if(NickeNameText.Text.Trim() == "昵称")
				{
					NickeNameText.Text = "";
				}
			};
			NickeNameText.ShouldReturn = (s) =>
			{
				NickeNameText.ResignFirstResponder();
				return true;
			};
			base.ViewDidLoad ();
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

