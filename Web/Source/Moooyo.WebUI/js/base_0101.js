//加了页面方法组 actionprovider 和 member_i_functions

/////////////////////////////////////////////////////////////////////////////
// 页面功能
/////////////////////////////////////////////////////////////////////////////
//处理键盘事件 禁止后退键（Backspace）密码或单行、多行文本框除外  
function banBackSpace(e) {
    var ev = e || window.event; //获取event对象     
    var obj = ev.target || ev.srcElement; //获取事件源     

    var t = obj.type || obj.getAttribute('type'); //获取事件源类型    

    //获取作为判断条件的事件类型  
    var vReadOnly = obj.getAttribute('readonly');
    var vEnabled = obj.getAttribute('enabled');
    //处理null值情况  
    vReadOnly = (vReadOnly == null) ? false : vReadOnly;
    vEnabled = (vEnabled == null) ? true : vEnabled;

    //当敲Backspace键时，事件源类型为密码或单行、多行文本的，  
    //并且readonly属性为true或enabled属性为false的，则退格键失效  
    var flag1 = (ev.keyCode == 8 && (t == "password" || t == "text" || t == "textarea")
                && (vReadOnly == true || vEnabled != true)) ? true : false;

    //当敲Backspace键时，事件源类型非密码或单行、多行文本的，则退格键失效  
    var flag2 = (ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea")
                ? true : false;

    //判断  
    if (flag2) {
        return false;
    }
    if (flag1) {
        return false;
    }
}
//是否为邮箱
function isEmail(str) {
    var reg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
    
    return reg.test(str);
}
//通用滑动门
function changInfoTab2(ind, allnum, pre) {
    for (var i = 1; i <= allnum; i++) {
        document.getElementById(pre + '_tabmenu' + String(i)).className = 'biaoti_unck';
        document.getElementById(pre + '_content' + String(i)).style.display = 'none';
    }
    document.getElementById(pre + '_tabmenu' + String(ind)).className = 'biaoti_ck';
    document.getElementById(pre + '_content' + String(ind)).style.display = 'block';
}
function removeHtml(htmlId) {
    $("#" + htmlId).remove();
}
//通用显示隐藏
function ShowHide(container, flag) {
    if (flag == "show") {
        $("[name = '" + container + "']").show();
    } else if (flag == "hide") {
        $("[name = '" + container + "']").hide();
    }
}
//获取鼠标当前位置x坐标
function getElemXCoor(elem) {
    return elem.offsetParent ? (elem.offsetLeft + getElemXCoor(elem.offsetParent)) : elem.offsetLeft;
}
//获取鼠标当前位置y坐标
function getElemYCoor(elem) {
    return elem.offsetParent ? (elem.offsetTop + getElemYCoor(elem.offsetParent)) : elem.offsetTop;
}
//获取鼠标当前位置x和y坐标
function getMousePositXY(elem) {
    if (!$.browser.msie) {
        return { x: (elem.offsetParent ? (elem.offsetLeft + getElemXCoor(elem.offsetParent)) : elem.offsetLeft) + 30,
            y: (elem.offsetParent ? (elem.offsetTop + getElemYCoor(elem.offsetParent)) : elem.offsetTop) + 30
        };
    } else {
        var sleft = !parseInt(document.body.scrollLeft) ? document.documentElement.scrollLeft - document.documentElement.clientLeft : (document.body.scrollLeft - document.body.clientLeft);
        var stop = !parseInt(document.body.scrollTop) ? document.documentElement.scrollTop - document.documentElement.clientTop : (document.body.scrollTop - document.body.clientTop);
        return {
            x: (window.event.clientX + sleft - document.body.clientLeft),
            y: (window.event.clientY + stop - document.body.clientTop)
        };
    }
}

//显示jbox信息提示
function showjbox(str) {
    $.jBox.tip(str, 'info');
}

//jquery 复制文本到剪贴板
function copyToClipboard(btnElement, copyText) {
    if (!$.browser.msie) {
        $(btnElement).zclip({
            path: '/swf/ZeroClipboard.swf',
            copy: copyText,
            afterCopy: function () {
                $.jBox.tip("已复制到剪贴板:<br />" + copyText, 'info');
            }
        });
    }
    else {
        var flag = true;
        if (window.clipboardData) {
            window.clipboardData.setData("Text", copyText);
            var getClipboardData = window.clipboardData.getData("Text");
            if (getClipboardData == "" || getClipboardData == undefined) flag = false;
            if (flag) {
                $.jBox.tip("已复制到剪贴板：<br />" + copyText, 'info');
            }
            else {
                $.jBox.tip("出错啦！没有复制到剪贴板", 'info');
            }
        }
    }
}

//获取元素浏览器页中可见区域的横和纵坐标
function getOffsetPosition(element) {
    var offsetParent = element;
    var left = 0, top = 0;
    while (offsetParent != null && offsetParent.nodeName != document.body) {
        left += offsetParent.offsetLeft;
        top += offsetParent.offsetTop;
        offsetParent = offsetParent.offsetParent;
    }
    var offsetPosit = {
        offsetLeft: left,
        offsetTop: top
    };
    return offsetPosit;
}
//获取用户主页简短地址
function getuserminid(mid) {

    var url = "";
    $.ajaxSetup({ async: false });
    AdminProvider.getUserMin_Id(mid, function (result) {
        var id = $.parseJSON(result);
        if ($.trim(id).length > 0) {
            url = "/u/" + id;
        }
        else {
            url = "empty";
        }
    });
    $.ajaxSetup({ async: true });
    return url;
}

//JavaScript过滤特殊字符
function filterString(s) {
    var pattern = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）&mdash;—|{}【】‘；：”“'。，、？]")
    var rs = "";
    for (var i = 0; i < s.length; i++) {
        rs = rs + s.substr(i, 1).replace(pattern, '');
    }
    return rs;
}
//JavaScript过滤HTML字符
function filterHtmlString(str) {
    str = str.replace(/<\/?[^>]*>/g, ''); //去除HTML tag
    str.value = str.replace(/[ | ]*\n/g, '\n'); //去除行尾空白
    str = str.replace(/\n[\s| | ]*\r/g,'\n'); //去除多余空行
    return str;
}
//关闭当前浏览器窗口或这选项卡
function closenowwindow() {
    window.opener = null;
    window.open('', '_self'); 
    window.close();
}


/////////////////////////////////////////////////////////////////////////////
// 字符串、日期方法
/////////////////////////////////////////////////////////////////////////////
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month  
        "d+": this.getDate(),    //day  
        "h+": this.getHours(),   //hour  
        "m+": this.getMinutes(), //minute  
        "s+": this.getSeconds(), //second  
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter  
        "S": this.getMilliseconds() //millisecond  
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
     (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
       RegExp.$1.length == 1 ? o[k] :
         ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}

jQuery.extend(
         {
             toJSON: function (object) {
                 var type = typeof object;
                 if ('object' == type) {
                     if (Array == object.constructor)
                         type = 'array';
                     else if (RegExp == object.constructor)
                         type = 'regexp';
                     else
                         type = 'object';
                 }
                 switch (type) {
                     case 'undefined':
                     case 'unknown':
                         return;
                         break;
                     case 'function':
                     case 'boolean':
                     case 'regexp':
                         return object.toString();
                         break;
                     case 'number':
                         return isFinite(object) ? object.toString() : 'null';
                         break;
                     case 'string':
                         return '"' + object.replace(/(\\|\")/g, "\\$1").replace(/\n|\r|\t/g,
        function () {
            var a = arguments[0];
            return (a == '\n') ? '\\n' :
                        (a == '\r') ? '\\r' :
                        (a == '\t') ? '\\t' : ""
        }) + '"';
                         break;
                     case 'object':
                         if (object === null) return 'null';
                         var results = [];
                         for (var property in object) {
                             var value = jQuery.toJSON(object[property]);
                             if (value !== undefined)
                                 results.push(jQuery.toJSON(property) + ':' + value);
                         }
                         return '{' + results.join(',') + '}';
                         break;
                     case 'array':
                         var results = [];
                         for (var i = 0; i < object.length; i++) {
                             var value = jQuery.toJSON(object[i]);
                             if (value !== undefined) results.push(value);
                         }
                         return '[' + results.join(',') + ']';
                         break;
                 }
             },
             evalJSON: function (strJson) {
                 return eval("(" + strJson + ")");
             }
         });

/*** 删除左右两端的空格 */
function trim(str) {
    return str.replace(/(^\s*)(\s*$)/g, '');
}
/**
* 删除左边的空格
*/
function ltrim(str) {
    return str.replace(/(^\s*)/g, '');
}
/**
* 删除右边的空格
*/
function rtrim(str) {
    return str.replace(/(\s*$)/g, '');
}
//将时间戳转换为时间
function getConversionTimeStamp(strDT) {
    // 声明变量。
    var d, s;
    // 创建 Date 对象。
    d = new Date(strDT * 1000);
    s = d.getFullYear() + "-";
    s += ("0" + (d.getMonth() + 1)).slice(-2) + "-";
    s += ("0" + d.getDate()).slice(-2) + " ";
    s += ("0" + d.getHours()).slice(-2) + ":";
    s += ("0" + d.getMinutes()).slice(-2) + ":";
    s += ("0" + d.getSeconds()).slice(-2) + ".";
    s += ("00" + d.getMilliseconds()).slice(-3);
    return s.toLocaleString();
}
//得到距离当前时间
function getTimeSpan(date) {
    var df = ((new Date()).getTime() - date.getTime()) / 1000;
    var time;
    if (df < 0) return date.format("yyyy-mm-dd hh:mm:ss");
    if (df >= 0 & df < 60) time = Math.floor(df) + "秒前";
    if (df >= 60 & df < 3600) time = Math.floor(df / 60) + "分钟前";
    if (df >= 3600 & df < 86400) time = Math.floor(df / 3600) + "小时前";
    if (df >= 86400 & df < 2592000) time = Math.floor(df / 86400) + "天前";
    if (df >= 2592000) time = Math.floor(df / 2592000) + "个月前";
    return time;
}
function getAge(date) {
    var df = ((new Date()).getTime() - date.getTime()) / 1000;
    var time = Math.floor(df / 31536000) + "岁";
    return time;
}
//监测输入框文本长度
function checkLen(content) {
    var res = true;
    if (strlength(content) > 140) {
        res = false;
        $.jBox.tip("请写得简短些，只支持140字符以内哦..", 'info');
        return false;
    }
    return res;
}
/*统计字符个数(一个汉字算1个)*/
function strlength(str) {
    var strl = 0;
    var l = str.length;
    for (var i = 0; i < l; i++) {
        //全角字符  
        if (str.charCodeAt(i) < 0 || str.charCodeAt(i) > 255)
            strl = strl + 1;
        else
            strl++;
    }
    return strl
}
/*超长字符串截取*/
String.prototype.cut = function (length) {
    if (length > this.length) return this;
    return this.substring(0, length) + "<span class=\"letspa--3\">...</span>";
}
/*格式化字符串*/
String.format = function (src) {
    if (arguments.length == 0) return null;
    var args = Array.prototype.slice.call(arguments, 1);
    return src.replace(/\{(\d+)\}/g,
    function (m, i) { return args[i]; });
};
/* ubb处理 */
function html_trans(str) {
    str = str.replace(/\r/g, "");
    str = str.replace(/on(load|click|dbclick|mouseover|mousedown|mouseup)="[^"]+"/ig, "");
    str = str.replace(/<script[^>]*?>([\w\W]*?)<\/script>/ig, "");
    str = str.replace(/<style[^>]*?>([\w\W]*?)<\/stylet>/ig, "");
    str = str.replace(/<embed[^>]*?>([\w\W]*?)<\/embed>/ig, "");

    str = str.replace(/<a[^>]+href="([^"]+)"[^>]*>(.*?)<\/a>/ig, "[url=$1]$2[/url]");
    str = str.replace(/<font[^>]+color=([^ >]+)[^>]*>(.*?)<\/font>/ig, "[color=$1]$2[/color]");
    str = str.replace(/<img[^>]+src="([^"]+)"[^>]*>/ig, "[img]$1[/img]");
    str = str.replace(/<param NAME="Movie" value="([^>"]+\.swf)"[^>]*>/ig, "[flash]$1[/flash]");

    str = str.replace(/<([\/]?)b>/ig, "[$1b]");
    str = str.replace(/<([\/]?)strong>/ig, "[$1b]");
    str = str.replace(/<([\/]?)u>/ig, "[$1u]");
    str = str.replace(/<([\/]?)i>/ig, "[$1i]");

    str = str.replace(/&nbsp;/g, " ");
    str = str.replace(/&amp;/g, "&");
    str = str.replace(/&quot;/g, "\"");
    str = str.replace(/&lt;/g, "<");
    str = str.replace(/&gt;/g, ">");

    str = str.replace(/\[url=([^\]]+)\]\[img\]/g, "[img]");
    str = str.replace(/\[\/img\]\[\/url\]/g, "[/img]");

    str = str.replace(/<br>/ig, "\n");
    str = str.replace(/<[^>]*?>/g, "");

    str = str.replace(/\n+/g, "\n");

    return str;
}


/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// 地图相关方法
/////////////////////////////////////////////////////////////////////////////
//googleMap apis
var map;
var marker;

function getMap(address, map_canvas, callback) {
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode(
        { 'address': address },
        function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var myOptions = { zoom: 11, center: results[0].geometry.location, mapTypeId: google.maps.MapTypeId.ROADMAP };
                map = new google.maps.Map(map_canvas, myOptions);
                google.maps.event.addListener(map, 'click', function (event) { callback(event.latLng); });
            }
            else {
                $.jBox.tip("地图获取失败，原因: " + status, 'info');
            }
        });
}
function getMapByLatLng(lat, lng, map_canvas, callback) {
    var latlng = new google.maps.LatLng(lat, lng);
    var myOptions = { zoom: 11, center: latlng, mapTypeId: google.maps.MapTypeId.ROADMAP };
    map = new google.maps.Map(map_canvas, myOptions);
    google.maps.event.addListener(map, 'click', function (event) { callback(event.latLng); });

}
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// URL方法
/////////////////////////////////////////////////////////////////////////////
//获取url中的参数
function UrlRequest(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

function openwindow(url, name, iWidth, iHeight) {
    var url;                                 //转向网页的地址;    
    var name;                           //网页名称，可为空;    
    var iWidth;                          //弹出窗口的宽度;    
    var iHeight;                        //弹出窗口的高度;    
    var iTop = (window.screen.availHeight - 30 - iHeight) / 2;       //获得窗口的垂直位置;    
    var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;           //获得窗口的水平位置;   
    return window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=auto,resizeable=yes,location=no,status=no');
}

function openurl(url) {
    window.location = url;
}

///////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// Json对象处理方法
/////////////////////////////////////////////////////////////////////////////
//转变Json中的日期格式
function paserJsonDate(jsondate) {
    var i = parseInt((jsondate.replace('\/Date(', '')).replace(')\/', ''));
    var dateobj = new Date(i);
    return dateobj;
}

var dateFormat = function () {
    var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
		timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
		timezoneClip = /[^-+\dA-Z]/g,
		pad = function (val, len) {
		    val = String(val);
		    len = len || 2;
		    while (val.length < len) val = "0" + val;
		    return val;
		};

    // Regexes and supporting functions are cached through closure
    return function (date, mask, utc) {
        var dF = dateFormat;

        // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
        if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
            mask = date;
            date = undefined;
        }

        // Passing date through Date applies Date.parse, if necessary
        date = date ? new Date(date) : new Date;
        if (isNaN(date)) throw SyntaxError("invalid date");

        mask = String(dF.masks[mask] || mask || dF.masks["default"]);

        // Allow setting the utc argument via the mask
        if (mask.slice(0, 4) == "UTC:") {
            mask = mask.slice(4);
            utc = true;
        }

        var _ = utc ? "getUTC" : "get",
			d = date[_ + "Date"](),
			D = date[_ + "Day"](),
			m = date[_ + "Month"](),
			y = date[_ + "FullYear"](),
			H = date[_ + "Hours"](),
			M = date[_ + "Minutes"](),
			s = date[_ + "Seconds"](),
			L = date[_ + "Milliseconds"](),
			o = utc ? 0 : date.getTimezoneOffset(),
			flags = {
			    d: d,
			    dd: pad(d),
			    ddd: dF.i18n.dayNames[D],
			    dddd: dF.i18n.dayNames[D + 7],
			    m: m + 1,
			    mm: pad(m + 1),
			    mmm: dF.i18n.monthNames[m],
			    mmmm: dF.i18n.monthNames[m + 12],
			    yy: String(y).slice(2),
			    yyyy: y,
			    h: H % 12 || 12,
			    hh: pad(H % 12 || 12),
			    H: H,
			    HH: pad(H),
			    M: M,
			    MM: pad(M),
			    s: s,
			    ss: pad(s),
			    l: pad(L, 3),
			    L: pad(L > 99 ? Math.round(L / 10) : L),
			    t: H < 12 ? "a" : "p",
			    tt: H < 12 ? "am" : "pm",
			    T: H < 12 ? "A" : "P",
			    TT: H < 12 ? "AM" : "PM",
			    Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
			    o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
			    S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
			};

        return mask.replace(token, function ($0) {
            return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
        });
    };
} ();

// Some common format strings
dateFormat.masks = {
    "default": "ddd mmm dd yyyy HH:MM:ss",
    shortDate: "m/d/yy",
    mediumDate: "mmm d, yyyy",
    longDate: "mmmm d, yyyy",
    fullDate: "dddd, mmmm d, yyyy",
    shortTime: "h:MM TT",
    mediumTime: "h:MM:ss TT",
    longTime: "h:MM:ss TT Z",
    isoDate: "yyyy-mm-dd",
    isoTime: "HH:MM:ss",
    isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
    isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
    dayNames: [
		"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
		"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
	],
    monthNames: [
		"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
		"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
	]
};

// For convenience...
Date.prototype.format = function (mask, utc) {
    return dateFormat(this, mask, utc);
};

/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// 控件处理方法
/////////////////////////////////////////////////////////////////////////////
//select未选中时变灰
function SetSelectNoneGray(sel) {
    sel.each(function () {
        if ($(this).val() == '')
        { $(this).addClass('gray'); }
        else $(this).removeClass('gray');
    });

    sel.change(function () {
        if ($(this).val() == '')
        { $(this).addClass('gray'); }
        else $(this).removeClass('gray');
    });
}

//设置地区选择级联选择框
function setZone(provSelect, citySelect, provValue, cityValue) {

    //citySelect.hide();
    provSelect.get(0).options.add(new Option("请选择", ""));
    citySelect.get(0).options.add(new Option("请选择", ""));

    $.getJSON("/SystemFunc/GetProvince", function (data) {
        if (data.length != 0) {
            provSelect.empty();
            provSelect.get(0).options.add(new Option("请选择", ""));
            $.each(data, function (i, item) {
                provSelect.get(0).options.add(new Option(item, item));
            });
        }
        provSelect.attr('value', provValue);

        if (provSelect.val() != '') {
            provSelect.removeClass('gray');
            citySelect.show();

            $.post("/SystemFunc/GetCitysByProvinceName/", { prov: provSelect.val() }, function (data) {
                if (data.length != 0) {
                    citySelect.empty();
                    citySelect.get(0).options.add(new Option("请选择", ""));
                    $.each(data, function (i, item) {
                        citySelect.get(0).options.add(new Option(item, item));
                    });
                }
                citySelect.attr('value', cityValue);
                if (citySelect.val() != '') citySelect.removeClass('gray');
            });
        }
    });

    provSelect.change(function () {
        if (provSelect.val() == '') {
            citySelect.hide();
            return;
        }
        citySelect.show();
        $.post("/SystemFunc/GetCitysByProvinceName/", { prov: provSelect.val() }, function (data) {
            if (data.length != 0) {
                citySelect.empty();
                citySelect.get(0).options.add(new Option("请选择", ""));
                $.each(data, function (i, item) {
                    citySelect.get(0).options.add(new Option(item, item));
                });
            }
        });
    });
};
//内容超出设置滚动条
function setscroll(container, num) {
    var de = document.documentElement;
    var db = document.body;
    var viewW = de.clientWidth == 0 ? db.clientWidth : de.clientWidth;
    var viewH = de.clientHeight == 0 ? db.clientHeight : de.clientHeight;
    var conheight = viewH - num;
    container.attr("style", "height:" + conheight + "px");

}

//设置鼠标进入清空
function setFocuseEmptyInput(input, str) {
    input.focusin(function () {
        if (input.val() == str) {
            input.attr("value", "");
            input.addClass("color:black"); 
        }
    });
    input.focusout(function () {
        if (input.val() == "") {
            input.attr("value", str);
            input.addClass("color:grey"); 
        }
    });
}
//设置鼠标进入清空2
function setFocusEmptyInput(textarea, input, str) {
    input.keyup(function (event) {
        if (event.keyCode == 8) {
            if (input.val().length < 1) {
                textarea.html(str);
            }
        }
        else {
            if (input.val().length > 0) {
                textarea.html("");
            }
        }
    });
}
//控件回车响应
function setenterdown(writter, callback) {
    writter.keydown(function (e) {
        //        if (e.keyCode == 13) {
        //            callback();
        //        }
        //兼容IE和FireFox
        e = e || window.event;
        var key = e ? (e.charCode || e.keyCode) : 0;
        if (key == 13) {
            callback();
        }
    });
}
//设置年月日
function setYMD(yy, m, d) {
    var year = $("#year");
    var month = $("#month");
    var day = $("#day");
    var date = new Date();
    var y = date.getFullYear().toString();
    MonDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    //初始化年份
    year.get(0).options.add(new Option("请选择", ""));
    for (var i = y - 10; i >= y - 60; i--) {
        year.get(0).options.add(new Option(i, i));
    }
    year.attr("value", yy); //1989应该是从数据库取出来的字段，使用ajax取出来
    //初始化月份
    month.get(0).options.add(new Option("请选择", ""));
    for (var i = 1; i < 13; i++) {
        month.get(0).options.add(new Option(i, i));
    }
    month.attr("value", m); //同上
    //初始化天数
    var mn = month.val();
    var ye = year.val();
    var n = MonDays[mn - 1];
    if ((ye % 4 == 0 && ye % 100 != 0) || ye % 400 == 0) {
        if (mn == 2) {
            n++;
        }
    }
    day.get(0).options.add(new Option("请选择", ""));
    for (var i = 1; i < n + 1; i++) {
        day.get(0).options.add(new Option(i, i));
    }
    day.attr("value", d);
    //月份改变时加载天数
    $("#month").change(function () {
        var mn = month.val();
        var ye = year.val();
        var n = MonDays[mn - 1];
        day.empty();
        if ((ye % 4 == 0 && ye % 100 != 0) || ye % 400 == 0) {
            if (mn == 2) {
                n++;
            }
        }
        day.get(0).options.add(new Option("请选择", ""));
        for (var i = 1; i < n + 1; i++) {
            day.get(0).options.add(new Option(i, i));
        }
    });

};
//聚焦控件后把光标放到最后
function focusControlCursorLast() {
    var e = event.srcElement;
    var r = e.createTextRange();
    r.moveStart("character", e.value.length);
    r.collapse(true);
    r.select();
}
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// 图片对象相关方法
/////////////////////////////////////////////////////////////////////////////
var photofunctions = {
    getsmallphotoname: function (filename) {
        if (filename == null || filename == "") { return "/pics/noicon.jpg" };
        return uploadpath + "/" + filename.substring(0, filename.lastIndexOf('.')) + '_s.jpg';
    },
    geticonphotoname: function (filename) {
        if (filename == null || filename == "") { return "/pics/noicon.jpg" };
        return uploadpath + "/" + filename.substring(0, filename.lastIndexOf('.')) + '_i.jpg';
    },
    getprofileiconphotoname: function (filename) {
        if (filename == null || filename == "") { return "/pics/noicon.jpg" };
        return filename.indexOf(uploadpath) > -1 ? filename : uploadpath + "/" + filename.substring(0, filename.lastIndexOf('.')) + '_p.jpg';
    },
    getnormalphotoname: function (filename) {
        if (filename == null || filename == "") { return "/pics/noicon.jpg" };
        return uploadpath + "/" + filename.substring(0, filename.lastIndexOf('.')) + '.jpg';
    },
    bindphotowallcenter: function (uploadpath, photowalldata, photowall, leftno, centersize) {
        var str = "";
        $.each(photowalldata, function (i) {
            if (i >= leftno & i < leftno + centersize)
                str += "<div style=\"float:left;cursor:pointer;\"><a id='photolink" + photowalldata[i].ID + "' onclick='photofunctions.openphoto(\"" + photowalldata[i].ID + "\");'><img src='" + photofunctions.geticonphotoname(photowalldata[i].FileName) + "'/></a></div>";
        });
        photowall.html(str);
    },
    bindwall: function (uploadpath, photowalldata, photowall, leftno, centersize) {
        var str = "";
        $.each(photowalldata, function (i) {
            if (i >= leftno & i < leftno + centersize)
                str += "<div><a id='photolink" + photowalldata[i].ID + "' onclick='photofunctions.openphoto(\"" + photowalldata[i].ID + "\");'><img src='" + photofunctions.geticonphotoname(photowalldata[i].FileName) + "'/></a></div>";
        });
        photowall.html(str);

    },
    openphoto: function (id) {
        if (imgid != null) {
            bindphoto(id);
        }
        else {
            $("#photolink" + id).attr("href", "/photo/show/" + id);
            $("#photolink" + id).attr("target", "_blank");
        }
    }
}

/////////////////////////////////////////////////////////////////////////////
// 外部平台相关方法
/////////////////////////////////////////////////////////////////////////////
var microConnOperation = {
    shareMessage: function (bindedPlatforms, content, url, fuc) {
        MicroConn.sendInfo(bindedPlatforms, content, '', url, function (result) { fuc(); });
    },
    sendInfo: function (platform, userbindedplatforms, content, pic, Url) {
        var hasbinded = userbindedplatforms.indexOf(platform);
        if (hasbinded >= 0) {
            MicroConn.sendInfo(platform, content, pic, Url, function (result) { });
            return 0;
        }
//        else {
//            if (platform.toString() == "1") {
//                actionprovider.openconn_sina(true, true, content);
//            }
//            if (platform.toString() == "2") {
//                actionprovider.openconn_txwb(true, true, content);
//            }
//            if (platform.toString() == "3") {
//                actionprovider.openconn_rr(true, true, content);
//            }
//            return 1;
//        }
    },
    sendSyncBoxInfo: function (syncBoxObjid, userbindedplatforms) {
        var syncBox = $("#" + syncBoxObjid);
        if (syncBox == null) return;
        var contentobjid1 = syncBox.attr("data-content1");
        var contentobjid2 = syncBox.attr("data-content2");
        var textPaserstr = syncBox.attr("data-type");

        var unbindplatforms = new Array();
        var issended = false;
        syncBox.find("input:checkbox:checked").each(function () {
            var platform = $(this).attr("data-platform");
            if (platform != null) {
                if (bindedPlatforms.indexOf(platform) < 0)
                    unbindplatforms.push(platform);
                else {
                    microConnOperation.sendInfo(platform, userbindedplatforms, microConnOperation.getControlContent(textPaserstr, contentobjid1, contentobjid2), "http://www.moooyo.com");
                    issended = true;
                }
            }
        });
        for (i = 0; i < unbindplatforms.length; i++) {
            if (unbindplatforms[i].toString() == "1") {
                actionprovider.openconn_sina(true, true, microConnOperation.getControlContent(textPaserstr, contentobjid1, contentobjid2));
            }
            if (unbindplatforms[i].toString() == "2") {
                actionprovider.openconn_txwb(true, true, microConnOperation.getControlContent(textPaserstr, contentobjid1, contentobjid2));
            }
            if (unbindplatforms[i].toString() == "3") {
                actionprovider.openconn_rr(true, true, microConnOperation.getControlContent(textPaserstr, contentobjid1, contentobjid2));
            }
        }
        if (issended) {
            $.jBox.tip("已成功转发", 'success');
        }
    },
    getControlContent: function (textPaserstr, contentobjid1, contentobjid2) {
        var content1 = getControlHtml($("#" + contentobjid1));
        var content2 = getControlHtml($("#" + contentobjid2));
        var textPaser = eval(textPaserstr);
        var textPaser = eval(textPaserstr);
        var content = textPaser(new Array(content1, content2));
        return content;
    },
    getControlContentAndSend: function (platform, userbindedplatforms, textPaserstr, contentobjid1, contentobjid2) {
        var content = microConnOperation.getControlContent(textPaserstr, contentobjid1, contentobjid2);
        if (content == "") return;
        var result = microConnOperation.sendInfo(platform, userbindedplatforms, content, "http://www.moooyo.com");
        if (result == 0) {
            $.jBox.tip("已成功转发", 'success');
        }
    },
    bindSyncBox: function () {
        $("span[name='syncBox']").each(function () {
            var title = $(this).attr("data-title");
            microConnOperation.getSyncBox($(this), title);
        });
    },
    getSyncBox: function (container, title) {
        var sinaflag = "";
        var txflag = "";
        var rrflag = "";
        if (bindedPlatforms.indexOf("1") >= 0) sinaflag = "checked";
        if (bindedPlatforms.indexOf("2") >= 0) txflag = "checked";
        if (bindedPlatforms.indexOf("3") >= 0) rrflag = "checked";

        var str = "";
        str += "<div class='fl mt5' name='synccbs'>";
        str += "<span class='cblue ml10 fl'>" + title + ":</span>";
        str += "<input name='sinacb' type='checkbox' " + sinaflag + " class='ml5 fl' data-platform='1'></input><span class='stico st_button stico_tsina ml5 fl'></span>";
        str += "<input name='txcb' type='checkbox' " + txflag + " class='ml5 fl' data-platform='2'></input><span class='stico st_button stico_fav ml5 fl'></span>";
        str += "<input name='rrcb' type='checkbox' " + rrflag + " class='ml5 fl' data-platform='3'></input><span class='stico st_button stico_fav ml5 fl'></span>";
        str += "</div>";
        container.html(str);
    },
    bindShareBox: function () {
        $("span[name='shareBox']").each(function () {
            var title = $(this).attr("data-title");
            var contentobjid1 = $(this).attr("data-content1");
            var contentobjid2 = $(this).attr("data-content2");
            var textPaserstr = $(this).attr("data-type");
            microConnOperation.getShareBox($(this), title, textPaserstr, contentobjid1, contentobjid2);
        });
    },
    getShareBox: function (container, title, textPaserstr, contentobjid1, contentobjid2) {
        var str = "";
        str += "<div class='shareto_toolbox'>";
        if (title != "")
            str += "<span class='cblue fl'>" + title + ":</span>";
        str += "<a href='javascript:void(0)' onclick='microConnOperation.getControlContentAndSend(\"1\",bindedPlatforms,\"" + textPaserstr + "\",\"" + contentobjid1 + "\",\"" + contentobjid2 + "\",\"\")'><span class='stico st_button stico_tsina'></span></a>";
        str += "<a href='javascript:void(0)' onclick='microConnOperation.getControlContentAndSend(\"2\",bindedPlatforms,\"" + textPaserstr + "\",\"" + contentobjid1 + "\",\"" + contentobjid2 + "\",\"\")'><span class='stico st_button stico_fav'></span></a>";
        str += "<a href='javascript:void(0)' onclick='microConnOperation.getControlContentAndSend(\"3\",bindedPlatforms,\"" + textPaserstr + "\",\"" + contentobjid1 + "\",\"" + contentobjid2 + "\",\"\")'><span class='stico st_button stico_rr'></span></a>";
        str += "</div>";
        container.html(str);
    },
    ShareTxTweet: function () {
        var title = '米柚网';
        var url = encodeURI('http://www.moooyo.com');
        var appkey = '333cf198acc94876a684d043a6b48e14';
        var site = encodeURI;
        var url = 'http://v.t.qq.com/share/share.php?title=' + title + '&url=' + url + '&appkey=' + appkey + '&site=' + site;
        var width = window.screen.width;
        var height = window.screen.height;
        var left = (width - height) / 2;
        window.open(url, '分享到腾讯微博', 'width=' + height + ', height=' + height + ', top=0, left=' + left + ', toolbar=no, menubar=no, scrollbars=no, location=yes, resizable=no, status=no');
    },
    ShareSinaTweet: function () {
        var title = '米柚网';
        var url = encodeURI('http://www.moooyo.com');
        var url = 'http://v.t.sina.com.cn/share/share.php?title=' + title + '&url' + url;
        var width = window.screen.width;
        var height = window.screen.height;
        var left = (width - height) / 2;
        window.open(url, '分享到新浪微博', 'width=' + height + ', height=' + height + ', top=0, left=' + left + ', toolbar=no, menubar=no, scrollbars=no, location=yes, resizable=no, status=no')
    }
}
function getControlHtml(control) {
    if (control == null) return "";

    var str = "";
    str = control.val();
    if (str == null || str == "") str = control.html();
    if (str == null) return "";
    return str;
}
var shareInfos = {
    interestQuestShare: function (contents) {
        if (contents.length < 1) return "";
        var str = "狗仔队追问我“{0}”，我都不知道该怎么回答。你会怎么说？";
        return String.format(str, contents[0]);
    },
    interviewQuestShare: function (contents) {
        if (contents.length < 1) return "";
        var str = "狗仔队追问我“{0}”，我都不知道该怎么回答。你会怎么说？";
        return String.format(str, contents[0]);
    },
    interviewAnswerShare: function (contents) {
        if (contents.length < 2) return "";
        var str = "狗仔队追问我“{0}”，我说“{1}”，有才吧。看这里";
        return String.format(str, contents[0], contents[1]);
    },
    interestShare: function (contents) {
        if (contents.length < 1) return "";
        var str = "喜欢{0}的，有木有？";
        return String.format(str, contents[0]);
    },
    conveneMyFans: function (contents) {
        var str = "我在[米柚]网发起号召，大家都来成为我的铁杆粉丝吧！";
        return str;
    }
}

/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// 分页相关方法
/////////////////////////////////////////////////////////////////////////////
function profilePaging(pageno, pagesize, pagecount, getfunc) {
    var str = "";
    if ((pageno < 1) || (pagecount <= 1)) {
        return;
    }
    str = str + "<div class=\"paging\">";
    str = str + "<a href=\"javascript:void(0);\" data-i=\"" + pageno + "\" onclick=\"javascript:" + getfunc + "(" + pagesize + ", 1);\"><<首页</a>";
    if (pageno == 1) {
        str = str + "<span class=\"prev\">&nbsp;<前页</span>";
    }
    else {
        str += "<a href=\"javascript:void(0);\" data-i=\"" + pageno + "\" onclick=\"javascript:" + getfunc + "(" + pagesize + ", " + (pageno - 1) + ");\">&nbsp;<前页</a>";
    }
    if (pageno == pagecount) {
        str = str + "<span class=\"next\">&nbsp;后页></span>";
    }
    else {
        str = str + "<a href=\"javascript:void(0);\" data-i=\"" + pageno + "\" onclick=\"javascript:" + getfunc + "(" + pagesize + ", " + (pageno + 1) + ");\">&nbsp;后页><a>";
    }
    str = str + "<a href=\"javascript:void(0);\" data-i=\"" + pageno + "\" onclick=\"javascript:" + getfunc + "(" + pagesize + ", " + pagecount + ");\">&nbsp;尾页>></a>";
    str = str + "</div>";
    return str;
}
function profileSmallPaging(pageno, pagesize, pagecount, getfunc) {
    var str = "";
    if ((pageno < 1) || (pagecount <= 1)) {
        return;
    }
    str = str + "<div class=\"paging\">";
    if (pageno == 1) {
        str = str + "<span class=\"prev\">&nbsp;<前页</span>";
    }
    else {
        str += "<a href=\"javascript:void(0);\" data-i=\"" + pageno + "\" onclick=\"javascript:" + getfunc + "(" + pagesize + ", " + (pageno - 1) + ");\">&nbsp;<前页</a>";
    }
    if (pageno == pagecount) {
        str = str + "<span class=\"next\">&nbsp;后页></span>";
    }
    else {
        str = str + "<a href=\"javascript:void(0);\" data-i=\"" + pageno + "\" onclick=\"javascript:" + getfunc + "(" + pagesize + ", " + (pageno + 1) + ");\">&nbsp;后页><a>";
    }
    str = str + "</div>";
    return str;
}
function profileQueryStrPaging(pageno, pagecount, pageurl) {
    var pagger = "<div class=\"pagger\">";
    if (pageno == 1)
        pagger += "<span class='prev'>〈前页</span>";
    else
        pagger += "<a href='" + pageurl + (pageno - 1) + "' data-i='" + (pageno - 1) + "'>〈前页</a>";
    if (pagecount < 11) {
        for (var i = 1; i <= pagecount & i < 11; i++) {
            if (pageno != i)
                pagger += "<a href='" + pageurl + i + "' data-i='" + i + "'>" + i + "</a>";
            else
                pagger += "<span class='thispage'>" + i + "</span>";
        }
    }
    else {
        var begin = pageno - 4;
        var end = pageno + 4;
        for (var i = 1; i <= pagecount; i++) {
            if (pageno < 6 & i < 10) {
                if (pageno != i)
                    pagger += "<a href='" + pageurl + i + "' data-i='" + i + "'>" + i + "</a>";
                else
                    pagger += "<span class='thispage'>" + i + "</span>";

                if (i == 9 & pagecount > 12)
                    pagger += "<span class='break'> ... </span>";

                continue;
            }
            if (pageno > pagecount - 7 & i > pagecount - 10) {
                if (i == pagecount - 9 & pagecount > 12)
                    pagger += "<span class='break'> ... </span>";
                if (pageno != i)
                    pagger += "<a href='" + pageurl + i + "' data-i='" + i + "'>" + i + "</a>";
                else {
                    pagger += "<span class='thispage'>" + i + "</span>";
                }
                continue;
            }
            if ((i <= begin || i > end) & i == pagecount - 10) continue;
            if (i < 3) {
                if (pageno != i)
                    pagger += "<a href='" + pageurl + i + "' data-i='" + i + "'>" + i + "</a>";
                else {
                    pagger += "<span class='thispage'>" + i + "</span>";
                }
                continue;
            }
            if (i >= begin & i <= end) {
                if (i == begin)
                    pagger += "<span class='break'> ... </span>";
                if (pageno != i)
                    pagger += "<a href='" + pageurl + i + "' data-i='" + i + "'>" + i + "</a>";
                else
                    pagger += "<span class='thispage'>" + i + "</span>";
                if (i == end) {
                    pagger += "<span class='break'> ... </span>";
                }
                continue;
            }
            if (i > pagecount - 2) {
                if (pageno != i)
                    pagger += "<a href='" + pageurl + i + "' data-i='" + i + "'>" + i + "</a>";
                else {
                    pagger += "<span class='thispage'>" + i + "</span>";
                }
                continue;
            }
        }
    }
    if (pageno == pagecount)
        pagger += "<span class='next'>后页〉</span>";
    else {
        pagger += "<a href='" + pageurl + (pageno + 1) + "' data-i='" + (pageno + 1) + "'>后页〉</a>";
    }
    return pagger + "</div>";
}

/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// 变量对象定义
/////////////////////////////////////////////////////////////////////////////
//用户相关
var alreadylogin;
var member; //用户
var me; //登录用户ID
var nickname; //昵称
var sex; //性别
var requestMember; //请求访问用户
var mid; //访问的用户ID
var isMe; //是否是本人访问
var unReadTotal; //未读总量
var unReadMsgCount; //未读消息数量
var unReadBeenViewedTimes; //未读来访数量
var unReadBeenFavorCount; //未读被关注数量
//相册相关
var photosdata; //相册数据
var phototype; //访问的相册类别
var uploadpath; //上传文件路径
var imgid; //显示的图片ID
var photourl; //显示的图片URL
var photocomments; //相片评论json对象
//照片墙相关
var photowalldata; //照片墙相册数据
//用户关联
var vistors; //来访用户数据
var openedwin;
//操作相关
var mousePos; //鼠标坐标 mousePos.x 或 mousePos.y
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// 用户操作活动封装
/////////////////////////////////////////////////////////////////////////////
var actionprovider = {
    open: function (url, title, width, height) {
        var dheight = height - 27;
        $.jBox("iframe:" + url,
            {
                title: title,
                width: width,
                height: height,
                showScrolling: false,
                opacity: 0.3,
                iframeScrolling: 'auto',
                persistent: false,
                buttons: {},
                loaded: function (h) {
                    h.attr("style", "overflow-y:hidden");
                    h.attr("style", "height:" + dheight + "px");
                }
            })
    },
    openmsg: function (youid) {
        if (openedwin) {
            if (!openedwin.closed)
                openedwin.close();
        }
        openedwin = openwindow("/msg/msgs/" + youid, "聊天", 900, 600);
    },
    openhi: function (youid) {
        actionprovider.open("/member/SayHi?you=" + youid, "打个招呼", 450, 350);
    },
    openlogin: function (jumpUrl) {
        actionprovider.open("/Account/alertlogin" + ((jumpUrl != "" && jumpUrl != "null") ? "?ReturnUrl=" + jumpUrl : ""), "用户登录", 450, 270);
    },
    openCustomSmallPicture: function (getjump, photoRelativePath, photoType) {
        actionprovider.open("/Shared/CustomSmallPicture?getjump=" + getjump + (photoRelativePath == null ? "" : "&photoRelativePath=" + photoRelativePath + "") + "&phototype=" + photoType, "剪裁图片", 750, 530);
    },
    //添加了一个ifshare参数：是否分享头像到微博
    openCustomBigPicture: function (getjump, photoRelativePath, photoType, skintype, pictureproportion, defaultwidth, defaultheight, contentid) {
        actionprovider.open("/Shared/CustomBigPicture?getjump=" + getjump + (photoRelativePath == null ? "" : "&photoRelativePath=" + photoRelativePath + "") + "&phototype=" + photoType + (skintype == null ? "" : "&skintype=" + skintype) + (pictureproportion == null ? "" : "&pictureproportion=" + pictureproportion) + (defaultwidth == null ? "" : "&defaultwidth=" + defaultwidth) + (defaultheight == null ? "" : "&defaultheight=" + defaultheight) + (contentid == null ? "" : "&contentid=" + contentid), "剪裁图片", 800, 530);
    },
    //    openCustomSmallPicture: function (getjump, photoRelativePath, photoType, skintype) {
    //        actionprovider.open("/Shared/CustomSmallPicture?getjump=" + getjump + (photoRelativePath == null ? "" : "&photoRelativePath=" + photoRelativePath + "") + "&phototype=" + photoType + (skintype == null ? "" : "&skintype=" + skintype + "") + "", "自定义头像", 850, 600);
    //    },
    //    openskill: function (containername) {
    //        actionprovider.open("/member/selectskill?containername=" + containername, "选择才艺", 460, 330);
    //    },
    openiwants: function (containername) {
        actionprovider.open("/member/SelectIWant?containername=" + containername, "选择想做的事", 460, 330);
    },
    openFansGroupName: function () {
        actionprovider.open("/member/SetFansGroupName", "设置粉丝团的名称", 450, 250);
    },
    opencalladmin: function (mid, tp) {
        actionprovider.open("/Admin/CallAdmin?id=" + mid + "&tp=" + tp, "联系管理员", 450, 350);
    },
    openup: function () {
        //        actionprovider.open("/Member/Upgrade", "升级", 650, 480);
    },
    openverifyemail: function () {
        actionprovider.open("/Setting/VerifyEmail", "", 600, 420);
    },
    openconn_sina: function (isbinding, issendingcontent, content) {
        actionprovider.open("/MicroConn/ConnectToSinaWeibo?isbinding=" + isbinding + "&isSendingContent=" + issendingcontent + "&content=" + content, "新浪微博登录", 650, 450);
    },
    openconn_txwb: function (isbinding, issendingcontent, content) {
        actionprovider.open("/MicroConn/ConnectToTXWeibo?isbinding=" + isbinding + "&isSendingContent=" + issendingcontent + "&content=" + content, "腾讯微博登录", 830, 630);
    },
    openconn_rr: function (isbinding, issendingcontent, content) {
        actionprovider.open("/MicroConn/ConnectToRenRen?isbinding=" + isbinding + "&isSendingContent=" + issendingcontent + "&content=" + content, "人人帐号登录", 830, 630);
    },
    openconn_qq: function () {
        actionprovider.open("/MicroConn/ConnectToQQ", "新浪微博登录", 650, 450);
    },
    openconn_txwb: function (isbinding, issendingcontent, content) {
        actionprovider.open("/MicroConn/ConnectToTXWeibo?isbinding=" + isbinding + "&isSendingContent=" + issendingcontent + "&content=" + content, "腾讯微博登录", 830, 630);
    }
}
/////////////////////////////////////////////////////////////////////////////



//兴趣悬浮标签相关方法
var ifmoveLabel = false;
var ifmoveAuthorCard = false;
var interestOverTimeoutID;
var interestCenter = {
    bindinterestLabel: function (ulobj) {
        var left = 0, top = 0;
        ulobj.mouseenter(function () {
            var inteProfile = $(this);
            var interestID = inteProfile.attr("data-interestid");
            var interDivId = "interestedAuthorCard" + interestID;
            $(this).mousemove(function (e) {
                left = e.pageX + 2;
                top = e.pageY + 2;
            });

            interestOverTimeoutID = setTimeout(function () {
                ifmoveLabel = true;
                if ($("#" + interDivId).length > 0) {
                    //名片溢出处理
                    var interInfoWidth = $("#" + interDivId).width(), interInfoHeight = $("#" + interDivId).height();
                    var scrolltop = document.documentElement.scrollTop + document.body.scrollTop;
                    var left2 = left, top2 = top;
                    var clientwidth = document.documentElement.clientWidth;
                    var clientheight = document.documentElement.clientHeight;
                    left2 = (left2 + interInfoWidth + 23) > clientwidth ? left2 - (left2 + interInfoWidth - clientwidth) - 23 : left2;
                    top2 = (top2 + interInfoHeight + 24) > (clientheight + scrolltop) ? (top2 - ((top2 + interInfoHeight) - (clientheight + scrolltop)) - 24) : top2;
                    //                                        if (left < (interInfoWidth + 23)) {
                    //                                            left2 = interInfoWidth + 23;
                    //                                        }
                    //                                        else if ((left + (interInfoWidth + 23)) > document.documentElement.clientWidth) {
                    //                                            left2 = document.documentElement.clientWidth - (interInfoWidth + 23);
                    //                                        }
                    //                                        else {
                    //                                            left2 = left;
                    //                                        }
                    //                                        if (top < (interInfoHeight + 24)) {
                    //                                            top2 = interInfoHeight + 24;
                    //                                        }
                    //                                        else if ((top + (interInfoHeight + 24)) > document.documentElement.clientHeight + document.documentElement.scrollTop + document.body.scrollTop) {
                    //                                            top2 = document.documentElement.clientHeight - (interInfoHeight + 24) + document.documentElement.scrollTop + document.body.scrollTop;
                    //                                        }
                    //                                        else {
                    //                                            top2 = top;
                    //                                        }
                    $("#" + interDivId).css({ "left": left2, "top": top2 });
                    $("#" + interDivId).show();
                }
                else {
                    $.ajaxSetup({ async: false });
                    interestCenter.interestLabelControl(interestID, left, top);
                }
            }, 500);
            ulobj.mouseleave(function () {
                clearTimeout(interestOverTimeoutID);
                var interId = $(this).attr("data-interestid");
                ifmoveLabel = false;
                interestCenter.interestmouseleave("interestedAuthorCard" + interId);
            });
        });
    },
    interestmouseleave: function (inteAuthorCardId) {
        var hiddenauthercard = setTimeout(function () {
            if (!ifmoveAuthorCard && !ifmoveLabel) {
                $("#" + inteAuthorCardId).hide();
            }
        }, 200);
    },
    interestLabelControl: function (iId, left, top) {
        var str = "";
        interestCenterProvider.getInterest(iId, function (data) {
            var objs = $.parseJSON(data);
            str += "<div id=\"interestedAuthorCard" + iId + "\" class=\"hot-inter-infor2\" style=\"left:" + left + "px; top:" + top + "px;\">";
            str += "<div>";
            str += "<div class=\"creat-inter-mbber clearfix\">";
            str += "<div class=\"inter50 fl\"><a href=\"/InterestCenter/InterestFans?iId=" + objs.ID + "\" target=\"_blank\"><img src=\"" + photofunctions.geticonphotoname(objs.ICONPath) + "\" width=\"50\" height=\"50\" border=\"0\" class=\"imgWH50 fl\" /></a></div>";
            str += "<div class=\"creat-infor ml10 fl\">";
            str += "<p class=\"name\"><a href=\"/InterestCenter/InterestFans?iId=" + objs.ID + "\" target=\"_blank\" class=\"fl\">" + (objs.Title.length > 7 ? objs.Title.substring(0, 7) + "<span class=\"letspa--3\">...</span>" : objs.Title) + "</a><span class=\"fl mr15\">" + objs.FansCount + "个粉丝</span><br><span id=\"isFans" + iId + "\" class=\"ml15\"></span></p>";
            str += "<div class=\"mt5\" style=\"width:200px;\">" + objs.Content.cut(30) + "</div>";
            str += "</div>";
            str += "</div>";
            str += "<div class=\"else-ask\">";
            $.ajaxSetup({ async: false });
            interestCenterProvider.getInterestWenwen(iId, function (result) {
                var wenwens = $.parseJSON(result);
                var index = Math.random() * wenwens.length;
                if (wenwens.length > 0) {
                    str += "<h4>关于TA的话题</h4>";
                    str += " <p class='mt5'><img src=\"" + photofunctions.geticonphotoname(wenwens[parseInt(index.toString())].Creater.ICONPath) + "\" width=\"25\" height=\"25\" border=\"0\" class=\"imgWH25\" />" + (wenwens[parseInt(index.toString())].Content.length > 18 ? wenwens[parseInt(index.toString())].Content.substring(0, 18) + "<span class=\"letspa--3\">...</span>" : wenwens[parseInt(index.toString())].Content) + "</p>";
                }
            });
            $.ajaxSetup({ async: true });
            str += "</div></div></div>";
            interestCenterFunctions.getMemberInterest(iId);
            $("body").append(str);
            //名片溢出处理
            var interInfoWidth = $("#interestedAuthorCard" + iId).width(), interInfoHeight = $("#interestedAuthorCard" + iId).height();
            var scrolltop = document.documentElement.scrollTop + document.body.scrollTop;
            var left2 = left, top2 = top;
            var clientwidth = document.documentElement.clientWidth;
            var clientheight = document.documentElement.clientHeight;
            left2 = (left2 + interInfoWidth + 23) > clientwidth ? left2 - (left2 + interInfoWidth - clientwidth) - 23 : left2;
            top2 = (top2 + interInfoHeight + 24) > (clientheight + scrolltop) ? (top2 - ((top2 + interInfoHeight) - (clientheight + scrolltop)) - 24) : top2;
            //            var interInfoWidth = $("#interestedAuthorCard" + iId).width(), interInfoHeight = $("#interestedAuthorCard" + iId).height();
            //            var left2, top2;
            //            if (left < (interInfoWidth + 23)) {
            //                left2 = (interInfoWidth + 23);
            //            }
            //            else if ((left + (interInfoWidth + 23)) > document.documentElement.clientWidth) {
            //                left2 = document.documentElement.clientWidth - (interInfoWidth + 23);
            //            }
            //            else {
            //                left2 = left;
            //            }
            //            if (top < (interInfoHeight + 24)) {
            //                top2 = (interInfoHeight + 24);
            //            }
            //            else if ((top + (interInfoHeight + 24)) > document.documentElement.clientHeight + document.documentElement.scrollTop + document.body.scrollTop) {
            //                top2 = document.documentElement.clientHeight - (interInfoHeight + 24) + document.documentElement.scrollTop + document.body.scrollTop;
            //            }
            //            else {
            //                top2 = top;
            //            }
            $("#interestedAuthorCard" + iId).css({ "left": left2, "top": top2 });

            $("#interestedAuthorCard" + iId).mouseenter(function () {
                ifmoveAuthorCard = true;
            });
            $("#interestedAuthorCard" + iId).mouseleave(function () {
                ifmoveAuthorCard = false;
                interestCenter.interestmouseleave("interestedAuthorCard" + iId);
            });
        });
    }
}

//用户信息悬浮标签相关方法
var headlabel = false;
var bodylabel = false;
var divid = "";
var TimeoutEvent;
var MemberInfoCenter = {
    BindDataInfo: function (divobj) {
        var left = 0, top = 0;
        divobj.mouseenter(function (e) {
            var inteProfile = $(this);
            var meID = inteProfile.attr("data_me_id");
            var heID = inteProfile.attr("data_member_id");
            var divid = heID + "showdiv";
            $(this).mousemove(function (e) { 
                left = e.pageX + 2; 
                top = e.pageY + 2; 
            });

            TimeoutEvent = setTimeout(function () {
                headlabel = true;
                if ($("#" + divid).length > 0) {
                    //名片溢出处理
                    var memberInfoWidth = $("#" + divid).width(), memberInfoHeight = $("#" + divid).height();
                    var left2, top2;
                    var scrolltop = document.documentElement.scrollTop + document.body.scrollTop;
                    var clientwidth = document.documentElement.clientWidth;
                    var clientheight = document.documentElement.clientHeight;
                    if (left < (memberInfoWidth + 6)) {
                        left2 = (memberInfoWidth + 6);
                    }
                    else if ((left + (memberInfoWidth + 6)) > clientwidth) {
                        left2 = clientwidth - (memberInfoWidth + 6);
                    }
                    else {
                        left2 = left;
                    }
                    if ((top + (memberInfoHeight + 13)) > clientheight + scrolltop) {
                        top2 = clientheight - (memberInfoHeight + 13) + scrolltop;
                    }
                    else {
                        top2 = top;
                    }

                    $("#" + divid).css({ "left": left2, "top": top2 });
                    $("#" + divid).show();
                }
                else {
                    $.ajaxSetup({ async: false });
                    MemberInfoCenter.showmemberinfo(heID, meID, left, top);

                }
            }, 500);
        });
        divobj.mouseleave(function () {
            clearTimeout(TimeoutEvent);
            var heID = $(this).attr("data_member_id");
            headlabel = false;
            MemberInfoCenter.membermouseleave(heID + "showdiv");
        });
    },
    membermouseleave: function (id) {

        var hiddebody = setTimeout(function () {
            if (!headlabel && !bodylabel) {
                $("#" + id).hide();
            }
        }, 200);
        //        var hiddebody = setTimeout(function () {
        //            if (!headlabel && !bodylabel) {
        //               
        //                $("#" + id).hide();                
        //            }
        //        }, 500);

    },
    showmemberinfo: function (heid, meid, left, top) {
        var str = "";

        memberprovider.getMemberInfo(heid, function (data) {
            var objs = $.parseJSON(data);
            var sex = objs.Sex == 1 ? "男" : "女";
            var age = objs.Age == "" ? "" : objs.Age + "岁";
            var memberHeight = parseInt(objs.Height) == "NaN" ? objs.Height + "cm" : "";
            var memberCareer = $.trim(objs.Career) == "问我" ? "" : objs.Career;
            var distance = "";
            if (objs.Lat != 0 && objs.Lng != 0) {
                $.ajaxSetup({ async: false });
                memberprovider.getMemberDistance(meid, heid, function (result) {
                    distance = $.parseJSON(result);
                });
                $.ajaxSetup({ async: true });
            }
            else {
                distance = "未知";
            }
            var img = "<div class=\"membericon\"><img onclick=\"javascript:window.open('/Content/TaContent/" + heid + "')\" alt=\"头像\" id=\"iconImgOO\" width=\"50\" height=\"50\" src=\"" + objs.ICONPath + "\"/></div>";
            str += "<div id=\"" + heid + "showdiv" + "\" class=\"memberinfo\" style=\"left:" + left + "px; top:" + top + "px;\" id=\"aa\">";
            str += img;
            str += "<div class=\"memberin\"><a href=\"/Content/TaContent/" + heid + "\" target=\"_blank\">" + objs.Name + "</a>&nbsp;&nbsp;" + objs.MemberLevel + "<br/>" + age + "&nbsp;" + sex + "&nbsp;&nbsp;" + memberHeight + "&nbsp;" + memberCareer + "<br/>" + objs.City + "&nbsp;距离" + distance + "<br/></div>";

            str += "<div class=\"memberimg\">";
            if (objs.IsRealPhotoIdentification) {
                str += " <img src=\"/upload/photoreal.gif\" alt=\"已经通过视频认证\" title=\"已经通过视频认证\"/>";
            }
            else {
                str += " <img src=\"/upload/photonoreal.gif\" alt=\"未通过视频认证\" title=\"未通过视频认证\"/>";
            }
            if (objs.MemberType == 0) {
                str += "<img src=\"/upload/normal.gif\" alt=\"普通会员\" title=\"普通会员\"/>";
            }
            else if (objs.MemberType == 1) {
                str += "<img src=\"/upload/h_user.gif\" alt=\"高级会员\" title=\"高级会员\"/>";
            }
            else if (objs.MemberType == 2) {
                str += "<img src=\"/upload/vip.gif\" alt=\"米柚VIP\" title=\"米柚VIP\"/>";
            }
            //添加徽章
            if (objs.Badgelist) {
                $(objs.Badgelist).each(function (index, val) {
                    if (val.BadegStatus == 1 && val.BadegType == 1) {
                        str += "<img src=\"/pics/badge_superman.gif\" alt=\"米柚超人\" title=\"米柚超人\"/> ";
                    }
                });
            }
            str += "<div class=\"hot-bar clearfix\"><span class=\"fl\">人气：</span><b class=\"" + objs.Hot + "\"></b></div>";
            str += "<p class=\"cgreen\">" + (objs.Want == "" ? "TA还没有写\"我想\"。<a id='invertiwan' href=\"javascript:;\" onclick=\"Invert.iWant('" + objs.ID + "',$('#invertiwan'))\">邀请TA填写\"我想\"</a>" : "\"我想和一个" + (sex == "男" ? "女" : "男") + "孩" + objs.Want + "\"") + "</p>";
            str += "</div>";
            if (heid != meid) {
                str += "<ul class=\"memberul\">";
                str += "<li class=\"but nocolor\"><a href=\"javascript:void(0);\" onclick=\"actionprovider.openmsg('" + heid + "')\">私信</a></li>";
                str += "<li class=\"but nocolor\"><a onclick='actionprovider.openhi(\"" + heid + "\")' href=\"javascript:void(0);\">打招呼</a></li>";
                str += "<li class=\"but prevent\"><a href=\"javascript:void(0);\" onclick=\"member_i_functions.disablemember('" + meid + "')\">屏蔽</a></li>";

                $.ajaxSetup({ async: false });
                memberprovider.getMemberIsInFavor(meid, heid, function (isInfavor) {
                    if ($.parseJSON(isInfavor)) {
                        //str += "<li class=\"but cancel\"><a onclick=\"member_i_functions.deletefavormember('" + heid + "',$(this))\">" + "取消</a></li>";
                        str += "<li class=\"but\"><a class=\"delete-btn\" onclick=\"member_i_functions.deletefavormemberStyle3('" + heid + "',$(this))\"><i></i>取消</a></li>";
                    } else {
                        // str += "<li class=\"but attention\"><a onclick=\"member_i_functions.favormember" + "('" + heid + "',$(this))\">" + "关注</a></li>";
                        str += "<li class=\"but\"><a class=\"add-btn\" onclick=\"member_i_functions.favormemberStyle3('" + heid + "',$(this))\"><i></i>关注</a></li>";
                    }
                    var ifgz = $.parseJSON(isInfavor);
                });
                $.ajaxSetup({ async: true });

                str += "</ul>";
            }
            str += "</div>";
            $("body").append(str);
            //名片溢出处理
            var memberInfoWidth = $("#" + heid + "showdiv").width(), memberInfoHeight = $("#" + heid + "showdiv").height();
            var left2, top2;
            var scrolltop = document.documentElement.scrollTop + document.body.scrollTop;
            var clientwidth = document.documentElement.clientWidth;
            var clientheight = document.documentElement.clientHeight;
            if (left < (memberInfoWidth + 6)) {
                left2 = (memberInfoWidth + 6);
            }
            else if ((left + (memberInfoWidth + 6)) > clientwidth) {
                left2 = clientwidth - (memberInfoWidth + 6);
            }
            else {
                left2 = left;
            }
            //if (top < (memberInfoHeight + 15)) top2 = (memberInfoHeight + 15);
            if ((top + (memberInfoHeight + 13)) > clientheight + scrolltop) 
            {
                top2 = clientheight - (memberInfoHeight + 13) + scrolltop;
            }
            else {
                top2 = top;
            }
            $("#" + heid + "showdiv").css({ "left": left2, "top": top2 });

            $("#" + heid + "showdiv").mouseenter(function () {
                bodylabel = true;
            });
            $("#" + heid + "showdiv").mouseleave(function () {
                bodylabel = false;
                MemberInfoCenter.membermouseleave(heid + "showdiv");
            });
        });
    }
}

/////////////////////////////////////////////////////////////////////////////
// 系统标签
/////////////////////////////////////////////////////////////////////////////
var SystemMsgBoxes = {
    getSystemMsgBox: function (sHtml) {
        //系统消息div
        sHtml = sHtml.replace(/\[systemmsg_t1\]\s*(((?!")[\s\S])*?)(?:"[\s\S]*?)?\s*\[\/systemmsg_t1\]/ig, "<div style='padding:5px;background-color:#fcfcfc;color:#666;margin-top:5px;margin-bottom:5px;'>$1</div>");
        //链接div
        var smatch = /\[url_t1\]\s*(((?!")[\s\S])*?)(?:"[\s\S]*?)?\s*\[\/url_t1\]/ig;
        var matchstr = smatch.exec(sHtml);
        var linkstr = "";
        if (matchstr) {
            var s = matchstr[0].replace("[url_t1]", "");
            s = s.replace("[/url_t1]", "");
            var link = $.parseJSON(s);
            if (link) {
                linkstr += "<a class='ml10' href='" + link.url + "' target='_blank'>[" + link.text + "]</a>";
            }
        }
        sHtml = sHtml.replace(/\[url_t1\]\s*(((?!")[\s\S])*?)(?:"[\s\S]*?)?\s*\[\/url_t1\]/ig, linkstr);
        return sHtml;
    },
    clearSystemMsgBox: function (sHtml) {
        //去除systemmsgbox
        sHtml = sHtml.replace(/\[systemmsg_t1\]\s*(((?!")[\s\S])*?)(?:"[\s\S]*?)?\s*\[\/systemmsg_t1\]/ig, "");
        sHtml = sHtml.replace(/\[url_t1\]\s*(((?!")[\s\S])*?)(?:"[\s\S]*?)?\s*\[\/url_t1\]/ig, "");
        return sHtml;
    }
}

/*———————————————————————————————————————————————————*/
/* 页面对象相关方法
/*∨∨∨∨∨——————————————————————————————————————————————*/

//输入框自适应高度
function AutomaticHeight(obj, minheight) {
    obj.keydown(function () {
        if (minheight != null) {
            if (this.scrollHeight > minheight) 
                this.style.height = (this.scrollHeight + 20) + "px";
            else 
                this.style.height = minheight + "px";
        }
        else {
            this.style.height = (this.scrollHeight + 20) + "px";
        }
    });
    obj.keypress(function () {
        if (minheight != null) {
            if (this.scrollHeight > minheight) 
                this.style.height = (this.scrollHeight + 20) + "px";
            else 
                this.style.height = minheight + "px";
        }
        else {
            this.style.height = (this.scrollHeight + 20) + "px";
        } 
    });
    obj.keyup(function () {
        if (minheight != null) {
            if (this.scrollHeight > minheight) 
                this.style.height = (this.scrollHeight + 20) + "px";
            else 
                this.style.height = minheight + "px";
        }
        else {
            this.style.height = (this.scrollHeight + 20) + "px";
        }
    });
    obj.focus(function () {
        if (minheight != null) {
            if (this.scrollHeight > minheight) 
                this.style.height = (this.scrollHeight + 20) + "px";
            else 
                this.style.height = (minheight + 20) + "px";
        }
        else {
            this.style.height = (this.scrollHeight + 20) + "px";
        }
    });
    obj.blur(function () {
        obj.val(rtrim(obj.val()));
        if (minheight != null) {
            if (this.scrollHeight > minheight) 
                this.style.height = (this.scrollHeight) + "px";
            else 
                this.style.height = (minheight) + "px";
        }
        else {
            this.style.height = (this.scrollHeight) + "px";
        }
    });
}

//按比例缩小图片尺寸(javascript)
//var imgsize = 105;
//function ImageZoom(imgobj, size) {
//    var image = new Image();
//    image.src = imgobj.src;
//    if (image.width < imgsize && image.height < imgsize) {
//        imgobj.width = image.width;
//        imgobj.height = image.height;
//    }
//    else {
//        var imagewidth = image.width;
//        var imageheight = image.height;
//        if (imagewidth > imageheight) {
//            imgobj.width = imgsize;
//            imgobj.height = imageheight * (imgsize / imagewidth);
//        }
//        else if (imagewidth < imageheight) {
//            imgobj.width = imagewidth * (imgsize / imageheight);
//            imgobj.height = imgsize;
//        }
//        else {
//            imgobj.width = imgsize;
//            imgobj.height = imgsize;
//        }
//    }
//}
////固定宽度并按比例缩小图片尺寸(javascript)
//function FixedImageWidth(imgobj, fixedImageWith) {
//    var image = new Image();
//    image.src = imgobj.src;
//    if (image.width < fixedImageWith && image.height < fixedImageWith) {
//        imgobj.width = image.width;
//        imgobj.height = image.height;
//    }
//    else {
//        var imagewidth = image.width;
//        var imageheight = image.height;
//        if (imagewidth > imageheight) {
//            imgobj.width = fixedImageWith;
//            imgobj.height = imageheight * (fixedImageWith / imagewidth);
//        }
//        else if (imagewidth < imageheight) {
//            imgobj.width = imagewidth * (fixedImageWith / imageheight);
//            imgobj.height = fixedImageWith;
//        }
//        else {
//            imgobj.width = fixedImageWith;
//            imgobj.height = fixedImageWith;
//        }
//    }
//}
//按比例缩小图片尺寸(jquery)
function ImageZoomToJquery(imgobj, size) {
    var image = new Image();
    image.src = imgobj.attr("src");
    if (image.width < size && image.height < size) {
        imgobj.width(image.width);
        imgobj.height(image.height);
    }
    else {
        var imagewidth = image.width;
        var imageheight = image.height;
        if (imagewidth > imageheight) {
            imgobj.width(size);
            imgobj.height(imageheight * (size / imagewidth));
        }
        else if (imagewidth < imageheight) {
            imgobj.width(imagewidth * (size / imageheight));
            imgobj.height(size);
        }
        else {
            imgobj.width(size);
            imgobj.height(size);
        }
    }
}
//按比例缩小图片尺寸(jquery)
//function ImageZoomToJqueryToHTML(imgobj, size) {
//    var image = new Image();
//    image.src = imgobj.src;
//    if (image.width < size && image.height < size) {
//    }
//    else {
//        var imagewidth = image.width;
//        var imageheight = image.height;
//        if (imagewidth > imageheight) {
//            imgobj.width = size;
//            imgobj.height = imageheight * (size / imagewidth);
//        }
//        else if (imagewidth < imageheight) {
//            imgobj.width = imagewidth * (size / imageheight);
//            imgobj.height = size;
//        }
//        else {
//            imgobj.width = size;
//            imgobj.height = size;
//        }
//    }
//}

//var mouseleft = 0;
//var mousetop = 0;
//function mouseMove(ev) {
//    ev = ev || window.event;
//    var mousePos = mouseCoords(ev);
//    mouseleft = mousePos.x;
//    mousetop = mousePos.y;
//    //谷歌浏览器
//    if ($.browser.safari) {
//    }
//    //opera浏览器
//    else if ($.browser.opera) {
//    }
//    //IE浏览器
//    else if ($.browser.msie) {
//    }
//    //其他浏览器
//    else {
//        mousetop += document.body.scrollTop;
//    }
//}
//function mouseCoords(ev) {
//    if (ev.pageX || ev.pageY) { 
//        return { x: ev.pageX, y: ev.pageY }; 
//    }
//    return {
//        x: ev.clientX, y: ev.clientY 
//    };
//}

//加载表情
//function showExpression(textobj) {
//    var expressiondivstr = "<div id=\"ExpressionDiv\">"
//        + "<img alt=\"\" title=\"[anj]\" src=\"/pics/Expression/anj.gif\"/>"
//        + "<img alt=\"\" title=\"[aom]\" src=\"/pics/Expression/aom.gif\"/>"
//        + "<img alt=\"\" title=\"[baiy]\" src=\"/pics/Expression/baiy.gif\"/>"
//        + "<img alt=\"\" title=\"[bis]\" src=\"/pics/Expression/bis.gif\"/>"
//        + "<img alt=\"\" title=\"[biz]\" src=\"/pics/Expression/biz.gif\"/>"
//        + "<img alt=\"\" title=\"[chah]\" src=\"/pics/Expression/chah.gif\"/>"
//        + "<img alt=\"\" title=\"[ciy]\" src=\"/pics/Expression/ciy.gif\"/>"
//        + "<img alt=\"\" title=\"[dak]\" src=\"/pics/Expression/dak.gif\"/>"
//        + "<img alt=\"\" title=\"[dang]\" src=\"/pics/Expression/dang.gif\"/>"
//        + "<img alt=\"\" title=\"[dao]\" src=\"/pics/Expression/dao.gif\"/>"
//        + "<img alt=\"\" title=\"[dey]\" src=\"/pics/Expression/dey.gif\"/>"
//        + "<img alt=\"\" title=\"[fad]\" src=\"/pics/Expression/fad.gif\"/>"
//        + "<img alt=\"\" title=\"[fank]\" src=\"/pics/Expression/fank.gif\"/>"
//        + "<img alt=\"\" title=\"[fend]\" src=\"/pics/Expression/fend.gif\"/>"
//        + "<img alt=\"\" title=\"[fendou]\" src=\"/pics/Expression/fendou.gif\"/>"
//        + "<img alt=\"\" title=\"[fenn]\" src=\"/pics/Expression/fenn.gif\"/>"
//        + "<img alt=\"\" title=\"[gang]\" src=\"/pics/Expression/gang.gif\"/>"
//        + "<img alt=\"\" title=\"[gouy]\" src=\"/pics/Expression/gouy.gif\"/>"
//        + "<img alt=\"\" title=\"[guz]\" src=\"/pics/Expression/guz.gif\"/>"
//        + "<img alt=\"\" title=\"[haix]\" src=\"/pics/Expression/haix.gif\"/>"
//        + "<img alt=\"\" title=\"[hanx]\" src=\"/pics/Expression/hanx.gif\"/>"
//        + "<img alt=\"\" title=\"[haq]\" src=\"/pics/Expression/haq.gif\"/>"
//        + "<img alt=\"\" title=\"[huaix]\" src=\"/pics/Expression/huaix.gif\"/>"
//        + "<img alt=\"\" title=\"[jie]\" src=\"/pics/Expression/jie.gif\"/>"
//        + "<img alt=\"\" title=\"[jingk]\" src=\"/pics/Expression/jingk.gif\"/>"
//        + "<img alt=\"\" title=\"[jingy]\" src=\"/pics/Expression/jingy.gif\"/>"
//        + "<img alt=\"\" title=\"[kea]\" src=\"/pics/Expression/kea.gif\"/>"
//        + "<img alt=\"\" title=\"[kel]\" src=\"/pics/Expression/kel.gif\"/>"
//        + "<img alt=\"\" title=\"[koubs]\" src=\"/pics/Expression/koubs.gif\"/>"
//        + "<img alt=\"\" title=\"[ku]\" src=\"/pics/Expression/ku.gif\"/>"
//        + "<img alt=\"\" title=\"[kuaikl]\" src=\"/pics/Expression/kuaikl.gif\"/>"
//        + "<img alt=\"\" title=\"[laoh]\" src=\"/pics/Expression/laoh.gif\"/>"
//        + "<img alt=\"\" title=\"[lengh]\" src=\"/pics/Expression/lengh.gif\"/>"
//        + "<img alt=\"\" title=\"[liul]\" src=\"/pics/Expression/liul.gif\"/>"
//        + "<img alt=\"\" title=\"[nang]\" src=\"/pics/Expression/nang.gif\"/>"
//        + "<img alt=\"\" title=\"[no]\" src=\"/pics/Expression/no.gif\"/>"
//        + "<img alt=\"\" title=\"[out]\" src=\"/pics/Expression/out.gif\"/>"
//        + "<img alt=\"\" title=\"[peif]\" src=\"/pics/Expression/peif.gif\"/>"
//        + "<img alt=\"\" title=\"[piez]\" src=\"/pics/Expression/piez.gif\"/>"
//        + "<img alt=\"\" title=\"[qinqin]\" src=\"/pics/Expression/qinqin.gif\"/>"
//        + "<img alt=\"\" title=\"[qoudl]\" src=\"/pics/Expression/qoudl.gif\"/>"
//        + "<img alt=\"\" title=\"[se]\" src=\"/pics/Expression/se.gif\"/>"
//        + "<img alt=\"\" title=\"[shuai]\" src=\"/pics/Expression/shuai.gif\"/>"
//        + "<img alt=\"\" title=\"[shui]\" src=\"/pics/Expression/shui.gif\"/>"
//        + "<img alt=\"\" title=\"[tiaop]\" src=\"/pics/Expression/tiaop.gif\"/>"
//        + "<img alt=\"\" title=\"[toux]\" src=\"/pics/Expression/toux.gif\"/>"
//        + "<img alt=\"\" title=\"[weiq]\" src=\"/pics/Expression/weiq.gif\"/>"
//        + "<img alt=\"\" title=\"[weix]\" src=\"/pics/Expression/weix.gif\"/>"
//        + "<img alt=\"\" title=\"[wos]\" src=\"/pics/Expression/wos.gif\"/>"
//        + "<img alt=\"\" title=\"[xia]\" src=\"/pics/Expression/xia.gif\"/>"
//        + "<img alt=\"\" title=\"[xianh]\" src=\"/pics/Expression/xianh.gif\"/>"
//        + "<img alt=\"\" title=\"[xianhdx]\" src=\"/pics/Expression/xianhdx.gif\"/>"
//        + "<img alt=\"\" title=\"[xin]\" src=\"/pics/Expression/xin.gif\"/>"
//        + "<img alt=\"\" title=\"[xins]\" src=\"/pics/Expression/xins.gif\"/>"
//        + "<img alt=\"\" title=\"[yinx]\" src=\"/pics/Expression/yinx.gif\"/>"
//        + "<img alt=\"\" title=\"[yiw]\" src=\"/pics/Expression/yiw.gif\"/>"
//        + "<img alt=\"\" title=\"[youhh]\" src=\"/pics/Expression/youhh.gif\"/>"
//        + "<img alt=\"\" title=\"[yun]\" src=\"/pics/Expression/yun.gif\"/>"
//        + "<img alt=\"\" title=\"[zaij]\" src=\"/pics/Expression/zaij.gif\"/>"
//        + "<img alt=\"\" title=\"[zan]\" src=\"/pics/Expression/zan.gif\"/>"
//        + "<img alt=\"\" title=\"[zhem]\" src=\"/pics/Expression/zhem.gif\"/>"
//        + "<img alt=\"\" title=\"[zhoum]\" src=\"/pics/Expression/zhoum.gif\"/>"
//        + "<img alt=\"\" title=\"[zhuak]\" src=\"/pics/Expression/zhuak.gif\"/>"
//        + "<img alt=\"\" title=\"[zoun]\" src=\"/pics/Expression/zoun.gif\"/>"
//        + "<img alt=\"\" title=\"[zuohh]\" src=\"/pics/Expression/zuohh.gif\"/>"
//        + "</div>";
//    $("body").append(expressiondivstr);
//    var obj = $("#ExpressionDiv");
//    obj.css({ "left": mouseleft + 10, "top": mousetop + 10 });
//    obj.css("display", "block");
//    if (textobj != null)
//        textobject = textobj;
//    obj.focus();
//    obj.children("img").click(function () { 
//        clickExpression(this.title, textobj); 
//    });
//    obj.blur(function () { clearExpression(); });
//    if ($("#topicimageupdate").html() != null) {
//        $("#topicimageupdate").css("display", "none");
//    }
//}
//点击插入表情
//function clickExpression(imgtitle, textobj) {
//    textobj.val(textobj.val() + imgtitle);
//}
//关闭并清除表情
//function clearExpression() {
//    var obj = $("#ExpressionDiv");
//    if (obj.html() != null) {
//        obj.css("display", "none");
//        obj.remove(); 
//    }
//}
//显示内容中的表情图片
//function getExpression(oldstr) {
//    var keys = "[anj],[aom],[baiy],[bis],[biz],[chah],[ciy],[dak],[dang],[dao],[dey],[fad],[fank],[fend],[fendou],[fenn],[gang],[gouy],[guz],[haix],[hanx],[haq],[huaix],[jie],[jingk],[jingy],[kea],[kel],[koubs],[ku],[kuaikl],[laoh],[lengh],[liul],[nang],[no],[out],[peif],[piez],[qinqin],[qoudl],[se],[shuai],[shui],[tiaop],[toux],[weiq],[weix],[wos],[xia],[xianh],[xianhdx],[xin],[xins],[yinx],[yiw],[youhh],[yun],[zaij],[zan],[zhem],[zhoum],[zhuak],[zoun],[zuohh]";
//    var keylist = keys.split(',');
//    for (var i = 0; i < oldstr.length; i++) {
//        var ifindex = false;
//        for (var j = 0; j < keylist.length; j++) {
//            var key = keylist[j];
//            if (oldstr.indexOf(key) >= 0) {
//                ifindex = true;
//                var keystr = key.substr(1, key.length - 2);
//                oldstr = oldstr.replace(key, "<img src=\"/pics/Expression/" + keystr + ".gif\"/>");
//            }
//        }
//        if (!ifindex) {
//            break;
//        }
//    }
//    return oldstr;
//}

//加载认证图标
function showrzimg(obj, width, height) {
    var rzimgstr = "";
    var stylestr = "style=\"float:left;width:" + width + "px;height:" + height + "px;\"";
    //加载视频认证图标
    if (obj.Creater.MemberPhoto != null) {
        rzimgstr += "<a href=\"/Setting/Authentica\" target=\"_blank\">";
        if (obj.Creater.MemberPhoto.IsRealPhotoIdentification) {
            rzimgstr += "<img src=\"/upload/photoreal.png\" alt=\"已经通过视频认证\" title=\"已经通过视频认证\" " + stylestr + "/>";
        }
        else {
            rzimgstr += "<img src=\"/upload/photonoreal.png\" alt=\"未通过视频认证\" title=\"未通过视频认证\" " + stylestr + "/>";
        }
        rzimgstr += "</a>";
    }
    //加载会员图标
    rzimgstr += "<a href=\"/Setting/AcctUpgrade\" target=\"_blank\">";
    if (obj.Creater.MemberType == 0) {
        rzimgstr += "<img src=\"/upload/normal.png\" alt=\"普通会员\" title=\"普通会员\" " + stylestr + "/>";
    }
    else if (obj.Creater.MemberType == 1) {
        rzimgstr += "<img src=\"/upload/h_user.png\" alt=\"高级会员\" title=\"高级会员\" " + stylestr + "/>";
    }
    else if (obj.Creater.MemberType == 2) {
        rzimgstr += "<img src=\"/upload/vip.png\" alt=\"米柚VIP\" title=\"米柚VIP\" " + stylestr + "/>";
    }
    else {
        rzimgstr += "<img src=\"/upload/normal.png\" alt=\"普通会员\" title=\"普通会员\" " + stylestr + "/>";
    }
    rzimgstr += "</a>";
    return rzimgstr;
}


///喜欢的点击、回复和内容的回复
//展开或关闭指定页面标签：id：要展开的对象id；textid：要展开的对象中的文本框的id（为空）
function showanswerdiv(id, textid) {
    var ifopen = $("#" + id).attr("data-ifopen");
    if (ifopen == "false") {
        $("#" + id).attr("data-ifopen", "true");
        $("#" + id).css("display", "block");
        if (textid != null) {
            $("#" + textid).focus();
            $("#" + textid).bind("keydown", function () {
                if (event.keyCode == 13) {
                    $(this).parent().parent().children("span.arrow_ans").children("a.blue02").click();
                }
            });
        }
    }
    else {
        $("#" + id).attr("data-ifopen", "false");
        $("#" + id).css("display", "none");
        if (textid != null) {
            $("#" + textid).unbind("keydown");
            $("#" + textid).val("");
        }
    }
    $("div.answeranswerdiv").attr("data-ifopen", "false");
    $("div.answeranswerdiv").css("display", "none");
    $("a.answeranswerbu").html("回应");
    $("div.answerdiv").attr("data-ifopen", "true");
    $("div.answerdiv").css("display", "block");
    //重新布局内容
    masonrynotimelinetoshowanswer();
}

//加载回复内容：id：内容编号；commentList：内容回复的集合；userID：当前登录用的编号
function showanswerHtml(id, commentList, userID) {
    var str = "";
    $.each(commentList, function (i) {
        str += "<div class=\"ans_com\">";
        str += "<div class=\"ans_compic\"><a href=\"/Content/TaContent/" + commentList[i].Creater.MemberID + "/all/1\" target=\"_blank\"><img src=\"" + photofunctions.getnormalphotoname(commentList[i].Creater.ICONPath) + "\" height=\"25\" width=\"25\" /></a></div>";
        str += "<div class=\"ans_span\">";
        str += "<span class=\"left_s\">";
        str += "<a href=\"/Content/TaContent/" + commentList[i].Creater.MemberID + "/all/1\" class=\"blue02\" target=\"_blank\">" + commentList[i].Creater.NickName + "</a>";
        var contentstr = replaceToN(commentList[i].Content);
        contentstr = contentstr.length > 100 ? contentstr.substr(0,100) + ".." : contentstr;
        str += "<font class=\"huise autoW\" id=\"autoW\">" + contentstr + "</font>";
        str += "<nobr><font class=\"gray01\">" + getTimeSpan(paserJsonDate(commentList[i].CreatedTime)) + "</font></nobr>";
        //如果回复的用户编号和当前登录的用户编号相同，不加载回应按钮
        if (commentList[i].MemberID != userID) {
            str += "<nobr><a class=\"gray01 answeranswerbu\" onclick=\"showcontentanswertext('" + commentList[i].ID + "','" + id + "','" + commentList[i].Creater.NickName + "','" + userID + "','" + commentList[i].MemberID + "',$(this))\">回应</a></nobr>";
        }
        str += "</span>";
        str += "</div>";
        str += "</div>";
        //如果回复的用户编号和当前登录的用户编号相同，不加载回应的输入框
        if (commentList[i].MemberID != userID) {
            str += "<div class=\"ans_com ans_txt answeranswerdiv\" id=\"contentanswertextdiv" + commentList[i].ID + "\" data-ifopen=\"false\" style=\"display:none;\"></div>";
        }
    });
    if (commentList.length >= 6) {
        $("#contentmore" + id).html("<span class=\"ans_right\">更多…</span>");
    }
    return str;
}
//点击回应的方法
function showcontentanswertext(answerid, contentid, nickname, userid, toMember,thisobj) {
    var answertextdiv = $("#contentanswertextdiv" + answerid);
    var contenttextdiv = $("#contenttextdiv" + contentid);
    if (answertextdiv.attr("data-ifopen") == "false") {
        contenttextdiv.attr("data-ifopen", "false");
        contenttextdiv.css("display", "none");
        var str = "";
        str += "<span><input type=\"text\" id=\"contentsanswer" + answerid + "\" class=\"ans_t\" value=\"回应" + nickname + "：\"/></span>";
        str += "<span class=\"arrow_ans\"><a class=\"blue02\" onclick=\"addcontentanswer('" + contentid + "','" + answerid + "','" + userid + "', '" + toMember + "')\">发送</a><em></em></span>";
        answertextdiv.html(str);
        answertextdiv.attr("data-ifopen", "true");
        answertextdiv.css("display", "block");
        $("#contentsanswer" + answerid).focus().focus(function () { focusControlCursorLast(); });
        $("#contentsanswer" + answerid).bind("keydown", function () {
            if (event.keyCode == 13) {
                $(this).parent().parent().children("span.arrow_ans").children("a.blue02").click();
            }
        });
        thisobj.html("取消");
    }
    else {
        contenttextdiv.attr("data-ifopen", "true");
        contenttextdiv.css("display", "block");
        answertextdiv.attr("data-ifopen", "false");
        answertextdiv.css("display", "none");
        $("#contentsanswer" + answerid).unbind("keydown");
        thisobj.html("回应");
    }
    //重新布局内容
    masonrynotimelinetoshowanswer();
}
//加载内容回复的方法
//function showcontentanswer(id, type, count, pageno, userid) {
//    var pagecount = count % 6 == 0 ? count / 6 : parseInt(count / 6) + 1;
//    if (pageno <= pagecount) {
//        CommentProvider.ShowComment(id, pageno, function (data) {
//            var data = $.parseJSON(data);
//            var AnswerList = data.commentList;
//            var AnswerCount = data.contentObject.AnswerCount;
//            var str = showanswerHtml(id, AnswerList, userid);
//            $("#" + type + id).val("");
//            $("#" + type + "answer" + id).html($("#" + type + "answer" + id).html() + str);
//        });
//    }
//}
//添加内容回复
function addcontentanswer(id, answerid, userid, toMember, iflikecomment) {
    var answeriobj = null;
    var content = "";
    if (answerid != null && answerid != "") {
        answeriobj = $("#contentsanswer" + answerid);
    }
    else if (iflikecomment != null && iflikecomment) {
        answeriobj = $("#contentlike" + id);
    }
    else {
        answeriobj = $("#content" + id);
    }
    if (answeriobj.val() == null) {
        answeriobj = $("#content" + id);
    }
    content = answeriobj.val();
    if (content != "") {
        answeriobj.val("");
        CommentProvider.AddComment(id, content, toMember, answerid, function (data) {
            var data = $.parseJSON(data);
            var AnswerList = data.contentObject.AnswerList;
            var AnswerCount = data.contentObject.AnswerCount;
            var str = showanswerHtml(id, AnswerList, userid);
            $("#contentanswer" + id).html(str);
            $("#contentanswercount" + id).html(AnswerCount + "");
            //重新布局内容
            masonrynotimelinetoshowanswer();
        });
    }
    else {
        answeriobj.focus();
        $.jBox.tip("请填写回复内容。", 'error');
    }
}
//添加内容喜欢重载(用于ContentDetail)
function addlikeinContentDetail(id, likename, likecountdiv,commentinputbox) {
    likecountdiv.html(parseInt(likecountdiv.html() == "" ? "0" : likecountdiv.html()) + 1);
    ContentProvider.AddContentLike(id,likename, function (data) {
        var data = $.parseJSON(data);
        if (data.toString() == "False" || data.toString() == "false") {
            likecountdiv.html(parseInt(likecountdiv.html() == "" ? "0" : likecountdiv.html()) - 1);
            $.jBox.tip("只能mo一次的", 'info');
        }
        else {
            $.jBox.tip("+1<br/>是否还要对Ta说点什么呢？", 'info');
            commentinputbox.focus();
        }
    });
}
function showContentAnswerInDetail(id, commentList, userID) {
    var str = "";
    $.each(commentList, function (i) {
        str += "<dl class=\"clearfix\">";
        str += "<dt> <a href=\"/Content/TaContent/" + commentList[i].Creater.MemberID + "\" class=\"nickname\" target=\"_blank\"><img class=\"avatar\" src=\"" + photofunctions.getprofileiconphotoname(commentList[i].Creater.ICONPath) + "\" alt=\"avatar\"/></a></dt>";
        str += "<dd>";
        str += "<div>";
        str += "<a href=\"/Content/TaContent/" + commentList[i].Creater.MemberID + "\" class=\"nickname\" target=\"_blank\">" + commentList[i].Creater.NickName + "</a>";
        str += "<small class=\"datetime\">";
        if (commentList[i].MemberID != userID) {
            str += "<nobr><a class=\"gray01 answeranswerbu\" onclick=\"showcommentcomment('" + commentList[i].Creater.NickName + "','" + commentList[i].Creater.MemberID + "','" + commentList[i].ID + "')\">回应</a></nobr>";
        }
        str += "&nbsp;&nbsp;" + getTimeSpan(paserJsonDate(commentList[i].CreatedTime)) + "";
        str += "</small>";
        str += "</div>";
        str += "<div class=\"content\">" + commentList[i].Content + "</div>";
        str += "</dd>";
        str += "</dl>";
        /*
        //如果回复的用户编号和当前登录的用户编号相同，不加载回应按钮
        if (commentList[i].MemberID != userID) {
        str += "<a class=\"gray01 answeranswerbu\" onclick=\"showcontentanswertext('" + commentList[i].ID + "','" + id + "','" + commentList[i].Creater.NickName + "','" + userID + "','" + commentList[i].MemberID + "',$(this))\">回应</a>";
        }
        str += "</span>";
        str += "</div>";
        str += "</div>";
        //如果回复的用户编号和当前登录的用户编号相同，不加载回应的输入框
        if (commentList[i].MemberID != userID) {
        str += "<div class=\"ans_com ans_txt answeranswerdiv\" id=\"contentanswertextdiv" + commentList[i].ID + "\" data-ifopen=\"false\" style=\"display:none;\"></div>";
        }*/
    });

    return str;
}
//加载内容回复的方法(用于ContentDetail)
function showcontentanswerinContentDetail(id, mainbox, continer, countcontiner, morecontiner, pagesize, pageno, userid) {
    CommentProvider.ShowComment(id, pagesize, pageno, function (data) {
        var data = $.parseJSON(data);
        var AnswerList = data.commentList;
        var AnswerCount = data.contentObject.AnswerCount;
        if (AnswerCount == 0) mainbox.hide(); else mainbox.show();
        var str = showContentAnswerInDetail(id, AnswerList, userid);
        continer.val("");
        if (pageno == 1) continer.html("");
        continer.html(continer.html() + str);
        countcontiner.html(AnswerCount);
        if (AnswerList.length >= pagesize) {
            //morecontiner.show();
        }
        else {
            morecontiner.remove();
        }
    });
}
//添加内容喜欢
function addlike(id, likename, likeContentType) {
    var likecountdiv = $("#contentlikecount" + id);
    likecountdiv.html(parseInt(likecountdiv.html() == "" ? "0" : likecountdiv.html()) + 1);
    ContentProvider.AddContentLike(id, likeContentType, function (data) {
        var data = $.parseJSON(data);
        if (data.toString() == "False" || data.toString() == "false") {
            var ifopen = $("#contentanswerdiv" + id).attr("data-ifopen", "true");
            showanswerdiv("contentanswerdiv" + id);
            likecountdiv.html(parseInt(likecountdiv.html() == "" ? "0" : likecountdiv.html()) - 1);
            $.jBox.tip("只能mo一次的", 'info');
        }
        else {
            $.jBox.tip("+1<br/>是否还要对Ta说点什么呢？", 'info');
            var obj = data.contentobj;
            if ($("#contentlikeshowlikename" + id).html() != null) {
                $("#contentlikeshowlikename" + id).html(obj.LikeCount + likename);
            }
            if ($("#contentlikemember" + id).html() != null) {
                var str = "";
                for (var j = 0; j < obj.LikeList.length; j++) {
                    if (j >= 8) break;
                    str += "<a class=\"list_ajax\"><img src=\"" + photofunctions.getnormalphotoname(obj.LikeList[j].MemberIcon) + "\" height=\"25\" width=\"25\"/></a>";
                }
                $("#contentlikemember" + id).html(str);
            }
            showanswerdiv("contentanswerdiv" + id, "content" + id);
        }
    });
}

//与我有关的动态回复评论
function addActivityReplyComment(id, type, ifmy, answerid, memberLabel) {
    var content = "";
    if (ifmy) {
        content = $("#" + type + "Reply" + answerid).val();
    }
    else {
        content = $("#" + type + answerid).val();
    }

    if (content.length < 3) {
        $.jBox.tip("输入点东西吧，最少输入3个字哦", 'error');
        return;
    }
    else if (content.length > 140) {
        $.jBox.tip("最多只能输入140个字哦", 'error');
        return;
    }
    CommentProvider.addMemberLabelComment(answerid, content, memberLabel, function (data) {
        var data = $.parseJSON(data);
        var AnswerList = data.commentList;
        var AnswerCount = data.commentCount;
        var str = getActivityCommentReplyHtml(id, type, AnswerList, AnswerCount, "showActivityCommentReply", 1, ifmy, id);
        $("#" + type + answerid).val("回应" + memberLabel + "：");
        $("#reply" + answerid).hide();
        $("#" + type + "Reply" + answerid).html(str);
        $("#" + type + "ReplyCount" + answerid).html(AnswerCount + "");
    });
}

//加载回复内容
function getActivityCommentReplyHtml(id, type, commentList, commentcount, movrename, pageno, ifmy, contentid) {
    var str = "";
    $.each(commentList, function (i) {
        str += "<div class=\"ans_com\">";
        str += "<div class=\"ans_span\">";
        str += "<span class=\"left_s\">";
        str += "&nbsp;&nbsp;<font class=\"huise\">" + commentList[i].Content + "</font>";
        str += "&nbsp;<font class=\"gray01\">" + getTimeSpan(paserJsonDate(commentList[i].CreatedTime)) + "</font>";
        str += "</span>";
        str += "</div>";
        str += "</div>";
    });
    return str;
}
//点击加载更多类容回复的方法
function showActivityCommentReply(id, type, memberLabel, ifmy) {
    alert("aa");
    CommentProvider.getMemberLabelComment(id, memberLabel, function (data) {
        var data = $.parseJSON(data);
        var AnswerList = data.commentList;
        var AnswerCount = data.contentObject.AnswerCount;
        var str = getActivityCommentReplyHtml(id, type, AnswerList, AnswerCount, "showActivityCommentReply", pageno, ifmy, id);
        $("#" + type + id).val("");
        $("#" + type + "Reply" + id).html(str);
        $("#" + type + "ReplyCount" + id).html(AnswerCount);
    });
}
/*
var FramedImages = {
    ImageObjectList: Array,
    FrameType: String,
    IsImageH: function (imageurl) {
        var imageobj = new Image();
        imageobj.src = imageurl;
        imageobj.onLoad = 
        image.src = imageobj.attr("src");
        if (image.complete) {
            imagewidth = image.width;
            imageheight = image.height;
        }
        else {
            var imgobj = document.createElement("img");
            imgobj.setAttribute("id", "imagecontentimg");
            imgobj.setAttribute("src", imageobj.attr("src"));
            document.body.appendChild(imgobj);
            image.src = $("#imagecontentimg").attr("src");
            imagewidth = image.width;
            imageheight = image.height;
            imgobj.parentNode.removeChild(imgobj);
        }
        if (imageobj.complete) {

        }
    },
    PushImageUrls: function (imageurllist) {

        if (imageurllist.length == 0) return;
        if (imageurllist.length == 1) {

            imageobj.src = imageurllist[0];
        }
        for (i = 0; i < imageurllist.length; i++) {

        }
    }

/**********************************************begin  内容操作的相关方法*/
//加载更多内容
function loadcontent() {
    if ($("#ContentLoadMore").html() != null && $("#ContentLoadMore").html() != "") {
        if ($("#ContentLoadMoreClick").html() != null && $("#ContentLoadMoreClick").html() != "") {
            $("#ContentLoadMoreClick").html("努力加载中…");
            var clientheight = document.documentElement.clientHeight;
            var scrolltop = document.body.scrollTop + document.documentElement.scrollTop;
            var offsetheight = document.body.offsetHeight;
            if (clientheight + scrolltop > $("#ContentLoadMore").offset().top - (clientheight / 2)) {
//                $("#ContentLoadMoreClick").html("<img src=\"/pics/Ajax_loading.gif\"/>");
                $("#ContentLoadMoreClick").click();
                $("#ContentLoadMoreClick").parent().html("<a id=\"ContentLoadMoreClick\">努力加载中…</a>");
            }
        }
    }
}
//加载喜欢和回复的按钮效果
function loadRightBu() {
    $('.bottom_right').find('.love_hit').hover(function () {
        $(this).find('em').css({ "background-position": "-41px -28px" });
        $(this).find('s').css({ "color": "#b40001" });
    }, function () {
        $(this).find('em').css({ "background-position": "-7px -28px" });
        $(this).find('s').css({ "color": "#d5836f" });
    });
    $('.bottom_right').find('.msg_hit').hover(function () {
        $(this).find('.emhover').css({ "display": "block" });
        $(this).find('.em-hit').css({ "display": "none" });
        $(this).find('s').css({ "display": "none" });

    }, function () {
        $(this).find('.emhover').css({ "display": "none" });
        $(this).find('.em-hit').css({ "display": "block" });
        $(this).find('s').css({ "display": "block" });
    });
}
//是否开启瀑布流
var ifopenmasonrynotimeline = false;
//第一次加载内容后的瀑布流布局方法
function masonrynotimeline() {
    var showcontent = $('#showcontent');
    if (showcontent.html() != null) {
        if (ifopenmasonrynotimeline) {
            showcontent.masonry({ itemSelector: '.care_com' });
            if ($(".contentLoading").html() != null) $(".contentLoading").remove();
            $("div.care_com").css("display", "block");
        }
    }
}
//当内容的显示区域高度变化时重新布局内容
function masonrynotimelinetoshowanswer() {
    var showcontent = $('#showcontent');
    if (showcontent.html() != null) {
        if (ifopenmasonrynotimeline) {
            showcontent.masonry({ itemSelector: '.care_com' });
        }
    }
}
//删除内容
function deleteContent(contentid, thisobj, locationurl) {
    $.jBox.confirm("你确定要删除这条内容吗？", "确认提示", function (data) {
        if (data.toString() == "ok") {
            ContentProvider.DeleteContent(contentid, function (data) {
                var data = $.parseJSON(data);
                if (data) {
                    $.jBox.tip("删除成功！", 'info');
                    window.location = locationurl;
                }
                else {
                    $.jBox.tip("删除失败，系统维护中，给您带来了不便，请谅解！", 'error');
                }
            });
        }
    });
}

var carePic = null;
var ifmouseIn = false;
function showShare(obj, myid, heid) {
    var iffans = false;
    var carePic = obj;
    var myID = myid;
    var heiID = heid;
    $.ajaxSetup({ async: false });
    memberprovider.getMemberIsInFavor(myID, heiID, function (data) {
        iffans = $.parseJSON(data);
    });
    $.ajaxSetup({ async: true });
    var dialogdiv = "";
    dialogdiv += "<div class=\"msg-p\" style=\"z-index:1000\"><div class=\"toparrow\"><div class=\"arr\"></div><div class=\"arrg\"></div></div>";
    dialogdiv += "<ul><li class=\"b\"><a onclick=\"window.open('/Msg/Messagedetails/" + heiID + "')\">发米邮</a></li>";
    if (iffans == true) {
        dialogdiv += "<li class=\"b\"><a onclick=\"member_i_functions.deljoinfans('" + heiID + "',$(this));\">取消关注</a></li>";
    } else {
        dialogdiv += "<li class=\"b\"><a onclick=\"member_i_functions.injoinfans('" + heiID + "',$(this));\">加关注</a></li>";
    }
    dialogdiv += "<li><a href=\"/Content/TaContent/" + heiID + "/all/1\">Ta的地盘</a></li></ul></div>";
  
    $('body').append(dialogdiv);
    var msgdiv = $('.msg-p');
    msgdiv.css({
        left: carePic.offset().left - 8 + "px",
        top: carePic.offset().top + 55 + "px"
    });
    msgdiv.find('a').hover(function () {
        $(this).css({ "color": "#333" });
    }, function () {
        $(this).css({ "color": "#999" });
    });
    msgdiv.mouseenter(function () {
        ifmouseIn = true;
    });
    msgdiv.mouseleave(function () {
        ifmouseIn = false;
        var timeout = setTimeout(function () {
            if (!ifmouseIn) {
                 msgdiv.remove();

            }
            clearTimeout(timeout);
        }, 200);
    });
}

function loadiconmove() {
    $(".showS img").mouseenter(function () {
        ifmouseIn = true;
        showShare($(this), $(this).attr("data-myid"), $(this).attr("data-heid"));
    });
    $(".showS img").mouseleave(function () {
        ifmouseIn = false;
        var timeout = setTimeout(function () {
            if (!ifmouseIn) {
                $('.msg-p').remove();
            }
            clearTimeout(timeout);
        }, 200);
    });
}
$(window).resize(function () {
    if (carePic != null) {
        $('.msg-p').css({
            left: carePic.offset().left - 8 + "px",
            top: carePic.offset().top + 55 + "px"
        });
    }
});

//内容控件js加载方法
function getContentListStr(models, contentobj, ifshowmember, ifmy, pageno, userid) {
    var str = "";
    //获取内容类别名称和喜欢类别名称
    var names = getTypeNamesAndLikeName(models, contentobj);
    var typename = names.split('|')[0];
    var likename = names.split('|')[1];
    //如果不是打开自己的页面，显示内容的发布者
    if (ifshowmember.toString() == "true" || ifshowmember.toString() == "True") {
        str += "<div class=\"carepic\">";
        str += "<span class=\"showS\">";
        str += "<a href=\"/Content/TaContent/" + contentobj.MemberID + "/all/1\" target=\"_blank\">";
        str += "<img src=\"" + photofunctions.getprofileiconphotoname(contentobj.Creater.ICONPath) + "\" height=\"45\" width=\"45\" data-myid=\"" + userid + "\" data-heid=\"" + contentobj.MemberID + "\"/>";
        str += "</a>";
        str += "</span>";
        str += "<span class=\"blue02\"><a href=\"/Content/TaContent/" + contentobj.MemberID + "/all/1\" target=\"_blank\">" + contentobj.Creater.NickName + "</a></span>";
        str += "<span class=\"gray01\">" + getTimeSpan(paserJsonDate(contentobj.UpdateTime)) + "</span>";
        str += "</div>";
    }
    str += "<div class=\"caretxt\">";
    str += "<div class=\"ajax_box\">";
    str += "<div class=\"top_angle em\"></div>";
    str += "<div class=\"ajax_main\">";
    str += "<div class=\"feed_arrow\"><div class=\"arrow_l\"></div></div>";
    var jl = "";
    if (contentobj.ContentType.toString() == "0") { str += getImageContent(contentobj, typename, likename, pageno); }
    if (contentobj.ContentType.toString() == "1") { str += getShuoShuoContent(contentobj, typename, likename, pageno); }
    if (contentobj.ContentType.toString() == "4") { str += getInterViewContent(contentobj, typename, likename, ifmy,pageno); }
    if (contentobj.ContentType.toString() == "5") { str += getCallForContent(contentobj, typename, likename, pageno); }
    if (contentobj.ContentType.toString() == "6") { str += getInterestContent(contentobj, typename, likename); }
    if (contentobj.ContentType.toString() == "7") {
        str += getMemberContent(contentobj, typename, likename, userid);
        if (contentobj.Type == "0" || contentobj.Type == "设置个人位置") {
            $.ajaxSetup({ async: false });
            memberprovider.getMemberDistance(userid, contentobj.MemberID, function (data) {
                var data = $.parseJSON(data);
                jl = data;
            });
            $.ajaxSetup({ async: true });
        }
    }

    str += "<div class=\"mt10 clear\"></div>";
    str += "<div class=\"ajax_com\">";
    str += "<nobr><span class=\"bottom_left\" style=\"width:100px;\">&nbsp;" + jl + "</span></nobr>";
    if (models.UserID == null) {
        str += "<span class=\"bottom_right\">";
        str += "<b class=\"love_hit\" onclick=\"window.open('/Account/Login');\"><em class=\"em-love\"></em><s>" + (contentobj.AnswerCount > 0 ? contentobj.AnswerCount.toString() : "") + "</s></b>";
        str += "<b class=\"msg_hit\" onclick=\"window.open('/Account/Login');\">";
        str += "<em class=\"emhover\" style=\"display:none\">mo<q>一下</q></em>";
        str += "<em class=\"em-hit\">mo</em><s>" + (contentobj.LikeCount > 0 ? contentobj.LikeCount.toString() : "") + "</s>";
        str += "</b>";
        str += "</span>";
    }
    else {
        str += "<span class=\"bottom_right\">";
        str += "<b class=\"love_hit\" onclick=\"showanswerdiv('contentanswerdiv" + contentobj.ID + "','content" + contentobj.ID + "');\"><em class=\"em-love\"></em><s id=\"contentanswercount" + contentobj.ID + "\">" + (contentobj.AnswerCount > 0 ? contentobj.AnswerCount.toString() : "") + "</s></b>";
        str += "<b class=\"msg_hit\" onclick=\"addlike('" + contentobj.ID + "','" + likename + "','" + typename + "');\">";
        str += "<em class=\"emhover\" style=\"display:none\">mo<q>一下</q></em>";
        str += "<em class=\"em-hit\">mo</em><s id=\"contentlikecount" + contentobj.ID + "\">" + (contentobj.LikeCount > 0 ? contentobj.LikeCount.toString() : "") + "</s>";
        str += "</b>";
        str += "</span>";
    }
    str += "</div>";
    str += "</div>";
    str += "</div>";
    str += "<div class=\"bottom_angle em\"></div>";
    str += "</div>";
    str += "<div class=\"ans_box\" id=\"contentanswerdiv" + contentobj.ID + "\" data-ifopen=\"" + (ifmy && contentobj.AnswerCount > 0 ? "true" : "false") + "\" style=\"display:" + (ifmy && contentobj.AnswerCount > 0 ? "block" : "none") + ";\">";
    str += "<div class=\"ans_com ans_txt\">";
    str += "<span><em onclick=\"showanswerdiv('contentanswerdiv" + contentobj.ID + "');\" ></em></span>";
    str += "</div>";
    str += "<div class=\"clear1\"></div>";
    if (!ifmy || contentobj.AnswerCount <= 0) {
        str += "<div class=\"ans_com ans_txt answerdiv\" id=\"contenttextdiv" + contentobj.ID + "\" data-ifopen=\"true\" style=\"display:block;\">";
        str += "<span><input type=\"text\" id=\"content" + contentobj.ID + "\" class=\"ans_t\" /></span>";
        if (ifmy) {
            str += "<span class=\"arrow_ans\"><a class=\"blue02\" onclick=\"addcontentanswer('" + contentobj.ID + "','','" + userid + "','');$('#contenttextdiv" + contentobj.ID + "').remove();\">发送</a><em></em></span>";
        }
        else {
            str += "<span class=\"arrow_ans\"><a class=\"blue02\" onclick=\"addcontentanswer('" + contentobj.ID + "','','" + userid + "','')\">发送</a><em></em></span>";
        }
        str += "</div>";
    }
    str += "<div class=\"clear1\"></div>";
    str += "<div id=\"contentanswer" + contentobj.ID + "\">";
    //加载回复的内容
    str += showanswerHtml(contentobj.ID, contentobj.AnswerList, userid);
    str += "</div>";
    str += "<div class=\"ans_com ans_txt\" id=\"contentmore" + contentobj.ID + "\">";
    if (contentobj.AnswerCount > 6) {
        str += "<span class=\"ans_right\"><a href=\"/Content/ContentDetail/" + contentobj.ID + "\">更多…</a></span>";
    }
    str += "</div>";
    str += "</div>";
    str += "</div>";
    return str;
}
//获取内容类别名称和喜欢类别名称
function getTypeNamesAndLikeName(model, contentobj) {
    var typename = "", likename = "", typs = "";
    if (contentobj.ContentType.toString() == "0") { typs = model.namesModel.imageTypes; }
    if (contentobj.ContentType.toString() == "1") { typs = model.namesModel.suisuinianTypes; }
    if (contentobj.ContentType.toString() == "5") { typs = model.namesModel.callforTypes; }
    if (contentobj.ContentType.toString() == "4") { typename = "访谈"; likename = "访谈"; }
    if (contentobj.ContentType.toString() == "6") { typs = model.namesModel.interestTypes; }
    if (contentobj.ContentType.toString() == "7") { typs = model.namesModel.memberTypes; }
    for (var i = 0; i < typs.length; i++) {
        if (typs[i].split(',')[0] == contentobj.Type || typs[i].split(',')[1] == contentobj.Type) {
            typename = typs[i].split(',')[1];
            likename = typs[i].split(',')[2];
            break;
        }
    }
    if (typename == "") { typename = contentobj.Type; likename = "喜欢"; }
    return typename + "|" + likename;
}

//图片自动缩放
function getContentImageFristLoad(obj) {
    var layoutwidth = obj.parent().width();
    var layoutheight = obj.parent().height();

    var thiswidth = 0, thisheight = 0;
    var imgobj = document.createElement("img");
    imgobj.setAttribute("id", "imagecontentimg");
    imgobj.setAttribute("src", obj.attr("src"));
    imgobj.setAttribute("style", "display:none");
    document.body.appendChild(imgobj);
    var image = new Image();
    image.src = $("#imagecontentimg").attr("src");
    thiswidth = image.width;
    thisheight = image.height;
    imgobj.parentNode.removeChild(imgobj);

    if (layoutheight <= 0) {
        obj.width(layoutwidth);
        obj.height(thisheight * (layoutwidth / thiswidth));
        obj.parent().height(obj.height());
    }
    else {
        obj.width(layoutwidth);
        obj.height(thisheight * (layoutwidth / thiswidth));
        if (obj.height() < layoutheight) {
            obj.width(obj.width() * (layoutheight / obj.height()));
            obj.height(layoutheight);
            obj.css("marginLeft", (layoutwidth - obj.width()) / 2);
        }
        if (obj.height() > layoutheight) {
            obj.css("marginTop", (layoutheight - obj.height()) / 2);
        }
    }
    masonrynotimelinetoshowanswer();
}
var one_one = new Array("260,0");
var two_one = new Array("260,0","260,0");
var two_two = new Array("129,0","129,0");
var three_one = new Array("260,0","129,90","129,90");
var three_two = new Array("129,90","129,90","260,0");
var four_one = new Array("260,0","129,90","129,90","260,0");
var four_two = new Array("129,90","129,90","129,90","129,90");
var four_three =new Array("259,0","85,85","85,85","85,85");
var four_four = new Array("85,85","85,85","85,85","259,0");
var five_one = new Array("260,0","129,90","129,90","129,90","129,90");
var five_two = new Array("129,90","129,90","260,0","129,90","129,90");
var five_three = new Array("129,90","129,90","129,90","129,90","260,0");
//加载内容中的图片
function getContentImageStr(contentobj, pageno) {
    var imagelayouttypelist = new Array();
    var str = "";
    if (contentobj.LayOutType != null) {
        switch (contentobj.LayOutType.toString()) {
            case "1": imagelayouttypelist = one_one; break;
            case "3": imagelayouttypelist = two_one; break; 
            case "4": imagelayouttypelist = two_two; break;
            case "5": imagelayouttypelist = three_one; break;
            case "6": imagelayouttypelist = three_two; break;
            case "9": imagelayouttypelist = four_one; break;
            case "10": imagelayouttypelist = four_two; break;
            case "11": imagelayouttypelist = four_three; break;
            case "12": imagelayouttypelist = four_four; break
            case "14": imagelayouttypelist = five_one; break;
            case "15": imagelayouttypelist = five_two; break;
            case "16": imagelayouttypelist = five_three; break;
        }
        if (imagelayouttypelist.length > 0) {
            var width = 0, height = 0;
            str += "<div class=\"ajax_com contentshowimagediv" + pageno + "\" onclick=\"window.location='/Content/ContentDetail/" + contentobj.ID + "'\">";
            for (var i = 0; i < imagelayouttypelist.length; i++) {
                width = parseInt(imagelayouttypelist[i].split(',')[0]);
                height = parseInt(imagelayouttypelist[i].split(',')[1]);
                str += "<div style=\"width:" + width + "px;height:" + height + "px;float:left;margin-right:2px;margin-bottom:2px;overflow:hidden; cursor:pointer;\">";
                str += "<img src=\"" + photofunctions.getnormalphotoname(contentobj.ImageList[i].ImageUrl) + "\" width=\"0\" height=\"0\" onload=\"getContentImageFristLoad($(this))\"/>";
                str += "</div>";
            }
            str += "</div>";
        }
    }
    return str;
}
//加载图片内容
function getImageContent(image, typename, likename, pageno) {
    var str = "";
    str += "<div class=\"ajax_title\">" + typename + "<em class=\"n_pic\"></em></div>";
    str += "<div class=\"ajax_content\">";
    if (image.ImageList.length > 0) {
        str += getContentImageStr(image, pageno);
    }
    if (image.Content != null && image.Content != "") {
        image.Content = getSubStringToIndex(image.Content, 100);
        str += "<div class=\"ajax_com\">";
        str += "<a href=\"/Content/ContentDetail/" + image.ID + "\"><span>" + replaceToN(image.Content) + "</span></a>";
        str += "</div>";
    }
    return str;
}
//加载说说内容
function getShuoShuoContent(suosuo, typename, likename, pageno) {
    var str = "";
    str += "<div class=\"ajax_title\">" + typename + "<em class=\"xf\"></em></div>";
    str += "<div class=\"ajax_content\">";
    if (suosuo.ImageList.length > 0) {
        str += getContentImageStr(suosuo, pageno);
    }
    if (suosuo.Content != null && suosuo.Content != "") {
        suosuo.Content = getSubStringToIndex(suosuo.Content, 100);
        str += "<div class=\"ajax_com\">";
        str += "<span><a href=\"/Content/ContentDetail/" + suosuo.ID + "\">" + replaceToN(suosuo.Content) + "</a></span>";
        str += "</div>";
    }
    return str;
}
//加载访谈内容
function getInterViewContent(interview, typename, likename, ifmy, pageno) {
    var str = "";
    str += "<div class=\"ajax_title\">" + typename + "<em class=\"ft\"></em></div>";
    str += "<div class=\"ajax_content\">";
    for (var i = 0; i < interview.InterviewList.length; i++) {
        if (i >= 10) {break;}
        var obj = interview.InterviewList[i];
        str += "<a href=\"/Content/ContentDetail/" + interview.ID + "\">";
        str += "<div class=\"ajax_com addfont\">";
        str += "<span class=\"huise\">";
        str += "小编：" + obj.Question + "";
        str += "</span>";
        str += "</div>";
        str += "<div class=\"ajax_com addfont\">";
        str += "<span class=\"black\" title=\"" + obj.Answer + "\">";
        str += "回答：" + replaceToN(getSubStringToIndex(obj.Answer, 100)) + "";
        str += "</span>";
        str += "</div>";
        str += "</a>";
    }
    if (interview.InterviewList.length > 10) {
        str += "<div class=\"ajax_com addfont interviewmove\">";
        str += "<a href=\"/Content/ContentDetail/" + interview.ID + "\">";
        str += "还有" + (interview.InterviewList.length - 10) + "条";
        str += "</a>";
        str += "</div>";
    }
    return str;
}
//加载号召内容
function getCallForContent(callfor, typename, likename, pageno) {
    var str = "";
    str += "<div class=\"ajax_title\">" + typename + "<em class=\"hz\"></em></div>";
    str += "<div class=\"ajax_content\">";
    if (callfor.ImageList.length > 0) {
        str += getContentImageStr(callfor, pageno);
    }
    if (callfor.Content != null && callfor.Content != "") {
        str += "<div class=\"ajax_com\">";
        callfor.Content = getSubStringToIndex(callfor.Content, 100);
        str += "<span><a href=\"/Content/ContentDetail/" + callfor.ID + "\">" + replaceToN(callfor.Content) + "</a></span>";
        str += "</div>";
    }
    return str;
}
//加载兴趣操作内容
function getInterestContent(interest, typename, likename) {
    var str = "";
    str += "<div class=\"ajax_title\">" + typename + "<em class=\"jr\"></em></div>";
    str += "<div class=\"ajax_content\">";
    str += "<div class=\"ajax_com\">";
    str += "<div class=\"ajax_xiqu\">";
    str += "<a href=\"/InterestCenter/ShowInterest/" + interest.Interest.ID + "\">";
    str += "<span style=\"width:80px; height:80px;\">";
    str += "<img src=\"" + photofunctions.getprofileiconphotoname(interest.Interest.ICONPath) + "\" width=\"49\" onload=\"getContentImageFristLoad($(this))\"/>";
    str += "</span>";
    str += "</a>";
    str += "<span class=\"blue02\">";
    str += "<a href=\"/InterestCenter/ShowInterest/" + interest.Interest.ID + "\">";
    str += getSubStringToIndex(interest.Interest.Title, 5);
    str += "</a>";
    str += "</span>";
    str += "</div>";
    if (interest.Type == "0" || interest.Type == "添加兴趣组") {
        str += "<div class=\"ajax_dis\">";
        str += "<span>" + replaceToN(getSubStringToIndex(interest.Content, 40)) + "</span>";
        str += "</div>";
    }
    else if (interest.Type == "1" || interest.Type == "创建兴趣组") {
        str += "<div class=\"ajax_dis\">";
        str += "<span>" + replaceToN(getSubStringToIndex(interest.Interest.Content, 40)) + "</span>";
        str += "</div>";
    }
    str += "</div>";
    return str;
}
//加载用户操作内容
function getMemberContent(member, typename, likename, userid) {
    var str = "";
    str += "<div class=\"ajax_title\">" + typename + "<em class=\"jr\"></em></div>";
    str += "<div class=\"ajax_content\">";
    if (member.Type == "0" || member.Type == "设置个人位置") {
        var lat = member.Lat;
        var lng = member.Lng;
        str += "<div class=\"ajax_com\" style=\"width:260px;height:260px;\">";
        str += "<img src=\"http://maps.google.com/maps/api/staticmap?center=" + lat + "," + lng + "&zoom=11&size=260x260&maptype=roadmap&markers=color:red|label:A|" + lat + "," + lng + "&sensor=false\" onload=\"getContentImageFristLoad($(this))\" onclick=\"window.open('/Content/TaContent/" + member.Creater.MemberID + "/all/1')\"/>";
        str += "</div>";
    }
    else if (member.Type == "1" || member.Type == "修改个人头像") {
        str += "<div class=\"ajax_com\" style=\"width:260px;height:0px;\">";
        var memberAvatar = (member.MemberAvatar != null && member.MemberAvatar != "") ? member.MemberAvatar : member.Creater.ICONPath;
        str += "<img src=\"" + photofunctions.getnormalphotoname(memberAvatar) + "\" onload=\"getContentImageFristLoad($(this))\" onclick=\"window.open('/Content/TaContent/" + member.Creater.MemberID + "/all/1')\"/>";
        str += "</div>";
    }
    return str;
}
/****************************************************************end*/

/****************************************************************begin*/
//js加载时间标签
var timestrlist = new Array("刚刚", "今天", "昨天", "前天");
var timetimelist = new Array(60 * 60, 1 * 60 * 60 * 24, 2 * 60 * 60 * 24, 3 * 60 * 60 * 24);
var timelist = null;
function showtimeclick(time) {
    var nowtime = new Date();
    nowtime.setMonth(nowtime.getMonth() + 1);
    if (timelist == null) {
        timelist = new Array();
        if ($("#timelist").val() != null) {
            var timeliststr = $("#timelist").val();
            var timelists = timeliststr.split(','); //页面中定义的一个隐藏域，保存系统加载过的时间标签
            for (var index = 0; index < timelists.length; index++) {
                timelist[timelist.length] = timelists[index];
            }
        }
    }
    var ifExceed = false;
    var showtime = time;
//    showtime.setMonth(showtime.getMonth() + 1);
    var timeca = parseInt((nowtime.getTime() - showtime.getTime()) / 1000);
    if (timetimelist[timetimelist.length - 1] > timeca) {
        if (timelist.length <= 0 || (timelist.length > 0 && (timelist[timelist.length - 1] < timeca))) {
            for (var j = 0; j < timetimelist.length; j++) {
                if (timetimelist[j] > timeca) {
                    timelist[timelist.length] = timetimelist[j];
                    ifExceed = false;
                    return timestrlist[j];
                }
                else { ifExceed = true; }
            }
        }
        else {
            return "";
        }
    }
    else {
        ifExceed = true;
    }
    if (ifExceed) {
        if ((timelist.length > 0 && timelist[timelist.length - 1] <= timetimelist[timetimelist.length - 1]) || timelist.length <= 0) {
            timelist[timelist.length] = new Date(showtime.getFullYear().toString(), showtime.getMonth().toString(), showtime.getDate().toString(), "00", "00", "00").getTime();
            return showtime.getFullYear() + "-" + showtime.getMonth() + "-" + showtime.getDate();
        }
        else {
            if ((timelist[timelist.length - 1] > showtime.getTime())) {
                timelist[timelist.length] = new Date(showtime.getFullYear().toString(), showtime.getMonth().toString(), showtime.getDate().toString(), "00", "00", "00").getTime();
                return showtime.getFullYear() + "-" + showtime.getMonth() + "-" + showtime.getDate();
            }
            else {
                return "";
            }
        }
    }
    return "";
}
/****************************************************************end*/
//替换回车
function replaceToN(content) {
    //替换回车
    for (var i = 0; i < content.length; i++) {
        if (content.indexOf("\n") >= 0) {
            content = content.replace("\n", "<br/>");
        }
        else if (content.indexOf("\\n") >= 0) {
            content = content.replace("\\n", "<br/>");
        }
        else {
            break;
        }
    }
    return content;
}
//截取指定长度的字符串
function getSubStringToIndex(content, index) {
    return content.length > index ? content.substr(0, index) + ".." : content;
}
//获取半透明遮层
function getOpacityDiv() {
    var str = "";
    str += "<div id=\"opacityDiv\" style=\"background-color:#ddd;width:100%;height:500px;position:absolute;left:0px;top:0px;z-index:9998;display:none;\"></div>";
    return str;
}
