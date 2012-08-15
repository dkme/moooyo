<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<div class="about_self" style="width:162px;">
    <div class="about_t"><em><img src="/pics/about_title.png" /></em></div>
    <div class="about_com">
    <%=Model.Member.Age != "问我" ? "<span>" + Model.Member.Age + "岁</span>" : ""%>
    <%=Model.Member.City != "问我" ? "<span>" + Model.Member.City + "</span>" : ""%>
    </div>
    <div class="about_com">
    <%=Model.Member.Height != "问我" ? "<span>" + Model.Member.Height + "cm</span>" : "" %>
    <%=Model.Member.SexStr != "问我" ? "<span>" + Model.Member.SexStr + "</span>" : ""%>
    </div>
    <div class="about_com"><%=Model.Member.Career != "问我" ? "<span>" + Model.Member.Career + "</span>" : ""%></div>
    <div id="usermessagediv" data-ifopen="false" style="">
    <div class="about_com">
    <%=(Model.Member.Star != "问我") ? "<span>" + Model.Member.Star + "</span>" : "" %>
    </div>
    </div>
    <div class="about_com"><a class="blue02">更多…</a></div><%-- onclick="showmoremessaeg($(this))"--%>
    <div class="about_map"><img src="http://maps.google.com/maps/api/staticmap?center=<%= Model.Member.Lat %>,<%= Model.Member.Lng %>&zoom=13&size=150x100&maptype=roadmap&markers=color:red|label:A|<%= Model.Member.Lat %>,<%= Model.Member.Lng %>&sensor=false" /></div>
    <div class="about_com">与你相距 <%= Moooyo.WebUI.Models.DisplayObjProvider.GetWeDistance(Model.UserID, Model.MemberID)%></div>
</div>