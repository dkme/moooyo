﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.InterestListControlModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<!--推兴趣-->
<%if (Model != null && Model.interestListObj.Count > 0)
  { %>
<h3 class="caption-tit mt18"><%=Model == null ? "我" : Model.IsOwner ? "我" : "TA"%>喜欢的人都喜欢</h3>
    <ul class="pic-list clearfix" id="theylikeul">
        <% foreach (var obj in Model.interestListObj)
           { %>
            <li data-interestid="<%=obj.ID %>"><a href="/InterestCenter/InterestFans?iid=<%=obj.ID%>"><img src="<%=Comm.getImagePath(obj.ICONPath, ImageType.Icon) %>" width="50" height="50" title="<%=obj.Title%>" alt="<%=obj.Title%>" border="0" /></a></li>
        <% } %>
    </ul>
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
    <script type="text/javascript">
        var interestOverTimeoutID = null;
        $(document).ready(function () {
//            if ($("#interestedAuthorCard").html() == null) { $("<div id=\"interestedAuthorCard\"></div>").appendTo("body"); }
            interestCenter.bindinterestLabel($("#theylikeul li"));
        });
    </script>
<%} %>
