<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%Moooyo.BiZ.Content.IWantContent iwant = (Moooyo.BiZ.Content.IWantContent)Model.contentobj; %>
<div class="ajax_title"><%=Model.typename%><em class="jh"></em></div>
<div class="ajax_content">
    <%if (iwant.ImageList != null && iwant.ImageList.Count > 0)
      {
          List<string> imagelayouttypelist = new List<string>();
          if (iwant.LayOutType != null)
          {
              Model.imageLayOutModel.layOutTypeList.TryGetValue(iwant.LayOutType, out imagelayouttypelist);
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
                <img src="<%=Comm.getImagePath(iwant.ImageList[i].ImageUrl, ImageType.Original) %>" width="0" height="0" onload="getContentImageFristLoad($(this))"/>
            </div>
        <%}
          }%>
        </div>
    <%} %>
    <%if (iwant.Content != null && iwant.Content != "")
      { %>
    <div class="ajax_com addfont">
        <span><%=iwant.Content%></span>
    </div>
    <%} %>
