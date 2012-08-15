<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	登录 米柚网-单身欢乐季
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <div id="wrap1">
   <div class="contain2">
       <div id="sideleft">
         <div  style="min-height:380px; height:auto !important; height:380px; overflow:visible;">
	       <div class="box_com h90 border_b"><h1>登录</h1></div>
		   <div class="box_com p40">
          <form id="loginform" action="/account/useralertlogin" method="post">
            <input type="hidden" id="fromPage" name="fromPage" value="login" />
            <input id="ReturnUrl" name="ReturnUrl" type="hidden" value="<%=ViewData["ReturnUrl"] %>"/>
		      <dl>
            <dd class="clearfix" >
                <span class="at_text t_1_d" id="areaEmail">
                    <input type="text"  class="txtput" id="email" name="email" />
					<b>邮箱</b>
                </span>          
		   </dd>
            <dd class="clearfix">
                <span class="at_text t_1_d" id="areaPassword">
                    <input type="password" id="password" name="password"  class="txtput newpasstxt"  />
					<b></b>
                </span>
			</dd>
            <%if (Session["loginerrorcount"] != null && int.Parse(Session["loginerrorcount"].ToString()) >= 3)
                {
                    %>
            <dd class="clearfix">
                <span class="at_text t_1_d" id="areaVerification">
                    <input type="text" id="verificationCode" name="verificationCode" style="width:100px;" class="txtput" />
					<b>验证码</b>
					<label class="code" style="clear:none; float:left;">
                        <div class="fl"><img id="ImgValidateCode" height="30" width="80" alt="点击换一个！" title="点击换一个！" src="<%=Url.Action("SecurityCode", "Account")%>" style=" float:left; clear:none; margin:0px; padding:0px; margin-top:-8px; position:relative; cursor:pointer;" class="fl" /></div>
                    </label>
                </span>		
			</dd>
            <%}%>
            <dd class="clearfix">
                <span class="at_text t_1_d">
				  <input type="checkbox" id="remember" class="chekbox" name="remember" tabindex="4" onclick="rememberMember()" />  14天内访问自动登录
                </span>		
			</dd>
            <dd class="clearfix">
			    <span class="at_text t_1_d">
                <input name="btnenter" id="btnenter" type="button" class="reg_btn btn"  value="登录米柚" />
            
                <a href="javascript:void(0)" onclick="window.parent.location='/Account/ResetPassword?back=/Account/Login'" class="login_link">忘记密码</a><a href="/Register/Regist" class="login_link">注册米柚账号</a>
				<!-- <a href="#">马上注册</a> -->
                </span>
			</dd>
			 </dl>
             </form>
		 </div>
         </div>
      </div>
       <div id="sideright"> 
       <div style="min-height:380px; height:auto !important; height:380px; overflow:visible;">
         <div class="box_com h90 "><img src="/pics/mylogo.gif" alt="米柚网" /></div>
		  <div style="position:absolute; top:260px; left:0; border:0;">
		    <div class="box_com" style="padding:5px 20px" ><span>已有账号</span></div>
             <div class="box_com ">
			   <dl id="LinkList">
			    <dt>
                <a href="javascript:;" onclick="window.open('/MicroConn/ConnectToTXWeibo');" title="使用腾讯微博账号登录"><img src="/pics/demo14.png" height="25" width="25"/></a>
                </dt>
				<dt>
                <a href="javascript:;" onclick="window.open('/MicroConn/ConnectToSinaWeibo');" title="使用新浪微博账号登录"><img src="/pics/demo15.png" height="25" width="25"/></a>
                </dt>
                <dt>
                <a href="javascript:;" onclick="window.open('/MicroConn/ConnectToRenRen');" title="使用人人网账号登录"><img src="/pics/weibo3.png" height="25" width="25" /></a>
                </dt>
                <dt>
                <a href="javascript:;" onclick="window.open('/MicroConn/ConnectToDouBan');" title="使用豆瓣网账号登录"><img src="/pics/weibo_4.png" height="25" width="25" /></a>
                </dt>
				<%--<dt><a href=""><img src="" height="25" width="25"/></a></dt>
				<dt><a href=""><img src="" height="25" width="25"/></a></dt>
				<dt><a href=""><img src="" height="25" width="25"/></a></dt>--%>
			   </dl>
			</div>
		  </div> 
		</div>
     </div>
   </div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
        #areaVerification label.error{margin-left:90px;}
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
   <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script type='text/javascript' src='/Scripts/jquery.autocomplete.js'></script>
    <script type="text/javascript" src="/js/autoCompleteData.js" ></script>
    <script src="/Scripts/jquery.validate.min.js" type="text/javascript"></script>
    <!--[if IE 6]>
    <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
        DD_belatedPNG.fix('.txtput');
	</script>
    <![endif]-->
    <script language="javascript" type="text/javascript">
        $(window).load(function () {
            if($("#password").val() == "") {
                $("#areaPassword b").html("输入密码");
            }
            else
            {
                $("#areaPassword b").html("");
            }
        });
        $(document).ready(function () {
            $("body").css({ "background-color": "#dedee0" });
            var errInfo = "<%=ViewData["errinfo"] %>";
            if(errInfo != "")  $.jBox.tip(errInfo, 'error');
            var obj = $("#email").autocomplete(emailAutoCompleteDatas, {
                autoFill: false,
                mustMatch: false,
                scrollHeight: 150,
                max: 40
            });

            setValidate(); //绑定验证

            $("#btnenter").bind("click", function () {
                var form = $("#loginform");
                if (form.valid()) {
                    seveUserLoginInfo();
                    form.submit();
                }
            });

            var userNameCookie = $.cookie("clientLoginUserNameCookie");
            if (userNameCookie != null) {
                $("#areaEmail b").html("");
                $("#email").val(userNameCookie);
                $("#password").focus();
            }

            setFocusEmptyInput($("#areaEmail b"), $("#email"), "邮箱");
            setFocusEmptyInput($("#areaPassword b"), $("#password"), "密码");
            setFocusEmptyInput($("#areaVerification b"), $("#verificationCode"), "验证码");

            /*更换验证码*/
            $("#ImgValidateCode").click(function () {
                $("#ImgValidateCode").attr("src", '/Account/SecurityCode?time=' + (new Date()).valueOf());
                return false;
            });
        });
        //回车事件
        document.onkeypress = function (e) {
            e = e || window.event;
            var key = e ? (e.charCode || e.keyCode) : 0;
            if (key == 13) {
                $("#btnenter").click();
            }
        }
        function seveUserLoginInfo() {
            var userName = $("#email").val();
            var userNameCookie = $.cookie("clientLoginUserNameCookie");

            if (userNameCookie == null) {
                $.cookie("clientLoginUserNameCookie", userName, { expires: 7 }); // 存储一个带7天期限的 cookie
            }
            else if (userNameCookie != userName) {
                $.cookie("clientLoginUserNameCookie", userName, { expires: 7 }); // 存储一个带7天期限的 cookie
            }
        }
        function rememberMember() {
            if (document.getElementById("remember").checked) {
                $.jBox.tip("如果是公用电脑，建议不要使用此功能", 'info');
            }
        }
        //验证邮箱事件
        function setValidate() {
            $("#loginform").validate({
                errorPlacement: function(error, element) {
                    error.attr("class", "error");
				    error.appendTo(element.parent("span"));
			    },
//                success: function(label) {
//				    label.text("").addClass("success");
//			    },
                rules: {
                    email: {
                        required: true, email: true
                    },
                    password: {
                        required: true, maxlength: 16, minlength: 6
                    },
                    verificationCode: {
                        required: true, maxlength: 10, minlength: 2
                    }
                },
                messages: {
                    email: {
                        required: "请填写邮箱！",
                        email: "邮箱无效！"
                    },
                    password: {
                        required: "请填写密码！",
                        maxlength: "密码过长！",
                        minlength: "密码过短！"
                    },
                    verificationCode: {
                        required: "请填写验证码！",
                        maxlength: "验证码过长！",
                        minlength: "验证码过短！"
                    }
                }
            });
        }
    </script>
</asp:Content>
