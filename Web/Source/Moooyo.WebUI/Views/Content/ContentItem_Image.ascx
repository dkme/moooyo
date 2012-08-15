<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%Moooyo.BiZ.Content.ImageContent image = (Moooyo.BiZ.Content.ImageContent)Model.contentobj;%>
<div class="ajax_title"><%=Model.typename%><em class="n_pic"></em></div>
<div class="ajax_content">
    <%if (image.ImageList != null && image.ImageList.Count > 0)
      {
          List<string> imagelayouttypelist = new List<string>();
          if (image.LayOutType != null)
          {
              Model.imageLayOutModel.layOutTypeList.TryGetValue(image.LayOutType, out imagelayouttypelist);
          }
          %>
        
        <div class="ajax_com contentshowimagediv" onclick="window.location='/Content/ContentDetail/<%=image.ID%>'">
            <%if (imagelayouttypelist != null && imagelayouttypelist.Count > 0)
              {
                  int width = 0, height = 0;
                  for (int i = 0; i < imagelayouttypelist.Count; i++)
                  {
                      width = int.Parse(imagelayouttypelist[i].Split(',')[0]);
                      height = int.Parse(imagelayouttypelist[i].Split(',')[1]);
                  %>
                <div style="width:<%=width%>px;height:<%=height%>px;float:left;margin-right:2px;margin-bottom:2px;overflow:hidden; cursor:pointer;">
                    <img src="<%=Comm.getImagePath(image.ImageList[i].ImageUrl, ImageType.Original) %>" width="0" height="0" onload="getContentImageFristLoad($(this))"/>
                </div>
            <%}
              }%>
        </div>
    <%} %>
    <%if (image.Content != null && image.Content != "")
      {
          image.Content = Comm.getSubStringToIndex(image.Content, 100);
          image.Content = Comm.replaceToN(image.Content);
          %>
        <div class="ajax_com">
            <span><a href="/Content/ContentDetail/<%=image.ID%>"><%=image.Content%></a></span>
        </div>
        <%
      }%>
<%--    <div class="ajax_com" style="padding:0px 15px;">
        <span id="contentlikeshowlikename<%=Model.contentobj.ID %>" class="gray01"><%=Model.contentobj.LikeCount%><%=Model.likename%></span>
    </div>
    <div style="clear:both"></div>
    <div class="ajax_com" style="padding:0px 15px;" id="contentlikemember<%=Model.contentobj.ID %>">
    <%for (int i = 0; i < image.LikeList.Count; i++)
      {%>
        <%if (i > 8) { break; }%>
            <a class="list_ajax"><img src="<%=Comm.getImagePath(image.LikeList[i].MemberIcon,ImageType.Middle) %>" height="25" width="25"/></a>
    <%}%>
    </div>--%>