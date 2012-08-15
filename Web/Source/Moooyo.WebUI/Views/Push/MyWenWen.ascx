<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.InterestWenWenModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<% if (Model != null && Model.wenwens.Count > 0) {
       %>
<h3 class="caption-tit mt18" style="color:#006600;text-indent:5px;"><span class="fl">我参与的话题...</span><a href="/InterestCenter/InterestsForMy" target="_blank" style="float:right;">所有(<%=Model.wenwens.Count %>)</a></h3>
<ul class="their-ask-list mt11 clearfix" id="ulqurestion"><%
       int i = 0;
       foreach (var obj in Model.wenwens)
       {
           if (i < 9)
           {
               if (Model.interestTowenwen[i] != null)
               {
                   string content = obj.Content;
                   content = content.Length > 18 ? content.Substring(0, 18) + "..." : content;
                   if (obj.ContentImage!=null&&obj.ContentImage!="")
                   {
                       content = content + "[图]";
                   }
           %>
    <li title="<%=content %>" style="height:30px; line-height:25px; margin-left:25px;"><img class="infos" data-interestid="<%=Model.interestTowenwen[i].ID%>" src="<%=Comm.getImagePath(Model.interestTowenwen[i].ICONPath, ImageType.Icon)%>" width="25" height="25"/><a target="_blank" href="/WenWen/ShowWenWen?wwid=<%=obj.ID%>"><%=content%></a></li>
           <%}
           }
           else { break; }
           i++;
       }
       %></ul><br />
<script language="javascript" type = "text/javascript">
    $(document).ready(function () {
        interestCenter.bindinterestLabel($("#ulqurestion img.infos"));
    });
</script>
<%
   } %>
