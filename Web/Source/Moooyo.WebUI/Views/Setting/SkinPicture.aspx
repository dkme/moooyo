<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.Setting.MemberSkinModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	个性图片
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
	    <% Html.RenderPartial("~/Views/Setting/LeftPanel.ascx");%>
		<div class="Set_content">
		    <div class="Set_title"> <span>设置个性图片</span><em>▲</em><a href="/Setting/Skin" class="backset blue01">返回个性设置</a></div>
			 <div class="Set_box">
			         <div class="bind_form  tabbox1"><span id="pageCounter" data-pageValue="1"></span>
				   <span class="at_text t_1_d">  |   <font color="#333" style=" width:400px; padding-right:220px;">在推荐图片中选择一个作为我的个性图片</font>  <a href="javascript:;" class="changepic blue01" onclick="bindPersonalityPicture()">换一批</a> <a href="javascript:;" class="changepic tabnav1 blue01">自己上传</a> </span>
				    <span class="at_text  p15">
					     <ul class="setpic_list">
                            <% if (Model.memberSkinList.Count < 1)
                               { %>
                                没有图片哦，<a href="javascript:;" class="changepic tabnav1 blue01" style="margin-left:0px; padding-left:0px;">自己上传</a>？
                            <% } %>
						    <% foreach (Moooyo.BiZ.Member.MemberSkin.MemberSkin memberSkin in Model.memberSkinList)
                              { %>
                            <li><a href="javascript:;" onclick="setPersonalityPicture('<%=memberSkin.ID %>')"><img src="<%=Comm.getImagePath(memberSkin.PersonalityPicture, ImageType.Original) %>" width="10" height="10" alt="个性背景图片" onload="ImageZoomToJquery($(this), 290)" /></a></li>
                           <% } %>
						 </ul>
				    </span> 
			      </div>
			        <div class="bind_form tabbox2">
				        <span class="at_text t_1_d">  |   <font color="#333" style=" width:400px; padding-right:300px;">上传自己喜欢的图片作为我的个性图片</font>   <a href="javascript:;" class="changepic tabnav2 blue01">推荐图片</a> </span>
					  <div class="bind_com pr65 mtb20">
					   <div class="pic_upload"><%--<a href="#" class="link_uploat"></a>--%>
                        <form action="/Shared/UploadTempCustomPhoto" method="post" enctype="multipart/form-data" id="frmMemberSkinPicture" name="frmMemberSkinPicture">
                            <input type="hidden" id="pictureType" name="pictureType" value="PersonalityPicture" />
			                <div class="buttonforup reg_btn btn link_uploat">
                                <div class="button">
                                    <input type="file" name="uploadPhoto" id="uploadPhoto" value="上传" onclick="uploadPersonalityPicture()" onchange="changePersonalityPicture()" size="1"/>
                                </div>
                            </div>
                        </form>
                       宽大于766像素，高大于200像素，jpg、gif或png格式，单张小于5MB。</div>  
					</div>	 
                   </div>
			
			 </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
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
    <script type="text/javascript">
        uploadpath = "<%=ViewData["uploadpath"] %>";
        $(document).ready(function () {
            SetSkinShowHide();
            $("input#uploadPhoto").css({opacity:0});
        });
        function bindPersonalityPicture() {
            var str = "";
            var pageCount = <%=Model.Pagger.PageCount %>;
            var pageNo = $("#pageCounter").attr("data-pageValue");
            if(pageNo < pageCount) 
                pageNo = Number(pageNo) + 1;
            else 
                pageNo = 1;
            memberprovider.getPersonalityPicture2(8, pageNo, "PersonalityPicture", function (data) {
                var objs = $.parseJSON(data);
                $.each(objs, function (i) {
                    str += "<li><a href=\"javascript:;\" onclick=\"setPersonalityPicture('" + objs[i].ID + "')\"><img src=\"" + photofunctions.getnormalphotoname(objs[i].PersonalityPicture) + "\" width=\"10\" height=\"10\" alt=\"个性背景图片\" onload=\"ImageZoomToJquery($(this), 290)\" /></a></li>";
                });
                $("ul.setpic_list").html(str);
            });
            $("#pageCounter").attr("data-pageValue", pageNo);
        }
        function setPersonalityPicture(ppId) {
            memberprovider.setMemberSkin(ppId, "PersonalityPicture", function (data) {
                var result = $.parseJSON(data);
                if(result.ok) {
                    window.location.href = "/Setting/Skin";
                }
                else $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error');
            });
        }
        function uploadPersonalityPicture() {
            var imgSrc = $("#uploadPhoto").val();
            if (imgSrc != "" && imgSrc != null && imgSrc != undefined) {
                if (!imgSrc.match(/.jpg|.gif|.png/i)) {
                    $.jBox.tip("图片类型必须是gif, jpg, png中的一种哦！", "err");
                    return false;
                }
                $('#frmMemberSkinPicture').ajaxSubmit(function (data) {
//                    if ((data != null) && (data != "") && (data != -1)) {
//                        var obj = $.parseJSON(data);
//                        if(obj.ok)
//                        {
//                            setTimeout(function () {
//                                window.location.href = "/Setting/Skin";
//                            }, 100);
//                        }
//                        else
//                        {
//                            $.jBox.tip(obj.info, 'err');
//                        }
//                    }
                    if ((data != null) && (data != "") && (data != -1)) {
                        var dataArr = data.split('|');
                        if(dataArr[0] != "" && dataArr[1].toString() == "SUCCESS") {
                            actionprovider.openCustomBigPicture('/Setting/SkinPicture', dataArr[0], 11, "PersonalityPicture", 4, 800, 200, '');
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
        function changePersonalityPicture() {
            var uploadPhoto=$("#uploadPhoto");
            if(uploadPhoto.val()!=""){
                uploadPhoto.unbind("onchange");
                uploadPhoto.removeAttr("onchange");
                uploadPhoto.change(function(){});
                uploadPersonalityPicture();
            }
        }
    </script>
</asp:Content>
