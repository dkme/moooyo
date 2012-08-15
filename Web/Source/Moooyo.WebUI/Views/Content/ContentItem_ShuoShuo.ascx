<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%Moooyo.BiZ.Content.SuiSuiNianContent suosuo = (Moooyo.BiZ.Content.SuiSuiNianContent)Model.contentobj;%>
<div class="ajax_title"><%=Model.typename%><em class="xf"></em></div>
<div class="ajax_content">
    <%if (suosuo.ImageList != null && suosuo.ImageList.Count > 0)
      {
          List<string> imagelayouttypelist = new List<string>();
          if (suosuo.LayOutType != null)
          {
              Model.imageLayOutModel.layOutTypeList.TryGetValue(suosuo.LayOutType, out imagelayouttypelist);
          }
          %>
        <div class="ajax_com contentshowimagediv" onclick="window.location='/Content/ContentDetail/<%=suosuo.ID%>'">
            <%if (imagelayouttypelist != null && imagelayouttypelist.Count > 0)
              {
                  int width = 0, height = 0;
                  for (int i = 0; i < imagelayouttypelist.Count; i++)
                  {
                      width = int.Parse(imagelayouttypelist[i].Split(',')[0]);
                      height = int.Parse(imagelayouttypelist[i].Split(',')[1]);
                  %>
                <div style="width:<%=width%>px;height:<%=height%>px;float:left;margin-right:2px;margin-bottom:2px;overflow:hidden; cursor:pointer;">
                    <img src="<%=Comm.getImagePath(suosuo.ImageList[i].ImageUrl, ImageType.Original)%>" width="0" height="0" onload="getContentImageFristLoad($(this))"/>
                </div>
            <%}
              }%>
        </div>
    <%} %>
    <%if (suosuo.Content != null && suosuo.Content != "")
      {
          suosuo.Content = Comm.getSubStringToIndex(suosuo.Content, 100);
          suosuo.Content = Comm.replaceToN(suosuo.Content);
          %>
        <div class="ajax_com addfont">
            <span><a href="/Content/ContentDetail/<%=suosuo.ID%>"><%=suosuo.Content%></a></span>
        </div>
    <%
      }%>
<%--    <div class="ajax_com" style="padding:0px 15px;">
        <span id="contentlikeshowlikename<%=Model.contentobj.ID %>" class="gray01"><%=Model.contentobj.LikeCount%><%=Model.likename%></span>
    </div>
    <%if (suosuo.LikeList.Count > 0)
      { %>
    <div style="clear:both"></div>
    <div class="ajax_com" style="padding:0px 15px;" id="contentlikemember<%=Model.contentobj.ID %>">
    <%for (int i = 0; i < suosuo.LikeList.Count; i++)
          {
              if (i > 10) { break; }%>
        <a class="list_ajax" href="/Content/TaContent/<%=suosuo.LikeList[i].MemberID %>/all/1" target="_blank"><img src="<%=Comm.getImagePath(suosuo.LikeList[i].MemberIcon,ImageType.Middle) %>" height="25" width="25"/></a>
    <%}%>
    </div>
    <%}%>--%>
