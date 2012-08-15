<%@ Page Language="C#" MasterPageFile="~/Views/Shared/IndexEnter.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
出错啦 米柚网-单身欢乐季
</asp:Content>

<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">

<div style="line-height:30px; padding-left:48px; text-align:left; color:#333; font-size:24px; font-family:‘\5FAE\8F6F\96C5\9ED1’;"><%=ViewData["errorNo"].ToString() == "" ? "" : ViewData["errorNo"].ToString() + "，"%><br/>
<%=ViewData["errorInfo"].ToString()%></div>
<div class="backhome" ><a href="/">去首页</a></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<style type="text/css">
    .wrap{ width:970px; margin:0 auto; }
    .backhome {margin-top:30px; color:#0099cc; font-size:12px; line-height:30px; padding-left:48px;}
    .backhome a { color:#0099cc; font-size:12px; line-height:30px; text-decoration:none; font-family:"\5FAE\8F6F\96C5\9ED1";}
</style>
</asp:Content>
