<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
平台连接
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/a_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    var isMestr = '<%=ViewData["IsBindingPlatform"]%>';
    var isSendingContentFlag = '<%=ViewData["isSendingContentFlag"]%>'
    var sendingContent = '<%=ViewData["SendingContent"] %>';
    $().ready(function () {
        if (isSendingContentFlag == "True") {
            microConnOperation.sendInfo('4', '4', sendingContent, '', "http://www.moooyo.com");
            loadparentObject();
            window.parent.location = window.parent.location;
            window.opener.location = window.opener.location;
            window.close();
        }
        else {
            loadparentObject();
            window.parent.location = window.parent.location;
            window.opener.location = window.opener.location;
            window.close();
        }
    });
    function loadparentObject() {
        var dbdt1 = window.parent.document.getElementById("dbdt");
        var dbdt2 = window.opener.document.getElementById("dbdt");
        var dbzh1 = window.parent.document.getElementById("dbzh");
        var dbzh2 = window.opener.document.getElementById("dbzh");
        if (dbdt1 != null && dbdt2 != null && dbzh1 != null && dbzh2 != null) {
            dbdt1.innerHTML = "<a><img src=\"/pics/weibo3.png\" height=\"24\" width=\"25\"/></a><em></em>";
            dbdt1.onclick = function () { window.parent.closeWb(dbdt1); window.opener.closeWb(dbdt1); }
            dbzh1.value = "true";
            dbdt2.innerHTML = "<a><img src=\"/pics/weibo3.png\" height=\"24\" width=\"25\"/></a><em></em>";
            dbdt2.onclick = function () { window.parent.closeWb(dbdt2); window.opener.closeWb(dbdt2); }
            dbzh2.value = "true";
        }
    }
</script>
</asp:Content>
