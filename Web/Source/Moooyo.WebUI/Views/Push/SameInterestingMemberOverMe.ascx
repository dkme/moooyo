<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.InterestOverMeMemberModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%if (Model != null && Model.memberList.Count > 0)
  {%>
<h3 class="caption-tit mt18">与<%=Model == null ? "我" : Model.IsOwner ? "我" : "TA"%>兴趣相同的人</h3>
    <ul>
    <%foreach (var obj in Model.memberList)
      {
          bool ifgz = false;
          foreach (var gzobj in Model.funslist) { if (obj.Key.ID == gzobj.ToMember) { ifgz = true; } }%>
        <li class="mt15 clearfix">
        <div name="memberinfo" class="list-show clearfix">
            <div class="head50 fl infos" data_me_id="<%=Model.UserID %>" data_member_id="<%=obj.Key.ID %>"><a href="/Member/I/<%=obj.Key.ID %>" target="_blank" ><img src="<%=Comm.getImagePath(obj.Key.MemberInfomation.IconPath,ImageType.Icon) %>" width="50" height="50" border="0" /></a></div>
            <div class="fl">
                <p class="cblue fb"><a href="/Member/I/<%=obj.Key.ID %>" target="_blank" title="<%=obj.Key.MemberInfomation.NickName %>"><%=obj.Key.MemberInfomation.NickName%></a></p>
                <%--<a class="<%=ifgz?"delete-btn":"add-btn" %>"" onclick="<%=ifgz?"member_i_functions.deletefavormember":"member_i_functions.favormember" %>('<%=obj.Key.ID %>',$(this))"><%=ifgz ? "<i></i>取消" : "<i></i>关注"%></a>--%>
                <p class="pr pr15 sameinterest"><a href='javascript:void(0)'><%=obj.Value.Count%>个共同爱好</a><i class="more arrow-bot"></i></p>
            </div>
        </div>
        <div class="com-inter" <%=obj.Value.Count>5?"style=\"height:97px;\"":"style=\"height:67px;\"" %>>
            <div class="com-interpz" <%=obj.Value.Count>5?"style=\"height:85px;\"":"style=\"height:55px;\"" %>>
                <p>可能的共同兴趣</p>
                <ul class="pic25-list son-mt5" name="sameinteresttomeul">
                <%foreach (var interest in obj.Value)
                  {
                      if (interest != null)
                      {
                    %><li data-interestid="<%=interest.ID %>"><a href="/InterestCenter/InterestFans?iid=<%=interest.ID%>" target="_blank" title="<%=interest.Title%>"><img src="<%=Comm.getImagePath(interest.ICONPath,ImageType.Icon) %>" width="25" height="25" border="0" /></a></li><%
                      }
                  } %>
                </ul>
            </div>
        </div>
        </li>
    <%} %>
    </ul>
<script language="javascript" type = "text/javascript">
    var interestOverTimeoutID = null;
    $().ready(function () {
        MemberInfoCenter.BindDataInfo($("[name='memberinfo'] div.infos"));
        $("p.sameinterest:first").parents("div.list-show").siblings("div.com-inter").slideDown(1);
        $("p.sameinterest:first").attr("data-showed", "true");
        $("p.sameinterest").click(function () {
            if ($(this).attr("data-showed") == "true") {
                $(this).parents("div.list-show").siblings("div.com-inter").slideUp(500);
                $(this).attr("data-showed", "false");
                return;
            }
            else {
                $("p.sameinterest").attr("data-showed", "false");
                $("div.list-show").siblings("div.com-inter").slideUp(500);
                $(this).parents("div.list-show").siblings("div.com-inter").slideDown(500);
                $(this).attr("data-showed", "true");
                return;
            }
        });
        //        if ($("#interestedAuthorCard").html() == null) { $("<div id=\"interestedAuthorCard\"></div>").appendTo("body"); }
        interestCenter.bindinterestLabel($("[name='sameinteresttomeul'] li"));
    });
    </script>
    <%}%>
