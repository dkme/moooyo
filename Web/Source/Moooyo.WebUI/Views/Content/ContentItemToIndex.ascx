<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%
string contenttype = Model.contenttype;
if (contenttype == "InterView")
{
    Html.RenderPartial("ContentHolder_Interview");
}
else
{
    //try{
    if (contenttype == "Image")
    {
        Html.RenderPartial("ContentHolder_Image");
    }
    if (contenttype == "SuiSuiNian")
    {
            Moooyo.BiZ.Content.SuiSuiNianContent suosuo = (Moooyo.BiZ.Content.SuiSuiNianContent)Model.contentobj;
            if (suosuo.Type == "3")//添加兴趣
            {
                Html.RenderPartial("ContentHolder_AddInterestFans"); 
            }
            else if (suosuo.Type == "4")//创建兴趣
            {
                Html.RenderPartial("ContentHolder_CreateInterest"); 
            }
            else if (suosuo.Type == "5")//修改兴趣
            {
                Html.RenderPartial("ContentHolder_EditInterest"); 
            }
            else if (suosuo.Type == "")
            {
                Html.RenderPartial("ContentHolder_ShuoShuo");
            }
        }
        if (contenttype == "IWant")
        {
            Html.RenderPartial("ContentHolder_IWant"); 
        }
        if (contenttype == "Mood")
        {
            Html.RenderPartial("ContentHolder_Mood"); 
        }
        if (contenttype == "CallFor")
        {
        Html.RenderPartial("ContentHolder_CallFor"); 
        }
    }
    //catch(Exception e)
    //{
    //    throw new CBB.ExceptionHelper.OperationException( 
    //        CBB.ExceptionHelper.ErrType.SystemErr, 
    //        CBB.ExceptionHelper.ErrNo.ColumnDistanceNotFind, 
    //        e);
    //}
    %>
    <!--喜欢和回复-->
            <div class="box_bottom"><span class="bottom_left"></span>
		    <span class="bottom_right"><b class="love_hit" id="contentlikecount<%=Model.contentobj.ID %>" onclick="showlikeorshowanswer($(this),1,'<%=Model.AlreadyLogon %>','<%=Model.contentobj.ID %>')"><%=Model.contentobj.LikeCount %></b><b class="msg_hit" id="contentanswercount<%=Model.contentobj.ID %>" onclick="showlikeorshowanswer($(this),2,'<%=Model.AlreadyLogon %>','<%=Model.contentobj.ID %>')"><%=Model.contentobj.AnswerCount %></b></span><div id="contentanswerliststr<%=Model.contentobj.ID %>" style="display:none">
        <%                
    foreach (var obj in Model.contentobj.AnswerList)
    {
            %>
            <div class="pup_com"><span class="puplist"><a href="/Content/TaContent/<%=obj.MemberID %>/all/1" target="_blank"><img src="<%=Comm.getImagePath(obj.Creater.ICONPath, ImageType.Middle) %>" height="25" width="25"></a><a href="/Content/TaContent/<% =obj.MemberID%> /all/1" target="_blank" class="blue02"><% =obj.Creater.NickName%></a><font><% =obj.Content%></font></span></div>
            <%
    }

    if (Model.contentobj.AnswerCount > 6)
    {%>
        <div class="pup_com"><span class="pup_more answermore" onclick="showanswertoindex('<%=Model.contentobj.ID %>',<%=Model.contentobj.AnswerCount %>,2)"><a>更多回应</a></span></div>";
    <%
        }
    %>
            </div>
	    </div>
	    <div class="rbottom"></div>
    </div></div>
        <%
//}
%>
