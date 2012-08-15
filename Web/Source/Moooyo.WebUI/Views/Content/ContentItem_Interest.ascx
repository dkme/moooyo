<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<% Moooyo.BiZ.Content.InterestContent interest = (Moooyo.BiZ.Content.InterestContent)Model.contentobj; %>
<div class="ajax_title"><%=Model.typename%><em class="jr"></em></div>
<div class="ajax_content">
    <div class="ajax_com">
        <div class="ajax_xiqu">
            <a href="/InterestCenter/ShowInterest/<%=interest.Interest.ID %>">
                <span style="width:80px; height:80px;">
                    <img src="<%=Comm.getImagePath(interest.Interest.ICONPath, ImageType.Middle) %>" width="49" onload="getContentImageFristLoad($(this))"/>
                </span>
            </a>
            <span class="blue02"><a href="/InterestCenter/ShowInterest/<%=interest.Interest.ID %>"><%=interest.Interest.Title.Length > 5 ? interest.Interest.Title.Substring(0, 5) + ".." : interest.Interest.Title%></a></span>
        </div>
<%if (interest.Type == "0" || interest.Type == "添加兴趣组")
  {
      interest.Content = Comm.getSubStringToIndex(interest.Content, 40);
      interest.Content = Comm.replaceToN(interest.Content);
      %>
    <div class="ajax_dis">
        <span><%=interest.Content%></span>
    </div>
<%
  }%>
<%else if (interest.Type == "1" || interest.Type == "创建兴趣组")
  {
      interest.Interest.Content = Comm.getSubStringToIndex(interest.Interest.Content, 40);
      interest.Interest.Content = Comm.replaceToN(interest.Interest.Content);
      %>
    <div class="ajax_dis">
        <span><%=interest.Interest.Content%></span>
    </div>
<%
  }%>
    </div>