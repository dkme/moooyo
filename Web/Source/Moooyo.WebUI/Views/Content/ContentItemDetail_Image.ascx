<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%@ Import Namespace="Moooyo.BiZ.Like" %>
<%Moooyo.BiZ.Content.ImageContent image = (Moooyo.BiZ.Content.ImageContent)Model.contentobj;%>
<div class="header clearfix">
    <h1 class="title"><%=Model.Member.Name %>的照片</h1>
    <small class="datetime"><%=image.CreatedTime%></small>
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
    <p class="intro"><%=Comm.replaceToN(image.Content)%></p>
    <div class="photo-list">
    <% 
        var i = 0;
        foreach (var img in image.ImageList)
       {
           i++;
            %>
           <img src="<%=Comm.getImagePath(img.ImageUrl, ImageType.Original) %>" alt="p<%=i %>" class="imageContent" onload="imageonload(this,670)"/>
    <% } %>
    </div>
    <% Html.RenderPartial("ContentItemDetail_Footer"); %>
</div>