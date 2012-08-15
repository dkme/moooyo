<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.MyInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<% if (Model != null && Model.interests.Count > 0) {
       %>
<h3 class="caption-tit mt18"><span class="fl">我的兴趣(<%=Model.interestcount %>)</span><a href="/InterestCenter/Interests" target="_blank" style="float:right;">(全部)</a></h3>
    <ul class="pic-list clearfix" id="myinterestul">
        <% foreach (var obj in Model.interests)
           { %>
            <li class="interestli" data-interestid="<%=obj.ID %>"><a href="/InterestCenter/InterestFans?iId=<%=obj.ID%>" target="_blank"><img src="<%=Comm.getImagePath(obj.ICONPath, ImageType.Icon) %>" width="50" height="50" alt="" border="0" /></a></li>
        <% } %>
    </ul>
    <script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
    <script language="javascript" type = "text/javascript">
        $().ready(function () {
            interestCenter.bindinterestLabel($("#myinterestul li.interestli"));
        });
    </script>
<%
   } %>