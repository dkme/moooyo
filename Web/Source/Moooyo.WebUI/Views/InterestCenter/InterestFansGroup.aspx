<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%@ Import Namespace="Moooyo.BiZ.InterestCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
[米柚] 兴趣粉丝们
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="c976 clearfix">
<%--    <aside class="asidebox mt32 bor-r3 mr15 fl">
		<% Html.RenderAction("LeftInterest", "InterestCenter", new { memberID = Model.MemberID });%>
    </aside>--%>
    <!-- .leftside  end -->
    <section class="inter-conbox mt32 fl" style="width:600px;">
    	<% Html.RenderAction("Interest", "InterestCenter", new { interestObj = Model.interestObj, memberInterestObj = Model.memberInterestObj }); %>
        <div class="mt32 clearfix"><h3 class="cblue f14 mr15 fl">喜欢它的粉丝（<%=Model.interestFansCount%>）………………</h3><a href="/InterestCenter/InterestFans<% if(!Model.IsOwner) Response.Write("/" + Model.MemberID); %>?iId=<%=Request.QueryString["iId"]%>" class="c666" id="goBack">返回</a></div>
        <ul class="like-inter-list">
        <% foreach (var iFObj in Model.interestFansListObje)
           { %>    
            <li class="listbar clearfix bdr-botm-ee" id="interestFans<%=iFObj.Creater.MemberID%>">
            	<div class="head50 mt6"><a href="/Content/IContent/<%= iFObj.Creater.MemberID%>" target="_blank" name="interestFansGroupMemberInfo"><img src="<%= Comm.getImagePath(iFObj.Creater.ICONPath, ImageType.Icon) %>" width="50" height="50" border="0" alt="<%=iFObj.Creater.NickName%>" title="<%=iFObj.Creater.NickName%>" data_me_id="<%=Model.UserID %>" data_member_id="<%=iFObj.Creater.MemberID %>" name="interestFansGroupMemberInfoArea" /></a><a href="javascript:void(0);" onclick="actionprovider.openmsg('<%=iFObj.Creater.MemberID%>')"><i></i>私信</a></div>
                <div class="w468 fl">
                    <p><a href="/Content/IContent/<%= iFObj.Creater.MemberID%>" target="_blank"><%=iFObj.Creater.NickName%></a><b class="ml15">
                    <%  string strMemberType = "";
                        switch(Convert.ToByte(iFObj.Creater.MemberType)) {
                            case 0: strMemberType = "普通会员"; break;
                            case 1: strMemberType = "高级会员"; break;
                            case 2: strMemberType = "米柚VIP"; break;
                        }
                        Response.Write(strMemberType); %>
                    </b></p>
                    <p><%=iFObj.Creater.Age%>岁 <%=iFObj.Creater.Career%> <%=iFObj.Creater.City.Replace("|","")%> 距离<%=Moooyo.WebUI.Models.DisplayObjProvider.GetWeDistance(Model.MemberID, iFObj.Creater.MemberID)%></p>
                    <p class="cgreen"><%if (iFObj.Creater.IWant != "")
                                        {%>"我想和一个<%Response.Write((iFObj.Creater.Sex == 1 ? "女孩" : "男孩") + iFObj.Creater.IWant + "\"");
                                        }
                                        else
                                        {%>TA还没有写“我想”。<a id='invertiwan' href="javascript:;" onclick="Invert.iWant('<%=iFObj.Creater.MemberID%>',$('#invertiwan'))">邀请TA填写“我想”</a><% } %></p>
                    <div class="clearfix mt6">
                    	<b class="c999 db fl">兴趣：</b>
                        <ul class="pic25-hlist fl" id="interestFansGroupContainer">
                        <% foreach (var iObj in InterestFactory.GetMemberInterest(iFObj.Creater.MemberID, 16, 1))
                           {
                               if (Model.UserID == iFObj.Creater.MemberID)
                               { %> 
                            <li data-interestid="<%=iObj.ID %>"><a href="/InterestCenter/InterestFans?iId=<%=iObj.ID%>" target="_blank"><img src="<%= Comm.getImagePath(iObj.ICONPath,ImageType.Icon) %>" width="25" height="25" title="<%=iObj.Title%>" alt="<%=iObj.Title%>" border="0" /></a></li>
                            <% continue;
                                } %>

                           <li data-interestid="<%=iObj.ID %>"><a href="/InterestCenter/InterestFans<% if(!Model.IsOwner) Response.Write("/" + Model.MemberID); %>?iId=<%=iObj.ID%>" target="_blank"><img src="<%= Comm.getImagePath(iObj.ICONPath, ImageType.Icon) %>" width="25" height="25" title="<%=iObj.Title%>" alt="<%=iObj.Title%>" border="0" /></a></li>
                        <% } %>
                        </ul>
                    </div>
                </div>
                <div class="inter-btn-list fr">
                    <% if(Model.UserID != iFObj.Creater.MemberID) {
                           if (Moooyo.WebUI.Models.DisplayObjProvider.IsInFavor(Model.UserID, iFObj.Creater.MemberID))
                           {%>
                            <a href="javascript:void(0);" class="delete-btn" onclick="member_i_functions.deletefavormember('<%=iFObj.Creater.MemberID%>',$(this))"><i></i>取消</a>
                        <% } else { %>
                            <a class="add-btn" href="javascript:void(0);" onclick="member_i_functions.favormember('<%=iFObj.Creater.MemberID%>',$(this))"><i></i>关注</a>
                        <% }
                        if (InterestFactory.GetMemberInterest(Request.QueryString["iId"], Model.UserID) != null) { %> 
                            <a href="javascript:void(0);" onclick="delMemberInterestFans('<%=iFObj.Creater.MemberID%>', '<%=Request.QueryString["iId"]%>')" class="cgray">移除粉丝</a><br/>
                       <% }  
                       }
                        if (Model.UserID != iFObj.Creater.MemberID)
                        { %>
                        <a href="javascript:void(0);" class="cgray" onclick="actionprovider.opencalladmin('<%=iFObj.Creater.MemberID%>', 1)">举报</a>
                    <% } %>
                </div>
            </li>
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
<aside class="asidebox-r mt15 fr" style="width:335px;">
    <% Html.RenderAction("TheyFavorsInteresting", "Push"); %>
</aside>
</div>   
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/data_<%=Model.JsVersion %>.js"></script>
<script language="javascript" type="text/javascript">
    $().ready(function () {
        //绑定会员标签
        MemberInfoCenter.BindDataInfo($("[name='interestFansGroupMemberInfo'] [name='interestFansGroupMemberInfoArea']"));
        //绑定兴趣标签
        interestCenter.bindinterestLabel($("#interestFansGroupContainer li"));
    });
    function delMemberInterestFans(memberId, interestId) {
        interestCenterProvider.delMemberInterestFans(memberId, interestId, function (data) {
            var result = $.parseJSON(data);
            if (result.ok) {
                $("#interestFans" + memberId).remove();
                alert("移除成功");
            }
            else {
                $.jBox.tip("移除失败，系统维护中，给您带来了不便，请谅解！", 'error');
            }
        });
    }
</script>
</asp:Content>
