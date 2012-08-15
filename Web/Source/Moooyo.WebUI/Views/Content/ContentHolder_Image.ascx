<%@ Control Language="C#"  Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<% Moooyo.BiZ.Content.ImageContent image = (Moooyo.BiZ.Content.ImageContent)Model.contentobj;%>
<div class="box_demo w412">
    <div class="box_content">
<div class="box_top">
    <div class="contetn_l"><span><a href="/Content/TaContent/<%=Model.contentobj.Creater.MemberID %>/all/1" class="blue" target="_blank"><%=Model.contentobj.Creater.NickName%></a></span><span><%=Model.typename %></span><span class="userpic"><a href="/Content/TaContent/<%=Model.contentobj.Creater.MemberID %>/all/1"><img src="<%=Comm.getImagePath(Model.contentobj.Creater.ICONPath,ImageType.Middle) %>" width="58" height="58"></a></span>
    <div><%=image.Content%></div></div>
    <div class="content_r" style="position:relative; width:265px;">
    <%for (int i = 0; i < image.ImageList.Count; i++)
        {
            if (i > 3) { break; }%>
        <a  style="margin:7px 0px 0px 7px;float:right;"><img src="<%=Comm.getImagePath(image.ImageList[i].ImageUrl,ImageType.Middle) %>" height="88" width="115px"></a>
    <%}%>
        <div class="left_div"><%=image.ImageList.Count %>张</div>
    </div>
</div>
