<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%Moooyo.BiZ.Content.InterViewContent interview = (Moooyo.BiZ.Content.InterViewContent)Model.contentobj;%>
<div class="ajax_title">访谈<em class="ft"></em></div>
<div class="ajax_content">
    <%
        for (int i = 0; i < interview.InterviewList.Count; i++)
        {
            if (i >= 10) { break; }
            var obj = interview.InterviewList[i];
            %>
            <a href="/Content/ContentDetail/<%=interview.ID%>">
                <div class="ajax_com addfont">
                    <span class="huise">小编：<%=obj.Question%></span>
                </div>
                <div class="ajax_com addfont">
                    <span class="black" title="<%=obj.Answer %>">Ta：<%=Comm.replaceToN(Comm.getSubStringToIndex(obj.Answer, 100))%></span>
                </div>
            </a>
            <%
        }
        if (interview.InterviewList.Count > 10)
        {
            %>
                <div class="ajax_com addfont interviewmove">
                    <a href="/Content/ContentDetail/<%=interview.ID%>">
                        还有<%=interview.InterviewList.Count-10 %>条
                    </a>
                </div>
            <%
        }
      %>