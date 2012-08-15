<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%if(Model.ifshowmember){  %>
<div class="carepic">
    <span class="showS">
    <a href="/Content/TaContent/<%=Model.contentobj.MemberID %>/all/1"  target="_blank">
    <img src="<%=Comm.getImagePath(Model.contentobj.Creater.ICONPath,ImageType.Middle) %>" height="45" width="45" data-myid="<%=Model.UserID %>" data-heid="<%=Model.contentobj.MemberID %>" />
    </a>
    </span>
    <span class="blue02">
    <a href="/Content/TaContent/<%=Model.contentobj.MemberID %>/all/1" target="_blank">
    <%=Model.contentobj.Creater.NickName %>
    </a>
    </span>
    <span class="gray01"><%=Comm.getTimeSpan(Model.contentobj.UpdateTime) %></span>
</div>

<%} %>
<div class="caretxt">
    <div class="ajax_box">
       <div class="top_angle em"></div>
        <div class="ajax_main">
            <div class="feed_arrow"><div class="arrow_l"></div></div> 
            <%
                Boolean ifshowUpdate = false;
                Boolean ifshowDelete = false;
                if (Model.ifmy)
                {
                    ifshowUpdate = ifshowDelete = true;
                }
                string updateHtml = "";
                string deleteHtml = "<a class=\"contentmanager\" onclick=\"deleteContent('" + Model.contentobj.ID + "',$(this),'/Content/IContent')\">删除</a>";
                if (Model.contenttype == Moooyo.BiZ.Content.ContentType.Image.ToString())
                {
                    Html.RenderPartial("ContentItem_Image");
                    updateHtml = "<a class=\"contentmanager\" href=\"/Content/AddImageContent/" + Model.contentobj.ID + "\">编辑</a>";
                }
                if (Model.contenttype == Moooyo.BiZ.Content.ContentType.SuiSuiNian.ToString())
                {
                    Html.RenderPartial("ContentItem_ShuoShuo");
                    updateHtml = "<a class=\"contentmanager\" href=\"/Content/AddSuiSuiNianContent/" + Model.contentobj.ID + "\">编辑</a>";
                }
                if (Model.contenttype == Moooyo.BiZ.Content.ContentType.IWant.ToString())
                {
                    Html.RenderPartial("ContentItem_IWant");
                }
                if (Model.contenttype == Moooyo.BiZ.Content.ContentType.Mood.ToString())
                {
                    Html.RenderPartial("ContentItem_Mood");
                }
                if (Model.contenttype == Moooyo.BiZ.Content.ContentType.InterView.ToString())
                {
                    Html.RenderPartial("ContentItem_InterView");
                    updateHtml = "<a class=\"contentmanager\" href=\"/Content/AddInterViewContent\">编辑</a>";
                }
                if (Model.contenttype == Moooyo.BiZ.Content.ContentType.CallFor.ToString())
                {
                    Html.RenderPartial("ContentItem_CallFor");
                    updateHtml = "<a class=\"contentmanager\" href=\"/Content/AddCallForContent/" + Model.contentobj.ID + "\">编辑</a>";
                }
                if (Model.contenttype == Moooyo.BiZ.Content.ContentType.Interest.ToString())
                {
                    Html.RenderPartial("ContentItem_Interest");
                    ifshowUpdate = false;
                }
                if (Model.contenttype == Moooyo.BiZ.Content.ContentType.Member.ToString())
                {
                    Html.RenderPartial("ContentItem_Member");
                    ifshowUpdate = false;
                }
            %>
                <div class="mt10 clear"></div>
                <div class="ajax_com">
                    <span class="bottom_left">
                    <%
                        if (ifshowUpdate)
                        {
                            %><%=updateHtml %><%
                        }
                    %>
                    <%
                        if (ifshowDelete)
                        {
                            %><%=deleteHtml%><%
                        }                
                            %>
                    </span>
                    <%if (Model.UserID != null)
                        { %>
                        <span class="bottom_right">
                            <b class="love_hit" onclick="showanswerdiv('contentanswerdiv<%=Model.contentobj.ID %>','content<%=Model.contentobj.ID %>');"><em class="em-love"></em><s id="contentanswercount<%=Model.contentobj.ID %>"><%=Model.contentobj.AnswerCount > 0 ? Model.contentobj.AnswerCount.ToString() : ""%></s></b>
                            <b class="msg_hit" onclick="addlike('<%=Model.contentobj.ID %>','<%=Model.likename%>', '<%=Model.typename%>');">
                                <em class="emhover" style="display:none">mo<q>一下</q></em>
                                <em class="em-hit">mo</em><s id="contentlikecount<%=Model.contentobj.ID %>"><%=Model.contentobj.LikeCount > 0 ? Model.contentobj.LikeCount.ToString() : ""%></s>
                            </b>
                        </span>
                    <%} %>
                    <%else
                        { %>
                    <span class="bottom_right">
                        <b class="love_hit" onclick="window.open('/Account/Login');"><em class="em-love"></em><s><%=Model.contentobj.AnswerCount > 0 ? Model.contentobj.AnswerCount.ToString() : ""%></s></b>
                        <b class="msg_hit" onclick="window.open('/Account/Login');">
                            <em class="emhover" style="display:none">mo<q>一下</q></em>
                            <em class="em-hit">mo</em><s><%=Model.contentobj.LikeCount > 0 ? Model.contentobj.LikeCount.ToString() : ""%></s>
                        </b>
                    </span>
                    <%} %>
                </div>
            </div>
        </div>
        <div class="bottom_angle em"></div>
</div>
<div class="ans_box" id="contentanswerdiv<%=Model.contentobj.ID %>" data-ifopen="<%=Model.ifmy&&Model.contentobj.AnswerCount>0 ? "true" : "false"%>" <%=Model.ifmy&&Model.contentobj.AnswerCount>0 ? "style=\"display:block;\"" : "style=\"display:none;\""%>>
    <div class="ans_com ans_txt">
        <span><em onclick="showanswerdiv('contentanswerdiv<%=Model.contentobj.ID %>')" style="cursor:pointer;"></em></span>
    </div>
    <div class="clear1"></div>
    <%if (!Model.ifmy || Model.contentobj.AnswerCount <= 0)
      { %>
    <div class="ans_com ans_txt answerdiv" id="contenttextdiv<%=Model.contentobj.ID %>" data-ifopen="true" style="display:block;">
        <span><input type="text" id="content<%=Model.contentobj.ID %>" class="ans_t"/></span>
        <%if (Model.ifmy)
          {
              %><span class="arrow_ans"><a class="blue02" onclick="addcontentanswer('<%=Model.contentobj.ID %>','','<%=Model.UserID %>', '');$('#contenttextdiv<%=Model.contentobj.ID %>').remove();">发送</a><em></em></span><%
          }
          else
          {
              %><span class="arrow_ans"><a class="blue02" onclick="addcontentanswer('<%=Model.contentobj.ID %>','','<%=Model.UserID %>', '')">发送</a><em></em></span><%
          } %>
    </div>
    <%}%>
    <div class="clear1"></div>
    <div id="contentanswer<%=Model.contentobj.ID %>">
        <%foreach (var obj in Model.contentobj.AnswerList)
          {%>
        <div class="ans_com">
            <div class="ans_compic">
                <img src="<%=Comm.getImagePath(obj.Creater.ICONPath,ImageType.Icon) %>" height="25" width="25" />
            </div>
            <div class="ans_span">
                <span class="left_s">
                    <a href="/Content/TaContent/<%=obj.Creater.MemberID%>/all/1" target="_blank" class="blue02"><%=obj.Creater.NickName%></a>
                    <font class="huise autoW" id="autoW">
                        <%
                            string contentstr = Comm.replaceToN(obj.Content);
                            contentstr = contentstr.Length > 100 ? contentstr.Substring(0, 100) + ".." : contentstr;
                           %>
                        <%=contentstr%>
                    </font>
                    <nobr><font class="gray01"><%=Comm.getTimeSpan(obj.CreatedTime)%></font></nobr>
                    <%if (obj.MemberID != Model.UserID)
                      { %>
                        <nobr><a class="gray01 answeranswerbu" onclick="showcontentanswertext('<%=obj.ID %>','<%=Model.contentobj.ID %>','<%=obj.Creater.NickName %>','<%=Model.UserID %>','<%=obj.MemberID %>',$(this))">回应</a></nobr>
                    <%}%>
                </span>
            </div>
        </div>
        <%if (obj.MemberID != Model.UserID)
          { %>
        <div class="ans_com ans_txt answeranswerdiv" id="contentanswertextdiv<%=obj.ID %>" data-ifopen="false" style="display:none;"></div>
        <%}%>
        <%}%>
    </div>
    <div class="ans_com ans_txt" id="contentmore<%=Model.contentobj.ID %>">
        <%if (Model.contentobj.AnswerCount > 6){ %>
        <span class="ans_right"><a class="gray01" href="/Content/ContentDetail/<%=Model.contentobj.ID%>">更多…</a></span>
        <%} %>
    </div>
</div>
</div>


<script type="text/javascript">
    $().ready(function () {
        loadRightBu();
        loadiconmove();
    });
   
  
</script>
