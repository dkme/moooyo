function mousedown(obj) {
    obj.parent().children("div.Framediv").attr("data-ifclick", "false");
    obj.parent().children("div.Framediv").css({ "background": "#f3f3f3" });
    obj.parent().children("div.Framediv").children(".childFrame").css({ "background": "#ccc" });
    var data_ifclick = obj.attr("data-ifclick");
    if (data_ifclick == "false" || data_ifclick == "False") {
        obj.css({ "background": "#b40001" });
        obj.children('.childFrame').css({ "background": "#fff" });
        obj.attr("data-ifclick", "true");
        var layoutype = obj.attr("data-layouttype");
        $("#imageLayOutType").val(layoutype);
    }
    else {
        obj.css({ "background": "#f3f3f3" });
        obj.children('.childFrame').css({ "background": "#ccc" });
        obj.attr("data-ifclick", "false");
    }
}

//获取图片布局框架Json对象
function getImageLayoutFrameObjs(imageCount) 
{
    imageFrames = null;
    var frames = imageLayoutFrameGroups;
    var frameJsonStr = "[";
    switch (imageCount) {
        case 2:
            frameJsonStr += JSON.stringify(frames[0]);
            frameJsonStr += ",";
            frameJsonStr += JSON.stringify(frames[1]);
            frameJsonStr += "]";
            return $.parseJSON(frameJsonStr);
            break;
        case 3:
            frameJsonStr += JSON.stringify(frames[2]);
            frameJsonStr += ",";
            frameJsonStr += JSON.stringify(frames[3]);
            frameJsonStr += "]";
            return $.parseJSON(frameJsonStr);
            break;
        case 4:
            frameJsonStr += JSON.stringify(frames[4]);
            frameJsonStr += ",";
            frameJsonStr += JSON.stringify(frames[5]);
            frameJsonStr += ",";
            frameJsonStr += JSON.stringify(frames[6]);
            frameJsonStr += ",";
            frameJsonStr += JSON.stringify(frames[7]);
            frameJsonStr += "]";
            return $.parseJSON(frameJsonStr);
            break;
        case 5:
            frameJsonStr += JSON.stringify(frames[8]);
            frameJsonStr += ",";
            frameJsonStr += JSON.stringify(frames[9]);
            frameJsonStr += ",";
            frameJsonStr += JSON.stringify(frames[10]);
            frameJsonStr += "]";
            return $.parseJSON(frameJsonStr);
            break;
        default: break;
    }
}

//获取图片布局框架Html
function getImageLayoutFrameHtml(imageCount) {
    var imageFrameObj = getImageLayoutFrameObjs(imageCount);
    var htmlStr = generateImageLayoutFrameHtml(imageFrameObj, imageCount);
    return htmlStr;
}

//生成图片布局框架Html
function generateImageLayoutFrameHtml(imageFrameObj, imageCount) {
    var imagecountstr = "";
    var imagetypestr = "";
    switch (imageCount) {
        case 2: imagecountstr = "two"; break;
        case 3: imagecountstr = "three"; break;
        case 4: imagecountstr = "four"; break;
        case 5: imagecountstr = "five"; break;
    }
    var htmlStr = "<div class=\"parentframe\">";
    for (var i = 0; i < imageFrameObj.length; i++) {
        switch (i) {
            case 0: imagetypestr = "one"; break;
            case 1: imagetypestr = "two"; break;
            case 2: imagetypestr = "three"; break;
            case 3: imagetypestr = "four"; break;
            case 4: imagetypestr = "five"; break;
            case 5: imagetypestr = "six"; break;
        }
        var child = imageFrameObj[i];
        htmlStr += "<div class=\"Framediv\" id=\"Framediv\" onclick=\"mousedown($(this))\" data-layouttype=\"" + imagecountstr + "_" + imagetypestr + "\" data-ifclick=\"false\" style=\"width:" + child.parentFrame.w + "px;height:" + child.parentFrame.h + "px;\">";
        for (var j = 0; j < child.framePartList.length; j++) {
            var childframe = child.framePartList[j];
            htmlStr += "<div class=\"childFrame\" style=\"left:" + childframe.x + "px;top:" + childframe.y + "px;width:" + childframe.w + "px;height:" + childframe.h + "px;\">&nbsp;</div>";
        }
        htmlStr += "</div>";
    }
    htmlStr += "</div>";
    return htmlStr;
}

//Json图片布局框架相关属性实体
var imageLayoutFrameGroups = 
    [
        {
            imageCount: 2,
            parentFrame: {
                w: 100,
                h: 112
            },
            framePartList: [
                { x: 5, y: 5, w: 90, h: 50 }, 
                { x: 5, y: 57, w: 90, h: 50 }
            ]
        }, {
            imageCount: 2,
            parentFrame: {
                w: 132,
                h: 60
            },
            framePartList: [
                { x: 5, y: 5, w: 60, h: 50 },
                { x: 67, y: 5, w: 60, h: 50 }
            ]
        }, {
            imageCount: 3,
            parentFrame: {
                w: 100,
                h: 86
            },
            framePartList: [
                { x: 5, y: 5, w: 90, h: 50 },
                { x: 5, y: 57, w: 44, h: 24 },
                { x: 51, y: 57, w: 44, h: 24 }
            ]
        }, {
            imageCount: 3,
            parentFrame: {
                w: 100,
                h: 86
            },
            framePartList: [
                { x: 5, y: 5, w: 44, h: 24 },
                { x: 51, y: 5, w: 44, h: 24 },
                { x: 5, y: 31, w: 90, h: 50 }
            ]
        }, { 
            imageCount: 4,
            parentFrame: {
                w: 100,
                h: 138
            },
            framePartList: [
                { x: 5, y: 5, w: 90, h: 50 },
                { x: 5, y: 57, w: 44, h: 24 },
                { x: 51, y: 57, w: 44, h: 24 },
                { x: 5, y: 83, w: 90, h: 50 }
            ]
        }, {
            imageCount: 4,
            parentFrame: {
                w: 100,
                h: 60
            },
            framePartList: [
                { x: 5, y: 5, w: 44, h: 24 },
                { x: 51, y: 5, w: 44, h: 24 },
                { x: 5, y: 31, w: 44, h: 24 },
                { x: 51, y: 31, w: 44, h: 24 }
            ]
        }, {
            imageCount: 4,
            parentFrame: {
                w: 98,
                h: 91
            },
            framePartList: [
                { x: 5, y: 5, w: 88, h: 50 },
                { x: 5, y: 57, w: 28, h: 29 },
                { x: 35, y: 57, w: 28, h: 29 },
                { x: 65, y: 57, w: 28, h: 29 }
            ]
        }, {
            imageCount: 4,
            parentFrame: {
                w: 98,
                h: 91
            },
            framePartList: [
                { x: 5, y: 5, w: 28, h: 29 },
                { x: 35, y: 5, w: 28, h: 29 },
                { x: 65, y: 5, w: 28, h: 29 },
                { x: 5, y: 36, w: 88, h: 50 }
            ]
        }, {
            imageCount: 5,
            parentFrame: {
                w: 100,
                h: 112
            },
            framePartList: [
                { x: 5, y: 5, w: 90, h: 50 },
                { x: 5, y: 57, w: 44, h: 24 },
                { x: 51, y: 57, w: 44, h: 24 },
                { x: 5, y: 83, w: 44, h: 24 },
                { x: 51, y: 83, w: 44, h: 24 }
            ]
        }, {
            imageCount: 5,
            parentFrame: {
                w: 100,
                h: 112
            },
            framePartList: [
                { x: 5, y: 5, w: 44, h: 24 },
                { x: 51, y: 5, w: 44, h: 24 },
                { x: 5, y: 31, w: 90, h: 50 },
                { x: 5, y: 83, w: 44, h: 24 },
                { x: 51, y: 83, w: 44, h: 24 }
            ]
        }, {
            imageCount: 5,
            parentFrame: {
                w: 100,
                h: 112
            },
            framePartList: [
                { x: 5, y: 5, w: 44, h: 24 },
                { x: 51, y: 5, w: 44, h: 24 },
                { x: 5, y: 31, w: 44, h: 24 },
                { x: 51, y: 31, w: 44, h: 24 },
                { x: 5, y: 57, w: 90, h: 50 }
            ]
        }
    ]
