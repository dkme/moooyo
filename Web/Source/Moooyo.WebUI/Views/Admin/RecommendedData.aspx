<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    推荐数据
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Admin_TopNav">
        <% Html.RenderPartial("~/Views/Admin/AdminTopNav.ascx");%>
    </div>
    <div class="Admin_Dic_LeftNav">
        <ul class="leftlist magT10">
            <li><a href="/Admin/RecommendedData/FeaturedInterestTopic">精选兴趣话题</a></li>
            <li><a href="/Admin/RecommendedData/FeaturedContentTopic">精选内容</a></li>
            <li><a href="/Admin/RecommendedData/FeaturedContentImage">内容图片置顶部</a></li>
            <li><a href="/Admin/RecommendedData/ContentImageToHome">内容图片置首页</a></li>
        </ul>
    </div>
    <div class="content magT10">
        <div>
            <% if (ViewData["t"].ToString() == "FeaturedInterestTopic")
               { %>
            创建者(如:10001)：<input type="text" id="txtCreator" />&nbsp;<input type="button" value="搜索" onclick="viewInterestTopic('briefMemberIdTopic')" />&nbsp;|&nbsp;<input type="button" value="取消所有推荐" onclick="cancelLikeAllInterestTopic()" />&nbsp;<input type="button" value="按已推荐" onclick="viewInterestTopic('adminLikeTopic')" />&nbsp;<input type="button" value="按未推荐" onclick="viewInterestTopic('notAdminLikeTopic')" />&nbsp;<input type="button" value="查看所有" onclick="viewInterestTopic('allTopic')" />
            <% } %>
            <%else if (ViewData["t"].ToString() == "FeaturedContentTopic")
               { %>
               <input type="button" value="图片" onclick="viewContentTopic('image');"/>
               <input type="button" value="说说" onclick="viewContentTopic('shuoshuo');"/>
               <input type="button" value="计划" onclick="viewContentTopic('iwant');"/>
               <input type="button" value="心情" onclick="viewContentTopic('mood');"/>
               <input type="button" value="访谈" onclick="viewContentTopic('interview');"/>
               <input type="button" value="号召" onclick="viewContentTopic('callfor');"/>
            <% }
               else if (ViewData["t"].ToString() == "FeaturedContentImage")
               { %>
                <input type="button" value="所有推送图片" onclick="bindContentPushImagesList('allContentPushImages')" />&nbsp;<input type="button" value="按显示的推送图片" onclick="bindContentPushImagesList('shownContentPushImages')" />&nbsp;<input type="button" value="按未显示的推送图片" onclick="bindContentPushImagesList('hiddenContentPushImages')" />
            <% }
               else if (ViewData["t"].ToString() == "ContentImageToHome")
               { %>
               <form name="frmContentImageToHome" id="frmContentImageToHome" method="post" action="/SystemFunc/SaveFeaturedContentImage" enctype="multipart/form-data">
               <input type="hidden" id="featuredToHomeImage" name="featuredToHomeImage" value="" />
               内容：
               <textarea id="imageToHomeContent" name="imageToHomeContent" style="width:260px;"></textarea>
               图片：<input id="imageToHomeImage" name="imageToHomeImage" type="file" /><input value="保存图片" type="button" onclick="saveFeaturedContentImage()" /> <input value="添加" type="button" onclick="addContentImageToHome()" />
               </form>
               <br />
               查看
               <select id="viewContentImageToHome" name="viewContentImageToHome" onchange="viewContentImageToHomes($(this).val())">
               <option value="2" selected="selected">查看所有</option>
                <option value="1">按已使用</option>
                <option value="0">按未使用</option>
               </select>
            <% }%>
            <br />
            <br />
            <div id="listcontiner">
            </div>
            <div style="width:850px; padding-bottom:30px; text-align:center;">
            <center>
            <div id="pager" class="verifyPager"></div>
            </center>
            </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/scripts/jquery.form.js" type="text/javascript"></script>
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/main_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/admin.js" type="text/javascript"></script>
    <script type="text/javascript">
        var pagesize = 10, briefMemberId = 0;
        var pageno = 1;
        var mypagecount = 1;
        var uploadpath = '<%=ViewData["uploadpath"] %>';
        var type = '<%=ViewData["t"].ToString() %>';
        var usedFlag = 2;

        $(document).ready(function () {
            switch (type) {
                case "FeaturedInterestTopic":
                    viewInterestTopic("allTopic");
                    break;
                case "FeaturedContentTopic":
                    viewContentTopic("image");
                    break;
                case "FeaturedContentImage":
                    bindContentPushImagesList("allContentPushImages");
                    break;
                case "ContentImageToHome":
                    viewContentImageToHomes(usedFlag);
                    break;
                default: 
                    break;
            }

            var pushImagePath = '<%=ViewData["pushImagePath"] %>';
            var contentId = '<%=ViewData["contentId"] %>';
            if (pushImagePath != null && pushImagePath != "") {
                AdminRecommendedDataProvider.addTopPushImage(contentId, pushImagePath, function (data) {
                    var result = $.parseJSON(data);
                    if (result.ok) {
                        $.jBox.tip("成功添加该张内容推送置顶图片！", 'success');
                        count("allContentPushImages");
                        bindContentPushImagesList("allContentPushImages");
                    }
                    else {
                        $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error');
                    }
                });
            }
        });

        function viewInterestTopic(bindCondition) {
            switch (bindCondition) {
                case "allTopic": 
                    count("allTopic"); 
                    bindlist("allTopic"); 
                    break;
                case "adminLikeTopic": 
                    count("adminLikeTopic"); 
                    bindlist("adminLikeTopic"); 
                    break;
                case "notAdminLikeTopic": 
                    count("notAdminLikeTopic"); 
                    bindlist("notAdminLikeTopic"); 
                    break;
                    case "briefMemberIdTopic":
                        briefMemberId = $.trim(document.getElementById("txtCreator").value);
                        if (briefMemberId != "") {
                            count("briefMemberIdTopic");
                            bindlist("briefMemberIdTopic");
                        }
                        else 
                            $.jBox.tip("输入些东西吧", 'info');
                        break;
                default: 
                    break;
            }
        }

        function viewContentTopic(bindCondition) {
            switch (bindCondition) {
                case "image": pageno = 1;
                    count("image");
                    bindcontentlist("0"); 
                    break;
                case "shuoshuo": pageno = 1;
                    count("shuoshuo");
                    bindcontentlist("1"); 
                    break;
                case "iwant": pageno = 1;
                    count("iwant");
                    bindcontentlist("2"); 
                    break;
                case "mood": pageno = 1;
                    count("mood");
                    bindcontentlist("3"); 
                    break;
                case "interview": pageno = 1;
                    count("interview");
                    bindcontentlist("4"); 
                    break;
                case "callfor": pageno = 1;
                    count("callfor");
                    bindcontentlist("5"); 
                    break;
                default: break;
            }
        }

        function viewContentPushImage(bindCondition) {
            switch (bindCondition) {
                case "allContentPushImages":
                    count("allContentPushImages");
                    bindContentPushImagesList("allContentPushImages");
                    break;
                case "0":
                    count("shownContentPushImages");
                    bindContentPushImagesList("shownContentPushImages");
                    break;
                case "1":
                    count("hiddenContentPushImages");
                    bindContentPushImagesList("hiddenContentPushImages");
                    break;
                default:
                    break;
            }
        }

        function viewContentImageToHomes(usedFlag) {
            usedFlag = usedFlag;
            count("ContentImageToHome");
            bindContentImageToHomes(usedFlag);
        }

        function bindContentImageToHomes(usedFlag) {
            AdminRecommendedDataProvider.getFeaturedContents(usedFlag, pagesize, pageno, function (data) {
                var objs = $.parseJSON(data);
                var str = "";
                var generatedMember = "";
                var usedMember = "";
                var creator = "";
                var imagestr = "";

                if (objs != null) {
                    $.each(objs, function (i) {
                        creator = objs[i].Creator != null && objs[i].Creator != undefined ? (objs[i].Creator.UniqueNumber != null ? objs[i].Creator.UniqueNumber.ConvertedID : objs[i].Creator.MemberID) : "";

                        str += "<table width=\"850\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" class=\"table01\" bgcolor=\"#cccccc\"><tr>";
                        str += "<th width=\"55\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建者：</th><td width=\"120\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + creator + "</td>";
                        str += "<th width=\"65\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建时间：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + paserJsonDate(objs[i].CreatedTime).format('yyyy-mm-dd HH:MM') + "</td>";
                        str += "<th width=\"65\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">显示次数：</th><td width=\"50\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + objs[i].ShowedCount + "</td>";
                        str += "<th width=\"65\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">是否启用：</th><td width=\"50\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\" id=\"featuredContentUseStatus" + objs[i].ID + "\">" + (objs[i].UsedFlag == 0 ? "未使用" : "已使用") + "</td>";

                        str += "<td width=\"120\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">";
                        
                        str += "<input type=\"button\"" + (objs[i].UsedFlag == 0 ? " class=\"hidden\"" : "") + " id=\"btnCancelUseFeaturedContent" + objs[i].ID + "\" value=\"取消使用\" onclick=\"btnUseFeaturedContent(0, '" + objs[i].ID + "')\" />";

                        str += "<input type=\"button\"" + (objs[i].UsedFlag == 1 ? " class=\"hidden\"" : "") + " value=\"使用\" id=\"btnUseFeaturedContent" + objs[i].ID + "\" onclick=\"btnUseFeaturedContent(1, '" + objs[i].ID + "')\" />";

                        str += "&nbsp;<input type=\"button\" value=\"删除\" id=\"btnDeleteFeaturedContent" + objs[i].ID + "\" onclick=\"deleteFeaturedContent('" + objs[i].ID + "')\" />";

                        str += "</td>";
                        str += "</tr><tr>";

                        imagestr = "<a href=\"" + photofunctions.getnormalphotoname(objs[i].Image) + "\" target=\"_blank\" title=\"查看大图\"><img src=\"" + photofunctions.getnormalphotoname(objs[i].Image) + "\" width=\"10\" height=\"10\"  onload=\"ImageZoomToJquery($(this), 830)\" /></a>";

                        str += "<td valign=\"top\" align=\"left\" bgcolor=\"#ffffff\" colspan=\"9\">" + objs[i].Content + "<br />" + imagestr + "";
                        str += "</td></tr></table>";

                    });
                }
                else
                    str += "<li>没有数据！</li>";

                $("#listcontiner").html(str);
            });
        }

        function saveFeaturedContentImage() {
            file = $("#imageToHomeImage").val();
            var pos = file.lastIndexOf(".");
            var lastName = file.substring(pos, file.length);
            if ($("#imageToHomeImage").val() == "") {
                alert("必须先上传图片");
                return;
            }
            if ((lastName.toLowerCase() != ".jpg") && (lastName.toLowerCase() != ".gif") && (lastName.toLowerCase() != ".png") && (lastName.toLowerCase() != ".bmp")) {
                $("#imageToHomeContent").focus();
                alert("文件必须为图片类型");
            }
            else {
                $('#frmContentImageToHome').ajaxSubmit(function (data) {
                    if ((data != null) && (data != "")) {
                        $("#featuredToHomeImage").val(data);
                        alert("首页推送图片保存成功");
                    }
                    else {
                        $("#imageToHomeContent").focus();
                        alert("首页推送图片保存失败");
                    }
                });
            }
        }

        function addContentImageToHome() {
            var content = $("#imageToHomeContent").val();
            if (trim(content) == "") {
                $("#imageToHomeContent").focus();
                alert("内容不能为空");
                return;
            }
            var image = $("#featuredToHomeImage").val();
            if (trim(image) == "") {
                $("#imageToHomeImage").focus();
                alert("请先上传图片");
                return;
            }
            AdminRecommendedDataProvider.addFeaturedContent(image, content, 0, function (data) {
                var result = $.parseJSON(data);
                if (result.ok) {
                    $("#imageToHomeImage").val("");
                    $("#imageToHomeContent").val("");
                    viewContentImageToHomes(usedFlag);
                    alert("推送至首页内容新增成功");
                }
                else {
                    alert("推送至首页内容新增失败"); 
                }
            });
        }

        function deleteFeaturedContent(featContentId) {
            AdminRecommendedDataProvider.DeleteFeaturedContent(featContentId, function (data) {
                var result = $.parseJSON(data);
                if (result.ok) {
                    viewContentImageToHomes(usedFlag);
                    alert("推送至首页内容删除成功");
                }
                else {
                    alert("推送至首页内容删除失败");
                }
            });
        }

        function btnUseFeaturedContent(use, featCentId) {
            var useStr = "";
            var useStr2 = "";
            useStr = (use == 0 ? "取消使用" : "使用");
            useStr2 = (use == 0 ? "未使用" : "已使用");
            AdminRecommendedDataProvider.ajaxSetFeaturedContentHasUsed(featCentId, use, function (data) {
                var result = $.parseJSON(data);
                if (result.ok) {
                    alert("成功" + useStr + "该精选内容！");
                    switch(use) {
                        case 0: $("#btnCancelUseFeaturedContent" + featCentId).hide();
                            $("#btnUseFeaturedContent" + featCentId).show();
                            $("#featuredContentUseStatus" + featCentId).html(useStr2);
                            break;
                        case 1: $("#btnCancelUseFeaturedContent" + featCentId).show();
                            $("#btnUseFeaturedContent" + featCentId).hide();
                            $("#featuredContentUseStatus" + featCentId).html(useStr2);
                            break;
                        default: 
                            break;
                    }
                }
            });
        }

        function bindcontentlist(type) {
            var objs = null, str = "";

            $.ajaxSetup({ async: false });
            ContentProvider.getContentToType(type, pagesize, pageno, function (data) {
                objs = $.parseJSON(data).ContentList; 
            });
            $.ajaxSetup({ async: true });

            if (objs != null) {
                $.each(objs, function (i) {
                    var topicContent = objs[i].Content;
                    var memberNumbered = "";
                    if (objs[i].Creater.UniqueNumber != null) {
                        memberNumbered = objs[i].Creater.UniqueNumber.ConvertedID;
                    }

                    str += "<table width=\"850\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" class=\"table01\" bgcolor=\"#cccccc\"><tr>";
                    str += "<th width=\"65\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建者：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + objs[i].Creater.NickName + "</td>";
                    str += "<th width=\"65\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建时间：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + paserJsonDate(objs[i].CreatedTime).format('yyyy-mm-dd HH:MM') + "</td>";
                    str += "<th width=\"75\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建者城市：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + objs[i].Creater.City + "</td>";
                    str += "<th width=\"75\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建者性别：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + (objs[i].Creater.Sex == 1 ? "男" : "女") + "</td>";
                    str += "</tr><tr>";
                    str += "<th width=\"65\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">用户喜欢：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + objs[i].LikeCount + "</td>";
                    str += "<th width=\"75\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">回复数：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + objs[i].AnswerCount + "</td>";
                    str += "<th width=\"75\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建者编号：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + memberNumbered + "</td>";
                    str += "<td width=\"175\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\" colspan=\"2\">";

                    var ifLikedTopic = false;
                    $.ajaxSetup({ async: false });
                    AdminRecommendedDataProvider.ifAdminLikedTopicContent("", objs[i].ID, function (data) {
                        ifLikedTopic = $.parseJSON(data);
                    });
                    $.ajaxSetup({ async: true });

                    str += "<input type=\"button\"" + (ifLikedTopic ? "" : " class=\"hidden\"") + " id=\"btnCancelLikeTopic" + objs[i].ID + "\" value=\"取消推荐\" onclick=\"likeTopicOperate(true, '" + objs[i].ID + "',true)\" />";

                    str += "<input type=\"button\"" + (ifLikedTopic ? " class=\"hidden\"" : "") + " value=\"推荐\" id=\"btnLikeTopic" + objs[i].ID + "\" onclick=\"likeTopicOperate(false, '" + objs[i].ID + "',true)\" />";

                    if (type == "0") {
                        $.ajaxSetup({ async: false });
                        AdminRecommendedDataProvider.ifImagePush(objs[i].ID, function (data) {
                            if ($.parseJSON(data).toString() == "false" || $.parseJSON(data).toString() == "False")
                                str += "<input type=\"button\" value=\"顶部推送\" onclick=\"imagetoppush($(this), '" + objs[i].ID + "','" + type + "')\" />";
                        });
                        $.ajaxSetup({ async: true });
                    }

                    str += "</td>";
                    str += "</tr><tr>";
                    var imagestr = "";
                    if (objs[i].ImageList != null) {
                        for (var j = 0; j < objs[i].ImageList.length; j++) {
                            imagestr += "<img src=\"" + photofunctions.getnormalphotoname(objs[i].ImageList[j].ImageUrl) + "\"/><br />";
                            imagestr += "<input type=\"button\" value=\"推送该张到顶部 ∧\" onclick=\"cropAddPushImage('/photo/Get/" + objs[i].ImageList[j].ImageUrl + "', " + objs[i].ContentType + ", '" + objs[i].ID + "')\" /><br /><br />";
                        }
                    }
                    str += "<td valign=\"top\" align=\"left\" bgcolor=\"#ffffff\" colspan=\"8\">" + topicContent + "<br />" + imagestr + "</td>";
                    str += "</tr></table>";
                });
            }
            else str = "<center>没有数据！</center>";

            $("#listcontiner").html(str);
        }

        function bindlist(bindCondition) {
            var objs = null, str = "";
            if (pageno < 1) pageno = 1;
            $.ajaxSetup({ async: false });
            switch (bindCondition) {
                case "allTopic":
                    WenWenLinkProvider.getInterestIdTopic("", pagesize, pageno, function (data) { objs = $.parseJSON(data); });
                    break;
                case "adminLikeTopic":
                    AdminRecommendedDataProvider.getAdminLikeOrNotTopics("", pagesize, pageno, true, function (data) { objs = $.parseJSON(data); });
                    break;
                case "notAdminLikeTopic":
                    AdminRecommendedDataProvider.getAdminLikeOrNotTopics("", pagesize, pageno, false, function (data) { objs = $.parseJSON(data); });
                case "briefMemberIdTopic":
                    WenWenLinkProvider.getBriefMemberIdTopics(briefMemberId, pagesize, pageno, function (data) { objs = $.parseJSON(data); })
                    break;
                default: break;
            }
            $.ajaxSetup({ async: true });

            if (objs != null) {
                $.each(objs, function (i) {
                    var topicContent = decodeURIComponent(getExpression(objs[i].Content));
                    var topicContentImage = getFixedWidthImageToTopic(objs[i].ContentImage == null ? "" : objs[i].ContentImage, 5, 350);

                    str += "<table width=\"850\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" class=\"table01\" bgcolor=\"#cccccc\"><tr>";
                    str += "<th width=\"65\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建者：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + getuserminid(objs[i].Creater.MemberID) + "</td>";
                    str += "<th width=\"65\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建时间：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + paserJsonDate(objs[i].CreatedTime).format('yyyy-mm-dd HH:MM') + "</td>";
                    str += "<th width=\"75\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建者城市：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + objs[i].Creater.City + "</td>";
                    str += "<th width=\"75\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建者性别：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + (objs[i].Creater.Sex == 1 ? "男" : "女") + "</td>";
                    str += "</tr><tr>";
                    str += "<th width=\"65\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">用户喜欢：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + objs[i].Likecount + "</td>";
                    str += "<th width=\"65\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">管理喜欢：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + objs[i].AdminLikeCount + "</td>";
                    str += "<th width=\"75\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">回复数：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + objs[i].AnswerCount + "</td>";
                    str += "<td width=\"175\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\" colspan=\"2\">";

                    var ifLikedTopic = false;
                    $.ajaxSetup({ async: false });
                    AdminRecommendedDataProvider.ifAdminLikedTopic("", objs[i].ID, function (data) {
                        ifLikedTopic = $.parseJSON(data);
                    });
                    $.ajaxSetup({ async: true });

                    str += "<input type=\"button\"" + (ifLikedTopic ? "" : " class=\"hidden\"") + " id=\"btnCancelLikeTopic" + objs[i].ID + "\" value=\"取消推荐\" onclick=\"likeTopicOperate(true, '" + objs[i].ID + "')\" />";

                    str += "<input type=\"button\"" + (ifLikedTopic ? " class=\"hidden\"" : "") + " value=\"推荐\" id=\"btnLikeTopic" + objs[i].ID + "\" onclick=\"likeTopicOperate(false, '" + objs[i].ID + "')\" />";

                    str += "</td>";
                    str += "</tr><tr>";
                    str += "<td valign=\"top\" align=\"left\" bgcolor=\"#ffffff\" colspan=\"8\">" + topicContent + "<br />" + topicContentImage + "</td>";
                    str += "</tr></table>";
                });
            }
            else str = "<center>没有数据！</center>";
            
            $("#listcontiner").html(str);
        }

        function bindContentPushImagesList(bindCondition) {
            var objs = null, str = "";
            $.ajaxSetup({ async: false });
            if (isNaN(bindCondition)) { //非数值
                AdminRecommendedDataProvider.getAllTopImagesPush(
                    pagesize,
                    pageno,
                    function (data) {
                        objs = $.parseJSON(data);
                    }
                );
            }
            else {
                AdminRecommendedDataProvider.getTopImagesPush(
                    bindCondition,
                    pagesize,
                    pageno,
                    function (data) {
                        objs = $.parseJSON(data);
                    }
                );
            }
            $.ajaxSetup({ async: true });

            if (objs != null) {
                $.each(objs, function (i) {

                    var topicContent = objs[i].Content;
                    str += "<table width=\"850\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" class=\"table01\" bgcolor=\"#cccccc\"><tr>";
                    str += "<th width=\"65\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建者：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + (objs[i].Creater.UniqueNumber == null ? objs[i].Creater.MemberID : objs[i].Creater.UniqueNumber.ConvertedID) + "</td>";
                    str += "<th width=\"65\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建时间：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + paserJsonDate(objs[i].CreatedTime).format('yyyy-mm-dd HH:MM') + "</td>";
                    str += "<th width=\"75\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建者城市：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + objs[i].Creater.City + "</td>";
                    str += "<th width=\"75\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">创建者性别：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + (objs[i].Creater.Sex == 1 ? "男" : "女") + "</td>";
                    str += "</tr><tr>";
                    str += "<th width=\"65\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">显示次数：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + objs[i].ImagePushCount.ShowCount + "</td>";
                    str += "<th width=\"75\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">点击次数：</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\">" + objs[i].ImagePushCount.ClickCount + "</td>";
                    str += "<th width=\"75\" valign=\"middle\" align=\"right\" bgcolor=\"#eeeeee\">";

                    str += "是否显示：";

                    //                    var ifLikedTopic = false;
                    //                    $.ajaxSetup({ async: false });
                    //                    AdminRecommendedDataProvider.ifAdminLikedTopicContent("", objs[i].ID, function (data) {
                    //                        ifLikedTopic = $.parseJSON(data);
                    //                    });
                    //                    $.ajaxSetup({ async: true });

                    //                    str += "<input type=\"button\"" + (ifLikedTopic ? "" : " class=\"hidden\"") + " id=\"btnCancelLikeTopic" + objs[i].ID + "\" value=\"取消推荐\" onclick=\"likeTopicOperate(true, '" + objs[i].ID + "',true)\" />";

                    //                    str += "<input type=\"button\"" + (ifLikedTopic ? " class=\"hidden\"" : "") + " value=\"推荐\" id=\"btnLikeTopic" + objs[i].ID + "\" onclick=\"likeTopicOperate(false, '" + objs[i].ID + "',true)\" />";

                    //                    if (type == "0") {
                    //                        $.ajaxSetup({ async: false });
                    //                        AdminRecommendedDataProvider.ifImagePush(objs[i].ID, function (data) {
                    //                            if ($.parseJSON(data).toString() == "false" || $.parseJSON(data).toString() == "False")
                    //                                str += "<input type=\"button\" value=\"顶部推送\" onclick=\"imagetoppush($(this), '" + objs[i].ID + "','" + type + "')\" />";
                    //                        });
                    //                        $.ajaxSetup({ async: true });
                    //                    }

                    str += "</th><td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\" id=\"pushImageShowHide" + objs[i].ID + "\">" + (objs[i].DeleteFlag == 0 ? "显示" : "未显示") + "</td>";
                    str += "<td width=\"100\" valign=\"middle\" align=\"left\" bgcolor=\"#ffffff\" colspan=\"2\">";
                    if (objs[i].DeleteFlag == 0) {
                        str += "<input type=\"button\" id=\"btnPushImageShowHide" + objs[i].ID + "\" value=\"取消显示\" onclick=\"showHidePushImage('" + objs[i].ID + "', 1, '取消显示')\" />";
                    }
                    else {
                        str += "<input type=\"button\" id=\"btnPushImageShowHide" + objs[i].ID + "\" value=\"显示\" onclick=\"showHidePushImage('" + objs[i].ID + "', 0, '显示')\" />";
                    }

                    str += "&nbsp;<input type=\"button\" id=\"btnDeletePushImage" + objs[i].ID + "\" value=\"删除\" onclick=\"btnDeletePushImage('" + objs[i].ID + "', '" + bindCondition + "')\" /></td>";
                    str += "</tr><tr>";

                    var imagestr = "";
                    if (objs[i].ImageList != null) {
                        for (var j = 0; j < objs[i].ImageList.length; j++) {
                            imagestr += "<img src=\"" + photofunctions.getnormalphotoname(objs[i].ImageList[j].ImageUrl) + "\"/>";
                        }
                    }

                    str += "<td valign=\"top\" align=\"left\" bgcolor=\"#ffffff\" colspan=\"8\">" + objs[i].ImageContent + "<br />" + imagestr + "";
                    str += "</td></tr></table>";

                });
            }
            else 
                str = "<center>没有数据！</center>";

            $("#listcontiner").html(str);
        }

        function imagetoppush(obj, contentid) {
            AdminRecommendedDataProvider.addTopImagePush(contentid, function (data) {
                if (data.toString() == "true" || data.toString == "True") {
                    $.jBox.tip("顶部图片推送成功！", 'info');
                    obj.remove();
                }
                else {
                    $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error');
                }
            });
        }

        function showHidePushImage(pushImageId, showStatus, prompt) {
            AdminRecommendedDataProvider.setPushImageShowStatus(pushImageId, showStatus, function (data) {
                var result = $.parseJSON(data);
                if (result.ok) {
                    $.jBox.tip("成功" + prompt + "推送图片！", 'info');
                    switch (showStatus) {
                        case 0:
                            $("#btnPushImageShowHide" + pushImageId).attr("value", "取消显示");
                            $("#btnPushImageShowHide" + pushImageId).unbind("click").click(function () {
                                showHidePushImage(pushImageId, 1, "取消显示");
                            });
                            $("#pushImageShowHide" + pushImageId).html("显示");
                            break;
                        case 1:
                            $("#btnPushImageShowHide" + pushImageId).attr("value", "显示");
                            $("#btnPushImageShowHide" + pushImageId).unbind("click").click(function () {
                                showHidePushImage(pushImageId, 0, "显示");
                            });
                            $("#pushImageShowHide" + pushImageId).html("未显示");
                            break;
                        default:
                            break;
                    }
                }
                else {
                    $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error');
                }
            });
        }

        function btnDeletePushImage(pushImageId, bindCondition) {
            AdminRecommendedDataProvider.deleteTopImagePush(pushImageId, function (data) {
                var result = $.parseJSON(data);
                if (result.ok) {
                    $.jBox.tip("成功删除该张内容推送置顶图片！", 'success');
                    bindContentPushImagesList(bindCondition);
                }
                else {
                    $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error');
                }
            });
        }

        function cropAddPushImage(imgPath, contentType, contentId) {
            var photoType = 201;
            var newImgPath = "";
            var state = "";
            switch (contentType) {
                case 0:
                    photoType = 201;
                    break;
                case 1:
                    photoType = 202;
                    break;
                case 5:
                    photoType = 203;
                    break;
                default: break;
            }
            $.ajaxSetup({ async: false });
            commonProvider.savePathPhotoGetUploadedPath(imgPath, "", function (data) {
                if (data != null && data != "") {
                    var rData = $.parseJSON(data);
                    newImgPath = rData.filePath;
                    state = rData.state;
                    if (state == "failure") {
                        $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error');
                    }
                }
                else {
                    $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error');
                }
            });
            $.ajaxSetup({ async: true });
            if (state == "failure") {
                return;
            }
            actionprovider.openCustomBigPicture('/Admin/RecommendedData/FeaturedContentImage', newImgPath, photoType, "", 3, 800, 250, contentId);
        }

        function likeTopicOperate(ifLikedTopic, topicId, ifcontent) {
            if (!ifLikedTopic) {
                if (ifcontent == null) {
                    AdminRecommendedDataProvider.addAdminLikeTopic("", topicId, function (data) {
                        var adminLikeTopicCount = $.parseJSON(data);
                        if (!isNaN(adminLikeTopicCount)) {
                            $("#btnLikeTopic" + topicId).hide();
                            $("#btnCancelLikeTopic" + topicId).show();
                            $.jBox.tip("成功推荐该兴趣话题", 'success');
                        }
                        else if (adminLikeTopicCount == false) { $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error'); }
                    });
                }
                else {
                    AdminRecommendedDataProvider.addAdminLikeTopicContent("", topicId, function (data) {
                        var adminLikeTopicCount = $.parseJSON(data);
                        if (!isNaN(adminLikeTopicCount)) {
                            $("#btnLikeTopic" + topicId).hide();
                            $("#btnCancelLikeTopic" + topicId).show();
                            $.jBox.tip("成功推荐该兴趣话题", 'success');
                        }
                        else if (adminLikeTopicCount == false) { $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error'); }
                    });
                }
            }
            else {
                if (ifcontent == null) {
                    AdminRecommendedDataProvider.deleteAdminLikeTopic("", topicId, function (data) {
                        var adminLikeTopicCount = $.parseJSON(data);
                        if (!isNaN(adminLikeTopicCount)) {
                            $("#btnCancelLikeTopic" + topicId).hide();
                            $("#btnLikeTopic" + topicId).show();
                            $.jBox.tip("成功取消推荐该兴趣话题", 'success');
                        }
                        else if (adminLikeTopicCount == false) { $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error'); }
                    });
                }
                else {
                    AdminRecommendedDataProvider.deleteAdminLikeTopicContent("", topicId, function (data) {
                        var adminLikeTopicCount = $.parseJSON(data);
                        if (!isNaN(adminLikeTopicCount)) {
                            $("#btnCancelLikeTopic" + topicId).hide();
                            $("#btnLikeTopic" + topicId).show();
                            $.jBox.tip("成功取消推荐该兴趣话题", 'success');
                        }
                        else if (adminLikeTopicCount == false) { $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error'); }
                    });
                }
            }
        }

        function cancelLikeAllInterestTopic() {
            if (confirm("确定要取消推荐所有的兴趣话题？")) {
                AdminRecommendedDataProvider.updateAllTopicsAdminLikeCount("", function (data) {
                    var result = $.parseJSON(data);
                    if (result.ok) {
                        $.jBox.tip("成功取消所有推荐的兴趣话题", 'success');
                    }
                    else { $.jBox.tip(result.err, 'error'); }
                });
            }
        }

        function allcheck() {
            $("#listcontiner [name='mycheck']").prop("checked", function (i, val) { return !val });
        }

        function count(bindCondition) {
            var _pagecount = 0, allcount = 0;

            $.ajaxSetup({ async: false });
            switch (bindCondition) {
                case "allTopic":
                    WenWenLinkProvider.getTopicCount("", function (data) { allcount = $.parseJSON(data); });
                    break;
                case "adminLikeTopic":
                    AdminRecommendedDataProvider.getAdminLikeOrNotTopicsCount("", true, function (data) { allcount = $.parseJSON(data); });
                    break;
                case "notAdminLikeTopic":
                    AdminRecommendedDataProvider.getAdminLikeOrNotTopicsCount("", false, function (data) { allcount = $.parseJSON(data); });
                    break;
                case "briefMemberIdTopic":
                    WenWenLinkProvider.getBriefMemberIdTopicsCount(briefMemberId, function (data) { allcount = $.parseJSON(data); });
                    break;
                case "image":
                    ContentProvider.getContentToType("0", pagesize, 1, function (data) { allcount = $.parseJSON(data).contentCount; });
                    break;
                case "shuoshuo":
                    ContentProvider.getContentToType("1", pagesize, 1, function (data) { allcount = $.parseJSON(data).contentCount; });
                    break;
                case "iwant":
                    ContentProvider.getContentToType("2", pagesize, 1, function (data) { allcount = $.parseJSON(data).contentCount; });
                    break;
                case "mood":
                    ContentProvider.getContentToType("3", pagesize, 1, function (data) { allcount = $.parseJSON(data).contentCount; });
                    break;
                case "interview":
                    ContentProvider.getContentToType("4", pagesize, 1, function (data) { allcount = $.parseJSON(data).contentCount; });
                    break;
                case "callfor":
                    ContentProvider.getContentToType("5", pagesize, 1, function (data) { allcount = $.parseJSON(data).contentCount; });
                    break;

                case "allContentPushImages":
                    AdminRecommendedDataProvider.getAllPushImageCount(function (data) {
                        allcount = data; 
                    });
                    break;
                case "shownContentPushImages":
                    AdminRecommendedDataProvider.getPushImageCount(0, function (data) {
                        allcount = data;
                    });
                    break;
                case "hiddenContentPushImages":
                    AdminRecommendedDataProvider.getPushImageCount(1, function (data) {
                        allcount = data;
                    });
                    break;
                case "ContentImageToHome": 
                    AdminRecommendedDataProvider.getFeaturedContentsCount(usedFlag, function (data) {
                        allcount = data;
                    });
                    break;
                default: 
                    break;
            }
            $.ajaxSetup({ async: true });

            _pagecount = parseInt((parseInt(allcount) + parseInt(pagesize) - 1) / parseInt(pagesize));
            mypagecount = _pagecount;
            if (pageno > mypagecount) {
                pageno = mypagecount;
            }
            setPager(bindCondition);
        }

        allTopicPageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: allTopicPageClick });
            bindlist("allTopic");
        }

        adminLikeTopicPageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: adminLikeTopicPageClick });
            bindlist("adminLikeTopic");
        }

        notAdminLikeTopicPageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: notAdminLikeTopicPageClick });
            bindlist("notAdminLikeTopic");
        }

        briefMemberIdTopicPageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: briefMemberIdTopicPageClick });
            bindlist("briefMemberIdTopic");
        }

        imageTopicPageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: imageTopicPageClick });
            bindcontentlist("0");
        }

        shuoshuoTopicPageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: shuoshuoTopicPageClick });
            bindcontentlist("1");
        }

        iwantTopicPageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: iwantTopicPageClick });
            bindcontentlist("2");
        }

        moodTopicPageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: moodTopicPageClick });
            bindcontentlist("3");
        }

        interviewTopicPageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: interviewTopicPageClick });
            bindcontentlist("4");
        }

        callforTopicPageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: callforTopicPageClick });
            bindcontentlist("5");
        }

        allContentPushImagePageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: allContentPushImagePageClick });
            bindcontentlist("5");
        }

        shownContentPushImagePageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: shownContentPushImagePageClick });
            bindcontentlist("5");
        }

        hiddenContentPushImagePageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: hiddenContentPushImagePageClick });
            bindcontentlist("5");
        }

        contentImageToHomeClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) { pageno = mypagecount; }
            else { pageno = pageclickednumber; }
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: contentImageToHomeClick });
            bindContentImageToHomes(usedFlag);
        }

        function setPager(bindCondition) {
            switch (bindCondition) {
                case "allTopic":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: allTopicPageClick });
                    break;
                case "adminLikeTopic":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: adminLikeTopicPageClick });
                    break;
                case "notAdminLikeTopic":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: notAdminLikeTopicPageClick });
                    break;
                case "briefMemberIdTopic":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: briefMemberIdTopicPageClick });
                    break;

                case "image":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: imageTopicPageClick });
                    break;
                case "shuoshuo":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: shuoshuoTopicPageClick });
                    break;
                case "iwant":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: iwantTopicPageClick });
                    break;
                case "mood":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: moodTopicPageClick });
                    break;
                case "interview":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: interviewTopicPageClick });
                    break;
                case "callfor":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: callforTopicPageClick });
                    break;

                case "allContentPushImages":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: allContentPushImagePageClick });
                    break;
                case "shownContentPushImages":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: shownContentPushImagePageClick });
                    break;
                case "hiddenContentPushImages":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: hiddenContentPushImagePageClick });
                    break;

                case "ContentImageToHome":
                    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: contentImageToHomeClick });
                    break;

                default: 
                    break;
            }
        }
    </script>
</asp:Content>
