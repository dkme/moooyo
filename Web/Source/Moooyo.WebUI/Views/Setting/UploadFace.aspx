<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberProfileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	上传头像
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
	    <% Html.RenderPartial("~/Views/Setting/LeftPanel.ascx");%>
        <div class="Set_content">
		    <div class="Set_title"> <span>上传头像</span><em>▲</em></div>
		    <div class="Set_box">
		    <div class="bind_form"><span class="at_text t_1_d">
                <div style="clear:both; padding-top:15px;">
                <form action="/Shared/UploadTempCustomPhoto" method="post" enctype="multipart/form-data" id="UploadTempCustomPhotoForm" name="UploadTempCustomPhotoForm" class="fl">
                <input type="hidden" name="AUTHID" id="AUTHID" value="<%=Request.Cookies[FormsAuthentication.FormsCookieName].Value%>" />
                <input type="hidden" name="ASPSESSID" id="ASPSESSID" value="<%=Session.SessionID %>" />

			    <div class="buttonforup reg_btn btn">
                    <div class="button">
                        上传头像
                        <input type="file" name="uploadPhoto" id="uploadPhoto" value="这里上传头像" onclick="getImgSrc()" onchange="resetimageFileInput()" size="1" />
                    </div>
                </div>
                </form>
                <div class="fl" style="margin-left:30px; color:#cccccc;">支持JPG/PNG/BMP图片文件，文件大小不超过5M</div>
                </div>
                <div class="clearfix"></div>
                <div style="clear:both; padding-top:10px;">你最好上传一张近期的生活照哦~<br />
戴着卡通/宠物/明星等虚假面目是碰不到好柚子滴~
</div>
		    </span></div>
		    <div class="bind_form ">
			    <span><img src="/pics/upload.gif" alt="头像" title="头像" id="imgRegisterMemberFacePreview" width="150" height="150" />
                    </span>	
            </div>
		</div>
	</div>
    </div>
    <input type="hidden" id="interestIds" value="<%=Model.interestIds %>" />
    <input type="hidden" id="permissions" name="permissions" value="0"/>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<style type="text/css">
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/js/main_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/scripts/jquery.form.js" type="text/javascript"></script>
    <!--[if IE 6]>
    <script type="text/javascript">
        DD_belatedPNG.fix('em');
	 </script>
    <![endif]-->
    <script language="javascript" type="text/javascript">
        var uploadpath = "<%=ViewData["uploadpath"] %>";
        $().ready(function () {
            $("#uploadPhoto").css({opacity:0});

            var ICONPath = "<%=Model.Member.ICONPath %>";
            if(ICONPath != "") {
                $("#imgRegisterMemberFacePreview").attr("src", ICONPath);
            }  
        });
        function resetimageFileInput(object) {
            var uploadPhoto=$("#uploadPhoto");
            if(uploadPhoto.val()!=""){
                uploadPhoto.unbind("onchange");
                uploadPhoto.removeAttr("onchange");
                uploadPhoto.change(function(){});
                getImgSrc();
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
                            actionprovider.openCustomSmallPicture('/Setting/UploadFace', dataArr[0], 1);
                            
                        }
                        else $.jBox.tip(dataArr[1], "err");
                    }
                    else $.jBox.tip("系统维护中，给您带来了不便，请谅解！", "err");
                });

                var timeoutId = setTimeout(function () {
//                        $("#uploadPhoto").select();
//                        document.execCommand("Delete");
//                        document.getElementById("uploadPhoto").value= "";
                            $("div.buttonforup div.button").html("上传头像<input type=\"file\" name=\"uploadPhoto\" id=\"uploadPhoto\" value=\"上传\" onclick=\"getImgSrc()\" onchange=\"resetimageFileInput()\" size=\"1\"/>");
                            $("input#uploadPhoto").css({opacity:0});
                        }, 50
                    );
            }
        }
    </script>
</asp:Content>
