<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>
<article class="top-title-box mt32 fl bor-b1">
    <div class="clearfix">
        <div class="fl">
            <b class="cblue f18 fb fyahei fl mr15"><a href="/Content/IContent/<%= Model.Member.ID %>"><%= Model.Member.Name  %></a></b>
            <b class="fl mr15 mt6"><i class="vip-icon"></i><%= Model.Member.MemberLevel%></b>
            <% if (Model.Member.Want.Trim() != "")
            { %>
            <b class="fl f14 mt5">“我想和一个<%= Model.Member.WantSexStr%>生<%= Model.Member.Want%>”</b>
            <% } %>
        </div>
        <ul class="sayhi-btn-list fr">
                <li class="roundButton1" title=""><a href="javascript:void(0);" onclick="actionprovider.openmsg('<%=Model.MemberID%>')"><i class="circle-icon"></i>私信</a></li>
                <li class="roundButton1"><a onclick='actionprovider.openhi("<%=Model.MemberID%>")' href="javascript:;"><i class="circle-icon"></i>打招呼</a></li>
                <li class="roundButton1" onmouseover="ShowHide('moreToMemberOperat', 'show')" onmouseout="ShowHide('moreToMemberOperat', 'hide')"><a href="javascript:;" class="fl">更多</a><i class="arrow-icon2"></i>
                    <div name="moreToMemberOperat" class="moreToMemberOperat">
                    	<ul class="dropList">
                            <li><a href='javascript:;' onclick='actionprovider.opencalladmin("<%=Model.MemberID%>",1)'>举报</a></li>
                            <li><a href='javascript:;' onclick='member_i_functions.disablemember("<%=Model.MemberID%>")'>屏蔽</a></li>
                        </ul>
                    </div>
                </li>
            </ul>
    </div>
    <ul class="member-infor-list clearfix">
        <li>年龄：<%= Model.Member.Age != null ? Model.Member.Age : ""%>岁</li>
        <li>城市：<%= Model.Member.City != null ? Model.Member.City : ""%></li>
        <li>职业：<%= Model.Member.Career != null ? Model.Member.Career : ""%></li>
        <li>身高：<%= Model.Member.Height != null ? Model.Member.Height : ""%></li>
        <li>薪水：<%= Model.Member.Gainings != null ? Model.Member.Gainings : ""%></li>
        <li><a href="/Content/IContent/<%=Model.Member.ID %>">更多</a></li>
    </ul>
</article>
