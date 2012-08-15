<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>

<aside class="asidebox mt32 bor-r3 mr15 fl">
    <div class="memb-pic"><a href="/Content/IContent/<%= Model.Member.ID %>"><img alt="头像" id="iconImg" src="<%= Model.Member.ICONPath  %>"/></a></div>
    <ul class="honour-list clearfix">
        <li>
        <%if (Model.Member.IsRealPhotoIdentification)
              { %>
              <img src="/upload/photoreal.gif" alt="已经通过视频认证" title="已经通过视频认证"/>
        <%}
              else
              {%>
              <a href="/Setting/Authentica" target="_blank"><img src="/upload/photonoreal.gif" alt="未通过视频认证" title="未通过视频认证"/></a>
        <%} %>
        </li>
        <li>
        <a href="/Setting/AcctUpgrade" target="_blank">
            <% 
            Moooyo.BiZ.Member.MemberType mt = (Moooyo.BiZ.Member.MemberType)Model.Member.MemberType;
            if (mt == Moooyo.BiZ.Member.MemberType.Level0)
            {%>
            <img src="/upload/normal.gif" alt="普通会员" title="普通会员"/>    
        <%}
            else if (mt == Moooyo.BiZ.Member.MemberType.Level1)
            {%>
            <img src="/upload/h_user.gif" alt="高级会员" title="高级会员"/>    
        <%}
            else if (mt == Moooyo.BiZ.Member.MemberType.Level2)
            {%>
            <img src="/upload/vip.gif" alt="米柚VIP" title="米柚VIP"/> 
        <%}
             %>
        </a>
        </li>
        <%
        if (null != Model.Member.Badgelist && Model.Member.Badgelist.Count > 0)
        {
            foreach (Moooyo.BiZ.Member.MemberBadge mb in Model.Member.Badgelist)
            {
                if (mb.BadegStatus == 1)
                {
                    if (mb.BadegType == 1)
                    { 
                        %>
                        <li><img src="/pics/badge_superman.gif" alt="超人" title="米柚超人" /></li>
                        <%
                    }
                }
            }
        }
         %>
    </ul>
    <div class="hot-bar clearfix"><span class="fl">人气：</span><b class="<%= Model.Member.Hot %>"></b></div>
    <div class="memb-tit mt5">
        <p>魅力值：<%= Model.Member.GlamourCount%></p>
        <!--<p>今日排名：第10名</p>-->
        <p>封 &nbsp;号：<%=Model.Member.MemberTitle %></p>
    </div>
    <ul class="side-list mt11">
        <li id="content/icontent"><a href="/Member/I/<%= Model.Member.ID %>"><b class="cover sprites"></b>封面</a></li>
        <li id="mplist"><a href="/Photo/mplist/<%= Model.Member.ID %>"><b class="photo sprites"></b>照片(<%= Model.Member.PhotoCount%>)</a></li>
        <li id="interview"><a href="/Member/interview/<%= Model.Member.ID %>"><b class="talk sprites"></b>访谈(<%= Model.Member.InterViewCount%>)</a></li>
        <li id="intersert"><a href="/InterestCenter/Interests/<%= Model.Member.ID %>"><b class="inter sprites"></b>兴趣(<%= Model.Member.InterestCount%>)</a></li>
        <%--<li id="skill"><a href="/Member/skill/<%= Model.Member.ID %>"><b class="skill sprites"></b>才艺(<%= Model.Member.SkillCount%>)</a></li>--%>
    </ul>
    <div class="mapbox mt20">
    <% if (Model.Member.Lat != 0 && Model.Member.Lng != 0)
       { %>
           <% if (!Model.IsOwner)
              { %>
                <h3>与Ta相距&nbsp;<span><%= Moooyo.WebUI.Models.DisplayObjProvider.GetWeDistance(Model.UserID, Model.MemberID)%></span></h3>
            <% }
              else
              { %>
              <h3>我的位置</h3><a href="/Setting/SetLocation" class="fr">更改</a>
            <% } %>
            <div class="map mt5">
            <img class="map" src="http://maps.google.com/maps/api/staticmap?center=<%= Model.Member.Lat %>,<%= Model.Member.Lng %>&zoom=13&size=150x100&maptype=roadmap&markers=color:red|label:A|<%= Model.Member.Lat %>,<%= Model.Member.Lng %>&sensor=false" />
            </div>
    <% }
       else
       {%>
            <% if (!Model.IsOwner)
               { %>
            <h3 class="cgray">Ta还没有标注位置<br /><a id="invertmap" href="javascript:" onclick="Invert.updateLocation('<%=Model.MemberID %>',$('#invertmap'))">问问Ta在哪</a></h3>
            <%}
               else
               { %>
               <h3 class="cgray">还没有标注位置<br /><a href="/Setting/SetLocation">去标注我的位置</a></h3>
            <%} %>

    <%} %>
    </div>
</aside><!-- .leftside  end -->
<script language="javascript" type="text/javascript">
    $().ready(function () {
        var url = location.href.toLowerCase();
        $("#navlist LI").each(function () {
            $(this).removeClass('current');
        });
        $("#navlist LI").each(function () {
            if (url.indexOf(this.id.toLowerCase()) > 0) {
                $(this).addClass('current');
            }
        });

        
    });
    
</script>

