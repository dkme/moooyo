<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
升级
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="shengji">
    <div class="h33 bgf2 tcenter f18">升级成高级用户</div>
        <div class="m50 h310 w520" >
                	<div class=" h60 ">
                	  <p>为了保证每位会员的质量，给大家营造一个良好的沟通环境。</p>
                	  <p>系统限制普通会员每天内只能发送10条消息</p>
                      <p>高级会员将不受限制</p>
       	    	</div>
       		  	<div class="mt15 bgf7">
           	   		满足以下条件即可升级成为高级用户：
				</div>
				<div class="mt10 h25 f1">
                	  <div class="fl">1、至少上传一张头像照片</div><div class="fl ml20 right-btn-huiyuan"><a href="/photo/photoupload?t=1" class="radius3 btn w80 tcenter h23 fl" target="_blank">上传照片</a></div>
				</div>
                <div class="mt15 h25">
                	<span>2、发送如下链接给</span> <span><img src="/pics/1.gif" align="absmiddle"/></span> <span>位好友成功加入</span>
				</div>
                <div class="mt15 ml30 h25">
                	<input type="text" id="upgrade_input" value="http://www.moooyo.com/<%=ViewData["me"] %>" style="height:25px; width:350px;"/>
                	<div class="upinput cgray"><span class="fl colorf00"><a href="#" id="copybtn" class="fl">点这里</a><span class="fl">复制后粘贴到QQ、MSN(非IE浏览器要自己复制)</span></span></div>
				</div>
                <div class="clearfix"></div>
                <div class="ml30 mt5">
           	   		<span>或推荐到：</span>
                    <span><img src="/pics/xinlangweibo.gif" height="23" align="absmiddle"/></span>
                    <span><img src="/pics/tengxunweibo.gif" height="23" align="absmiddle"/></span>
				</div>
				<div class="mt5 ml30" style="color:#F00;">
           	   		您邀请的好友有客服严格审核，虚假注册不予通过，并停用您的账号
				</div>
                <div class="mt5 fr" style="color:#F00;">
           	   		<input type="button" value="关闭" class="w80 h23" onClick="window.parent.jBox.close(true);"/>
				</div>
        </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/a_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    $().ready(function () {
        if (!$.browser.msie) {
            //复制到剪贴板
            copyToClipboard($("#copybtn"), $("#upgrade_input").val());
        }
        else {
            $("#copyToClipboardBtn").bind("click", function () {
                //复制到剪贴板
                copyToClipboard($("#copybtn"), $("#upgrade_input").val());
            });
        }
    });
</script>
</asp:Content>
