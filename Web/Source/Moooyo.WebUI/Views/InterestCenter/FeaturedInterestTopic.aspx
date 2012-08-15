<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberFeaturedInterestTopicModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	发现
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="FeaturedInterestTopic">
        <div class="top cgray">
            <div class="fl"><span class="fs24">群落中发现...</span><span>
            
            <%--<%=Model.boyPublishedTopics == "1" ? "<span class=\"boyPublishedTopics\" onclick=\"boyclick()\">【大米】</span>" : "<span class=\"girlPublishedTopics\" onclick=\"boyclick()\">【大米】</span>"%><%=Model.girlPublishedTopics == "1" ? "<span class=\"boyPublishedTopics\" onclick=\"girlclick()\">【柚子】</span>" : "<span class=\"girlPublishedTopics\" onclick=\"girlclick()\">【柚子】</span>"%>--%>
            <%
    string aclick = "";
    string topictoboystr = Model.boyPublishedTopics;
    string topictogirlstr = Model.girlPublishedTopics;
    String personalityName = "";
    switch (Model.publishedTopicSex) { 
        case 1: personalityName = "大米"; break;
        case 2: personalityName = "柚子"; break;
        default: break;   
    }
    
    if (topictoboystr == "1" && topictogirlstr == "0")
    {
        aclick = "<a class=\"boyPublishedTopics\">【大米】</a><a class=\"unselectedPublishedTopics\" href=\"/InterestCenter/FeaturedInterestTopic/01\">【柚子】</a><a class=\"unselectedPublishedTopics\" href=\"/InterestCenter/FeaturedInterestTopic/11\">【所有】</a>";
    }
    if (topictoboystr == "0" && topictogirlstr == "1")
    {
        aclick = "<a class=\"unselectedPublishedTopics\" href=\"/InterestCenter/FeaturedInterestTopic/10\">【大米】</a><a class=\"girlPublishedTopics\">【柚子】</a><a class=\"unselectedPublishedTopics\" href=\"/InterestCenter/FeaturedInterestTopic/11\">【所有】</a>";
    }
    if (topictoboystr == "1" && topictogirlstr == "1")
    {
        aclick = "<a class=\"unselectedPublishedTopics\" href=\"/InterestCenter/FeaturedInterestTopic/10\">【大米】</a><a class=\"unselectedPublishedTopics\" href=\"/InterestCenter/FeaturedInterestTopic/01\">【柚子】</a><a class=\"allPublishedTopics\">【所有】</a>";
    }
    Response.Write(aclick);%>
            
            
            （<%=Model.allMemberCount%>单身米柚们在这里群居）</span></div><div class="fr"><%--<span class="fs24">长沙</span>--%></div>
        </div>

         
        <div class="middle">
            <div class="left">
                <div class="title">
                    <span class="fc-85bf53 f14 fl ml15">单身<%=personalityName%>说</span>
                </div>
                
                <div class="left2">
                <% if (Model.latestWenWenList != null)
                   {
                       Html.RenderAction("MyInterestTowwItem", "Push", new { topiclist = Model.latestWenWenList, showcount = 4 });
                   } %>
                </div>
                
            </div>
            <div class="right" style="float:left;">
                <div class="title4">
                    <span class="fc-85bf53 f14 fl ml15">单身增加最快兴趣组</span>
                </div>
                <div class="content3">
                    <ul class="content-ul01">
                 <% if (Model != null && Model.dailyInterestRankingList.Count > 0 && Model.dailyInterestRankingInterestList.Count > 0)
                    { 
                        for (int i = 0; i < Model.dailyInterestRankingInterestList.Count; i++)
                        {
                            if (i < Model.dailyInterestRankingInterestList.Count)
                            {
                                var dailyInterestRankingObj = Model.dailyInterestRankingList[i];
                                var dailyInterestRankingInterestObj = Model.dailyInterestRankingInterestList[i];
                                %>
                        <li data-interestid="<%=dailyInterestRankingInterestObj.ID %>"><a href="/InterestCenter/InterestFans?iid=<%=dailyInterestRankingInterestObj.ID %>" target="_blank" title="<%=dailyInterestRankingInterestObj.Title %>"><img src="<%=Comm.getImagePath(dailyInterestRankingInterestObj.ICONPath,ImageType.Middle) %>" width="60" height="60" border="0" /></a><br /><a href="/InterestCenter/InterestFans?iid=<%=dailyInterestRankingInterestObj.ID %>"  class="none-12-000000" target="_blank" title="<%=dailyInterestRankingInterestObj.Title %>"><%=dailyInterestRankingInterestObj.Title.Length > 4 ? dailyInterestRankingInterestObj.Title.Substring(0, 4) + "<span class=\"letspa--3\">...</span>" : dailyInterestRankingInterestObj.Title%></a></li>
                        <% } %>
                    <% } %>
                 <% } %>
                    </ul>
                    <div class="clearfix" style="_height:0px; _line-height:0px;"></div>
                    <div class="addInterestbtn"><a href="/InterestCenter/AddInterest"> + 创建我自己的兴趣组</a></div>
                </div>
            </div>
        </div>
        

        <div class="bottom">
            <div class="title5">
                <span class="fc-85bf53 f14 fl ml15">单身<%=personalityName%>有料</span>
            </div>
            <div class="content4">

                <div class="topicLeft">
               <% foreach (Moooyo.BiZ.WenWen.WenWen topic in Model.leftTopicList)
               {
                   if (topic == null) continue;
                    if (topic.ContentImage == "" || topic.ContentImage == null)
                    {
                        Html.RenderAction("FeaturedInterestTopicNoImgPanel", "InterestCenter", new { topic = topic });
                    }
                    else
                    {
                        Html.RenderAction("FeaturedInterestTopicHaveImgPanel", "InterestCenter", new { topic = topic });
                    } 
                }%>
                </div>
                <div class="topicRight">
               <% foreach (Moooyo.BiZ.WenWen.WenWen topic in Model.rightTopicList)
               {
                   if (topic == null) continue;
                    if (topic.ContentImage == "" || topic.ContentImage == null)
                    {
                        Html.RenderAction("FeaturedInterestTopicNoImgPanel", "InterestCenter", new { topic = topic });
                    }
                    else
                    {
                        Html.RenderAction("FeaturedInterestTopicHaveImgPanel", "InterestCenter", new { topic = topic });
                    } 
                }%>
                </div>

                <% if (Model.pagecount > 10 && Model.Pagger.PageNo < 2)
                { %>
                <div class="clearfix"></div>
                    <div class="loadMoreTopics">
                        <div onclick="showMoreTopics(2)">加载更多...</div>
                    </div>
            <% } %>
            </div>
        </div>
        <div class="clearfix"></div>
        <div id="featuredTopicPaging" class="hidden">
        <%--<!--Begin paging-->
        <% if (Model.Pagger != null) {
            if (Model.Pagger.PageCount > 1) { %> 
               <% Html.RenderAction("QueryStrPaging", "Shared", new { nowpage = Model.Pagger.PageNo, pagecount = Model.Pagger.PageCount, additionID = Model.Pagger.AdditionParams, url = Model.Pagger.PageUrl }); %> 
           <% } %>
        <% } %>
        </div>
        <!--End paging-->--%>
        
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/data_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript">
    var boyPublishedTopic = "<%=Model.boyPublishedTopics %>", girlPublishedTopic = "<%=Model.girlPublishedTopics %>";
    var pageSize = <%=Model.Pagger.PageSize %>, bigPageNo = <%=Model.Pagger.PageNo %>, publishedTopicSex = <%=Model.publishedTopicSex %>;
    var pageCount = <%=Model.pagecount %>, bigPageSize = 30, pageNo = 1;
    var pageCounts = Math.ceil(pageCount / 30);
    var pageUrl = "/InterestCenter/FeaturedInterestTopic/" + boyPublishedTopic + girlPublishedTopic + "/";

    $().ready(function () {
        interestCenter.bindinterestLabel($(".content-ul01 li"));
        MemberInfoCenter.BindDataInfo($("div.myinterestuser img"));
        interestCenter.bindinterestLabel($(".bottom .content4 [name='interestA']"));
        MemberInfoCenter.BindDataInfo($(".bottom .content4 [name='memberInfoLabel']"));
        if(bigPageNo > 1) { 
            $("#featuredTopicPaging").removeClass("hidden");
            $("#featuredTopicPaging").show();
            $("#featuredTopicPaging").html(profileQueryStrPaging(bigPageNo, pageCounts, pageUrl).toString());
        }
    });

    function interestTopicHaveImgPanel(topic) {
        var str = "";
        var topicContent = decodeURIComponent(getExpression(topic.Content));
        var topicContentImage = getImageToTopic(topic.ContentImage == null ? "" : topic.ContentImage,1);
   
        str += "<div class=\"content5\">";
        str += "<div class=\"left3\">";
        str += "<div class=\"imageProjection\">";
        str += "<a href=\"/WenWen/ShowWenWen?wwid=" + topic.ID + "\" target=\"_blank\">" + topicContentImage + "</a>";
        str += "</div></div>";
        str += "<div class=\"center\">";
        str += "<div class=\"text\">";
        str += "<a href=\"/WenWen/ShowWenWen?wwid=" + topic.ID + "\" target=\"_blank\" class=\"none-12-666666\">" + (topicContent.Length > 89 ? topicContent.substring(0, 89) + "<span class=\"letspa--3\">...</span>" : topicContent) + "</a>";
        str += "</div><div class=\"author\">";
        str += "<span class=\"fl fc-666666\">by <a href=\"/Member/Ta/" + topic.Creater.MemberID + "\" target=\"_blank\" title=\"" + topic.Creater.NickName + "\">" + topic.Creater.NickName + "</a> &nbsp;</span><span class=\"mt5 fl\"><a href=\"/Member/Ta/" + topic.Creater.MemberID + "\" target=\"_blank\" title=\"" + topic.Creater.NickName + "\" name=\"memberInfoLabel\" data_me_id=\"<%=Model.UserID %>\" data_member_id=\"" + topic.Creater.MemberID + "\"><img src=\"" + photofunctions.geticonphotoname(topic.Creater.ICONPath) + "\" width=\"22\" height=\"22\" border=\"0\" style=\"margin-top:4px;\" /></a> &nbsp;</span><span class=\"fl\"></span><span class=\"fl cgray ml10\">" + (topic.AnswerCount == 0 ? "" : ("" + topic.AnswerCount + "回应")) + "</span>";
        str += "</div><div class=\"favorite\" style=\"float:left;\">";
        str += "<span class=\"mt8 manadiv fl\" style=\"float:left; margin:0px; padding:0px;\"><span class=\"like3\" style=\"float:left;\">&nbsp;</span><span class=\"like2\" id=\"like" + topic.ID + "\" style=\"float:left;\" onclick=\"WenWenControl.likeclick('" + topic.ID + "')\">" + topic.Likecount + "&nbsp;♥</span><span class=\"like1\" style=\"float:left;\">&nbsp;</span></span>";
        str += "</div></div><div class=\"right2\">";

        var interest = null;
        $.ajaxSetup({ async: false });
        interestCenterProvider.getInterest(topic.InterestID, function (data) {
            interest =  $.parseJSON(data);
        });
        $.ajaxSetup({ async: true });

        str += "<a href=\"/InterestCenter/InterestFans?iid=" + interest.ID + "\" target=\"_blank\" title=\"" + interest.Title + "\" name=\"interestA\" data-interestid=\"" + interest.ID + "\"><img src=\"" +  photofunctions.geticonphotoname(interest.ICONPath) + "\" width=\"30\" height=\"30\" border=\"0\" /></a></div></div>";

        return str;
    }

    function interestTopicNoImgPanel(topic) {
        var str = "";
        var topicContent = decodeURIComponent(getExpression(topic.Content));
        var topicContentImage = getImageToTopic(topic.ContentImage == null ? "" : topic.ContentImage, 1);

        str += "<div class=\"content5\">";
        str += "<div class=\"left4\">";
        str += "<a href=\"/WenWen/ShowWenWen?wwid=" + topic.ID + "\" target=\"_blank\" class=\"none-12-666666\">" + (topicContent.Length > 89 ? (topicContent.substring(0, 89) + "<span class=\"letspa--3\">...</span>") : topicContent) + "</a>";
        str += "</div><div class=\"right3\">";
        
        var interest = null;
        $.ajaxSetup({ async: false });
        interestCenterProvider.getInterest(topic.InterestID, function (data) {
            interest =  $.parseJSON(data);
        });
        $.ajaxSetup({ async: true });

        str += "<a href=\"/InterestCenter/InterestFans?iid=" + interest.ID + "\" target=\"_blank\" title=\"" + interest.Title + "\" name=\"interestA\" data-interestid=\"" + interest.ID + "\"><img src=\"" +  photofunctions.geticonphotoname(interest.ICONPath) + "\" width=\"30\" height=\"30\" border=\"0\" /></a></div>";
        str += "<div class=\"bottom2\"><span class=\"fl fc-666666\">by <a href=\"/Member/Ta/" + topic.Creater.MemberID + "\" target=\"_blank\" title=\"" + topic.Creater.NickName + "\">" + topic.Creater.NickName + "</a> &nbsp;</span><span class=\"mt5 fl\"><a href=\"/Member/Ta/" + topic.Creater.MemberID + "\" target=\"_blank\" title=\"" + topic.Creater.NickName + "\" name=\"memberInfoLabel\" data_me_id=\"<%=Model.UserID %>\" data_member_id=\"" + topic.Creater.MemberID + "\"><img src=\"" + photofunctions.geticonphotoname(topic.Creater.ICONPath) + "\" width=\"22\" height=\"22\" border=\"0\" style=\"margin-top:4px;\" /></a> &nbsp;</span><span class=\"mt8 manadiv fl\" style=\"float:left; margin:0px; padding:0px;\"><span class=\"like3\" style=\"float:left;\">&nbsp;</span><span class=\"like2\" id=\"like" + topic.ID + "\" style=\"float:left;\" onclick=\"WenWenControl.likeclick('" + topic.ID + "')\">" + topic.Likecount + "&nbsp;♥</span><span class=\"like1\" style=\"float:left;\">&nbsp;</span></span><span class=\"fl\"></span><span class=\"fl cgray ml10\">" + (topic.AnswerCount == 0 ? "" : ("" + topic.AnswerCount + "回应")) + "</span></div></div>";
             
         return str;   
    }

    var morePageSize = 10;
    function showMoreTopics(pageNo) {
        $("div.loadMoreTopics").hide();
        var leftTopicHtml = "", rightTopicHtml = "";
        
        $.ajaxSetup({ async: false });
        WenWenLinkProvider.getTypesetFeaturedInterestTopic(publishedTopicSex, morePageSize, pageNo, function (data) {
            
            var objs = $.parseJSON(data);
            var leftTopics = objs.leftTopicList;
            var rightTopics = objs.rightTopicList;

            $.each(leftTopics, function (i) {
                if(leftTopics[i] == null) return true;
                if (leftTopics[i].ContentImage == "" || leftTopics[i].ContentImage == null) 
                {
                    leftTopicHtml += interestTopicNoImgPanel(leftTopics[i]);
                }
                else
                {
                    leftTopicHtml += interestTopicHaveImgPanel(leftTopics[i]);
                } 
            });

            $.each(rightTopics, function (i) {
                if(rightTopics[i] == null) return true;
                if (rightTopics[i].ContentImage == "" || rightTopics[i].ContentImage == null) 
                {
                    rightTopicHtml += interestTopicNoImgPanel(rightTopics[i]);
                }
                else
                {
                    rightTopicHtml += interestTopicHaveImgPanel(rightTopics[i]);
                } 
            });
        });
        $.ajaxSetup({ async: true });

        $("div.topicLeft").html($("div.topicLeft").html() + leftTopicHtml);
        $("div.topicRight").html($("div.topicRight").html() + rightTopicHtml);

        if((((pageNo + 1) * 10) <= (pageCount >= 30 ? 30 : pageCount)) && bigPageNo < 2) {
            $("div.loadMoreTopics").show();
            $("div.loadMoreTopics").html("<div onclick=\"showMoreTopics(" + (pageNo + 1) + ")\">加载更多...</div>");
        }
        else {
            $("#featuredTopicPaging").removeClass("hidden");
            $("#featuredTopicPaging").show();
            $("#featuredTopicPaging").html(profileQueryStrPaging(bigPageNo, pageCounts, pageUrl).toString());
        }
    }
</script>
</asp:Content>
