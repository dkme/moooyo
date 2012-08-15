<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
平台连接
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    if (ViewData["error"] != null)
    {
        %>
        <input type="hidden" id="error" name="error" value="true"/>
        <div style="width:100%; font-size:20px; font-family:微软雅黑; text-align:center; margin-top:100px;">嗯，这个入口现在柚子太多堵住了哦，先试试其他方法登陆米柚吧^-^</div>
        <%
    } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/a_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    var isMestr = '<%=ViewData["IsBindingPlatform"]%>';
    var isSendingContentFlag = '<%=ViewData["isSendingContentFlag"]%>'
    var sendingContent = '<%=ViewData["SendingContent"] %>';

    $().ready(function () {
        var error = document.getElementById("error");
        if (isSendingContentFlag == "True") {
            if (error == null) {
                microConnOperation.sendInfo('1', '1', sendingContent, '', "http://www.moooyo.com");
                loadparentObject();
                window.parent.location = window.parent.location;
                window.opener.location = window.opener.location;
                window.close();
            }
        }
        else {
            if (error == null) {
                loadparentObject();
                window.parent.location = window.parent.location;
                window.opener.location = window.opener.location;
                window.close();
            }
        }
    });
    function loadparentObject() {
        var sinadt1 = window.parent.document.getElementById("sinadt");
        var sinadt2 = window.opener.document.getElementById("sinadt");
        var sinawb1 = window.parent.document.getElementById("sinawb");
        var sinawb2 = window.opener.document.getElementById("sinawb");
        if (sinadt1 != null && sinadt2 != null && sinawb1 != null && sinawb2 != null) {
            sinadt1.innerHTML = "<a><img src=\"/pics/demo15.png\" height=\"24\" width=\"25\"/></a><em></em>";
            sinadt1.onclick = function () { window.parent.closeWb(sinadt1); window.opener.closeWb(sinadt1); }
            sinawb1.value = "true";
            sinadt2.innerHTML = "<a><img src=\"/pics/demo15.png\" height=\"24\" width=\"25\"/></a><em></em>";
            sinadt2.onclick = function () { window.parent.closeWb(sinadt2); window.opener.closeWb(sinadt2); }
            sinawb2.value = "true";
        }
    }
</script>
</asp:Content>
