<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/IndexEnter.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	成为他们
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script type="text/javascript">
    //*** 禁止浏览器后退 *****************
    //禁用后退按钮
    window.history.forward(1);
    //禁用后退键，作用于Firefox、Opera  
    document.onkeypress = banBackSpace;
    //禁用后退键，作用于IE、Chrome  
    document.onkeydown = banBackSpace;
    //************************************ 
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    <link rel="stylesheet" href="/css/reg_<%=ViewData["cssversion"] %>.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="c976 clearfix">
    <div class="h60"></div>
    <div style=" height:480px; margin:0 60px 60px 60px; text-align:center;">
    	<div style="height:480px; width:470px; border-right:1px solid #CCC; float:left;">
        	<div style="text-align:left;"><img src="/pics/tuxiang.gif" ></div>
            <div style="height:50px; line-height:50px;">98%的米柚会员上传了自己的照片，并认识了新朋友。</div>
            <div style="height:110px; line-height:100px; font-size:100px; font-family:'\5FAE\8F6F\96C5\9ED1'; color:#666;">98%</div>
            <div style="padding-left:150px;">
            	<div id="zs_tabmenu1" class="right-btn-huiyuan"><a style="width:150px; height:30px; line-height:30px; text-align:center; font-size:18px; font-family:'\5FAE\8F6F\96C5\9ED1'" href="/Register/UploadFace?pt=<%=ViewData["photoType"] %>" class="radius3 btn fl">成为TA们</a></div>
            </div>
        </div>
        <div style="height:480px; width:380px; float:left;">
        	<div style="margin-top:40px;"><img src="/pics/xiaoren.gif"></div>
            <div style="height:50px; line-height:50px; margin-top:16px;"></div>
            <div style="height:110px; line-height:100px; font-size:100px; font-family:'\5FAE\8F6F\96C5\9ED1'; color:#666;">2%</div>
            <div style="padding-left:110px;">
            	<div id="zs_tabmenu1" class="right-btn-huiyuan"><a style="width:150px; height:30px; line-height:30px; text-align:center; font-size:18px; font-family:'\5FAE\8F6F\96C5\9ED1'; background-color:#666" href="/Register/RegAddInterest" class="radius3 btn fl">成为TA们</a></div>
            </div>
        </div>
    </div>
    <div class="h30"></div>
</div>
</asp:Content>

