<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    [米柚] <%=Model.Member.Name %>的照片墙
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="c976" style="min-height:80px; height:auto; height:auto!important;">
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
    <!--个人顶部块-->
     <% if (!Model.IsOwner)
        {%>
        <% Html.RenderPartial("~/Views/Member/ProfileTopPanel.ascx"); %>
    <% }
        else
        {%>
    <%--<article class="top-title-box mt20 fl"/>--%>
    <%} %>
    <!--endof 个人顶部块-->
    <section class="inter-conbox mt11 fl">
    <h2 class="f14 fb fl"><a href="/photo/mplist/<%=Model.MemberID %>">返回相册</a>&nbsp;</h2>
    <div class="fl top10 left55" style="width:90px; text-align:center;">
    <% if (Model.IsOwner)
       {%>
    <a href="/photo/photoupload?t=0" class="btn">&nbsp;上传照片&nbsp;</a>
    <%} %>
    </div>
    <div class="fr" style="margin:5px;">
    <ul>
    <li class="fl"><a style="cursor:pointer" onclick="getprevphoto()"> < 前一张 </a></li>
    <li class="fl"><a href="/photo/mplist/<%= Model.MemberID %>?t=-1">&nbsp;+&nbsp;</a></li>
    <li class="fl"><a style="cursor:pointer" onclick="getnextphoto()"> 后一张 >&nbsp;</a></li>
    </ul>
    </div>
    <div class="clearfix"></div>
    <div class="mt11">
    <div class="photoshowpanel"><div id="photoPreview" style="min-height:80px; height:auto; height:auto!important; display:block;"><img src="<%=Model.UploadPath %>/<%=ViewData["photourl"] %>" title="点击看下一张" id="bigimg" class="bigimg linkT1" onclick="getnextphoto()" /></div>
    <%--<div class="clearfix"></div>
    <div class="clearBoth fr">
    <div class="fl"><a href="javascript:;" onclick="rotatePhoto($('#bigimg'), 'left');">&lt; 向左旋转</a></div><div class="fl">&nbsp;&nbsp;<a href="javascript:;" onclick="rotatePhoto($('#bigimg'), 'right');">向右旋转 &gt;</a></div>
    </div>--%>
    </div>
    <div class="editorpanel" id="editorpanel"></div>
    <div id="metapanel" style="width:600px; min-height:105px; height:auto!important; height:auto; clear:both; padding:0px; margin:0px;">
    <div id="phototitleeditbtn" class="metatitleeditbtn"></div>
    <div id="phototitle" class="fb" title='<%=ViewData["phototitle"] %>'><%=ViewData["phototitle"].ToString().Length > 35 ? ViewData["phototitle"].ToString().Substring(0, 35) + "<span class=\"letspa--3\">...</span>" : ViewData["photocontent"]%></div>
    <div id="photocontent" title='<%=ViewData["photocontent"] %>'><%=ViewData["photocontent"].ToString().Length > 35 ? ViewData["photocontent"].ToString().Substring(0, 35) + "<span class=\"letspa--3\">...</span>" : ViewData["photocontent"]%></div>
    <div class="cgray mt11" style="clear:both;">上传于 <span class="blue" id="photodate"><%=ViewData["photodate"] %></span>
    &nbsp;<span id="photoviewscount"><%=ViewData["photoviewscount"]%>浏览</span>&nbsp;
    <% if ((int)ViewData["photocommentscount"] > 0)
       {%>
    <span id="photocommentscount"><%=ViewData["photocommentscount"]%>评论</span>
    <% } %>
    </div>
    </div>
    <div style="width:100%;height:5px;"></div>
    <div class="clearfix2"></div>
    <div id="cmts" class="cmts" style="width:600px; clear:both; padding:0px; margin:0px;">
    </div>
    <div class="writecomment">
    <textarea id="photocommentwriter"></textarea><br />
    <input type="button" value=" 评论 " class="btn mt11" onclick="addcmt();"/>
    </div>
    </div>
    </section>
     <aside class="asidebox-r mt32 fr">
        <% Html.RenderAction("TheyLike", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
        <% Html.RenderAction("SameInterestingMemberOverMe", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
        <% if (!Model.IsOwner) Html.RenderAction("TheyFavorsMember", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
    </aside>
</div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/a_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script type="text/javascript" src="/Scripts/exts_0101.js"></script>
<script language="javascript" type="text/javascript">
    var photowalldata;
    $().ready(function () {
        imgid = '<%=ViewData["imgid"] %>';
        uploadpath = '<%=Model.UploadPath %>';
        photocomments = <%=ViewData["photocomments"] %>;
        photowalldata = <%=ViewData["photowalldata"] %>;
        mid = '<%=Model.MemberID%>';
        var isMestr = '<% =Model.IsOwner%>';
        isMe = false;
        if (isMestr=="True") isMe=true;
        binddata();
        $("#phototitle").html(gettitle('<%=ViewData["phototitle"]%>'));
        $("#photocontent").html(getcontent('<%=ViewData["photocontent"]%>'));
        setenterdown($("#photocommentwriter"),addcmt);
    });
    function editPhoto()
    {
        var editorpanel = $("#editorpanel");
        var metapanel = $("#metapanel");
        var titlep = $("#phototitle");

        editorpanel.empty();
        var titleinput = "想一个开心的标题吧：<br><textarea id=\'photoeditinputtitle\' class=\'photoeditinput\' style='width:300px; height:30px;'>"+titlep.text()+"</textarea><br>";
        editorpanel.append(titleinput);

        var contentp = $("#photocontent");
        var contentinput = "还可以分享拍照的经历和心情呢：<br><textarea id=\'photoeditinputcontent\' class=\'photoeditinput\' style='width:300px; height:60px;'>"+contentp.text()+"</textarea><br>";
        editorpanel.append(contentinput);

        var submitbtn = "<input type=\'button\' class=\'btnDone\' value=\'写好了\' onclick=\'submitEdit()\' style=\"width:60px; margin-top:5px;cursor:pointer;\"/><input type=\'button\' class=\'btnDone\' value=\'不必了\' onclick=\'cancelEdit()\' style=\"width:60px; margin-top:5px;cursor:pointer;margin-left:10px;\"/><br/><br/>";
        editorpanel.append(submitbtn);
        metapanel.hide();
        editorpanel.show();

    }
    function bindphoto(id)
    {
        photoprovider.getphoto(id,function(result){
            var re = $.parseJSON(result);
            imgid = re.ID;
            $("#bigimg").attr("src",uploadpath+"/"+re.FileName);
            $("#phototitle").html(gettitle(re.Title));
            $("#photocontent").html(getcontent(re.Content));
            $("#photodate").html(paserJsonDate(re.CreatedTime).format("yyyy-mm-dd HH:MM:ss"));
            $("#photoviewscount").html(re.ViewCount + "浏览");
            if (re.CommentCount>0)
            {
                $("#photocommentscount").html(re.CommentCount + "评论");
                $("#photocommentscount").show();
            }
            else
            {
                $("#photocommentscount").hide();
            }
            photocomments = re.Comments;
            binddata();
        });
    }
    function getnextphoto()
    {
        var q = 0;
        $.each(photowalldata,function(i){
            if (photowalldata[i].ID == imgid)
            {
                q=i;
            }
        });
        q++;
        if (q>=photowalldata.length) q=0;
        bindwallphoto(q);
    }
    function getprevphoto()
    {
        var q = 0;
        $.each(photowalldata,function(i){
            if (photowalldata[i].ID == imgid)
            {
                q=i;
            }
        });
        q--;
        if (q<0) q=photowalldata.length-1;
        bindwallphoto(q);
    }
    function bindwallphoto(i)
    {
        imgid = photowalldata[i].ID;
        bindphoto(imgid);
    }
    function binddata()
    {
        //刷新浏览数量
        photoprovider.viewphoto(imgid,function(){});
        //绑定评论
        getcomments();
        bindcmts();
    }
    function addcmt()
    {
        var cmt = $("#photocommentwriter").val();
        if (trim(cmt)=="") {$("#photocommentwriter").focus();return;}
        if (!checkLen(cmt)) return;

        photoprovider.addphotocomment(imgid,cmt,function(){
            $("#photocommentwriter").val("");
            getcomments();
        });
    }
    function getcomments()
    {
        photoprovider.getcomments(imgid,function(result){
            photocomments = $.parseJSON(result);
            bindcmts();
        });
    }
    function bindcmts()
    {
        if (photocomments==null || photocomments.length==0) {$("#cmts").html("");return;}
        var str = "";
        $.each(photocomments,function(i){
            str += "<ul class='cmt' id=\"showmember\">";
            str += "<li class='cmticon'><a href='/member/i/"+photocomments[i].MemberID+"' target='_blank'><img class=\"infos\" data_me_id=\"<%=Model.UserID %>\" data_member_id=\""+photocomments[i].MemberID+"\" src='"+photofunctions.geticonphotoname(photocomments[i].ICONPath)+"'/></a></li>";
            str += "<li class='cmtnickname'><a href='/member/i/"+photocomments[i].MemberID+"' target='_blank'>"+photocomments[i].NickName+"</a> ("+getTimeSpan(paserJsonDate(photocomments[i].CreatedTime))+")</li>";
            str += "<li class='cmtdelbtn' style=\"display:none\">";
            if (isMe)
            {
                str += "<a href='javascript:' onclick='delcmt(\""+photocomments[i].MemberID+"\",\""+photocomments[i].CreatedTime+"\")'>删除</a>";
            }
            str += " <a href='javascript:' onclick='actionprovider.opencalladmin(\""+photocomments[i].MemberID+"\",1)'>举报</a></li>";
            str += "<li class='clearfix'></li>";
            str += "<li class='cmtcontent'>"+photocomments[i].Content+"</li>";
            str += "</ul>";
            });

        
        $("#cmts").html(str);
        //头像弹窗
        MemberInfoCenter.BindDataInfo($("#showmember img.infos"));
        //鼠标效果
        $(".cmt").mouseenter(function(){            
            $(this).children(".cmtdelbtn").show();           
        });
        $(".cmt").mouseleave(function(){
            $(this).children(".cmtdelbtn").hide();
        });
    }
    function delcmt(mid,createdtime)
    {
        if (!confirm("确认删除这条评论？")) return;
        photoprovider.delphotocomment(mid,imgid,createdtime,function(result){
            getcomments();
        });
    }
    function gettitle(title)
    {
        var str = title;
        if (isMe)
        {
            $("#phototitleeditbtn").html("<div><a href='javascript:' onclick='editPhoto()'><i>修改标题和描述</i></a><div>");
        }
        return str;
    }
    function cancelEdit()
    {
        var editorpanel = $("#editorpanel");
        var metapanel = $("#metapanel");
        editorpanel.hide();
        metapanel.show();
    }
    function submitEdit()
    {
        var titleinput = $("#photoeditinputtitle");
        var title = titleinput.text();
        var contentinput = $("#photoeditinputcontent");
        var content = contentinput.text();   

        photoprovider.updatephoto(imgid,title,content,function(i){
                //更新页面对象
                var titlep = $("#phototitle");
                var contentp = $("#photocontent");
                titlep.text(title);
                contentp.text(content);
                //更新编辑状态
                var editorpanel = $("#editorpanel");
                var metapanel = $("#metapanel");
                editorpanel.hide();
                metapanel.show();
            });
    }
    function getcontent(content)
    {
        var str = content;
        return str;
    }

    function rotatePhoto(imgElement, direction) {
        switch(direction) {
            case "left":
                $(imgElement).rotateLeft();
                //alert($('#photoPreview').parent().height() + $('#bigimg').height());
                //$('#photoPreview').parents().height($('#photoPreview').parent().height() + $('#bigimg').height());

//                $('#content').height($('#content').height() + $('#bigimg').height() - $('#bigimg').width());
//                $('#content').css('height', $('#content').height() + $('#bigimg').height() - $('#bigimg').width());

//                $('.c976').height($('.c976').height() + $('#bigimg').height() - $('#bigimg').width());
//                $('.c976').css('height', $('.c976').height() + $('#bigimg').height() - $('#bigimg').width());

//                $('.inter-conbox').height($('.inter-conbox').height() + $('#bigimg').height() - $('#bigimg').width());
//                $('.inter-conbox').css('height', $('.inter-conbox').height() + $('#bigimg').height() - $('#bigimg').width());

//                $('.mt11').height($('.mt11').height() + $('#bigimg').height() - $('#bigimg').width());
//                $('.mt11').css('height', $('.mt11').height() + $('#bigimg').height() - $('#bigimg').width());

//                $('.photoshowpanel').height($('.photoshowpanel').height() + $('#bigimg').height() - $('#bigimg').width());
//                $('.photoshowpanel').css('height', $('.photoshowpanel').height() + $('#bigimg').height() - $('#bigimg').width());

                $('#photoPreview').css('height', $('#bigimg').height());
                $('#photoPreview').height($('#bigimg').height());

                $("#bigimg").removeAttr("width").removeAttr("height").attr("title", "点击看下一张").addClass("bigimg linkT1").bind("click", function () {
                    getnextphoto();
                    $("#bigimg").removeAttr("style");
                });

                break;
            case "right":
                $(imgElement).rotateRight();
                //$('#photoPreview').parent().height($('#photoPreview').parent().height() + $('#bigimg').height());
                $('#photoPreview').css('height', $('#bigimg').height());
                $('#photoPreview').height($('#bigimg').height());

                $("#bigimg").removeAttr("width").removeAttr("height").attr("title", "点击看下一张").addClass("bigimg linkT1").bind("click", function () {
                    getnextphoto();
                    $("#bigimg").removeAttr("style");
                });
            default: break;
        }
    }
</script>
</asp:Content>
