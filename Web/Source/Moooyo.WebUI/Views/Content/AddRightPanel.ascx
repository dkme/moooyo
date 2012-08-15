<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.AddContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<style type="text/css">
    .fb_box_com .chosepic_list li{cursor:pointer;}
    .fb_box_com .txdt a{cursor:pointer;}
    .fb_box_com .sinadt a{cursor:pointer;}
    .mt36 { height:36px;}
</style>
<%if (Model.AlreadyLogon)
  { 
      %>
<div class="box_demo1 fb_right">
    <div class="box_demo2">
	<div class="mt36"></div>
	<div class="fb_box_com " style="height:25px; overflow:visible;" title="点击选择内容保密程度">
		<ul id="main_box">
            <li class="select_box">
            <span>
                <%if (Model.contentObj != null)
                  {
                      switch (Model.contentObj.ContentPermissions)
                      {
                          case Moooyo.BiZ.Content.ContentPermissions.AllOpen:%>对所有人公开<% break;
                          case Moooyo.BiZ.Content.ContentPermissions.MyFriend:%>我关注的人可以看<% break;
                      }
                  }
                  else
                  {
                      %>对所有人公开<%
                  } %>
            </span>
            <ul class="select_list">
                <li data-select="no" data-permissionstype="0">对所有人公开</li>
                <li data-select="no" data-permissionstype="1">我关注的人可以看</li>
            </ul>
            </li>
        </ul>
	</div>
	<div class="fb_box_com m_top30"><span>发布到</span></div>
	<div class="fb_box_com chosepic" style="display:block; padding-bottom:0px;">
        <ul class="chosepic_list" style="padding-top:0px;">
        <%
                  //记录兴趣的编号
                  string ids = "";
                  //兴趣是否被选中
                  Boolean ifclick = false;
                  if (Model.interestList == null || Model.interestList.Count <= 0)
                  {
            %>你还没有添加兴趣，不能发布信息哦~~<%
                  }
                  foreach (var interest in Model.interestList)
                  {
                      if (Model.contentObj != null)
                      {
                          if (Model.contentObj.InterestID.IndexOf(interest.ID) >= 0 && Model.contentObj.ContentPermissions == Moooyo.BiZ.Content.ContentPermissions.AllOpen)
                              ifclick = true;
                          else if (Model.contentObj.InterestID.IndexOf(interest.ID) < 0 && Model.contentObj.ContentPermissions == Moooyo.BiZ.Content.ContentPermissions.AllOpen)
                              ifclick = false;
                          else
                              ifclick = true;
                      }
                      else
                      {
                          ifclick = true;
                      }
                      if (ifclick)
                      {
                          ids += interest.ID + ",";
                %><li class="userInterest" data-ifclick="true" data-ID="<%=interest.ID %>"><a><img src="<%=Comm.getImagePath(interest.ICONPath, ImageType.Middle) %>" height="51" width="51" title="<%=interest.Title %>" alt="<%=interest.Title %>" /></a><em></em></li><%
                      }
                      else
                      {
                %><li class="userInterest" data-ifclick="false" data-ID="<%=interest.ID %>"><a><img src="<%=Comm.getImagePath(interest.ICONPath, ImageType.Middle) %>" height="51" width="51" title="<%=interest.Title %>" alt="<%=interest.Title %>" /></a></li><%
                      }
                  }
                  //去除最后一个逗号
                  ids = ids != "" ? ids.Substring(0, ids.Length - 1) : "";
        %>
        </ul>
    </div>
	<div class="fb_box_com m_top30"><span>同步到</span><font class="cgray" >（点击即可绑定）</font></div>
    <div class="fb_box_com ">
		<dl style="padding-left:10px;">
        <%string wb = Model.User.BindedPlatforms;
          if (wb == "")
          {
          %>
		    <dt id="txdt" title="未绑定腾讯微博，点击绑定后，发布的内容将自动同步。"><a onclick="window.open('/MicroConn/ConnectToTXWeibo?isbinding=true')"><img src="/pics/weibo-2.png" height="24" width="25"/></a></dt>
		    <dt id="sinadt" title="未绑定新浪微博，点击绑定后，发布的内容将自动同步"><a onclick="window.open('/MicroConn/ConnectToSinaWeibo?isbinding=true')"><img src="/pics/demo15.png" height="24" width="25"/></a></dt>
		    <dt id="rrdt" title="未绑定人人帐号，点击绑定后，发布的内容将自动同步"><a onclick="window.open('/MicroConn/ConnectToRenRen?isbinding=true')"><img src="/pics/weibo3.png" height="24" width="25"/></a></dt>
		    <dt id="dbdt" title="未绑定豆瓣帐号，点击绑定后，发布的内容将自动同步"><a onclick="window.open('/MicroConn/ConnectToDouBan?isbinding=true')"><img src="/pics/weibo_4.png" height="24" width="25"/></a></dt>
            <input type="hidden" id="txwb" name="txwb" value="false" />
            <input type="hidden" id="sinawb" name="sinawb" value="false" />
            <input type="hidden" id="rrzh" name="rrzh" value="false" />
            <input type="hidden" id="dbzh" name="dbzh" value="false" />
          <%
          }
          else
          {
              if (wb.IndexOf("1") >= 0)
              {
                  %>
                    <dt id="sinadt" title="已绑定新浪微博" data-iftrue="true" data-hidid="sinawb" onclick="closeWb($(this))"><img src="/pics/demo15.png" height="24" width="25"/><em></em></dt>
                    <input type="hidden" id="sinawb" name="sinawb" value="true" />
                  <%
              }
              else
              {
                  %>
                    <dt id="sinadt" title="未绑定新浪微博，点击绑定后，发布的内容将自动同步" data-iftrue="true" data-hidid="sinawb"><a onclick="window.open('/MicroConn/ConnectToSinaWeibo?isbinding=true')"><img src="/pics/demo15.png" height="24" width="25"/></a></dt>
                    <input type="hidden" id="sinawb" name="sinawb" value="false" />
                  <%
              }
              if (wb.IndexOf("2") >= 0)
              {
                  %>
                    <dt id="txdt" title="已绑定腾讯微博" data-iftrue="true" data-hidid="txwb" onclick="closeWb($(this))"><img src="/pics/demo14.png" height="24" width="25"/><em></em></dt>
                    <input type="hidden" id="txwb" name="txwb" value="true" />
                  <%
              }
              else
              {
                  %>
                    <dt id="txdt" title="未绑定腾讯微博，点击绑定后，发布的内容将自动同步。" data-iftrue="true" data-hidid="txwb"><a onclick="window.open('/MicroConn/ConnectToTXWeibo?isbinding=true')"><img src="/pics/demo14.png" height="24" width="25"/></a></dt>
                    <input type="hidden" id="txwb" name="txwb" value="false" />
                  <%
              }
              if (wb.IndexOf("3") >= 0)
              {
                  %>
		            <dt id="rrdt" title="已绑定人人帐号" data-iftrue="true" data-hidid="rrzh" onclick="closeWb($(this))"><img src="/pics/weibo3.png" height="24" width="25"/><em></em></dt>
                    <input type="hidden" id="rrzh" name="rrzh" value="true" />
                  <%
              }
              else
              {
                  %>
		            <dt id="rrdt" title="未绑定人人帐号，点击绑定后，发布的内容将自动同步" data-iftrue="true" data-hidid="rrzh"><a onclick="window.open('/MicroConn/ConnectToRenRen?isbinding=true')"><img src="/pics/weibo3.png" height="24" width="25"/></a></dt>
                    <input type="hidden" id="rrzh" name="rrzh" value="false" />
                  <%
              }
              if (wb.IndexOf("4") >= 0)
              {
                  %>
		            <dt id="dbdt" title="已绑定豆瓣帐号" data-iftrue="true" data-hidid="dbzh" onclick="closeWb($(this))"><img src="/pics/weibo_4.png" height="24" width="25"/><em></em></dt>
                    <input type="hidden" id="dbzh" name="dbzh" value="true" />
                  <%
              }
              else
              {
                  %>
		            <dt id="dbdt" title="未绑定豆瓣帐号，点击绑定后，发布的内容将自动同步"><a onclick="window.open('/MicroConn/ConnectToDouBan?isbinding=true')"><img src="/pics/weibo_4.png" height="24" width="25"/></a></dt>
                    <input type="hidden" id="dbzh" name="dbzh" value="false" />
                  <%
              }
          }%>
		</dl>
	</div>
    </div>
</div>
<%if (Model.contentObj != null)
  {
      %>
    <input type="hidden" id="permissions" name="permissions" value="<%=Model.contentObj.ContentPermissions.GetHashCode() %>"/>
      <%
  }
  else
  {
      %>
    <input type="hidden" id="permissions" name="permissions" value="0"/>
      <%
  }%>
<input type="hidden" id="interestIDs" name="interestIDs" value="<%=ids %>"/>
<input type="hidden" id="interestIDshistory" name="interestIDshistory" value="<%=ids %>"/>
<%
  }%>
<script type="text/javascript">
    $().ready(function () {
        //加载兴趣选择的点击事件
        $(".userInterest").click(function () {
            interestClick($(this));
        });
        interestHover();

        $('.select_list').hide(); //初始ul隐藏
        $('.select_box span').click(function () { //鼠标点击函数
            $(this).parent().find('ul.select_list').slideDown();  //找到ul.son_ul显示
            $('ul.select_list li').eq(0).addClass('hover');
            $(this).parent().find('li').hover(function () {
                $(this).addClass('hover'); $(this).attr("data-select", "yes");
            }, function () {
                $(this).removeClass('hover'); $(this).attr("data-select", "none");
            }); //li的hover效果
            $(this).parent().hover(function () { },
						 function () {
						     $(this).parent().find("ul.select_list").slideUp();
						 });
        });
        $('ul.select_list li:first').each(function () {
            if ($(this).keypressed == true) {
                if (event.keyCode == '38' || event.keyCode == '33') {
                    alert("chenggng");
                }
            }
        });
        //鼠标按下事件
        $('ul.select_list li').click(function () {
            var permissionstype = $(this).attr("data-permissionstype");
            $("#permissions").val(permissionstype);
            $("#interestIDs").val($("#interestIDshistory").val());
            $(".userInterest").attr("data-ifopen", "true");
            var userInterests = $(".userInterest");
            $.each(userInterests, function (ij) {
                userInterests[ij].innerHTML = userInterests[ij].innerHTML + "<em></em>";
            });
            $(this).parents('li').find('span').html($(this).html());
            $(this).parents('li').find('ul').slideUp();
        });
    });
    function interestHover() {
        //加载兴趣选择的鼠标移动事件
        $(".userInterest").hover(
            function () {
                var ifopen = $(this).attr("data-ifopen");
                if (ifopen == "false") {
                    $(this).html($(this).html() + "<em></em>");
                }
            },
            function () {
                var ifopen = $(this).attr("data-ifopen");
                if (ifopen == "false") {
                    $(this).find("em").remove();
                }
            }
        );
    }
    //兴趣选择的点击方法
    function interestClick(obj) {
        var ids = $("#interestIDs").val();
        var id = obj.attr("data-ID");
        var idindex = ids.indexOf(id);
        //判断所点击的兴趣在记录的兴趣编号集合中是否存在
        var ifPresence = idindex >= 0 ? true : false;
        //不存在：添加
        if (!ifPresence) {
            if ($("#interestIDs").val() == "")
                $("#interestIDs").val(id);
            else
                $("#interestIDs").val($("#interestIDs").val() + "," + id);
            obj.attr("data-ifopen", "true");
            obj.html(obj.html() + "<em></em>");
        }
        //存在：移除
        else {
            if (idindex == 0) {
                var str = ids.substr((idindex + id.length + 1), ids.length - (idindex + id.length + 1));
                if (str != "") {
                    $("#interestIDs").val(str);
                    obj.attr("data-ifopen", "false");
                    obj.find("em").remove();
                }
                else {
                    $.jBox.tip("必须选择一个或多个兴趣。", 'error');
                }
            }
            else {
                var str = ids.substr(0, idindex - 1) + ids.substr((idindex + id.length), ids.length - (idindex + id.length));
                if (str != "") {
                    $("#interestIDs").val(str);
                    obj.attr("data-ifopen", "false");
                    obj.find("em").remove();
                }
                else {
                    $.jBox.tip("必须选择一个或多个兴趣。", 'error');
                }
            }
        }
        interestHover();
    }
    //打开或关闭微博分享
    function closeWb(thisobj) {
        var hidid = thisobj.attr("data-hidid");
        var iftrue = thisobj.attr("data-iftrue");
        if (iftrue == "true") {
            thisobj.children("em").remove();
            thisobj.attr("data-iftrue", "false");
            $("#" + hidid).val("false");
        }
        else {
            thisobj.append("<em></em>");
            thisobj.attr("data-iftrue", "true");
            $("#" + hidid).val("true");
        }
    }
    function ShareToWB(content, url) {
        //分享到微博
        var txwb = $("#txwb"), sinawb = $("#sinawb"), rrzh = $("#rrzh"), dbzh = $("#dbzh");
        var uploadFileNames = $("#uploadFileNames").val();
        var platform = "";
        if (sinawb.val() != null && sinawb.val() == "true") {
            if (platform == "") { platform = "1|"; }
            else { platform += "1|"; }
        }
        if (txwb.val() != null && txwb.val() == "true") {
            if (platform == "") { platform = "2|"; }
            else {platform += "2|";}
        }
        if (rrzh.val() != null && rrzh.val() == "true") {
            if (platform == "") { platform = "3|"; }
            else { platform += "3|"; }
        }
        if (dbzh.val() != null && dbzh.val() == "true") {
            if (platform == "") { platform = "4|"; }
            else { platform += "4|"; }
        }
        if (platform != "") {
            platform = platform.substr(0, platform.length - 1);
        }
        microConnOperation.sendInfo(platform, platform, content, "", url);
        if ((txwb.val() != null && txwb.val() == "true") || (sinawb.val() != null && sinawb.val() == "true") || (rrzh.val() != null && rrzh.val() == "true") || (dbzh.val() != null && dbzh.val() == "true")) {
            return true;
        }
        else {
            return false;
        }
    }
</script>
