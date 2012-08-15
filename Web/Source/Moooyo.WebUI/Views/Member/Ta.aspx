<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberProfileModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%@ Import Namespace="Moooyo.BiZ.InterestCenter" %>
<%@ Import Namespace="Moooyo.BiZ.WenWen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	[米柚] <%=Model.Member.Name%>的主页
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="label01">
<div class="label01-02">
<div class="label01-02-01"></div>
&nbsp;&nbsp;<a href="javascript:;" onclick="Invert.uploadAvatar('<%=Model.MemberID %>',$('.label01'))">Ta还没头像，点击这里邀请Ta传一个！</a>
</div>
</div>
<div class="c976">
    <!--个人左面板-->
    <% if (Model.IsOwner){ Html.RenderPartial("~/Views/Member/ProfileLeftPanel.ascx"); } 
       else {  Html.RenderPartial("~/Views/Member/MemberLeftPanel.ascx"); } %>
    <!--endof 个人左面板-->
    <article class="top-title-box mt32 fl">
    	<div class="clearfix">
            <div class="fl">
                <b class="cblue f18 fb fyahei fl mr15"><a href="/Member/I/<%= Model.Member.ID %>"><%=Model.Member.Name%></a></b>
                <b class="fl mr15 mt6"><i class="vip-icon"></i><%=Model.Member.MemberLevel%></b>
                <b class="fl f14 mt5"><% if (Model.Member.Want != "")
                                         {%>“我想和Ta<%=Model.Member.Want%>”<% }
                                         else
                                         { %>TA还没有写“我想”。<a id='invertiwan' href="javascript:;" onclick="Invert.iWant('<%=Model.MemberID%>',$('#invertiwan'))">邀请TA填写“我想”</a><% } %></b>
            </div>
            <% if (!Model.IsOwner)
               { %>
            <ul class="sayhi-btn-list fr">
                <li class="roundButton1"><a href="javascript:void(0);" onclick="actionprovider.openmsg('<%=Model.MemberID%>')"><i class="circle-icon"></i>私信</a></li>
                <li class="roundButton1"><a onclick='actionprovider.openhi("<%=Model.MemberID%>")' href="javascript:;"><i class="circle-icon"></i>打招呼</a></li>
                <li class="roundButton1" onmouseover="ShowHide('moreToMemberOperat', 'show')" onmouseout="ShowHide('moreToMemberOperat', 'hide')" style="position:relative;"><a href="javascript:;" class="fl">更多</a><i class="arrow-icon2"></i>
                    <div name="moreToMemberOperat" class="moreToMemberOperat">
                    	<ul class="dropList">
                            <li><a href='javascript:;' onclick='actionprovider.opencalladmin("<%=Model.MemberID%>",1)'>举报</a></li>
                            <li><a href='javascript:;' onclick='member_i_functions.disablemember("<%=Model.MemberID%>")'>屏蔽</a></li>
                        </ul>
                    </div>
                </li>
            </ul>
            <%} %>
    	</div>
        <div class="bgf7" style="height:auto; height:auto!important; min-height:130px;">
        	<div class="h30 fl f13">&nbsp;&nbsp;TA的兴趣爱好:&nbsp;&nbsp;</div><div class="h30 fr"><span class="fl">TA的首页地址：&nbsp;</span><a href="javascript:;" target="_self" id="linkCopyUrl" class="fl" style="padding-right:5px;">http://<%=HttpContext.Current.Request.Url.Host.ToString()%>/u/<%=Model.MemberUrl%></a><a href="javascript:;" class="fl mr15" id="copyToClipboardBtn">&nbsp;&nbsp;复制</a></div>
            <div style="padding-left:15px; height:auto; height:auto!important; min-height:30px; clear:both;" id="interestContainer">
            <%if (Model.interestListObje.Count > 0)
              { %>
             <% foreach (var iObj in Model.interestListObje)
                { %>
       	    	<div class="iObj fl h70 w70 m_r15" data-interestid="<%=iObj.ID %>">
                    <div class="main" >
                    	<div class="interest1" onmouseover="showHide('show', 'interestCard<%=iObj.ID%>')" onmouseout="showHide('hide', 'interestCard<%=iObj.ID%>')">
                        <a href="/InterestCenter/InterestFans?iid=<%=iObj.ID%>" target="_blank"><img src="<%= Comm.getImagePath(iObj.ICONPath, ImageType.Middle) %>" width="70" height="70" title="<%=iObj.Title%>" alt="<%=iObj.Title%>" border="0" /></a>
                        </div>
                    </div>
                </div>
             <% } %>
                
                <div class="fl h70 w70 m_r20">
                    <div>
                    	<a href="/InterestCenter/Interests/<%=Model.MemberID%>" target="_blank">
                        <div style="text-align:center; line-height:70px; background-color:#e4e4e4;">更多</div>
                        </a>
                    </div>
                </div>
                
                <% } else { %>
                <span class="c999 ml5">Ta还没有自己的兴趣哦。</span><a id='addInterest' href="javascript:;" onclick="Invert.AddInterest('<%=Model.MemberID%>',$('#addInterest'))">邀请Ta添加兴趣</a>
                <%} %>
                
          	</div>
            <div class="h30 w100ptge clearBoth">&nbsp;&nbsp;与TA的共同兴趣爱好（<%=InterestFactory.GetIAndTACommonInterestCount(Model.UserID, Model.MemberID)%>）</div>
            <div class="h33" id="interestContainer2" style="padding-bottom:8px;">
            <%if (Model.commonInterestListObje.Count < 1) { Response.Write("<span class=\"c999 ml20\">你和Ta还没有共同的兴趣爱好哦~</span>"); } %>
            <% foreach (var cIObj in Model.commonInterestListObje)
               { %>
                <div class="fl h33 w33 m_l20" data-interestid="<%=cIObj.ID %>"><a href="/InterestCenter/InterestFans?iid=<%=cIObj.ID%>" target="_blank"><img src="<%= Comm.getImagePath(cIObj.ICONPath, ImageType.Icon) %>" height="33" width="33" title="<%=cIObj.Title%>" alt="<%=cIObj.Title%>" border="0"></a></div>
            <% } %>
            </div>
        </div>
        
    </article>
    <section class="conbox mt11 fl">
        <div style="position:relative;">
    	<ul class="member-infor-list clearfix">
            <li>性别：<%=Model.Member.Sex==1?"男":"女"%></li>
            <li>年龄：<%=Model.Member.Age != "问我" ? Model.Member.Age + "岁" : Model.Member.Age%></li>
            <li>城市：<%=Model.Member.City %></li>
            <li>职业：<%=Model.Member.Career%></li>
            <li>身高：<%=Model.Member.Height != "问我" ? Model.Member.Height + "cm" : Model.Member.Height%></li>
            <li style="float:right;"><a href="javascript:;" class="cblue" onmouseover="ShowHide('memberInfoMore','show')" onmouseout="ShowHide('memberInfoMore','hide')">更多</a>
            <a id='invertprofile' href="javascript:;" onclick="Invert.profile('<%=Model.MemberID%>',$('#invertprofile'))">邀请Ta完善资料</a>
            </li>
        </ul>
        <div class="clearfix overLabel hidden" name="memberInfoMore">
        <ul class="member-infor-list clearfix">
            <li>薪水：<%=Model.Member.Gainings != "问我" ? Model.Member.Gainings + "元" : Model.Member.Gainings%></li>
            <li>星座：<%=Model.Member.Star%></li>
            <li>故乡：<%=Model.Member.Hometown%></li>
            <li>体型：<%=Model.Member.Figure%></li>
            <li>学历：<%=Model.Member.EducationalBackground%></li>
        </ul>
        <ul class="member-infor-list clearfix">
            <li>居住：<%=Model.Member.LivingStatus%></li>
            <li>已购房：<%=Model.Member.HaveHouse%></li>
            <li>已购车：<%=Model.Member.HaveCar%></li>
        </ul>
        </div>
        </div>
        <%--<div class="h30 fz16 mt fb"><a href="javascript:;">&nbsp;TA的签名档……</a></div>
      <div class="h30 lh mt pb-15"><%=Model.Member.Soliloquize == "" ? "<span class=\"c999 ml15\">TA还没有写签名档。</span> <a id='invertso' href=\"javascript:;\" onclick=\"Invert.soliloquize(\'" + Model.MemberID + "\',$('#invertso'))\">邀请TA填写签名档</a>" : Model.Member.Soliloquize%></div>--%>
      
      <div class="h30 fz16 mt fb"><a href="/Photo/mplist/<%=Model.MemberID%>" target="_blank">&nbsp;TA的照片……（全部）</a></div>
   <% if (Model.photoListObje.Count > 0)
      {
          Response.Write("<div class=\"h112 lh mt pb-15 clearBoth\">");
          foreach (var pObj in Model.photoListObje)
          { %>
      	<div class="h112 w112 fl m_r40 tupian_bj">
        	<div class="h100 w100 m4"><a href="/photo/show/<%=pObj.ID%>" target="_blank"><img src="<%= Comm.getImagePath(pObj.FileName, ImageType.Middle) %>" height="100" width="100" border="0" title="<%=pObj.Title%>" alt="<%=pObj.Title%>"></a></div>
        </div>
   <% }
      }
      else {
          Response.Write("<div class=\"lh mt pb-15 clearBoth\"><span class=\"c999 ml15\">TA还没有上传照片哦。</span><a id='invertphoto' href=\"javascript:;\" onclick=\"Invert.photo(\'" + Model.MemberID + "\',$('#invertphoto'))\">邀请TA上传照片</a>");
      } %>
      </div>
      <div class="h30 fz16 mt fb"><a href="/Member/InterView/<%=Model.MemberID%>" target="_blank">&nbsp;TA的访谈……（全部）</a></div>
      <div class="pb-15">
      <%if (Model.interViewListObje.Count < 1) { Response.Write("<span class=\"c999 ml15\">TA还没有访谈。</span><a id='invertinterview' href=\"javascript:;\" onclick=\"Invert.interview(\'" + Model.MemberID + "\',$('#invertinterview'))\">邀请TA参加访谈</a>"); } %>
   <% foreach (var iVObj in Model.interViewListObje)
      { %>
      <div class="h30 lh mt c666">小编：<%=iVObj.Question%></div>
      <div class="h30 lh">回答：<%=iVObj.Answer%></div>
   <% } %>
   </div>
    </section>
    <aside class="asidebox-r fr" style="overflow:visible";>
     <div class="caption-tit1 mt18 bg-f7f7f7">
        <%if (Model.Member.FansGroupName != null)
          {%>
     	<div class="h30 c0099cc">&nbsp;<strong>TA的<%=Model.Member.FansGroupName.Name.Length > 6 ? Model.Member.FansGroupName.Name.Substring(0, 6) + "<span class=\"letspa--3\">...</span>" : Model.Member.FansGroupName.Name%>们（<%=Model.Member.MemberFavoredMeCount%>）</strong></div>
        <%}
          else
          {%>
          <div class="h30 c0099cc">&nbsp;<strong>TA的粉丝们（<%=Model.Member.MemberFavoredMeCount%>）</strong></div>
        <%} %>
        <div style="clear:both; margin-bottom:5px;">
        <% if (!Model.IsOwner)
        { %>
        	<div style="margin-top:6px; padding:0px 0px 10px 8px; _padding-bottom:0px;">
            <%--提示--%>
            <div class="littip_div">
                <div class="littip ta_lit_tip"></div>
                <img id="ta_lit_tipdel" src="/pics/tip/littip_del.png" alt="" title="知道了，关闭" class="ta_lit_tipdel" />
            </div>
            <%--提示结束--%>
                <div class="clearBoth">
                    <span class="fl">
                       <% if (Moooyo.WebUI.Models.DisplayObjProvider.IsInFavor(Model.UserID, Model.MemberID))
                          { %>
                       <a href="javascript:void(0);" class="delete-btn" onclick="member_i_functions.deletefavormember('<%=Model.MemberID%>',$(this));$(this).addClass('fl')"><i></i>取消</a>
                    <% } else { %>
                       <a class="add-btn" href="javascript:void(0);" onclick="member_i_functions.favormember('<%=Model.MemberID%>',$(this));$(this).addClass('fl')"><i></i>关注</a>
                    <% } %>
                    </span>
                    <a href="javascript:;" class="add-btn2 fl ml5" onclick="presentGlamourValue()">送TA魅力值</a>
                </div>
                <% if (Model.AlreadyLogon && Moooyo.WebUI.Models.DisplayObjProvider.IsInFavor(Model.UserID, Model.MemberID))
                   { %>
                <div class="pt10 clearBoth" style="_padding-top:0px;">我为TA贡献了魅力值 <strong><%=Model.memberForTaContributionGlamourCount%>分</strong><br />
               我在TA的粉丝中排名 <strong><%=Model.iInTaFansRank%>名</strong></div>
               <% } else { %>
               <div class="pt10 clearBoth" style="_padding-top:0px;">成为TA的粉丝，并为TA贡献魅<br />力值，能提高TA对你的关注度</div>
               <% } %>
            </div>
        </div>
        <% } %>
     </div>
    <ul class="pic-list1 clearfix" id="topmember">
        <% Html.RenderAction("MyFansGlamourCountRank", "Relation", new { memberID = Model.MemberID, skin = "ta" });%>
    </ul>
    <%Html.RenderAction("SameInterestingMemberOverMe", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
    <%if (!Model.IsOwner) Html.RenderAction("TheyFavorsMember", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
    </aside>
</div> 
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        //绑定兴趣标签
        interestCenter.bindinterestLabel($("#interestContainer div.iObj"));
        interestCenter.bindinterestLabel($("#interestContainer2 div"));

        if (!$.browser.msie) {
            //复制到剪贴板
            copyToClipboard($("#copyToClipboardBtn"), $("#linkCopyUrl").html());
        }
        else {
            $("#copyToClipboardBtn").bind("click", function () {
                //复制到剪贴板
                copyToClipboard($("#copyToClipboardBtn"), $("#linkCopyUrl").html());
            });
        }

        $(".label01-02-01").bind("click", function () {
            $(".label01").hide(500);
        });
        fixedLabel01Pos();

        //提示相关
        var logintimes = <%=(Session["logintimes"]==null || Session["logintimes"]=="")?0:Session["logintimes"] %>;
        if(logintimes <=2 && $.cookie("<%=Model.UserID %>ta_lit_tipdel") == null)
        {
            $("#ta_lit_tipdel").bind("click", function () { $(this).parent().fadeOut(1000);$.cookie("<%=Model.UserID %>ta_lit_tipdel", "false", { expires: 1 }); });
            setTimeout(function () { $("#ta_lit_tipdel").parent().fadeIn(1000) }, 2000);
        }
    });

    function fixedLabel01Pos() {
        var memberAvatar = $("#iconImg")[0].src;
        if (memberAvatar.indexOf("/pics/noicon.jpg") > 0) {
            var referContainer = document.getElementById("iconImg"), left = 0, top = 0;
            var offsetPosit = getOffsetPosition(referContainer);
            left = offsetPosit.offsetLeft;
            top = offsetPosit.offsetTop;
            $(".label01").show();
            $(".label01").css({ "left": left + 15, "top": top - 15 });
        }
    }
    function showHide(flag, contaID) {
        switch (flag) {
            case "show": $("#" + contaID).show(); break;
            case "hide": $("#" + contaID).hide(); break;
        }
    }
    function presentGlamourValue() {
        var toMember = "<%=Model.MemberID%>";
        var glamourType = "1";
        var glamourValue = "1";
        MemberLinkProvider.presentGlamourValue(toMember, glamourType, glamourValue, function (data) {
            if (data.ok) { $.jBox.tip("成功赠送1分魅力值，赠送<br />后刷新页面就能看到变化啦", 'info'); }
            else { $.jBox.tip(data.err, 'info'); }
        });
    }
</script>
</asp:Content>

