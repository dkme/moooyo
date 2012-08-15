<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    注册 米柚网-单身欢乐季
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="wrap1">
        <div class="contain2">
            <div class="login_left w480" id="change1">
             
                <div class="upload">
                   <h1>你是哪种柚子?</h1>
                 </div>
                <div class="upload">
                <span class="red mb15">这样的头像是遇不到好柚子的：</span>
                    <span>
                        <img src="/pics/noicon.jpg" alt="头像" title="头像" id="imgRegisterMemberFacePreview"
                            width="150" height="150" border="0" /></span> <span class="red" style="padding-top:30px;">最后一步啦，上传一张你的近照<br />
                                一切虚假的头像都有可能导致分析结果有所偏差哦</span> <span class="divbtn">
                                    <form action="/Shared/UploadTempCustomPhoto" method="post" enctype="multipart/form-data"
                                    id="UploadTempCustomPhotoForm" name="UploadTempCustomPhotoForm" class="fl">
                                    <input type="hidden" name="AUTHID" id="AUTHID" value="<%=Request.Cookies[FormsAuthentication.FormsCookieName].Value%>" />
                                    <input type="hidden" name="ASPSESSID" id="ASPSESSID" value="<%=Session.SessionID %>" />
                                    <div class="buttonforup reg_btn btn">
                                        <div class="button">
                                            <label style="margin-left:-10px;">上传头像</label>
                                            <input type="file" name="uploadPhoto" id="uploadPhoto" value="上传" onclick="getImgSrc()"
                                                onchange="resetimageFileInput()" size="1" />
                                        </div>
                                    </div>
                                    </form>
                                </span>
                                <span style="padding-left:95px; padding-top:12px; display:block;"><a href="/Register/UploadFinish">不上传</a></span>
                </div>
            </div>
            <div class="login_right h610">
                <div class="box_com h90 ">
                    <img src="/pics/mylogo.gif" alt="米柚网" /></div>
                <div class="box_com h320">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" href="/css/jquery.Jcrop.css" type="text/css" media="screen"/>
<style type="text/css">
.upload { padding:45px 20px 0 45px; font: normal 14px/28px "微软雅黑";}
.upload span { display:block;}
.upload span.divbtn { padding-top:15px; display:block;}
.upload span.red { color:#9d0101; font-size:12px; line-height:20px;}
.mb15 { padding-bottom:15px;}
h1 { font-size:36px; line-height:40px; font-weight:400; font-family:"\5FAE\8F6F\96C5\9ED1"; text-align:left}
a{font-size:12px; color:#cccccc;}
a:visited{font-size:12px; color:#cccccc;}
a:hover{font-size:12px; color:#cccccc;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/scripts/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        //*** 禁止浏览器后退 *****************
        //禁用后退按钮
        window.history.forward(1);
        //禁用后退键，作用于Firefox、Opera  
        document.onkeypress = banBackSpace;
        //禁用后退键，作用于IE、Chrome  
        document.onkeydown = banBackSpace;
        //************************************ 
        //    var settings;
        var uploadpath = "<%=ViewData["uploadpath"] %>";
        function resetimageFileInput(object) {
            var uploadPhoto=$("#uploadPhoto");
            if(uploadPhoto.val()!=""){
                uploadPhoto.unbind("onchange");
                uploadPhoto.removeAttr("onchange");
                uploadPhoto.change(function(){});
                getImgSrc();
            }
        }
        $(document).ready(function () {
            $("body").css({ "background-color": "#dedee0" });
            $("#uploadPhoto").css({opacity:0});
        });
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
                            actionprovider.openCustomSmallPicture('/Register/UploadFinish', dataArr[0], 1);
                        }
                        else 
                            $.jBox.tip(dataArr[1], "err");
                    }
                    else 
                        $.jBox.tip("系统维护中，给您带来了不便，请谅解！", "err");
                });

                var timeoutId = setTimeout(function () {
                    $("div.buttonforup div.button").html("<label style=\"margin-left:-10px;\">上传头像</label><input type=\"file\" name=\"uploadPhoto\" id=\"uploadPhoto\" value=\"上传\" onclick=\"uploadPersonalityPicture()\" onchange=\"changePersonalityPicture()\" size=\"1\"/>");
                    $("input#uploadPhoto").css({opacity:0});
                }, 50);
            }
        }
    </script>
</asp:Content>
