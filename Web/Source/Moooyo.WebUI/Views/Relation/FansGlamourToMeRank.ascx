<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.GlamourCountsModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<%if (Model != null && Model.memberInterestListObj.Count > 0)
  {
      
      String[] arrGlamourCount;
      int i = 0;
      switch (Model.skin)
      {
          case "fans":
              %>
              <div class="fans_box"><span class="fans_s">魅力贡献最大的<% if (Model.Member.FansGroupName != null) { Response.Write(Model.Member.FansGroupName.Name); } else { Response.Write("粉丝"); } %></span></div>
              <div class="fans_box"><%
                
              foreach (var mILObj in Model.memberInterestListObj)
                {
                    arrGlamourCount = Model.memberGlamourCountListObj[i].ToString().Split('|');   %>

        <dl class="fans_list">
            <dt><a href="/Content/TaContent/<%=arrGlamourCount[0] %>/all/1" target="_blank" id="relationMemberInfo"><img src="<%=Comm.getImagePath(mILObj.Key.MemberInfomation.IconPath, ImageType.Middle) %>" height="82" width="81" data_me_id="<%=Model.UserID %>" data_member_id="<%=mILObj.Key.ID %>" name="relationMemberInfoArea" title="<%=mILObj.Key.MemberInfomation.NickName %>" /></a><em></em></dt>
            <dd><span class="blue02"><%=mILObj.Key.MemberInfomation.NickName.Length > 7 ? mILObj.Key.MemberInfomation.NickName.Substring(0, 6) + "<span class=\"ellipsis\">...</span>" : mILObj.Key.MemberInfomation.NickName%></span><em>
            <% if (mILObj.Key.MemberPhoto.IsRealPhotoIdentification)  { %><img src="/pics/video_pic.png" height="14" width="14" /> <% } %></em>
            </dd>
            <dd>贡献<font><%=arrGlamourCount[1]%>分</font></dd>
        </dl>

    <% i++;
        }
        %></div><%
        break;
          default: break;
      }
      
  } %>