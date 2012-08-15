<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
[米柚] 兴趣
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="c976 clearfix">
<section class="inter-conbox mt32 fl" style="width:600px;">
    <% Html.RenderAction("Interest", "InterestCenter", new { interestObj = Model.interestObj, memberInterestObj = Model.memberInterestObj }); %>
    <article>
    <form id="topicform" name="topicform" action="/InterestCenter/InsertTopic" enctype="multipart/form-data" method="post">
    <div class="TopicDiv">
        <input type="hidden" id="interestid" name="interestid" value="<%=Model.interestObj.ID %>" />
        <textarea id="pattern" name="pattern" rows="1" cols="50"></textarea>
        <div class="Shadow">&nbsp;</div>
        <div class="TopicBu"><span class="Expression" title="点击插入表情" onclick="showExpression($('#pattern'))">&nbsp;</span><span class="ImageFile" title="点击插入图片" onclick="showtopicimage()">&nbsp;</span><span class="ImageFileCount">已选择0个图片</span><span class="ImageFileClear" onclick="clearImageFile()">×</span><input type="button" class="Button" value="发言" onclick="topicsubmit()"/></div>
    </div>
    <div id="topicimageupdate">
        <div class="maindiv">
            <div class="titlediv">在本地文件中选择:(最多只能上传五张)</div>
        </div>
        <div class="imagefiledivparent">
            <div class="imagefilediv" id="imagefile1"><div id="showfiletext1" class="showfile">&nbsp;</div><div class="filetext"><input type="file" id="file1" name="file1" size="20" onchange="setimagefilecount(1);" style="position:absolute; right:0px; top:0px;"/></div></div>
            <div class="imagefilediv" id="imagefile2"><div id="showfiletext2" class="showfile">&nbsp;</div><div class="filetext"><input type="file" id="file2" name="file2" size="20" onchange="setimagefilecount(2);" style="position:absolute; right:0px; top:0px;"/></div></div>
            <div class="imagefilediv" id="imagefile3"><div id="showfiletext3" class="showfile">&nbsp;</div><div class="filetext"><input type="file" id="file3" name="file3" size="20" onchange="setimagefilecount(3);" style="position:absolute; right:0px; top:0px;"/></div></div>
            <div class="imagefilediv" id="imagefile4"><div id="showfiletext4" class="showfile">&nbsp;</div><div class="filetext"><input type="file" id="file4" name="file4" size="20" onchange="setimagefilecount(4);" style="position:absolute; right:0px; top:0px;"/></div></div>
            <div class="imagefilediv" id="imagefile5"><div id="showfiletext5" class="showfile">&nbsp;</div><div class="filetext"><input type="file" id="file5" name="file5" size="20" onchange="setimagefilecount(5);" style="position:absolute; right:0px; top:0px;"/></div></div>
        </div>
    </div>
    </form>
    <div id="showTopic">
    <%if (Model != null && Model.wenwenlist.Count > 0)
        {
            foreach (var obj in Model.wenwenlist)
            {
                %>
                <div class="Topicitem" name="wenwenMemberInfo">
                <div class="TopicImage">
                    <a href="/Content/TaContent/<%=obj.Creater.MemberID %>" target="_blank"><img src="<%=Comm.getImagePath(obj.Creater.ICONPath, ImageType.Icon) %>" data_me_id="<%=Model.UserID %>" data_member_id="<%=obj.Creater.MemberID %>" name="wenwenMemberInfoArea" width="50" height="50"/></a>
                </div>
                <div class="Topiccontent">
                    <div class="TopicTitle">
                        <a href="/Content/TaContent/<%=obj.Creater.MemberID %>" target="_blank"><%=obj.Creater.NickName%></a><%=(int)obj.Creater.MemberType == 2 ? "<span class=\"vip\" title=\"米柚VIP\">V</span>" : ""%>
                        <a href="/Setting/Authentica" target="_blank">
                        <%if (obj.Creater.MemberPhoto != null)
                        {
                            if (obj.Creater.MemberPhoto.IsRealPhotoIdentification)
                            {%><img src="/upload/photoreal.png" alt="已经通过视频认证" title="已经通过视频认证"/><%}
                            else
                            {%><img src="/upload/photonoreal.png" alt="未通过视频认证" title="未通过视频认证"/><%}
                        }%>
                        </a>
                        <a href="/Setting/AcctUpgrade" target="_blank">
                        <% 
                                        Moooyo.BiZ.Member.MemberType mt = (Moooyo.BiZ.Member.MemberType)obj.Creater.MemberType;
                        if (mt == Moooyo.BiZ.Member.MemberType.Level0)
                        {%><img src="/upload/normal.png" alt="普通会员" title="普通会员"/><%}
                        else if (mt == Moooyo.BiZ.Member.MemberType.Level1)
                        {%><img src="/upload/h_user.png" alt="高级会员" title="高级会员"/><%}
                        else if (mt == Moooyo.BiZ.Member.MemberType.Level2)
                        {%><img src="/upload/vip.png" alt="米柚VIP" title="米柚VIP"/><%}
                        %>
                        </a>
                        <%=obj.Creater.IWant != "" ? "<span title=\"" + obj.Creater.IWant + "\">(" + (obj.Creater.Sex == 2 ? "我想找一个男生" : "我想找一个女生") + (obj.Creater.IWant.Length > 16 ? obj.Creater.IWant.Substring(0, 16) + ".." : obj.Creater.IWant) + ")</span>" : ""%>
                    </div>
                    <div class="Topicmes"><%
                String contentstr = obj.Content.Length > 500 ? obj.Content.Substring(0, 500) + "..." : obj.Content;
                while (true)
                {
                    if (contentstr.IndexOf("\n") >= 0)
                        contentstr = contentstr.Replace("\n", "<br/>");
                    else
                        break;
                }
                contentstr = Comm.getExpression(contentstr);
                                           %>
                        <%=contentstr%><a href="/WenWen/ShowWenWen?wwid=<%=obj.ID %>" style=" margin-left:10px;">详情</a>
                        <div class="imgdiv"><%=Comm.getImageToTopic(obj.ContentImage, 5) %></div>
                        </div>
                    <div class="othermes">
                        <span class="span1"><%=Comm.getTimeSpan(obj.UpdateTime)%></span>
                        <span class="span2" id="answercount<%=obj.ID %>" onclick="showreplay('<%=obj.ID %>')"><%=obj.AnswerCount%>回应</span>
                        <span class="span3">&nbsp;</span>
                        <span class="span4" id="like<%=obj.ID %>" onclick="WenWenControl.likeclick('<%=obj.ID %>')"><%=obj.Likecount %>&nbsp;♥</span>
                        <span class="span5">&nbsp;</span>
                    </div>
                    <div id="Replayitem<%=obj.ID %>" class="Replaydiv">
                        <div class="top">&nbsp;</div>
                        <div class="main">
                            <div class="text"><span class="Expression" onclick="showExpression($('#Replaytext<%=obj.ID %>'))">&nbsp;</span><textarea id="Replaytext<%=obj.ID %>" class="contenttext"></textarea><span class="button" onclick="addanswer('<%=Model.interestObj.ID %>','<%=obj.ID %>')">&nbsp;</span></div>
                            <div class="Replayitem" id="Replay<%=obj.ID %>"></div>
                        </div>
                        <div id="replaymoer<%=obj.ID %>" class="bottom"></div>
                    </div>
                </div>
                </div>
                <div class="hr">&nbsp;</div>
                <%
            }
        }
        else
        {
            if (Request.QueryString["search"] != null)
            {
                %>没有搜索到您所需要的内容！请更换搜索字段或发布您所需要的话题！<%
            }
        } %>
    <!--Begin paging-->
    <% if (Model.Pagger != null) {
            if (Model.Pagger.PageCount > 1) {
                if (Model.Pagger.PageNo < Model.Pagger.PageCount)
                {
                    %><div class="wenwenmoer" onclick="wenwenmoer(<%=Model.Pagger.PageNo+1 %>,'<%=Model.interestObj.ID %>','<%=Model.UserID %>', true)">点击更多</div><%
                }
                %> 
            <% } %>
    <% } %>
    <!--End paging-->
    </div>
    </article>
</section>
<aside class="asidebox-r mt15 fr" style="width:335px;">
    <% Html.RenderAction("InterestFans", "Push"); %>
    <% Html.RenderAction("TheyFavorsInteresting", "Push"); %>
</aside>
</div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/data_<%=Model.JsVersion %>.js"></script>
<script src="/Scripts/jquery.validate.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).mousemove(mouseMove);
    $().ready(function () {
        MemberInfoCenter.BindDataInfo($("[name='wenwenMemberInfo'] [name='wenwenMemberInfoArea']"));
        AutomaticHeight($("#pattern"), $("#pattern").height());
        bindTopicImageZoom($("div.imgdiv"));
        $("body").focus(function () { $("#topicimageupdate").css("display", "none"); clearExpression(); });
        $("#pattern").focus(function () { $("#topicimageupdate").css("display", "none"); clearExpression(); });
        $(".Expression").focus(function () { $("#topicimageupdate").css("display", "none"); clearExpression(); });
        $(".Button").focus(function () { $("#topicimageupdate").css("display", "none"); clearExpression(); });
        $("#showTopic").click(function () { $("#topicimageupdate").css("display", "none"); });
        $(".asidebox-r").click(function () { $("#topicimageupdate").css("display", "none"); clearExpression(); });

        $("div.imgdiv").css("display", "block");

        AutomaticHeight($(".contenttext"));
    });

    function topicsubmit() {
        var interestid = $("#interestid").val();
        interestCenterProvider.isFans(interestid, function (data) {
            var isfans = $.parseJSON(data);
            if (isfans == "false" || isfans == "False" || !isfans) {
                var str = $.jBox.confirm("还没关注这个兴趣，立刻关注？", "提示", function (str) {
                    if (str == "ok") {
                        interestCenterProvider.addInterestFans(interestid, function (data) { });
                    }
                    topicsubmittwo();
                });
            }
            else {
                topicsubmittwo();
            }
        });
    }
    function topicsubmittwo() {
        var imgstr = ".jpg|.JPG|.png|.PNG|.jpeg|.JPEG";
        var imgstrs = imgstr.split('|');
        var ifimgok = true;
        for (var i = 1; i <= 5; i++) {
            var nowfilevalue = $("#file" + i).val();
            if (nowfilevalue != "") {
                var ifnowimgok = false;
                for (var j = 0; j < imgstrs.length; j++) {
                    if (nowfilevalue.indexOf(imgstrs[j]) >= 0) {
                        ifnowimgok = true;
                    }
                }
                if (!ifnowimgok) {
                    ifimgok = false;
                    resetimageFileInput($("#file" + i));
                    $("#showfiletext" + i).html("");
                }
            }
        }
        var pattern = $("#pattern");
        if (!ifimgok) {
            showjbox("你上传的非图片文件已经被自动移除！<br/>不能上传除一下格式以外的图片文件：" + imgstr);
        }
        else if (pattern.val() == "") {
            showjbox("请填写话题内容！");
        }
        else {
            $("#topicform").submit();
        }
    }
    var nowindex = 1;
    //打开图片上传框
    function showtopicimage() {
        var obj = $("#topicimageupdate");
        if (obj.css("display") == "none")
            obj.css({ "display": "block", "left": mouseleft - 20, "top": mousetop + 15 });
        else
            obj.css("display", "none");
        showimageFileInput(1);
        clearExpression();
    }
    //单个或多个图片上传点击
    function showimageFileInput(count) {
        $("div.imagefiledivparent div.imagefilediv").css("display", "none");
        for (var i = 0; i < count; i++)
            $("#imagefile" + (i + 1)).css("display", "block");
        $("div.imagefiledivparent").css({ "display": "block", "height": count * 30 + 10 });
    }
    //计算图片上传的张数
    function setimagefilecount(index) {
        var indexs = 0;
        for (var i = 1; i <= 5; i++) {
            if ($("#file" + i).val() != "") { indexs++; }
        }
        nowindex = indexs;
        $("span.ImageFileCount").html("已选择" + indexs + "个图片");
        $("#showfiletext" + index).html($("#file" + index).val());
        showimageFileInput(index + 1);
    }
    //清除上传的图片
    function clearImageFile() {
        for (var i = 1; i <= 5; i++) {
            resetimageFileInput($("#file" + i));
            $("#showfiletext" + i).html("");
        }
        setimagefilecount(0);
    }
    //清除上传的图片
    function resetimageFileInput(file) {
        file.after(file.clone().val(""));
        file.remove();
    }
    function wenwenmoer(pageno, interestid, userid, ifzj) {
        WenWenLinkProvider.getWenWen(pageno, interestid, function (data) {
            var data = $.parseJSON(data);
            var wwlist = data.wenwenlist;
            var pagecount = data.wenwenpagecount;
            var str = "";
            $.each(wwlist, function (i) {
                var contentstr = wwlist[i].Content.length > 500 ? wwlist[i].Content.substr(0, 500) + "..." : wwlist[i].Content;
                while (true) {
                    if (contentstr.indexOf('\n') >= 0)
                        contentstr = contentstr.replace("\n", "<br/>");
                    else
                        break;
                }
                var contentstr = getExpression(contentstr);
                var contentimage = getImageToTopic(wwlist[i].ContentImage, 5);
                var rzimgstr = showrzimg(wwlist[i], 15, 15);
                str += "<div class=\"Topicitem\" name=\"wenwenMemberInfo\"><div class=\"TopicImage\"><a href=\"/Content/TaContent/" + wwlist[i].Creater.MemberID + "\" target=\"_blank\"><img src=\"" + photofunctions.geticonphotoname(wwlist[i].Creater.ICONPath) + "\" data_me_id=\"" + userid + "\" data_member_id=\"" + wwlist[i].Creater.MemberID + "\" name=\"wenwenMemberInfoArea\" width=\"50\" height=\"50\"/></a></div><div class=\"Topiccontent\"><div class=\"TopicTitle\"><a href=\"/Member/Ta/" + wwlist[i].Creater.MemberID + "\" target=\"_blank\">" + wwlist[i].Creater.NickName + "</a>" + (parseInt(wwlist[i].Creater.MemberType) == 2 ? "<span class=\"vip\" title=\"米柚VIP\">V</span>" : "") + "" + rzimgstr + "<span>" + (wwlist[i].Creater.IWant != "" ? "(" + (wwlist[i].Creater.Sex == 2 ? "我想找一个男生" : "我想找一个女生") + (wwlist[i].Creater.IWant.length > 16 ? wwlist[i].Creater.IWant.substr(0, 16) + ".." : wwlist[i].Creater.IWant) + ")" : "") + "</span></div><div class=\"Topicmes\">" + contentstr + "<div class=\"imgdiv\">" + contentimage + "</div></div><div class=\"othermes\"><span class=\"span1\">" + getTimeSpan(paserJsonDate(wwlist[i].UpdateTime)) + "</span><span class=\"span2\" onclick=\"showreplay('" + wwlist[i].ID + "')\">" + wwlist[i].AnswerCount + "回应</span><span class=\"span3\">&nbsp;</span><span class=\"span4\" id=\"like" + wwlist[i].ID + "\" onclick=\"WenWenControl.likeclick('" + wwlist[i].ID + "')\">" + wwlist[i].Likecount + "&nbsp;♥</span><span class=\"span5\">&nbsp;</span></div><div id=\"Replayitem" + wwlist[i].ID + "\" class=\"Replaydiv\"><div class=\"top\">&nbsp;</div><div class=\"main\"><div class=\"text\"><span class=\"Expression\" onclick=\"showExpression($('#Replaytext" + wwlist[i].ID + "'))\">&nbsp;</span><textarea id=\"Replaytext" + wwlist[i].ID + "\" class=\"contenttext\"></textarea><span class=\"button\" onclick=\"addanswer('" + interestid + "','" + wwlist[i].ID + "')\">&nbsp;</span></div><div class=\"Replayitem\" id=\"Replay" + wwlist[i].ID + "\"></div></div><div id=\"replaymoer" + wwlist[i].ID + "\" class=\"bottom\">&nbsp;</div></div></div></div><div class=\"hr\">&nbsp;</div>";
            });
            $("div.wenwenmoer").remove();
            if (pageno + 1 <= pagecount) {
                str += "<div class=\"wenwenmoer\" onclick=\"wenwenmoer(" + (pageno + 1) + ",'" + interestid + "','" + userid + "', true)\">点击更多</div>";
            }
            if (ifzj) {
                $("#showTopic").html($("#showTopic").html() + str);
            }
            else {
                $("#showTopic").html(str);
            }
            bindTopicImageZoom($("div.imgdiv"));
            MemberInfoCenter.BindDataInfo($("[name='wenwenMemberInfo'] [name='wenwenMemberInfoArea']"));
        });
    }
    var pagesize = 5;
    var pagecount = 0;
    var pageno = 1;
    var count = 0;
    function showreplay(wenwenid) {
        var objparent = $("#Replayitem" + wenwenid);
        if (objparent.css("display") == "none") {
            pagecount = 0;
            pageno = 1;
            count = 0;
            showreplaytwo(wenwenid, false);
            objparent.css("display", "block");
            var textobj = $("#Replaytext" + wenwenid);
            var textparent = $("#Replayitem" + wenwenid + " div.main div.text");
            textobj.keydown(function () { replaytextheight(textparent, this, $("#Replaytext" + wenwenid).scrollHeight + 20); });
            textobj.keypress(function () { replaytextheight(textparent, this, $("#Replaytext" + wenwenid).scrollHeight + 20); });
            textobj.keyup(function () { replaytextheight(textparent, this, $("#Replaytext" + wenwenid).scrollHeight + 20); });
            textobj.focus(function () { replaytextheight(textparent, this, $("#Replaytext" + wenwenid).scrollHeight + 20); });
            textobj.blur(function () { replaytextheight(textparent, this, $("#Replaytext" + wenwenid).scrollHeight); })
        }
        else {
            objparent.css("display", "none");
            $("#Replaytext" + wenwenid).val("");
        }
    }
    function replaytextheight(textparent, textobj, textheight) {
        if (textobj.scrollHeight > textheight) textobj.style.height = textobj.scrollHeight + "px";
        else textobj.style.height = textheight + "px";
        textparent.height(parseInt(textobj.style.height) + 2);
    }
    function showreplaytwo(wenwenid, ifadditional) {
        var obj = $("#Replay" + wenwenid);
        WenWenLinkProvider.getwenwenanswer(wenwenid, pagesize, pageno, -1, function (data) {
            var data = $.parseJSON(data);
            var answerlist = data.answerlist;
            count = data.answercount;
            pagecount = data.answerpagecount;
            pageno = data.answernextpageno;
            var str = "";
            $.each(answerlist, function (i) {
                var thiscontent = answerlist[i].Content.toString().length > 40 ? answerlist[i].Content.toString().substr(0, 40) + ".." : answerlist[i].Content.toString();
                for (var j = 0; j < thiscontent.length; j++) {
                    if (thiscontent.indexOf('\n') >= 0)
                        thiscontent = thiscontent.replace("\n", "<br/>");
                    else
                        break;
                }
                thiscontent = getExpression(thiscontent) + "&nbsp;<span>" + getTimeSpan(paserJsonDate(answerlist[i].CreatedTime)) + "</span>";
                var ifmoer = answerlist[i].Content.toString().length > 40;
                var rzimgstr = showrzimg(answerlist[i], 15, 15);
                str += "<div class=\"memberimage\"><img src=\"" + photofunctions.geticonphotoname(answerlist[i].Creater.ICONPath) + "\" width=\"30\" height=\"30\"/></div><div class=\"replaycontent\" " + (ifmoer ? "title=\"" + answerlist[i].Content + "\"" : "") + "><a href=\"/Content/TaContent/" + answerlist[i].Creater.MemberID + "\" target=\"_blank\">" + answerlist[i].Creater.NickName + "</a>" + rzimgstr + "" + thiscontent + "</div><div class=\"replaymana\"><a onclick=\"answerhf('" + answerlist[i].Creater.NickName + "','" + wenwenid + "')\">回复</a></div>";
            });
            if (pageno <= pagecount) {
                str += "<div class=\"moer\"><span style=\"cursor:pointer;\" onclick=\"showreplaytwo('" + wenwenid + "',true)\">更多" + (count - (pageno - 1) * pagesize) + "个回复</span>&nbsp;&nbsp;&nbsp;<span style=\"cursor:pointer;\"onclick=\"showreplay('" + wenwenid + "')\">收起</span></div>";
            }
            else {
                str += "<div class=\"moer\"><span style=\"cursor:pointer;\"onclick=\"showreplay('" + wenwenid + "')\">收起</span></div>";
            }
            if (ifadditional) {
                $("div.moer").remove();
                obj.html(obj.html() + str);
            }
            else {
                obj.html(str);
            }
            $("#answercount" + wenwenid).html(count + "回应");
        });
    }
    function addanswer(interestid, wenwenid) {
        interestCenterProvider.isFans(interestid, function (data) {
            var isfans = $.parseJSON(data);
            if (isfans == "false" || isfans == "False" || !isfans) {
                var str = $.jBox.confirm("还没关注这个兴趣，立刻关注？", "提示", function (str) {
                    if (str == "ok") {
                        interestCenterProvider.addInterestFans(interestid, function (data) { });
                    }
                    addanswertwo(interestid, wenwenid);
                });
            }
            else {
                addanswertwo(interestid, wenwenid);
            }
        });
    }
    function addanswertwo(interestid, wenwenid) {
        var content = $("#Replaytext" + wenwenid).val();
        content = rtrim(content);
        if (content != "") {
            WenWenLinkProvider.addwenwenanswer(interestid, wenwenid, content, true, function (data) {
                var data = $.parseJSON(data);
                pagecount = 0;
                pageno = 1;
                count = 0;
                showreplaytwo(wenwenid, false);
            });
            $("#Replaytext" + wenwenid).val("");
            var textparent = $("#Replayitem" + wenwenid + " div.main div.text");
            replaytextheight(textparent, document.getElementById("Replaytext" + wenwenid), 21);
        }
        else {
            showjbox("请填写回复内容！");
        }
    }
    function answerhf(username, wenwenid) {
        $("#Replaytext" + wenwenid).val("回复" + username + "\n  ");
        $("#Replaytext" + wenwenid).focus();
    }
</script>
</asp:Content>
