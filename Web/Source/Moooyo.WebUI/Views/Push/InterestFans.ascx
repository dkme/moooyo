<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.InterestFansModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<% 
    if (Model != null && Model.fans.Count > 0) {
       %>
<h3 class="caption-tit mt18" style="color:#006600;text-indent:5px;"><span class="fl">新加入的...</span><a href="/InterestCenter/InterestFansGroup<% if(!Model.IsOwner) Response.Write("/" + Model.MemberID); %>?iId=<%=Request.QueryString["iId"] %>" style="float:right;">所有(<%=Model.fanscount %>)</a></h3>
    <ul class="pic-list clearfix" id="interestfansdiv">
        <% foreach (var obj in Model.fans)
           { %>
            <li class="fansli" style="height:90px; line-height:15px; text-align:center; margin-left:25px; margin-top:15px;"><a href="/Content/TaContent/<%=obj.Creater.MemberID%>" target="_blank"><img data_me_id="<%=Model.UserID %>" data_member_id="<%=obj.Creater.MemberID %>" src="<%=Comm.getImagePath(obj.Creater.ICONPath, ImageType.Icon) %>" width="50" height="50" alt="" border="0" /><%=obj.Creater.NickName.Length > 7 ? obj.Creater.NickName.Substring(0, 7) + "<span class=\"letspa--3\">...</span>" : obj.Creater.NickName%></a></li>
        <% } %>
    </ul>
    <script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
    <script language="javascript" type = "text/javascript">
        $().ready(function () {
            MemberInfoCenter.BindDataInfo($("#interestfansdiv li.fansli img"));
        });
    </script>
<%
   } %>