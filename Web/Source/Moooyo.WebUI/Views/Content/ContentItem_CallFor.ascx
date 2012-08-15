<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%Moooyo.BiZ.Content.CallForContent callfor = (Moooyo.BiZ.Content.CallForContent)Model.contentobj; %>
<div class="ajax_title"><%=Model.typename%><em class="hz"></em></div>
<div class="ajax_content">
    <%if (callfor.ImageList != null && callfor.ImageList.Count > 0)
      {
          List<string> imagelayouttypelist = new List<string>();
          if (callfor.LayOutType != null)
          {
              Model.imageLayOutModel.layOutTypeList.TryGetValue(callfor.LayOutType, out imagelayouttypelist);
          }
          %>
        <div class="ajax_com contentshowimagediv" onclick="window.location='/Content/ContentDetail/<%=callfor.ID%>'">
            <%if (imagelayouttypelist != null && imagelayouttypelist.Count > 0)
              {
                  int width = 0, height = 0;
                  for (int i = 0; i < imagelayouttypelist.Count; i++)
                  {
                      width = int.Parse(imagelayouttypelist[i].Split(',')[0]);
                      height = int.Parse(imagelayouttypelist[i].Split(',')[1]);
                  %>
                <div style="width:<%=width%>px;height:<%=height%>px;float:left;margin-right:2px;margin-bottom:2px;overflow:hidden; cursor:pointer;">
                    <img src="<%=Comm.getImagePath(callfor.ImageList[i].ImageUrl, ImageType.Original) %>" width="0" height="0" onload="getContentImageFristLoad($(this))"/>
                </div>
            <%}
              }%>
        </div>
    <%} %>
    <%if (callfor.Content != null && callfor.Content != "")
      {
          callfor.Content = Comm.getSubStringToIndex(callfor.Content, 100);
          callfor.Content = Comm.replaceToN(callfor.Content);
            %>
    <div class="ajax_com addfont">
        <span><a href="/Content/ContentDetail/<%=callfor.ID%>"><%=callfor.Content%></a></span>
    </div>
    <%
      }%>
