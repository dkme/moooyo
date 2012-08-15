<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	兴趣粉丝
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="c976 clearfix">
    <aside class="asidebox mt32 bor-r3 mr15 fl">
		<% Html.RenderAction("LeftInterest", "InterestCenter", new { memberID = Model.MemberID });%>
    </aside>
    <section class="inter-conbox mt32 fl" style="margin-top:0px;">
        <article>
        <div class="mt32 clearfix"><h3 class="cblue f14 mr15 fl">大家关于它的问题（<%=Model.wenwens.Count %>）</h3><a target="_blank" href="/WenWen/AddWenWens/<%=Request.QueryString["iid"].ToString()%>" class=" fr radius3 btn">&nbsp;提问难倒大家&nbsp;</a></div>
        <ul class="forinter-ask-list mt20">
        <%if (Model != null && Model.wenwens.Count > 0) {
              foreach (var obj in Model.wenwens)
              {
                  
                  %>
            <li class="greenbg clearfix" name="wenwenMemberInfo">
            
                <i class="q-icon"></i><img src="<%=Comm.getImagePath(obj.Key.Creater.ICONPath, ImageType.Icon) %>" data_me_id="<%=Model.UserID %>" data_member_id="<%=obj.Key.Creater.MemberID %>" name="wenwenMemberInfoArea" class="fl mr15  wwimage"/>
                <div class="fl mr15 wwcontent"><%=obj.Key.Content%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=Comm.getTimeSpan(obj.Key.CreatedTime)%><br /><b id="<%=obj.Key.ID %>answercount" name="<%=obj.Key.ID %>answercount" class="mr15 fl"><a onclick="WenWenControl.showanswer('<%=Model.UserID %>','<%=obj.Key.ID %>',5,1,false)"><%if (obj.Value.Count > 0)
                                                                                                                                                                                                      { %>(<%=obj.Value.Count%>个回复)<%} %></a></b><i class="more-icon mt6 mr15" onclick="WenWenControl.showanswer('<%=Model.UserID %>','<%=obj.Key.ID %>',5,1,false)"></i><span class="showmanager" style="display:none;"><a class="my-answer fl mr15" onclick="WenWenControl.showtext('<%=Model.UserID %>','<%=Request.QueryString["iid"].ToString()%>','<%=obj.Key.ID %>')"><i></i>我来回复</a><a href="#" class="my-enjoy fl mr15"><i></i>转发考考微博好友</a></span></div>
            <div id="<%=obj.Key.ID %>text" class="answer-frm mt18">
                
            </div>
            <ul id="<%=obj.Key.ID %>" class="for-inter-answer-list clearfix" name="showwenwenanswermember">
                <%if (ViewData["openanswer"] == "true" && obj.Key.ID == Request.QueryString["wenwenid"])
                  {
                      if (obj.Value.Count > 0)
                      {
                          int i = 0;
                          foreach (var objs in obj.Value)
                          {
                              i++;
                              if (i < 5)
                              {
                      %>
                <li class="clearfix">
                <i class="a-icon"></i><img src="<%=Comm.getImagePath(objs.Creater.ICONPath,ImageType.Icon) %>" class="fl mr15 wenwenmimg" data_me_id="<%=Model.UserID %>" data_member_id="<%=objs.Creater.MemberID %>" name="showwenwenanswermemberimg"/>
                <p><%=objs.Content%></p>
                <div class="ml60 c999"><b class="mr52" style="display:block;float:left;"><%=Comm.getTimeSpan(objs.CreatedTime)%></b><span class="showmanager" style="display:none;float:left;"><a href="#" class="mr52 c999">有用</a><a class="c999" onclick='actionprovider.opencalladmin("<%=Model.MemberID%>",1)'>举报</a></span></div>
                </li>
                      <%}
                              else { break; }
                          }
                              if (obj.Value.Count > 5)
                              {
                                  %><p class="tright"><a onclick="WenWenControl.showanswer('<%=obj.Key.ID %>',5,2,true)">更多<%=(obj.Value.Count-5) %></a></p><%
                              }
                      }
                  } %>
              </ul>
            </li>
                  <%
              }
          } %>
        </ul>
        </article>
        <!--Begin paging-->
        <% if (Model.Pagger != null) {
            if (Model.Pagger.PageCount > 1) { %> 
               <% Html.RenderAction("QueryStrPaging", "Shared", new { nowpage = Model.Pagger.PageNo, pagecount = Model.Pagger.PageCount, additionID = Model.Pagger.AdditionParams, url = Model.Pagger.PageUrl }); %> 
           <% } %>
        <% } %>
        <!--End paging-->
    </section>
<aside class="asidebox-r mt15 fr">
    <% Html.RenderAction("TheyFavorsInteresting", "Push"); %>
</aside>
</div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/data_<%=Model.JsVersion %>.js"></script>
<script src="/Scripts/jquery.validate.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $().ready(function () {
        $("li.greenbg").mouseenter(function () {
            $(this).children("div.wwcontent").children("span.showmanager").css("display", "block");
        });
        $("li.greenbg").mouseleave(function () {
            $(this).children("div.wwcontent").children("span.showmanager").css("display", "none");
        });
        $("li.clearfix").mouseenter(function () {
            $(this).children("div.c999").children("span.showmanager").css("display", "block");
        });
        $("li.clearfix").mouseleave(function () {
            $(this).children("div.c999").children("span.showmanager").css("display", "none");
        });
        MemberInfoCenter.BindDataInfo($("[name='wenwenMemberInfo'] [name='wenwenMemberInfoArea']"));
        MemberInfoCenter.BindDataInfo($("[name='showwenwenanswermember'] [name='showwenwenanswermemberimg']"));
    });
</script>
</asp:Content>