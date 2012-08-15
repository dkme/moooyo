<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberRelationsModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=Model.Member.Name%>阻止的人
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="c976">
    <!--个人左面板-->
    <% if (Model.IsOwner)
       {%>
    <% Html.RenderPartial("~/Views/Member/ProfileLeftPanel.ascx");%>
    <% }
       else
       { %>
    <% Html.RenderPartial("~/Views/Member/MemberLeftPanel.ascx");%>  
    <% } %>
    <!--endof 个人左面板-->
    <section class="conbox mt32 fl">
    <h2 class="f14 fb">我阻止的人(<%=Model.Member.FavorMemberCount %>)</h2>
    <% if (Model.relationObjs.Count == 0)
       { %>
       	<div class="tcenter f18 cgreen">还没有阻止的人</div>
    <%} %>
    <ul class="like-inter-list">
    <% foreach (var obj in Model.relationObjs)
        { %>
        <% Html.RenderAction("RelationListPanel", "Relation", new { obj = obj });%> 
        <%} %>
    </ul>
    <!--分页-->
        <% if (Model.Pagger!=null)
               if (Model.Pagger.PageCount>1)
           {%> 
           <% Html.RenderAction("pagger", "Shared", new { nowpage = Model.Pagger.PageNo, pagecount = Model.Pagger.PageCount, additionID = Model.Pagger.AdditionParams, url = Model.Pagger.PageUrl });%> 
        <%} %>
    <!--endof 分页-->
    </section>
    <aside class="asidebox-r mt32 fr">
        <% Html.RenderAction("AppPush", "Push");%> 
        <%if (!Model.IsOwner) Html.RenderAction("TheyFavorsMember", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID });%>
        <% Html.RenderAction("SameInterestingMemberOverMe", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID });%> 
        <% Html.RenderAction("GuessYourInterest", "Push");%> 
    </aside>
</div> 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/a_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    $().ready(function () {
        //绑定兴趣标签
        interestCenter.bindinterestLabel($("#interestRelationContainer li"));
        //绑定会员标签
        MemberInfoCenter.BindDataInfo($("#relationMemberInfo [name='relationMemberInfoArea']"));
    });
    function deleterelation(mid) {
        if (confirm("取消对Ta的阻止？")) {
            MemberLinkProvider.deldisable(mid, function (result) {
                if (result.ok) {
                    window.location.reload();
                }
            });
        }
    }
</script>
</asp:Content>
