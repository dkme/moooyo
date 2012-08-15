<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
审核管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Admin_TopNav"><% Html.RenderPartial("~/Views/Admin/AdminTopNav.ascx");%> </div>
<div class="Admin_Dic_LeftNav">
    <ul  class="leftlist magT10">
    <li><a href="/admin/verify/verifytext">待审内容</a></li>
    <li><a href="/admin/checkphoto">视频认证</a></li>
    </ul>
</div>

<div class="content magT10">
<div class="contenttit">
<% if (ViewData["t"].ToString() == "verifytext")
{%>
    查看：
    <select id="selectFilterWord" name="selectFilterWord" onchange="Rebind()">
        <option value="<%= Convert.ToInt32(Moooyo.BiZ.FilterText.VerifyStatus.waitaudit)%>">等待审核</option>
        <option value="<%= Convert.ToInt32(Moooyo.BiZ.FilterText.VerifyStatus.audidel)%>">已被删除</option>
        <option value="<%= Convert.ToInt32(Moooyo.BiZ.FilterText.VerifyStatus.audimod)%>">已被修改</option>
        <option value="<%= Convert.ToInt32(Moooyo.BiZ.FilterText.VerifyStatus.auditpass)%>">已经通过</option>
    </select>
    批量处理:
    <input id="btnFilterWordDel" name="btnFilterWordDel" type="button" value="删除" onclick="updateFilterTextlot('<%= Convert.ToInt32(Moooyo.BiZ.FilterText.VerifyStatus.audidel)%>')" />
    <input id="btnFilterWordUpdate" name="btnFilterWordDel" type="button" value="修改" onclick="updateFilterTextlot('<%= Convert.ToInt32(Moooyo.BiZ.FilterText.VerifyStatus.audimod)%>')" />
    <input id="btnFilterWordPass" name="btnFilterWordDel" type="button" value="审核通过" onclick="updateFilterTextlot('<%= Convert.ToInt32(Moooyo.BiZ.FilterText.VerifyStatus.auditpass)%>')" />
    <input type="button" value="删除存档" onclick="removeFilterTextlot()" />
<% } %>
</div>
<ul class="verify" id="listcontiner"></ul>
<div id="pager" class="verifyPager" style="width:850px; margin:0px;"></div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/scripts/jquery.form.js" type="text/javascript"></script>
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/admin.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

$(document).ready(function(){
    var category = "<%=ViewData["t"].ToString() %>";
    var verifytype = <%= Convert.ToInt32(Moooyo.BiZ.FilterText.VerifyStatus.waitaudit)%>;
    switch(category.toLowerCase()) {
        case "verifytext":             
            settype(verifytype);
            count();
            setPager();
            bindverifytext(); 
            break;
    }
});

function bindverifytext()
{
    AdminProvider.GetAllVerifyText(type,pageno,pagesize, function(result){
        var objs = $.parseJSON(result);
        var str = "";
        $.each(objs,function(i){
            str +="<li><input type='checkbox' name='checkbox' value='" + objs[i].ID + "' id='check" + i + "'/>&nbsp;&nbsp;";
            str += "创建时间：" + paserJsonDate(objs[i].Jion_time).format('yyyy-mm-dd HH:MM') + "&nbsp;&nbsp;";
            str += "所属表：" + objs[i].Tablename + "&nbsp;&nbsp;";
            str += "所属列：" + objs[i].Colname + "&nbsp;&nbsp;";
            if( objs[i].Verify_status == <%= Convert.ToInt32(Moooyo.BiZ.FilterText.VerifyStatus.waitaudit)%>)
            {
                str +="<a onclick=\"UpdateFilterText('" +objs[i].ID +"','<%= Convert.ToInt32(Moooyo.BiZ.FilterText.VerifyStatus.audidel)%>')\">[删除]</a>&nbsp;";
                str +="<a onclick=\"UpdateFilterText('" +objs[i].ID +"','<%= Convert.ToInt32(Moooyo.BiZ.FilterText.VerifyStatus.audimod)%>')\">[修改]</a>&nbsp;";
                str +="<a onclick=\"UpdateFilterText('" +objs[i].ID +"','<%= Convert.ToInt32(Moooyo.BiZ.FilterText.VerifyStatus.auditpass)%>')\">[通过]</a>&nbsp;";    
            }
            str +="<a onclick=\"removeFilterText('" +objs[i].ID +"')\">[删除存档]</a><br/>";                       
            str +="<textarea wrap='hysical'id='textarea" + objs[i].ID + "' class='filtertext'>" + objs[i].Verify_text+ "</textarea><br/></li>";            
        });
       
        $("#listcontiner").html(str);
    });
    $("#selectFilterWord").attr("value",type);
}
function Rebind()
{
    var _type = $("#selectFilterWord").val();
    settype(_type);
    count();
    setPager();  
    if(type != <%= Convert.ToInt32(Moooyo.BiZ.FilterText.VerifyStatus.waitaudit)%>)
    {
        $("#btnFilterWordDel").attr("disabled","disabled"); 
        $("#btnFilterWordUpdate").attr("disabled","disabled"); 
        $("#btnFilterWordPass").attr("disabled","disabled"); 
    }
    else
    {
        $("#btnFilterWordDel").attr("disabled","");
        $("#btnFilterWordUpdate").attr("disabled",""); 
        $("#btnFilterWordPass").attr("disabled",""); 
    }
    bindverifytext();
}

function updateFilterTextlot(_type)
{
    //settype(_type);
     var idlist="";
     var textlist ="";
    $("input[name='checkbox']:checked").each(function(){    
        var tvalues = $(this).val();
        idlist += tvalues+"&";   
        textlist += escape($("#textarea" + tvalues).val()) + "&";
    });
    if(idlist.length<=0)
    {
        alert("至少选择一条记录！");
        return;
    }
    idlist =idlist.substring(0,idlist.length-1);
    textlist =textlist.substring(0,textlist.length-1);   
    var adminid =  "<%=ViewData["me"].ToString() %>";
    AdminProvider.UpdateFilterTexts(idlist, _type, adminid, textlist,function(result) {
        if ($.parseJSON(result).ok) {
           //alert("更新成功");
        }
        else {
            alert("更新失败");
        }
        count();
        setPager();  
        bindverifytext();
    });
}

function removeFilterTextlot()
{    
    
    var idlist ="";
    $("input[name='checkbox']:checked").each(function(){    
        var tvalues = $(this).val();
        idlist += tvalues+"&";   
    });
    if(idlist.length<=0)
    {
        alert("至少选择一条记录！");
        return;
    }
    if(!confirm("确定批量删除存档？"))
        return;
    idlist =idlist.substring(0,idlist.length-1);
    AdminProvider.RemoveFilterTexts(idlist,function(result) {
        if ($.parseJSON(result).ok) {
           //alert("更新成功");
        }
        else {
            alert("更新失败");
        }
        settype($("#selectFilterWord").val());
        count();
        setPager();  
        bindverifytext();
    });
}
function UpdateFilterText(id,_type)
{
   // settype(_type);
    var adminid =  "<%=ViewData["me"].ToString() %>";
    var text = $("#textarea" + id).val();
    AdminProvider.UpdateFilterText(id, _type, adminid, text,function(result) {
        if ($.parseJSON(result).ok) {
          // alert("更新成功");
        }
        else {
            alert("更新失败");
        }
        count();
        setPager();  
        bindverifytext();
    });
}
function removeFilterText(id)
{
    if(!confirm("确定删除存档？"))
        return;
    AdminProvider.RemoveFilterText(id,function(result) {
        if ($.parseJSON(result).ok) {
           //alert("更新成功");
        }
        else {
            alert("更新失败");
        }
        settype($("#selectFilterWord").val());
        count();
        setPager();
        bindverifytext();
    });
     
}

var pagesize = 5;
var mypagecount = 1;
var pageno =1;
var type = 0;
function settype(_type)
{
    if(type == _type)
    {
        return;
    }
    else{
        pageno = 1;
        type = _type;
    }
}
function count()
{    
    var _pagecount=0;
    $.ajaxSetup({  
            async : false  
    });  
    AdminProvider.GetFilterTextcont(type,function(result) {
       var allcount = $.parseJSON(result).toString();
       _pagecount = parseInt((parseInt(allcount) + parseInt(pagesize) -1)/parseInt(pagesize));
    });
    mypagecount = _pagecount;
    if(pageno > mypagecount)
    {
        pageno = mypagecount;
    }
    $.ajaxSetup({  
            async : true
    }); 
}

PageClick = function (pageclickednumber) {
            if(pageclickednumber >mypagecount)
            {
                pageno = mypagecount;
            }
            else{
                pageno = pageclickednumber;
            }
            $("#pager").pager({ pagenumber: pageclickednumber, pagecount: mypagecount, buttonClickCallback: PageClick });
            bindverifytext();
        }
function setPager()
{
    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: PageClick });
}

</script>
</asp:Content>
