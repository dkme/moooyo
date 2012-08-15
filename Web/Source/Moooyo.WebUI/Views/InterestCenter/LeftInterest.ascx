<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%@ Import Namespace="Moooyo.BiZ.InterestCenter" %>

<h2 class="inter-tit">兴趣中心</h2>
<ul class="inter-list mt5">
    <li id="interestClassAllInterests"><a href="/InterestCenter/AllInterests" title="全部兴趣">全部兴趣(<%=Model.allInterestCount%>)</a></li>
    <% foreach (var obj in Model.interestClassListObje)
       { %>
        <li id="interestClass<%=obj.ID%>"><a href="/InterestCenter/ClasseInterests?icid=<%=obj.ID%>&ictitle=<%=Server.UrlEncode(obj.Title)%>" title="<%=obj.Title%>"><%=obj.Title%>(<%=InterestFactory.GetClassInterestCount(obj.Title)%>)</a></li>
    <% } %>
    <li><a href="/InterestCenter/MoreInterestClasses" title="更多">更多</a></li>
</ul>
<div class="myinter-box mt20">
    <h3><a href="/InterestCenter/Interests<% if(!Model.IsOwner) Response.Write("/" + Model.MemberID); %>" target="_blank"><%=Model.IsOwner ? "我" : "TA"%>的兴趣(<%=Model.memberInterestCount%>)</a></h3>
        <ul class="myinter-list mt5 clearfix" id="interestLeftContainer">
        <% foreach (var obj in Model.interestListObj)
           { %>
            <li data-interestid="<%=obj.ID %>"><a href="/InterestCenter/InterestFans<% if(!Model.IsOwner) Response.Write("/" + Model.MemberID); %>?iid=<%=obj.ID%>" target="_blank"><img src="<%= Comm.getImagePath(obj.ICONPath,ImageType.Icon) %>" width="25" height="25" title="<%=obj.Title%>" alt="<%=obj.Title%>" border="0" /></a></li>
        <% } %>
        </ul>
</div>
<script language="javascript" type="text/javascript">
    $().ready(function () {
        interestCenter.bindinterestLabel($("#interestLeftContainer li"));
        var url = location.href.toLowerCase();
        $(".inter-list li").each(function () {
            $(this).removeClass('current');
        });
        $(".inter-list li").each(function () {
            if (url.indexOf(this.id.toLowerCase().substring(13, this.id.length)) > 0) {
                $(this).addClass('current');
            }
        });
    });
</script>