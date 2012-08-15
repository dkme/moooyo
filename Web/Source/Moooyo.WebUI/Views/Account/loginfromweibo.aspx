<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script language="javascript" type="text/javascript">
    var notSelectedInterest = <%=ViewData["notSelectedInterest"] %>; //未选兴趣
    var notActivated = <%=ViewData["notActivated"] %>; //未激活
    var hasBeenActivatedAndSelectedInterested = <%=ViewData["hasBeenActivatedAndSelectedInterested"] %>; //已激活并且选择了兴趣
    var notActivatedAndSelectedInterest = <%=ViewData["notActivatedAndSelectedInterest"] %>; //未激活并且未选择兴趣

    $(document).ready(function () {
        if (hasBeenActivatedAndSelectedInterested) {
            jump("/Content/IFavorerContent");
        }
        if (notSelectedInterest) {
            jump("/Register/Regist?ifweibo=true");
        }
        if (notActivated) {
            jump("/Account/Welcome");
        }
        if (notActivatedAndSelectedInterest) {
            jump("/Register/Regist?ifweibo=true");
        }
    });

    function jump (url) {
        if (window.parent.parent != null)
            window.parent.parent.location = url;
        else
            window.parent.location = url;
    }
    
</script>
</asp:Content>
