﻿<%@ Control Language="C#"  Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%  Moooyo.BiZ.Content.InterestContent interest = (Moooyo.BiZ.Content.InterestContent)Model.contentobj; %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
    <div class="box_demo w412">
        <div class="box_content">
 <div class="box_title"><a href="/Content/TaContent/<%=Model.contentobj.Creater.MemberID %>/all/1" target="_blank"><%=Model.contentobj.Creater.NickName%></a><%=Model.typename %></div>
                <div class="box_info">
                <div class="info_left">
                    <div style="padding:10px 20px; line-height:25px;">
                        <img src="<%=Comm.getImagePath(interest.Interest.ICONPath,ImageType.Middle) %>" width="45" height="45" style="float:left; margin-right:10px;"/><span style="color:#A5a5a5; float:left;">刚刚加入</span><br /><span style="color:#0099cc; overflow:hidden;"><%=interest.Interest.Title%></span>
                    </div>
                    <div style="clear:both;"></div>
                    <%--<h3 class="h3 hleft"><img src="/pics/left_marks.gif"></h3>--%>
                    <span style="height:40px; line-height:20px; overflow:hidden; padding:0px 10px;"><%=interest.Interest.Content%></span>
                    <%--<h3 class="h3 hright"><img src="/pics/right_marks.gif"></h3>--%>
                </div>
                <div class="info_right"><a href="/Content/TaContent/<%=Model.contentobj.Creater.MemberID %>/all/1" target="_blank"><img src="<%=Comm.getImagePath(Model.contentobj.Creater.ICONPath, ImageType.Middle) %>"></a></div>
                </div>
