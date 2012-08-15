<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.WeeklyInterestRankingModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<% if (Model != null && Model.rankinglist.Count > 0 && Model.interestlist.Count > 0)
   { %>
<h3 class="caption-tit mt18">本周最热</h3>
<ul class="hot-inter-list mt11 clearfix" id="weeklyinterestul">
<%
       for (int i = 0; i < Model.interestlist.Count; i++)
       {
           if (i < Model.interestlist.Count)
           {
               var obj1 = Model.rankinglist[i];
               var obj2 = Model.interestlist[i];
      %>
    <li class="clearfix">
        <i class="index"><%=obj1.PositionInRanking%></i>
        <div class="user"><a href="/InterestCenter/InterestFans?iid=<%=obj2.ID%>" target="_blank"><img src="<%=Comm.getImagePath(obj2.ICONPath,ImageType.Icon) %>" data-interestid="<%=obj2.ID %>"/></a><a href="/InterestCenter/InterestFans?iid=<%=obj2.ID%>"><%=obj2.Title.Length > 8 ? obj2.Title.Substring(0, 8) + "<span class=\"letspa--3\">...</span>" : obj2.Title%></a></div>
    </li>
<%}
       }%>
</ul>
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
    <script type="text/javascript">
        var interestOverTimeoutID = null;
        $(document).ready(function () {
            interestCenter.bindinterestLabel($("#weeklyinterestul img"));
        });
    </script>
<%
   }%>
