<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Model.Member.Name%>的相册_照片上传
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    <link href="/css/upload-photo_<%=Model.CssVersion %>.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
.photoUpload{float:left;width:590px;padding-right:40px}
.split5px{padding-top:5px}
.hidden{display:none}
.photobuttons{color:#074945;text-align:right;font-size:12px;margin:0 10px;text-decoration:none;font-family:Arial;}
#photobuttons a {color:#074945;text-decoration:none;}
#photobuttons a:hover{color:#a90303;text-decoration:none;}
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
    <!--个人顶部块-->
     <% if (!Model.IsOwner)
        {%>
        <% Html.RenderPartial("~/Views/Member/ProfileTopPanel.ascx"); %>
    <% }
        else
        {%>
    <%--<article class="top-title-box mt20 fl" />--%>
    <%} %>
    <!--endof 个人顶部块-->
	<section class="add-inter fl mt32">
    	<div class="h50">
            <div class="h50 fl"><h2 class="add-inter-tips ">上传自己的照片，让更多的人关注你</h2></div>
            <div class=" h50 mr52 fl w146 tcenter" style="line-height:40px;"><a href="/photo/mplist/<%=Model.Member.ID %>">回到个人相册</a></div>
        </div>
      <form action="/photo/mplist/<%=Model.Member.ID %>" method="post" id="upload">
        <div id="uploadPhotoStep1">
      <h3 class="add-step1-tit titbg mt32">选择您要上传的照片</h3>
        <input type="hidden" value="" id="uploadFiles" name="uploadFiles" />
        <input type="hidden" value="" id="uploadFileNames" name="uploadFileNames" />
        <div class="upload-tips"></div>
        <div id="upload-btn"><span id="btn_holder"></span></div>
        <div class="upload-list hidden">
            <div class="hd"><span class="name">照片</span><span class="size">大小</span><span class="delete">删除?</span></div>
            <div class="bd"><fieldset class="flash" id="fsUploadProgress"></fieldset></div>
        </div>
        <div id="divStatus"></div>
        <div id="upload-status" class="hidden">
            <span class="num">共&nbsp;<b></b>&nbsp;张</span><span class="continue-wrapper"></span>
            <span class="total-size">总计：<b></b></span>
        </div>
        <div id="has-error" class="hidden">
            <b></b>张照片上传失败。&nbsp;<span class="nextstep hidden">&gt;&nbsp;<a href="#">继续下一步</a>？</span>
        </div>
        <div class="opt-btns hidden">
            <input type="button" id="upload-start" value="开始上传" class="btn-green" />
            <a id="btnCancel" href="#">取消上传</a>
        </div>
       </div> 
       </form>
       <div id="uploadPhotoStep2" class="hidden">
        <form action="" id="frmPhotoInfoList" name="frmPhotoInfoList">
            <h3 class="add-step2-tit titbg mt32">给您的图片写点什么吧，让大家觉得更有意思。</h3>
            <div id="photoInfoList"></div>
        </form>
        <div style="padding-top:10px;">
            <div class="fl ml10 mr20"><input type="button" value="完成" class="radius3 btn" onclick="updatePhotoInfo()" /></div>
            <a class="fl" href="javascript:;" onclick="$('#upload').submit()"> &nbsp;&nbsp;不必了</a>
        </div>
       </div>
    <noscript>
        <div class="message">
            您的浏览器不支持javascript，不能使用此页面的全部功能。
            请换用其他浏览器或者开启对javascript的支持。
        </div>
    </noscript>
 
    <div id="noflash" class="attn hidden">
            您没有安装flash播放器，或者您的flash版本不够，无法使用上传功能. 请安装最新版本的flashplayer.( <a href="http://get.adobe.com/cn/flashplayer/">官方下载</a> )
 
    </div>
    <div id="old-portal">
    </div>
    </section>    
</div>   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script type="text/javascript" src="/js/up/swfupload_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/up/swfupload.swfobject_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/up/swfupload.queue_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/up/photoprogress_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/up/photo_upload_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" language="javascript">
    var settings;
    $().ready(function () {
        settings = {
            debug: false,
            flash_url: '/swf/swfupload.swf',
            file_post_name: 'file',
            upload_url: '/up/addphoto',
            post_params: {
                "ASPSESSID": "<%=Session.SessionID %>",
                "AUTHID": "<%=Request.Cookies[FormsAuthentication.FormsCookieName].Value%>",
                "photoType": '<%=ViewData["phototype"] %>'
            },
            file_size_limit: '5 MB',
            file_types: '*.jpg; *.jpeg; *.gif; *.bmp; *.png',
            file_types_description: 'Picture Files',
            file_upload_limit: 10,
            file_queue_limit: 10,
            custom_settings: {
                progressTarget: 'fsUploadProgress',
                cancelButtonId: 'btnCancel',
                startButtonId: 'btnStart',
                jUrl: '/up/'
            },
            button_placeholder_id: 'btn_holder',
            button_width: 98,
            button_height: 29,
            button_image_url: '/pics/upload-btns.png',

            file_queued_handler: fileQueued,
            file_queue_error_handler: fileQueueError,
            file_dialog_complete_handler: fileDialogComplete,
            upload_start_handler: uploadStart,
            upload_progress_handler: uploadProgress,
            upload_error_handler: uploadError,
            upload_success_handler: uploadSuccess,
            upload_complete_handler: uploadComplete,
            queue_complete_handler: queueComplete,

            inimum_flash_version: '9.0.28',
            swfupload_pre_load_handler: preload,
            swfupload_load_failed_handler: swfuploadLoadFailed
        },

    oUploadBtn = $('#upload-btn'),
    oUploadStatus = $('#upload-status'),
    oStartUpload = $('#upload-start'),
    oBtnCancel = $('#btnCancel'),
    oOptBtns = $('.opt-btns'),
    oUploadTips = $('.upload-tips'),
    TMPL_UPLOAD_TIPS = '提示：支持<b>gif、jpg、png</b>，文件不能超过<b>2M</b>，一次最多可选择<b>20张</b><br><font color="#ff0000">拒绝暴利、色情、非法等图片，发现即封号!</font>';

        swfup = new SWFUpload(settings);

        oUploadBtn.show();

        // init tips
        oUploadTips.html(TMPL_UPLOAD_TIPS.replace('{CMD}', navigator.userAgent.search('Mac') !== -1 ? 'command' : 'ctrl'));

        // cancel queue
        oBtnCancel.click(function (e) {
            e.preventDefault();
            swfup.cancelQueue();
            oOptBtns.addClass('hidden');
            oUploadStatus.addClass('hidden');
            oUploadBtn.removeAttr('style');
            oStartUpload.val('开始上传').removeAttr('disabled');
            setTimeout(function () {
                //oUploadTips.removeClass('hidden');
            }, $.browser.msie ? 1200 : 800);
        });

        // start upload
        oStartUpload.click(function () {
            swfup.startUpload();
        });

        $('.nextstep a').click(function (e) {
            e.preventDefault();
            $('#upload').submit();
        });
    });

    //禁止浏览器后退
    //禁用后退按钮
    window.history.forward(1);
    //禁用后退键，作用于Firefox、Opera  
    document.onkeypress = banBackSpace;
    //禁用后退键，作用于IE、Chrome  
    document.onkeydown = banBackSpace;

    function uploadfinished() {
        var str = "";
        var filenames = $("#uploadFileNames").val();
        var fileIds = $("#uploadFiles").val();
        if (filenames.length < 1) {
            return;
        }
        var filenamelist = filenames.split('|');
        var fileIdList = fileIds.split('|');
        for (var i = 0; i < filenamelist.length; i++) {
            var filename = filenamelist[i];
            var fileId = fileIdList[i];
            if (filename != "") {
                str += "<div style=\"padding-top:10px;\" class=\"h70\">";
                str += "<div class=\"fl ml10 h70 w70\"><img src=\"" + photofunctions.getprofileiconphotoname(filename) + "\" height=\"70\" width=\"70\" border=\"0\" /></div>";
                str += "<div class=\"fl h70\">";
                str += "<p><input type=\"text\" name=\"title" + fileId + "\" id=\"title" + fileId + "\" class=\"ml5 h20\"/>";
                if (i == 1) {
                    str += "<a href=\"javascript:;\" onclick=\"copyToAllPhoto('title', '" + fileId + "')\" style=\"margin-left:10px;\">复制给所有图片</a></p>";
                }
                str += "<p class=\"mt5\"><textarea value=\"\" class=\"ml5 h40 w280\" id=\"content" + fileId + "\" name=\"content" + fileId + "\"></textarea>";
                if (i == 1) {
                    str += "<a href=\"javascript:;\" onclick=\"copyToAllPhoto('content', '" + fileId + "')\" style=\"margin-left:10px;\">复制给所有图片</a></p>";
                }
                str += "</div>";
                str += "</div>";
            }
        }
        $("#photoInfoList").html(str);
    }
    function updatePhotoInfo() { 
        var fileIds = $("#uploadFiles").val(), photoIDs = "", photoTitles = "", photoContents = "";
        if (fileIds.length < 1) {
            return;
        }
        var fileIdList = fileIds.split('|');
        for (var i = 0; i < fileIdList.length; i++) {
            var fileId = fileIdList[i];
            if (fileId != "") {
                photoIDs += fileId + "|";
                photoTitles += $("#title" + fileId).val() + "|";
                photoContents += $("#content" + fileId).val() + "|";
            }
        }
        photoprovider.batchUpdatePhotoInfo(photoIDs, photoTitles, photoContents, function (data) {
            var result = $.parseJSON(data);
            if (result.ok) {
                $("#upload").submit();
            }
            else {
                $.jBox.tip("图片上传失败，系统维护中，给您带来了不便，请谅解！", 'error');
            }
        });
    }
    //将文本复制给所有元素
    function copyToAllPhoto(container, containerID) {
        var val, frmPhotoInfoList = document.frmPhotoInfoList.elements;
        switch(container) {
            case "title":
                val = $("#title" + containerID).val();
                for (var i = 0; i < frmPhotoInfoList.length; i++) {
                    if (frmPhotoInfoList[i].type == 'text') {
                        $(frmPhotoInfoList[i]).val(val);
                    }
                }
                break;
            case "content":
                val = $("#content" + containerID).val();
                for (var i = 0; i < frmPhotoInfoList.length; i++) {
                    if (frmPhotoInfoList[i].type == 'textarea') {
                        $(frmPhotoInfoList[i]).val(val);
                    }
                }
                break;
        }
    }
</script>
</asp:Content>
