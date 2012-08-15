<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    用户照片审核
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Admin_TopNav">
        <% Html.RenderPartial("~/Views/Admin/AdminTopNav.ascx");%>
    </div>
    <div class="Admin_Dic_LeftNav">
        <ul class="leftlist magT10">
            <li><a href="/Admin/photoaudit?type=30">兴趣图片</a></li>
            <li><a href="/Admin/photoaudit?type=1">用户头像</a></li>
            <li><a href="/Admin/photoaudit?type=201">内容图片</a></li>
        </ul>
    </div>
    <div class="content magT10">
        <div class="contenttit">
            <% string type = "30";
               if (ViewData["type"] != null) type = ViewData["type"].ToString(); %>
            <% if (type == "201")
               { %>
                按内容类型：<select id="viewTypePictureAudits" name="viewTypePictureAudits" onchange="viewPictureAudit($(this).val())">
                        <option value="201" selected="selected">图片内容</option>
                        <option value="202">说说内容</option>
                        <option value="203">号召内容</option>
                        <option value="204">我想内容</option>
                        </select>
            <% } %>
        </div>
        <div class="selectbtn">
            <a id="btnInterestPhotoAuditAllPicture" href="javascript:;" onclick="audltokall(30)">审核兴趣图片当前页</a> 
            <a id="memberAvatarAuditAllPicture" href="javascript:;" onclick="audltokall(1)">审核用户头像当前页</a>
            <a id="ImageContentAuditAllPicture" href="javascript:;" onclick="audltokall(201)">审核图片内容图片当前页</a>
            <a id="SuoSuoContentAuditAllPicture" href="javascript:;" onclick="audltokall(202)">审核说说内容图片当前页</a>
            <a id="CallForContentAuditAllPicture" href="javascript:;" onclick="audltokall(203)">审核号召内容图片当前页</a>
            <a id="IWantContentAuditAllPicture" href="javascript:;" onclick="audltokall(204)">审核我想内容图片当前页</a>
        </div>
        <ul id="listcontiner" class="impresslist">
        </ul>
        <div class="clearfix">
        </div>
        <br />
        <br />
        <div style="width: 900px; padding-bottom: 30px; text-align: center; margin-top: 20px;
            clear: both; float: none;">
            <center>
                <div id="pager" class="verifyPager">
                </div>
            </center>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/admin.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        uploadpath = '<%=ViewData["uploadpath"] %>';

        var photos = [];
        var pageNo = 1;
        var pageCount = 0;
        var pageSize = 56;
        var pageCount2 = 0;
        var mypagecount = 0;

        var valPhotoType = <%=ViewData["type"] == null ? "30" : ViewData["type"].ToString() %>,
            valIsAudited = 0;

        $(document).ready(function () {
            viewPictureAudit(valPhotoType);
        });
        function viewPictureAudit(type) {
            pictureAuditCount(type);
            bindphotos(type);
        }
        function bindphotos(photoType) {
            $(".selectbtn a").each(function () {
                $(this).css('display', 'none');
            });
            $("div.selectbtn a").css('display', 'none');
            switch (Number(photoType)) {
                case 30:
                    $("#btnInterestPhotoAuditAllPicture").css('display', 'block');
                    break;
                case 1:
                    $("#memberAvatarAuditAllPicture").css('display', 'block');
                    break;
                case 201:
                    $("#ImageContentAuditAllPicture").css('display', 'block');
                    break;
                case 202:
                    $("#SuoSuoContentAuditAllPicture").css('display', 'block');
                    break;
                case 203:
                    $("#CallForContentAuditAllPicture").css('display', 'block');
                    break;
                case 204:
                    $("#IWantContentAuditAllPicture").css('display', 'block');
                    break;
                default:
                    break;
            }
            valPhotoType = photoType;
            adminImageAuditDataProvider.getauditphotos(photoType, 0, pageSize, pageNo, function (objs) {
                photos = objs;
                binding(photoType);
            });
        }
        function pictureAuditCount(type) {
            $.ajaxSetup({ async: false });
            switch (true) {
                case true:
                    adminImageAuditDataProvider.getTypeAuditPhotoCount(type, valIsAudited, function (data) {
                        pageCount = $.parseJSON(data);
                    });
                    break;
                default:
                    break;
            }
            $.ajaxSetup({ async: true });

            pageCount2 = parseInt((parseInt(pageCount) + parseInt(pageSize) - 1) / parseInt(pageSize));
            mypagecount = pageCount2;
            if (mypagecount >= 1 && pageNo > mypagecount) {
                pageNo = mypagecount;
            }
            setPager(type);
        }
        function audltokall(photoType) {
            var ps = "";
            $.each(photos, function (i) {
                if (photos[i] != null) {
                    ps += photos[i].ID + ",";
                }
            });
            if (ps != "") {
                adminImageAuditDataProvider.auditphotos(ps, 1, photoType, function (result) {
                    if ($.parseJSON(result).ok) {
                        viewPictureAudit(photoType);
                    }
                });
            }
            else
                viewPictureAudit(photoType);
        }
        function binding(photoType) {
            var str = "";
            $.each(photos, function (i) {
                if (photos[i] != null) {
                    str += "<li><img src='" + photofunctions.getprofileiconphotoname(photos[i].FileName) + "'/><br/>";
                    str += "<a onclick='delphoto(\"" + photos[i].ID + "\", \"" + photoType + "\")' href='javascript:;'>删除</a>  |  <a onclick='beautyphoto(\"" + photos[i].ID + "\")' href='javascript:;'>推荐</a></li>";
                }
            });
            $("#listcontiner").html(str);
        }
        function delphoto(id, photoType) {
            adminImageAuditDataProvider.auditphoto(id, -1, photoType, function (result) {
                if ($.parseJSON(result).ok) {
                    $.each(photos, function (i) {
                        if (photos[i] != null)
                            if (photos[i].ID == id) photos[i] = null;
                    });
                    viewPictureAudit(valPhotoType);
                }
                else {
                    alert("删除失败");
                }
            });
        }
        function beautyphoto(id) {
            adminImageAuditDataProvider.beautyphoto(id, 1, function (result) {
                if ($.parseJSON(result).ok) {
                    $.each(photos, function (i) {
                        if (photos[i] != null)
                            if (photos[i].ID == id) photos[i] = null;
                    });
                    viewPictureAudit(valPhotoType);
                }
                else {
                    alert("推荐失败");
                }
            });
        }

        pictureAuditClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) {
                pageNo = mypagecount;
            }
            else {
                pageNo = pageclickednumber;
            }
            $("#pager").pager({
                pagenumber: pageNo,
                pagecount: mypagecount,
                buttonClickCallback: pictureAuditClick
            });
            bindphotos(valPhotoType);
        }
        function setPager(type) {
            $("#pager").pager({
                pagenumber: pageNo,
                pagecount: mypagecount,
                buttonClickCallback: pictureAuditClick
            });
        }
    </script>
</asp:Content>
