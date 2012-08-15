<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="c1" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<%--<script src="/js/a_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>--%>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/Scripts/jquery.validate.min.js"></script>
<style type="text/css"> 
/* Reset */
body,div,dl,dt,dd,ul,ol,li,h1,h2,h3,h4,h5,h6,pre,form,fieldset,input,textarea,p,blockquote,th,td { margin:0; padding:0; }
table { border-collapse:collapse; border-spacing:0; }
fieldset,img { border:0; }
address,caption,cite,code,dfn,em,strong,th,var { font-style:normal; font-weight:normal; }
ol,ul { list-style:none; }
caption,th { text-align:left; }
h1,h2,h3,h4,h5,h6 { font-size:100%; font-weight:normal; }
q:before,q:after { content:''; }
abbr,acronym { border:0; }
 
/* Font,  Link & Container */
.tiffBlue{color:#00afbc}
body { font:12px/1.6 arial,helvetica,sans-serif; }
a:link { color:#369;text-decoration:none; }
a:visited { color:#669;text-decoration:none; }
a:hover { color:#fff;text-decoration:none;background:#039; }
a:active { color:#fff;text-decoration:none;background:#f93; }
button { cursor:pointer;line-height:1.2; }
.mod { width:100%; }
.hd:after, .bd:after, .ft:after, .mod:after {content:'\0020';display:block;clear:both;height:0; }
.error-tip { margin-left:10px; padding:6px 70px;}
.error-tip, .error { color:#fe2617; }
    
/* Layout */
.wrapper { width:950px;margin:0 auto; }
#header { padding-top:30px; }
#content { min-height:400px;*height:400px; }
#header, #content { margin-bottom:40px; }
#header, #content, #footer { width:100%;overflow:hidden; }
#footer { color:#999;padding-top:6px;border-top: 1px dashed #ddd; }
.article { float:left;width:590px; }
.aside { float:right;width:310px;color:#666; }
 
/* header */
.logo { float:left; width:199px;  height:34px; overflow:hidden; line-height:10em; }
a.logo:link,
a.logo:visited,
a.logo:hover,
h1 { color:#494949;display:block;font-size:25px;font-weight:bold;line-height:1.1;margin:0;padding:0 0 30px;word-wrap:break-word; }
 
/* form */
.item { margin:10px 0;zoom:1; }
label { display: inline-block; margin-right: 5px; text-align: right; width: 60px; font-size: 14px; line-height: 30px; vertical-align: baseline }
label.error {
	margin-left: 10px;
	width: auto;
	display: inline;
	color: #fe2617;
	font-size:12px;
}
.remember { cursor: pointer; font-size: 12px; display: inline; width: auto; text-align: left; float: none; margin: 0; color: #666 }
.item-captcha input,
.basic-input { width: 200px; padding: 5px; height: 18px; font-size: 14px;vertical-align:middle; -moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px; border: 1px solid #c9c9c9 }
.item-captcha input:focus,
.basic-input:focus { border: 1px solid #a9a9a9 }
.item-captcha input { width:100px; }
.item-captcha .pl { color:#666; }
.btn-submit { cursor: pointer;color: #ffffff;background: #00afbc; border: 1px solid #00737b; font-size: 14px; font-weight: bold; padding:6px 26px; border-radius: 3px; -moz-border-radius: 3px; -webkit-border-radius: 3px; *width: 100px;*height:30px; }
.btn-submit:hover { background-color:#02d3e2;border-color:#00737b; } 
.btn-submit:active { background-color:#02d3e2;border-color:#00737b; } 
#item-error { padding-left:75px; }
 
/* footer */
.fright { float:right; }
.icp { float:left; }
</style>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        var input = $("#name");
        setValidate();
    });
    function setValidate() {
        $("#loginform").validate({
            rules: {
                name: {
                    required: true
                },
                password: {
                    required: true
                }
            },
            messages: {
                name: {
                    required: "必填"
                },
                password: {
                    required: "必填"
                }
            }
        });
    }
</script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
管理登录
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="wrapper">
  <div id="header">
      
  </div>
 
<div id="content">
  <h1>管理登录</h1>
  <div class="article">
<form id="loginform" name="loginform" method="post" action="/admin/userlogin" onsubmit="return $(this).valid()">
    <div class="error-tip"><% =ViewData["errinfo"] %></div>
    <input id="ReturnUrl" name="ReturnUrl" type="Hidden" value="<% =ViewData["ReturnUrl"] %>"/>
    <div class="item">
        <label>用户名</label>
        <input id="name" name="name" type="name" class="basic-input" maxlength="60" value="" tabindex="1"/>
    </div>
    <div class="item">
        <label>密码</label>
        <input id="password" name="password" type="password" class="basic-input" maxlength="20" tabindex="2"/>
    </div>
    <div class="item">
        <label>&nbsp;</label>
        <input type="submit" value="登录" name="user_login" class="btn-submit" tabindex="5"/>
        
    </div>
</form>
 
  </div>

</div>

</asp:Content>
