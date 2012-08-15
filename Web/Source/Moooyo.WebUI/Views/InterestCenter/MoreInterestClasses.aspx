<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	兴趣分类
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="c976 clearfix">
    <aside class="asidebox mt32 bor-r3 mr15 fl">
		<% Html.RenderAction("LeftInterest", "InterestCenter");%> 
    </aside><!-- .leftside  end -->
    <section class="inter-conbox mt32 fl">
    	<div class="inter-contips">
            <h2>标记自己的兴趣，遇见对路的人</h2>
            <p><%=Model.allInterestCount%>个兴趣　　 <%=Model.allInterestFansCount%>个人分享兴趣</p>
        </div>
        <div class="interall mt11 clearfix"><h2 class="fl">更多分类<span><!--(<span class="interestClassCount"></span>)</span>--></h2></div>
        <ul class="inter-allicon-list mt5">
        <% foreach (var obj in Model.interestClassListObje)
           { %>
            <li><a href="/InterestCenter/ClasseInterests?icid=<%=obj.ID%>&ictitle=<%=Server.UrlEncode(obj.Title)%>" target="_blank"><img src="<%= Comm.getImagePath(obj.ICONPath, ImageType.Middle) %>" width="70" height="70" title="<%=obj.Title%>" alt="<%=obj.Title%>" border="0" /><br /><%=obj.Title.Length > 5 ? obj.Title.Substring(0, 5) + "<span class=\"letspa--3\">...</span>" : obj.Title%></a></li>
        <% } %>
        </ul>
        <!--Begin paging-->
        <% if (Model.Pagger != null) {
            if (Model.Pagger.PageCount > 1) { %> 
               <% Html.RenderAction("QueryStrPaging", "Shared", new { nowpage = Model.Pagger.PageNo, pagecount = Model.Pagger.PageCount, additionID = Model.Pagger.AdditionParams, url = Model.Pagger.PageUrl }); %> 
           <% } %>
        <% } %>
        <!--End paging-->
    </section>
    <aside class="asidebox-r mt32 fr">
    <% Html.RenderAction("AppPush", "Push");%> 
    <% Html.RenderAction("WeeklyInterestRanking","Push"); %>
    <% Html.RenderAction("GuessYourInterest", "Push"); %>
</aside>
</div>   
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/data_<%=Model.JsVersion %>.js"></script>
</asp:Content>
