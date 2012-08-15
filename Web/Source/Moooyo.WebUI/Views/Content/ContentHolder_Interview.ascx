<%@ Control Language="C#"  Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%  Moooyo.BiZ.Content.InterViewContent interview = (Moooyo.BiZ.Content.InterViewContent)Model.contentobj; %>
<div class="box_demo w623"> 
            <div class="box_content">
                <div class="box_title">
                <a href="/Content/TaContent/<%=Model.contentobj.Creater.MemberID%>/all/1" target="_blank"><%=Model.contentobj.Creater.NickName%></a>
                </div>
                <div class="box_info">
                <div class="contetn_l"><span><%=Model.typename %></span><span class="userpic"><a href="/Content/TaContent/<%=Model.contentobj.Creater.MemberID %>/all/1" target="_blank"><img src="<%=Comm.getImagePath(Model.contentobj.Creater.ICONPath,ImageType.Middle) %>"></a></span></div>
    <%
        int i = 0;
        foreach (var interviewobj in interview.InterviewList)
        {
            if (i > 2) { break; }
            string interviewcontentanswerstr = "";
            if (interviewobj.AnswerList == null) continue;
            
            foreach (var obj in interviewobj.AnswerList)
            {
                interviewcontentanswerstr += "<div class=\"pup_com\"><span class=\"puplist\"><a href=\"/Content/TaContent/" + obj.MemberID + "/all/1\" target=\"_blank\"><img src=\"" + Comm.getImagePath(obj.Creater.ICONPath, ImageType.Middle) + "\" height=\"25\" width=\"25\"></a><a href=\"/Content/TaContent/" + obj.MemberID + "/all/1\" target=\"_blank\" class=\"blue02\">" + obj.Creater.NickName + "</a><font>" + obj.Content + "</font></span></div>";
            }
            if (interviewobj.AnswerCount > 6)
            {
                interviewcontentanswerstr += "<div class=\"pup_com\"><span class=\"pup_more interviewanswermore\" onclick=\"showinterviewanswertoindex('" + interviewobj.ID + "'," + interviewobj.AnswerCount + ",2)\"><a>更多回应</a></span></div>";
            }%>
                <div class="content_com"> <span>小编：<%=interviewobj.Question%></span> <span><font class="blue1" >Ta：<%=interviewobj.Answer%></font></span> <span class="spanbottom"><b class="love_hit" id="interviewlikecount<%=interviewobj.ID %>" onclick="showlikeorshowanswer($(this),3,'<%=Model.AlreadyLogon %>','<%=Model.contentobj.ID %>','<%=interviewobj.ID %>')"><%=interviewobj.LikeCount%></b><b class="msg_hit" id="interviewanswercount<%=interviewobj.ID %>" onclick="showlikeorshowanswer($(this),4,'<%=Model.AlreadyLogon %>','<%=Model.contentobj.ID %>','<%=interviewobj.ID %>')"><%=interviewobj.AnswerCount%></b></span></div><div id="interviewanswerliststr<%=interviewobj.ID %>" style="display:none;"><%=interviewcontentanswerstr%></div>
        <%i++;
          }%>
                </div>
                <div class="box_bottom"><span class="bottom_left"></span>
			    <span class="bottom_right"></span></div>
            </div>
		    <div class="rbottom"></div>
        </div>
