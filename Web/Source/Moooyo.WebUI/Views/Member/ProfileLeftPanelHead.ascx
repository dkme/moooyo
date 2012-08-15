<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%
    if (Model.IsOwner)
    {
        string avatarToUrl = Model.Member.ICONPath == "/pics/noicon.jpg" ? "/Setting/UploadFace" : "/Content/IContent";
      %>
<div class="Letter_demo "><span class="head_pic"><a href="<%=avatarToUrl %>" title="<%=Model.Member.Name %>"><img src="<%= Model.Member.ICONPath  %>" height="92px" width="90px" alt="<%=Model.Member.Name %>" /></a>
           <%
        if (Model.Member.IsRealPhotoIdentification)
        { 
                 %><em>
             <img src="/pics/vedio_pic1.png" alt="已通过视频认证" title="已通过视频认证" width="16" height="17"/>
             </em><%
        }
        else
        {
                 %>
             <em>
             <img class="cursor" onclick="window.location='/Setting/Authentica';" src="/pics/vedio_pic2.png" alt="未通过视频认证" title="未通过视频认证" width="16" height="17"/>
             </em>
             <% 
        }
         %>
           </span><span class="person_info" title="魅力值"><b class="m"><%=Model.Member.GlamourCount%></b><b class="j" title="米果"><%=Model.Member.Points%></b></span></div>         
            <div class="Letter_demo">
            <%
        if (Model.UserID == Model.MemberID)
        {
             %>
                <span class="com_info" style="cursor:pointer;" title="关注" onclick="window.location.href='/Content/IFavorerContent'"><b class="h"><%=Model.Member.FavorMemberCount%></b></span>
                <span  class="com_info" style="cursor:pointer;" title="粉丝" onclick="window.location.href='/Relation/Fans'"><b class="f"><%=Model.Member.MemberFavoredMeCount%></b></span>
            <%
        }
        else
        { 
                  %>
                <span class="com_info" title="关注"><b class="h"><%=Model.Member.FavorMemberCount%></b></span>
                <span  class="com_info" title="粉丝"><b class="f"><%=Model.Member.MemberFavoredMeCount%></b></span>
             <%
        }
                %>
            </div>
            <%
    }
    else
    {
       %>
            <div class="About_demo ">
                <a href="/Content/TaContent/<%=Model.MemberID %>" target="_blank" class="about-a" title="<%=Model.Member.Name!=null?Model.Member.Name:"" %>"><img src="<%= Model.Member.ICONPath  %>" width="182" height="182" alt="<%=Model.Member.Name!=null?Model.Member.Name:"" %>" /></a>
           </div>         
            <div class="About_demo ">
              <span><%=Model.Member.Name != null ? Model.Member.Name : ""%>
           <%
        if (Model.Member.IsRealPhotoIdentification)
        { 
                 %><em>
             <img src="/pics/vedio_pic1.png" alt="已通过视频认证" title="已通过视频认证" width="16" height="17"/>
             </em><%
        }
        else
        {
                 %>
             <em>
             <img class="cursor" onclick="Invert.Authentica('<%=Model.MemberID %>',$(this))" src="/pics/vedio_pic2.png" alt="邀请Ta完成视频认证" title="邀请Ta完成视频认证" width="16" height="17"/>
             </em>
             <% 
        }
                %></span>
            </div>
            <%
    }
              %>
       