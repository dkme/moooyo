var Moooyo = {};
var errdetail = $.extend(Moooyo.errdetail, { 20: "抱歉，图片短边尺寸不能小于400像素", 21: "信息不全，请刷新页面重试。" });
var allFilesUploaded = 0, allErrorFiles = 0, uploadStarted = false, totalNum = 0, totalSize = 0, idsFiled, ids = "";
function updateTotal(a, b) {
    $("#upload-status .num b").text(a);
    $("#upload-status .total-size b").text(b / 1024 < 1024 ? (b / 1024).toFixed(1) + " KB" : (b / 1024 / 1024).toFixed(2) + " MB")
}
function fileQueued(c) {
    var a = this, b = new FileProgress(c, a.customSettings.progressTarget);
    totalNum += 1;
    totalSize += c.size; b.toggleCancel(true, a);
    if (!uploadStarted) {
        $(".upload-tips").addClass("hidden");
        //$("#btnCancel").removeClass("hidden");
        //$("#btnCancel").addClass("noup");
        //$("#upload-btn").css({ opacity: 0, height: 0 });
        $(".opt-btns, .upload-list, #upload-status").removeClass("hidden");
        $("#upload-status .num b").text(c.index + 1); $(".progressBarInProgress").addClass("hidden");
        if ($(".progressWrapper").length === 20) {
            $(".continue-wrapper").addClass("hidden")
        }
        updateTotal(totalNum, totalSize);
        swfup.startUpload();
    }

}

function swfuploadLoadFailed() {
    $("#noflash").show(); $("#old-portal").show()
}

function preload() { }
function debug_message(d, a, b, c) {
    d.debug("Error Code: " + a + ", File name: " + b.name + ", File size: " + b.size + ", Message: " + c)
}
function decrease_upload_count(b) { allFilesUploaded--; var a = b.getStats(); a.successful_uploads--; b.setStats(a) }
function fileQueueError(c, e, d) {
    var a = this;
    if (e === SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED) {
        alert("你选择了太多的文件,\n" + (d === 0 ? "达到了上传的上限" : "你最多可以选择 " + d + " 个文件。"));
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

function fileDialogComplete(a, c) {
    var b = this;
    if (a > 0) {
        //document.getElementById(this.customSettings.cancelButtonId).disabled = false;
    } 
}

function uploadStart(b) {
    $(".continue-wrapper").addClass("hidden"); $("#upload-start").val("上传中...").attr("disabled", "disabled");
    $(".progressBarInProgress").removeClass("hidden");
    var a = new FileProgress(b, this.customSettings.progressTarget);
    a.toggleCancel(true, this);
    return true
}

function uploadProgress(b, e, d) {
    var c = Math.ceil((e / d) * 100);
    var a = new FileProgress(b, this.customSettings.progressTarget);
    a.setProgress(c)
}

function uploadSuccess(e, c) {
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
        $(".upload-list .delete").remove();
        e.post.photo_id = d.id;
        idsFiled = $("#uploadFiles");
        ids = idsFiled.val() + "|" + d.ID;
        idsFiled.val(ids);
        $("#imgRegisterFace").attr("src", "/photo/get/" + d.FileName);
        idsFileNames = $("#uploadFileNames");
        filenames = idsFileNames.val() + "|" + d.FileName;
        idsFileNames.val(filenames);
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
        }
                    )
    }
                    )
}

function uploadError(c, e, d) {
    var a = this, b = new FileProgress(c, this.customSettings.progressTarget);
    b.setError();
    b.toggleCancel(true, a);
    allErrorFiles++;
    //console.log("uploadError" + allErrorFiles);
    b.setStatus("失败");
    alert("您的头像上传失败，请您再次上传！"); //上传失败时提示
    b.removeDelIcon();
    $(".upload-list .delete").remove();
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
            if (this.getStats().files_queued === 0) {
                //document.getElementById(this.customSettings.cancelButtonId).disabled = true
            }
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

function uploadComplete(a, b) {
    if (this.getStats().files_queued === 0) {
        //$("#" + this.customSettings.cancelButtonId)[0].disabled = true
    } 
}

//开始上传后才能进入队列完成
function queueComplete(a) {
    //$("#upload-start").val("开始上传").removeAttr("disabled");
    $("#upload-start").val("").removeAttr("disabled");
    $(".opt-btns").addClass("hidden");
    allFilesUploaded += (a - allErrorFiles);
    if (allFilesUploaded > 0) {
        idsFiled.val(ids.split("|")[1]);
        if (allFilesUploaded === a) {
            $("#upload").submit();
            allErrorFiles = 0
        }
        else {
            $("#has-error").removeClass().find("b").text(allErrorFiles);
            $(".nextstep").removeClass("hidden")
        } 
    }
    else {
        $(".continue-wrapper").removeClass("hidden");
        $("#has-error").removeClass().find("b").text(allErrorFiles);
        $(".nextstep").removeClass("hidden")
    }
    uploadStarted = false;
    //增加调用页面方法，上传队列完成后调用
    uploadfinished();
};
