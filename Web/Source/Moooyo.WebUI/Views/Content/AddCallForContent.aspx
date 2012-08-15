<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.Content.AddContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
发布新号召
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<input type="hidden" id="addToPoints" name="addToPoints" value="<%=Comm.getAddCallForContentToPoints() %>"/>
<%Html.RenderPartial("AddLeftPanel", "Content"); %>
<%
    if (Model.contentObj != null)
    {
        Moooyo.BiZ.Content.CallForContent callfor = (Moooyo.BiZ.Content.CallForContent)Model.contentObj;
        %>
        <div class="container">
            <div class="box_demo1 fb_left">
              <div class="box_left2" >
                <div class="mt10"></div>
                <div class="Set_title1 border_b1 p40">
                    <span class="large_font">新号召</span>
                    <font class="cgray" >（想跟同城柚子举办单身派对？爬山？唱歌？喝酒？发个号召，一呼百应。）</font>
                </div>
                <div class="Set_box1 p40" style="padding:40px;padding-top:20px;">
                    <%Html.RenderAction("AddContentType", "Content", new { contentType = Moooyo.BiZ.Content.ContentType.CallFor, contentObj = Model.contentObj != null ? Model.contentObj : null }); %>
                    <%Html.RenderAction("ImageUploadascx", "Content", new { ifShowUp = false, contentTitle = "号召内容", photoType = 203, contentObj = Model.contentObj != null ? Model.contentObj : null }); %>
                    <%Html.RenderPartial("AddContentBu", "Content"); %>
                </div>
            </div>
            </div>
            <%Html.RenderAction("AddRightPanel", "Content", new { contentObj = Model.contentObj != null ? Model.contentObj : null }); %>
        </div>
        <input type="hidden" id="updateContent" name="updateContent" value="<%=callfor.ID %>"/>
        <input type="hidden" id="contentContent" name="contentContent" value="<%=callfor.Content %>"/>
        <%
    }
    else
    {
        if (Model.Member.Points >= Comm.getAddCallForContentToPoints() || Model.contentObj != null)
        { 
      %>
        <div class="container">
            <div class="box_demo1 fb_left">
              <div class="box_left2" >
                <div class="mt10"></div>
                <div class="Set_title1 border_b1 p40">
                    <span class="large_font">新号召</span>
                    <font class="cgray" >（想跟同城柚子举办单身派对？爬山？唱歌？喝酒？发个号召，一呼百应。）</font>
                </div>
                <div class="Set_box1 p40" style="padding:40px;padding-top:20px;">
                    <%Html.RenderAction("AddContentType", "Content", new { contentType = Moooyo.BiZ.Content.ContentType.CallFor, contentObj = Model.contentObj != null ? Model.contentObj : null }); %>
                    <%Html.RenderAction("ImageUploadascx", "Content", new { ifShowUp = false, contentTitle = "号召内容", photoType = 203, contentObj = Model.contentObj != null ? Model.contentObj : null }); %>
                    <%Html.RenderPartial("AddContentBu", "Content"); %>
                </div>
            </div>
            </div>
            <%Html.RenderAction("AddRightPanel", "Content", new { contentObj = Model.contentObj != null ? Model.contentObj : null }); %>
        </div>
<%
        }
        else
        {
       %>
        <div class="container">
            <div class="box_demo1 w680 fb_left">
	            <div class="mt10"></div>
                <div class="Set_box1 p40" style="padding:40px;padding-top:20px;">
                    <div class="mt10"></div>
			            <div class="fb_box_com w600">
			            <span class="add_xiqu"></span>
			            </div>
                        <div class="fb_box_com w600">
                            <div class="noenough">
                            <span><img src="/pics/Not_enough.gif" width="600" /></span>
                            <span class="need_inter"><h2><%=Comm.getAddCallForContentToPoints()%></h2>
                            </span>
                            <span class="self_inter"><font> 你的米果</font>
                            <h3><%=Model.Member.Points%></h3></span>
                            </div>
                        </div>
                        <div class="fb_box_com w600">
                            <span class="inter_msg">oh，发布一个号召，需要消耗你50米果，你现在的米果不够。</span>
                            <span class="gh"><a href="/Content/AboutEarnPoints" class="blue02">如何积累米果？</a></span>
                            <span class="a_text"><a class="redlink" href="/Content/IndexContent">返回</a></span>
                        </div>
                        <div class="padding_b50"></div>
	            </div>
            </div>
            <%Html.RenderAction("AddRightPanel", "Content", new { contentObj = "" }); %>
        </div>
<%
        }
    }%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css"/>
<link rel="stylesheet" type="text/css" href="/css/fabu.css"/>
<style type="text/css">
    .fb_box_com .noenough { width:600px; position:relative; display:block;}
    .fb_box_com .noenough span.need_inter { display:block; position:absolute; left:380px; top:150px; font-family:"Arial"; font-size:36px;color:#b20000;}
    .fb_box_com .noenough span.self_inter { display:block; position:absolute; left:0px; top:270px; background:url(/pics/self_Integral.gif) no-repeat; z-index:10; width:85px; padding-left:50px; height:61px; line-height:26px; color:#fff;}
    .fb_box_com span.inter_msg { padding-top:20px; padding-bottom:10px; line-height:30px; position:relative; display:block; font-family:"\5FAE\8F6F\96C5\9ED1"; font-size:18px; color:#b20000; text-align:left;} 
    .fb_box_com span.gh a{  color:#0099cc;}
    .fb_box_com span.gh a:hover{  color:#b40001;}
    .fb_box_com .noenough span.need_inter h2{ font-family:"Arial"; font-size:36px;color:#b20000;}
    .fb_box_com .noenough span.self_inter h3 { font-size:24px; font-family:"Arial"; }
</style>
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
            $.jBox.tip("请选择一个号召类型", 'error');
        }
        else if (content == "") {
            $.jBox.tip("请填写内容", 'error');
        }
        else if (imagecount > 1 != "" && imageLayOutType == "") {
            $.jBox.tip("请选择一个照片布局类型", 'error');
        }
        else {
            var updateContent = $("#updateContent");
            if (updateContent.val() != null) {
                ContentProvider.InsertCallForContent(permissions, interestIDs, content, imageIDs, imageLayOutType, type, updateContent.val(), function (data) {
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
                var addToPoints = $("#addToPoints").val();
                $.jBox.confirm("发布号召需要" + addToPoints + "米果，确定发布吗？", "确认提示", function (data) {
                    if (data == "ok") {
                        ContentProvider.InsertCallForContent(permissions, interestIDs, content, imageIDs, imageLayOutType, type, "", function (data) {
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
                });
            }
        }
    }
    function clearAddImageData() {
        $("#submitbu").removeAttr("click").unbind("click");
        window.location = "/Content/IContent";
    }
</script>
</asp:Content>
