<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    审核管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Admin_TopNav">
        <% Html.RenderPartial("~/Views/Admin/AdminTopNav.ascx");%>
    </div>
    <div class="Admin_Dic_LeftNav">
        <ul class="leftlist magT10">
            <li><a href="/admin/verify/verifytext">待审内容</a></li>
            <li><a href="/admin/checkphoto">视频认证</a></li>
        </ul>
    </div>
    <div class="content magT10">
        <div class="contenttit">
            查看：
            <select id="selectphoto" name="selectphoto" onchange="Rebind()">
                <option value="<%= Convert.ToInt32(Moooyo.BiZ.PhotoCheck.CheckPhotoStatus.waitaudit)%>">
                    等待审核</option>
                <option value="<%= Convert.ToInt32(Moooyo.BiZ.PhotoCheck.CheckPhotoStatus.audidel)%>">
                    审核未通过</option>
                <option value="<%= Convert.ToInt32(Moooyo.BiZ.PhotoCheck.CheckPhotoStatus.auditpass)%>">
                    审核通过</option>
            </select>
            操作：
            <input id="btnCheckPhoto" name="btnCheckPhoto" type="button" value="全部通过" onclick="UpdateCheckPhotoLot('<%= Convert.ToInt32(Moooyo.BiZ.PhotoCheck.CheckPhotoStatus.auditpass)%>')" />
            <input id="btnCheckPhotodel" name="btnCheckPhotodel" type="button" value="彻底删除本页"
                onclick="RemoveCheckPhotoLot()" />
        </div>
        <ul class="verify" id="listcontiner">
        </ul>
    </div>
    <div id="pager" class="verifyPager">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/scripts/jquery.form.js" type="text/javascript"></script>
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/admin.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    
        var uploadpath = '<%=ViewData["uploadpath"] %>';
        $(document).ready(function(){
    
            var _type =<%= Convert.ToInt32(Moooyo.BiZ.PhotoCheck.CheckPhotoStatus.waitaudit)%>;
            settype(_type);
            count();
            setPager();
            binddata();
        });

        function binddata()
        {
             AdminProvider.GetAllCheckPhoto(type, pageno, pagesize, function(result){
                var objs = $.parseJSON(result);
                var str = "";
                $.each(objs,function(i){
                    str +="<li><input type='checkbox' name='checkbox' value='" + objs[i].ID + "' id='check" + i + "'/>&nbsp;&nbsp;&nbsp;&nbsp;";
                    if( objs[i].CheckStatus == 8 || objs[i].CheckStatus == 7 )
                    {
                        str +="<a onclick=\"UpdateCheckPhoto('" +objs[i].ID +"','<%= Convert.ToInt32(Moooyo.BiZ.PhotoCheck.CheckPhotoStatus.audidel)%>')\">不通过</a>&nbsp;";
                    }
                    if( objs[i].CheckStatus == <%= Convert.ToInt32(Moooyo.BiZ.PhotoCheck.CheckPhotoStatus.waitaudit)%>)
                    {
                        str +="<a onclick=\"UpdateCheckPhoto('" +objs[i].ID +"','<%= Convert.ToInt32(Moooyo.BiZ.PhotoCheck.CheckPhotoStatus.auditpass)%>')\">通过</a>&nbsp;";
                    }
                    str +="<a onclick=\"RemoveCheckPhoto('" +objs[i].ID +"')\">删除存档</a>&nbsp;<b/><br/>";   
                    str += "<img src=\"" + photofunctions.getprofileiconphotoname(objs[i].UserHeadName)  +"\" alt=\"\">";                    
                    str +="<img src=\"../photo/get/" + objs[i].CheckImgPath+ "\" alt=\"\"/><br/></li>";  
                    str += "<input type=\"hidden\" value=\"" + objs[i].UserId + "\" id=\"hidden" + objs[i].ID + "\" />";
                    str += "<input type=\"hidden\" value=\"" + objs[i].CheckImgPath + "\" id=\"img" + objs[i].ID + "\" />";
                      
                });
  
                $("#listcontiner").html(str);
            });
            $("#selectphoto").attr("value",type);
        }

        function Rebind()
        {
            var _type = $("#selectphoto").val();
            settype(_type);
            count();
            setPager();  
            if(type != <%=Convert.ToInt32(Moooyo.BiZ.PhotoCheck.CheckPhotoStatus.waitaudit) %>)
            {
                $("#btnCheckPhoto").attr("disabled", "disabled");      
            }
            else
            {
                $("#btnCheckPhoto").attr("disabled", "");
            }
            binddata();
        }

        function UpdateCheckPhoto(id, _type)
        {
            var userid = $("#hidden" + id).val();
            //settype(_type);
            var adminid = "<%=ViewData["me"].ToString() %>";
            AdminProvider.UpdateCheckPhoto(id, _type, adminid, userid,function(result) {
                if ($.parseJSON(result).ok) {
                    alert("更新成功");
                }
                else {
                    alert("更新失败");
                }
                count();
                setPager();  
                binddata();
            });
        }
        function RemoveCheckPhoto(id)
        {
            if(!confirm("确定删除存档？"))
                return;
            settype($("#selectphoto").val());
            var img = $("#img" + id).val();
            AdminProvider.RemoveCheckPhotos(id,img,function(result) {
                if ($.parseJSON(result).ok) {
                   //alert("更新成功");
                }
                else {
                    alert("更新失败");
                }
                count();
                setPager();  
                binddata();
            });    
        }
        function UpdateCheckPhotoLot(_type)
        {
            //settype(_type);
            var idlist="";
            var userlist ="";
            $("input[name='checkbox']").each(function(){    
                var tvalues = $(this).val();
                idlist += tvalues + "&";   
                userlist += $("#hidden" + tvalues).val() + "&";
            });
            idlist = idlist.substring(0, idlist.length - 1);
            userlist = userlist.substring(0, userlist.length - 1);
            var adminid = "<%=ViewData["me"].ToString() %>";
            AdminProvider.UpdateCheckPhotos(idlist, _type, adminid, userlist,function(result) {
                if ($.parseJSON(result).ok) {
                   //alert("更新成功");
                }
                else {
                    alert("更新失败");
                }
                count();
                setPager();  
                binddata();
            });
   
        }
        function RemoveCheckPhotoLot()
        {
            if(!confirm("确定删除本页所有存档?"))
            {
                return;
            }
            settype($("#selectphoto").val());
             var idlist="";
             var imglist = "";
            $("input[name='checkbox']").each(function(){    
                var tvalues = $(this).val();
                idlist += tvalues+"&";   
                 imglist += $("#img" + tvalues).val() + "&";
            });
            idlist =idlist.substring(0,idlist.length-1);
            imglist = imglist.substring(0,imglist.length-1);
            var adminid =  "<%=ViewData["me"].ToString() %>";
            AdminProvider.RemoveCheckPhotos(idlist,imglist,function(result) {
                if ($.parseJSON(result).ok) {
                   //alert("更新成功");
                }
                else {
                    alert("更新失败");
                }
                count();
                setPager();  
                binddata();
            });
        }


        var pagesize = 5;
        var mypagecount = 1;
        var pageno =1;
        var type = 0;
        function settype(_type)
        {
            if(type == _type)
                return;
            else{
                pageno = 1;
                type = _type;
            }
        }
        function count()
        {    
            var _pagecount=0;
            $.ajaxSetup({  async : false  });  
            AdminProvider.GetCheckPhotocount(type,function(result) {
               var allcount = $.parseJSON(result).toString();
               _pagecount = parseInt((parseInt(allcount) + parseInt(pagesize) -1)/parseInt(pagesize));
            });
            mypagecount = _pagecount;
            if(pageno > mypagecount)
                pageno = mypagecount;   
            $.ajaxSetup({ async : true }); 
        }

        PageClick = function (pageclickednumber) {
                    if(pageclickednumber >mypagecount)
                        pageno = mypagecount;
                    else
                        pageno = pageclickednumber;
                    $("#pager").pager({ pagenumber: pageclickednumber, pagecount: mypagecount, buttonClickCallback: PageClick });
                    binddata();
                }
        function setPager()
        {
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: PageClick });
        }


    </script>
</asp:Content>
