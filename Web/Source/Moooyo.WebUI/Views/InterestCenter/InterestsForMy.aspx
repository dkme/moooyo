<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    我的兴趣(<%=Model.interestCount%>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<style type="text/css">
    .tabdiv{width:100%; background-color:#f6f6f6; height:50px;}
    .tabdiv a{ margin-left:20px; margin-top:10px; font-size:16px; float:left;}
    .tabdiv .aone{color:#f17171;}
    .tabdiv .aonet{color:#f17171;}
    .tabdiv .atwo{color:#0099ff;}
    .tabdiv .atwot{color:#0099ff;border-bottom:solid 2px #0099ff;}
    .tabdiv .athe{color:#a3e978;}
    .tabdiv .athet{color:#a3e978;border-bottom:solid 2px #a3e978;}
    .tabdiv .addinterest{font-size:12px; float:right; margin-right:10px; margin-top:25px;}
    .machmoer{width:100%; background-color:#f6f6f6;height:30px; line-height:30px; text-align:center; cursor:pointer; margin-top:20px;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="c976 clearfix">
    <!--个人左面板-->
    <% if (Model.IsOwner)
       {%>
    <% Html.RenderPartial("~/Views/Member/ProfileLeftPanel.ascx");%>  
    <% }
       else
       { %>
    <% Html.RenderPartial("~/Views/Member/MemberLeftPanel.ascx");%> 
    <% } %>
    <!--endof 个人左面板-->
    <section class="inter-conbox mr15 mt32 fl">
    <div class="tabdiv">
<%--        <a href="/InterestCenter/InterestsForMy?type=1" class="aone" <%=ViewData["type"].ToString() == "1" ? "style=\"border-bottom:solid 2px #f17171;\"" : ""%>>所有话题</a>--%>
        <a onclick="bindnextno(1,2,'<%=Model.UserID %>',<%=(int)ViewData["pagesize"] %>,false)" class="atwo">我发布的话题</a>
        <a onclick="bindnextno(1,3,'<%=Model.UserID %>',<%=(int)ViewData["pagesize"] %>,false)" class="athe">我参与的话题</a>
        <a href="/InterestCenter/AddInterest" target="_blank" class="addinterest">添加兴趣</a>
    </div>
    <ul class="forinter-ask-list mt20" id="wenwenul">
        <%if (Model != null && Model.wenwenlist.Count > 0) {
              int i = 0;
              foreach (var wenwenobj in Model.wenwenlist)
              {
                  var interestobj = Model.interestlist[i];
                  if (wenwenobj != null && interestobj != null)
                  {
                      string content = wenwenobj.Content;
                      content = content.Length > 80 ? content.Substring(0, 80) + "<span class=\"letspa--3\">...</span>" : content;
                      if (wenwenobj.ContentImage!=null&&wenwenobj.ContentImage!="")
                      {
                          content = content + "[图]";
                      }
                      for (int j = 0; j < content.Length; j++) {
                          if (content.IndexOf("\n") >= 0) 
                              content = content.Replace("\n", "<br/>");
                          else 
                              break;
                      }
                      content = Comm.getExpression(content);
                %>
        <li class="greenbg clearfix">
            <a href="/InterestCenter/InterestFans?iid=<%=interestobj.ID %>" target="_blank"><img src="<%=Comm.getImagePath(interestobj.ICONPath, ImageType.Icon) %>" data-interestid="<%=interestobj.ID %>" class="fl mr15  wwimage interestimg"/></a>
            <div class="fl mr15 wwcontent"><a href="/WenWen/ShowWenWen?wwid=<%=wenwenobj.ID %>" target="_blank" id='interestqQuest<%=wenwenobj.ID %>'><span style="color:#666;"><%=content%></span></a><br /><span style="color:#aaa;"><%=Comm.getTimeSpan(wenwenobj.CreatedTime)%><b class="mr15 fl"><a href="/WenWen/ShowWenWen?wwid=<%=wenwenobj.ID %>" target="_blank" id='A1'>回复(<%=wenwenobj.AnswerCount%>)</a></b></span>
            </div>
        </li>
                <%}
                  i++;
              }
        } %>
        <%int ccount = Model.wenwencount - (Model.pageno * (int)ViewData["pagesize"]);
          if (ccount > 0)
          { %><li class="machmoer" onclick="bindnextno(<%=Model.pageno+1 %>,<%=ViewData["type"].ToString() %>,'<%=Model.UserID %>',<%=(int)ViewData["pagesize"] %>,true)">更多(<%=ccount%>)</li><%}%>
    </ul>
    </section>
    <aside class="asidebox-r mt32 fr">
    <% Html.RenderAction("AppPush", "Push");%> 
    <% Html.RenderAction("MyInterest", "Push"); %>
    <% Html.RenderAction("SameInterestingMemberOverMe", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
    <% if(Model.IsOwner) Html.RenderAction("GuessYourInterest", "Push"); %>
    <% Html.RenderAction("Question", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
    </aside>
</div>   
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/data_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript">
    $().ready(function () {
        interestCenter.bindinterestLabel($("#wenwenul img.interestimg"));
        showtab('<%=ViewData["type"].ToString() %>');
    });
    function showtab(type) {
        $("a.atwo").removeClass("atwot");
        $("a.athe").removeClass("athet");
        switch (parseInt(type)) {
            case 2: $("a.atwo").addClass("atwot"); break;
            case 3: $("a.athe").addClass("athet"); break;
        }
    }
    function bindnextno(pageno, type, userid, pagesize, ifadd) {
        showtab(type);
        WenWenLinkProvider.getwenwenformyinterest(pageno, type, function (data) {
            var wenwens = $.parseJSON(data);
            var wenwenlist = wenwens.wenwenlist;
            var interestlist = wenwens.interestlist;
            var str = "";
            var ccount = wenwens.wenwencount - (wenwens.pageno * pagesize);
            $.each(wenwenlist, function (i) {
                var wenwenobj = wenwenlist[i];
                var interestobj = interestlist[i];
                if (interestobj != null && wenwenobj != null) {
                    var titlestr = wenwenobj.Title;
                    var contentstr = wenwenobj.Content;
                    contentstr = contentstr.length > 80 ? contentstr.substr(0, 80) + "<span class=\"letspa--3\">...</span>" : contentstr;
                    if (wenwenobj.ContentImage != null && wenwenobj.ContentImage != "") {
                        contentstr = contentstr + "[图]";
                    }
                    for (var j = 0; j < contentstr.length; j++) {
                        if (contentstr.indexOf('\n') >= 0)
                            contentstr = contentstr.replace("\n", "<br/>");
                        else
                            break;
                    }
                    contentstr = getExpression(contentstr);
                    str += "<li class=\"greenbg clearfix\"><a href=\"/InterestCenter/InterestFans?iid=" + interestobj.ID + "\" target=\"_blank\"><img src=\"" + photofunctions.geticonphotoname(interestobj.ICONPath) + "\" data-interestid=\"" + interestobj.ID + "\" class=\"fl mr15  wwimage interestimg\"/></a><div class=\"fl mr15 wwcontent\"><a href=\"/WenWen/ShowWenWen?wwid=" + wenwenobj.ID + "\" target=\"_blank\" id='interestqQuest" + wenwenobj.ID + "'><span style=\"color:#666;\">" + contentstr + "</span></a><br /><span style=\"color:#aaa;\">" + getTimeSpan(paserJsonDate(wenwenobj.CreatedTime)) + "<b class=\"mr15 fl\"><a href=\"/WenWen/ShowWenWen?wwid=" + wenwenobj.ID + "\" target=\"_blank\" id='interestqQuest" + wenwenobj.ID + "'>回复(" + wenwenobj.AnswerCount + ")</a></b></span></div></li>";
                }
            });
            if (ccount > 0) {
                str += "<li class=\"machmoer\" onclick=\"bindnextno(" + (pageno + 1) + "," + type + ",'" + userid + "'," + pagesize + ",true)\">更多(" + ccount + ")</li>";
            }
            $("li.machmoer").remove();
            if (ifadd) $("#wenwenul").html($("#wenwenul").html() + str);
            else $("#wenwenul").html(str);
            interestCenter.bindinterestLabel($("#wenwenul img.interestimg"));
        });
    }
</script>
</asp:Content>
