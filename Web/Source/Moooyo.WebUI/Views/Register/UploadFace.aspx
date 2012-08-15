<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/IndexEnter.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    
    <%--<link rel="stylesheet" href="/css/reg_<%=ViewData["cssversion"] %>.css" type="text/css" media="screen"/>--%>

    <link rel="stylesheet" href="/css/jquery.Jcrop.css" type="text/css" media="screen"/>

   <%-- <link href="/css/reg_upload_photo_<%=ViewData["cssversion"] %>.css" rel="stylesheet" type="text/css" />--%>
    <style type="text/css">
        
        
    /*.photoUpload{float:left;width:590px;padding-right:40px}
    .split5px{padding-top:5px}
    .hidden{display:none}
    .photobuttons{color:#074945;text-align:right;font-size:12px;margin:0 10px;text-decoration:none;font-family:Arial;}
    #photobuttons a {color:#074945;text-decoration:none;}
    #photobuttons a:hover{color:#a90303;text-decoration:none;}*/
    
    .buttonforup{width:90px; height:30px; cursor:auto; font-size:15px;}
    .buttonforup .button{width:70px; height:20px; line-height:21px; margin:5px 10px; text-align:center; overflow:hidden; cursor:pointer; position:relative;}
    #uploadPhoto{width:70px; cursor:pointer; position:absolute; left:0px; top:0px;}
    
    </style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	上传头像
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="c976 clearfix">
    <div class="h100"></div>
    <div style=" height:380px; margin:0 250px 100px 250px;">
    	<div style="height:200px; width:200px; float:left; margin-right:50px; _margin-right:25px;">
        <img src="/pics/defultpic.png" alt="头像" title="头像" id="imgRegisterMemberFacePreview" width="150" height="150" border="0" />
        </div>

        <div style="height:200px; width:220px; float:left; ">
        	<div style="height:40px; line-height:20px; margin-top:15px;">
            	支持gif、jpg、png，文件不能大于5M<br/>
                真实的头像照将大大的提高您的关注度
            </div>
            <div style=" margin-top:20px;">

                <%--<div id="upload-btn"><a style="width:195px; height:34px; line-height:36px; text-align:center; font-size:20px; font-family:Tahoma,Arial; margin-top:10px;" href="javascript:;" onclick="actionprovider.openCustomSmallPicture('/Register/RegAddInterest')" class="radius3 btn fl" id="upload">上传头像</a></div>--%>

                <div style="clear:both;">
                <form action="/Shared/UploadTempCustomPhoto" method="post" enctype="multipart/form-data" id="UploadTempCustomPhotoForm" name="UploadTempCustomPhotoForm" class="fl">
                    <div class="buttonforup btn">
                        <div class="button">
                        上传头像
                    <input type="file" name="uploadPhoto" id="uploadPhoto" value="上传" onclick="getImgSrc()" onchange="resetimageFileInput()" size="1"/>
                        </div>
                    </div>
                </form>
                </div>
                <br /><br />
                <div class="right-btn-huiyuan hidden" id="uploadStartArea" style="clear:both;"><a style="width:195px; height:34px; line-height:36px; text-align:center; font-size:20px; font-family:Tahoma,Arial; margin-top:10px;" href="/Register/RegAddInterest" class="radius3 btn fl" id="upload-start">完&nbsp;&nbsp;成</a></div>
                <div class="clearfix" style="margin:0px; padding:0px; height:1px; line-height:1px;"></div>
                <div style="clear:both;">
                <a href="/Register/Become?pt=<%=ViewData["photoType"] %>" class="noup pt10">暂时不上传</a>
                </div>
            </div>
        </div>
        <%--<form action="/Member/I" method="post" id="uploadMemberFace">
        <input type="hidden" value="" id="uploadFileNames" name="uploadFileNames" />
        <input type="hidden" value="" id="uploadFiles" name="pid" />
        <div style="height:200px; width:220px; float:left; ">
        	<div style="height:40px; line-height:20px; margin-top:15px;">
            	支持gif、jpg、png，文件不能大于5M<br/>
                真实的头像照将大大的提供您的关注度
            </div>
            <div style=" margin-top:50px;">
            <fieldset class="flash hidden" id="fsUploadProgress"></fieldset>
                


                <div id="upload-btn"><span id="btn_holder" class="up"></span></div>
                <div class="right-btn-huiyuan hidden" id="uploadStartArea"><a style="width:195px; height:34px; line-height:36px; text-align:center; font-size:20px; font-family:Tahoma,Arial; margin-top:10px;" href="/Register/RegAddInterest" class="radius3 btn fl" id="upload-start">完&nbsp;&nbsp;成</a></div>
                <a href="/Register/Become?pt=<%=ViewData["photoType"] %>" class="noup">暂时不上传</a>
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
        </form>--%>



    </div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/scripts/jquery.form.js" type="text/javascript"></script>



<%--<script type="text/javascript" src="/js/up/swfupload_<%=ViewData["jsversion"] %>.js"></script>
<script type="text/javascript" src="/js/up/swfupload.swfobject_<%=ViewData["jsversion"] %>.js"></script>
<script type="text/javascript" src="/js/up/swfupload.queue_<%=ViewData["jsversion"] %>.js"></script>
<script type="text/javascript" src="/js/up/reg_photoprogress_<%=ViewData["jsversion"] %>.js"></script>
<script type="text/javascript" src="/js/up/photo_upload_regupface_<%=ViewData["jsversion"] %>.js"></script>--%>


<script type="text/javascript" language="javascript">
    //*** 禁止浏览器后退 *****************
    //禁用后退按钮
    window.history.forward(1);
    //禁用后退键，作用于Firefox、Opera  
    document.onkeypress = banBackSpace;
    //禁用后退键，作用于IE、Chrome  
    document.onkeydown = banBackSpace;
    //************************************ 
//    var settings;
    var uploadpath = "<%=ViewData["uploadpath"] %>";
    function resetimageFileInput(object) {
        var uploadPhoto=$("#uploadPhoto");
        if(uploadPhoto.val()!=""){
            uploadPhoto.unbind("onchange");
            uploadPhoto.removeAttr("onchange");
            uploadPhoto.change(function(){});
            getImgSrc();
        }
    }
    $().ready(function () {
        $("#uploadPhoto").css({opacity:0});
//        settings = {
//            debug: false,
//            flash_url: '/swf/swfupload.swf',
//            file_post_name: 'file',
//            upload_url: '/up/AddPhotoAndSetIcon',
//            post_params: {
//                "ASPSESSID" : "<%--<%=Session.SessionID %>--%>",
//                "AUTHID" : "<%--<%=Request.Cookies[FormsAuthentication.FormsCookieName].Value%>--%>",
//                "photoType": '<%--<%=ViewData["phototype"] %>--%>'
//            },
//            file_size_limit: '5 MB',
//            file_types: '*.jpg; *.jpeg; *.gif; *.bmp; *.png;',
//            file_types_description: 'Picture Files',
//            file_upload_limit: 20,
//            file_queue_limit: 1,
//            custom_settings: {
//                progressTarget: 'fsUploadProgress',
//                cancelButtonId: 'btnCancel',
//                startButtonId: 'btnStart',
//                jUrl: '/up/'
//            },
//            button_placeholder_id: 'btn_holder',
//            button_width: 213,
//            button_height: 40,
//            button_image_url: '/pics/memberface1.jpg',

//            file_queued_handler: fileQueued,
//            file_queue_error_handler: fileQueueError,
//            file_dialog_complete_handler: fileDialogComplete,
//            upload_start_handler: uploadStart,
//            upload_progress_handler: uploadProgress,
//            upload_error_handler: uploadError,
//            upload_success_handler: uploadSuccess,
//            upload_complete_handler: uploadComplete,
//            queue_complete_handler: queueComplete,

//            inimum_flash_version: '9.0.28',
//            swfupload_pre_load_handler: preload,
//            swfupload_load_failed_handler: swfuploadLoadFailed
//        },

//    oUploadBtn = $('#upload-btn'),
//    oUploadStatus = $('#upload-status'),
//    oStartUpload = $('#upload-start'),
//    oBtnCancel = $('#btnCancel'),
//    oOptBtns = $('.opt-btns'),
//    TMPL_UPLOAD_TIPS = '提示：每次最多可以<b>批量上传</b>二十张照片，按着&nbsp;“{CMD}”&nbsp;键可以一次选择多张照片';

//        swfup = new SWFUpload(settings);

//        $('#uploadFiles').val('');

//        // start upload
//        oStartUpload.click(function () {
//            window.location="/Register/RegAddInterest";
//        });
    });
    function getImgSrc() {
        var imgSrc = $("#uploadPhoto").val();
        if (imgSrc != "" && imgSrc != null && imgSrc != undefined) {
            if (!imgSrc.match(/.jpg|.gif|.png/i)) {
                $.jBox.tip("图片类型必须是gif,jpg,png中的一种哦！", "err");
                return false;
            }
            $('#UploadTempCustomPhotoForm').ajaxSubmit(function (data) {
                if ((data != null) && (data != "") && (data != -1)) {
                    var dataArr = data.split('|');
                    if(dataArr[0] != "" && dataArr[1].toString() == "SUCCESS") {
                        actionprovider.openCustomSmallPicture('/Register/RegAddInterest', dataArr[0]);
                    }
                    else $.jBox.tip(dataArr[1], "err");
                }
                else $.jBox.tip("系统维护中，给您带来了不便，请谅解！", "err");
            });

            var timeoutId = setTimeout(function () {
//                        $("#uploadPhoto").select();
//                        document.execCommand("Delete");
//                        document.getElementById("uploadPhoto").value= "";

                            $("div.buttonforup div.button").html("上传头像<input type=\"file\" name=\"uploadPhoto\" id=\"uploadPhoto\" value=\"上传\" onclick=\"uploadPersonalityPicture()\" onchange=\"changePersonalityPicture()\" size=\"1\"/>");
                            $("input#uploadPhoto").css({opacity:0});
                        }, 50
                    );
        }
    }
//    function uploadfinished() {
//        var filenames = $("#uploadFileNames").val();
//        if (filenames.length < 1) {
//            return;
//        }
//        var filenamelist = filenames.split('|');
//        for (var i = 0; i < filenamelist.length; i++) {
//            var filename = filenamelist[i];
//            if (filename != "") {
//                $("#imgRegisterMemberFacePreview").attr("src", photofunctions.getprofileiconphotoname(filename));
//            }
//        }
//        $("#uploadStartArea").removeClass("hidden");
//    }
</script>
</asp:Content>