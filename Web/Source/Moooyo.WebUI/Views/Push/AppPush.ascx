<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.ApplicationModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%if(Model!=null){ %>
<h3 class="caption-tit">活动</h3>
<div class="mt8" style="margin:0px; margin-top:5px;"><a href="<%=Model.appmodel.Url %>" target="_blank"><img src="<%=Comm.getImagePath(Model.appmodel.ImagePath1,ImageType.Original) %>" style="width:180px;"/></a></div>
<%} %>