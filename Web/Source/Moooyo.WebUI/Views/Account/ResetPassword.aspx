<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	找回密码 米柚网-单身欢乐季
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box_code" style="display:none;">
    <div class="box_top"><em></em></div>
    <div class="code_content"> <span>邮箱还没有注册</span></div>
    <div class="box_bottom"><em></em></div>
  </div>

  <!-- 内容-->
     <div class="container">
	    <div class="box_demo1 w960" style="background:#fff">
		    <div class="mt10"></div>
            <div style="height:20px;"></div>
		    <div class="Set_title2 admin_b  p40" style="padding-bottom:20px; overflow:hidden;"><span class="pass_pic" ><em></em></span><span class="title_font">找回密码</span> <a class="com_back blue02" href="<%=ViewData["back"] %>">返回</a></div>
            <div class="Set_box1 p40" style="padding:0 40px;">
                <div class="mt10"></div>
                <!-- 输入之前状态-->
		        <div class="admin_box " id="resetPasswordArea">
                <div class="reset_pass">
                <form id="resetPasswordForm" name="resetPasswordForm" method="post" action="/Account/BackAndResetPassword" enctype="multipart/form-data">
                  <span>请输入您注册的邮箱，系统会将重设的密码发送到您的注册邮箱中</span>
                         <div class="moyo_right"><span class="moyo_txt"><input type="text"  class="txtput pwdtxt" id="email"  name="email"  /><b  class="code_b">输入注册邮箱</b> <a class="redlink" href="javascript:;" onclick="backAndResetPassword()">找回密码</a>  <font>或</font><a class="blue01" href="/Account/Login">登录</a></span></div>
                     </form>     
                </div>   
                <div class="reset_msg"><span>还没有 米柚 帐号？ <a  href="/Register/Regist" class="blue01">立即注册</a></span></div>   
                  </div>
                 <!-- 输入之后状态-->
                 <div class="admin_box hidden" id="resetPasswordSuccess">
                <div class="reset_pass">
                  <span class="red-s">恭喜</span>
                  <span class="red-s">你的新密码已经发到<font id="registerEmail"></font>，请到您的邮箱查收。</span>
                          
                </div>   
                <div class="reset_msg"><span><a href="/Account/Login" class="blue01 blod">点击登录</a></span></div>   
                  </div>
			</div>
        </div>
	 </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css">
<link rel="stylesheet" type="text/css" href="/css/fabu.css">
<link rel="stylesheet" type="text/css" href="/css/msg.css">
<style type="text/css">
.Set_title2 span.pass_pic{ width:64px;  height:55px; margin-left:20px; margin-right:30px; padding-top:5px; position:relative; display:block; float:left; }
.Set_title2 span.title_font { font-family:'微软雅黑'; font-size:30px; color:#444;  }
.Set_title2 span.pass_pic em { width:64px; height:36px; position:absolute; left:0; top:15px; background:url(/pics/look_pass.png) no-repeat; display:block;}
.reset_pass ,.reset_msg{ padding:40px 80px; overflow:hidden;}
.reset_pass span{ display:block; text-align:left; font-size:16px; font-family:"微软雅黑"; color:#666; line-height:30px; padding-bottom:15px;}
.reset_pass span.red-s { color:#b30000;}
.reset_msg span { display:block; text-align:left;  font-size:14px; font-family:"微软雅黑"; color:#444; line-height:30px;}
.reset_msg span a.blod { font-size:14px;}
/* 此中间样式是从 内测页面复制过来的 */
.moyo_right span.moyo_txt { margin:20px 0; overflow:hidden; position:relative;}
.moyo_right span.moyo_txt b{ position:absolute; left:8px; top:4px; z-index:2; display:block; height: 30px; line-height: 30px;  color:#666; font-family:"微软雅黑"; font-size:14px; font-weight:normal; }
.moyo_right .moyo_txt input.txtput  {
	width: 195px; height: 30px; line-height: 30px; float:left; _background:#fff; margin-right:8px; padding:3px 0 0 6px; background:#fff url(/pics/border_bg01.png) 0 0 no-repeat;  color:#666; font-family:"微软雅黑"; font-size:14px; border: 1px solid #d4d4d4; }
.moyo_right .moyo_txt input.txtput:hover { border:1px solid #b50203;}
.moyo_right  .moyo_txt a{ display:block; float:left;  font-size:16px;  font-weight:bold; text-align:center; font-family:"微软雅黑"; }
.moyo_right  .moyo_txt a.redlink  { color:#fff;  width:79px;  height:31px; line-height:31px;  background:#b40001; margin-left:8px; border-bottom:#9e0203 3px solid; border-right:#9e0203 3px solid; padding:0 5px; }
.moyo_right  .moyo_txt a.redlink:hover { background:#b20000;}

.box_code  { width:252px; position:absolute; overflow:hidden; z-index:500; }
.code_content { width:252px; overflow:hidden; line-height:30px; text-align:center;  overflow:hidden; background:#ffcc00; color:#444; font-family:"微软雅黑"; font-size:16px;}
.box_code em{ display:block; width:252px;}
.box_top { width:252px; height:22px; overflow:hidden;}
.box_top em { background:url(/pics/code_msg_top.png) no-repeat; height:22px;}
.box_bottom { width:252px; height:10px; overflow:hidden;}
.box_bottom em { background:url(/pics/code_msg_bottom.png) no-repeat; height:10px;}
/* 此中间样式是从 内测页面复制过来的 */
.moyo_right  .moyo_txt a.blue01 { font-size:14px; height:20px; line-height:20px; margin-top:12px; color:#0099cc; }

.moyo_right  .moyo_txt font { display:block;  height:20px; line-height:20px; margin-top:12px; float:left; font-size:14px; color:#666; padding-left:20px;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/scripts/jquery.form.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="/js/msg.js" charset="utf-8"></script>
<!--[if IE 6]>
    <script type="text/javascript" src="js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('em,.txtput,textarea3');
	 </script>
<![endif]-->
<script type="text/javascript">
    $(document).ready(function () {
        $("body").css({ "background-color": "#dedee0" });
        $("div#wrap").css({ "background-color": "#dedee0" });
        setFocusEmptyInput($("span.moyo_txt b"), $("input.pwdtxt"), "输入注册邮箱");
    });

    function showActivationCodePrompt(prompt) {
        //邀请码错误提示框
        var pwdtxt = $('input#email');
        $('div.box_code').css({
            "left": pwdtxt.offset().left - $("div#wrap").offset().left - 20,
            "top": pwdtxt.offset().top - $("div#wrap").offset().top + 40,
            "display": "block"
        });
        $("div.code_content span").html(prompt);
        var timeoutId = setTimeout(function () {
            $('div.box_code').hide(300);
        }, 3000);
    }

    function backAndResetPassword() {
        if ($("input#email").val() != "") {
            if (isEmail($("input#email").val())) {
                $('form#resetPasswordForm').ajaxSubmit(function (data) {
                    if ((data != null) && (data != "")) {
                        var obj = $.parseJSON(data);
                        if (obj.status == "success") {
                            $("div#resetPasswordArea").hide();
                            $("div#resetPasswordSuccess").show();
                            $("font#registerEmail").html(obj.information);
                        }
                        else {
                            showActivationCodePrompt(obj.information);
                        }
                    }
                    else
                        $.jBox.tip("系统维护中，给您带来了不便，请谅解！", "error");
                });
            }
            else
                showActivationCodePrompt("请输正确的邮箱格式");
        }
        else
            showActivationCodePrompt("请输入注册邮箱");
    }
</script>
</asp:Content>
