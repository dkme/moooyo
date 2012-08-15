<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%int PointsSchedule = Model.Member.PointsSchedule; %>
<script src="/js/admin.js" type="text/javascript"></script>
<input type="hidden" id="pointsSchedule" name="pointsSchedule" value="<%=PointsSchedule %>"/>
<%
    switch (PointsSchedule)
    {
        case 0:
            %><div class="mt10 clear"></div>
            <div class="mo_account mh58 mobg" onclick="showpointsjindu()">
                <div class="mo_pic"><em class="mo1"></em></div>
                <div class="mo_content">
                    <span><font class="cz">还没人mo</font></span><span><font >果树还只是种子</font></span>
                </div>
            </div><% 
            break;
        case 1:
            %><div class="mt10 clear"></div>
            <div class="mo_account mh58 mobg" onclick="showpointsjindu()">
                <div class="mo_pic"><em class="mo2"></em></div>
                <div class="mo_content">
                    <span><em class="mb">mo</em><font class="cz">收集1</font></span><span><font >你的果树破土了</font></span>
                </div>
            </div><% 
            break;
        case 2:
            %><div class="mt10 clear"></div>
            <div class="mo_account mh58 mobg" onclick="showpointsjindu()">
                <div class="mo_pic"><em class="mo3"></em></div>
                <div class="mo_content">
                    <span><em class="mb">mo</em><font class="cz">收集2</font></span><span><font >你的果树发芽了</font></span>
                </div>
            </div><% 
            break;
        case 3:
            %><div class="mt10 clear"></div>
            <div class="mo_account mh58 mobg" onclick="showpointsjindu()">
                <div class="mo_pic"><em class="mo4"></em></div>
                <div class="mo_content">
                    <span><em class="mb">mo</em><font class="cz">收集3</font></span><span><font >你的果树长叶了</font></span>
                </div>
            </div><% 
            break;
        case 4:
            %><div class="mt10 clear"></div>
            <div class="mo_account mh58 mobg" onclick="showpointsjindu()">
                <div class="mo_pic"><em class="mo5"></em></div>
                <div class="mo_content">
                    <span><em class="mb">mo</em><font class="cz">收集4</font></span><span><font >你的果树开花了</font></span>
                </div>
            </div><%
            break;
        case 5:
            %><div class="mt10 clear"></div>
            <div class="mo_account mh103 mobg">
                <div class="mo_bigpic"><em></em></div>
                <div class="mo_bigcontent">
                    <span class="mospan"><em class="mb">mo</em><font class="cz">收集5</font></span>
                    <span class="mospan"><font class="mf">米果长出来了</font></span>
                    <%if (Model.IsOwner)
                      { 
                          %><span><a onclick="showaddpoints()"><em>采摘米果</em></a></span><%
                      } %>
                </div>
            </div><% 
          break;
    }
   %>

<div class="mt10 clear"></div>
<div class="mo_account mh55" onclick="showCreatedInviteCode()">
    <s></s>
    <div class="hatch-l"><img src="/pics/hatch_small.png" class="em"/></div>
    <div class="hatch-r">
       <div class="moyonum1" style="">邀请码孵化器</div>
       <div class="moyonum2" style="">邀请单身朋友玩转米柚</div>
    </div>
</div>
<%
    if (Model.Member.ICONPath.IndexOf("noicon") >= 0)
    {
        %><input type="hidden" id="noIcon" name="noIcon" value="true"/><%
    }
%>
<div class="mt10 clear"></div>
<div class="mo_account mh55" onclick="showSugge()">
    <s></s>
    <div class="moyotxt1" style="">我有建议，我要吐槽</div>
    <div class="moyotxt2" style="">米柚想要倾听你的声音</div>
</div>
<script type="text/javascript">
    $().ready(function () {
        $('.mh55').hover(function () {
            $(this).find('s').css({ "display": "block" });
            //$(this).find('div').css({"color":"#b40001"});
            $(this).find('s').addClass('Trans');

        }, function () {
            $(this).find('s').css({ "display": "none" });
            $(this).find('s').removeClass('Trans');
        });
        //左边柚子
        $('.mh58').each(function () {
            $(this).hover(
                function () { $(this).css({ "background-position": "-11px -214px", "cursor": "pointer" }); },
                function () { $(this).css({ "background-position": "-11px -150px", "cursor": "pointer" }); }
            );
        });
        $('.mh103').each(function () {
            $(this).hover(
                function () { $(this).css({ "background-position": "-242px -168px", "cursor": "pointer" }); },
                function () { $(this).css({ "background-position": "-243px -48px", "cursor": "pointer" }); }
            );
        });
    });

    //ADD 建议 BY XX

    function showSugge() {
        var suggestion = $("#suggestion").val();
        var str = "";
        str += "<div class=\"msg_box w600\" id=\"suggestion\" style=\"position:absolute; z-index:9999;left:50%; top:50%; margin-left:-300px!important;/*FF IE7 该值为本身宽的一半 */ margin-top:-250px!important;/*FF IE7 该值为本身高的一半*/ margin-top:0px; position:fixed!important;/* FF IE7*/ position:absolute;/*IE6*/ _top:expression(eval(document.compatMode && document.compatMode=='CSS1Compat') ? documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/ z-index:1000; background:#FFF;\">";
        str += "<div class=\"msg_title\">";
        str += "<h2></h2>";
        str += "<span class=\"msg_close\"><a onclick=\"$('#suggestion').css('display','none')\"><em></em></a></span>";
        str += "</div>";
        str += "<div class=\"say-m\" style=\"padding:0 40px; color:#444; line-height:30px; font-weight:bold; font-size:18px; font-family:'微软雅黑'; text-align:left;\">你的建议或吐槽：</div>";
        str +="<div class=\"say-s\" style=\"padding:20px 40px 0 40px; color:#878787; line-height:30px; font-size:16px; font-family:'微软雅黑'; text-align:left;\">";
        str += "<textarea id=\"inputtxt\" class=\"msgarea\" style=\"width:520px; height:260px; border:1px solid #ddd; overflow-y:hidden\"></textarea></div>";
        str += "<div class=\"msg_content\"><div class=\"msg_c_r w326\" style=\"float:right;\">";
        str += "<span class=\"btn_span\"><a class=\"redlink\" href=\"javascript:void(0)\" style=\"font-size:16px;\" onclick=\"submitGroov()\">提交</a></span></div></div>";
        str += "</div>";
        if ($('#suggestion').html() == null) {
            $("body").append(str);
        }
        else {
            $('#suggestion').css("display", "block");
        }
    }

    function submitGroov() {
        var content = $.trim($("#inputtxt").val());
        if (content == "") {
            $.jBox.tip("必须填写“你的建议或吐槽“！", 'error');
        }
        else {
            MyActivesProvider.AddGroove(content, function (data) {
                var obj = $.parseJSON(data);
                if (obj.ok) {
                    $.jBox.tip("您的信息已经提交，非常感谢您对米柚网的支持！", 'info');
                    $("#inputtxt").val("");
                    $('#suggestion').css("display", "none");
                }
                else {
                    $.jBox.tip("提交失败！", 'error');
                }
            });
        }
    }

    function showpointsjindu() {
        var pointsSchedule = $("#pointsSchedule").val();
        var str = "";
        str += "<div class=\"msg_box w600\" id=\"showpointsjindu\" style=\"position:absolute; z-index:9999;left:50%; top:50%; margin-left:-300px!important;/*FF IE7 该值为本身宽的一半 */ margin-top:-250px!important;/*FF IE7 该值为本身高的一半*/ margin-top:0px; position:fixed!important;/* FF IE7*/ position:absolute;/*IE6*/ _top:expression(eval(document.compatMode && document.compatMode=='CSS1Compat') ? documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/ z-index:1000; background:#FFF;\">";
        str += "<div class=\"msg_title\">";
        str += "<h2>米果进化论</h2>";
        str += "<span class=\"msg_close\"><a onclick=\"$('#showpointsjindu').css('display','none')\"><em></em></a></span>";
        str += "</div>";
        str += "<div class=\"msg_key\" style=\"padding-left:35px; padding-bottom:15px; text-align:left; line-height:30px; color:#666; font-family:'微软雅黑'; font-size:14px;\">每当你发布的精彩内容获得1个mo，米果树就会成长一步；<br/>累计获得5个mo，就可以采摘米果啦~</div>";
        str += "<div class=\"msg_content\" style=\"background:#f2f2f2\">";
        str += "<div class=\"msg_comb\" style=\" padding-bottom:25px;width:475px; margin:0 auto; overflow:hidden \">";
        str += "<dl class=\"mo_list\"><dt class=\"moyobtn " + (pointsSchedule == "0" ? "on" : "") + "\"><em class=\"mo1\"></em></dt><dd><em class=\"moyo\">0 mo</em></dd></dl>";
        str += "<dl class=\"mo_list\"><dt class=\"moyobtn " + (pointsSchedule == "1" ? "on" : "") + "\"><em class=\"mo2\"></em></dt><dd><em class=\"moyo\">1 mo</em></dd></dl>";
        str += "<dl class=\"mo_list\"><dt class=\"moyobtn " + (pointsSchedule == "2" ? "on" : "") + "\"><em class=\"mo3\"></em></dt><dd><em class=\"moyo\">2 mo</em></dd></dl>";
        str += "<dl class=\"mo_list\"><dt class=\"moyobtn " + (pointsSchedule == "3" ? "on" : "") + "\"><em class=\"mo4\"></em></dt><dd><em class=\"moyo\">3 mo</em></dd></dl>";
        str += "<dl class=\"mo_list\"><dt class=\"moyobtn " + (pointsSchedule == "4" ? "on" : "") + "\"><em class=\"mo5\"></em></dt><dd><em class=\"moyo\">4 mo</em></dd></dl>";
        str += "</div>";
        str += "<div class=\"msg_comb\" style=\" padding-bottom:25px;width:475px; margin:0 auto; overflow:hidden \">";
        str += "<dl class=\"mo_max\"><dt class=\"moyobtn \"><em></em></dt><dd><em class=\"moyo\">5 mo</em></dd></dl>";
        str += "</div>";
        str += "<div class=\"msg_comb\" style=\" width:475px; margin:0 auto; color:#666; font-size:16px; font-family:‘微软雅黑’; overflow:hidden; text-align:center;\">";
        switch (pointsSchedule) {
            case "0": str += "（┬＿┬）还没有人mo你，赶紧发布点精彩的内容，抓住大伙儿的眼球吧！"; break;
            case "1": str += "（￣︶￣）已经获得1个mo，加油加油，米果树已经破土啦！"; break;
            case "2": str += "（￣▽￣）已经获得2个mo，米果树发芽了，努力培养更高人气吧！"; break;
            case "3": str += "（＞﹏＜）已经获得3个mo，米果树茁壮成长ing，都长叶了！"; break;
            case "4": str += "（°▽°）已经获得4个mo，米果树开花了，收获在望！"; break;
        }
        str += "</div>";
        str += "</div>";
        str += "<div class=\"msg_bottom\" style=\"height:40px; padding:10px 15px 15px 15px; overflow:hidden;\">";
        str += "<span style=\"display:block;height:35px;line-height:35px;float:left;padding-left:10px;font-size:12px;\"><a href=\"/Content/AboutUsePoints\">米果有什么用？</a></span>";
        str += "<span class=\"btn_span\" style=\"display:block\"><a class=\"redlink\" onclick=\"$('#showpointsjindu').css('display','none')\">确   定</a></span>";
        str += "</div>";
        str += "</div>";
        if ($('#showpointsjindu').html() == null) {
            $("body").append(str);
        }
        else {
            $('#showpointsjindu').css("display", "block");
        }
    }
    function showaddpoints() {
        //随机获取米果数
        randompoints();
        var str = "";
        str += "<div class=\"msg_box w600\" id=\"addpoints\" style=\"position:absolute; z-index:9999;left:50%; top:50%; margin-left:-300px!important;/*FF IE7 该值为本身宽的一半 */ margin-top:-250px!important;/*FF IE7 该值为本身高的一半*/ margin-top:0px; position:fixed!important;/* FF IE7*/ position:absolute;/*IE6*/ _top:expression(eval(document.compatMode && document.compatMode=='CSS1Compat') ? documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/ z-index:1000; background:#FFF;\">";
        str += "<div class=\"msg_title\">";
        str += "<h2>采摘米果</h2>";
        str += "<span class=\"msg_close\"><a onclick=\"$('#addpoints').css('display','none');\"><em></em></a></span>";
        str += "</div>";
        str += "<div class=\"msg_key\" style=\"padding-left:35px; padding-bottom:15px; text-align:left; line-height:30px; color:#666; font-family:'微软雅黑'; font-size:16px;\"></div>";
        str += "<div class=\"msg_content modemo1\" style=\"background:#f2f2f2\">";
        str += "<div class=\"msg_comb\" style=\" padding-bottom:25px;width:475px; margin:0 auto;color:#666; font-size:16px; font-family:'微软雅黑'; overflow:hidden; text-align:center; line-height:30px;\">（≧▽≦）人类已经无法阻止你的高人气了！马上采摘米果吧！<br />试试手气，看看你这次能摘到几个</div>";
        str += "<div class=\"msg_comb\" style=\" padding-bottom:25px;width:475px; margin:0 auto; overflow:hidden \">";
        str += "<dl class=\"mo_large\"><dt class=\"moyobtn\"><em class=\"lag\"></em></dt></dl>";
        str += "</div>";
        str += "<div class=\"msg_comb\" style=\" width:475px; margin:0 auto; color:#666; font-size:16px; font-family:'微软雅黑'; overflow:hidden; text-align:center;\">（单击果树采摘）</div>";
        str += "</div>";
        str += "<div class=\"msg_content modemo2\" style=\"background:#f2f2f2; display:none\">";
        str += "<div class=\"msg_mo\" style=\"width:100%; overflow:hidden;\">";
        str += "<div class=\"msg_comb\" style=\" padding-bottom:25px;width:166px; float:left; margin:0 auto; overflow:hidden \">";
        str += "<dl class=\"mo_large\"><dt class=\"moyobtn\"><em class=\"moyo\"></em></dt></dl>";
        str += "</div>";
        str += "<div class=\"msg_comb\" style=\" padding:20px 0;width:266px; float:left; margin-left:30px; overflow:hidden \">";
        str += "<div class=\"randomCount\" style=\"text-align:left; font-size:16px; color:#666; font-family:'微软雅黑';\">采到" + points + "个米果</div>";
        str += "<div class=\"moyoo_ul\">";
        str += "<ul>";
        for (var i = 0; i < points; i++) {
            str += "<li><em></em></li>";
        }
        str += "</ul>";
        str += "</div>";
        str += "</div>";
        str += "</div>";
        str += "<div class=\"msg_comb pointsmsg\" style=\" width:475px; margin:0 auto; line-height:25px; color:#666; font-size:16px; font-family:'微软雅黑'; overflow:hidden; text-align:center;\">";
        switch (points) {
            case 1: str += "终于到米果大丰收的季节了，可因为你木有悉心照料米果树<br/>，导致树虫泛滥，收到一颗米果都算你运气不错啦！"; break;
            case 2: str += "终于到米果大丰收的季节了，可因为你木有悉心照料米果树<br/>，导致树虫泛滥，收到两颗米果都算你运气不错啦！"; break;
            case 3: str += "终于到米果大丰收的季节了，收成还不错<br/>，收到三颗米果！"; break;
            case 4: str += "终于到米果大丰收的季节了，收成相当好<br/>，收到四颗米果！"; break;
            case 5: str += "终于到米果大丰收的季节了，真是太好啦<br/>，竟然收到了五颗米果！"; break;
        }
        str += "</div>";
        str += "</div>";
        str += "<div class=\"msg_bottom\" style=\"height:40px; padding:10px 15px 15px 15px; overflow:hidden;\">";
        str += "<span class=\"btn_span\" style=\"display:block\"><a class=\"redlink mobtn\" onclick=\"window.location.reload();\">确   定</a></span>";
        str += "</div>";
        str += "</div>";
        if ($("#addpoints").html() != null) {
            $("#addpoints").css("display", "block");
        }
        else {
            $("body").append(str);
            $('.mobtn').hide();
            $('.mo_large').hover(
                function () { $(this).children('dt').css({ "background-position": "-445px -115px" }); },
                function () { $(this).children('dt').css({ "background-position": "-627px -115px" }); }
            );
            $('.mo_large').click(function () {
                var thismarginleft = ($(".moyobtn").width() - $(".lag").width()) / 2;
                $(".lag").css("marginLeft", thismarginleft);
                $(".lag").stop(true, true);
                $(".lag").animate({ marginLeft: thismarginleft + thismarginleft / 3 }, 100).animate({ marginLeft: thismarginleft - thismarginleft / 3 }, 100).animate({ marginLeft: thismarginleft + thismarginleft / 4 }, 90).animate({ marginLeft: thismarginleft - thismarginleft / 4 }, 90).animate({ marginLeft: thismarginleft + thismarginleft / 6 }, 70).animate({ marginLeft: thismarginleft - thismarginleft / 6 }, 70).animate({ marginLeft: thismarginleft + thismarginleft / 3 }, 100).animate({ marginLeft: thismarginleft - thismarginleft / 3 }, 100).animate({ marginLeft: thismarginleft + thismarginleft / 4 }, 90).animate({ marginLeft: thismarginleft - thismarginleft / 4 }, 90).animate({ marginLeft: thismarginleft + thismarginleft / 6 }, 70).animate({ marginLeft: thismarginleft - thismarginleft / 6 }, 70).animate({ marginLeft: thismarginleft + thismarginleft / 8 }, 40).animate({ marginLeft: thismarginleft - thismarginleft / 8 }, 40).animate({ marginLeft: thismarginleft }, 10, function () {
                    $(".lag").stop(true, true);
                    $('.modemo1').hide();
                    $('.mobtn').show();
                    $('.modemo2').css({ "display": "block" });
                    $('.mo_large').removeAttr("click");
                    $('.mo_large').unbind("click");
                    submitPoints();
                    $(".msg_close").children("a").removeAttr("click");
                    $(".msg_close").children("a").unbind("click");
                    $(".msg_close").children("a").bind("click", function () { window.location.reload(); });
                });
            });
        }
    }
    var points = 0;
    function randompoints() {
        points = parseInt(Math.random() * 5 + 1);
    }
    //采集米果
    function submitPoints() {
        if (points == 0) { randompoints(); }
        memberprovider.setPointsScheduleZero(points, function (data) {
            var data = $.parseJSON(data);
            if (data) {
            }
            else {
                $.jBox.tip("失败，系统维护中，给您带来了不便，请谅解！", 'error');
            }
        });
    }

    function showCreatedInviteCode() {
        var noIcon = $("#noIcon").val();
        if (noIcon != null) {
            createCodeFlase();
        }
        else {
            adminActivityDataProvider.getInviteCodesToMember(function (data) {
                var obj = $.parseJSON(data);
                if (obj.toString() == "true" || obj.toString() == "True") {
                    createCode();
                }
                else {
                    createCodestep1(obj.InviteCode);
                }
            });
        }
    }
    function createdInviteCode() {
        adminActivityDataProvider.generateInviteCodeToMember(function (data) {
            var data = $.parseJSON(data);
            createCodestep2(data.InviteCode);
        });
    }
    //add create code by xx
    function createCode() {
        var hatchId = $("#hatchId").val();
        var str = "";
        str += "<div class=\"msg_box w600\" id=\"hatchId\" style=\"position:absolute; z-index:9999;left:50%; top:50%; margin-left:-300px!important;/*FF IE7 该值为本身宽的一半 */ margin-top:-250px!important;/*FF IE7 该值为本身高的一半*/ margin-top:0px; position:fixed!important;/* FF IE7*/ position:absolute;/*IE6*/ _top:expression(eval(document.compatMode && document.compatMode=='CSS1Compat') ? documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/ z-index:1000; background:#FFF;\">";
        str += "<div class=\"msg_title\">";
        str += "<h2>孵化邀请码</h2>";
        str += "<span class=\"msg_close\"><a onclick=\"$('#hatchId').css('display','none')\"><em></em></a></span>";
        str += "</div>";
        str += "<div class=\"say-s\" style=\"padding:20px 40px 0 40px; color:#444; line-height:30px; font-size:16px; font-family:'微软雅黑'; text-align:left;\">";
        str += "<div class=\"say-img mt20\" style=\"margin:20px 0; text-align:center;\"><img src=\"/pics/hatch.gif\" /></div>";
        str += "<div class=\"say-c\">作为行走米柚多年的老江湖，现在，你有特权带身边因木有邀请码，而只能苦苦在外翘首企盼的单身进入米柚啦~</div>";
        str += "</div>";
        str += "<div class=\"msg_content\"><div class=\"msg_c_r w326\" style=\"float:right;\">";
        str += "<span class=\"btn_span\"><a class=\"redlink\" href=\"javascript:void(0)\" onclick=\"$('#hatchId').css('display','none');createdInviteCode()\" style=\"font-size:16px; float:left\">孵化邀请码</a></span></div></div>";
        str += "</div>";
        if ($('#hatchId').html() == null) {
            $("body").append(str);
        }
        else {
            $('#hatchId').css("display", "block");
        }
    }

    // 点击 孵化邀请码 状态1

    function createCodestep1(code) {
        var hatchStep1 = $("#hatchStep1").val();
        var str = "";
        str += "<div class=\"msg_box w600\" id=\"hatchStep1\" style=\"position:absolute; z-index:9999;left:50%; top:50%; margin-left:-300px!important;/*FF IE7 该值为本身宽的一半 */ margin-top:-250px!important;/*FF IE7 该值为本身高的一半*/ margin-top:0px; position:fixed!important;/* FF IE7*/ position:absolute;/*IE6*/ _top:expression(eval(document.compatMode && document.compatMode=='CSS1Compat') ? documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/ z-index:1000; background:#FFF;\">";
        str += "<div class=\"msg_title\">";
        str += "<h2>孵化邀请码</h2>";
        str += "<span class=\"msg_close\"><a onclick=\"$('#hatchStep1').css('display','none')\"><em></em></a></span>";
        str += "</div>";
        str += "<div class=\"say-s\" style=\"padding:20px 40px 0 40px; color:#444; line-height:30px; font-size:16px; font-family:'微软雅黑'; text-align:left;\">";
        str += "<div class=\"say-img mt20\" style=\"margin:20px 0;  \">";
        str += " <div class=\"say-img-l\" style=\" height:95px; width:90px; float:left;  padding-left:120px;  \"><img src=\"/pics/harched.png\" /></div>";
        str += "<div class=\"say-img-r\" style=\"width:260px; padding-left:30px; padding-top:10px; height:85px; float:left; text-align:left\">";
        str += "<span style=\"display:block; line-height:35px; font-size:18px; color:#b20000; font-family:'微软雅黑'; \">你今天孵化的邀请码是</span>";
        str += " <span id=\"code1\" style=\"display:block; line-height:35px; font-size:30px; color:#000; font-family:'微软雅黑'; \">" + code + "</span>";
        str += "</div>";
        str += "</div>";
        str += "<div class=\"say-c\" style=\"text-align:center; color:#444; clear:both; padding-top:30px; _padding-top:5px;\">米柚一码值千金！每天只能孵化一个，明日趁早哟~</div>";
        str += "<div class=\"msg_content\"><div class=\"msg_c_r w326\" style=\"float:right; _padding-bottom:20px;\">";
        str += "<span class=\"btn_span\"><a class=\"redlink\" href=\"javascript:void(0)\" onclick=\"$('#hatchStep1').css('display','none')\" style=\"font-size:16px; float:left; margin-left:55px;\">好</a></span></div></div>";
        str += "</div>";
        if ($('#hatchStep1').html() == null) {
            $("body").append(str);
        }
        else {
            $('#hatchStep1').css("display", "block");
            $("#code1").html(code);
        }
    }
    // 点击 孵化邀请码 状态2
    function createCodestep2(code) {
        var hatchStep2 = $("#hatchStep2").val();
        var str = "";
        str += "<div class=\"msg_box w600\" id=\"hatchStep2\" style=\"position:absolute; z-index:9999;left:50%; top:50%; margin-left:-300px!important;/*FF IE7 该值为本身宽的一半 */ margin-top:-250px!important;/*FF IE7 该值为本身高的一半*/ margin-top:0px; position:fixed!important;/* FF IE7*/ position:absolute;/*IE6*/ _top:expression(eval(document.compatMode && document.compatMode=='CSS1Compat') ? documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/ z-index:1000; background:#FFF;\">";
        str += "<div class=\"msg_title\">";
        str += "<h2>成功</h2>";
        str += "<span class=\"msg_close\"><a onclick=\"$('#hatchStep2').css('display','none')\"><em></em></a></span>";
        str += "</div>";
        str += "<div class=\"say-s\" style=\"padding:20px 40px 0 40px; color:#444; line-height:30px; font-size:16px; font-family:'微软雅黑'; text-align:left;\">";
        str += "<div class=\"say-img mt20\" style=\"margin:20px 0; \">";
        str += " <div class=\"say-img-l\" style=\" height:95px; width:90px; float:left;  padding-left:120px;\"><img src=\"/pics/harched.png\" /></div>";
        str += "<div class=\"say-img-r\" style=\"width:260px; padding-left:30px; padding-top:10px; height:85px; float:left; text-align:left\">";
        str += "<span style=\"display:block; line-height:35px; font-size:18px; color:#b20000; font-family:'微软雅黑'; \">你今天孵化的邀请码是</span>";
        str += " <span id=\"code2\" style=\"display:block; line-height:35px; font-size:30px; color:#000; font-family:'微软雅黑'; \">" + code + "</span>";
        str += "</div>";
        str += "</div>";
        str += "<div class=\"say-c\" style=\"text-align:center; color:#444; clear:both; padding-top:30px; _padding-top:5px;\">邀请码已孵化。如此珍宝，请轻手轻脚小心谨慎地交给你的单身朋友哟~</div>";
        str += "<div class=\"msg_content\"><div class=\"msg_c_r w326\" style=\"float:right; _padding-bottom:20px;\">";
        str += "<span class=\"btn_span\"><a class=\"redlink\" href=\"javascript:void(0)\" onclick=\"$('#hatchStep2').css('display','none')\" style=\"font-size:16px; float:left; margin-left:55px; \">好</a></span></div></div>";
        str += "</div>";
        if ($('#hatchStep2').html() == null) {
            $("body").append(str);
        }
        else {
            $('#hatchStep2').css("display", "block");
            $("#code2").html(code);
        }
    }

    //没有上传头像的用户生成邀请码
    function createCodeFlase() {
        var hatchFlase = $("#hatchFlase").val();
        var str = "";
        str += "<div class=\"msg_box w600\" id=\"hatchFlase\" style=\"position:absolute; z-index:9999;left:50%; top:50%; margin-left:-300px!important;/*FF IE7 该值为本身宽的一半 */ margin-top:-250px!important;/*FF IE7 该值为本身高的一半*/ margin-top:0px; position:fixed!important;/* FF IE7*/ position:absolute;/*IE6*/ _top:expression(eval(document.compatMode && document.compatMode=='CSS1Compat') ? documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/ z-index:1000; background:#FFF;\">";
        str += "<div class=\"msg_title\">";
        str += "<h2>孵化邀请码</h2>";
        str += "<span class=\"msg_close\"><a onclick=\"$('#hatchFlase').css('display','none')\"><em></em></a></span>";
        str += "</div>";
        str += "<div class=\"say-s\" style=\"padding:20px 40px 0 40px; color:#444; line-height:30px; font-size:16px; font-family:'微软雅黑'; text-align:left;\">";
        str += "<div class=\"say-img mt20\" style=\"margin:20px 0; text-align:center;\">";
        str += " <div class=\"say-img-l\" style=\" height:118px; width:118px; float:left; margin-left:40px;\"><img src=\"/pics/upload.gif\" height=\"118\" width=\"118\" /></div>";
        str += "<div class=\"say-img-r\" style=\"width:310px; padding-left:30px; padding-top:10px; height:118px; float:left; text-align:left\">";
        str += " <span style=\"display:block; line-height:30px; margin-top:40px; width:230px;font-size:18px; color:#b20000; font-family:'微软雅黑'; \">你还没有上传头像，不能孵化邀请码哦</span>";

        str += "</div>";
        str += "</div>";
        str += "<div class=\"say-c\" style=\"text-align:center; color:#444; clear:both;\">邀请码每天只能生成一个，这么宝贵的机会怎么可以断送在木有头像这样的原因上？！</div>";
        str += "<div class=\"msg_content\"><div class=\"msg_c_r w326\" style=\"float:right;\">";
        str += "<span class=\"btn_span\"><a class=\"redlink\" href=\"javascript:void(0)\" onclick=\"window.location='/Setting/UploadFace';\" style=\"font-size:16px; float:left\">上传头像</a></span></div></div>";
        str += "</div>";
        if ($('#hatchFlase').html() == null) {
            $("body").append(str);
        }
        else {
            $('#hatchFlase').css("display", "block");
        }
    }
</script>
