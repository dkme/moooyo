<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	图片剪裁
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--<div class="clippingArea">
        <div class="rel mb20">
            <img id="xuwanting" class="hidden" src="" />
            <span id="preview_box" class="crop_preview"><img id="crop_preview" src="" class="hidden" /></span>
        </div>
    </div>
    <div class="clippingForm">
    <form action="/Register/UploadTempCustomPhoto" method="post" enctype="multipart/form-data" id="UploadTempCustomPhotoForm" name="UploadTempCustomPhotoForm" class="fl">
        <input type="file" name="uploadPhoto" id="uploadPhoto" value="浏览..." onchange="getImgSrc()" />
    </form>
    <form action="/Register/CustomSmallPicture" method="post" id="crop_form" class="fl ml20">
        <input type="hidden" id="x" name="x" />
        <input type="hidden" id="y" name="y" />
        <input type="hidden" id="w" name="w" />
        <input type="hidden" id="h" name="h" />
        <input type="hidden" id="uploadPhoto" name="uploadPhoto" value="" />
        <input type="submit" value="&nbsp;&nbsp;确认剪裁并保存&nbsp;&nbsp;" id="crop_submit" />
    </form>
    </div>--%>

    <div class="zxx_out_box">
    <div class="zxx_in_box">
        <div class="zxx_header">
            
            <div class="fl">
                <font color="#4fb3d6" size="2">拖拽高亮框可以移动裁剪位置，鼠标移到角柄上可以调整大小。</font>
            </div>

            <div class="fr">
                <input type="button" id="resetSelectPhoto" value="重选图片" class="fr ml10 reg_btn btn" onclick="window.parent.jBox.close(true);" style="display:none;" />
                <input type="button" id="resetSelectPhoto2" value="重选图片" class="fr ml10 reg_btn btn" onclick="window.location.reload();" style="display:none;" />
                <form action="/Shared/CustomBigPicture" method="post" id="crop_form" class="fr ml20">
                    <input type="hidden" id="x" name="x" />
                    <input type="hidden" id="y" name="y" />
                    <input type="hidden" id="w" name="w" />
                    <input type="hidden" id="h" name="h" />
                    <input type="hidden" id="uploadAvatar" name="uploadPhoto" value="" />
                    <input type="hidden" id="getjump" name="getjump" value="<%=ViewData["getjump"] %>" />
                    <input type="hidden" id="phototype" name="phototype" value="<%=ViewData["phototype"] %>" />
                    <input type="hidden" id="skintype" name="skintype" value="<%=ViewData["skinType"] %>" />
                    <input type="hidden" id="pictureproportion" name="pictureproportion" value="<%=ViewData["pictureProportion"] %>" />
                    <input type="hidden" id="defaultwidth" name="defaultwidth" value="<%=ViewData["defaultwidth"] %>" />
                    <input type="hidden" id="defaultheight" name="defaultheight" value="<%=ViewData["defaultheight"] %>" />
                    <input type="hidden" id="contentid" name="contentid" value="<%=ViewData["contentid"] %>" />
                    <input type="button" value="保存图片" id="crop_submit" style="display:none;" class="reg_btn btn" />
                </form>
            </div>

        </div> 
        <h1 class="zxx_title">

        </h1>
        <div class="zxx_main_con">

        	<div class="zxx_test_list">

                <div class="rel">
                    <%--<div class="clearBoth" style="float:none; line-height:18px; margin-bottom:10px;">
                        <font color="#666666" size="2">原始大小</font>
                    </div>--%>
                    <div class="originalImage">
                	<img id="xuwanting" class="hidden" src="" />
                    <form action="/Shared/UploadTempCustomPhoto" method="post" enctype="multipart/form-data" id="UploadTempCustomPhotoForm" name="UploadTempCustomPhotoForm" class="fl hidden">
                    <div class="buttonforupdiv">
                    <div class="buttonforup btn">
                        <div class="button">
                            点击浏览
                            <input type="file" name="uploadPhoto" id="uploadPhoto" value="浏览..." onchange="getImgSrc()" size="1"/>
                        </div>
                    </div><br />
                    <font style="color:#999; line-height:20px;" size="2">仅支持gif,jpg,png等格式，且文件大小小于5M，建议尽量上传较清晰图片。</font>
                    </div>
                    </form>
                    
                    </div>
                    
                    <%--<div style="position:absolute; left:520px; top:0px; width:150px; height:150px; color:#9a9a9a; font-size:12px;">1.(150×150)像素尺寸
                    </div>
                    <span id="preview_box" class="crop_preview"><img id="crop_preview" src="" class="hidden" /></span>
                    <div style="position:absolute; left:520px; top:190px; width:150px; height:150px; color:#999999; font-size:12px;">2.(50×50)像素尺寸
                    </div>
                    <span id="preview_box2" class="crop_preview2"><img id="crop_preview2" src="" class="hidden" /></span>--%>
                </div>
                
            </div>
        </div>
        <%--<div class="zxx_footer">
            
            
        </div>--%>
    </div>
</div>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    <link rel="stylesheet" href="/css/jquery.Jcrop.css" type="text/css" media="screen"/>
    <style type="text/css">
        
        .clippingArea{ margin-top:10px; width: 700px; height:500px; margin:0px auto; }
        .clippingForm{ margin-top:10px; width: 700px; height:40px; margin:0px auto; line-height:26px; }
        /*.crop_preview{position:absolute; left:520px; top:18px; width:150px; height:150px; overflow:hidden; background-color:#dfdfdf;}
        .crop_preview2{position:absolute; left:520px; top:210px; width:50px; height:50px; overflow:hidden; background-color:#dfdfdf;}*/
    
        body{padding:0; margin:0; font-size:84%; background:#eeeeee; color:#333333; font-family:'宋体',Verdana, Geneva, sans-serif;}
        ul,li,form,h1,h2,h3,h4,h5,h6,p{padding:0; margin:0; list-style-type:none;}
        i,cite{font-style:normal;}
        a{color:#34538b; text-decoration:underline;}a:hover{color:#F30; text-decoration:underline;}
        .l{float:left;}.r{float:right;}.cl{clear:both;}img{border:0;}
        .tc{text-align:center;}.tr{text-align:right;}
        
        .g0{color:#000000;}.g3{color:#333333;}.g6{color:#666666;}.g9{color:#999999;}
        .vm{vertical-align:middle;}.vtb{vertical-align:text-bottom;}.vt{vertical-align:top;}.vn{vertical-align:-2px;}
        .ml2{margin-left:2px;}.ml5{margin-left:5px;}.ml10{margin-left:10px;}.ml20{margin-left:20px;}.mr2{margin-right:2px;}.mr5{margin-right:5px;}.mr10{margin-right:10px;}.mr20{margin-right:20px;}.mt2{margin-top:2px;}.mt5{margin-top:5px;}.mt10{margin-top:10px;}.mt20{margin-top:20px;}.mb2{margin-bottom:2px;}.mb5{margin-bottom:5px;}.mb10{margin-bottom:10px;}.mb20{margin-bottom:20px;}
        .f9{font-size:0.9em;}.f10{font-size:1em;}.f11{font-size:1.1em;}.f12{font-size:1.2em;}.f13{font-size:1.3em;}.f14{font-size:1.4em;}.f15{font-size:1.5em;}.f16{font-size:1.6em;}
        .fix{zoom:1;}.fix:after,.fix:before{display:block; content:"clear"; height:0; clear:both; overflow:hidden; visibility:hidden;}
        .rel{position:relative;}.abs{position:absolute;}.fixed{position:fixed;}
        .dn{display:none;}.db{display:block;}.dib{display:inline-block;}.di{display:inline;}
        .dot{background:url() repeat-x 0 bottom;}
        .zxx_out_box{width:100%; margin:0 auto;}
        .zxx_in_box{min-height:488px; _height:488px; height:488px; background:white; padding:15px 15px 0;}
        .zxx_header{padding:5px 5px 5px 5px; overflow:hidden; zoom:1;}
        .zxx_author_time{float:right; margin-top:34px; color:#787878; font-family:"Courier New", Courier, monospace;}
        .zxx_title{font-size:1.6em; text-align:center; margin:0px 0;}
        .zxx_main_con{padding:10px 0px 0px 10px; clear:both; height:390px;}
        .zxx_footer{padding-bottom:0px; border-top:1px solid #dddddd; line-height:26px; padding-top:13px;}
        .zxx_test_list{padding-left:10px; font-size:1.1em; padding-top:5px; line-height:1.3; overflow:hidden; zoom:1; clear:both;}
        .zxx_code{display:block; padding:10px; margin:5px 0; background:#eeeeee; border:1px dashed #cccccc; clear:both; zoom:1;}
        .zxx_code xmp{margin:0; color:#00F; font-size:12px; white-space:pre-wrap; word-wrap:break-word;}
        .zxx_btn{display:inline-block; background:url(../image/down_btn.png) no-repeat; padding-left:25px;}
        .zxx_btn span,.zxx_btn a{display:inline-block; height:45px; line-height:45px; background:url(../image/down_btn.png) no-repeat right top; padding:0 45px 0 20px; cursor:pointer;}
        .zxx_btn:hover{text-decoration:none; color:#34538b;}
        .zxx_ad_left{position:absolute; left:10px; top:120px; padding:10px 6px; background:white;}
        .zxx_ad_right{position:absolute; right:10px; top:120px; padding:10px 6px; background:white;}
        .zxx_ad_left.fixed,.zxx_ad_right.fixed{position:fixed!important; position:absolute;}
        .zxx_ad_fixed{top:5px; position:fixed!important; position:absolute;}
        .originalImage{width:720px; height:400px; border:solid 2px #cdcdcd; padding:2px; text-align:center; position:relative;}
        
        .buttonforupdiv{position:absolute; top:170px; text-align:center; width:350px; left:50px;}
        .buttonforup{width:100px; height:40px; cursor:auto; font-size:15px; margin-left:115px;}
        .buttonforup .button{width:70px; height:20px; line-height:21px; margin:10px 15px; text-align:center; overflow:hidden; cursor:pointer; position:relative;}
        #uploadPhoto{width:90px; cursor:pointer; position:absolute; left:0px; top:0px; height:32px;}
        
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script type="text/javascript" src="/js/base_<%=ViewData["jsversion"] %>.js"></script>
    <script type="text/javascript" src="/js/data_<%=ViewData["jsversion"] %>.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.Jcrop.js"></script>
    
    <script src="/scripts/jquery.form.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        var photoRelativePath, photoRelativePathVal;

        $(document).ready(function () {
            $("#uploadPhoto").css({opacity:0});
            photoRelativePath = "<%=ViewData["photoRelativePath"] %>";

            if(photoRelativePath != "" && photoRelativePath != null) {
                $("#resetSelectPhoto").show();
                showCropPanel(photoRelativePath);
            }
            else {
                $("#UploadTempCustomPhotoForm").removeClass("hidden").show();
            }

            $("#crop_submit").click(function () {
                if (!isNaN($("#x").val()) && $("#w").val() != 0 && $("#h").val() != 0) {
                    $("#crop_form").submit();
                    $.jBox.tip("成功保存图片", "success");
                } else {
                    $.jBox.tip("要先在图片上划一个选区再单击保存图片的按钮哦！", "info");
                }
            });

        });

        function showCropPanel(photoRelatPath) {
            $("#uploadAvatar").val(photoRelatPath);
            $("#crop_submit").show();
            $("#xuwanting").removeClass("hidden").show();
            $("#xuwanting").attr("src", photoRelatPath + '?' + Math.random(new Date().getTime() / 1000).toString().substring(2, 7));
            //ImageZoomToJquery($("#xuwanting"), 500);
//            $("#crop_preview").removeClass("hidden").show();
//            $("#crop_preview").attr("src", photoRelatPath + '?' + Math.random());
//            $("#crop_preview2").removeClass("hidden").show();
//            $("#crop_preview2").attr("src", photoRelatPath + '?' + Math.random());

            var pictureProportion = $("#pictureproportion").val();
            var defaultwidth = $("#defaultwidth").val();
            var defaultheight = $("#defaultheight").val();

            var timeoutID = setTimeout(function () {
                var jcropApi = $("#xuwanting").Jcrop({
                    aspectRatio: Number(pictureProportion),
                    onChange: showCoords,
                    onSelect: showCoords,
                    setSelect: [0, 0, Number(defaultwidth), Number(defaultheight)],
                    boxWidth: 720,
                    boxHeight: 400
                });
            }, 100);
        }

        function getImgSrc() {
            var imgSrc = $("#uploadPhoto").val();

            if (imgSrc != "") {
                if (!imgSrc.match(/.jpg|.gif|.png/i)) {
                    $.jBox.tip("图片类型必须是gif,jpg,png中的一种哦！", "err");
                    return false;
                }
//                $.ajax({
//                    type: 'POST',
//                    url: "/Register/UploadTempCustomPhoto",
//                    data: $("#UploadTempCustomPhotoForm").serialize(), //序列化表单里所有的内容
//                    success: function (data) {
//                        alert(data);
//                        var objs = $.parseJSON(data);
//                        $("#xuwanting").removeClass("hidden").show();
//                        $("#xuwanting").attr("src", objs.filePath);
//                        $("#crop_preview").removeClass("hidden").show();
//                        $("#crop_preview").attr("src", objs.filePath);
//                        if (objs.state != "SUCCESS") {
//                            $.jBox.tip(objs.state, "err");
//                            return false;
//                        }
//                    },
//                    error: function (data) {
//                    }
//                });
                $('#UploadTempCustomPhotoForm').ajaxSubmit(function (data) {
                    if ((data != null) && (data != "") && (data != -1)) {
//                        $("#uploadAvatar").val(data);
//                        $("#uploadPhoto").hide();
//                        $("#crop_submit").show();
//                        $("#resetSelectPhoto").show();
//                        $("#xuwanting").removeClass("hidden").show();
//                        $("#xuwanting").attr("src", data + '?' + Math.random());
//                        //ImageZoomToJquery($("#xuwanting"), 500);
//                        $("#crop_preview").removeClass("hidden").show();
//                        $("#crop_preview").attr("src", data + '?' + Math.random());
//                        //ImageZoomToJquery($("#crop_preview"), 150);
//                        //记得放在jQuery(window).load(...)内调用，否则Jcrop无法正确初始化
//                        var api = $("#xuwanting").Jcrop({
//                            aspectRatio: 1,
//                            onChange: showCoords,
//                            onSelect: showCoords,
//                            setSelect: [0, 0, 150, 150]
//                        });
                        var dataArr = data.split('|');
                        if(dataArr[0] != "" && dataArr[1].toString() == "SUCCESS") {
                            $("#UploadTempCustomPhotoForm").hide();
                            $("#resetSelectPhoto2").show();
                            showCropPanel(dataArr[0]);
                        }
                        else $.jBox.tip(dataArr[1], "err");
                    }
                    else $.jBox.tip("系统维护中，给您带来了不便，请谅解！", "err");
                });      
            }
        }
        //简单的事件处理程序，响应自onChange,onSelect事件，按照上面的Jcrop调用
        function showCoords(obj) {
            $("#x").val(obj.x);
            $("#y").val(obj.y);
            $("#w").val(obj.w);
            $("#h").val(obj.h);
//            if (parseInt(obj.w) > 0) {
//                //计算预览区域图片缩放的比例，通过计算显示区域的宽度(与高度)与剪裁的宽度(与高度)之比得到
//                var rx = $("#preview_box").width() / obj.w;
//                var ry = $("#preview_box").height() / obj.h;
//                //通过比例值控制图片的样式与显示
//                //alert(Math.round(rx * $("#xuwanting").width()) + "px");
//                $("#crop_preview").css({
//                    width: Math.round(rx * $("#xuwanting").width()) + "px", //预览图片宽度为计算比例值与原图片宽度的乘积
//                    height: Math.round(rx * $("#xuwanting").height()) + "px", //预览图片高度为计算比例值与原图片高度的乘积
//                    marginLeft: "-" + Math.round(rx * obj.x) + "px",
//                    marginTop: "-" + Math.round(ry * obj.y) + "px"
//                });
//                //计算预览区域图片缩放的比例，通过计算显示区域的宽度(与高度)与剪裁的宽度(与高度)之比得到
//                var rx = $("#preview_box2").width() / obj.w;

//                var ry = $("#preview_box2").height() / obj.h;
//                //通过比例值控制图片的样式与显示
//                $("#crop_preview2").css({
//                    width: Math.round(rx * $("#xuwanting").width()) + "px", //预览图片宽度为计算比例值与原图片宽度的乘积
//                    height: Math.round(rx * $("#xuwanting").height()) + "px", //预览图片高度为计算比例值与原图片高度的乘积
//                    //width: 50, height: 50,
//                    marginLeft: "-" + Math.round(rx * obj.x) + "px",
//                    marginTop: "-" + Math.round(ry * obj.y) + "px"
//                });
//            }
        }
    </script>
</asp:Content>
