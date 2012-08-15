<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<% Moooyo.BiZ.Content.MemberContent member = (Moooyo.BiZ.Content.MemberContent)Model.contentobj; %>
<div class="ajax_title"><%=Model.typename%><em class="jr"></em></div>
<div class="ajax_content">
<%if (member.Type == "1" || member.Type == "修改个人头像")
  { %>
        <div class="ajax_com">
            <div class="ajax_pic" style="width:260px; height:0px;">
                <% string memberAvatar = (member.MemberAvatar != null && member.MemberAvatar != "") ? member.MemberAvatar : member.Creater.ICONPath; %>
                <img src="<%=Comm.getImagePath(memberAvatar, ImageType.Original) %>" onload="getContentImageFristLoad($(this))" onclick="window.open('/Content/TaContent/<%=member.Creater.MemberID %>/all/1')"/>
            </div>
        </div>
<%} %>
<%else if (member.Type == "0" || member.Type == "设置个人位置")
  {
      double lat = member.Lat;
      double lng = member.Lng; %>
        <div class="ajax_com" style="width:260px; height:260px;">
            <img src="http://maps.google.com/maps/api/staticmap?center=<%=lat %>,<%=lng %>&zoom=11&size=260x260&maptype=roadmap&markers=color:red|label:A|<%=lat %>,<%=lng %>&sensor=false" onload="getContentImageFristLoad($(this))" onclick="window.open('/Content/TaContent/<%=member.Creater.MemberID %>/all/1')"/>
        </div>
        <div class="ajax_com">
            <div class="ajax_pic"><%=Moooyo.WebUI.Models.DisplayObjProvider.GetWeDistance(Model.UserID, member.MemberID)%></div>
        </div>
<%} %>