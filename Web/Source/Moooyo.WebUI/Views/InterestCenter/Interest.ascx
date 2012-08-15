<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<article class="tips-box clearfix" id="interestbox">
    <div class="in100 fl"><img src="<%= Comm.getImagePath(Model.interestObj.ICONPath, ImageType.Middle) %>" width="100" height="100" border="0" alt="<%= Model.interestObj.Title%>" title="<%= Model.interestObj.Title%>" />
    </div>
    <div class="fl" style="width:440px;">
        <div class="inter-message-box fl">
            <p><a href="javascript:void(0);" class="f14"><a href="/InterestCenter/InterestFans?iid=<%= Model.interestObj.ID %>" id="interestname<%= Model.interestObj.ID%>"><%= Model.interestObj.Title%></a></a><span class="ml15"><%= Model.interestObj.Classes%></span></p>
            <p class="mt5" id="interestMemberInfo">创建：<a href="/Content/IContent/<%= Model.interestObj.Creater.MemberID%>" target="_blank"><img src="<%= Comm.getImagePath(Model.interestObj.Creater.ICONPath, ImageType.Icon) %>" class="creat-user-head" width="25" height="25" border="0" alt="<%= Model.interestObj.Creater.NickName%>" title="<%= Model.interestObj.Creater.NickName%>" data_me_id="<%=Model.UserID %>" data_member_id="<%=Model.interestObj.Creater.MemberID %>" name="interestMemberInfoArea" /></a><a href="/Member/I/<%= Model.interestObj.Creater.MemberID%>" target="_blank"><%= Model.interestObj.Creater.NickName%></a></p>
        </div>
        <div class="enjoy-inter fr">
        <span name='shareBox' data-title='分享到' data-type='shareInfos.interestShare' data-content1='interestname<%= Model.interestObj.ID%>'></span>
        </div>
        <div style="width:440px;" class="fl">
        <%
            String contentstr = Model.interestObj.Content;
            while (true)
          {
              if (contentstr.IndexOf("\n") >= 0)
                  contentstr = contentstr.Replace("\n", "<br/>");
              else
                  break;
              
          } %>
            <p class="mt5"><%=contentstr%></p>
            <p class="mt11">
            <span id="isFans<%=Model.interestObj.ID%>"></span>
            <div style="clear:both"></div>
            <%--提示--%>
            <div class="littip_div">
                <div class="littip interest_lit_tip"></div>
                <img id="interest_lit_tipdel" src="/pics/tip/littip_del.png" alt="" title="知道了，关闭" class="interest_lit_tipdel" />
            </div>
            <%--提示结束--%>
            <span id="modifyInterest"></span>
            </p>
        </div>
     </div>
</article>

<script language="javascript" type="text/javascript">
    $().ready(function () {
        isMemberInterest();
        //绑定转发到微博
        microConnOperation.bindShareBox();

        $("#interestbox").each(function () {
            $('.shareto_toolbox', this).css("visibility", "visible");
        });
        //绑定会员标签
        MemberInfoCenter.BindDataInfo($("#interestMemberInfo [name='interestMemberInfoArea']"));
         //提示相关
        var logintimes = <%=(Session["logintimes"]==null || Session["logintimes"]=="")?0:Session["logintimes"] %>;

        if(logintimes <=2 && $.cookie("<%=Model.UserID %>interest_lit_tipdel") == null && $("#modifyInterest").html() == "")
        {
            $("#interest_lit_tipdel").bind("click", function () { 
                $(this).parent().fadeOut(1000);
                $.cookie("<%=Model.UserID %>interest_lit_tipdel", "false", { expires: 1 });
            });
            setTimeout(function () { $("#interest_lit_tipdel").parent().fadeIn(1000) }, 2000);
        }
    });
    function isMemberInterest() {
        var str = "", iId = "<%= Model.interestObj.ID%>";
        //是我的兴趣则为true，反则为false
        var isMembInte = <%= Model.memberInterestObj != null ? "true" : "false" %>;
        if(isMembInte) {
             str += "&nbsp;&nbsp;<a href=\"/InterestCenter/ModifyInterest?iId=" + iId + "\">修改兴趣</a>";
             $("#modifyInterest").html(str);
        }
        else
        {
            isFans(iId);
        }
    }
    function isFans(iId) {
        var str = "";
        var isFans = <%= Model.isFans ? "true" : "false" %>;
        if (isFans) {
            str += "<a href=\"javascript:void(0);\" onclick=\"interestCenterFunctions.delInterestFans(\'" + iId + "\')\" class=\"delete-btn fl\"><i></i>删粉</a>";
        }
        else {
            str += "<a href=\"javascript:void(0);\" onclick=\"interestCenterFunctions.addInterestFans(\'" + iId + "\');\" class=\"add-btn fl mr5\"><i></i>加粉</a>";
        }
        $("#isFans" + iId).html(str);
    }
</script>
