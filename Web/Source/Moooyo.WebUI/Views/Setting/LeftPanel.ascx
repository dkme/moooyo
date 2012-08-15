<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>

<div class="Set_menu">
	<ul>
		<li id="personinfo"><a href="/Setting/PersonInfo">个人信息<em class="mo1"></em></a></li>
		<li id="uploadface"><a href="/Setting/UploadFace">上传头像<em class="mo2"></em></a></li>
        <li id="skin"><a href="/Setting/Skin">个性设置<em class="mo3"></em></a></li>
		<li id="changepassword"><a href="/Setting/ChangePassword">修改密码<em class="mo4" ></em></a></li>
		<li id="privacy"><a href="/Setting/Privacy">隐私设置<em class="mo5"></em></a></li>
		<li id="accountbind"><a href="/Setting/AccountBind" >绑定设置<em class="mo6"></em></a></li>
		<li id="invite"><a href="/Setting/Invite">邀请好友<em class="mo7"></em></a></li>
		<li id="setlocation"><a href="/Setting/SetLocation">位置设置<em class="mo8"></em></a></li>
		<li id="authentica"><a href="/Setting/Authentica">视频认证<em class="mo9"></em></a></li>
	</ul>	
</div>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        var url = location.href.toLowerCase();
        $("div.Set_menu ul li").each(function () {
            $("div.Set_menu ul li a").removeClass('on');
           
        });
        $("div.Set_menu ul li").each(function () {
            if (url.indexOf(this.id.toLowerCase()) > 0) {
                $("div.Set_menu ul li#" + this.id.toLowerCase() + " a").addClass('on');

                var emClass = $("div.Set_menu ul li#" + this.id.toLowerCase() + " a em");
                if (emClass.attr("class") == "mo1") {
                    emClass.addClass('no1');
                }
                else if (emClass.attr("class") == "mo2") {
                    emClass.addClass('no2');
                }
                else if (emClass.attr("class") == "mo3") {
                    emClass.addClass('no3');
                }
                else if (emClass.attr("class") == "mo4") {
                    emClass.addClass('no4');
                }
                else if (emClass.attr("class") == "mo5") {
                    emClass.addClass('no5');
                }
                else if (emClass.attr("class") == "mo6") {
                    emClass.addClass('no6');
                }
                else if (emClass.attr("class") == "mo7") {
                    emClass.addClass('no7');
                }
                else if (emClass.attr("class") == "mo8") {
                    emClass.addClass('no8');
                }
                else {
                    emClass.addClass('no9');
                }

            }
        });
    });
</script>