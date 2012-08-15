<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.Content.IndexContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
遇见柚子
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="Msgbox4" data-ifopen="false"  style="position:absolute; display:none; top:0px; left:0px; z-index:888;">
    <div class="pup_upbox boxy" >
        <div class="left_arrowy "><div class="top_al y"></div><div class="arrowbg02 y"></div></div>
        <div class="pup_content"><span style="color:#7e5e38">从这可以找到更多柚子</span></div>
    </div>
</div>
<%
    string interestid = ViewData["interestID"].ToString();
    string city = ViewData["city"].ToString();
    string sex = ViewData["sex"].ToString();
    interestid = interestid == "" ? "all" : interestid;
    city = city == "" ? "all" : city;
    sex = sex == "" ? "all" : sex;
%>
<input type="hidden" id="interestid" name="interestid" value="<%=interestid %>"/>
<input type="hidden" id="city" name="city" value="<%=city %>"/>
<input type="hidden" id="sex" name="sex" value="<%=sex %>"/>
<input type="hidden" id="userid" name="userid" value="<%=Model.UserID %>"/>
<%Html.RenderPartial("~/Views/Content/ShowNewMember.ascx"); %>
    <!--start 中间内容部分 -->
<div class="indexbox">
    <div class="Letter_intro">
        <!--个人左面板-->
        <% if (Model.Member != null)
           {
               Html.RenderPartial("~/Views/Member/ProfileLeftPanelHead.ascx");
               %><div style="clear:both"></div><div class="mt10"></div><% 
           }%>
        <!--endof 个人左面板-->
        <div class="mycare" style="background:none">
            <div class="Letter_demo ">
            <%foreach (var obj in Model.interestList)
              {
                  if (obj == null) { continue; }
                  %><div class="Letter_interview1" ><a href="/Content/IndexContent/<%=obj.ID %>/<%=city %>/<%=sex %>/1"><img src="<%=Comm.getImagePath(obj.ICONPath,ImageType.Middle) %>" width="90px" height="90"></a><font class="fzt"><%=obj.Title.Length > 10 ? obj.Title.Substring(0, 10) + ".." : obj.Title%></font></div><%
              }%>
            <%if (Model.Member != null)
              { 
                  %><div class="addview"><a href="/InterestCenter/AddInterestFans" ></a></div><%
              } %>
            <%else
              { 
                  %><div class="addview"><a href="/Account/Login" target="_blank"></a></div><%
              }%>
            </div>
        </div>
        <div class="mt10"></div>
        <%Html.RenderPartial("AddBu"); %>
        <%if(Model.Member!=null) Html.RenderPartial("~/Views/Member/ProfileLeftPanelPointsSchedule.ascx"); %>
    </div>
    
    <div class="indexcontent"> 
        <%
            string fristinterestID = Model.interestList[0] != null ? Model.interestList[0].ID : "";
            string fristinterestTitle = Model.interestList[0] != null ? Model.interestList[0].Title : "";
            string fristinterestContent = Model.interestList[0] != null ? Model.interestList[0].Content : "";
            string fristinterestFansCount = Model.interestList[0] != null ? Model.interestList[0].FansCount + "" : "0";
            string fristinterestImage = Model.interestList[0] != null ? Comm.getImagePath(Model.interestList[0].ICONPath, ImageType.Middle) : "";
            string fristInterestSelfhoodPicture = Model.interestList[0] != null ? Comm.getImagePath(Model.interestList[0].SelfhoodPicture, ImageType.Original) : "";
             %>
        <div class="mainppt1 h250">
           <div class="self_con Trans iedata">
             <%if (Model.interestList.Count > 0)
               {
                   string content = fristinterestContent;
                   int index = 0;
                   string[] contentList = content.Split('\n');
                   content = "";
                   foreach (string str in contentList)
                   {
                       if ((double)str.Length / (double)29 > 1)
                           index += 2;
                       else if ((double)str.Length / (double)29 > 2)
                           index += 3;
                       else if ((double)str.Length / (double)29 > 3)
                           index += 4;
                       else
                           index++;
                       if (index <= 3) { content += str + "<br/>"; }
                       else if (index == 4) { content += str.Length > 29 ? str.Substring(0, 25) + ".." : str; break; }
                       else { break; }
                   }
                   %><a class="fan-content" href="/InterestCenter/ShowInterest/<%=fristinterestID %>"><%=content%></a><%
               }%>
           </div>
           <div class="edit_con">
               <a class="fans_hit" href="/InterestCenter/UpdateInterestFansList/<%=fristinterestID %>"><%=fristinterestFansCount%>个柚子在这里</a>
               <a class="share_a" title="分享" data-oldobj="indexContent" onmouseover="shareClick('http://www.moooyo.com/InterestCenter/ShowInterest/<%=fristinterestID %>','<%=fristinterestTitle %>','<%=fristinterestContent.Replace("\n", "")%>',$(this),'http://www.moooyo.com<%=fristinterestImage %>')">&nbsp;&nbsp;&nbsp;</a>
               <%--<a class="quit_a" href="javascript:void(0)" target="_blank" title=""><em></em></a>--%>
           </div>
            <div class="chose_city">
                <div class="city_title">
                   <div class="city_con Trans">
                    <span class="s-l">{ <%if (Model.interestList.Count > 0)
                              { %><a href="/InterestCenter/ShowInterest/<%=fristinterestID %>"><%=fristinterestTitle%></a><%}
                              else { %>遇见柚子<%} %> @ <a class="city_t" href="javascript:void(0)"><%=city=="all"?"全国":city %></a> }</span>
                              <span class="s-r"><a href="/InterestCenter/ShowInterest/<%=fristinterestID %>"><img src="<%=fristinterestImage%>" width="50px" height="50px"  /></a></span> 
                              </div>
                    <div class="city_box">
                        <div class="pup_upbox boxw" style="padding:0;" >
                            <div class="top_arrow arrow_left"> <div class="top_al w"></div><div class="arrowbg02 w"></div></div>
                            <div class="pup_content">
                                <div class="pup_city">
                                    <div class="city_l"><span><a href="/Content/IndexContent/<%=interestid %>/all/<%=sex %>/1">全国</a></span></div>
                                    <div class="city_r">
                                        <ul>
                                            <% foreach (string activitycity in Model.ActivityCityList)
                                               {
                                                    %><li><a href="/Content/IndexContent/<%=interestid %>/<%=activitycity %>/<%=sex %>/1"><%=activitycity%></a></li><%
                                               }%>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="pptindex">   
                <%--<%if (Model.imagePush != null)
                  {
                      String pushImage = Comm.getImagePath(Model.imagePush.ImageList[0].ImageUrl, ImageType.Original);
                      String setPushImage = ViewData["pushImage"] == null ? "" : ViewData["pushImage"].ToString();
                      if (setPushImage != "" && setPushImage != null)
                      {
                          pushImage = setPushImage;
                      }
                      %><img src="<%=pushImage%>" height="250" width="785"/><%
                  }--%>
                  <%if (fristInterestSelfhoodPicture != "" && fristInterestSelfhoodPicture != "/pics/noicon.jpg")
                    { 
                        %><img src="<%=fristInterestSelfhoodPicture%>" width="785" height="250" /><%
                    }
                    else
                    { 
                      %><img src="/pics/ppt.jpg" width="785" /><%
                    }%>
            </div>
            <div class="ifocus_btn">
                <div class="nav_right">
                <%if (Model.imagePush != null)
                    { %>
                    <span><%=Model.imagePush.ImageContent %></span>
                    <span>by <%if (Model.Member != null)
                               { %><a href="/Content/TaContent/<%=Model.imagePush.MemberID %>/all/1" style="color:#FFF;" onclick="clickImagePush('<%=Model.imagePush.ID %>')" target="_blank"><%=Model.imagePush.Creater.NickName%></a><%} %><%else{ %><a href="/Account/Login" style="color:#FFF;" target="_blank" onclick="clickImagePush('<%=Model.imagePush.ID %>')"><%=Model.imagePush.Creater.NickName%></a><%} %></span>
                    <%} %>
                </div>
            </div>
        </div>
        <div class="index_box alpha60 TransOne">
            <span class="contentLoading">加载中...</span>
            <div id="showcontent" style="position:relative;">
                <%if (Model.Pagger != null && Model.Pagger.PageNo >= 2)
                    {
                        foreach (var obj in Model.ContentList)
                        { 
                        %><div class="care_com" style="display:none;"><%Html.RenderAction("ContentItem", "Content", new { contentobj = obj, ifshowmember = true, ifmy = false });%></div><%
                        }
                    }%>
            </div>
         <div class="padding_b50">&nbsp;</div>
        <!--分页-->
        <div id="pagediv" <%=Model.Pagger.PageNo==1?"style=\"display:none\"":"" %>>
            <%if (Model.Pagger != null)
              {
                  if (Model.Pagger.PageCount > 1)
                  {
                      Html.RenderAction("pagger", "Shared", new { nowpage = Model.Pagger.PageNo, pagecount = Model.Pagger.PageCount, additionID = Model.Pagger.AdditionParams, url = Model.Pagger.PageUrl });
                  }
                  if (Model.Pagger.PageNo <= 1)
                  {
                  %><input type="hidden" id="data-pageno"  name="data-pageno" value="<%=Model.Pagger.PageNo %>" /><%
                  }
              }%>
        </div>
        <%if (Model.Pagger.PageNo <= 1 && Model.contentCount > 10)
          { 
              %><div class="loading" id="ContentLoadMore"></div><%
          } %>
        <!--endof 分页-->
        <div class="padding_b50">&nbsp;</div>
        </div>
      
    </div>
    <!--end中间内容部分 -->
    <div id="gotop"></div>
    <!--用户自定义的背景图片-->
<%if (Model.Member.MemberSkin != null){
      if (Model.Member.MemberSkin.PersonalityBackgroundPicture != "")
      { %>
<input type="hidden" id="backimage" name="backimage" value="<%=Model.Member.MemberSkin.PersonalityBackgroundPicture %>" style=""/>
    <% }
  }%>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css" media="screen"/>
<link href="/css/msg.css" rel="stylesheet" type="text/css" />
<style type="text/css"> 
  .msg-p { width:65px; height:70px; position:absolute;  z-index:500;  display:block; background:url(/pics/msghover.png);}
  .msg-p ul { width:65px; list-style:none; padding:0; margin:0;  }
  .msg-p ul li { line-height:22px;  display:block; height:22px; width:65px; }
  .msg-p ul li.b { border-bottom:1px solid #ddd;}
  .msg-p ul li a{ font:normal 12px/22px "\5FAE\8F6F\96C5\9ED1"; height:22px; width:57px; display:inline-block; font-size:12px; padding-left:8px; color:#999; text-align:left; text-decoration:none;}
  .msg-p ul li a:hover { color:#333;}
  .msg-p .toparrow { width:8; height:8; position:absolute; top:-7px;  left:15px; z-index:505;  }
  .msg-p .arr,.msg-p .arrg  {  border-width:0 8px 8px 8px; border-style:dashed  dashed solid dashed; border-color: transparent transparent #fff transparent; position:absolute; line-height:0; font-size:0;}
  .msg-p .arrg { border-bottom-color:#fff; }

</style>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/main_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/Scripts/jquery.masonry.js" type="text/javascript"></script>
<script src="/Scripts/jquery.masonry.min.js" type="text/javascript"></script>
<script src="/js/mainBobyimg.js" type="text/javascript"></script>

<!--[if IE 6]>
    <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('em,.self_con,.Trans,.edit_con a,.em');
	 </script>
<![endif]-->
<!--[if lt IE 10]>
<script type="text/javascript" src="/js/PIE.js"></script>
<![endif]-->
<script language="JavaScript" type="text/javascript">
    uploadpath = '<%=Model.UploadPath %>';
    var noContentMsg = "粉少不能怪父母，怪你分享不刻苦。<br />赶紧<a href=\"/Content/AddImageContent\">发布</a>点新鲜内容，让更多柚子知道你的存在~";
    //更新推送图片的点击计数器
    function clickImagePush(id) {
        ImagePushProvider.UpdatePushClickCount(id, function (data) { });
    }
    $(document).ready(function () {
       
        $('#wrap').css({ "width": "980px", "background": "none" });
        //更新推送内容的显示计数器
        if ($("#pushimageid").val() != null) {
            ImagePushProvider.UpdatePushShowCount($("#pushimageid").val(), function (data) { });
        }
        $("li.addinterestfans").hover(function () {
            var msgdiv = $("#Msgbox4");
            msgdiv.css("left", $(this).offset().left + 65 + "px");
            msgdiv.css("top", $(this).offset().top + msgdiv.height() / 2 + "px");
            msgdiv.attr("data-ifopen", "true");
            msgdiv.stop();
            msgdiv.slideDown(300);
        }, function () {
            var msgdiv = $("#Msgbox4");
            msgdiv.css("left", $(this).offset().left + 65 + "px");
            msgdiv.css("top", $(this).offset().top + msgdiv.height() / 2 + "px");
            msgdiv.attr("data-ifopen", "false");
            msgdiv.stop();
            msgdiv.slideUp(300);
        });

        $('.city_t').click(function () { $('.city_box').css({ "visibility": "visible" }); $('.city_box').slideDown(5000); });

        $('.Letter_demo ').each(function () {
            $(this).find('.Letter_interview1').hover(function () {
                $(this).find('font').css({ "display": "block" });
                $(this).find('a').css({ "opacity": "0.4", "background": "#000" });
            }, function () {
                $(this).find('a').css({ "opacity": "1", "background": "none" });
                $(this).find('font').css({ "display": "none" });
            });
        });
        //当处于第一页时，第一批数据用Ajax加载
        if ($("#data-pageno").val() != null) {
            var data_pageno = $("#data-pageno").val();
            if (parseInt(data_pageno) <= 1) {
                showContent(1, true, false);
            }
            else {
                ifopenmasonrynotimeline = true;
                masonrynotimeline();
            }
        }
        else {
            ifopenmasonrynotimeline = true;
            masonrynotimeline();
        }

    });

    $(window).scroll(function () { loadcontent(); });

    function showContent(pageno, ifshowmember, ifmy) {
        ifopenmasonrynotimeline = true;
        var interestid = $("#interestid").val();
        var city = $("#city").val();
        var sex = $("#sex").val();
        var userid = $("#userid").val();
        ContentProvider.IndexContentToAjax(pageno, interestid, city, sex, function (data) {
            var data = $.parseJSON(data);
            var ContentList = data.ContentList;
            var top = $("#showcontent .care_com:last").html() != null ? $("#showcontent .care_com:last").offset().top : 5;
            var height = $("#showcontent .care_com:last").html() != null ? $("#showcontent .care_com:last").height() : 0;
            str = "";
            if (data.ContentList.length <= 0 && pageno == 1) {
                str = noContentMsg;
            }
            else {
                for (var i = 0; i < ContentList.length; i++) {
                    str += "<div class=\"care_com\" style=\"position: absolute; top:" + (top + height + 5) + "px;left:5px;display:none;\">" + getContentListStr(data, ContentList[i], ifshowmember, ifmy, pageno, userid) + "</div>";
                }
            }
            if (pageno <= 1) {
                $("#showcontent").html(str).masonry({ itemSelector: '.care_com' });
                getpagestr(pageno);
                if ($(".contentLoading").html() != null) $(".contentLoading").remove();
                $("div.care_com").css("display", "block");
            }
            else {
                var strobj = $(str);
                $("#showcontent").append(strobj).masonry('appended', strobj);
                getpagestr(pageno);
                if ($(".contentLoading").html() != null) $(".contentLoading").remove();
                masonrynotimeline();
                $("div.care_com").css("display", "block");
            }
            loadRightBu();
            loadiconmove();
        });
    }
    function getpagestr(pageno) {
        if (pageno < 3) {
            $("#ContentLoadMore").html("<b class=\"rtop\"><b class=\"r1\"></b><b class=\"r2\"></b><b class=\"r3\"></b><b class=\"r4\"></b></b><div class=\"loaded\"><a id=\"ContentLoadMoreClick\" onclick=\"showContent(" + (pageno + 1) + ",true,false)\">点击加载</a></div><b class=\"rbottom\"><b class=\"r4\"></b><b class=\"r3\"></b><b class=\"r2\"></b><b class=\"r1\"></b></b>");
        }
        else {
            $("#ContentLoadMore").remove();
            $("#pagediv").css("display", "block");
        }
    }

   

 </script>
<!-- newcomer render -->
<script id="j-newcomer" type="text/tpl">
  <div id="newcomer" class="newcomer">
  	<div class="hd">新柚子</div>
  	<div class="newcomer-list"></div>
  </div>
</script>
<script id="j-newcomer-item" type="text/tpl">
  <div class="newcomer-item" data-toobjectid="{{ToObjectID}}" data-type="{{Type}}">
    <a href="/Content/TaContent/{{MemberID}}" target="_blank">
        <img src="{{Creater.ICONPath}}" alt="{{Creater.NickName}}" class="avatar" />
    </a>
    <div class="popover nickname">{{Creater.NickName}}</div>
    <div class="popover detail">
      <a href="/Content/TaContent/{{MemberID}}" target="_blank">
        <img src="{{Creater.ICONPath}}" alt="{{Creater.NickName}}" class="avatar" />
      </a>
      <div class="cnt">
        <a class="nickname" href="/Content/TaContent/{{MemberID}}" target="_blank">{{Creater.NickName}}</a>
        <p class="sex">{{Creater.Sex}}</p>
        <p class="city">{{Creater.City}}</p>
        <div class="action" data-ifmove="false">
          <p class="action-1 cf"><a href="javascript:void(0)" class="mo">mo</a><span class="count"></span></p>
          <p class="action-2 cf"><a href="javascript:void(0)" class="comment hide-text">comment</a><span class="count"></span></p>
        </div>
      </div>
    </div>
  </div>
</script>
<link rel="stylesheet" type="text/css" href="/css/newcomer.css"/>
<script src="/js/mustache.min.js" type="text/javascript"></script>
<script src="/js/newcomer.js" type="text/javascript"></script>

<% 
//如果用户第一次登陆，显示引导
if (!(bool)ViewData["guideShowed"])
{%>
<script id="j-guide-meet" type="text/tpl">
  <div id="guide" class="guide meet">
    <div class="mask"></div>
    <div class="step step-1">
      <div class="pic">
        <img src="/pics/guide/tree.png" alt="moooyo" />
      	<p class="text">这里可以<br/>遇见柚子</p>
      </div>
    	<div class="text">
    		<p>很好，你来到了遇见柚子板块~</p>
    		<p>在这里，你可以找到和你兴趣相投的柚子们~</p>
    		<p class="last cf"><span class="fn-left">还可以发布你的单身分享~</span><img class="next" src="/pics/guide/mouse.png" alt="click"/></p>
    	</div>
    </div>
    <div class="step step-2">
      <div class="groups"></div>
    	<img class="arrow" src="/pics/guide/arrow-left.png" alt="->" />
      <div class="text">
        <p>这是你注册时选择的 3 个兴趣群组。</p>
        <p class="cf"><span class="fn-left">分别点它们就能看到所选群组的柚子发布的最新内容了。</span><img class="next" src="/pics/guide/mouse.png" alt="click"/></p>
      </div>
    </div>
    <div class="step step-3">
    	<img src="/pics/guide/meet-mask-step-3.png" alt="118" />
    	<img class="arrow" src="/pics/guide/arrow-left.png" alt="->" />
      <div class="text">
        <p>3个兴趣群组不够？</p>
        <p class="cf"><span class="fn-left">点这里，添加更多兴趣群组，认识更多柚子。</span><img class="next" src="/pics/guide/mouse.png" alt="click"/></p>
      </div>
    </div>
    <div class="step step-4">
      <p class="text cf"><span class="fn-left">与其自己去寻找，为何不让他人来发现你呢？<br/>点这里，精彩内容一经发布，就会有志趣相投的柚子发现你哦~</span><img class="next" src="/pics/guide/mouse.png" alt="click"/></p>
    	<img class="arrow" src="/pics/guide/arrow-down-left.png" alt="->" />
    	<img src="/pics/guide/meet-mask-step-4.png" alt="118" />
    </div>
    <div class="step guide-newcomer">
      <img class="arrow" src="/pics/guide/arrow-up-right.png" alt="->" />
      <p class="text cf"><span class="fn-left">在米柚，不以真实照片作为头像的行为都是耍流氓~<br/>当你上传一张近照作为头像后，你就能华丽丽站在这里，吸引更多柚子的关注了啦~</span><img class="next" src="/pics/guide/mouse.png" alt="click"/></p>
    </div>
    <div class="step step-5">
      <div class="pic">
        <img src="/pics/guide/tree.png" alt="moooyo" />
      	<p class="text finish">开始玩吧</p>
      </div>
      <div class="text">
    		<p>懂得差不多了？那就赶紧发布精彩内容，玩转米柚吧！</p>
    		<p>当然，米柚的乐趣可不止这一点点哦，这还有待你日后发掘哟~</p>
      </div>
    </div>
    <div class="steps">
    	<span class="dot current">·</span>
    	<span class="dot">·</span>
    	<span class="dot">·</span>
    	<span class="dot">·</span>
    	<span class="dot">·</span>
        <span class="dot">·</span>
    </div>
  </div>
</script>
<link rel="stylesheet" type="text/css" href="/css/guide.css"/>
<script src="/js/guide.js" type="text/javascript"></script>
<script src="/js/guide.meet.js" type="text/javascript"></script>
<%} %>
</asp:Content>
