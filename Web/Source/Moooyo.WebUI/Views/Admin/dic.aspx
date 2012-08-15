<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
字典项目管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Admin_TopNav"><% Html.RenderPartial("~/Views/Admin/AdminTopNav.ascx");%> </div>
<div class="Admin_Dic_LeftNav">
    <ul  class="leftlist magT10">
    <li><a href="/admin/dic/iwants">我想</a></li>
    <li><a href="/admin/dic/interview">小编访问</a></li>
    <%--<li><a href="/admin/dic/hi">招呼</a></li>--%>
    <%--<li><a href="/admin/dic/marks">标签</a></li>--%>
    <%--<li><a href="/admin/dic/skills">才艺</a></li>--%>
    <%--<li><a href="/admin/dic/bbm">帮帮忙</a></li>--%>
    <li><a href="/admin/dic/InterestClass">兴趣分类</a></li>
    <li><a href="/admin/dic/FilterWord">过滤字符</a></li>
    <li><a href="/admin/dic/application">系统应用</a></li>
    <li><a href="/admin/dic/MemberSkin">用户皮肤</a></li>
    </ul>
</div>

<div class="content magT10">
<div class="contenttit">
<% if (ViewData["t"].ToString() == "iwants")
   {%>
   我想：<input id="iwantstr" />
   约会时发送<input id="iwantcontent" /> 
   类别：
   <select id="type" name="type" tabindex="10">
        <option value="0" selected="1">一般</option>
        <option value="11">一般（男）</option>
        <option value="12">幽默（男）</option>
        <option value="13">搞怪（男）</option>
        <option value="14">赞美（男）</option>
        <option value="15">耍酷（男）</option>
        <option value="21">一般（女）</option>
        <option value="22">幽默（女）</option>
        <option value="23">搞怪（女）</option>
        <option value="24">赞美（女）</option>
        <option value="25">耍酷（女）</option>
   </select>
   <input value="添加" type="button" onclick="saveIWant()" />
<% } %>

<%if (ViewData["t"].ToString() == "FilterWord")
  {
     %>
    
     添加关键字：
    <select id="wordtype" name="wordtype">
        <option value="<%=Convert.ToInt32(CBB.CheckHelper.FilterWord.word_type.delete).ToString() %>">删除</option>
        <option value="<%=Convert.ToInt32(CBB.CheckHelper.FilterWord.word_type.shift).ToString() %>">替换</option>
        <option value="<%=Convert.ToInt32(CBB.CheckHelper.FilterWord.word_type.trial).ToString() %>">审核</option>
    </select>
    <input type="text" id="wordname" name="wordname" size="20" />
    是否可用：
     <select id="word_is_enabel" name="word_is_enabel">
        <option value="true">可用</option>
        <option value="false">不可用</option>
    </select>
    <input type="button" value="添加" onclick="saveFilterWord()" />
    <br /><br />    
    <form id="uploadWordform" name="uploadWordform" action="/SystemFunc/UploadFilterWords" method="post" enctype="multipart/form-data" />
        上传类型：
        <select id="selectwordtype" name="selectwordtype" onchange="selectword()">
            <option value="<%=Convert.ToInt32(CBB.CheckHelper.FilterWord.word_type.delete).ToString() %>">删除</option>
            <option value="<%=Convert.ToInt32(CBB.CheckHelper.FilterWord.word_type.shift).ToString() %>">替换</option>
            <option value="<%=Convert.ToInt32(CBB.CheckHelper.FilterWord.word_type.trial).ToString() %>">审核</option>
        </select>&nbsp;&nbsp;&nbsp;&nbsp;选择文件:
        <input type="file" value="" name="upwordfile" id="upwordfile" />
        <input type="button" value="上传" onclick="uploadWordfile()" />
    </form>
    <br /> 
     查看类型:
    <select id="selectword" name="selectword" onchange="selectword()">
        <option value="<%=Convert.ToInt32(CBB.CheckHelper.FilterWord.word_type.delete).ToString() %>">删除</option>
        <option value="<%=Convert.ToInt32(CBB.CheckHelper.FilterWord.word_type.shift).ToString() %>">替换</option>
        <option value="<%=Convert.ToInt32(CBB.CheckHelper.FilterWord.word_type.trial).ToString() %>">审核</option>
    </select>&nbsp;&nbsp;&nbsp;&nbsp;
    操作所选关键字：<input type="button" value="禁用" onclick="disable_word()" />
    <input type="button" value="启用" onclick="enable_word()" />
    <input type="button" value="删除" onclick="delete_word()" /> 
<%} %>

<% if (ViewData["t"].ToString() == "interview")
   {%>
   小编问题：<input id="question" /> 答案说明：<input id="answer" />
   <input value="添加" type="button" onclick="saveinterview()" />
<% } %>

<% if (ViewData["t"].ToString() == "hi")
   {%>
   类别：
   <select id="type" name="type" tabindex="10">
        <option value="11">一般（男）</option>
        <option value="12">幽默（男）</option>
        <option value="13">搞怪（男）</option>
        <option value="14">赞美（男）</option>
        <option value="15">耍酷（男）</option>
        <option value="21">一般（女）</option>
        <option value="22">幽默（女）</option>
        <option value="23">搞怪（女）</option>
        <option value="24">赞美（女）</option>
        <option value="25">耍酷（女）</option>
  </select> 
   招呼：<input id="comment" /> 
   <input value="添加" type="button" onclick="savehi()" />
<% } %>
<% if (ViewData["t"].ToString() == "marks")
   {%>
   类别：
   <select id="sex" name="sex" tabindex="10">
        <option value="1" selected="1">男</option>
        <option value="2">女</option>
        </select>
   标签：<input id="content" /> 发送内容：<input id="contentsend" /> 封面照内容：<input id="contentcove" /> 
   <input value="添加" type="button" onclick="savemark()" />
<% } %>
<% if (ViewData["t"].ToString() == "skills")
   {%>
   类别：
   <select id="type" name="type" tabindex="10">
        <option value="另类" selected="1">另类</option>
        <option value="实用类">实用类</option>
        <option value="体育类">体育类</option>
        <option value="才艺类">才艺类</option>
        <option value="语言类">语言类</option>
        <option value="信息类">信息类</option>
        <option value="专业类">专业类</option>
        </select>
   才艺：<input id="skillname" /> 发送内容：<input id="contentsend" />
   <input value="添加" type="button" onclick="saveskill()" />
<% } %>
<%--<% if (ViewData["t"].ToString() == "bbm")
   {%>
  <form name="frmBBM" id="frmBBM" method="post" action="/SystemFunc/SaveBBMIcon" enctype="multipart/form-data">
   <input type="hidden" id="iconpath" value="" name="iconpath" />
   帮帮忙标签：<input id="bbmcontent" name="bbmcontent" type="text" />
   帮帮忙内容：<input id="bbmcontentsend" name="bbmcontentsend" type="text" />
   帮帮忙图标：<input id="iconpathFile" name="iconpathFile" type="file" /><input value="保存图标" type="button" onclick="saveBBMIcon()" />
   <input value="添加" type="button" onclick="addBBM()" />
  </form>
<% } %>--%>
<% if (ViewData["t"].ToString() == "InterestClass")
   {%>
   <form name="frmInterestClass" id="frmInterestClass" method="post" action="/SystemFunc/SaveInterestClassIcon" enctype="multipart/form-data">
    <input type="hidden" id="hidnInterestClassIcon" name="hidnInterestClassIcon" value="" />
    分类标题：<input id="txtInterestTitle" name="bbmcontent" type="text" />
    分类图标：<input id="fileInterestClassIcon" name="fileInterestClassIcon" type="file" /><input value="保存图标" type="button" onclick="saveInterestClassIcon()" />
    分类排序：<input id="txtInterestOrder" name="txtInterestOrder" type="text" size="8" />
    <input value="添加" type="button" onclick="addInterestClass()" />
   </form>
<% } %>
<%if (ViewData["t"].ToString() == "application")
  { %>

    <%Html.BeginForm("dic/application", "Admin", FormMethod.Post, new { enctype = "multipart/form-data" });%>
    图片：<input type="file" name="File1" size="30" />
    <input type="submit" value="上传" /><span id="pzimgupmes"><%=ViewData["imgpath"] != null ? ViewData["imgpath"].ToString() : ""%></span>
    <%Html.EndForm(); %>
    <form name="applicationClass" id="applicationClass" method="post" action="/SystemFunc/AddApplication" enctype="multipart/form-data">
    <input type="hidden" id="applicationimgpath" name="applicationimgpath" value="<%=ViewData["imgpath"]!=null?ViewData["imgpath"].ToString():""%>"/>
    描述：<input type="text" id="applicationdes" name="applicationdes" />
    链接地址：<input type="text" id="linkrul" name="linkurl" />模式：/***/***
    <input type="button" value="添加" onclick="addApplication()" />
    </form>
<%} %>
<%if (ViewData["t"].ToString() == "MemberSkin")
  { %>
    <form action="/SystemFunc/AddMemberSkin" method="post" enctype="multipart/form-data" id="frmMemberSkin" name="frmMemberSkin" style="float:left;"> 
    <input type="hidden" name="kidneyPicture" id="kidneyPicture" value="" />
    <input type="hidden" name="kidneyBgPicture" id="kidneyBgPicture" value="" />
    个性图片：<input type="file" name="personalityPicture" id="personalityPicture" size="30" />&nbsp;&nbsp;
    个性背景图片：<input type="file" name="personalityBackgroundPicture" id="personalityBackgroundPicture" size="30" />&nbsp;&nbsp;<input value="添加" type="button" onclick="addMemberSkin()" />
    </form> <font color="#ff0000" style="float:left; margin-left:10px;">个性图片应为：800×200像素</font>
<%} %>
</div>
<ul class="iwantlist magT10" id="listcontiner"></ul>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/scripts/jquery.form.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/admin.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

uploadpath = '<%=ViewData["uploadpath"] %>';
$(document).ready(function(){
    var category = "<%=ViewData["t"].ToString() %>";
    switch(category.toLowerCase()) {
        case "iwants": bindIWant(); break;
        case "interview": bindinterview(); break;
        case "hi": bindhi(); break;
        case "marks": bindmark(); break;
        case "skills": bindskill(); break;
//        case "bbm": bindBBM(); break;
        case "interestclass": bindInterestClass(); break;
        case "filterword":bindFilterWord(1); break;
        case "application":bindapplication();break;
        case "memberskin":bindMemberSkin();break;
    }
});

function bindFilterWord(type)
{
    AdminProvider.GetAllSystemFilterWord(type,function(result){
        var objs = $.parseJSON(result);
        var str = "<li>";
        $.each(objs,function(i){
            
            str +="<input type='checkbox' name='checkbox' value='" + objs[i].ID + "' id='check" + i + "'/>";
           if(!objs[i].Is_enable){
             
             str +="<label for='check" + i+ "' class='filterword'>" + objs[i].Word+ "</label>";            
           }
           else
           {
             str +="<label style='clolr:#000000' for='check" + i+ "'>" + objs[i].Word+ "</label>";     
           }
           
        })
        str+="</li>";
        $("#listcontiner").html(str);
    });
    $("#selectword").attr("value",type);
}

function selectword()
{
    var type =  $("#selectword").val();
    bindFilterWord(type);
}

function bindIWant() {
    AdminProvider.GetAllSystemWants(function(result) {
       var objs = $.parseJSON(result);
       var str = "";
       $.each(objs,function(i) {   
           //str += objs[i].ID;                
           str += "<li class='iwantlistart'><span  class='markicon'>" + objs[i].IWantStr + "</span>"; 
           str +="<span>"+ objs[i].Content + "</span>";
           str +="<span>类别："+ objs[i].type + "</span>";
           str +="<span>已使用<label>["+ objs[i].UseCount + "]</label>次</span>";
           //str += objs[i].IsAudited;
           str += "<a onclick='deliwant(\""+objs[i].ID+"\")'>删除</a></li>";
        });
        $("#listcontiner").html(str);
     });
}
function bindinterview() {
    AdminProvider.GetAllSystemInterView(function(result) {
            var objs = $.parseJSON(result);
            var str = "";
            $.each(objs,function(i) {
                //str += objs[i].ID;
                str += "<li><span>小编访问："+ objs[i].Question + "</span><br/>";                
                str +="<span>答案说明："+  objs[i].Answer + "</span>";
                str +="<span>已使用<label>["+  objs[i].UseCount + "]</label>次</span>";
                //str += objs[i].IsAudited;
                str += "<a onclick='delinterview(\""+objs[i].ID+"\")' href='#'>删除</a></li>";
            });
            $("#listcontiner").html(str);
        });
}
function bindhi() {
    AdminProvider.GetAllSystemHi(function(result) {
            var objs = $.parseJSON(result);
            var str = "";
            $.each(objs,function(i) {
                //str += objs[i].ID;
                str += "<li><span>类别："+ objs[i].type + "</span>";
                str += "<span class='markicon'>"+ objs[i].Comment + "</span>";
                str +="<span>已使用<label>["+ objs[i].UseCount + "]</label>次</span>";
                //str += objs[i].IsAudited;
                str += "<a onclick='delhi(\""+objs[i].ID+"\")'>删除</a></li>";
            });
            $("#listcontiner").html(str);
        });
}
function bindmark() {
    AdminProvider.GetAllSystemMarks(function(result) {
            var objs = $.parseJSON(result);
            var str = "";
            $.each(objs,function(i){
                //str += objs[i].ID;
                str +="<li><span>性别："+  objs[i].Sex + "</span>";
                str += "<span class='markicon'>" + objs[i].Content + "</span>";
                str +="<span>"+ objs[i].ContentSend + "</span>";
                str +="<span>"+ objs[i].ContentCove + "</span>";
                str +="<span>已使用<label>["+ objs[i].UseCount + "]</label>次</span>";
                //str += objs[i].IsAudited;
                str += "<a onclick='delmark(\""+objs[i].ID+"\")' href='#'>删除</a></li>";
            });
            $("#listcontiner").html(str);
        });
}
function bindskill() {
    AdminProvider.GetAllSystemSkills(function(result) {
            var objs = $.parseJSON(result);
            var str = "";
            $.each(objs,function(i){
                //str += objs[i].ID;
                str += "<li><span>类别："+ objs[i].Type + "</span>";
                str += "<span class='markicon'>" +  objs[i].SkillName + "</span>";
                str +="<span>发送内容："+  objs[i].ContentSend + "</span>";
                str +="<span>已使用<label>["+  objs[i].UseCount + "]</label>次</span>";
                //str += objs[i].IsAudited;
                str += "<a onclick='delskill(\""+objs[i].ID+"\")' href='#'>删除</a>";
            });
            $("#listcontiner").html(str);
        });
}
//function bindBBM() {
//    AdminProvider.GetAllSystemBBMs(function(result) {
//        var objs = $.parseJSON(result);
//        var str = "";
//        $.each(objs, function(i){
//            str += "<li class='iwantlistart'><span class='markicon'>标签：" + objs[i].Content + "</span>";
//            str += "<span>内容：" + objs[i].ContentSend + "</span>";
//            str += "<span>已使用<label>[" + objs[i].UseCount + "]</label>次</span>";
//            str += "<a onclick='delBBM(\"" + objs[i].ID + "\")' href='javascript:void(0);'>删除</a></li>";
//        });
//        $("#listcontiner").html(str);
//    });
//}
function bindInterestClass() {
    AdminProvider.GetAllInterestClass(function(result) {
        var objs = $.parseJSON(result);
        var str = "";
        $.each(objs, function(i) {
            var dtCreatedTime = paserJsonDate(objs[i].CreatedTime).toLocaleString();
            str += "<li class='iwantlistart'><span class='markicon'>分类标题：" + objs[i].Title + "</span>";
            str += "<span>创建时间：" + dtCreatedTime + "</span>";
            str += "<span>分类排序：" + objs[i].Order + "</span>";
            str += "<span>兴趣数量：" + objs[i].InterestCount + "</span>";
            str += "<a onclick='delInterestClass(\"" + objs[i].ID + "\")' href='javascript:void(0);'>删除</a></li>";
        });
        $("#listcontiner").html(str);
    });
}
function bindMemberSkin() {
    AdminDictionaryDataProvider.getMemberSkins(80, 1, function(result) {
        var objs = $.parseJSON(result);
        var str = "";
        var userType = "";
        var dtCreatedTime = null;
        $.each(objs, function(i) {
            dtCreatedTime = paserJsonDate(objs[i].CreatedTime).format('yyyy-mm-dd HH:MM');
            switch(objs[i].UserType) {
                case 0: userType = "会员"; 
                    break;
                case 1: userType = "管理员"; 
                    break;
            }

            str += "<li class='iwantlistart'><span class='markicon'>创建者：" + objs[i].Creater + "</span>";
            str += "<span>创建者类别：" + userType + "</span>";
            str += "<span>个性图片：<a href=\"/photo/get/" + objs[i].PersonalityPicture + "\" target=\"_blank\" title=\"点击查看个性图片\">" + objs[i].PersonalityPicture + "</a></span>";
            str += "<span>个性背景图片：<a href=\"/photo/get/" + objs[i].PersonalityBackgroundPicture + "\" target=\"_blank\" title=\"点击查看个性背景图片\">" + objs[i].PersonalityBackgroundPicture + "</a></span>";
            str += "<span>创建时间：" + dtCreatedTime + "</span>";
            str += "<a onclick='deleteMemberSkin(\"" + objs[i].ID + "\")' href='javascript:void(0);'>删除</a></li>";
        });
        $("#listcontiner").html(str);
    });
}
function deleteMemberSkin(id) {
    AdminDictionaryDataProvider.deleteMemberSkin(id, function(result) {
        if ($.parseJSON(result).ok) {
            alert("成功删除用户皮肤！");
            bindMemberSkin();
        }
        else {
            alert("用户皮肤删除失败！");
        }
    });
}
function deliwant(id) {
    AdminProvider.DelSystemWant(id,function(result) {
        if ($.parseJSON(result).ok) {
            bindIWant();
        }
        else {
            alert("删除失败");
        }
    });
}
//function delBBM(id) {
//    AdminProvider.DelSystemBBM(id, function(result) {
//        if($.parseJSON(result).ok) { bindBBM(); }
//        else { alert("删除失败"); }
//    });
//}
function delinterview(id) {
    AdminProvider.DelSystemInterView(id,function(result) {
        if ($.parseJSON(result).ok) {
            bindinterview();
        }
        else {
            alert("删除失败");
        }
    });
}
function delhi(id) {
    AdminProvider.DelSystemHi(id,function(result) {
        if ($.parseJSON(result).ok) {
            bindhi();
        }
        else {
            alert("删除失败");
        }
    });
}
function delmark(id) {
    AdminProvider.DelSystemMark(id,function(result) {
        if ($.parseJSON(result).ok) {
            bindmark();
        }
        else {
            alert("删除失败");
        }
    });
}
function delskill(id) {
    AdminProvider.DelSystemSkill(id,function(result) {
        if ($.parseJSON(result).ok) {
            bindskill();
        }
        else {
            alert("删除失败");
        }
    });
}
function delInterestClass(id) {
    AdminProvider.DelInterestClass(id,function(result) {
        if ($.parseJSON(result).ok) {
            bindInterestClass();
            alert("兴趣分类删除成功");
        }
        else {
            alert("兴趣分类删除失败");
        }
    });
}
function saveIWant() {
    var iwantstr = $("#iwantstr").val();
    var iwantcontent = $("#iwantcontent").val();
    var type = $("#type").val();
    if (trim(iwantstr) == "") { $("#iwantstr").focus(); return; }
    if (trim(iwantcontent) == "") { $("#iwantcontent").focus(); return; }
    var isaudited=true;
    var writter="";
    
    AdminProvider.AddSystemWant(type,isaudited,writter,iwantstr,iwantcontent,function(result) {
        if ($.parseJSON(result).ok) {
            $("#iwantstr").val("");
            $("#iwantcontent").val("");
            bindIWant();
        }
        else {
            alert("新增失败");
        }
    });
}
function saveFilterWord()
{
    var wordtype = $("#wordtype").val();
    var wordname = $("#wordname").val();
    var word_is_enable = $("#word_is_enabel").val();
    if (trim(wordname) == "") { $("#wordname").focus(); return; }
    AdminProvider.AddSystemFilterWord(wordname,word_is_enable,wordtype,function(result){
       if($.parseJSON(result).ok)
       {
        //alert("添加成功");
        $("#wordname").val("");
        bindFilterWord(wordtype);
       } 
       else
       {
        alert("添加失败");
       }
    });
}
function disable_word()
{
    var idlist="";
    $("input[name='checkbox']:checked").each(function(){    
        idlist += $(this).val()+"&";   
    });
    idlist =idlist.substring(0,idlist.length-1);
    if(idlist.length == 0 )
    {
        alert("请至少选择一个关键字！");
        return;
    }
    AdminProvider.DisableFilterWord(idlist,function(result){
       if($.parseJSON(result).ok)
       {
        //alert("更新成功");
        bindFilterWord($("#selectword").val());
       } 
       else
       {
        alert("更新失败");
       }
    });
}
function delete_word()
{
    if(!confirm("确定删除？"))
    {
        return;
    }
    var idlist="";
    $("input[name='checkbox']:checked").each(function(){    
        idlist += $(this).val()+"&";   
    });
    idlist =idlist.substring(0,idlist.length-1);
    if(idlist.length == 0 )
    {
        alert("请至少选择一个关键字！");
        return;
    }
    AdminProvider.DeleteFilterWord(idlist,function(result){
       if($.parseJSON(result).ok)
       {
        //alert("更新成功");
        bindFilterWord($("#selectword").val());
       } 
       else
       {
        alert("更新失败");
       }
    });
}
function enable_word()
{
     var idlist="";
    $("input[name='checkbox']:checked").each(function(){    
        idlist += $(this).val()+"&";   
    });
    idlist =idlist.substring(0,idlist.length-1);
    if(idlist.length == 0 )
    {
        alert("请至少选择一个关键字！");
        return;
    }
    AdminProvider.EnableFilterWord(idlist,function(result){
       if($.parseJSON(result).ok)
       {
        alert("更新成功");
        bindFilterWord($("#selectword").val());
       } 
       else
       {
        alert("更新失败");
       }
    }); 
}

function uploadWordfile()
{
    file=$("#upwordfile").val();
    var pos = file.lastIndexOf(".");
    var lastName = file.substring(pos, file.length);
    if($("#upwordfile").val() == "") {
        alert("请选择文件！");
        return;
    }
    if((lastName.toLowerCase() != ".txt")) {
        $("#upwordfile").focus();
        alert("请上传一个文本文档");
        return;
    }   

    $('#uploadWordform').submit();
}

function saveinterview() {
    var question = $("#question").val();
    var answer = $("#answer").val();
    if (trim(question) == "") { $("#question").focus(); return; }
    //if (trim(answer) == "") { $("#answer").focus(); return; }
    var isaudited=true;
    var writter="";
    var type=11;
    AdminProvider.AddSystemInterView(type,isaudited,writter,question,answer,function(result) {
        if ($.parseJSON(result).ok) {
            $("#question").val("");
            //$("#answer").val("");
            bindinterview();
        }
        else {
            alert("新增失败");
        }
    });
}
function savehi() {
    var comment = $("#comment").val();
    if (trim(comment) == "") { $("#comment").focus(); return; }
    var isaudited=true;
    var writter="";
    var type=$("#type").val();
    AdminProvider.AddSystemHi(type,isaudited,writter,comment,function(result){
        if ($.parseJSON(result).ok) {
            $("#comment").val("");
            bindhi();
        }
        else {
            alert("新增失败");
        }
    });
}
function savemark() {
    var content = $("#content").val();
    if (trim(content) == "") { $("#content").focus(); return; }
    var contentsend = $("#contentsend").val();
    if (trim(contentsend) == "") { $("#contentsend").focus(); return; }
    var contentcove = $("#contentcove").val();
    if (trim(contentcove) == "") { $("#contentcove").focus(); return; }
    var isaudited=true;
    var writter="";
    var sex=$("#sex").val();
    AdminProvider.AddSystemMark(sex,isaudited,writter,content,contentsend,contentcove,function(result) {
        if ($.parseJSON(result).ok) {
            $("#content").val("");
            $("#contentsend").val("");
            $("#contentcove").val("");
            bindmark();
        }
        else {
            alert("新增失败");
        }
    });
}
function saveskill() {
    var skillname = $("#skillname").val();
    if (trim(skillname) == "") { $("#skillname").focus(); return; }
    var contentsend = $("#contentsend").val();
    //if (trim(contentsend) == "") { $("#contentsend").focus(); return; }
    var isaudited=true;
    var writter="";
    var type=$("#type").val();
    AdminProvider.AddSystemSkill(type,isaudited,writter,skillname,contentsend,function(result) {
        if ($.parseJSON(result).ok) {
            $("#skillname").val("");
            //$("#contentsend").val("");
            bindskill();
        }
        else {
            alert("新增失败");
        }
    });
}
//function saveBBMIcon() {
//    file=$("#iconpathFile").val();
//    var pos = file.lastIndexOf(".");
//    var lastName = file.substring(pos, file.length);
//    if($("#iconpathFile").val() == "") {
//        alert("您必须先上传图标");
//        return;
//    }
//    if((lastName.toLowerCase() != ".jpg") && (lastName.toLowerCase() != ".gif") && (lastName.toLowerCase() != ".png") && (lastName.toLowerCase() != ".bmp")) {
//        $("#iconpathFile").focus();
//        alert("你上传的文件必须为图片类型");
//    }
//    else {
//        $('#frmBBM').ajaxSubmit(function (data) {
//            if((data != null) || (data != "")) {
//                $("#iconpath").val(data);
//                alert("图标保存成功");
//            }
//            else { 
//                $("#iconpathFile").focus();
//                alert("图标保存失败");
//            }
//        });
//    }
//}
//function addBBM() {
//    var content = $("#bbmcontent").val();
//    if(trim(content) == "") { 
//        $("#bbmcontent").focus(); 
//        alert("标签名不能为空");
//        return; 
//    }
//    var contentsend = $("#bbmcontentsend").val();
//    var iconpath = $("#iconpath").val();
//    AdminProvider.AddSystemBBM(content, contentsend, iconpath, function (result) {
//        if($.parseJSON(result).ok) {
//            $("#bbmcontent").val("");
//            $("#bbmcontentsend").val("");
//            bindBBM();
//            alert("帮帮忙新增成功");
//        }
//        else { alert("帮帮忙新增失败"); }
//    });
//}
function addMemberSkin() {
    var ppFile = $("#personalityPicture").val();
    var pbpFile = $("#personalityBackgroundPicture").val();

    if(ppFile == "" && pbpFile == "") {
        alert("请上传个性图片，或者个性背景图片！");
        return; 
    }
    if(ppFile != "")
    {
        $("#kidneyPicture").val("yes");
        var ppPosition = ppFile.lastIndexOf(".");
        var ppLastName = ppFile.substring(ppPosition, ppFile.length);
        if((ppLastName.toLowerCase() != ".jpg") && (ppLastName.toLowerCase() != ".gif") && (ppLastName.toLowerCase() != ".png")) {
            alert("你上传的个性图片，必须为jpg/gif/png类型！");
            return;
        }
    }
    if(pbpFile != "")
    {
        $("#kidneyBgPicture").val("yes");
        var pbpPosition = pbpFile.lastIndexOf(".");
        var pbpLastName = pbpFile.substring(pbpPosition, pbpFile.length);
        if((pbpLastName.toLowerCase() != ".jpg") && (pbpLastName.toLowerCase() != ".gif") && (pbpLastName.toLowerCase() != ".png")) {
            alert("你上传的个性背景图片，必须为jpg/gif/png类型！");
            return;
        }
    }

    $('#frmMemberSkin').ajaxSubmit(function (data) {
        var obj = $.parseJSON(data);
        if(obj.ok) {
            alert("成功添加用户皮肤");
            bindMemberSkin();
        }
        else { 
            $("#fileInterestClassIcon").focus();
            alert("用户皮肤添加失败");
        }
    });
}
function addInterestClass() {
    var title = $("#txtInterestTitle").val();
    if(trim(title) == "") { 
        $("#txtInterestTitle").focus(); 
        alert("分类标题不能为空");
        return; 
    }
    var icon = $("#hidnInterestClassIcon").val();
    var order = $("#txtInterestOrder").val();
    if((isNaN(order)) && (order != "")) {
        $("#txtInterestOrder").focus(); 
        alert("兴趣分类排序应输入数字");
        return;
    }
    if(order == "") { order = 0; }
    AdminProvider.AddInterestClass(title, icon, order, function (result) {
        if($.parseJSON(result).ok) {
            $("#txtInterestTitle").val("");
            $("#txtInterestOrder").val("");
            bindInterestClass();
            alert("兴趣分类新增成功");
        }
        else { alert("兴趣分类新增失败"); }
    });
}
function saveInterestClassIcon() {
    file=$("#fileInterestClassIcon").val();
    var pos = file.lastIndexOf(".");
    var lastName = file.substring(pos, file.length);
    if($("#fileInterestClassIcon").val() == "") {
        alert("您必须先上传图标");
        return;
    }
    if((lastName.toLowerCase() != ".jpg") && (lastName.toLowerCase() != ".gif") && (lastName.toLowerCase() != ".png") && (lastName.toLowerCase() != ".bmp")) {
        $("#fileInterestClassIcon").focus();
        alert("你上传的文件必须为图片类型");
    }
    else {
        $('#frmInterestClass').ajaxSubmit(function (data) {
            if((data != null) && (data != "")) {
                $("#hidnInterestClassIcon").val(data);
                alert("兴趣类型图标保存成功");
            }
            else { 
                $("#fileInterestClassIcon").focus();
                alert("兴趣类型图标保存失败");
            }
        });
    }
}
function bindapplication(){
    AdminProvider.getapplication(function(result){
        var objs=$.parseJSON(result);
        var str="";
        $.each(objs,function(i){
           str += "<li class='iwantlistart'><span><img src=\"" + photofunctions.getnormalphotoname(objs[i].ImagePath1) + "\"/></span>"; 
           str +="<span>"+ objs[i].Description1 + "</span>";
           str +="<span>"+ objs[i].Url + "</span>";
           str += "<a onclick=\"delapplication('"+objs[i].ID+"','"+objs[i].ImagePath1+"')\">删除</a></li>";
        });
        $("#listcontiner").html(str);
    });
}
function addApplication(){
    var imgpath=$("#applicationimgpath").val();
    var description=$("#applicationdes").val();
    var linkrul=$("#linkrul").val();
    if(imgpath!=""){
    AdminProvider.addapplication(imgpath,description,linkrul,function(result){
        var obj=$.parseJSON(result);
        if(obj.ok){
            $("#pzimgupmes").html("");
            $("#applicationimgpath").val("");
            $("#applicationdes").val("");
            bindapplication();
            }
        else
            alert("添加失败！");
    });
    }
    else
        alert("请先上传图片！");
}
function delapplication(id,imgname){
    AdminProvider.delapplication(id,imgname,function(result){
        var obj=$.parseJSON(result);
        if(obj.ok)
            bindapplication();
        else
            alert("删除失败！");
    });
}
</script>
</asp:Content>
