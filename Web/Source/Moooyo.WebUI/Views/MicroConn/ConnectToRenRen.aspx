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
                microConnOperation.sendInfo('3', '3', sendingContent, '', "http://www.moooyo.com");
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
        var rrdt1 = window.parent.document.getElementById("rrdt");
        var rrdt2 = window.opener.document.getElementById("rrdt");
        var rrzh1 = window.parent.document.getElementById("rrzh");
        var rrzh2 = window.opener.document.getElementById("rrzh");
        if (rrdt1 != null && rrdt2 != null && rrzh1 != null && rrzh2 != null) {
            rrdt1.innerHTML = "<a><img src=\"/pics/weibo3.png\" height=\"24\" width=\"25\"/></a><em></em>";
            rrdt1.onclick = function () { window.parent.closeWb(rrdt1); window.opener.closeWb(rrdt1); }
            rrzh1.value = "true";
            rrdt2.innerHTML = "<a><img src=\"/pics/weibo3.png\" height=\"24\" width=\"25\"/></a><em></em>";
            rrdt2.onclick = function () { window.parent.closeWb(rrdt2); window.opener.closeWb(rrdt2); }
            rrzh2.value = "true";
        }
    }
</script>
</asp:Content>