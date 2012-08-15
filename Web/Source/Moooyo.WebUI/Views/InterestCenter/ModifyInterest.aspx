<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    修改我的兴趣
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
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
        <aside class="asidebox mt32 bor-r3 mr52 fl">
		<% Html.RenderAction("LeftInterest", "InterestCenter");%>
    </aside>
        <!-- .leftside  end -->
        <section class="add-inter fl mt32">
    	<h2 class="add-inter-tips">添加自己的兴趣，做兴趣团的老大</h2>
        <h3 class="add-step1-tit titbg mt32">上传一张你感兴趣的东东的图片</h3>
       <div class="mt18 clearfix">
        
        <div class="upload-photo mt18"><img src="<%= Comm.getImagePath(Model.interestObj.ICONPath, ImageType.Middle) %>" width="102" height="102" alt="兴趣图片" title="兴趣图片" id="imgInterestIconPreview" />(上传后显示在这里，选择的图片要比这个大)</div>
        <fieldset class="flash hidden" id="fsUploadProgress"></fieldset>
        <div id="divStatus"></div>
        <div id="upload-btn">
            <span id="btn_holder"></span>
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
        <!--上传图片结束-->
        <form id="frmInterest" name="frmInterest" action="/InterestCenter/AddMemberInterest" method="post" enctype="multipart/form-data">
        <input type="hidden" value="" id="uploadFiles" name="uploadFiles" />
        <input type="hidden" value="" id="uploadFileNames" name="uploadFileNames" />
        <h3 class="add-step2-tit titbg mt32">告诉大家它是什么</h3>
        <div class="mt32 askfrm">
            <p>名称<input type="text" name="txtTitle" id="txtTitle" class="ml15" value="<%= Model.interestObj.Title %>" /></p>
            <p class="mt6">介绍<textarea value="" name="txtContent" id="txtContent" class="ml15"><%= Model.interestObj.Content%></textarea></p>
            <div class="mt6">标签<input type="text" name="txtClasses" id="txtClasses" name="span" value="<%= Model.interestObj.Classes %>" class=" ml15"/><b class="c999">(用逗号分隔标签)</b>
                <div class="span-list mt5">
                <% foreach (var obj in Model.interestClassListObje)
                   { %>
                    <a href="javascript:void(0);" onclick="getInterestClassVal('<%= obj.Title %>')"><%= obj.Title %></a>
                <% } %>
                    <a href="/InterestCenter/MoreInterestClasses" target="_blank">更多分类</a>
                </div>
            </div>
            <input type="button" value="搞定了，保存" class="radius3 btn fl" onclick="modifyInterest()" /><input type="button" value="不想修改" class="radius3 btn fl margLeft4" onclick="javascript:location.href='/InterestCenter/InterestFans?iId=<%= Model.interestObj.ID %>';" />
        </form>
        </div>
    </section>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/data_<%=Model.JsVersion %>.js"></script>
<script src="/Scripts/jquery.validate.min.js" type="text/javascript"></script>
<script type="text/javascript" src="/js/up/swfupload_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/up/swfupload.swfobject_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/up/swfupload.queue_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/up/photoprogress_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/up/photo_upload_regupface_<%=Model.JsVersion %>.js"></script>
<script language="javascript" type="text/javascript">
    var settings;
    $().ready(function () {
        frmInterestValidate();
        uploadpath = "<%=Model.UploadPath %>";
        //上传图片开始
        settings = {
            debug: false,
            flash_url: '/swf/swfupload.swf',
            file_post_name: 'file',
            upload_url: '/up/addphoto',
            post_params: {
                "ASPSESSID": "<%=Session.SessionID %>",
                "AUTHID": "<%=Request.Cookies[FormsAuthentication.FormsCookieName].Value%>",
                "photoType": '30'
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
        oUploadBtn = $("#upload-btn"),
        oUploadStatus = $("#upload-status"),
        oStartUpload = $('#upload-start'),
        oBtnCancel = $('#btnCancel'),
        oOptBtns = $('.opt-btns'),
        oUploadTips = $('.upload-tips'),
        TMPL_UPLOAD_TIPS = "";
        swfup = new SWFUpload(settings);

        oUploadBtn.show();
        $('#uploadFiles').val('');
        $('#uploadFileNames').val('');

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
                oUploadTips.removeClass('hidden');
            }, $.browser.msie ? 1200 : 800);
        });

        // start upload
        oStartUpload.click(function () {
            swfup.startUpload();
        });

        // continue upload
        oUploadStatus.find('.continue').click(function (e) {
            e.preventDefault();
            oUploadBtn.removeAttr('style');
            oUploadStatus.addClass('hidden');
        });

        $('.nextstep a').click(function (e) {
            e.preventDefault();

            $('#upload').submit();
        });
        //上传图片结束
    });
    function uploadfinished() {
        var filenames = $("#uploadFileNames").val();
        if (filenames.length < 1) {
            return;
        }
        var filenamelist = filenames.split('|');
        for (var i = 0; i < filenamelist.length; i++) {
            var filename = filenamelist[i];
            if (filename != "") {
                $("#imgInterestIconPreview").attr("src", photofunctions.getprofileiconphotoname(filename));
            }
        }
    }
    function frmInterestValidate() {
        $("#frmInterest").validate({
            rules: {
                txtTitle: { required: true },
                txtContent: { required: true },
                txtClasses: { required: true }
            },
            messages: {
                txtTitle: { required: "必须输入名称" },
                txtContent: { required: "必须输入介绍" },
                txtClasses: { required: "必须输入标签" }
            }
        });
    }
    function modifyInterest() {
        var interestId = "<%= Model.interestObj.ID %>"; 
        var frmInterest = $("#frmInterest");
        if (frmInterest.valid()) {
            interestCenterProvider.modifyInterest(interestId, $("#txtTitle").val(), $("#txtContent").val(), $("#txtClasses").val(), $("#uploadFiles").val(), function(data) {
                var result = $.parseJSON(data);
                if (result.ok) {
                    alert("兴趣修改成功！");
                    window.location.href = "/InterestCenter/InterestFans?iId=" + interestId;
                }
                else {
                    $.jBox.tip("兴趣修改失败，系统维护中，给您带来了不便，请谅解！", 'error');
                    location.reload();
                }
            });
        }
     }
     function getInterestClassVal(val) {
         if ($("#txtClasses").val() != "") {
             $("#txtClasses").val($("#txtClasses").val() + ',' + val);
             return;
         }
         $("#txtClasses").val(val);
     }
</script>
</asp:Content>
