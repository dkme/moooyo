<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberProfileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
[米柚] <%=Model.Member.Name %>的照片墙
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="c976">
    <!--个人左面板-->
    <% if (Model.IsOwner)
       {%>
    <% Html.RenderPartial("~/Views/Member/ProfileLeftPanel.ascx");%>  
    <% }
       else
       { %>
    <% Html.RenderPartial("~/Views/Member/MemberLeftPanel.ascx");%>  
    <% } %>
    <!--endof 个人左面板-->
    <!--个人顶部块-->
     <% if (!Model.IsOwner)
        {%>
        <% Html.RenderPartial("~/Views/Member/ProfileTopPanel.ascx"); %>
    <% }
        else
        {%>
    <%--<article class="top-title-box mt20 fl"/>--%>
    <%} %>
    <!--endof 个人顶部块-->
    <section class="inter-conbox mt32 fl">
    <h2 class="f14 fb fl">相册(<%=Model.Member.PhotoCount %>)&nbsp;</h2>
    <div class="mt2"><span class="fl ml15"><%=!Model.IsOwner?"TA":"我" %>的相册地址：&nbsp;</span><a href="javascript:;" target="_self" id="linkCopyUrl" class="fl">http://<%=HttpContext.Current.Request.Url.Host.ToString()%>/u/p/<%=Model.MemberUrl%></a><a href="javascript:;" class="fl" id="copyToClipboardBtn">&nbsp;&nbsp;复制</a>
        </div>
        <% if (!Model.IsOwner)
           {%>
    <div class="morelink h25 fr">
    <span id="moreinvert" class="moreinvert"><a onclick="Invert.photo('<%=Model.MemberID %>',$('#moreinvert'));">邀请Ta上传更多照片</a></span>
    </div>
           <% } %>
    <div class="fr left55">
    <% if (Model.IsOwner)
       {%>
    <a href="/photo/photoupload?t=0" class="btn w-60">&nbsp;上传照片&nbsp;</a>
    <%} %>
    </div>
    <div class="clearfix"></div>
    <div id="photoContent" class="photoContent">
    <div id="photoPanelContent1" class="photoPanelContent"></div>
    <div id="photoPanelContent2" class="photoPanelContent"></div>
    <div id="photoPanelContent3" class="photoPanelContent_right"></div>
    </div>
    <div class="clear"></div>
    <div class="loadingComment" id="loadingComment"></div>
    </section>
    <aside class="asidebox-r mt32 fr">
        <% Html.RenderAction("AppPush", "Push");%> 
        <% Html.RenderAction("TheyLike", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
        <% Html.RenderAction("SameInterestingMemberOverMe", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
    </aside>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
var photoPanelContents = new Array();
var nowpage = 1;
var pagesize = 20;
var hght=0;//初始化滚动条总长
var top=0;//初始化滚动条的当前位置
var scrollaction=false;
var lastfillpanel=-1;
var loadover=false;

function checkscroll(){
  if(scrollaction)
  if (( nowpage <2 & (top>parseInt(hght/2)*1) ) || ( nowpage >=2 & (top>parseInt(hght/10)*8) ))//判断滚动条当前位置是否超过总长的2/3，parseInt为取整函数
  {
     if(nowpage<5)
        appenddata();//如果是，调用show函数加载内容。
  }
}
function appenddata()
{
    nowpage++;
    getPhotos();
    hght=0;
    top=0;
}
$(document).ready(function () {

    $("#navtopphotowall").addClass("activeitem");

    $(window).scroll(function(){
        hght=$(document).height();//得到滚动条总长，赋给hght变量
        top=$(document).scrollTop();//得到滚动条当前值，赋给top变量
    });
    setInterval("checkscroll();",500);

    phototype = <% =ViewData["phototype"] %>;
    uploadpath = '<% =Model.UploadPath %>';
    mid = '<%=Model.MemberID %>';
    var isMestr = '<% =Model.IsOwner%>';
    isMe = false;
    if (isMestr=="True") isMe=true;

    photoPanelContents.push($("#photoPanelContent1"));
    photoPanelContents.push($("#photoPanelContent2"));
    photoPanelContents.push($("#photoPanelContent3"));

    memberprovider.getmemberprofile(mid,function(result){
        member=$.parseJSON(result); 
        bindMemberProfile(member);
    });

    getPhotos();

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
 });
 function getPhotos()
 {
     scrollaction=false;
     
     photoprovider.getmemberphotolist(mid,phototype,pagesize,nowpage,function(result){
        photosdata = $.parseJSON(result);
        if (result.length<pagesize) loadover = true;
        lastfillpanel = bindPhoto(photosdata,photoPanelContents,lastfillpanel);
        scrollaction=true;
    });
 }
 function bindMemberProfile(member)
 {
 }
 function delPhoto(id)
 {
    photoprovider.delphoto(id,function(result){
        $.each(photosdata,function(i){
            //判断是否已经被删除
            if (photosdata[i]==null)  { return true; }
            if (photosdata[i].ID == id)
            {
                delete photosdata[i];
            }
        });
        $.each(photoPanelContents,function(i){
            photoPanelContents[i].empty();
        });
        lastfillpanel = bindPhoto(photosdata,photoPanelContents,0);
        alert("成功删除照片");
    });
 }
 function cancelEdit(id)
 {
    var editorpanel = $("#editorpanel"+id);
    var metapanel = $("#meta"+id);
    editorpanel.hide();
    metapanel.show();
 }
 function closeAllEditPanel()
 {
    $.each($(".editorpanel"),function(){
        $(this).hide();
    });
    $.each($(".photometa"),function(){
        $(this).show();
    });
 }
 function submitEdit(id)
 {
    var titleinput = $("#photoeditinputtitle"+id);
    var title = titleinput.text();
    var contentinput = $("#photoeditinputcontent"+id);
    var content = contentinput.text();   

    photoprovider.updatephoto(id,title,content,function(i){
            
           $.each(photosdata,function(i){
                //判断是否已经被删除
                if (photosdata[i]==null)  { return true; }

                //给JSON数据对象更新
                if (photosdata[i].ID == id)
                {
                    photosdata[i].Title=title;
                    photosdata[i].Content=content;
                }
            });
            //更新页面对象
            var titlep = $("#phototitle"+id);
            var contentp = $("#photocontent"+id);
            titlep.text(title);
            contentp.text(content);
            //更新编辑状态
            var editorpanel = $("#editorpanel"+id);
            var metapanel = $("#meta"+id);
            editorpanel.hide();
            metapanel.show();
        });
 }
 function setIconPhoto(id)
 {
 var iconpath = "<%= Model.Member.ICONPath%>";
 var isreal = <%= (Model.Member.IsRealPhotoIdentification)?"true":"false" %>;
 if((iconpath != "/pics/noicon.jpg") && isreal == true)
 {
    if(!confirm("更换头像后需要重新进行视频认证，您确认要更换头像？"))
    {
        return;
    }
 }

    memberprovider.seticonphoto(id,function(result){
        var re = $.parseJSON(result);
        if (re.ok)
        {
            alert("头像设置成功");
        }
    });
 }
 function editPhoto(id)
 {
    editPhoto(id,false);
 }
 function editPhoto(id,iseditall)
 {

    if (!iseditall) {closeAllEditPanel();}

    var editorpanel = $("#editorpanel"+id);
    var metapanel = $("#meta"+id);
    var titlep = $("#phototitle"+id);

    editorpanel.empty();
    var titleinput = "<img src=\'/pics/spaceout.gif\' border=\'0\' height=\'15\' width=\'15\' class=\'f-sprite fs-icon_quan\' /> 想一个开心的标题吧：<br><textarea id=\'photoeditinputtitle"+id+"\' class=\'photoeditinput\'>"+titlep.text()+"</textarea>";
    editorpanel.append(titleinput);

    var contentp = $("#photocontent"+id);
    var contentinput = "<br><img src=\'/pics/spaceout.gif\' border=\'0\' height=\'15\' width=\'15\' class=\'f-sprite fs-icon_quan\' /> 还可以分享拍照的经历和心情呢：<br><textarea id=\'photoeditinputcontent"+id+"\' class=\'photoeditinput\'>"+contentp.text()+"</textarea>";
    editorpanel.append(contentinput);

    var submitbtn = "<br><input type=\'button\' class=\'btnDone\' value=\'写好了\' onclick=\'submitEdit(\""+id+"\")\'/> <span class=\'btnCancel\' onclick=\'cancelEdit(\""+id+"\")\'>不必了</span>";
    editorpanel.append(submitbtn);
    metapanel.hide();
    editorpanel.show();
 }
 function editAllPhoto()
 {
    $.each(photosdata,function(i){
            //判断是否已经被删除
            if (photosdata[i]==null)  { return true; }
            editPhoto(photosdata[i].ID,true);
    });
 }
 function bindPhoto(objs,photoPanelContents,lastfillpanel){
     var t=lastfillpanel;
     $.each(objs,function(i){
        //判断是否已经被删除
        if (objs[i]==null)  { return true; }
        var html = '';        
        html += '<div class=\'photoPanel\' id=\'photo'+objs[i].ID+'\'>';        
        var sfilename = photofunctions.getsmallphotoname(objs[i].FileName);
        //var sfilename = objs[i].FileName;
        html += '<div class=\'photobox\'><a href=\'/photo/show/'+objs[i].ID+'\'><img class="memberphoto" src=\''+sfilename+'\' /></a>';
        //如果是用户本人，则显示编辑标签
        if (isMe){
            html +='<div class=\'photobtnpanel\'></div><div class=\'photoeditbutton\' style=\'display:none;\'>';
            //html +='<a style=\'cursor: pointer;\' alt=\'设置为头像\' title=\'设置为头像\' onclick=\'setIconPhoto("'+objs[i].ID+'");\'>设置为头像</a>';
            //html +='<a style=\'cursor: pointer;\' alt=\'设置为头像\' title=\'设置为头像\' onclick=\"actionprovider.openCustomSmallPicture(\'/Photo/mplist/\', \'' + photofunctions.getnormalphotoname(objs[i].FileName) + '\');\">设置为头像</a>';
            //html +='<a style=\'cursor: pointer;\' alt=\'照片说明\' title=\'照片说明\' onclick=\'editPhoto("'+objs[i].ID+'");\'>照片说明</a>';
            html +='&nbsp;<a style=\'cursor: pointer;\' onclick=\'delPhoto("'+objs[i].ID+'");\'>删除照片</a>';        
            html +='</div></div>';
            //html += '<div class=\'photoeditbutton\' style=\'display:none;\'><a style=\'cursor: pointer;\' onclick=\'delPhoto("'+objs[i].ID+'");\'></a> <a style=\'cursor: pointer;\' alt=\'修改照片\' title=\'修改照片\' onclick=\'editPhoto("'+objs[i].ID+'");\'><img src="/pics/spaceout.gif" border="0" height="15" width="15" class="f-sprite fs-icon_edit" /></a> <a style=\'cursor: pointer;\' alt=\'设置为头像\' title=\'设置为头像\' onclick=\'setIconPhoto("'+objs[i].ID+'");\'><img src="/pics/spaceout.gif" border="0" height="15" width="15" class="f-sprite fs-setmembericon" /></a></div>';
        }
        html += '<div id="meta'+objs[i].ID+'" class="photometa">'; 
        html += '<p class=\'fb\' id=\'phototitle'+objs[i].ID+'\'>'+objs[i].Title+'</p>';
        html += '<p class=\'\' id=\'photocontent'+objs[i].ID+'\'>'+objs[i].Content+'</p>';
        html += '<ul>'
        if (objs[i].ViewCount != 0)
        {
            html += '<li class=\'fl\'><a href=\'/photo/show/'+objs[i].ID+'\'>'+objs[i].ViewCount+'浏览</a></li>';
        }
        if (objs[i].CommentCount != 0)
        {
            html += '<li>&nbsp;<a href=\'/photo/show/'+objs[i].ID+'\'>'+objs[i].CommentCount+'回复</a></li>';
        }
        html += '</ul>'
        html += '<div class=\'clearfix\'></div></div>';
        html += '<div id="editorpanel'+objs[i].ID+'" class="editorpanel" style="display:none;"></div>';
        html += '</div>';
        t++;
        if (t>photoPanelContents.length-1) t=0;
        {   
            lastfillpanel = t;
            photoPanelContents[t].append(html);
        }
        
        $("#photo"+objs[i].ID).fadeIn(1000);
     });
     
    if (isMe)
    {
        $(".photobox").mouseover(function(){            
            $('.photoeditbutton',this).show();
            $('.photobtnpanel',this).show();            
        });
        $(".photobox").mouseout(function(){
            $('.photoeditbutton',this).hide();
            $('.photobtnpanel',this).hide();
        });
    }
    return lastfillpanel;
 }
 
    </script>
</asp:Content>
