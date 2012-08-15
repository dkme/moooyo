<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<div class="selfshare">
    <div class="share_c">
        <ul>
            <%if (Model.UserID != null)
                {
                    if (Model.isfans)
                    { %>
            <li class="b_left greenbg a cursor" title="取消加粉" onclick="member_i_functions.deletefavormembertwo('<%=Model.MemberID%>',$(this));">
                <span>取消</span><em style="background:url('/pics/self_del.png'); width:13px; height:13px;" ></em>
            </li>
            <%} %>
            <%else
                    { %>
            <li class="b_left greenbg a cursor" title="添加关注" onclick="member_i_functions.favormembertow('<%=Model.MemberID%>',$(this));">
                <span>加关注</span><em style="background:url('/pics/self_add.png');width:13px; height:13px;"></em>
            </li>
            <%}
                }%>
            <%if (Model.UserID != null)
                { %><li class="b_left l_bulebg l cursor" title="发布私信" onclick="window.open('/Msg/Messagedetails/<%=Model.MemberID%>')"><span>信件</span><em style=" width:23px; height:15px;"></em></li><%} %>
            <li class="redbg m" title="魅力值"><span><%=Model.Member.GlamourCount%></span><em></em></li>
            <li class="b_left redbg f" title="米果"><span><%=Model.Member.Points %></span><em></em></li>
            <%for (int j = 0; j < Model.interestList.Count; j++)
            {
                if (j == 1 || (j % 4 == 0 && j != 0))
                {
                    %><li class="cursor" title="<%=Model.interestList[j].Title %>"><a></a><em><a href="/InterestCenter/ShowInterest/<%=Model.interestList[j].ID %>"><img src="<%=Comm.getImagePath(Model.interestList[j].ICONPath,ImageType.Middle) %>" height="60px" width="60px"/></a></em></li><%
                }
                else
                {
                    %><li class="b_left cursor" title="<%=Model.interestList[j].Title %>"><a></a><em><a href="/InterestCenter/ShowInterest/<%=Model.interestList[j].ID %>"><img src="<%=Comm.getImagePath(Model.interestList[j].ICONPath,ImageType.Middle) %>" height="60px" width="60px" /></a></em></li><%
                }
            }%>
        </ul>
    </div>
</div>
