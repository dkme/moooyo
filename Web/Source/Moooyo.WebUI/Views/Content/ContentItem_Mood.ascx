<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%Moooyo.BiZ.Content.MoodContent mood = (Moooyo.BiZ.Content.MoodContent)Model.contentobj; %>
<div class="ajax_title"><%=Model.typename%><em class="xqbh"></em></div>
<div class="ajax_content">
    <%if (mood.ImageList != null && mood.ImageList.Count > 0)
      {
          List<string> imagelayouttypelist = new List<string>();
          if (mood.LayOutType != null)
          {
              Model.imageLayOutModel.layOutTypeList.TryGetValue(mood.LayOutType, out imagelayouttypelist);
          }
          %>
        <div class="ajax_com contentshowimagediv">
        <%if (imagelayouttypelist != null && imagelayouttypelist.Count > 0)
          {
              int width = 0, height = 0;
              for (int i = 0; i < imagelayouttypelist.Count; i++)
              {
                  width = int.Parse(imagelayouttypelist[i].Split(',')[0]);
                  height = int.Parse(imagelayouttypelist[i].Split(',')[1]);
              %>
            <div style="width:<%=width%>px;height:<%=height%>px;float:left;margin-right:2px;margin-bottom:2px;overflow:hidden;">
                <img src="<%=Comm.getImagePath(mood.ImageList[i].ImageUrl, ImageType.Original) %>" width="0" height="0" onload="getContentImageFristLoad($(this))"/>
            </div>
        <%}
          }%>
        </div>
    <%} %>
    <%if (mood.Content != null && mood.Content != "")
      { %>
    <div class="ajax_com addfont">
        <span><%=mood.Content%></span>
    </div>
    <%} %>
<%--    <div class="ajax_com" style="padding:0px 15px;">
        <span id="contentlikeshowlikename<%=Model.contentobj.ID %>" class="gray01"><%=Model.contentobj.LikeCount%><%=Model.likename%></span>
    </div>
    <div style="clear:both"></div>
    <div class="ajax_com" style="padding:0px 15px;" id="contentlikemember<%=mood.ID %>">
    <%for (int i = 0; i < mood.LikeList.Count; i++)
      {%>
        <%if (i > 8) { break; }%>
            <a class="list_ajax"><img src="<%=Comm.getImagePath(mood.LikeList[i].MemberIcon,ImageType.Middle) %>" height="25" width="25"/></a>
    <%}%>
    </div>--%>
