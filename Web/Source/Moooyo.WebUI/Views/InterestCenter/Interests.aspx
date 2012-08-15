<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<% switch (Model.type)
    {
        case "meTaCommon": Response.Write("与TA的共同兴趣爱好"); break;
        default:%>
        <%=Model.IsOwner ? "我" : "TA"%>的兴趣(<%=Model.interestCount%>)
        <% break;
    }%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="c976 clearfix">
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
    <section class="inter-conbox mr15 mt32 fl">
    <h2 class="f14 fb">
    <% switch (Model.type)
    {
        case "meTaCommon": Response.Write("与TA的共同兴趣爱好"); break;
        default:%>
        <%=Model.IsOwner ? "我" : "TA"%>的兴趣(<%=Model.interestCount%>)
        <% break;
    }%>
    </h2>
    <% if (Model.IsOwner)
       { %>
    <a href="/InterestCenter/AddInterest" class="add-myinter mt32 "><i></i></a>
    <% } %>
    <ul class="add-myinter-list fl" id="interestContainer">
    <% if(Model.interestListObj.Count>0){
            foreach (var obj in Model.interestListObj)
           { %>
            <li data-interestid="<%=obj.ID %>"><a href="/InterestCenter/InterestFans<% if(!Model.IsOwner) Response.Write("/" + Model.MemberID); %>?iid=<%=obj.ID%>" target="_blank"><img src="<%= Comm.getImagePath(obj.ICONPath, ImageType.Middle) %>" width="50" height="50" title="<%=obj.Title%>" alt="<%=obj.Title%>" border="0" /><br /><%=obj.Title.Length > 5 ? obj.Title.Substring(0, 5) + "<span class=\"letspa--3\">...</span>" : obj.Title%></a></li>
        <% }
        } %>
    </ul>
    <!--Begin paging-->
    <% if (Model.Pagger != null) {
           if (Model.Pagger.PageCount > 1) {%> 
               <% Html.RenderAction("QueryStrPaging", "Shared", new { nowpage = Model.Pagger.PageNo, pagecount = Model.Pagger.PageCount, additionID = Model.Pagger.AdditionParams, url = Model.Pagger.PageUrl });%> 
           <% } %>
    <% } %>
    <!--End paging-->
    </section>
    <aside class="asidebox-r mt32 fr">
  <%--  <% Html.RenderAction("MyInterest", "Push"); %>--%>
    <% Html.RenderAction("SameInterestingMemberOverMe", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
    <% if(Model.IsOwner) Html.RenderAction("GuessYourInterest", "Push"); %>
    <% Html.RenderAction("Question", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
    </aside>
</div>   
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/data_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        interestCenter.bindinterestLabel($("#interestContainer li"));
    });
</script>
</asp:Content>
