<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.TheyFavorsMemberModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<!--推人-->
<%if (Model!=null&&Model.memberList.Count > 0)
  { %>
<h3 class="caption-tit mt18">喜欢<%=Model == null ? "我" : Model.IsOwner ? "我" : "TA"%>的人还喜欢</h3>
    <ul class="pic-list clearfix">
    <%foreach (var obj in Model.memberList)
      { %>
        <li><a href="/Member/Ta/<%=obj.ID %>" target="_blank"><img src="<%=Comm.getImagePath(obj.MemberInfomation.IconPath, ImageType.Icon) %>"/></a></li>
    <%} %>
    </ul>
<%} %>


<%--<script language="javascript" type="text/javascript">
    $().ready(function () {
        var ifopen = false;
        $("i.more").click(function () {
            if (!ifopen) {
                $(this).parents("div.list-show").siblings("div.com-inter").slideDown(800);
                ifopen = true;
            } else if (ifopen) {
                $(this).parents("div.list-show").siblings("div.com-inter").slideUp(800);
                ifopen = false;
            }
        });
    });
</script>--%>