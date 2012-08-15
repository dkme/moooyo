<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberProfileModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Models" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%@ Import Namespace="Moooyo.WebUI.Controllers" %>
<%@ Import Namespace="Moooyo.BiZ.Member.Activity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
[米柚] <%=Model.Member.Name%>的主页
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<style type="text/css">
    *+html #uploadPhoto{margin-top:0px;}
    .buttonforup{width:65px; height:20px; cursor:auto; padding:0px; margin:0px;}
    .buttonforup .button{width:65px; height:20px; line-height:21px; text-align:center; overflow:hidden; cursor:pointer; position:relative; margin:0px; padding:0px;}
    #uploadPhoto{width:65px; cursor:pointer; position:absolute; left:0px; top:0px;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="label01">
<div class="label01-02" onclick="$('div.label01').hide(800)" title="知道了，关闭">
<div class="label01-02-01"></div>

<span class="fl">&nbsp;&nbsp;还没有头像？上传一个！&nbsp;</span>
<span class="fl" style="margin-left:3px; margin-top:7px;">

    <div style="width:71px; height:22px; overflow:hidden;" class="fl">
        <form action="/Register/UploadTempCustomPhoto" method="post" enctype="multipart/form-data" id="UploadTempCustomPhotoForm" name="UploadTempCustomPhotoForm" style="width:71px; height:22px;">
            <div class="buttonforup btn">
                <div class="button">
                    点击浏览
        <input type="file" name="uploadPhoto" id="uploadPhoto" value="上传" accept="image/jpeg,image/gif,image/png,image/jpg,image/bmp" onchange="getImgSrc()" />
                </div>
            </div>
        </form>
    </div>


<%--<form action="/Member/I" method="post" id="uploadMemberFace">
        <input type="hidden" value="" id="uploadFileNames" name="uploadFileNames" />
        <input type="hidden" value="" id="uploadFiles" name="pid" />
        <div>
            <div>
            <fieldset class="flash hidden" id="fsUploadProgress"></fieldset>
                <div id="upload-btn"><span id="btn_holder" class="up"></span></div>
                <div class="right-btn-huiyuan hidden" id="uploadStartArea"></div>
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



</span>


<%--&nbsp;&nbsp;<a href="/Photo/mplist/">还没有头像？点击这里传一个！</a>--%>

</div>
</div>

<div class="label02">
    <div class="label01-02">
        <div class="label01-02-01" onclick="$('div.label02').hide(800)" title="知道了，关闭"></div>
        <span class="fl">&nbsp;&nbsp;还没有视频认证？马上认证！&nbsp;</span>
        <span class="fl" style="margin-left:3px; margin-top:-2px;"><input type="button" value="&nbsp;认证&nbsp;" onclick="javascript:document.location.href='/Setting/Authentica'" /></span>
    </div>
</div>

    <div class="c976" style="position:relative; overflow:inherit;">
    <div style="position:absolute; width:550px; height:47px; left:152px; top:58px; z-index:5;"  >
        	<div style="height:47px; width:34px; background:url(/pics/dhk1.gif) no-repeat; float:left;"></div>
            <div style="height:47px; width:502px; background:url(/pics/dhk2.gif) repeat-x; float:left;">
            	<div style="width:auto; height:47px; line-height:47px; float:left; _line-height:47px;">
                    <span id="meWant" style="_padding-top:15px;">
                    <% if (Model.Member.Want != "") { %>
                    我想和Ta<span id="meWant2"><%=Model.Member.Want.Length > 32 ? Model.Member.Want.Substring(0, 32) + "<span class=\"letspa--3\">...</span>" : Model.Member.Want%></span>
                    <% } else { %>还没有填写“我想”？点击填写<% } %>
                    </span>
                    <input id="txtMeWant" type="text" class="hidden" onclick="openIWants()" maxlength="0" style="_margin-top:8px;" />
                </div>
                <div style="width:auto; height:47px; float:right; line-height:47px; margin-left:10px;">
                    <a href="javascript:;" onclick="updateMeWant()" id="updateMeWant"><% if (Model.Member.Want != "") { Response.Write("修改"); } else { Response.Write("填写"); }%></a>&nbsp;
                    <a href="javascript:;" onclick="cancelMeWant()" id="cancelUpdateMeWant" style="display:none;"></a>
                </div>
            </div>
            <div style="height:47px; width:14px; background:url(/pics/dhk3.gif) no-repeat; float:left;"></div>
        </div>
   <!--个人左面板-->
    <% if (Model.IsOwner) {%>
    <% Html.RenderPartial("~/Views/Member/ProfileLeftPanel.ascx");%>  
    <% }
       else { %>
        <% Html.RenderPartial("~/Views/Member/MemberLeftPanel.ascx");%>  
    <% } %>
    <!--endof 个人左面板-->
          
    <section class="conbox-r mt32 fl">
    	<div class="h75 w619">
            <div><span class="fl">我的首页地址：&nbsp;</span>
            <a href="javascript:;" class="fl" id="linkCopyUrl">http://<%=HttpContext.Current.Request.Url.Host.ToString()%>/u/<%=Model.MemberUrl%></a><a href="javascript:;" class="fl" id="copyToClipboardBtn">&nbsp;&nbsp;复制</a></div>
        </div>
            	<%--<% Html.RenderAction("MyFansGlamourCountRank", "Relation", new { skin = "i" });%> 我的粉丝团们已经撤销 --%>
                <%--引导--%>
                <div class="introduction">
                <% 
                    //if (Model.Member.PhotoCount.Equals("0") || Model.Member.InterViewCount.Equals("0") || Model.Member.SkillCount.Equals("0") || (int.Parse(Model.Member.InterestCount) > 5 && Model.Member.FavorMemberCount < 10))
                    if (Model.Member.PhotoCount.Equals("0") || Model.Member.InterViewCount.Equals("0") || (int.Parse(Model.Member.InterestCount) > 5 && Model.Member.FavorMemberCount < 10))
                   {%>
                    <div class="div1 txt1"></div>
                    <div class="div1 txt2"></div>
                    <% } if (Model.Member.PhotoCount.Equals("0"))
                       {%>
                    <div id="mytip1" class="div2 tip1"><a href="/photo/photoupload?t=0">照片</a></div>
                    <%} if (Model.Member.InterViewCount.Equals("0"))
                        {%>
                    <div id="mytip2" class="div2 tip2"><a href="/Member/interview">访谈</a></div>
                     <%-- <%} if (Model.Member.SkillCount.Equals("0"))
                        {%>
                    <div id="mytip3" class="div2 tip3"><a href="/Member/skill">才艺</a></div>--%>
                      <%} if (int.Parse(Model.Member.InterestCount) < 5 && Model.Member.FavorMemberCount<10)
                       {%>
                    <div id="mytip4" class="div2 tip4"><a href="/InterestCenter/FeaturedInterestTopic">更多</a></div>
                    <%} %>
                </div>
                <%--引导结束--%>
     <% foreach (var aHLObjs in Model.activityHolderListObje) 
        { %> 
        <div style="width:600px; clear:both; display:block; height:auto; padding:15px 0px;">
       	  	<div style="width:60px; height:90px; float:left;">
            	<a href="/Member/I/<%=aHLObjs.MemberID %>" target="_blank"><img src="<%=Comm.getImagePath(aHLObjs.MemberInfomation.IconPath, ImageType.Icon) %>" height="50" width="50" title="<%=aHLObjs.MemberInfomation.NickName %>" alt="<%=aHLObjs.MemberInfomation.NickName %>" border="0" /></a>
          	</div>
            <div style="width:540px; float:left;">
            	<span style="line-height:28px;height:25px;float:left;margin-right:5px;"><a href="/Member/I/<%=aHLObjs.MemberID %>" target="_blank"><%=aHLObjs.MemberInfomation.NickName%></a></span>
                <%  string strMemberType = "";
                    switch (aHLObjs.IsRealPhotoIdentification)
                    {
                        case true: strMemberType += "<img src=\"/upload/photoreal.gif\" alt=\"已经通过视频认证\" title=\"已经通过视频认证\" style=\"width:15px;height:15px;margin-right:2px;\"/>"; break;
                    }
                    switch (Convert.ToByte(aHLObjs.MemberType))
                    {
                        case 2: strMemberType += "<img src=\"/upload/vip.gif\" alt=\"米柚VIP\" title=\"米柚VIP\" style=\"width:15px;height:15px;\"/>"; break;
                    }
                    Response.Write(strMemberType); %>
                    <div style="width:540px;clear:both;">
                <%=aHLObjs.MemberInfomation.Age%>岁&nbsp;&nbsp;<%=aHLObjs.MemberInfomation.Career%>&nbsp;&nbsp;<%=aHLObjs.MemberInfomation.City.Replace("|", "")%>&nbsp;&nbsp;距离<%=DisplayObjProvider.GetWeDistance(Model.MemberID, aHLObjs.MemberID)%></div>
                 <% string str = Moooyo.WebUI.Controllers.ActivityController.GetActivityStr(aHLObjs);
                    Response.Write(str); %>
          	</div>
            <hr style="width:auto; height:1px; background-color:#eeeeee; clear:both; border:none 0px;" />
        </div>
     <% } %>
     <!--分页-->
        <% if (Model.Pagger!=null)
               if (Model.Pagger.PageCount > 1)
           {%> 
           <% Html.RenderAction("pagger", "Shared", new { nowpage = Model.Pagger.PageNo, pagecount = Model.Pagger.PageCount, additionID = Model.Pagger.AdditionParams, url = Model.Pagger.PageUrl });%> 
        <%} %>
    <!--endof 分页-->       
    </section>
    <aside class="asidebox-r mt32 fr">
      <% Html.RenderAction("AppPush", "Push");%> 
      <% Html.RenderAction("GuessYourInterest", "Push");%> 
      <% Html.RenderAction("SameInterestingMemberOverMe", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID });%> 
    </aside>
    </div> 
    <input type="hidden" id="EmailIsVaild" name="EmailIsVaild" value="<%=Model.User.EmailIsVaild %>" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/scripts/jquery.form.js" type="text/javascript"></script>


<%--<script type="text/javascript" src="/js/up/swfupload_<%=ViewData["jsversion"] %>.js"></script>
<script type="text/javascript" src="/js/up/swfupload.swfobject_<%=ViewData["jsversion"] %>.js"></script>
<script type="text/javascript" src="/js/up/swfupload.queue_<%=ViewData["jsversion"] %>.js"></script>
<script type="text/javascript" src="/js/up/reg_photoprogress_<%=ViewData["jsversion"] %>.js"></script>
<script type="text/javascript" src="/js/up/photo_upload_regupface_<%=ViewData["jsversion"] %>.js"></script>--%>



<script language="javascript" type="text/javascript">
    $(document).ready(function (){
        $("#uploadPhoto").css({opacity:0});
        if (!$.browser.msie) {
            //复制到剪贴板
            copyToClipboard($("#copyToClipboardBtn"), $("#linkCopyUrl").html());
        }
        else {
            $("#copyToClipboardBtn").bind("click", function () {
                //复制到剪贴板
                copyToClipboard($("#copyToClipboardBtn"), $("#linkCopyUrl").html());
            });
        } 
        //引导样式变化
        $("#mytip1").mouseenter(function(){ $(this).attr("class","div2 tip1hover");}).mouseleave(function(){ $(this).attr("class","div2 tip1");});
        $("#mytip2").mouseenter(function(){ $(this).attr("class","div2 tip2hover");}).mouseleave(function(){ $(this).attr("class","div2 tip2");});
        $("#mytip3").mouseenter(function(){ $(this).attr("class","div2 tip3hover");}).mouseleave(function(){ $(this).attr("class","div2 tip3");});
        $("#mytip4").mouseenter(function(){ $(this).attr("class","div2 tip4hover");}).mouseleave(function(){ $(this).attr("class","div2 tip4");});
        
        
        $(".label01-02-01").bind("click", function () {
            $(".label01").hide(500);
        });

        var timeoutId = setTimeout(function () {
            fixedLabel01Pos();
            fixedLabel02Pos();
        }, 15000);

        if($.browser.webkit) {
            $("#UploadTempCustomPhotoForm").css("cssFloat", "right");
        }



        /* Uploaded picture code begin */

//        settings = {
//            debug: false,
//            flash_url: '/swf/swfupload.swf',
//            file_post_name: 'file',
//            upload_url: '/up/AddPhotoAndSetIcon',
//            post_params: {
//                "ASPSESSID" : "<%=Session.SessionID %>",
//                "AUTHID" : "<%=Request.Cookies[FormsAuthentication.FormsCookieName].Value%>",
//                "photoType": '<%=Model.photoType %>'
//            },
//            file_size_limit: '5 MB',
//            file_types: '*.jpg; *.jpeg; *.gif; *.bmp; *.png;',
//            file_types_description: 'Picture Files',
//            file_upload_limit: 1,
//            file_queue_limit: 1,
//            custom_settings: {
//                progressTarget: 'fsUploadProgress',
//                cancelButtonId: 'btnCancel',
//                startButtonId: 'btnStart',
//                jUrl: '/up/'
//            },
//            button_placeholder_id: 'btn_holder',
//            button_width: 38,
//            button_height: 21,
//            button_image_url: '/pics/small_upload_pic.png',

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

//        oUploadBtn = $('#upload-btn'),
//        oUploadStatus = $('#upload-status'),
//        oBtnCancel = $('#btnCancel'),
//        oOptBtns = $('.opt-btns'),
//        TMPL_UPLOAD_TIPS = '提示：每次最多可以<b>上传</b>一张照片';

//        swfup = new SWFUpload(settings);

//        $('#uploadFiles').val('');

        /* Uploaded picture code end */

    });
    
//    function uploadfinished() {
//        document.location.reload();
//    }


    function fixedLabel01Pos() {
        var memberAvatar = $("#iconImg")[0].src;
        if (memberAvatar.indexOf("/pics/noicon.jpg") > 0) {
            var referContainer = document.getElementById("iconImg"), left = 0, top = 0;
            var offsetPosit = getOffsetPosition(referContainer);
            left = offsetPosit.offsetLeft;
            top = offsetPosit.offsetTop;
            var topcha = $("#EmailIsVaild").val() == "False" ? 45 : 15;
            $(".label01").css({ "left": left + 11, "top": top - topcha, opacity: 0.8, "display": "block" });
        }
    }

    function fixedLabel02Pos() {
        var memberAuthentication = $("#videoAuthentication")[0].src;
        var memberAvatar = $("#iconImg")[0].src;
        if (memberAuthentication.indexOf("/upload/photonoreal.gif") > 0 && memberAvatar.indexOf("/pics/noicon.jpg") == -1) {
            var referContainer = document.getElementById("iconImg"), left = 0, top = 0;
            var offsetPosit = getOffsetPosition(referContainer);
            left = offsetPosit.offsetLeft;
            top = offsetPosit.offsetTop;
            var topcha = $("#EmailIsVaild").val() == "False" ? 45 : 15;
            $(".label02").css({ "left": left + 11, "top": top - topcha, opacity: 0.8, "display": "block" });
        }
    }

    function updateMeWant() {
        var flag = $("#updateMeWant").html();
        if (flag == "修改" || flag == "填写") {
            $("#cancelUpdateMeWant").html("取消");
            $("#meWant").html("我想和Ta");
            $("#txtMeWant").add("#cancelUpdateMeWant").show();
            $("#txtMeWant").val($("#meWant2").html() == null ? "" : $("#meWant2").html());
            $("#updateMeWant").html("保存");
            actionprovider.openiwants('txtMeWant');
        }
        else if (flag == "保存") {
            var txtMeWant = $("#txtMeWant").val();
            if (txtMeWant == "") {
                $.jBox.tip("填写些想做的事情吧", 'info');
                return;
            }
            memberprovider.setiwant(txtMeWant, function (data) {
                var result = $.parseJSON(data);
                if (result.ok) {
                    $("#txtMeWant").add("#cancelUpdateMeWant").hide();
                    memberprovider.getmemberprofile("", function (data) {
                        var jData = $.parseJSON(data);
                        $("#meWant").html("我想和Ta" + "<span id=\"meWant2\">" + (jData.IWant.length > 30 ? jData.IWant.substring(0, 30) + "<span class=\"letspa--3\">...</span>" : jData.IWant) + "</span>");
                    });
                    $("#updateMeWant").html("修改");
                }
            });
        }
    }

    function openIWants() { actionprovider.openiwants('txtMeWant'); }

    function cancelMeWant() {
        var meWant;
        $.ajaxSetup({ async: false });
        memberprovider.getmemberprofile("", function (data) {
            meWant = $.parseJSON(data);
        });
        $.ajaxSetup({ async: true });
        if(meWant.IWant == "") {
            $("#meWant").html("还没有填写“我想”？点击填写");
            $("#txtMeWant").add("#cancelUpdateMeWant").hide();
            $("#updateMeWant").html("填写");
        } else { 
            $("#meWant").html("我想和一个<%=Model.Member.Sex == 1 ? "女孩" : "男孩"%>" + "<span id=\"meWant2\">" + (meWant.IWant.length > 30 ? meWant.IWant.substring(0, 30) + "<span class=\"letspa--3\">...</span>" : meWant.IWant) + "</span>"); 
            $("#txtMeWant").add("#cancelUpdateMeWant").hide();
            $("#updateMeWant").html("修改");
        }
    }

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
                        actionprovider.openCustomSmallPicture('/Member/I', dataArr[0]);
                    }
                    else $.jBox.tip(dataArr[1], "err");
                }
                else $.jBox.tip("系统维护中，给您带来了不便，请谅解！", "err");
            });

            var timeoutId = setTimeout(function () {
//                        $("#uploadPhoto").select();
//                        document.execCommand("Delete");
//                        document.getElementById("uploadPhoto").value= "";

                            $("div.buttonforup div.button").html("<input type=\"file\" name=\"uploadPhoto\" id=\"uploadPhoto\" value=\"上传\" onclick=\"uploadPersonalityPicture()\" onchange=\"changePersonalityPicture()\" size=\"1\"/>");
                            $("input#uploadPhoto").css({opacity:0});
                        }, 50
                    );
        }
    }
</script>
</asp:Content>
