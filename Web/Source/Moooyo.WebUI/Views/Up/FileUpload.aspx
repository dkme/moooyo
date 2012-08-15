<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>

<asp:Content ID="Contentjs" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<link href="/Content/upload-photo.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/scripts/p/up/swfupload.js"></script>
<script type="text/javascript" src="/scripts/p/up/swfupload.swfobject.js"></script>
<script type="text/javascript" src="/scripts/p/up/swfupload.queue.js"></script>
<script type="text/javascript" src="/scripts/p/up/photoprogress.js"></script>
<script type="text/javascript" src="/scripts/p/up/photo_upload.js"></script>
<style>
.photoUpload{float:left;width:590px;padding-right:40px}
.split5px{padding-top:5px}
.hidden{display:none}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="content">

    <h1>上传照片 - 走，去看海</h1>
 
    <div class="photoUpload p13">
        
    <div class="article">

<div id="photos-upload">
    <form action="/up/addphotoComplete" method="get" id="upload">
        <input type="hidden" value="" id="uploadFiles" name="pid" />
        <div class="upload-list hidden">
            <div class="hd">
                <span class="name">照片</span>
                <span class="size">大小</span>
                <span class="delete">删除?</span>
            </div>
            <div class="bd">
                <fieldset class="flash" id="fsUploadProgress"></fieldset>
            </div>
        </div>
        <div id="divStatus"></div>
        <div id="upload-btn">
            <span id="btn_holder"></span>
        </div>
        <div class="upload-tips"></div>
        <div id="upload-status" class="hidden">
            <span class="num">共&nbsp;<b></b>&nbsp;张</span><span class="continue-wrapper">&nbsp;|&nbsp;<a href="#" class="continue">继续添加照片</a></span>
            <span class="total-size">总计：<b></b></span>
        </div>
        <div id="has-error" class="hidden">
            <b></b>张照片上传失败。&nbsp;<span class="nextstep hidden">&gt;&nbsp;<a href="#">继续下一步</a>？</span>
        </div>
        <div class="opt-btns hidden">
            <input type="button" id="upload-start" value="开始上传" class="btn-green" />
            <a id="btnCancel" href="#">取消上传</a>
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
    <input id="complete" type="submit" class="hidden minisubmit" value="&gt;&nbsp;下一步,填写图片信息" />
    </form>
    <div id="old-portal">
        <br />
        无法上传？<a href="http://www.douban.com/photos/album/23318690/upload?type=n">使用普通方式上传照片></a>
    </div>
</div>
 
</div></div></div>
</asp:Content>
