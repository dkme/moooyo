<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.InterestWenWenModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<% if (Model != null && Model.wenwens.Count > 0) {
       %>
<h3 class="caption-tit mt18" style="text-indent:5px;color:#006600;">最新话题：</h3>
<ul class="their-ask-list mt11 clearfix" id="ulqurestion" name="ulqurestion"><%
       foreach (var obj in Model.wenwens)
       {
           if (obj.Content != null)
           {
               string content = obj.Content;
               content = content.Length > 18 ? content.Substring(0, 18) + "..." : content;
               if (obj.ContentImage != null && obj.ContentImage != "")
               {
                   content = content + "[图]";
               }
           %>
    <li title="<%=content %>" style="height:30px;line-height:30px;"><img class="infos" data_me_id="<%=Model.UserID %>" data_member_id="<%=obj.Creater.MemberID %>" src="<%=Comm.getImagePath(obj.Creater.ICONPath,ImageType.Icon) %>" style="margin-left:25px;width:30px;height:30px;"/><a style="color:#333;" target="_blank" href="/WenWen/ShowWenWen?wwid=<%=obj.ID %>"><%=content%></a></li>
           <%
           }
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