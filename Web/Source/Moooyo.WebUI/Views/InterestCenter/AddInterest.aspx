<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>

<%@ Import Namespace="Moooyo.WebUI.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    创建兴趣群组
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- 内容-->
    <%if (ViewData["error_max"] != null)
      {
    %>兴趣已达到上相，不能创建！<%
  }
      else
      {
    %>
    <div class="container">
        <div class="box_demo1 w680 fb_left">
            <div class="mt10">
            </div>
            <div class="Set_title2 border_b1 p40">
                <img src="/pics/add_xiqu.gif" width="51" height="51" /><span style="font-size: 22px">创建兴趣群组
                    <font class="cgray">（还有别的爱好？赶紧创建一个群组吧，别放过了同兴趣的单身柚子。）</font></span><a class="com_back blue02"
                        href="/InterestCenter/AddInterestFans">返回</a>
            </div>
            <div class="Set_box1 p40" style="padding: 40px; padding-top: 20px;" id="addInterest">
                <div class="mt10">
                </div>
                <div class="fb_box_com w600">
                    <span class="add_xiqu"></span>
                </div>
                <div class="one_step">
                    <div class="fb_box_com w600">
                        <span class="step_01 step">上传自己喜欢的图片作为兴趣组图标</span></div>
                    <div class="fb_box_com w600">
                        <div class="step_01_l">
                            <span class="step_pic">
                                <img src="/pics/step_bg.gif" height="148" width="148" id="imgInterestIconPreview"></span>
                            <fieldset id="fsUploadProgress" style="display: none;">
                            </fieldset>
                            <div id="divStatus">
                            </div>
                            <span class="step_btn">
                                <div id="upload-btn">
                                    <span id="btn_holder"></span>
                                </div>
                            </span>
                        </div>
                        <div class="step_01_r">
                            <span class="uploadmsg">图片上传后显示在这里，选择的图片要比这个大jpg、gif、png或bmp格式，单张图片不超过8MB</span></div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="two_step">
                    <div class="fb_box_com w600">
                        <span class="step_02 step">上传自己喜欢的图片作为兴趣组个性图片</span></div>
                    <p class="step-cnt">
                        <img class="placeholder" src="/pics/step_bg2.gif" id="imgInterestSelfhoodPicturePreview" width="450" height="141" /></p>
                        <p class="tipText">图片上传后显示在这里，选择的图片宽应大于等于800像素，高应大于等于250像素，可以是jpg、gif、png或bmp格式，单张图片不超过8MB</p>
                        <form action="/Shared/UploadTempCustomPhoto" method="post" enctype="multipart/form-data" id="frmInterestSelfhoodPicture" name="frmInterestSelfhoodPicture">
                        <input type="hidden" id="interestSelfhoodPictureId" name="interestSelfhoodPictureId" value="" />
                        <input type="hidden" id="interestSelfhoodPicture" name="interestSelfhoodPicture" value="" />
                        <input type="hidden" id="pictureType" name="pictureType" value="31" />
                    <div class="buttonforup reg_btn btn link_uploat2">
                        <div class="button">
                            <input type="file" name="uploadPhoto" id="uploadPhoto" value="上传" onclick="uploadPersonalityPicture()"
                                onchange="changePersonalityPicture()" size="1" />
                        </div>
                    </div>
                    </form>
                </div>
                <div class="clearfix"></div>
                <div class="third_step" style="display: none">
                    <div class="fb_box_com w600">
                        <span class="step_03 step">给大家介绍一下</span></div>
                    <div class="fb_box_com w600">
                        <div class="from_add">
                            <span>
                                <input type="text" name="txtTitle" id="txtTitle" value="" class="txtput" /></span>
                            <span>
                                <textarea name="txtContent" id="txtContent" class="textarea3" style="width: 350px;
                                    height: 80px;"></textarea></span> <span>
                                        <input type="text" name="txtClasses" id="txtClasses" value="" class="txtput select_xq" />
                                        <font class="cgray">（用逗号分隔标签）</font></span> <span class="taget_list">
                                            <% foreach (var obj in Model.interestClassListObje)
                                               { %><nobr><a href="javascript:void(0);" onclick="getInterestClassVal('<%= obj.Title %>')"><%= obj.Title %></a></nobr><% } %>
                                        </span><span class="create_btn"><a class="redlink" onclick="addInterest()">创 建</a></span>
                        </div>
                    </div>
                </div>
                <div class="padding_b50">
                </div>
            </div>
        </div>
        <%Html.RenderAction("AddRightPanel", "Content", new { contentObj = "" }); %>
    </div>
    <%
  } %>
    <noscript>
        <div style="display: none;">
            您的浏览器不支持javascript，不能使用此页面的全部功能。请换用其他浏览器或者开启对javascript的支持。</div>
    </noscript>
    <div id="noflash" style="display: none;">
        您没有安装flash播放器，或者您的flash版本不够，无法使用上传功能. 请安装最新版本的flashplayer.( <a href="http://get.adobe.com/cn/flashplayer/">
            官方下载</a> )</div>
    <!--上传图片结束-->
    <form id="frmInterest" name="frmInterest" action="" method="post" enctype="multipart/form-data">
    <input type="hidden" value="" id="uploadFiles" name="uploadFiles" />
    <input type="hidden" value="" id="uploadFileNames" name="uploadFileNames" />
    <input type="hidden" id="createdtppoints" name="createdtppoints" value="<%=ViewData["nowCreatedInterestToPoints"].ToString() %>" />
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css"/>
<link rel="stylesheet" type="text/css" href="/css/fabu.css"/>
<style type="text/css">
    .fb_box_com span.add_xiqu img, .border_b1 img { width:51px; height:51px; display:block; padding-right:10px; float:left;}
    .fb_box_com span.add_xiqu a{ display:block; float:left; width:350px; line-height:25px;}
    .fb_box_com span.xiqu_t { line-height:28px; text-align:left; font-family:"Adobe 黑体 Std,黑体"; color:#333; font-size:16px;}
    .fb_box_com span.step{ width:440px; height:35px; display:block; float:left; padding-left:55px; line-height:35px; color:#666; font-size:14px; text-align:left;}
    .fb_box_com span.step_01 { background:url(/pics/step_01.gif) 0 0 no-repeat;}
    .fb_box_com span.step_02 { background:url(/pics/step_02.gif) 0 0 no-repeat;}
    .fb_box_com span.step_03 { background:url(/pics/step_03.gif) 0 0 no-repeat;}
    .fb_box_com .step_01_l span{ display:block;}
    .fb_box_com .step_01_l { width:150px; height:200px; float:left; padding:15px 20px 15px 30px;}
    .fb_box_com .step_01_r { width:280px; height:60px; float:left; padding-top:120px; text-align:left; line-height:25px; color:#ccc; }
    .fb_box_com .step_01_l span.step_pic { width:148px; height:148px; border:1px solid #dedede;}
    .two_step .step-cnt { width:450px; height:141px; border:1px solid #dedede; }
    .two_step .tipText{ width:450px; height:35px; text-align:left; line-height:20px; color:#ccc; margin-left:30px; margin-top:10px; }
    .fb_box_com .step_01_l span.step_btn{ width:100px; margin:0 auto; height:35px; padding-top:15px;}
    .fb_box_com .from_add { margin-top:20px;}
    .fb_box_com .from_add span { display:block; padding-bottom:15px; padding-left:50px; width:454px; overflow:hidden;}
    .from_add span.taget_list a{ text-align:left; color:#999; line-height:25px; display:block; float:left; padding:0 10px; }
    .from_add span.create_btn a.redlink { color:#fff; font-size:16px; font-weight:600; float:left; width:100px; background:#b40001; line-height:32px; text-align:center; border-bottom:#9e0203 3px solid; border-right:#9e0203 3px solid;}
    .from_add span.Prese_btn a {  width:80px; font-family:"\5FAE\8F6F\96C5\9ED1";  text-align:center; line-height:28px; margin-right:10px; display:block; font-size:14px; font-weight:600;  }
    .from_add span.Prese_btn a.graylink{ color:#979797; float:left;  background:#e7e7e7; border-bottom:#dedee0 3px solid; border-right:#dedee0 3px solid;}
    .from_add span.Prese_btn a.redlink{ color:#fff; float:left;  background:#b40001;  border-bottom:#9e0203 3px solid; border-right:#9e0203 3px solid;}
    .fb_box_com .from_add input.txtput {
	    width: 200px; height: 30px; line-height: 30px;  _background:#fff; margin-right:20px; padding:3px 5px 0 5px;   background:#fff url(/pics/border_bg01.png) 0 0 no-repeat;  color:#666; font-family:"\5FAE\8F6F\96C5\9ED1"; font-size:14px; border: 1px solid #d4d4d4; }
    .fb_box_com .xq_list { }
    div.third_step{margin-top:30px;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/scripts/jquery.jBox-2.3.min.js"></script>
    <script type="text/javascript" src="/scripts/jquery.jBox-zh-CN.js"></script>
    <script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.validate.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/up/swfupload_<%=Model.JsVersion %>.js"></script>
    <script type="text/javascript" src="/js/up/swfupload.swfobject_<%=Model.JsVersion %>.js"></script>
    <script type="text/javascript" src="/js/up/swfupload.queue_<%=Model.JsVersion %>.js"></script>
    <script type="text/javascript" src="/js/up/photoprogress_<%=Model.JsVersion %>.js"></script>
    <script type="text/javascript" src="/js/up/photo_upload_regupface_<%=Model.JsVersion %>.js"></script>
    <script src="/scripts/jquery.form.js" type="text/javascript"></script>
    <!--[if IE 6]>
    <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('em,.txtput,textarea3');
	 </script>
<![endif]-->
    <script language="javascript" type="text/javascript">
        var settings;
        $().ready(function () {
            $("#wrap").css("background", "#DEDEE0");
            $("input#uploadPhoto").css({ opacity: 0 });
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
                    "photoType": "30"
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
                button_width: 101,
                button_height: 35,
                button_image_url: '/pics/load_pic_pz.png',
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
        });
        function uploadfinished() {
            $('.uploadmsg').css({ "display": "none" });
            $('div.two_step').css({ "display": "block" });
            var filenames = $("#uploadFileNames").val();
            if (filenames.length < 1) { return; }
            var filenamelist = filenames.split('|');
            var filename = filenamelist[filenamelist.length - 1];
            $("#imgInterestIconPreview").attr("src", photofunctions.getprofileiconphotoname(filename));
        }
        function frmInterestValidate() {
            $("#frmInterest").validate({
                rules: {
                    txtTitle: { required: true, maxlength: 25 },
                    txtContent: { required: true, maxlength: 180 },
                    txtClasses: { required: true, maxlength: 40 }
                },
                messages: {
                    txtTitle: { required: "必须输入名称", maxlength: "名称最多只允许输入25个字符" },
                    txtContent: { required: "必须输入介绍", maxlength: "介绍最多只允许输入180个字符" },
                    txtClasses: { required: "必须输入标签", maxlength: "标签最多只允许输入40个字符" }
                }
            });
        }
        function addInterest() {
            var frmInterest = $("#frmInterest");
            if (frmInterest.valid()) {
                var fileids = $("#uploadFiles").val();
                if (fileids.length < 1) { $.jBox.tip("请先上传兴趣图标", 'error'); return; }
                var createdtppoints = $("#createdtppoints").val();
                $.jBox.confirm("此次创建兴趣需要" + createdtppoints + "米果，确定添加吗？", "确认提示", function (data) {
                    if (data == "ok") {
                        interestCenterProvider.addInterest(
                    $("#txtTitle").val(),
                    $("#txtContent").val(),
                    $("#txtClasses").val(),
                    $("#uploadFiles").val(),
                    $("input#interestSelfhoodPictureId").val(),
                    $("input#interestSelfhoodPicture").val(),
                    function (data) {
                        var objs = $.parseJSON(data);
                        if (objs.Title != "") {

                            //添加说说内容
                            var permissions = $("#permissions").val();
                            var interestIDs = $("#interestIDs").val();
                            interestIDs += "," + objs.ID;
                            if (interestIDs == "") {
                                $.jBox.tip("请至少至少选择一个兴趣", 'error');
                            }
                            else {
                                ContentProvider.InsertInterestContent(permissions, interestIDs, "", objs.ID, "创建兴趣组", function (data) {
                                    var data = $.parseJSON(data);
                                    var url = "http://www.moooyo.com/InterestCenter/ShowInterest/" + objs.ID;
                                    //分享到微博
                                    var iffxwb = ShareToWB(objs.Title + " " + objs.Content, url);
                                    $.jBox.tip("发布成功", 'info');
                                    $("#txtTitle").val("");
                                    $("#txtContent").val("");
                                    $("#txtClasses").val("");
                                    $("#uploadFiles").val("");
                                    window.location = "/InterestCenter/AddInterestFans";
                                });
                            }
                        }
                        else {
                            $.jBox.tip("兴趣添加失败，系统维护中，给您带来了不便，请谅解！", 'error');
                            location.reload();
                        }
                    });
                    }
                });
            }
        }
        function getInterestClassVal(val) {
            var txtClasses = $("#txtClasses");
            var txtClassesVal = $("#txtClasses").val();
            var txtClassesValIndex = txtClassesVal.indexOf(val);
            var txtClassesValIndex2 = txtClassesVal.indexOf("," + val);
            if (txtClassesVal != "") {
                if (txtClassesValIndex >= 0) {
                    if (txtClassesValIndex2 >= 0) {
                        txtClasses.val(txtClassesVal.replace("," + val, ""));
                    }
                    else {
                        if (txtClassesVal.length == val.length)
                            txtClasses.val(txtClassesVal.replace(val, ""));
                        else
                            txtClasses.val(txtClassesVal.replace(val + ",", ""));
                    }
                    return false;
                }
                txtClasses.val(txtClasses.val() + ',' + val);
                return false;
            }
            txtClasses.val(val);
        }
        function uploadPersonalityPicture() {
            var imgSrc = $("#uploadPhoto").val();
            if (imgSrc != "" && imgSrc != null && imgSrc != undefined) {
                if (!imgSrc.match(/.jpg|.gif|.png/i)) {
                    $.jBox.tip("图片类型必须是gif, jpg, png中的一种哦！", "err");
                    return false;
                }
                $('#frmInterestSelfhoodPicture').ajaxSubmit(function (data) {
                    if ((data != null) && (data != "") && (data != -1)) {
                        var dataArr = data.split('|');
                        if (dataArr[0] != "" && dataArr[1].toString() == "SUCCESS") {
                            actionprovider.openCustomBigPicture('', dataArr[0], 31, "", 3, 800, 250, '');
                        }
                        else $.jBox.tip(dataArr[1], "err");
                    }
                    else $.jBox.tip("系统维护中，给您带来了不便，请谅解！", "err");
                });

                var timeoutId = setTimeout(function () {
                    $("div.buttonforup div.button").html("<input type=\"file\" name=\"uploadPhoto\" id=\"uploadPhoto\" value=\"上传\" onclick=\"uploadPersonalityPicture()\" onchange=\"changePersonalityPicture()\" size=\"1\"/>");
                    $("input#uploadPhoto").css({ opacity: 0 });
                }, 50
                    );
            }
        }
        function changePersonalityPicture() {
            var uploadPhoto = $("#uploadPhoto");
            if (uploadPhoto.val() != "") {
                uploadPhoto.unbind("onchange");
                uploadPhoto.removeAttr("onchange");
                uploadPhoto.change(function () { });
                uploadPersonalityPicture();
            }
        }
        function trimmedPictureAfter(pictrueId, pictruePath) {
            if (pictruePath != null && pictruePath != "") {
                $("input#interestSelfhoodPicture").val(pictruePath);
                $("input#interestSelfhoodPictureId").val(pictrueId);
                $("img#imgInterestSelfhoodPicturePreview").attr("src", photofunctions.getnormalphotoname(pictruePath));
                $('div.third_step').css({ "display": "block" });
            }
        }
    </script>
</asp:Content>
