<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="jiathis_style_32x32" class="em" style="position:absolute; left:0px; top:0px; display:none; z-index:99999; width:120px;">
    <a><img id="tengxun" src="/pics/weibo-2.png" title="分享到腾讯微博。" style="cursor:pointer;"/></a>
    <a><img id="sina" src="/pics/weibo-01.png" title="分享到新浪微博。" style="cursor:pointer;"/></a>
    <a><img id="renren" src="/pics/weibo3.png" title="分享到人人网。" style="cursor:pointer;"/></a>
    <a><img id="douban" src="/pics/weibo_4.png" title="分享到豆瓣网。" style="cursor:pointer;"/></a>
</div>
<script type="text/javascript" >
    var oldobjindex = null;
    var ifopen = false;
    function shareClick(url, title, content, thisobj, pic) {
        var data_oldobj = thisobj.attr("data-oldobj");
        var left = thisobj.offset().left + thisobj.width() + 10;
        var top = thisobj.offset().top + 6;
        var jiathis_style_32x32 = $("#jiathis_style_32x32");
        if (oldobjindex == null) {
            oldobjindex = data_oldobj;
            ifopen = true;
            jiathis_style_32x32.css({ left: left, top: top });
            jiathis_style_32x32.slideDown(300);
        }
        if (oldobjindex != data_oldobj) {
            oldobjindex = data_oldobj;
            ifopen = true;
            jiathis_style_32x32.slideUp(300, function () {
                jiathis_style_32x32.animate({ left: left, top: top }, 300);
            });
        }
        else {
            ifopen = true;
            jiathis_style_32x32.css({ left: left, top: top });
            jiathis_style_32x32.slideDown(300);
        }
        thisobj.mouseleave(function () {
            ifopen = false;
            jiathis_style_32x32.mouseenter(function () {
                ifopen = true;
                jiathis_style_32x32.mouseleave(function () {
                    ifopen = false;
                    var timeout = setTimeout(function () {
                        if (!ifopen) {
                            jiathis_style_32x32.slideUp(300);
                            $("#sina").unbind("click");
                            $("#tengxun").unbind("click");
                            $("#renren").unbind("click");
                            $("#douban").unbind("click");
                        }
                        clearTimeout(timeout);
                    }, 500);
                });
            });
            var timeout = setTimeout(function () {
                if (!ifopen) {
                    jiathis_style_32x32.slideUp(300);
                    $("#sina").unbind("click");
                    $("#tengxun").unbind("click");
                    $("#renren").unbind("click");
                    $("#douban").unbind("click");
                }
                clearTimeout(timeout);
            }, 500);
        });
        $("#sina").unbind("click");
        $("#tengxun").unbind("click");
        $("#renren").unbind("click");
        $("#douban").unbind("click");
        if (ifopen) {
            $("#sina").bind("click", function () { shareSina(title, content, url, pic); });
            $("#tengxun").bind("click", function () { shareTX(title, content, url, pic); });
            $("#renren").bind("click", function () { shareRenRen(title, content, url, pic); });
            $("#douban").bind("click", function () { shareDouBan(title, content, url, pic); });
        }
    }
    function shareSina(title, content, url, pic) {
        url = url + "?fx1";
        if (title == "" || title=="米柚网") {
            var mes = "@米柚网 " + encodeURI(content);
            window.open("http://v.t.sina.com.cn/share/share.php?title=" + mes + "&url=" + url + "&pic=" + pic);
        }
        else {
            var mes = "@米柚网 " + encodeURI(title + "：" + content);
            window.open("http://v.t.sina.com.cn/share/share.php?title=" + mes + "&url=" + url + "&pic=" + pic);
        }
    }
    function shareTX(title, content, url, pic) {
        url = url + "?fx2";
        if (title == "" || title == "米柚网") {
            var mes = "@moooyoweibo " + encodeURI(content);
            window.open("http://v.t.qq.com/share/share.php?title=" + mes + "&url=" + url + "&pic=" + pic);
        }
        else {
            var mes = "@moooyoweibo " + encodeURI(title + "：" + content);
            window.open("http://v.t.qq.com/share/share.php?title=" + mes + "&url=" + url + "&pic=" + pic);
        }
    }
    function shareRenRen(title, content, url, pic) {
        url = url + "?fx3";
        var rrShareParam = {
            resourceUrl: url, //分享的资源Url
            pic: pic, 	//分享的主题图片Url
            title: title == "" || title == "米柚网" ? "" : title, 	//分享的标题
            description: "@米柚网(601435167) " + content	//分享的详细描述
        };
        rrShareOnclick(rrShareParam);
    }
    function shareDouBan(title, content, url, pic) {
        url = url + "?fx4";
        if (title == "" || title == "米柚网") {
            var mes = "" + encodeURI(content);
            window.open("http://www.douban.com/recommend/?title=" + mes + "&url=" + url + "");
        }
        else {
            var mes = "" + encodeURI(title + "：" + content);
            window.open("http://www.douban.com/recommend/?title=" + mes + "&url=" + url + "");
        }
    }
</script>
<script type="text/javascript" src="http://widget.renren.com/js/rrshare.js"></script>