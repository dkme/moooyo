<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%Moooyo.BiZ.Content.InterViewContent interview = (Moooyo.BiZ.Content.InterViewContent)Model.contentobj;%>
<div class="header clearfix">
    <h1 class="title"><%=Model.Member.Name %>的小编访谈</h1>
    <small class="datetime"><%=interview.CreatedTime%></small>
     <% if (Model.IsOwner)
       { %>
        <a class="back" href="/Content/IContent"><%=Model.Member.Name%>的首页</a>
     <% }
       else
       {%>
       <a class="back" href="/Content/TaContent/<%=Model.Member.ID %>/all/1"><%=Model.Member.Name%>的首页</a>
     <%} %>
</div>
<div class="main-content">
    <div class="photo-list">
    <% 
        var i = 0;
        foreach (var obj in interview.InterviewList)
       {
           i++;
            %>
        <div ><span class="huise">小编：<%=obj.Question%></span> </div>
        <div ><span ><%=Model.Member.Name %>：<%=obj.Answer%></span></div>
    <% } %>
    </div>
    <% Html.RenderPartial("ContentItemDetail_Footer"); %>
</div>