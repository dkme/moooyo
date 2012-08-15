<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=ViewData["nickname"] %>的小编访谈
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
    <!--个人顶部块-->
     <% if (!Model.IsOwner)
        {%>
        <% Html.RenderPartial("~/Views/Member/ProfileTopPanel.ascx"); %>
    <% }
        else
        {%>
    <%--<article class="top-title-box mt20 fl"/>--%>
    <%} %>
    <!--endof 个人顶部块-->
    <section class="inter-conbox mt32 fl">
    <h2>我的兴趣(<%= Model.Member.InterestCount %>)</h2>
    <a href="#" class="add-myinter mt32 fl"><i></i>新增</a>
    <!-- 兴趣列表 -->
    <ul class="add-myinter-list fl">
        <%--<% foreach (var obj in Model.interestObj)
           { %>--%>
            <li><a href="<%=Model.interestObj.ID %>"><img src="<%=Model.interestObj.ICONPath %>"/><br><%=Model.interestObj.Title%></a></li>
        <%--<% } %>--%>
    </ul>
    <!-- endof 兴趣列表 -->
    </section>
    <aside class="asidebox mt32 fr">
    <h3 class="caption-tit clearfix"><b class="fl">他们在问</b><a href="#" class="fr c999">换一批</a></h3>
    <ul class="their-ask-list mt11 clearfix">
        <li><img src="/uploads/head25.png"/><a>你从小到大做过的最糗的一件事是什么？</a></li>
        <li><img src="/uploads/head25.png"/><a>你从小到大做过的最糗的一件事是什么？</a></li>
        <li><img src="/uploads/head25.png"/><a>你从小到大做过的最糗的一件事是什么？</a></li>
        <li><img src="/uploads/head25.png"/><a>你从小到大做过的最糗的一件事是什么？</a></li>
        <li><img src="/uploads/head25.png"/><a>你从小到大做过的最糗的一件事是什么？</a></li>
    </ul>
        <% Html.RenderAction("SameInterestingMemberOverMe", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
        <% Html.RenderAction("TheyLike", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
    </aside>
</div> 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
</asp:Content>
