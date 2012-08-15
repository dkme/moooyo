<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=Model.Member.Name%>的小编访谈
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="c976">
    <!--个人左面板-->
    <% if (Model.IsOwner)
       {%>
    <% Html.RenderPartial("~/Views/Member/ProfileLeftPanel.ascx");%>
    <% }
       else
       { %>
    <% Html.RenderPartial("~/Views/Member/MemberLeftPanel.ascx");%>  
    <% } %>
    <!--endof 个人左面板-->
    <!--个人顶部块-->
     <% if (!Model.IsOwner)
        {%>
        <% Html.RenderPartial("~/Views/Member/ProfileTopPanel.ascx"); %>
    <% }
        else
        {%>
    <%--<article class="top-title-box mt20 fl" />--%>
    <%} %>
    <!--endof 个人顶部块-->
    <section class="inter-conbox mt32 fl">
    <% if (!Model.IsOwner)
       {%>
    <h2 class="f14 fb">TA的访谈(<%=Model.Member.InterViewCount %>)</h2>
    <%}
       else
       { %>
    <h2 class="f14 fb">访谈(<%=Model.Member.InterViewCount%>)</h2>
    <div class="inter-contips">
    <h2 id="titlemsg">有空的时候，来这里聊自己感兴趣的话题。别只顾埋头往前走，偶尔歇下脚步，给自己做一回人生专访！</h2>
    </div>
    <p class='mt11'>如果您有好的问题，也可以提交给我们<span class='cblue' onclick='actionprovider.opencalladmin("",2);'>编辑部</span>，一经采用，您将收到我们精心准备的礼物哦。</p>
    <%} %>   
    <!--已回答的小编访问列表-->
    <div id="listcontainer" class="mt20">
    <% foreach (var obj in Model.interviewlist)
       { %>
       <div class="talk-box mt20 clearfix" id='interview<%=obj.ID %>'>
       <ul class='talk-list clearfix'>
        <li>小编：<span id='question<%=obj.ID %>'><%=obj.Question%></span></li>
        <li><b class='fl mr15'><%=Model.Member.Name%>：<span id='answer<%=obj.ID %>' edittype='edit'><%=obj.Answer%></span></b>
        <% if (!Model.IsOwner)
           { %>
        <a href="javascript:" id="btncomment<%=obj.ID %>" class="talk-btn fl" onclick="member_i_functions.sendcommentshow('<%=obj.ID %>')"><i></i>评论</a>
        <% }
           else
           { %>
        <span name='shareBox' data-title='分享到' data-type='shareInfos.interviewAnswerShare' data-content1='question<%=obj.ID %>' data-content2='answer<%=obj.ID %>'></span>
        <%} %>
        </li>
        <% if (Model.IsOwner)
        { %>
        <li id='answerbtn<%=obj.ID %>' class="answerbtn fr mr30">
        <a href="javascript:" class="cblue fl mr5" onclick='member_i_functions.editanswer("<%=obj.ID %>")'>修改</a>
        <a href="javascript:" class="cgray" onclick='member_i_functions.delanswer("<%=obj.ID %>")'>删除</a>
        </li>
        <% }%>
        <li><div id='answerarea<%=obj.ID %>' class='answerarea'>
        <textarea id='input<%=obj.ID %>'></textarea>
        <input type='button' value='保存答案' class='btn mt5' onclick='member_i_functions.saveanswer("<%=obj.ID %>")'/>
        </div>
        <div id='commentarea<%=obj.ID %>' class='answerarea'>
        <textarea id='commentinput<%=obj.ID %>'></textarea>
        <input type='button' value='发送' class='btn mt5' onclick='member_i_functions.sendcommenttomember("<%=obj.ID %>","<%=Model.MemberID %>")'/>
        </div>
        </li>
        </ul>
    </div>
    <%} %>
    </div>
    <!--endof 已回答的小编访问列表-->
    <!--系统中随机提取的问题列表-->
    <div id="listcontainer2">
    <% if (Model.IsOwner) { %>
        <% foreach (var obj in Model.systeminterviewlist)
           { %>
        <div class="talk-box mt20 clearfix" id='interview<%=obj.ID %>'>
        <ul class='talk-list clearfix'>
        <li class="fl">小编：<span id='question<%=obj.ID %>'><%=obj.Question%></span></li>
        <li style="width:100px; height:22px; display:block; float:left;"><span name='shareBox' data-title='&nbsp;问问好友' data-type='shareInfos.interestQuestShare' data-content1='question<%=obj.ID%>'></span></li>
        <li style="clear:both"></li>
        <li id='answer<%=obj.ID %>' edittype='new'></li>
        <li id='answerbtn<%=obj.ID %>'>
        <input type='button' value='回答问题' class='btn fr mr30 mt5' onclick='member_i_functions.editanswer("<%=obj.ID%>")'/>
        </li>
        <li><div id='answerarea<%=obj.ID %>' class='answerarea'>
        <textarea id='input<%=obj.ID %>'></textarea>
        <input type='button' value='保存答案' class='btn mt5 fl' onclick='member_i_functions.saveanswer("<%=obj.ID%>")'/>
        <span id='syncBox<%=obj.ID %>' name='syncBox' data-title='同步到'  data-type='shareInfos.interviewAnswerShare' data-content1='question<%=obj.ID%>' data-content2='answer<%=obj.ID%>'></span>
        </div></li>
        </ul>
        </div>
        <%} %>
    <%} %>
    </div>
    <!--endof 系统中随机提取的问题列表-->
    <div class="morelink mt32">
    <% if (!Model.IsOwner)
       { %>
    <span id="moreinvert" class="moreinvert"><a onclick="Invert.interview('<%=Model.MemberID %>',$('#moreinvert'));">邀请Ta完成更多专访</a></span>
    <span class='domelink' id='domelink'><a href="/member/interview/<%=Model.UserID %>" target='_blank'>去完成我自己的访谈</a></span>
    <% } %>
    </div>
    </section>
    <aside class="asidebox-r mt32 fr">
    <% Html.RenderAction("AppPush", "Push");%> 
    <%Html.RenderAction("SameInterestingMemberOverMe", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
    <% if(!Model.IsOwner) Html.RenderAction("TheyFavorsMember", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
    </aside>
</div> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
mid = '<%=Model.MemberID %>';
nickname = '<%=Model.Member.Name %>';
var isMestr = '<% =Model.IsOwner%>';
isMe = false;
if (isMestr == "True") isMe = true;
$().ready(function () {
    if (isMe) {
        $(".talk-box").mouseover(function () {
            $('.answerbtn', this).show();
            $('.shareto_toolbox', this).css("visibility", "visible");
        });

        $(".talk-box").mouseout(function () {
            $('.answerbtn', this).hide();
            $('.shareto_toolbox', this).css("visibility", "hidden");
        });
        //绑定转发到微博
        microConnOperation.bindShareBox();
        //绑定同步到微博
        microConnOperation.bindSyncBox();
    }
});
</script>
</asp:Content>
