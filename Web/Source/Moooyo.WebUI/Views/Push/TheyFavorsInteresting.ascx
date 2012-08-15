<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.InterestListControlModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<!--推兴趣-->
<%if (Model != null && Model.interestListObj.Count > 0)
  { %>
<h3 class="caption-tit mt18" style="text-indent:5px;color:#006600;">喜欢它的人也喜欢... ...</h3>
    <ul class="pic-list clearfix" id="theyinterestingul">
        <% foreach (var obj in Model.interestListObj)
           { %>
            <li style="height:82px; line-height:15px; text-align:center; margin-left:25px; margin-top:15px;"><a href="/InterestCenter/InterestFans?iid=<%=obj.ID%>" target="_blank"><img data-interestid="<%=obj.ID %>" src="<%=Comm.getImagePath(obj.ICONPath, ImageType.Middle) %>" width="50" height="50" title="<%=obj.Title%>" alt="<%=obj.Title%>" border="0" /><%=obj.Title.Length > 7 ? obj.Title.Substring(0, 7) + "<span class=\"letspa--3\">...</span>" : obj.Title%></a></li>
        <% } %>
    </ul>
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
    <script type="text/javascript">
        var interestOverTimeoutID = null;
        $(document).ready(function () {
//            if ($("#interestedAuthorCard").html() == null) { $("<div id=\"interestedAuthorCard\"></div>").appendTo("body"); }
            interestCenter.bindinterestLabel($("#theyinterestingul li img"));
        });
    </script>
<%} %>