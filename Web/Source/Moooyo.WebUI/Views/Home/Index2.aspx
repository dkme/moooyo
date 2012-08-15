<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
米柚
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%Html.RenderPartial("~/Views/MicroConn/ShareBu.ascx"); %>
<div id="bigbg"><img id="bgimg" src="" title="" alt="" /></div>
<div class="box_code" style="display:none;">
    <div class="box_top"><em></em></div>
    <div class="code_content"> <span></span></div>
    <div class="box_bottom"><em></em></div>
</div>
<div id="wrap-l" style=" display:none;">
  <div class="my-login">
     <div class="my-logo">
     <img src="/pics/LOGO-L.png" title="米柚网" alt=""/>
     </div>
     <div class="clear"></div>
     <div class="my-txt">
     <form id="loginform" action="/account/useralertlogin" method="post">
        <input type="hidden" id="fromPage" name="fromPage" value="index" />
        <input id="ReturnUrl" name="ReturnUrl" type="hidden" value="<%=ViewData["ReturnUrl"] %>"/>
       <ul>
       
         <li class="li-t" id="areaEmail"><input class="mytxt" name="email" id="email" value="" type="text" autocomplete="off" /><b onclick="moveCursor();">输入你的邮箱</b></li>
         <li class="li-t" id="areaPassword"><input class="mytxt" name="password" id="password" value="" type="password" onchange="" /><b onclick="moveCursor();"></b></li>
         <%if (Session["loginerrorcount"] != null && int.Parse(Session["loginerrorcount"].ToString()) >= 3)
                {
                    %>
            <li class="li-t" id="areaVerification"><input class="mytxt" name="verificationCode" id="verificationCode" value="" style="width:100px; float:left;" /><b>输入验证码</b>
            <label class="code" style="clear:none; float:left;">
                        <div class="fl"><img id="ImgValidateCode" height="57" width="125" alt="点击换一个！" title="点击换一个！" src="<%=Url.Action("SecurityCode", "Account")%>" style=" float:left; clear:none; margin:0px; padding:0px; margin-left:8px; position:relative; cursor:pointer;" class="fl" /></div>
                    </label>
            </li>
            <%}%>
         <li class="li-b"><a href="javascript:;" name="btnenter" id="btnenter">登 &nbsp;   入</a><span class="r-s"><font><input type="checkbox" id="remember" class="chekbox" name="remember" tabindex="4" onclick="rememberMember()" class="my-check" /> 自动登录</font><a href="javascript:void(0)" onclick="window.parent.location='/Account/ResetPassword?back=/Home/Index'">忘记密码？</a></span> </li>
         
         <li class="li-s">
             没有账号？  
             <a href="javascript:;" onclick="window.open('/MicroConn/ConnectToTXWeibo');" title="使用腾讯微博账号登录"><img src="/pics/weibo-2.png" height="25" width="25" /></a>
             <a href="javascript:;" onclick="window.open('/MicroConn/ConnectToSinaWeibo');" title="使用新浪微博账号登录"><img src="/pics/weibo-01.png" height="25" width="25" /></a>
             <a href="javascript:;" onclick="window.open('/MicroConn/ConnectToRenRen');" title="使用人人网账号登录"><img src="/pics/weibo3.png" height="25" width="25" /></a>
             <a href="javascript:;" onclick="window.open('/MicroConn/ConnectToDouBan');" title="使用豆瓣网账号登录"><img src="/pics/weibo_4.png" height="25" width="25" /></a>
         </li>
         <li class="li-b"><a class="regLink" href="/Register/Regist">立即注册</a></li>
       </ul>
       </form>
     </div>
     <div class="my-wen"><%=ViewData["choiceContent"] %> <a style="cursor:pointer;" href="#" title="分享" data-oldobj="index" onmouseover="shareClick('http://www.moooyo.com','','<%=ViewData["choiceContent"] %>',$(this),'http://www.moooyo.com/photo/Get/<%=ViewData["choiceImage"] %>')"><img src="/pics/share-bg.png" width="36" height="33"/></a></div>

  </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link type="text/css" rel="Stylesheet" id="LINK"  />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/Scripts/jquery.validate.min.js" type="text/javascript"></script>

<!--[if IE 6]>
    <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('.my-logo img,.mytxt,.li-b a,a img,.box_code em,.box_bottom em');
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

       //鼠标悬浮底部文字描述位置
     
       $('.my-wen,.my-wen a').mouseenter(function(){
          $(this).find('img').attr("src","/pics/share-bghover.png");
         // $('.my-wen a').html("<img src=\"/pics/share-bghover.png\" height=\"33\" width=\"36\" />");
       });
        $('.my-wen,.my-wen a').mouseleave(function(){
           $(this).find('img').attr("src","/pics/share-bg.png");
          //$('.my-wen a').html("<img src=\"/pics/share-bg.png\" height=\"33\" width=\"36\" />");
       });
       //判断屏幕像素比例 add by xx
        if (screen.height > 800) 
           {
             document.getElementById('LINK').href="/css/Login1.css";
             $('#wrap-l').css({"display":"block"});
              }
			else 
			{
            document.getElementById('LINK').href="/css/Login2.css";
            $('#wrap-l').css({"display":"block"});
	    	}


        var errInfo = "<%=ViewData["errinfo"] %>";
        if(errInfo != "") showPrompt(errInfo);
        //setValidate(); //绑定验证

        $("#btnenter").click(function () {
            var form = $("#loginform");
//            if (form.valid()) {
//                seveUserLoginInfo();
//                form.submit();
//            }
              var email = $("#email").val();
              var passwd = $("#password").val();
              if(passwd == "") $("#password").focus();
              if(email == "") $("#email").focus();

              if(email != "" && passwd != "") {
                    var verificationCode = $("#verificationCode").val();
                    if(verificationCode != undefined) { 
                        if(verificationCode == "") { 
                            $("#verificationCode").focus();
                            return;
                        }
                    }
                    setTimeout(function () {
                        seveUserLoginInfo();
                        form.submit();
                    }, 20);
              }
        });

        var userNameCookie = $.cookie("clientLoginUserNameCookie");
        if (userNameCookie != null) {
            $("#areaEmail b").html("");
            $("#email").val(userNameCookie);
            $("#password").focus();
        }
        $("#areaEmail b").click(function () {
            $("#email").focus();
        });
        $("#areaPassword b").click(function () {
            $("#password").focus();
        });
          $("#areaVerification b").click(function () {
            $("#verificationCode").focus();
        });
        setFocusEmptyInput($("#areaEmail b"), $("#email"), "输入你的邮箱");
        setFocusEmptyInput($("#areaPassword b"), $("#password"), "输入密码");
        setFocusEmptyInput($("#areaVerification b"), $("#verificationCode"), "输入验证码");

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

    function showPrompt(prompt) {
        //邀请码错误提示框

        var element2 = $('#btnenter');
        
        $('div.box_code').css({
            left: element2.offset().left +"px",
            top: element2.offset().top + 40 +"px",
            "display": "block"
        });
        $("div.code_content span").html(prompt);
        var timeoutId = setTimeout(function () {
            $('div.box_code').hide(300);
        }, 4000);
    }

    function changeTxtEmail()
    {
        var input = $("#email");
        var textarea = $("#areaEmail b");
        var str = "输入你的邮箱";
        if (input.val().length > 1 && input.val() != str) {
            textarea.html("");
        }
    }

    //窗口自适背景图设置 ADD BY hulu
    $(document).ready(function() {
        var bg = '/photo/Get/<%=ViewData["choiceImage"] %>';
        var $bg = $("#bgimg").hide().attr('src', bg);
        $('#wrap-l').hide();
        $bg.load(function() {
             // 排除IE 6
            if (true || !$('html').hasClass('lt-ie7')) {
                var theWindow = $(window),
                    bgWidth = $bg.width(),
                    bgHeight = $bg.height(),
                    aspectRatio = bgWidth / bgHeight;
                //alert('bgWidth = ' + bgWidth + ' bgHeight = ' + bgHeight + 'winWidth = ' + theWindow.width() + ' winHeight = ' + theWindow.height());
                function resizeBg() {
                    var winWidth = theWindow.width(),
                        winHeight = theWindow.height(),
                        w = h = 0,
                        offsetX = bgWidth - winWidth > 0 ? (bgWidth - winWidth) / 2 : 0,
                        offsetY = bgHeight - winHeight > 0 ? (bgHeight - winHeight) / 2 : 0;
                    if ( (winWidth / winHeight) < aspectRatio ) {
                        h = offsetY + winHeight;
                        w = h * aspectRatio;
                    } else {
                        w = offsetX + winWidth;
                        h = w / aspectRatio;
                    }
                    $bg.css({
                        width: w,
                        height: h,
                        left: -offsetX,
                        top: -offsetY
                    }).css('visibility', 'visible').fadeIn(500);
                }
                                  
                theWindow.resize(function() {
                 resizeBg();
            
                }).trigger("resize");
            }
            else {
                $('body').attr('style', '_background: transparent url(' + bg + ') no-repeat fixed center center');
            }
            $('#wrap-l').show();
            var element2 = $('#btnenter');
            $('div.box_code').css({
                left: element2.offset().left +"px",
                top: element2.offset().top + 40 +"px"
           });
        });
    });

    $(window).resize(function () {
      // Wenpotion();
       var element2 = $('#btnenter');
       $('div.box_code').css({
            left: element2.offset().left +"px",
            top: element2.offset().top + 40 +"px"
           });
    });


//     function moveCursor()
//   {
//	
//	var rng=this.email.createTextRange();
//	rng.move("character",0);
//	rng.select();

//    var rng1=this.password.createTextRange();
//	rng1.move("character",0);
//	rng1.select();

//     var rng2=this.verificationCode.createTextRange();
//	rng2.move("character",0);
//    rng2.collapse(true); 
//    rng2.select();
//	
//	}

      
</script>
</asp:Content>


