using System;
using System.Drawing;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Moooyo.App.Iphone
{
	public class UsageAgreement
	{
		private static UIImage AGREEMENTBACKIMAGE = new UIImage("UI/Image/Agreement/AgreementBackImage.png");
		private static UIImage AGREEMENTBUTTONIN = new UIImage("UI/Image/Agreement/AgreementButtonIn.png");
		private static UIImage AGREEMENTBUTTONOUT = new UIImage("UI/Image/Agreement/AgreementButtonOut.png");
		private static string FONTFAMILY = "STHeitiSC-Medium";
		private static string AgreementMessage = ""
			+"&nbsp;&nbsp;&nbsp;&nbsp;米柚网（以下简称“米柚”）根据以下服务条款为您提供服务。这些条款可由米柚随时更新，且毋须另行通知。米柚使用协议（以下简称“使用协议”）一旦发生变动，米柚将在网页上公布修改内容。修改后的使用协议一旦在网页上公布即有效代替原来的使用协议。此外，当您使用米柚特殊服务时，您和米柚应遵守米柚随时公布的与该服务相关的指引和规则。前述所有的指引和规则，均构成本使用协议的一部分。</br></br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;您在使用米柚提供的各项服务之前，应仔细阅读本使用协议。如您不同意本使用协议及/或随时对其的修改，请您立即停止使用米柚网所提供的全部服务；您一旦使用米柚服务，即视为您已了解并完全同意本使用协议各项内容，包括米柚对使用协议随时所做的任何修改，并成为米柚用户（以下简称“用户”）。</br></br>"
			+"① 服务说明</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;米柚目前向用户提供如下服务：发布照片、心情、号召等，以及对其他用户分享的精彩内容进行评论；加入或退出兴趣群组；在小组中发言或评论；在米柚访谈中回答问题。除非本使用协议另有其它明示规定，增加或强化目前本服务的任何新功能，包括所推出的新产品，均受到本使用协议之规范。您了解并同意，本服务仅依其当前所呈现的状况提供，对于任何用户信息或个人化设定之时效、删除、传递错误、未予储存或其它任何问题，米柚均不承担任何责任。米柚保留不经事先通知为维修保养、升级或其它目的暂停本服务任何部分的权利。</br></br>"
			+"遵守法律</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;您同意遵守中华人民共和国相关法律法规的所有规定，并对以任何方式使用您的密码和您的帐号使用本服务的任何行为及其结果承担全部责任。如您的行为违反国家法律和法规的任何规定，有可能构成犯罪的，将被追究刑事责任，并由您承担全部法律责任。</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;同时如果米柚有理由认为您的任何行为，包括但不限于您的任何言论和其它行为违反或可能违反国家法律和法规的任何规定，米柚可在任何时候不经任何事先通知终止向您提供服务。</br></br>"
			+"您的注册义务</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;为了能使用本服务，您同意以下事项：依本服务注册提示请您填写正确的注册邮箱、密码和昵称，并确保今后更新的登陆邮箱、昵称、头像等资料的有效性和合法性。若您提供任何违法、不道德或米柚认为不适合在米柚上展示的资料；或者米柚有理由怀疑你的资料属于程序或恶意操作，米柚有权暂停或终止您的帐号，并拒绝您于现在和未来使用本服务之全部或任何部分。</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;米柚无须对任何用户的任何登记资料承担任何责任，包括但不限于鉴别、核实任何登记资料的真实性、正确性、完整性、适用性及/或是否为最新资料的责任。</br></br>"
			+"用户帐号、密码及安全</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;完成本服务的注册程序并成功注册之后，您可使用您的Email和密码，登陆到您在米柚的帐号（下称“帐号”）。保护您的帐号安全，是您的责任。</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;您应对所有使用您的密码及帐号的活动负完全的责任。您同意：</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.您的米柚帐号遭到未获授权的使用，或者发生其它任何安全问题时，您将立即通知米柚；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.如果您未保管好自己的帐号和密码，因此而产生的任何损失或损害，米柚无法也不承担任何责任；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.每个用户都要对其帐号中的所有行为和事件负全责。如果您未保管好自己的帐号和密码而对您、米柚或第三方造成的损害，您将负全部责任。</br></br>"
			+"米柚隐私权政策</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;您提供的登记资料及米柚保留的有关您的若干其它资料将受到中国有关隐私的法律和本公司《隐私声明》 之规范。</br></br>"
			+"提供者之责任</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;根据有关法律法规，米柚在此郑重提请您注意，任何经由本服务而发布、上传的文字、资讯、资料、音乐、照片、图形、视讯、信息或其它资料（以下简称“内容”），无论系公开还是私下传送，均由内容提供者承担责任。米柚仅为用户提供内容存储空间，无法控制经由本服务传送之内容，因此不保证内容的正确性、完整性或品质。您已预知使用本服务时，可能会接触到令人不快、不适当或令人厌恶之内容。在任何情况下，米柚均不为任何内容负责，但米柚有权依法停止传输任何前述内容并采取相应行动，包括但不限于暂停用户使用本服务的全部或部分，保存有关记录，并向有关机关报告。</br></br>"
			+"用户行为</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;用户同意将不会利用本服务进行任何违法或不正当的活动，包括但不限于下列行为∶</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.发布或以其它方式传送含有下列内容之一的信息：</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•反对宪法所确定的基本原则的；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•危害国家安全，泄露国家秘密，颠覆国家政权，破坏国家统一的；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•损害国家荣誉和利益的；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•煽动民族仇恨、民族歧视、破坏民族团结的；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•破坏国家宗教政策，宣扬邪教和封建迷信的；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•散布谣言，扰乱社会秩序，破坏社会稳定的；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•散布淫秽、色情、赌博、暴力、凶杀、恐怖或者教唆犯罪的；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•侮辱或者诽谤他人，侵害他人合法权利的；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•含有虚假、诈骗、有害、胁迫、侵害他人隐私、骚扰、侵害、中伤、粗俗、猥亵、或其它道德上令人反感的内容；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•含有中国法律、法规、规章、条例以及任何具有法律效力之规范所限制或禁止的其它内容的；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•含有米柚认为不适合在米柚展示的内容；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.以任何方式危害他人的合法权益；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.冒充其他任何人或机构，或以虚伪不实的方式陈述或谎称与任何人或机构有关；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.将依据任何法律或合约或法定关系（例如由于雇佣关系和依据保密合约所得知或揭露之内部资料、专属及机密资料）知悉但无权传送之任何内容加以发布、发送电子邮件或以其它方式传送；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.将侵害他人著作权、专利权、商标权、商业秘密、或其它专属权利（以下简称“专属权利”）之内容加以发布或以其它方式传送；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.将任何广告信函、促销资料、“垃圾邮件”、““滥发信件”、“连锁信件”、“直销”或其它任何形式的劝诱资料加以发布、发送或以其它方式传送；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.将设计目的在于干扰、破坏或限制任何计算机软件、硬件或通讯设备功能之计算机病毒（包括但不限于木马程序（trojan horses）、蠕虫（worms）、定时炸弹、删除蝇（cancelbots）（以下简称“病毒”）或其它计算机代码、档案和程序之任何资料，加以发布、发送或以其它方式传送；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.干扰或破坏本服务或与本服务相连线之服务器和网络，或违反任何关于本服务连线网络之规定、程序、政策或规范；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.跟踪、人肉搜索或以其它方式骚扰他人；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.故意或非故意地违反任何适用的当地、国家法律，以及任何具有法律效力的规则；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;11.未经合法授权而截获、篡改、收集、储存或删除他人个人信息、站内邮件或其它数据资料，或将获知的此类资料用于任何非法或不正当目的。</br></br>"
			+"您已认可米柚未对用户的使用行为进行全面控制，您使用任何内容时，包括依赖前述内容之正确性、完整性或实用性时，您同意将自行加以判断并承担所有风险，而不依赖于米柚。但米柚依其自行之考虑，拒绝和删除可经由本服务提供之违反本条款的或其它引起米柚反感的任何内容。</br></br>"
			+"您了解并同意，米柚依据法律法规的要求，或基于诚信为了以下目的或在合理必要范围内，认定必须将内容加以保存或揭露时，得加以保存或揭露：</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;1.遵守法律程序；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;2.执行本使用协议；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;3.回应任何第三人提出的权利主张；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;4.保护米柚、其用户及公众之权利、财产或个人安全；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;5.其它米柚认为有必要的情况。</br></br>"
			+"国际使用之特别警告</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;您已了解国际互联网的无国界性，同意遵守当地所有关于网上行为及内容之法律法规。您特别同意遵守有关从中国或您所在国家或地区输出信息之传输的所有适用法律法规。</br></br>"
			+"在米柚发布的公开信息</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;1.在本使用协议中，“本服务公开使用区域”系指一般公众可以使用的区域；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;2.用户在米柚上传或发布的内容，用户应保证其为著作权人或已取得合法授权，并且该内容不会侵犯任何第三方的合法权益，用户同意授予米柚所有上述内容在全球范围内的免费、不可撤销的、永久的、可再许可或转让的独家使用权许可，据该许可米柚将有权以展示、推广及其他不为我法律所禁止的方式使用前述内容。</br></br>"
			+"赔偿</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;由于您通过本服务提供、发布或传送之内容、您与本服务连线、您违反本使用协议、或您侵害他人任何权利因而衍生或导致任何第三人提出任何索赔或请求，包括合理的律师费，您同意赔偿米柚及其子公司、关联企业、高级职员、代理人、品牌共有人或其它合作伙伴及员工，并使其免受损害，并承担由此引发的全部法律责任。</br></br>"
			+"关于使用及储存之一般措施</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;您同意不对本服务任何部分或本服务之使用或获得，进行复制、拷贝、出售、转售或用于任何其它商业目的。</br></br>"
			+"关于使用及储存之一般措施</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;您承认关于本服务的使用米柚有权制订一般措施及限制，包含但不限于本服务将保留所发布内容或其它发布内容之最长期间，以及一定期间内您使用本服务之次数上限（及每次使用时间之上限）。通过本服务发布或传送之任何信息、通讯资料和其它内容，如被删除或未予储存，您同意米柚毋须承担任何责任。您也同意，米柚有权依其自行之考虑，不论通知与否，随时变更这些一般措施及限制。</br></br>"
			+"服务之修改</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;米柚有权于任何时间暂时或永久修改或终止本服务（或其任何部分），且无论通知与否。您同意对于本服务所作的任何修改、暂停或终止，米柚对您和任何第三人均无需承担任何责任。</br></br>"
			+"终止服务</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;您同意米柚基于其自行之考虑，因任何理由，包含但不限于缺乏使用，或米柚认为您已经违反本使用协议的文字及精神，终止您的帐号或本服务之使用（或服务之任何部分），并将您在本服务内任何内容加以移除并删除。您同意依本使用协议任何规定提供之本服务，无需进行事先通知即可中断或终止，您承认并同意，米柚可立即关闭或删除您的帐号及您帐号中所有相关信息及文件，及/或禁止继续使用前述文件或本服务。此外，您同意若本服务之使用被中断或终止或您的帐号及相关信息和文件被关闭或删除，米柚对您或任何第三人均不承担任何责任。</br></br>"
			+"与广告商及其他第三方进行交易</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;您通过本网站与广告商及其他第三方进行任何形式的通讯或商业往来，或参与促销活动，包含相关商品或服务之付款及交付，以及达成的其它任何相关条款、条件、保证或声明，完全为您与广告商及其他第三方之间之行为。您因前述任何交易或前述广告商及其他第三方而遭受的任何性质的损失或损害，米柚对此不负任何法律责任。</br></br>"
			+"米柚之专属权利</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;您了解并同意，本服务及本服务所使用之相关软件（以下简称“软件”）含有受到相关知识产权及其它法律保护之专有保密资料。您也了解并同意，经由本服务或广告商向您呈现之赞助广告或信息所包含之内容，亦受到著作权、商标权、服务商标、专利权或其它专属权利之法律保护。未经米柚或广告商明示授权，您不得修改、出租、出借、出售、散布本服务或软件之任何部份或全部，或据以制作衍生著作，或使用擅自修改后的软件，包括但不限于为了未经授权而使用本服务之目的。米柚仅授予您个人、不可移转及非专属之使用权，使您得于单机计算机使用其软件之目的码，但您不得（并不得允许任何第三人）复制、修改、创作衍生著作、进行还原工程、反向组译，或以其它方式发现原始码，或出售、转让、再授权或提供软件设定担保，或以其它方式移转软件之任何权利。您同意将通过由米柚所提供的界面而非任何其它途径使用本服务。</br></br>"
			+"担保与保证</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;您明确了解并同意∶</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.本使用协议的任何规定不会免除米柚对造成您人身伤害的、或因故意或重大过失造成您财产损失的任何责任；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.您使用本服务之风险由您个人负担。本服务系依“现状”及“现有”基础提供。米柚对本服务不提供任何明示或默示的担保或保证，包含但不限于商业适售性、特定目的之适用性及未侵害他人权利等担保或保证；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.任何第三方在本服务中所作之声明或行为；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.与本服务相关的其它事宜，但本使用协议有明确规定的除外；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.与本服务相关的其它事宜，但本使用协议有明确规定的除外；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.第三方以任何方式发布或投递欺诈性信息，或诱导用户受到经济损失，米柚不承担任何责任。</br></br>"
			+"米柚商标信息</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;米柚网、米柚以及其它米柚注册商标、标志及产品、服务名称，均为米柚公司之商标（以下简称“米柚标记”）。未经米柚事先书面同意，您同意不将米柚标记以任何方式展示或使用或作其它处理，或表示您有权展示、使用或另行处理米柚标记。</br></br>"
			+"用户专属权利</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;米柚尊重他人知识产权，呼吁用户也要同样尊重知识产权。</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;米柚之服务与资料是基于“现状”提供，而且米柚明确地表示拒绝对于“服务”、“资料”或“产品”给予任何明示或暗示之保证，包括但不限于，得为商业使用或适合于特定目的之保证。米柚对于因“服务”、“资料”或“产品”所生之任何直接、间接、附带的或因此而导致之衍生性损失概不负责。</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;如果您对他人的知识产权造成了侵害，米柚将依国家法律法规的规定，或在适当的情形下，将依其服务条款或其相关规范性规定，删除特定内容或以至终止您对账户的使用。</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;米柚尊重他人的任何权利（包括知识产权），同时也要求我们的使用者也尊重他人之权利。米柚在适当情况下，得自行决定终止侵害或违反他人权利之使用者的帐号。</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;若您认为您的作品的著作权遭到侵害或您的知识产权被侵犯，根据《信息网络传播权保护条例》的规定，您需及时向米柚联系并提供详实的举证材料。或请到中华人民共和国国家版权局下载《要求删除或断开链接侵权网络内容的通知》（下称“删除通知”）的示范格式，如果您不明白“删除通知”的内容，请登录中华人民共和国国家版权局查看《要求删除或断开链接侵权网络内容的通知》填写说明。</br></br>"
			+"一般条款</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;1.本使用协议、社区指导原则 和免责声明 构成您与米柚之全部协议，并规范您对于本服务之使用行为。在您使用相关服务、使用第三方提供的内容或软件时，亦应遵从所适用之附加条款及条件；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;2.本使用协议及您与米柚之关系，均受到中华人民共和国法律管辖。您与米柚就本服务、本使用协议或其它有关事项发生的争议，应首先友好协商解决，协商不成时应提请中国国际经济贸易仲裁委员会仲裁，仲裁裁决是终局性的，对双方均有约束力；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;3.米柚未行使或执行本使用协议任何权利或规定，不构成对前述权利或权利之放弃；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;4.倘本使用协议之任何规定因与中华人民共和国法律抵触而无效，您依然同意应依照法律，努力使该规定所反映之当事人意向具备效力，且本使用协议其它规定仍应具有完整的效力及效果；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;5.本使用协议之标题仅供方便而设，不具任何法律或契约效果；</br>"
			+"&nbsp;&nbsp;&nbsp;&nbsp;6.米柚对本使用协议享有最终解释权。</br>";
		private static string html = ""
				+ "<html>"
				+ "<body style=\"font-size:14px; font-family:Hiragino Sans GB; color:#646464; background-color:#fafafa; padding:0px; margin:0px\">"
				+ AgreementMessage
				+ "</body>"
				+ "</html>";

		private static UIView maskView;
		private static UIView agreementView;

		public static void ShowAgreement (UIViewController mainView)
		{
			float width = mainView.View.Frame.Width;
			float height = mainView.View.Frame.Height + mainView.NavigationController.NavigationBar.Frame.Height + 20;
			maskView = Mask.GetMask(width, height);
			agreementView = GetAgreementView(width, height);
			mainView.NavigationController.Add(maskView);
			mainView.NavigationController.Add(agreementView);
		}

		public static void CloseAgreement ()
		{
			maskView.RemoveFromSuperview();
			agreementView.RemoveFromSuperview();
		}

		public static UIView GetAgreementView(float viewWidth, float viewHeight)
		{
			UIView View = new UIView(new RectangleF((viewWidth - 290) / 2, 25, 290, 450));
			View.BackgroundColor = UIColor.Clear;

			UIImageView backImageView = new UIImageView(new RectangleF(0, 0, 290, 450));
			backImageView.Image = AGREEMENTBACKIMAGE;
			backImageView.BackgroundColor = UIColor.Clear;

			UILabel titleLable = new UILabel(new RectangleF(20, 12, 100, 20));
			titleLable.TextAlignment = UITextAlignment.Left;
			titleLable.TextColor = UIColor.FromRGB(200, 20, 30);
			titleLable.Font = UIFont.FromName(FONTFAMILY, 16);
			titleLable.BackgroundColor = UIColor.Clear;
			titleLable.Text = "使用协议";

			UIWebView textView = new UIWebView(new RectangleF(22, 50, 247, 330));
			textView.LoadHtmlString(html, new NSUrl(NSBundle.MainBundle.BundlePath, true));
			textView.BackgroundColor = UIColor.FromRGB(250, 250, 250);

			UIButton button = new UIButton(new RectangleF(15, 385 , 257, 50));
			button.SetImage(AGREEMENTBUTTONOUT, UIControlState.Normal);
			button.AdjustsImageWhenHighlighted = false;
			UILabel buttonTitle = new UILabel(new RectangleF(100, 0, 60, 50));
			buttonTitle.Text = "确定";
			buttonTitle.Font = UIFont.FromName(FONTFAMILY, 18);
			buttonTitle.TextColor = UIColor.FromRGB(255, 255, 255);
			buttonTitle.BackgroundColor = UIColor.Clear;
			buttonTitle.TextAlignment = UITextAlignment.Center;
			button.Add(buttonTitle);
			button.TouchUpInside += (sender, e) => 
			{
				button.SetImage(AGREEMENTBUTTONIN, UIControlState.Normal);
				CloseAgreement();
			};
			button.TouchDown += (sender, e) => 
			{
				button.SetImage(AGREEMENTBUTTONIN, UIControlState.Normal);
			};
			button.TouchDragInside += (sender, e) => 
			{
				button.SetImage(AGREEMENTBUTTONOUT, UIControlState.Normal);
			};

			View.Add(backImageView);
			View.Add(titleLable);
			View.Add(textView);
			View.Add(button);

			return View;
		}
	}
}

