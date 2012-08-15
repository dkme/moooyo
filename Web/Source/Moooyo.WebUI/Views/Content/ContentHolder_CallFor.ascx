<%@ Control Language="C#"  Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%Moooyo.BiZ.Content.CallForContent callfor = (Moooyo.BiZ.Content.CallForContent)Model.contentobj; %>

    <div class="box_demo w412">
        <div class="box_content">
<div class="box_title"><a href="/Content/TaContent/<%=Model.contentobj.Creater.MemberID %>/all/1" target="_blank"><%=Model.contentobj.Creater.NickName%></a><%=Model.typename %></div>
                <div class="box_info">
                <div class="info_left">
                    <%--<h3 class="h3 hleft"><img src="/pics/left_marks.gif"></h3>--%>
                    <span class="h80" style="overflow:hidden; padding:0px 10px;"><%=callfor.Content%></span>
                   <%-- <h3 class="h3 hright"><img src="/pics/right_marks.gif"></h3>--%>
                    <span class="red1"><font id="contentlikecount1<%=Model.contentobj.ID %>"><%=Model.contentobj.LikeCount %></font><font><%=Model.likename%></font><em onclick="showlikeorshowanswer($(this),1,'<%=Model.AlreadyLogon %>','<%=Model.contentobj.ID %>')">+1</em></span>
                    <span class="h25" id="contentlikemember<%=Model.contentobj.ID %>">
                    <%int i = 0;
                      foreach (var obj in Model.contentobj.LikeList)
                      {
                          if (i > 6) { break; }%>
                        <a href="/Content/TaContent/<%=obj.MemberID %>/all/1" target="_blank"><img src="<%=Comm.getImagePath(obj.MemberIcon,ImageType.Middle) %>" height="25" width="25"></a>
                        <%i++;
                      } %>
                      </span>
                </div>
                <div class="info_right"><a href="/Content/TaContent/<%=Model.contentobj.Creater.MemberID %>/all/1" target="_blank"><img src="<%=Comm.getImagePath(Model.contentobj.Creater.ICONPath, ImageType.Middle) %>"></a></div>
                </div>
