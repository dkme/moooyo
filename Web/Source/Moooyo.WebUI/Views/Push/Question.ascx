<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.InterestWenWenModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<% if (Model != null && Model.wenwens.Count > 0) {
       %>
<h3 class="caption-tit mt18"><b class="fl">他们在说</b><a onclick="WenWenControl.showrandomwenwen('<%=Model.UserID %>')" class="fr c999">换一批</a></h3><ul class="their-ask-list mt11 clearfix" id="ulqurestion" name="ulqurestion"><%
       foreach (var obj in Model.wenwens)
       {
           string content = obj.Content;
           content = content.Length > 18 ? content.Substring(0, 18) + "..." : content;
           if (obj.ContentImage != null && obj.ContentImage != "")
           {
               content = content + "[图]";
           }
           %>
    <li title="<%=content %>"><img class="infos" data_me_id="<%=Model.UserID %>" data_member_id="<%=obj.Creater.MemberID %>" src="<%=Comm.getImagePath(obj.Creater.ICONPath,ImageType.Icon) %>" /><a target="_blank" title="<%=obj.Content %>" href="/WenWen/ShowWenWen?wwid=<%=obj.ID %>"><%=content%></a></li>
           <%
       }
       %></ul><br />
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
<script language="javascript" type = "text/javascript">
    var interestOverTimeoutID = null;
    $().ready(function () {
        MemberInfoCenter.BindDataInfo($("#ulqurestion img.infos"));
    });
</script>
<%
   } %>