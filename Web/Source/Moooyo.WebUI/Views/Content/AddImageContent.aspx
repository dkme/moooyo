﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.Content.AddContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
分享新照片
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%Html.RenderPartial("AddLeftPanel", "Content"); %>
<div class="container" style="margin: 0px auto;">
    <div class="box_demo1 fb_left">
      <div class="box_left2" >
	    <div class="mt10"></div>
	    <div class="Set_title1 border_b1 p40">
            <span class="large_font">新照片</span>
            <font class="cgray">（拍了新照片？赶紧分享吧。用图片来记录一部属于你的单身电影。）</font>
        </div>
        <div class="Set_box1 p40" style="padding:40px;padding-top:20px;">
            <%Html.RenderAction("AddContentType", "Content", new { contentType = Moooyo.BiZ.Content.ContentType.Image, contentObj = Model.contentObj != null ? Model.contentObj : null }); %>
            <%Html.RenderAction("ImageUploadascx", "Content", new { ifShowUp = false, contentTitle = "照片描述", photoType = 201, contentObj = Model.contentObj != null ? Model.contentObj : null }); %>
            <%Html.RenderPartial("AddContentBu", "Content"); %>
	    </div>
      </div>
    </div>
    <%Html.RenderAction("AddRightPanel", "Content", new { contentObj = Model.contentObj != null ? Model.contentObj : null }); %>
</div>
<%if (Model.contentObj != null)
  {
      Moooyo.BiZ.Content.ImageContent image = (Moooyo.BiZ.Content.ImageContent)Model.contentObj;
      %>
<input type="hidden" id="updateContent" name="updateContent" value="<%=image.ID %>"/>
<input type="hidden" id="contentContent" name="contentContent" value="<%=image.Content %>"/>
<%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css"/>
<link rel="stylesheet" type="text/css" href="/css/fabu.css"/>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<!--[if IE 6]>
    <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('em,.left_menu ul li a,.textarea3');
	 </script>
<![endif]-->
<script type="text/javascript">
    $().ready(function () {
        $("#wrap").css({ "background": "#DEDEE0", "width": "1056px" });
        $(".left_menu").css("display", "block");
        var updateContent = $("#updateContent");
        if (updateContent.val() != null) {
            $("#content").val($("#contentContent").val());
        }
    });
    function contentsubmit() {
        var permissions = $("#permissions").val();
        var type = $("#type").val();
        var content = $("#content").val();
        var interestIDs = $("#interestIDs").val();
        var imageIDs = $("#imageIDs").val();
        var imageLayOutType = $("#imageLayOutType").val();
        var imagecount = imageIDs.split(',').length;
        if (imagecount == 1) { imageLayOutType = "one_one"; }
        if (interestIDs == "") {
            $.jBox.tip("请至少至少选择一个兴趣", 'error');
        }
        else if (type == "") {
            $.jBox.tip("请选择一个照片类型", 'error');
        }
        else if (imageIDs == "") {
            $.jBox.tip("请至少上传一张图片", 'error');
        }
        else if (imagecount > 1 != "" && imageLayOutType == "") {
            $.jBox.tip("请选择一个照片布局类型", 'error');
        }
        else {
            var updateContent = $("#updateContent");
            if (updateContent.val() != null) {
                ContentProvider.InsertImageContent(permissions, interestIDs, content, imageIDs, imageLayOutType, type, updateContent.val(), function (data) {
                    var data = $.parseJSON(data);
                    if (data != null) {
                        $.jBox.tip("内容更新已成功。<br/>系统会自动跳转到个人页面", 'info');
                        clearAddImageData(); //添加完成后清除该清除的数据
                    }
                    else {
                        $.jBox.tip("更新失败，系统维护中，给您带来了不便，请谅解！", 'error');
                    }
                });
            }
            else {
                ContentProvider.InsertImageContent(permissions, interestIDs, content, imageIDs, imageLayOutType, type, "", function (data) {
                    var data = $.parseJSON(data);
                    if (data != null) {
                        //分享到微博，是否存在需要分享的微博，且是否分享成功
                        var url = "http://www.moooyo.com/Content/ContentDetail/" + data.ID;
                        var ifShareToWB = ShareToWB(content, url);
                        if (ifShareToWB) {
                            $.jBox.tip("内容发布已成功，并且已分享到你勾选的微博中。<br/>系统会自动跳转到个人页面", 'info');
                        }
                        else {
                            $.jBox.tip("内容发布已成功。<br/>系统会自动跳转到个人页面", 'info');
                        }
                        clearAddImageData(); //添加完成后清除该清除的数据
                    }
                    else {
                        $.jBox.tip("更新失败，系统维护中，给您带来了不便，请谅解！", 'error');
                    }
                });
            }
        }
    }
    function clearAddImageData() {
        window.location = "/Content/IContent";
        $("#submitbu").removeAttr("click").unbind("click");
    }
</script>
</asp:Content>
