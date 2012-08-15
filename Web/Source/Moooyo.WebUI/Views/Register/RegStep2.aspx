<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	注册 米柚网-单身欢乐季
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="wrap1">
       <div class="contain2">
          <div class="login_left w480">
	         <div class="box_com h90 border_b"><h1>注册</h1></div>
             <form id="regform" name="regform" method="post" action="/Register/ProcRegStep2" onsubmit="return $(this).valid()">
                <%--<input type="hidden" id="preID" name="preID" value="<%=ViewData["preID"] %>"/>--%>
                <input type="hidden" id="inviterID" name = "inviterID" value="" />
                <input type="hidden" id="t" name="t" value="1" />
		     <div class="box_com p40">
		          <dl>
                <dd class="clearfix">
                    <span class="at_text t_1_d" id="areaEmail">
                        <input type="text"  class="txtput" id="email" name="email" value="" />
				     <b>邮箱</b>				 
			     </span>		  
		      </dd>
                <dd class="clearfix">
                    <span class="at_text t_1_d" id="areaPwd">
                        <input type="password" id="pwd" name="pwd" value="" class="txtput newpasstxt"  />
					    <b>密码</b>
                    </span>			</dd>
                <dd class="clearfix">
                    <span class="at_text t_1_d" id="areaNickName">
                        <input type="text" id="nickName" name="nickName" value="" class="txtput" />
					    <b>昵称</b>
                    </span>			</dd>
			      <dd class="clearfix">
                    <span class="at_text t_1_d">
                        <div class="fl" style="margin-left:7px;">生日：</div>
					    <select id="year" name="year" class="select-com">
              		    </select>年
                        <select name="month" id="month" class="select-com">
                        </select>月
                        <select id="day" name="day" class="select-com">
                        </select>日
                    </span>		   </dd>
                  <dd class="clearfix"> <span class="at_text t_1_d">
                    <div class="fl" style="margin-left:7px;"></div>
                    <input type="radio" class="radio" name="sex" id="sexBoy" checked="checked" value="1" />
                    男
                    <input type="radio" name="sex" class="radio" id="sexGirl" value="2" />
                    女 </span> 
                    </dd>
                  <dd class="clearfix">
                    <span class="at_text t_1_d">
				      <input type="checkbox" class="chekbox" checked="checked" id="agree" name="agree" />  同意米柚网<a class="blue02" href="/About/Agreement" target="_blank">使用协议</a>                </span>			</dd>
                <dd class="clearfix">
			        <span class="at_text t_1_d">
                    <input name="register_submit" type="submit" class="reg_btn btn"  value="马上注册" />
				    <!-- <a href="#">马上注册</a> -->
                    </span>			</dd>
			     </dl>
		     </div>
		     </form>
	      </div>
	      <div class="login_right h610">
	         <div class="box_com h90 "><img src="/pics/mylogo.gif" alt="米柚网" /></div>
		     <div class="box_com h320"></div>
		     <div class="box_com" style="padding:5px 20px" ><span>已有账号</span></div>
             <div class="box_com ">
			       <dl id="LinkList">
			        <dt><a href="javascript:;" onclick="window.open('/MicroConn/ConnectToTXWeibo');" title="使用腾讯微博账号登录"><img src="/pics/weibo-2.png" height="25" width="25"/></a></dt>
				    <dt><a href="javascript:;" onclick="window.open('/MicroConn/ConnectToSinaWeibo');" title="使用新浪微博账号登录"><img src="/pics/demo15.png" height="25" width="25"/></a></dt>
                    <dt><a href="javascript:;" onclick="window.open('/MicroConn/ConnectToRenRen');" title="使用人人网账号登录"><img src="/pics/weibo3.png" height="25" width="25" /></a></dt>
                    <dt><a href="javascript:;" onclick="window.open('/MicroConn/ConnectToDouBan');" title="使用豆瓣网账号登录"><img src="/pics/weibo_4.png" height="25" width="25" /></a></dt>
				    <%--<dt><a href=""><img src="" height="25" width="25"/></a></dt>
				    <dt><a href=""><img src="" height="25" width="25"/></a></dt>
				    <dt><a href=""><img src="" height="25" width="25"/></a></dt>--%>
			       </dl>
			    </div>
	      </div>
       </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/jquery.autocomplete.css" />
    <style type="text/css">
        
.select-com { border: 1px solid #d4d4d4; background:#fff; height:28px;  margin-right:5px; line-height:28px; font-size:14px; color:#666; text-align:center;
               font-family:'微软雅黑'; vertical-align:middle; }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/Scripts/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src='/scripts/jquery.autocomplete.js' type='text/javascript'></script>
    <script src="/js/autoCompleteData.js" type="text/javascript" ></script>
      <!--[if IE 6]>
        <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
         <script type="text/javascript">
          DD_belatedPNG.fix('.txtput');
       </script>
    <![endif]-->
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("body").css({ "background-color": "#dedee0" });

            //*** 禁止浏览器后退 *****************
            //禁用后退按钮
            window.history.forward(1);
            //禁用后退键，作用于Firefox、Opera  
            document.onkeypress = banBackSpace;
            //禁用后退键，作用于IE、Chrome  
            document.onkeydown = banBackSpace;
            //************************************ 

            setValidate();

            if ($.cookie("inviterID")) {
                $("#inviterID").val($.cookie("inviterID"));
            }

            $("#email").autocomplete(emailAutoCompleteDatas, {
                autoFill: false,
                mustMatch: false,
                scrollHeight: 214,
                max: 30
            });

            setFocusEmptyInput($("#areaEmail b"), $("#email"), "邮箱");
            setFocusEmptyInput($("#areaPwd b"), $("#pwd"), "密码");
            setFocusEmptyInput($("#areaNickName b"), $("#nickName"), "昵称");

            setYMD('请选择', '请选择', '请选择');
        });

        function setValidate() {
            $("#regform").validate({
                rules: {
                    email: {
                        required: true, email: true, maxlength: 40, minlength: 5, remote: "/Register/IsEmailUsed"
                    },
                    pwd: {
                        required: true, maxlength: 16, minlength: 6
                    },
                    nickName: {
                        required: true, minlength: 2, chineseEnglishMaxLength: 18, nicknameCheck: true, userNameFliterCheck: true
                    },
                    year: {
                        required: true
                    },
                    month: {
                        required: true
                    },
                    day: {
                        required: true
                    },
                    agree: "required"
                },
                messages: {
                    email: {
                        required: "请输入您的Email", email: "Email无效", maxlength: "Email不能超过40个字符", minlength: "Email不能少于5个字符", remote: "这个Email已经注册"
                    },
                    pwd: {
                        required: "请输入您的密码", maxlength: "不能超过16个字符", minlength: "不能少于6个字符"
                    },
                    nickName: {
                        required: "填个个性十足的昵称吧", minlength: "不能低于2个字", chineseEnglishMaxLength: "不能超过9个中文或18个英文字", nicknameCheck: "只能中文,英文,数字和下划线"
                    },
                    year: {
                        required: "请选"
                    },
                    month: {
                        required: "请选"
                    },
                    day: {
                        required: "请选"
                    },
                    agree: "您必须"
                }
            });
        }
    </script>
</asp:Content>
