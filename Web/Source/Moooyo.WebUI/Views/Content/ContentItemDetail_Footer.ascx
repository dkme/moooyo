<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%@ Import Namespace="Moooyo.BiZ.Like" %>
    <div class="footer">
        <div class="like">
        <%if (Model.Member != null)
          { %>
                <%if (!Model.ifmy)
                    { %>
                <a href="javascript:void(0)" onclick="addlikeinContentDetail('<%=Model.contentobj.ID %>','<%=Model.likename%>',$('#likecount'),$('#content<%=Model.contentobj.ID %>'))"><span class="plus"><b>mo</b>一下</span><span id="likecount"><%=Model.contentobj.LikeCount > 0 ? Model.contentobj.LikeCount.ToString(): ""%></span></a>
        <% }
          } %>
        <div class="avatars">
        <%foreach (LikeMember m in Model.contentobj.LikeList)
          {%>
            <a href="/Content/TaContent/<%=m.MemberID %>/all/1" target="_blank"><img src="<%=Comm.getImagePath(m.MemberIcon, ImageType.Icon) %>" alt="<%=m.MemberName %>" title="<%=m.MemberName %>"/></a>
         <%} %>
        </div>
        </div>
        <div class="actions clearfix"><%--
        <%if (Model.contentobj.MemberID == Model.UserID)
            {
                switch (Model.contentobj.ContentType)
                {
                    case Moooyo.BiZ.Content.ContentType.Image:
                        %>
                        <a href="/Content/AddImageContent/<%=Model.contentobj.ID %>">编辑</a>
                        <a onclick="deleteContent('<%=Model.contentobj.ID %>',$(this),'/Content/IndexContent')">删除</a>
                        <% 
break;
                    case Moooyo.BiZ.Content.ContentType.SuiSuiNian:
                        %>
                        <a href="/Content/AddSuiSuiNianContent/<%=Model.contentobj.ID %>">编辑</a>
                        <a onclick="deleteContent('<%=Model.contentobj.ID %>',$(this),'/Content/IndexContent')">删除</a>
                        <% 
break;
                    case Moooyo.BiZ.Content.ContentType.InterView: 
                        %>
                        <a href="/Content/AddInterViewContent">编辑</a>
                        <a onclick="deleteContent('<%=Model.contentobj.ID %>',$(this),'/Content/IndexContent')">删除</a>
                        <%
break;
                    case Moooyo.BiZ.Content.ContentType.CallFor:
                        %>
                        <a href="/Content/AddCallForContent/<%=Model.contentobj.ID %>">编辑</a>
                        <a onclick="deleteContent('<%=Model.contentobj.ID %>',$(this),'/Content/IndexContent')">删除</a>
                        <% 
break;
                } 
            }%>--%>
        <a href="javascript:;" class="fl" id="copyToClipboardBtn">&nbsp;&nbsp;复制链接</a><input type="hidden" id="linkCopyUrl" name="linkCopyUrl" value="<%=HttpContext.Current.Request.Url.AbsoluteUri.ToString() %>" />
        <%
            string title = Model.Member.Name;
            string content = "";
            string imageurl = "";
            switch (Model.contentobj.ContentType)
            {
                case Moooyo.BiZ.Content.ContentType.Image:
                    List<Moooyo.BiZ.Content.Image> imagelist1 = ((Moooyo.BiZ.Content.ImageContent)Model.contentobj).ImageList;
                    if (imagelist1.Count > 0)
                        imageurl = Comm.getImagePath(imagelist1[0].ImageUrl, ImageType.Middle);
                    else
                        imageurl = Model.Member.ICONPath;
                    content = ((Moooyo.BiZ.Content.ImageContent)Model.contentobj).Content;
                    break;
                case Moooyo.BiZ.Content.ContentType.SuiSuiNian:
                    List<Moooyo.BiZ.Content.Image> imagelist2 = ((Moooyo.BiZ.Content.SuiSuiNianContent)Model.contentobj).ImageList;
                    if (imagelist2.Count > 0)
                        imageurl = Comm.getImagePath(imagelist2[0].ImageUrl, ImageType.Middle);
                    else
                        imageurl = Model.Member.ICONPath;
                    content = ((Moooyo.BiZ.Content.SuiSuiNianContent)Model.contentobj).Content;
                    break;
                case Moooyo.BiZ.Content.ContentType.InterView:
                    imageurl = Model.Member.ICONPath;
                    content = "小编访谈";
                    break;
                case Moooyo.BiZ.Content.ContentType.CallFor:
                    List<Moooyo.BiZ.Content.Image> imagelist4 = ((Moooyo.BiZ.Content.CallForContent)Model.contentobj).ImageList;
                    if (imagelist4.Count > 0)
                        imageurl = Comm.getImagePath(imagelist4[0].ImageUrl, ImageType.Middle);
                    else
                        imageurl = Model.Member.ICONPath;
                    content = ((Moooyo.BiZ.Content.CallForContent)Model.contentobj).Content;
                    break;
            }
            %>
<span class="tweet">
    转发
    <a class="sina-weibo" onclick="shareSina('<%=title %>','<%=content %>','http://www.moooyo.com/Content/ContentDetail/<%=Model.contentobj.ID %>','http://www.moooyo.com<%=imageurl %>');">&nbsp;&nbsp;</a>
    <a class="tc-weibo" onclick="shareTX('<%=title %>','<%=content %>','http://www.moooyo.com/Content/ContentDetail/<%=Model.contentobj.ID %>','http://www.moooyo.com<%=imageurl %>');">&nbsp;&nbsp;</a>
</span>
        </div>
    </div>
