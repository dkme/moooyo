<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	兴趣
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" media="screen" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <span id="interestedAuthorCard"></span>
    <div class="c976 clearfix">
    <aside class="asidebox mt32 bor-r3 mr15 fl">
		<% Html.RenderAction("LeftInterest", "InterestCenter");%> 
    </aside><!-- .leftside  end -->
    <section class="inter-conbox mt32 fl">
    	<div class="inter-contips">
            <h2>标记自己的兴趣，遇见对路的人</h2>
            <p><%=Model.allInterestCount%>个兴趣　　 <%=Model.allInterestFansCount%>个人分享兴趣</p>
        </div>
        <div class="interall mt11 clearfix"><h2 class="fl"><%if ((Request.QueryString["ititle"] == null) || (Request.QueryString["ititle"] == ""))
                                       {%><%=Server.UrlDecode(Request.QueryString["ictitle"])%><span>(<%=ViewData["classIdInterestCount"]%>)</span><% }
                                       else {%>搜索结果<span>(<%=ViewData["classIdInterestCount"]%>)</span>&nbsp;&nbsp;<a href="/InterestCenter/ClasseInterests?icid=<%=Request.QueryString["icid"]%>&ictitle=<%=Server.UrlDecode(Request.QueryString["ictitle"])%>">取消搜索</a><% } %></h2>
        <div class="fr mt8">
        <form name="frmInterestSerach" method="get" action="/InterestCenter/ClasseInterests" onsubmit="return interestSerachSubmit()">
        <input type="hidden" name="icid" value="<%=Request.QueryString["icid"]%>" />
        <input type="hidden" name="ictitle" value="<%=Request.QueryString["ictitle"]%>" />
        <input type="text" size="35" maxlength="100" name="ititle" id="ititle" value="<%if((Request.QueryString["ititle"]==null)||(Request.QueryString["ititle"]=="")){Response.Write("输入兴趣名称");}else{Response.Write(Server.UrlDecode(Request.QueryString["ititle"]));} %>" class="fl" /><input type="submit" class="ml15 btn fl" value="搜索" /></form>
        </div></div><div>
        <a href="/InterestCenter/AddInterest" class="add-myinter mt20 fl"><i></i>新增</a>
        <ul class="add-myinter-list fl" id="interestContainer">
        <% foreach (var obj in Model.interestListObj)
           { %>
            <li data-interestid="<%=obj.ID %>"><a href="/InterestCenter/InterestFans?iid=<%=obj.ID%>" target="_blank"><img src="<%= Comm.getImagePath(obj.ICONPath, ImageType.Middle) %>" width="70" height="70" title="<%=obj.Title%>" alt="<%=obj.Title%>" border="0" /><br /><%=obj.Title.Length > 5 ? obj.Title.Substring(0, 5) + "<span class=\"letspa--3\">...</span>" : obj.Title%></a></li>
        <% } %>
        </ul>
        </div>
        <div style="padding-top:5px; clear:both; margin-left:35px;">
        <!--Begin paging-->
        <% if (Model.Pagger != null) {
            if (Model.Pagger.PageCount > 1) { %> 
               <% Html.RenderAction("QueryStrPaging", "Shared", new { nowpage = Model.Pagger.PageNo, pagecount = Model.Pagger.PageCount, additionID = Model.Pagger.AdditionParams, url = Model.Pagger.PageUrl }); %> 
           <% } %>
        <% } %>
        <!--End paging-->
        </div>
    </section>
    <aside class="asidebox-r mt32 fr">
        <% Html.RenderAction("AppPush", "Push");%> 
        <% Html.RenderAction("WeeklyInterestRanking","Push"); %>
        <% if(Model.IsOwner) Html.RenderAction("GuessYourInterest", "Push"); %>
    </aside>
</div>   
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/data_<%=Model.JsVersion %>.js"></script>
<script type='text/javascript' src='/Scripts/jquery.autocomplete2.js'></script>
<script type="text/javascript">
    var interestOverTimeoutID = null, uploadpath = "<%=Model.UploadPath %>";
    $(document).ready(function () {
        setFocuseEmptyInput($("#ititle"), "输入兴趣名称");
        $("#ititle").autocomplete("/InterestCenter/GetAllInterestTitles", {
            matchContains: true,
            autoFill: false,
            mustMatch: false,
            scrollHeight: 210,
            max: 30
        });
        interestCenter.bindinterestLabel($("#interestContainer li"));
    });
    function interestSerachSubmit() {
        var ititle = $("#ititle").val();
        if ((ititle == "输入兴趣名称") || ititle == "") {
            $("#ititle").focus();
            return false;
        }
        return true;
    }
</script>
</asp:Content>
