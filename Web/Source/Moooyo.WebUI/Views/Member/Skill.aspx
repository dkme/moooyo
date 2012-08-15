<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberSkillModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%@ Register src="../Push/SkillPush.ascx" tagname="SkillPush" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=Model.Member.Name%>的才艺
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--    <form id="form1" runat="server">--%>
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
       <%--<h2 class="fb f14">TA的才艺(<%=Model.Member.SkillCount %>)</h2>--%>
    <%}
       else
       { %>
    <h2 class="f14 fb">才艺(<%=Model.Member.SkillCount %>)</h2>
    <div class="inter-contips">
    <h2 id="H1">在这里你可以便捷的找到并结识大量来自各地的交换伙伴，通过在线社区相互学习，互相帮助，彼此促进。</h2>
    </div>
    <%} %>   
    <div id="titlemsg"></div>
    <!--懂得的才艺-->
    <div id="listcontainer" class="mt20">
    <div class='skills'>
        <p class='cblue f14'><%= Model.IsOwner ? "我" : "TA" %>懂的（<%=Model.knownlist.Count%>）：</p>
        <% foreach (var obj in Model.knownlist)
           {%>
           <div class='skill' id='skill<%=obj.ID %>'>
           <%= Model.IsOwner ? "我" : "TA" %>会 <span class='labelname cblue' id='skillname<%=obj.ID %>'><%=obj.SkillName%></span>
           <span class='labellevel'>，<%=obj.SkillLevel%></span>
            <% if (obj.Payment != "")
              { %>        
            <span class='labelpayment'>，<%=obj.Payment%></span>";
            <%} %>
            <% if (Model.IsOwner)
               { %>
               <span id='editbtns<%=obj.ID %>'><a onclick='member_i_functions.delskill("<%=obj.ID %>","<%=Model.UserID %>")' class='linkT1 cgray'>删除</a></span>
            <% }
               else
               {  %>
               <span class='invertbtn'><input type='button' value='跟TA学' class='learnbtn ' onclick='Invert.wantToLearnFromYou("<%=Model.MemberID %>",$("#skillname<%=obj.ID %>").html(),$(this).parent());'/></span>
            <% } %>
            <%if (Model.Tkownlist.Count > 0 && Model.IsOwner)
              {
                  int i = 0;
                  foreach (var tkownobj1 in Model.Tkownlist) { if (tkownobj1.SkillName == obj.SkillName) { i++; } }
                  if (i > 0)
                  {
                  %><div class="clearfix"><a class="c666 ml30" href="javascript:;">TA想学</a><ul class="pic25-list clearfix" name="learnMemberInfo"><%
                  foreach (var tkownobj1 in Model.Tkownlist)
                  {
                      if (tkownobj1.SkillName == obj.SkillName) 
                      {%>
                  <li class="skillpush" name="learnMemberInfoArea" data_me_id="<%=Model.UserID %>" data_member_id="<%=tkownobj1.MemberID %>"><a href="/Member/Skill/<%=tkownobj1.MemberID%>"><img src="<%=Comm.getImagePath(tkownobj1.MemberInfomation.IconPath, ImageType.Icon)%>" /></a></li>
                    <%}
                  }
                  %></ul></div><%
                  }
              }
               if (Model.Tkownlist.Count > 0 && !Model.IsOwner)
               {
                  int i = 0;
                  foreach (var tkownobj2 in Model.Tkownlist) { if (tkownobj2.SkillName == obj.SkillName) { i++; } }
                  if (i > 0)
                  {
                  %><div class="clearfix"><a class="c666 ml30" href="#">还有谁会？</a><ul class="pic25-list clearfix" name="moreKnowMemberInfo"><%
                      foreach (var tkownobj2 in Model.Tkownlist)
                      {
                       if (tkownobj2.SkillName == obj.SkillName)
                       {%>
     <li class="skillpush" data_me_id="<%=Model.UserID %>" data_member_id="<%=tkownobj2.MemberID %>" name="moreKnowMemberInfoArea"><a href="/Member/Skill/<%=tkownobj2.MemberID%>"><img src="<%=Comm.getImagePath(tkownobj2.MemberInfomation.IconPath,ImageType.Icon)%>" /></a></li>
                    <%}
                   }
                  %></ul></div><%
                  }
               }%>
            </div>
        <% } %>
        <% if (Model.knownlist.Count == 0)
           {
               if (Model.IsOwner)
               {%>
                   <p class='nonemsg'>还没有设置我懂得的才艺。赶快设置，让Ta们都来羡慕吧</p>
                <% }else{ %>
                   <p class='nonemsg'>Ta还没有懂得的才艺，我们来鄙视Ta</p>
                <%} %>
         <% } %>
        <% if (Model.IsOwner)
            { %>
            <a id='addiknowbtn' onclick='$("#addiknowpanel").show();$(this).hide();' class='ml20 linkT1'>添加我会</a>
            <div id='addiknowpanel' class='knowpanel'>我懂得 <input type='text' id='inputknows' class='inputknows' onclick='actionprovider.openskill("inputknows");'/> ,说明 <input type='text' id='inputknowslevel' class='inputknowslevel'/> 。<input type='button' value='保存' class='savebtn' onclick='member_i_functions.addknowskill();'/> <a onclick='$(this).parent().hide();$("#addiknowbtn").show();' class='linkT1 titlemsgsmall'>下次吧</a></div>
         <% } %>
        </div>
        <!--endof 懂得的才艺-->
        <!--想学的才艺-->
        <div class='skills'>
        <p class='cblue f14'><%= Model.IsOwner ? "我" : "TA" %>想学（<%=Model.wanttoknowlist.Count%>）：</p>
        <% foreach (var obj in Model.wanttoknowlist)
           {%>
           <div class='skill' id='skill<%=obj.ID %>'>
           <%= Model.IsOwner ? "我" : "TA" %>想学 <span class='labelname cblue' id='skillname<%=obj.ID %>'><%=obj.SkillName%></span>
           <span class='labellevel'>，<%=obj.SkillLevel%></span>
            <% if (obj.Payment != "")
              { %>        
            <span class='labelpayment'>，<%=obj.Payment%></span>";
            <%} %>
            <% if (Model.IsOwner)
               { %>
               <span id='editbtns<%=obj.ID %>'><a onclick='member_i_functions.delskill("<%=obj.ID %>","<%=Model.UserID %>")' class='linkT1 cgray'>删除</a></span>
            <% }
               else
               { %>
               <span class='invertbtn'><input type='button' value='教TA' class='teachbtn' onclick='Invert.wantToTeachYou("<%=Model.MemberID %>",$("#skillname<%=obj.ID %>").html(),$(this).parent());'/></span>
            <% } %>
            
            <%if (Model.Twantkownlist.Count > 0 && Model.IsOwner)
              {
                  int i = 0;
                  foreach (var tkownobj1 in Model.Twantkownlist) { if (tkownobj1.SkillName == obj.SkillName) { i++; } }
                  if (i > 0)
                  {
                  %><div class="clearfix"><a class="c666 ml30" href="#">TA们会</a><ul class="pic25-list clearfix" name="knowMemberInfo"><%
                      foreach (var twantobj1 in Model.Twantkownlist)
                  {
                      if (twantobj1.SkillName == obj.SkillName)
                      {%>
                  <li class="skillpush" name="knowMemberInfoArea" data_me_id="<%=Model.UserID %>" data_member_id="<%=twantobj1.MemberID %>"><a href="/Member/Skill/<%=twantobj1.MemberID%>"><img src="<%=Comm.getImagePath(twantobj1.MemberInfomation.IconPath,ImageType.Icon)%>" /></a></li>

                    <%}
                  }
                  %></ul></div><%
                  }
              }
               if (Model.Twantkownlist.Count > 0 && !Model.IsOwner)
               {
                  int i = 0;
                  foreach (var tkownobj1 in Model.Twantkownlist) { if (tkownobj1.SkillName == obj.SkillName) { i++; } }
                  if (i > 0)
                  {
                  %><div class="clearfix"><a class="c666 ml30" href="#">还有谁想学</a><ul class="pic25-list clearfix" name="moreLearnMemberInfo"><%
                      foreach (var twantobj2 in Model.Twantkownlist)
                  {
                      if (twantobj2.SkillName == obj.SkillName)
                      {%>
                  <li class="skillpush" name="moreLearnMemberInfoArea" data_me_id="<%=Model.UserID %>" data_member_id="<%=twantobj2.MemberID %>"><a href="/Member/Skill/<%=twantobj2.MemberID%>"><img src="<%=Comm.getImagePath(twantobj2.MemberInfomation.IconPath,ImageType.Icon)%>" /></a></li>

                    <%}
                  }
                  %></ul></div><%
                  }
               }%></div>
        <% } %>
        <% if (Model.wanttoknowlist.Count == 0)
           {
               if (Model.IsOwner)
               {%>
                   <p class='nonemsg'>有什么是一直想学也没找到人学习的吗？该快让他们看到</p>
                <% }else{ %>
                   <p class='nonemsg'>Ta什么都懂，没有想要学习的才艺</p>
                <%} %>
         <% } %>
        <% if (Model.IsOwner)
            { %>
            <a id='addwanttoknowbtn' onclick='$("#addwanttoknowpanel").show();$(this).hide();' class='ml20 linkT1'>添加想学</a>
            <div id='addwanttoknowpanel' class='knowpanel'>我想学 <input type='text' id='inputwanttoknows' class='inputknows' onclick='actionprovider.openskill("inputwanttoknows");'/> ,说明 <input type='text' id='inputwanttoknowscomment' class='inputknowslevel'/> 。<input type='button' value='保存' class='savebtn' onclick='member_i_functions.addwanttoknowkill();'/> <a onclick='$(this).parent().hide();$("#addwanttoknowbtn").show();' class='linkT1 titlemsgsmall'>下次吧</a></div>
         <% } %>
        </div>
        <!--endof 想学的才艺-->
    </div>

    <div id="listcontainer2">
    </div>
    <div class="morelink mt32">
    <% if (!Model.IsOwner)
       { %>
    <span id="moreinvert" class="moreinvert"><a onclick="Invert.moreSkills('<%=Model.MemberID %>',$('#moreinvert'));">邀请Ta添加才艺</a></span>
    <span class='domelink' id='domelink'><a href="/member/skill/<%=Model.UserID %>" target='_blank'>去添加我的才艺</a></span>
    <% } %>
    </div>
    </section>
    <aside class="asidebox-r mt32 fr">
        <% Html.RenderAction("AppPush", "Push");%> 
        <% Html.RenderAction("SkillPush", "Push"); %>
    </aside>
</div>
  <%--  </form>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    mid = '<%=Model.MemberID %>'; me = "<%=Model.UserID %>";
    var isMestr = '<% =Model.IsOwner%>';
    isMe = false;
    if (isMestr == "True") isMe = true;
    $().ready(function () {
        //绑定会员标签
        MemberInfoCenter.BindDataInfo($("[name='learnMemberInfo'] [name='learnMemberInfoArea']"));
        MemberInfoCenter.BindDataInfo($("[name='moreLearnMemberInfo'] [name='moreLearnMemberInfoArea']"));
        MemberInfoCenter.BindDataInfo($("[name='knowMemberInfo'] [name='knowMemberInfoArea']"));
        MemberInfoCenter.BindDataInfo($("[name='moreKnowMemberInfo'] [name='moreKnowMemberInfoArea']"));
        /*
        member_i_functions.bindskillstitlemsg($("#titlemsg"), $("#moreinvert"), $("#domelink"));
        member_i_functions.getskills();
        */
    });
</script>
</asp:Content>
