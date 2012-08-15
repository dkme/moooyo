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
    var sendingContent = '<%=ViewData["SendingContent"]%>';
    $().ready(function () {
        var error = document.getElementById("error");
        if (isSendingContentFlag == "True") {
            if (error == null) {
                microConnOperation.sendInfo('2', '2', sendingContent, '', "http://www.moooyo.com");
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
        var txdt1 = window.parent.document.getElementById("txdt");
        var txdt2 = window.opener.document.getElementById("txdt");
        var txwb1 = window.parent.document.getElementById("txwb");
        var txwb2 = window.opener.document.getElementById("txwb");
        if (txdt1 != null && txdt2 != null && txwb1 != null && txwb2 != null) {
            txdt1.innerHTML = "<a><img src=\"/pics/demo14.png\" height=\"24\" width=\"25\"/></a><em></em>";
            txdt1.onclick = function () { window.parent.closeWb(txdt1); window.opener.closeWb(txdt1); }
            txwb1.value = "true";
            txdt2.innerHTML = "<a><img src=\"/pics/demo14.png\" height=\"24\" width=\"25\"/></a><em></em>";
            txdt2.onclick = function () { window.parent.closeWb(txdt2); window.opener.closeWb(txdt2); }
            txwb2.value = "true";
        }
    }
</script>
</asp:Content>
