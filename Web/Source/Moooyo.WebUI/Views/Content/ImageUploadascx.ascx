<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.AddContentModel>" %>
<link href="/css/upload-photo_<%=Model.CssVersion %>.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .uploadpic li{padding-bottom:10px;}
    .fb_box_com .title_r{width:auto; margin-right:7px;}
</style>
<%
    Boolean ifshowup = Boolean.Parse(ViewData["ifShowUp"].ToString());
    String contenttitle = ViewData["contentTitle"].ToString();
     %>
<form action="/photo/mplist/<%=Model.UserID %>" method="post" id="upload">
    <div id="uploadPhotoStep1">
        <div id="imageup" data-ifopen="false" <%=!ifshowup?"style=\"display:block;\"":"style=\"display:none;\"" %>>
	        <div class="fb_box_com w600 ">
                <div class="upload-list hidden"><fieldset class="flash" id="fsUploadProgress"></fieldset></div>
                <div style="width:100px; height:10px; border:none; margin:0px; padding:0px; background-color:none;">&nbsp;</div>
		        <div class="pic_upload"><div id="upload-btn" style="background:#ebebeb; background-image:url('/pics/addLoad_pz.png'); width:100px; height:35px;"><span id="btn_holder"></span></div>可多选，jpg、gif、png或bmp格式，单张图片不超过8MB。</div>
	        </div>
	        <div class="fb_box_com w600 " id="imagelayout"></div>
	        <div class="fb_box_com w600 ">
                <div id="uploadPhotoStep2" class="uploadpic">
                    <ul id="photoInfoList"></ul>
                </div>
            </div>
        </div>
		<div class="fb_box_com w600">
            <span class="title_l"><%=contenttitle%></span>
            <%if (ifshowup)
              { %>
            <a class="title_r" href="#" onclick="showuploadimage($(this))">+ 添加图片</a>
            <%} %>
        </div>
        <%if (Model.contentObj != null)
          {
              string imageids = "";
              string imagefiles = "";
              string imagefilennames = "";
              string imagelayouttype = "";
              switch (Model.contentObj.ContentType)
              {
                  case Moooyo.BiZ.Content.ContentType.Image:
                      Moooyo.BiZ.Content.ImageContent imagecontent = (Moooyo.BiZ.Content.ImageContent)Model.contentObj;
                      foreach (var image in imagecontent.ImageList)
                      {
                          Moooyo.BiZ.Photo.Photo photo = Moooyo.BiZ.Photo.PhotoManager.GetNamePhoto(image.ImageUrl);
                          string id = photo != null ? photo.ID : "";
                          imageids += id + ",";
                          imagefiles += id + "|";
                          imagefilennames += image.ImageUrl + "|";
                      }
                      imagelayouttype = imagecontent.LayOutType.ToString();
                      break;
                  case Moooyo.BiZ.Content.ContentType.SuiSuiNian:
                      Moooyo.BiZ.Content.SuiSuiNianContent shuoshuo = (Moooyo.BiZ.Content.SuiSuiNianContent)Model.contentObj;
                      foreach (var image in shuoshuo.ImageList)
                      {
                          Moooyo.BiZ.Photo.Photo photo = Moooyo.BiZ.Photo.PhotoManager.GetNamePhoto(image.ImageUrl);
                          string id = photo != null ? photo.ID : "";
                          imageids += id + ",";
                          imagefiles += id + "|";
                          imagefilennames += image.ImageUrl + "|";
                      }
                      imagelayouttype = shuoshuo.LayOutType.ToString();
                      break;
                  case Moooyo.BiZ.Content.ContentType.CallFor:
                      Moooyo.BiZ.Content.CallForContent callfor = (Moooyo.BiZ.Content.CallForContent)Model.contentObj;
                      foreach (var image in callfor.ImageList)
                      {
                          Moooyo.BiZ.Photo.Photo photo = Moooyo.BiZ.Photo.PhotoManager.GetNamePhoto(image.ImageUrl);
                          string id = photo != null ? photo.ID : "";
                          imageids += id + ",";
                          imagefiles += id + "|";
                          imagefilennames += image.ImageUrl + ",";
                      }
                      imagelayouttype = callfor.LayOutType.ToString();
                      break;
              }
              imageids = imageids != "" ? imageids.Substring(0, imageids.Length - 1) : imageids;
              imagefiles = imagefiles != "" ? imagefiles.Substring(0, imagefiles.Length - 1) : imagefiles;
              imagefilennames = imagefilennames != "" ? imagefilennames.Substring(0, imagefilennames.Length - 1) : imagefilennames;
              %>
        <input type="hidden" id="imageIDs" name="imageIDs" value="<%=imageids %>" />
        <input type="hidden" id="uploadFiles" name="uploadFiles" value="<%=imagefiles %>" />
        <input type="hidden" id="uploadFileNames" name="uploadFileNames" value="<%=imagefilennames %>" />
        <input type="hidden" id="imageLayOutType" name="imageLayOutType" value="<%=imagelayouttype %>"/>
        <input type="hidden" id="filename" name="filename" value=""/>
              <%
          }
          else
          {
              %>
        <input type="hidden" id="imageIDs" name="imageIDs" />
        <input type="hidden" id="uploadFiles" name="uploadFiles" value="" />
        <input type="hidden" id="uploadFileNames" name="uploadFileNames" value="" />
        <input type="hidden" id="imageLayOutType" name="imageLayOutType" value=""/>
        <input type="hidden" id="filename" name="filename" value=""/>
              <%
          } %>
    </div> 
</form>
<div id="noflash" class="attn hidden">
    您没有安装flash播放器，或者您的flash版本不够，无法使用上传功能. 请安装最新版本的flashplayer.( <a href="http://get.adobe.com/cn/flashplayer/">官方下载</a> )
</div>
<div id="old-portal"></div>

<script type="text/javascript" src="/js/up/swfupload_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/up/swfupload.swfobject_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/up/swfupload.queue_<%=Model.JsVersion %>.js"></script>
<script src="/js/ImageLayoutFrameGroup.js" type="text/javascript"></script>
<script src="/Scripts/json2.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
//    //禁止浏览器后退
//    //禁用后退按钮
//    window.history.forward(1);
//    //禁用后退键，作用于Firefox、Opera  
//    document.onkeypress = banBackSpace;
//    //禁用后退键，作用于IE、Chrome  
//    document.onkeydown = banBackSpace;

    uploadpath = '<%=Model.UploadPath %>';
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
                startButtonId: 'btnStart',
                jUrl: '/up/'
            },
            button_placeholder_id: 'btn_holder',
            button_width: 100,
            button_height: 35,

            file_queued_handler: fileQueued,
            file_queue_error_handler: fileQueueError,
            upload_start_handler: uploadStart,
            upload_progress_handler: uploadProgress,
            upload_error_handler: uploadError,
            upload_success_handler: uploadSuccess,
            queue_complete_handler: queueComplete,

            inimum_flash_version: '9.0.28',
            swfupload_pre_load_handler: preload,
            swfupload_load_failed_handler: swfuploadLoadFailed
        },

        oUploadBtn = $('#upload-btn'),
        oUploadStatus = $('#upload-status'),
        swfup = new SWFUpload(settings);
        oUploadBtn.show();
        $("object.swfupload").css({ opacity: 0 });
        loadupdate();
    });
</script>
<script type="text/javascript">
    var Moooyo = {};
    var errdetail = $.extend(Moooyo.errdetail, { 20: "抱歉，图片短边尺寸不能小于400像素", 21: "信息不全，请刷新页面重试。" });
    var allFilesUploaded = 0, allErrorFiles = 0, uploadStarted = false, totalNum = 0, totalSize = 0, idsFiled, ids = "";
    function loadupdate() {
        if ($("#updateContent").val() != null) {
            imageidlist = $("#imageIDs").val().split(',');
            filenamelist = $("#uploadFileNames").val().split('|');
            imageLayOutType = $("#imageLayOutType").val();
            var str = "";
            for (var i = 0; i < imageidlist.length; i++) {
                str += "<li id=\"uploadimageshow" + imageidlist[i] + "\" data-id=\"" + imageidlist[i] + "\"><img src=\"" + photofunctions.getprofileiconphotoname(filenamelist[i]) + "\" width=\"134\" height=\"134\" title=\"图片\" /></li>";
            }
            $("#photoInfoList").html($("#photoInfoList").html() + str);
            imagedelmover();
            //设置计数器，加载布局类型
            var counttimeout = setTimeout(function () {
                allFilesUploaded = imageidlist.length;
                var a = swfup.getStats();
                a.successful_uploads = imageidlist.length;
                swfup.setStats(a);
                loadimagelayout(imageLayOutType);
            }, 500);
        }
    }
    //页面级清楚所有当前上传的图片
    function closeimageuploat() {
        $("#uploadFileNames").val("");
        $("#uploadFiles").val("");
        $("#imageIDs").val("");
        $("#imageLayOutType").val("");
        allFilesUploaded = 0;
        var a = swfup.getStats();
        a.successful_uploads = 0;
        swfup.setStats(a);
        $("#photoInfoList").children().remove();
        $("#imagelayout").html("");
    }
    //打开或关闭图片上传
    function showuploadimage(obj) {
        var ifopen = $("#imageup").attr("data-ifopen");
        if (ifopen == "false") {
            $("#imageup").attr("data-ifopen", "true");
            $("#imageup").show(500);
            obj.html("");
        }
    }
    //加载图片布局选择
    function loadimagelayout(type) {
        var layoutstr = "";
        var swfupobject = swfup.getStats();
        if (swfupobject.successful_uploads > 1) {
            var imagelength = 0;
            if (swfupobject.successful_uploads >= 5) {
                imagelength = 5;
            }
            else {
                imagelength = swfupobject.successful_uploads;
            }
            layoutstr = getImageLayoutFrameHtml(imagelength);
            $("#imagelayout").html(layoutstr);
            if (type != null) {
                $("#imagelayout [data-layouttype='" + type + "']").click();
            }
            else {
                $("#imagelayout .Framediv:eq(0)").click();
            }
        }
        else {
            if (swfupobject.successful_uploads == 1) {
                $("#imageLayOutType").val("one_one");
            }
            else {
                $("#imageLayOutType").val("");
            }
            $("#imagelayout").html("");
        }
    }
    //删除图片
    function deletePhoto(id, obj) {
        photoprovider.delphoto(id, function (data) {
            if (data.ok) {
                obj.animate({ height: 0, width: 0 }, 500, function () { obj.remove(); if ($("#deleteImage").html() != null) { $("#deleteImage").remove(); } });
                var filenamelist = $("#uploadFileNames").val().split('|');
                var fileidlist = $("#uploadFiles").val().split('|');
                var idlist = $("#imageIDs").val().split(',');
                var filenamestr = "";
                var fileidstr = "";
                var idstr = "";
                for (var i = 0; i < fileidlist.length; i++) {
                    if (fileidlist[i] != id) {
                        filenamestr += filenamelist[i] + "|";
                        fileidstr += fileidlist[i] + "|";
                        idstr += idlist[i] + ",";
                    }
                }
                $("#uploadFileNames").val(filenamestr.substr(0, filenamestr.length - 1));
                $("#uploadFiles").val(fileidstr.substr(0, fileidstr.length - 1));
                $("#imageIDs").val(idstr.substr(0, idstr.length - 1));

                decrease_upload_count(swfup);
                loadimagelayout(null);
            }
            else {
                $.jBox.tip("图片删除失败，系统维护中，给您带来了不便，请谅解！", 'error');
            }
        });
    }
    //加入队列
    function fileQueued(c) {
        var a = this, b = new FileProgress(c, a.customSettings.progressTarget);
        b.toggleCancel(true, a);
        if (!uploadStarted) {
            $(".upload-list, #upload-status").removeClass("hidden"); 
            $("#upload-status .num b").text(c.index + 1);
            $(".progressBarInProgress").addClass("hidden");
            if ($(".progressWrapper").length === 20) {
                $(".continue-wrapper").addClass("hidden")
            }
        }
        swfup.startUpload();
    }
    //未安装flash时调用该方法提示用户
    function swfuploadLoadFailed() { $("#noflash").show(); $("#old-portal").show(); }
    function preload() { }
    function debug_message(d, a, b, c) {
        d.debug("Error Code: " + a + ", File name: " + b.name + ", File size: " + b.size + ", Message: " + c)
    }
    //计数器减少一个
    function decrease_upload_count(b) {
        allFilesUploaded--;
        var a = b.getStats();
        a.successful_uploads--;
        b.setStats(a);
    }
    function fileQueueError(c, e, d) {
        var a = this;
        if (e === SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED) {
            alert("选择的文件太多,\n" + (d === 0 ? "达到了上传的上限" : "最多只能选择10个文件。"));
            return
        }
        var b = new FileProgress(c, this.customSettings.progressTarget);
        b.setError();
        b.toggleCancel(true, a);
        switch (e) {
            case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                b.setStatus(errdetail[2]);
                $.jBox.tip("文件大小不能超过" + this.settings.file_size_limit, 'info');
                break;
            case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                b.setStatus("不能上传大小为0的文件。");
                $.jBox.tip("不能上传大小为0的文件", 'info');
                break;
            case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
                b.setStatus(errdetail[12]);
                debug_message(this, "Invalid file type", c, d);
                $.jBox.tip("无效的文件类型", 'info');
                break;
            default:
                if (c !== null) { b.setStatus("错误") }
                debug_message(this, e, c, d);
                $.jBox.tip("系统维护中", 'info');
                break;
        }
    }
    //开始上传
    function uploadStart(b) {
        var a = new FileProgress(b, this.customSettings.progressTarget);
        a.toggleCancel(true, this);
        return true;
    }
    //加载上传时的进度条
    function uploadProgress(b, e, d) {
        var c = (e / d) * 250;
        var a = new FileProgress(b, this.customSettings.progressTarget);
        a.setProgress(c)
    }

    function uploadSuccess(e, c, responseReceived) {
        var b = new FileProgress(e, this.customSettings.progressTarget), d = $.parseJSON(c), a = this;
        if (d.error) {
            b.setError();
            b.setStatus(errdetail[d.error]);
            allErrorFiles++;
            console.log("uploadSuccess" + allErrorFiles);
            b.toggleCancel(true, a, function () { b.disappear(); decrease_upload_count(a) });
            return false
        }
        else {
            b.setComplete();
            b.setStatus("成功");
            b.removeDelIcon(d.id);
            e.post.photo_id = d.id;
            //2012-1-30 增加文件名的获取 by刘安
            imageIDs = $("#imageIDs");
            idlist = imageIDs.val() + "," + d.ID;
            imageIDs.val(idlist);
            idsFiled = $("#uploadFiles");
            ids = idsFiled.val() + "|" + d.ID;
            idsFiled.val(ids);
            idsFileNames = $("#uploadFileNames");
            filenames = idsFileNames.val() + "|" + d.FileName;
            idsFileNames.val(filenames);
            filename = $("#filename");
            
            var str = "";
            var filenames = $("#uploadFileNames").val();
            var fileIds = $("#uploadFiles").val();
            var ids = $("#imageIDs").val();
            $("#uploadFileNames").val(filenames.substr(0, 1) == "|" ? filenames.substr(1, filenames.length + 1) : filenames);
            $("#uploadFiles").val(fileIds.substr(0, 1) == "|" ? fileIds.substr(1, fileIds.length + 1) : fileIds);
            $("#imageIDs").val(ids.substr(0, 1) == "," ? ids.substr(1, ids.length + 1) : ids);

            str += "<li id=\"uploadimageshow" + d.ID + "\" data-id=\"" + d.ID + "\"><img src=\"" + photofunctions.getprofileiconphotoname(d.FileName) + "\" width=\"134\" height=\"134\" title=\"图片\" /></li>";

            $("#photoInfoList").html($("#photoInfoList").html() + str);

        }
        b.toggleCancel(true, a, function () {
            $.post_withck(a.customSettings.jUrl + "deletephoto", {
                photo_id: e.post.photo_id
            }, function (f) {
                f = $.parseJSON(f);
                if (f.r) {
                    b.setError();
                    b.setStatus("删除时：" + errdetail[f.r]);
                    allErrorFiles++; console.log("toggleCancel" + allErrorFiles)
                }
                else {
                    b.setCancelled();
                    idsFiled = $("#uploadFiles");
                    ids = idsFiled.val().replace("|" + e.post.photo_id, "");
                    idsFiled.val(ids);
                    setTimeout(function () { b.disappear() }, 2000);
                    decrease_upload_count(a)
                }
            })
        })
    }

    function uploadError(c, e, d) {
        var a = this, b = new FileProgress(c, this.customSettings.progressTarget);
        b.setError();
        b.toggleCancel(true, a);
        allErrorFiles++;
        b.setStatus("失败");
        b.removeDelIcon();
        switch (e) {
            case SWFUpload.UPLOAD_ERROR.HTTP_ERROR:
                b.setStatus("失败");
                debug_message(this, "HTTP Error", c, d);
                break;
            case SWFUpload.UPLOAD_ERROR.UPLOAD_FAILED:
                b.setStatus("失败");
                debug_message(this, "Upload Failed", c, d);
                break;
            case SWFUpload.UPLOAD_ERROR.IO_ERROR:
                b.setStatus("失败");
                debug_message(this, "IO Error", c, d);
                break;
            case SWFUpload.UPLOAD_ERROR.SECURITY_ERROR:
                b.setStatus("失败");
                debug_message(this, "Security Error", c, d);
                break;
            case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
                debug_message(this, "Upload limit exceeded", c, d);
                b.setStatus("失败");
                break;
            case SWFUpload.UPLOAD_ERROR.FILE_VALIDATION_FAILED:
                debug_message(this, "File validation failed", c, d);
                b.setStatus("失败");
                break;
            case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
                b.setStatus("取消");
                b.setCancelled();
                updateTotal(totalNum -= 1, totalSize -= c.size);
                allErrorFiles--;
                if ($(".progressWrapper").length <= 20) {
                    $(".continue-wrapper").removeClass("hidden")
                }
                break;
            case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
                b.setStatus("停止");
                break;
            default:
                b.setStatus("错误：" + e);
                debug_message(this, e, c, d);
                break
        }
    }

    var inimage = false;
    var leavetime = null;
    //上传完成
    function queueComplete(a) {
        allFilesUploaded += (a - allErrorFiles);
        if (allFilesUploaded > 0) {
            if (allFilesUploaded === a) { allErrorFiles = 0 }
            else { }
        }
        else { $(".continue-wrapper").removeClass("hidden"); }
        uploadStarted = false;
        imagedelmover();
        loadimagelayout(null, null);
    }
    function imagedelmover() {
        $("#photoInfoList li").mouseenter(function () {
            inimage = true;
            if ($("#deleteImage").html() != null) { $("#deleteImage").remove(); }
            clearTimeout(leavetime);
            var id = $(this).attr("data-id");
            $("body").append("<img id=\"deleteImage\" style=\"position:absolute;left:" + $(this).offset().left + "px;top:" + $(this).offset().top + "px;cursor:pointer; z-index:1000;\" title=\"点击删除图片\" onclick=\"deletePhoto('" + id + "',$('#uploadimageshow" + id + "'))\" src=\"/pics/ImageUp_Close.png\"/>");
            $("#deleteImage").mouseenter(function () {
                clearTimeout(leavetime);
                inimage = true;
                $(this).mouseleave(function () {
                    inimage = false;
                    leavetime = setTimeout(function () {
                        if (!inimage) {
                            if ($("#deleteImage").html() != null) { $("#deleteImage").remove(); }
                            clearTimeout(leavetime);
                        }
                    }, 100);
                });
            })
            $(this).mouseleave(function () {
                inimage = false;
                leavetime = setTimeout(function () {
                    if (!inimage) {
                        if ($("#deleteImage").html() != null) { $("#deleteImage").remove(); }
                        clearTimeout(leavetime);
                    }
                }, 100);
            });
        });
    }
</script>
<script type="text/javascript">
    function FileProgress(a, b) {
        var i = document;
        this.fileProgressID = a.id;
        this.opacity = 100;
        this.height = 0;
        this.fileProgressWrapper = i.getElementById(this.fileProgressID);
        if (!this.fileProgressWrapper) {
            this.fileProgressWrapper = i.createElement("div");
            this.fileProgressWrapper.className = "progressWrapper";
            this.fileProgressWrapper.id = this.fileProgressID;
            this.fileProgressElement = i.createElement("div");
            this.fileProgressElement.className = "progressContainer";

            var uploadlist = i.createElement("div");
            uploadlist.className = "uploadlist";

            var rtop = i.createElement("b");
            rtop.className = "rtop";
            var r1 = i.createElement("b");
            r1.className = "r1";
            var r2 = i.createElement("b");
            r2.className = "r2";
            var r3 = i.createElement("b");
            r3.className = "r3";
            var r4 = i.createElement("b");
            r4.className = "r4";
            rtop.appendChild(r1);
            rtop.appendChild(r2);
            rtop.appendChild(r3);
            rtop.appendChild(r4);

            var upload_demo = i.createElement("div");
            upload_demo.className = "upload_demo";

            var upload_l = i.createElement("div");
            upload_l.className = "upload_l";
            upload_l.appendChild(i.createTextNode(a.name));

            var upload_z = i.createElement("div");
            upload_z.className = "upload_z";

            var jindu = i.createElement("span");
            jindu.className = "jindu";
            var jinduimg = i.createElement("img");
            jinduimg.src = "/pics/jindu_01.gif";
            var jinduem = i.createElement("em");
            jindu.appendChild(jinduimg);
            jindu.appendChild(jinduem);

            upload_z.appendChild(jindu);
            upload_demo.appendChild(upload_l);
            upload_demo.appendChild(upload_z);

            var rbottom = i.createElement("b");
            rbottom.className = "rbottom";
            var r11 = i.createElement("b");
            r11.className = "r1";
            var r22 = i.createElement("b");
            r22.className = "r2";
            var r33 = i.createElement("b");
            r33.className = "r3";
            var r44 = i.createElement("b");
            r44.className = "r4";
            rbottom.appendChild(r44);
            rbottom.appendChild(r33);
            rbottom.appendChild(r22);
            rbottom.appendChild(r11);

            uploadlist.appendChild(rtop);
            uploadlist.appendChild(upload_demo);
            uploadlist.appendChild(rbottom);
            
            this.fileProgressElement.appendChild(uploadlist);
            this.fileProgressWrapper.appendChild(this.fileProgressElement);
            i.getElementById(b).appendChild(this.fileProgressWrapper)
        }
        else {
            this.fileProgressElement = this.fileProgressWrapper.firstChild
        }
        this.height = this.fileProgressWrapper.offsetHeight
    }

    FileProgress.prototype.setProgress = function (a) {
        this.fileProgressElement.childNodes[0].childNodes[1].childNodes[1].childNodes[0].childNodes[1].style.width = a + "px";
    };
    FileProgress.prototype.setComplete = function () {
        $(this.fileProgressWrapper).animate({ height: 0 }, 500, function () { $(this.fileProgressWrapper).remove(); });
    };
    FileProgress.prototype.setError = function () {
        this.fileProgressElement.childNodes[0].childNodes[1].childNodes[1].childNodes[0].childNodes[1].style.width = "";
    };
    FileProgress.prototype.setCancelled = function () {
        this.fileProgressElement.childNodes[0].childNodes[1].childNodes[1].childNodes[0].childNodes[1].style.width = "";
        var a = this; setTimeout(function () { a.disappear() }, 200)
    };
    FileProgress.prototype.setStatus = function (a) { };
    FileProgress.prototype.removeDelIcon = function () { };
    FileProgress.prototype.toggleCancel = function (b, c, d) { };
    FileProgress.prototype.disappear = function () {
        var f = 15, c = 4, b = 30;
        if (this.opacity > 0) {
            this.opacity -= f;
            if (this.opacity < 0) {
                this.opacity = 0
            }
            if (this.fileProgressWrapper.filters) {
                try {
                    this.fileProgressWrapper.filters.item("DXImageTransform.Microsoft.Alpha").opacity = this.opacity
                }
                catch (d) {
                    this.fileProgressWrapper.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=" + this.opacity + ")"
                }
            }
            else {
                this.fileProgressWrapper.style.opacity = this.opacity / 100
            }
        }
        if (this.height > 0) {
            this.height -= c;
            if (this.height < 0) {
                this.height = 0
            }
            this.fileProgressWrapper.style.height = this.height + "px"
        }
        if (this.height > 0 || this.opacity > 0) {
            var a = this; setTimeout(function () { a.disappear() }, b)
        }
        else {
            $(this.fileProgressWrapper).remove();
            if (!$("#fsUploadProgress").children().length) {
                $(".upload-list").addClass("hidden");
            }
        }
    };

</script>
