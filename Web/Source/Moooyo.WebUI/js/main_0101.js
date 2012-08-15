/////////////////////////////////////////////////////////////////////////////
// 页面方法组
/////////////////////////////////////////////////////////////////////////////

var member_i_functions = {
    favormemberStyle3: function (mid, container) {
        MemberLinkProvider.favor(mid, function (result) {
            var str = "";
            if (result.ok) {
                container.attr("class", "delete-btn");
                container.removeAttr('onclick').unbind('click').click(function () {
                    member_i_functions.deletefavormember(mid, $(this));
                });
                container.html("<i></i>取消");
            }
        });
    },
    deletefavormemberStyle3: function (mid, container) {
        var flag = confirm("取消关注Ta？");
        if (flag) {
            MemberLinkProvider.delfavor(mid, function (result) {
                if (result.ok) {
                    container.attr("class", "add-btn");
                    container.removeAttr('onclick').unbind('click').click(function () {
                        member_i_functions.favormember(mid, $(this));
                    });
                    container.html("<i></i>关注");
                }
            });
        }
        return flag;
    },
    favormembertow: function (mid, container, ifmove) {
        MemberLinkProvider.favor(mid, function (result) {
            var str = "";
            if (result.ok) {
                if (ifmove != null && ifmove) {
                    container.remove();
                }
                else {
                    container.html("<span>取消</span><em style=\"background:url('/pics/self_del.png')\"></em>");
                    //                container.html("取消");
                    //                container.parent().parent().children("em").css("background", "url('/pics/self_del.png')");
                    container.removeAttr("onclick").unbind("click").click(function () {
                        member_i_functions.deletefavormembertwo(mid, container);
                    });
                }
            }
        });
    },

    injoinfans: function (mid, container,ifmove) {
        MemberLinkProvider.favor(mid, function (result) {
          var str="";
          if(result.ok){
            if (ifmove != null && ifmove) {
               container.remove();
          }
          else
          {
           container.html("取消关注");
            container.removeAttr("onclick").unbind("click").click(function () {
                        member_i_functions.deljoinfans(mid, container);
                    });
           }
        }
      });
    },
    deljoinfans: function (mid, container) {
        var flag = confirm("确定取消对Ta的关注?");
        if (flag) {
            MemberLinkProvider.delfavor(mid, function (result) {
                if (result.ok) {
                    container.html("加关注");
                    container.removeAttr("onclick").unbind("click").click(function () {
                        member_i_functions.injoinfans(mid, container);
                    });
                }
            });
        }
        return flag;
    },
   deletefavormembertwo: function (mid, container) {
        var flag = confirm("取消关注Ta？");
        if (flag) {
            MemberLinkProvider.delfavor(mid, function (result) {
                if (result.ok) {
                    container.html("<span>加关注</span><em style=\"background:url('/pics/self_add.png')\"></em>");
                    //                    container.html("加关注");
                    //                    container.parent().parent().children("em").css("background", "url('/pics/self_add.png')");
                    container.removeAttr("onclick").unbind("click").click(function () {
                        member_i_functions.favormembertow(mid, container);
                    });
                }
            });
        }
        return flag;
    },
    favormember: function (mid, container) {
        MemberLinkProvider.favor(mid, function (result) {
            var str = "";
            if (result.ok) {
                container.attr("class", "delete");
                container.removeAttr('onclick').unbind('click').click(function () {
                    member_i_functions.deletefavormember(mid, $(this));
                });
            }
        });
    },
    deletefavormember: function (mid, container) {
        var flag = confirm("取消关注Ta？");
        if (flag) {
            MemberLinkProvider.delfavor(mid, function (result) {
                if (result.ok) {
                    container.attr("class", "add");
                    container.removeAttr('onclick').unbind('click').click(function () {
                        member_i_functions.favormember(mid, $(this));
                    });
                }
            });
        }
        return flag;
    },
    deletememberfavor: function (mid, container) {
        if (confirm("移除该粉丝？")) {
            MemberLinkProvider.delMemberFavor(mid, function (result) {
                if (result.ok) {
                    $.jBox.tip("粉丝已移除", 'success');
                }
            });
            return true;
        }
        else return false;
    },
    favormember_style2: function (mid, container) {
        MemberLinkProvider.favor(mid, function (result) {
            var str = "";
            if (result.ok) {
                container.attr("class", "delete-btn2");
                container.removeAttr('onclick').unbind('click').click(function () {
                    member_i_functions.deletefavormember_style2(mid, $(this));
                });
                container.html("<i></i>删粉");
            }
        });
    },
    deletefavormember_style2: function (mid, container) {
        if (confirm("取消关注Ta？")) {
            MemberLinkProvider.delfavor(mid, function (result) {
                if (result.ok) {
                    container.attr("class", "add-btn2");
                    container.removeAttr('onclick').unbind('click').click(function () {
                        member_i_functions.favormember_style2(mid, $(this));
                    });
                    container.html("<i></i>成为TA的粉丝");
                }
            });
        }
    },
    disablemember: function (mid) {
        if (confirm("禁止Ta联系你（如果你已关注Ta，将自动取消关注）？")) {
            MemberLinkProvider.disable(mid, function (result) {
                $.jBox.tip("已设置成功", 'success');
            });
        }
    },
    bindinterviewtitlemsg: function (container, moreinvert, domelink) {
        var str = "<div class='titletop'><p class='titlemsg'>有空的时候，来这里聊自己感兴趣的话题。别只顾埋头往前走，偶尔歇下脚步，给自己做一回人生专访！</p>";
        str += "<p class='titlemsgsmall'>如果您有好的问题，也可以提交给我们<span class='linkT1 colorf90' onclick='actionprovider.opencalladmin(\"\",2);'>编辑部</span>，一经采用，您将收到我们精心准备的礼物哦。</p></div>";

        container.html(str);
        if (!isMe) {
            moreinvert.html("<a onclick='Invert.interview(\"" + mid + "\",moreinvert);'>邀请Ta完成更多专访</a>");
            domelink.html("<a href='/Content/AddCallForContent/" + me + "/interview' target='_blank'>去完成我自己的访谈</a>");
        }
    },
    bindinterviews: function (objs, container) {
        var str = "<div class='talk-box mt5 clearfix'>";
        $.each(objs, function (i) {
            str += member_i_functions.buildinterviewlist(objs[i]);
        });
        str += "</div>";
        container.html(str);
    },
    buildinterviewlist: function (obj) {
        var str = "<ul class='talk-list clearfix' id='interview" + obj.ID + "'>";
        str += "<li id='question" + obj.ID + "'>小编：" + obj.Question + "</li>";
        if (obj.SystemQuestionID != null) {
            str += "<li><b class='fl mr15'>" + nickname + "：<span id='answer" + obj.ID + "' edittype='edit'>" + obj.Answer + "</span></b><a href='#' class='talk-btn fl'><i></i>评论</a></li>";
            if (isMe)
                str += "<li id='answerbtn" + obj.ID + "'><input type='button' value='修改' class='editbtn' onclick='member_i_functions.editanswer(\"" + obj.ID + "\")'/><input type='button' value='删除' class='delbtn' onclick='member_i_functions.delanswer(\"" + obj.ID + "\")''/></li>";
        }
        else {
            str += "<li class='label blue'>" + nickname + "：</li><li id='answer" + obj.ID + "' edittype='new'></li>";
            if (isMe)
                str += "<li id='answerbtn" + obj.ID + "'><input type='button' value='回答问题' class='answerbtn' onclick='member_i_functions.editanswer(\"" + obj.ID + "\")'/></li>";
        }
        str += "<li><div id='answerarea" + obj.ID + "' class='answerarea'><textarea id='input" + obj.ID + "'></textarea>";
        str += "<input type='button' value='保存答案' class='savebtn' onclick='member_i_functions.saveanswer(\"" + obj.ID + "\")'/><div></li>";
        str += "</ul>";
        return str;
    },
    delanswer: function (id) {
        memberprovider.deleteinterview(id, mid, function (result) {
            $("#interview" + id).remove();
        });
    },
    editanswer: function (id) {
        $("#answer" + id).hide();
        $("#answerbtn" + id).hide();
        $("#answerarea" + id).show();
        $("#input" + id).val($("#answer" + id).html());
        //标记是新增还是修改
        $("#input" + id).attr("edittype", $("#answer" + id).attr("edittype"));
    },
    saveanswer: function (id) {
        var question = $("#question" + id).html();
        var value = $("#input" + id).val();
        if (trim(value) == "") { $("#input" + id).focus(); return; }
        if (!checkLen(value)) return;

        if ($("#input" + id).attr("edittype") === "new") {
            memberprovider.addinterview(mid, id, question, value, function () {
                $("#input" + id).val("");
                $("#answerarea" + id).hide();
                $("#answer" + id).html(value);
                $("#answer" + id).show();

                //同步到微博
                microConnOperation.sendSyncBoxInfo("syncBox" + id, bindedPlatforms);
            });
        }
        if ($("#input" + id).attr("edittype") === "edit") {
            memberprovider.updateinterview(id, mid, value, function () {
                $("#input" + id).val("");
                $("#answerarea" + id).hide();
                $("#answer" + id).html(value);
                $("#answer" + id).show();

                //同步到微博
                microConnOperation.sendSyncBoxInfo("syncBox" + id, bindedPlatforms);
            });
        }
    }
};

//兴趣相关
var interestCenterFunctions = {
    addInterestFans: function (iId) { //加粉
        var str = "";
        interestCenterProvider.addInterestFans(iId, function (data) {
            var result = $.parseJSON(data);
            if (result.ok) {
                str += "<a href=\"javascript:void(0);\" onclick=\"interestCenterFunctions.delInterestFans(\'" + iId + "\')\" class=\"delete-btn fl\"><i></i>删粉</a>";
                $("#isFans" + iId).html(str);
                //                //打开发言按钮
                //                if ($("#addwwtoFans").html() != null) {
                //                    $("#addwwtoFans").html("<a href=\"/WenWen/AddWenWens/" + iId + "\" class=\"fr radius3 btn\" style=\"width:60px; text-align:center;\">发言</a>")
                //                }
            }
        });
    },
    delInterestFans: function (iId) { //删粉
        var str = "";
        interestCenterProvider.delInterestFans(iId, function (data) {
            var result = $.parseJSON(data);
            if (result.ok) {
                str += "<a href=\"javascript:void(0);\" onclick=\"interestCenterFunctions.addInterestFans(\'" + iId + "\')\" class=\"add-btn fl mr5\"><i></i>加粉</a>";
                $("#isFans" + iId).html(str);
                //                //关闭发言按钮
                //                if ($("#addwwtoFans").html() != null) {
                //                    $("#addwwtoFans").html("<a class=\"fr radius3 btn\" style=\"width:60px; text-align:center;\" onclick=\"$.jBox.tip('只有关注这个兴趣的人才能发言！', 'info'); \">发言</a>")
                //                }
            }
        });
    },
    isFans: function (iId) { //是否是粉丝
        var str = "";
        interestCenterProvider.isFans(iId, function (data) {
            var result = $.parseJSON(data);
            if (result) {
                str += "<a href=\"javascript:void(0);\" onclick=\"interestCenterFunctions.delInterestFans(\'" + iId + "\')\" class=\"delete-btn fl\"><i></i>删粉</a>";
            }
            else {
                str += "<a href=\"javascript:void(0);\" onclick=\"interestCenterFunctions.addInterestFans(\'" + iId + "\')\" class=\"add-btn fl mr5\"><i></i>加粉</a>";
            }
            $("#isFans" + iId).html(str);
        });
    },
    getMemberInterest: function (iId) { //获取我创建的兴趣
        interestCenterProvider.getMemberInterest(iId, function (data) {
            //不是我的兴趣则data为True，反则data为False
            if (data != "True") {
                interestCenterFunctions.isFans(iId);
            }
            else if (data != "False") {

            }
        });
    }
}

//问问相关
var nowanserid = "";
var WenWenControl = {
    insertwenwenanswer: function (interestid, wenwenid, content, upordown) {
        WenWenLinkProvider.addwenwenanswer(interestid, wenwenid, content, upordown, function (result) {
            var obj = $.parseJSON(result);
            if (!obj)
                $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error');
        });
    },
    addwenwenanswer: function (userid, interestid, wenwenid, content) {
        WenWenLinkProvider.addwenwenanswer(interestid, wenwenid, content, true, function (result) {
            var obj = $.parseJSON(result);
            var answerlist = obj.answerlist;
            if (answerlist.length > 0) {
                WenWenControl.showanswer(userid, wenwenid, 5, 1, true);
            }
        });
    },
    showtext: function (userid, interestid, wenwenid) {
        if (wenwenid != nowanserid && nowanserid.length > 2) {
            $("#" + nowanserid + "text").html("");
            $("#" + nowanserid).html("");
            $("#" + nowanserid).parent().css("backgroundColor", "");
        }
        if ($("#" + wenwenid).html() != null && $.trim($("#" + wenwenid).html()) != "")
            $("#" + wenwenid).html("");
        if ($.trim($("#" + wenwenid + "text").html()) == "") {
            var str = "<form action=\"#\"><textarea id=\"answertext\" name=\"answertext\" value=\"\" class=\"answer-txt\"></textarea><div class=\"clearfix mt11\"><b class=\"fl mr5\">同步到</b><input type=\"checkbox\" class=\"fl mr5 nobor\"/><i class=\"t-sina fl mr5\"></i><input type=\"checkbox\" class=\"fl mr5 nobor\"/><i class=\"t-qq fl\"></i><input id=\"answerbutton\" name=\"answerbutton\" type=\"button\" value=\"发送\" class=\"btn radius3 fr\" onclick=\"WenWenControl.addwenwenanswer('" + userid + "','" + interestid + "','" + wenwenid + "',$('#answertext').val())\"/></div></form>";
            $("#" + wenwenid + "text").html(str);
            nowanserid = wenwenid;
        }
        else {
            $("#" + wenwenid + "text").html("");
            $("#" + wenwenid).parent().css("backgroundColor", "");
        }
    },
    getnextwenwen: function (userid, page) {
        var str = "";
        $.ajaxSetup({
            async: false
        });
        WenWenLinkProvider.getnextwenwen(page, function (result) {
            var obj = $.parseJSON(result);
            if (obj.wenwen != null) {
                str += "<div class=\"divonepz\" upordown=\"" + obj.wenwen.UpDowner.DownCount + "," + obj.wenwen.UpDowner.UpCount + "\" wwid=\"" + obj.wenwen.ID + "\" xqid=\"" + obj.interest.ID + "\"><img class=\"imgonepz\" src=\"../../upload/wenwen/ww_12.gif\"/><img class=\"imgtwopz\" data-interestid=\"" + obj.interest.ID + "\" src=\"../../upload/wenwen/text.jpg\" onclick=\"window.open('/InterestCenter/InterestFans?iid=" + obj.interest.ID + "')\" alt=\"" + photofunctions.geticonphotoname(obj.interest.ICONPath) + "\"/><div class=\"nrdivpz\"><img data_me_id=\"" + userid + "\" data_member_id=\"" + obj.wenwen.Creater.MemberID + "\" src=\"" + photofunctions.getprofileiconphotoname(obj.wenwen.Creater.ICONPath) + "\" onclick=\"window.open('/Member/Ta/" + obj.wenwen.Creater.MemberID + "')\"/><div>" + obj.wenwen.Content + "</div></div></div>";
            }
        });
        $.ajaxSetup({
            async: true
        });
        return str;
    },
    getwenwenfroanswercount: function () {
        var count = 0;
        $.ajaxSetup({ async: false });
        WenWenLinkProvider.getwenwenfroanswercount(function (result) {
            var obj = $.parseJSON(result).toString();
            count = parseInt(obj);
        });
        $.ajaxSetup({ async: true });
        return count;
    },
    getwenwenfroanswer: function (userid, pagesize, pageno) {
        WenWenLinkProvider.getwenwenfroanswer(pagesize, pageno, function (result) {
            var obj = $.parseJSON(result);
            var objwenwen = obj.wenwenlist;
            var objwenwenanswers = obj.wenwenanswerlist
            var objinterest = obj.wenwenlistinterest;
            $("#wenwenul").html("");
            var str = "";
            $.each(objwenwen, function (i) {
                str += "<li class=\"greenbg clearfix\"><i class=\"q-icon\"></i><img src=\"" + photofunctions.geticonphotoname(objinterest[i].ICONPath) + "\" data-interestid=\"" + objinterest[i].ID + "\" class=\"fl mr15 wwimage interestimg\"/><div class=\"fl mr15 wwcontent\">" + objwenwen[i].Content + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + getTimeSpan(paserJsonDate(objwenwen[i].CreatedTime)) + "<br /><b id=\"" + objwenwen[i].ID + "answercount\" name=\"" + objwenwen[i].ID + "answercount\" class=\"mr15 fl\"><a onclick=\"WenWenControl.showanswer('" + userid + "','" + objwenwen[i].ID + "',5,1,false)\">" + (parseInt(objwenwenanswers[i]) > 0 ? "(" + objwenwenanswers[i] + "个回复)" : "(0个回复)") + "</a></b><i class=\"more-icon mt6 mr15\" onclick=\"WenWenControl.showanswer('" + userid + "','" + objwenwen[i].ID + "',5,1,false)\"></i><span class=\"showmanager\" style=\"display:none;\"><a class=\"my-answer fl mr15\" onclick=\"showhuida('" + userid + "','" + objwenwen[i].ID + "')\"><i></i>我来回复</a><a href=\"#\" class=\"my-enjoy fl mr15\"><i></i>转发考考微博好友</a></span></div><div id=\"" + objwenwen[i].ID + "text\" class=\"answer-frm mt18\"></div><ul id=\"" + objwenwen[i].ID + "\" class=\"for-inter-answer-list clearfix\" name=\"showwenwenanswermember\"></ul></li>";
            });
            $("#wenwenul").html(str);
            interestCenter.bindinterestLabel($("#wenwenul img.interestimg"));
            $("li.greenbg").mouseenter(function () {
                $(this).children("div.wwcontent").children("span.showmanager").css("display", "block");
            });
            $("li.greenbg").mouseleave(function () {
                $(this).children("div.wwcontent").children("span.showmanager").css("display", "none");
            });
        });
    },
    getwenwenfromembercount: function () {
        var count = 0;
        $.ajaxSetup({ async: false });
        WenWenLinkProvider.getwenwenfromembercount(function (result) {
            var obj = $.parseJSON(result).toString();
            count = parseInt(obj);
        });
        $.ajaxSetup({ async: true });
        return count;
    },
    getwenwenfromember: function (userid, pagesize, pageno) {
        WenWenLinkProvider.getwenwenfromember(pagesize, pageno, function (result) {
            var obj = $.parseJSON(result);
            var objwenwen = obj.wenwenlist;
            var objwenwenanswers = obj.wenwenanswerlist
            var objinterest = obj.wenwenlistinterest;
            var str = "";
            $("#wenwenul").html("");
            $.each(objwenwen, function (i) {
                str += "<li class=\"greenbg clearfix\"><i class=\"q-icon\"></i><img src=\"" + photofunctions.geticonphotoname(objinterest[i].ICONPath) + "\" data-interestid=\"" + objinterest[i].ID + "\" class=\"fl mr15 wwimage interestimg\"/><div class=\"fl mr15 wwcontent\">" + objwenwen[i].Content + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + getTimeSpan(paserJsonDate(objwenwen[i].CreatedTime)) + "<br /><b id=\"" + objwenwen[i].ID + "answercount\" name=\"" + objwenwen[i].ID + "answercount\" class=\"mr15 fl\"><a onclick=\"WenWenControl.showanswer('" + userid + "','" + objwenwen[i].ID + "',5,1,false)\">" + (parseInt(objwenwenanswers[i]) > 0 ? "(" + objwenwenanswers[i] + "个回复)" : "(0个回复)") + "</a></b><i class=\"more-icon mt6 mr15\" onclick=\"WenWenControl.showanswer('" + userid + "','" + objwenwen[i].ID + "',5,1,false)\"></i><span class=\"showmanager\" style=\"display:none;\"><a class=\"my-answer fl mr15\" onclick=\"showhuida('" + userid + "','" + objwenwen[i].ID + "')\"><i></i>我来回复</a><a href=\"#\" class=\"my-enjoy fl mr15\"><i></i>转发考考微博好友</a></span></div><div id=\"" + objwenwen[i].ID + "text\" class=\"answer-frm mt18\"></div><ul id=\"" + objwenwen[i].ID + "\" class=\"for-inter-answer-list clearfix\" name=\"showwenwenanswermember\"></ul></li>";
            });
            $("#wenwenul").html(str);
            interestCenter.bindinterestLabel($("#wenwenul img.interestimg"));
            $("li.greenbg").mouseenter(function () {
                $(this).children("div.wwcontent").children("span.showmanager").css("display", "block");
            });
            $("li.greenbg").mouseleave(function () {
                $(this).children("div.wwcontent").children("span.showmanager").css("display", "none");
            });
        });
    },
    showanswer: function (userid, wenwenid, pagesize, pageno, ifopen) {
        if (wenwenid != nowanserid && nowanserid.length > 2) {
            $("#" + nowanserid + "text").html("");
            $("#" + nowanserid).html("");
            $("#" + nowanserid).parent().css("backgroundColor", "");
        }
        if ($("#" + wenwenid + "text").html() != null && $.trim($("#" + wenwenid + "text").html()) != "")
            $("#" + wenwenid + "text").html("");
        WenWenLinkProvider.getwenwenanswer(wenwenid, pagesize, pageno, 1, function (result) {
            var obj = $.parseJSON(result);
            var answerlist = obj.answerlist;
            var nextpageno = obj.answernextpageno;
            var pagecount = obj.answerpagecount;
            var count = obj.answercount;
            if (answerlist.length > 0) {
                $("#" + wenwenid + "answercount").html("<a onclick=\"WenWenControl.showanswer('" + userid + "','" + wenwenid + "'," + pagesize + ",1,false)\">(" + count + "个回复)</a>");
                if (!ifopen) {
                    if ($.trim($("#" + wenwenid).html()) != "") {
                        $("#" + wenwenid).html("");
                        $("#" + wenwenid).parent().css("backgroundColor", "");
                    }
                    else {
                        var str = "";
                        $.each(answerlist, function (i) {
                            str += "<li class=\"clearfix\"><i class=\"a-icon\"></i><img src=\"" + photofunctions.geticonphotoname(answerlist[i].Creater.ICONPath) + "\" class=\"fl mr15 wenwenmimg\" data_me_id=\"" + userid + "\" data_member_id=\"" + answerlist[i].Creater.MemberID + "\" name=\"showwenwenanswermemberimg\"/><p>" + answerlist[i].Content + "</p><div class=\"ml60 c999\"><b class=\"mr52\" style=\"display:block;float:left;\">" + getTimeSpan(paserJsonDate(answerlist[i].CreatedTime)) + "</b><span class=\"showmanager\" style=\"display:none;float:left;\"><a href=\"#\" class=\"mr52 c999\">有用</a><a class=\"c999\" onclick='actionprovider.opencalladmin(\"<%=Model.MemberID%>\",1)'>举报</a></span></div></li>";
                        });
                        if (nextpageno <= pagecount) {
                            str += "<p class=\"tright\"><a onclick=\"WenWenControl.showanswer('" + userid + "','" + wenwenid + "'," + pagesize + "," + nextpageno + ",true)\">更多(" + (count - (nextpageno - 1) * pagesize) + ")</a></p>";
                        }
                        else {
                            str += "<p class=\"tright\"><a onclick=\"WenWenControl.showanswer('" + userid + "','" + wenwenid + "'," + pagesize + ",1,false)\">收起</a></p>";
                        }
                        $("#" + wenwenid).html(str);
                        $("#" + wenwenid).parent().css("backgroundColor", "#f7f7f7");
                        nowanserid = wenwenid;
                        MemberInfoCenter.BindDataInfo($("[name='showwenwenanswermember'] [name='showwenwenanswermemberimg']"));
                    }
                }
                else {
                    var str = "";
                    $.each(answerlist, function (i) {
                        str += "<li class=\"clearfix\"><i class=\"a-icon\"></i><img src=\"" + photofunctions.geticonphotoname(answerlist[i].Creater.ICONPath) + "\" class=\"fl mr15 wenwenmimg\" data_me_id=\"" + userid + "\" data_member_id=\"" + answerlist[i].Creater.MemberID + "\" name=\"showwenwenanswermemberimg\"/><p>" + answerlist[i].Content + "</p><div class=\"ml60 c999\"><b class=\"mr52\" style=\"display:block;float:left;\">" + getTimeSpan(paserJsonDate(answerlist[i].CreatedTime)) + "</b><span class=\"showmanager\" style=\"display:none;float:left;\"><a href=\"#\" class=\"mr52 c999\">有用</a><a class=\"c999\" onclick='actionprovider.opencalladmin(\"<%=Model.MemberID%>\",1)'>举报</a></span></div></li>";
                    });
                    if (nextpageno <= pagecount) {
                        str += "<p class=\"tright\"><a onclick=\"WenWenControl.showanswer('" + userid + "','" + wenwenid + "'," + pagesize + "," + nextpageno + ",true)\">更多(" + (count - (nextpageno - 1) * pagesize) + ")</a></p>";
                    }
                    else {
                        str += "<p class=\"tright\"><a onclick=\"WenWenControl.showanswer('" + userid + "','" + wenwenid + "'," + pagesize + ",1,false)\">收起</a></p>";
                    }
                    $("#" + wenwenid + " .tright").remove();
                    $("#" + wenwenid).html($("#" + wenwenid).html() + str);
                    $("#" + wenwenid).parent().css("backgroundColor", "#f7f7f7");
                    nowanserid = wenwenid;
                    MemberInfoCenter.BindDataInfo($("[name='showwenwenanswermember'] [name='showwenwenanswermemberimg']"));
                }
                $("li.clearfix").mouseenter(function () {
                    $(this).children("div.c999").children("span.showmanager").css("display", "block");
                });
                $("li.clearfix").mouseleave(function () {
                    $(this).children("div.c999").children("span.showmanager").css("display", "none");
                });
            }
        });
    },
    showrandomwenwen: function (meid) {
        WenWenLinkProvider.getrandonwenwen(function (result) {
            var obj = $.parseJSON(result);
            var wenwens = obj.wenwens;
            var str = "";
            $.each(wenwens, function (i) {
                str += "<li title=\"" + wenwens[i].Content + "\"><img class=\"infos\" data_me_id=\"" + meid + "\" data_member_id=\"" + wenwens[i].Creater.MemberID + "\"  src=\"" + photofunctions.geticonphotoname(wenwens[i].Creater.ICONPath) + "\" /><a target=\"_blank\" href=\"/WenWen/ShowWenWen?wwid=" + wenwens[i].ID + "\">" + (wenwens[i].Content.length > 20 ? wenwens[i].Content.toString().substr(0, 20) + "<span class=\"letspa--3\">...</span>" : wenwens[i].Content) + "</a></li>";
            });
            $("#ulqurestion").html(str);
            MemberInfoCenter.BindDataInfo($("#ulqurestion img.infos"));
        });
    },
    showanswerinterest: function (memberid) {
        var contentstr = "";
        $.ajaxSetup({
            async: false
        });
        WenWenLinkProvider.showanswerinterest(memberid, function (result) {
            var obj = $.parseJSON(result);
            var interests = obj.interests;
            var str = "<div style=\"float:left;color:#999;\">兴趣：</div>";
            $.each(interests, function (i) {
                str += "<div class=\"intitem\"><a href=\"/InterestCenter/InterestFans?iid=" + interests[i].ID + "\" target=\"_blank\"><img data-interestid=\"" + interests[i].ID + "\" src=\"" + photofunctions.geticonphotoname(interests[i].ICONPath) + "\" width=\"25\" height=\"25\"/></a></div>";
            });
            contentstr = str;
        });
        $.ajaxSetup({
            async: true
        });
        return contentstr;
    },
    likeclick: function (id) {
        WenWenLinkProvider.addMyLike(id, function (data) {
            var data = $.parseJSON(data);
            if (!data) {
                showjbox("你已经点过喜欢，不能重复点击！");
            }
            else {
                $("#like" + id).html(data.toString() + "&nbsp;♥");
            }
        });
    }
}

//信息相关方法
var msg_i_functions = {
    bindmsg: function (objs, you, isappend, container) {
        var str = "";
        var had = false;
        $.each(objs, function (i) {
            had = true;
            str += msg_i_functions.buildmsglist(objs[i], you);
        });
        if (had) {
            if (isappend)
                return str;
            else
                container.append(str);
        }
    },
    bindlastmsginmsger: function (me, you, objs, container) {
        var str = "";
        var had = false;
        var isfindyou = false;
        $.each(objs, function (i) {
            if (msg_i_functions.isfindyou(objs[i], you)) isfindyou = true;

            had = true;
            str += msg_i_functions.buildlastmsgersinmsger(objs[i], me, you);
        });

        //如果以前没有聊过，加入列表
        //        if (!isfindyou) {
        //            $.ajaxSetup({ async: false });
        //            MsgProvider.getLastMsgerTo(you, function (result) {
        //                str += msg_i_functions.buildlastmsgersinmsger($.parseJSON(result), me, you);
        //            });
        //            $.ajaxSetup({ async: true });
        //            return str;
        //        }
        //        else {
        if (had) {
            //container.html(str);
            return str;
        }
        else {
            //container.html("<ul class='listul'><li class='info'>还没有人聊过呢，去看看吧。<a href='/them/i'>传送门</a></li></ul>");
            return "<ul class='listul'><li class='info'>还没有人聊过呢，去看看吧。<a href='/them/i'>传送门</a></li></ul>";
        }
        //        }
    },
    bindlastmsg: function (me, objs, container) {
        var str = "";
        var had = false;
        $.each(objs, function (i) {
            had = true;
            if (objs[i].Comment.length > 20) {
                objs[i].Comment = objs[i].Comment.substring(0, 20) + "<span class=\"letspa--3\">...</span>";
            }
            str += msg_i_functions.buildlastmsglist(objs[i], me);
        });
        if (had)
            container.html(str);
        else
            container.html("<ul class='listul'><li class='info'>还没有人聊过呢，去看看吧。<a href='/them/i'>传送门</a></li></ul>");

        $(".mark").addClass(function (i) {
            return markfunctions.getcolor($(this).html())
        });
    },
    buildmsglist: function (obj, you) {
        var str = "";

        if (obj.ToMember == you)
            str += msg_i_functions.getmsgbanner(obj, myicon, myname);
        else
            str += msg_i_functions.getmsgbanner(obj, youricon, yourname);

        return str;
    },
    getmsgbanner: function (obj, icon, name) {
        var str = "";
        var isMe = false;
        //        str += "<li>";
        //        if (obj.ToMember == you) {
        //            str += "<div class='head50'><img src='" + icon + "'/></div>";
        //        }
        //        else {
        //            str += "<div class='head50'><a href='/Member/Ta/" + obj.FromMember + "' target='_blank'><img src='" + icon + "'/></a></div>";
        //        }
        //        str += "<div class='ml65'>";
        //        if (obj.ToMember == you) {
        //            str += "<p class='cblue'>" + name + "</p>";
        //        }
        //        else {
        //            str += "<p class='cblue'><a href='/Member/Ta/" + obj.FromMember + "' target='_blank'>" + name + "</a></p>";
        //        }
        //        var commentstr = "";
        //        if (obj.ToMember == you) {
        //            commentstr = SystemMsgBoxes.clearSystemMsgBox(obj.Comment);
        //        } else {
        //            commentstr = SystemMsgBoxes.getSystemMsgBox(obj.Comment);
        //        }
        //        str += "<p>" + commentstr + "</p>";
        //        str += "<p class='c999'>" + getTimeSpan(paserJsonDate(obj.CreatedTime)) + "</p>";
        //        str += "</div></li>";

        str += "<div class=\"Letter_info \">";
        str += "<div class=\"Letter_info_l\">";
        if (obj.ToMember == you) {
            str += "<a href=\"/Content/IContent/\" target=\"_blank\" title=\"" + myname + "\"><img src=\"" + icon + "\" height=\"64\" width=\"64\" alt=\"" + myname + "\" title=\"" + myname + "\" /></a>";
        }
        str += "</div>";
        if (obj.ToMember == you) {
            str += "<div class=\"Letter_info_c send02\">";
        }
        else {
            str += "<div class=\"Letter_info_c send03\">";
        }
        str += "<div class=\"ler_content\"><span class=\"top_c\">";
        if (obj.ToMember == you) {
            str += replaceToN(SystemMsgBoxes.clearSystemMsgBox(obj.Comment));
        } else {
            str += replaceToN(SystemMsgBoxes.getSystemMsgBox(obj.Comment));
        }
        str += "</span><span class=\"time_b\">" + paserJsonDate(obj.CreatedTime).format('yyyy-mm-dd HH:MM') + "</span></div>";
        if (obj.ToMember == you) {
            str += "<em class=\"gray_bg\"></em>";
        } else {
            str += "<em class=\"left_bg\"></em>";
        }
        str += "</div>";
        str += "<div class=\"Letter_info_r\">";
        if (obj.ToMember != you) {
            str += "<a href=\"/Content/TaContent/" + you + "\" target=\"_blank\" title=\"" + yourname + "\" id=\"activityMemberInfo\"><img src=\"" + icon + "\" height=\"64\" width=\"64\" data_me_id=\"" + me + "\" data_member_id=\"" + you + "\" name=\"activityMemberInfoArea\" /></a>";
        }
        str += "</div></div>";

        return str;
    },
    getleftmsgbanner: function (obj, maincss, canclick) {
        var str = "";
        if (canclick)
            str += "<div class='" + maincss + "' style='cursor:pointer;' onclick='actionprovider.openmsg(\"" + obj.FromMember + "\")'>";
        else
            str += "<div class='" + maincss + "'>";

        str += "<div class='timeleft'>" + getTimeSpan(paserJsonDate(obj.CreatedTime)) + "</div>";
        str += "<div class='l'>";
        str += "<div class='lt'><img src='/pics/spaceout.gif' width='15' height='15' class='f-img fs-msgbanner-lt-t1'/></div>";
        str += "<div class='lc f-img fs-msgbanner-lc-t1'></div>";
        str += "<div class='lb'><img src='/pics/spaceout.gif' width='15' height='16' class='f-img fs-msgbanner-lb-t1'/></div>";
        str += "</div>";
        str += "<div class='c'>";
        str += obj.Comment;
        str += "</div>";
        str += "<div class='r'>";
        str += "<div class='rt'><img src='/pics/spaceout.gif' width='8' height='15' class='f-img fs-msgbanner-rt-t1'/></div>";
        str += "<div class='rc'></div>";
        str += "<div class='rb'><img src='/pics/spaceout.gif' width='8' height='16' class='f-img fs-msgbanner-rb-t1'/></div>";
        str += "</div>";
        str += "</div><div class='clear'></div>";
        return str;
    },
    getrightmsgbanner: function (obj, maincss, canclick) {
        var str = "";
        if (canclick)
            str += "<div class='" + maincss + "' style='cursor:pointer;' onclick='actionprovider.openmsg(\"" + obj.ToMember + "\")'>";
        else
            str += "<div class='" + maincss + "'>";

        str += "<div class='l'>";
        str += "<div class='lt'><img src='/pics/spaceout.gif' width='8' height='15' class='f-img fs-msgbanner-lt-t2'/></div>";
        str += "<div class='lc '></div>";
        str += "<div class='lb'><img src='/pics/spaceout.gif' width='8' height='16' class='f-img fs-msgbanner-lb-t2'/></div>";
        str += "</div>";
        str += "<div class='c'>";
        str += obj.Comment;
        str += "</div>";
        str += "<div class='r'>";
        str += "<div class='rt'><img src='/pics/spaceout.gif' width='15' height='15' class='f-img fs-msgbanner-rt-t2'/></div>";
        str += "<div class='rc f-img fs-msgbanner-rc-t2'></div>";
        str += "<div class='rb'><img src='/pics/spaceout.gif' width='15' height='16' class='f-img fs-msgbanner-rb-t2'/></div>";
        str += "</div>";
        str += "<div class='timeright'>" + getTimeSpan(paserJsonDate(obj.CreatedTime)) + "</div>";
        str += "</div><div class='clear'></div>";

        return str;
    },
    //    buildlastmsgersinmsger: function (obj, me, you) {
    //        var str = "";
    //        var youid = msg_i_functions.getyouid(obj, me);
    //        var unreadcount = msg_i_functions.getunredcount(obj, youid);
    //        if (youid == you) {
    //            str += "<li class='current'>";
    //        }
    //        else {
    //            str += "<li onclick='msg_i_functions.changemsger(\"" + youid + "\")' onmouseover='$(this).addClass(\"current\");$(this).find(\"a[name=delicon]\").show();' onmouseout='$(this).removeClass(\"current\");$(this).find(\"a[name=delicon]\").hide();'>";
    //        }
    //        str += "<div class='w120 fl'><div class='fl mr15 infos' data_me_id=\"" + me + "\" data_member_id=\"" + youid + "\"><img width='25' height='25' src='" + obj.ICONPath + "'/></div>";
    //        str += "<p class='cblue'>" + obj.Name + "</p>";
    //        if (unreadcount > 0)
    //            str += "<span class='unreadcount'>+" + unreadcount + "</span>";
    //        //str += "<p><i></i></p>";
    //        str += "</div>";
    //        str += "<div class='fr'>";
    //        if (you != youid) {
    //            str += "<b class='cgray mr5'>" + getTimeSpan(paserJsonDate(obj.CreatedTime)) + "</b>";
    //        }
    //        if (youid == you) {
    //            str += "<i class='li-arrow-r fr mt8'></i>";
    //        }
    //        else {
    //            str += "<i class='li-arrow-r-space fr mt8'></i>";
    //        }
    //        str += "<br><a href='javascript:' name='delicon' class='clightgray mr5 mt5 fr' style='display:none' onclick='msg_i_functions.dellastmsger(\"" + youid + "\")'>移除</a>";
    //        str += "</div>";

    //        str += "</li>";
    //        return str;
    //    },
    buildlastmsgersinmsger: function (obj, me, you) {
        var str = "";
        var youid = msg_i_functions.getyouid(obj, me);
        var unreadcount = msg_i_functions.getunredcount(obj, youid);
        //        if (youid == you) {
        //            str += "<li class='current'>";
        //        }
        //        else {
        //            str += "<li onclick='msg_i_functions.changemsger(\"" + youid + "\")' onmouseover='$(this).addClass(\"current\");$(this).find(\"a[name=delicon]\").show();' onmouseout='$(this).removeClass(\"current\");$(this).find(\"a[name=delicon]\").hide();'>";
        //        }
        //        str += "<div class='w120 fl'><div class='fl mr15 infos' data_me_id=\"" + me + "\" data_member_id=\"" + youid + "\"><img width='25' height='25' src='" + obj.ICONPath + "'/></div>";
        //        str += "<p class='cblue'>" + obj.Name + "</p>";
        //        if (unreadcount > 0)
        //            str += "<span class='unreadcount'>+" + unreadcount + "</span>";
        //        //str += "<p><i></i></p>";
        //        str += "</div>";
        //        str += "<div class='fr'>";
        //        if (you != youid) {
        //            str += "<b class='cgray mr5'>" + getTimeSpan(paserJsonDate(obj.CreatedTime)) + "</b>";
        //        }
        //        if (youid == you) {
        //            str += "<i class='li-arrow-r fr mt8'></i>";
        //        }
        //        else {
        //            str += "<i class='li-arrow-r-space fr mt8'></i>";
        //        }
        //        str += "<br><a href='javascript:' name='delicon' class='clightgray mr5 mt5 fr' style='display:none' onclick='msg_i_functions.dellastmsger(\"" + youid + "\")'>移除</a>";
        //        str += "</div>";

        //        str += "</li>";

        var ID = obj.ID == null ? "javascript:;" : "/Content/TaContent/" + obj.ID;
        var Name = obj.Name == null ? "管理员" : obj.Name;
        var iconPath = obj.ICONPath == null ? "/pics/defultpic.png" : obj.ICONPath;

        str += "<div class=\"Letter_list\">";
        str += "<span class=\"let_close\">";
        if (obj.ID != null) {
            str += "<img src=\"/pics/Lertter_close.png\" title=\"删除\" onclick='msg_i_functions.dellastmsger(\"" + youid + "\")' style=\"cursor:pointer;\" />";
        }
        str += "</span>";
        str += "<dl class=\"clearfix\">";
        str += "<dt class=\"pic\">";
        if (obj.ID != null) {
            str += "<a href=\"" + ID + "\" target=\"_blank\" title=\"" + Name + "\" id=\"activityMemberInfo\">";
        }
        else {
            str += "<a href=\"javascript:;\" title=\"" + Name + "\" id=\"activityMemberInfo\">";
        }
        str += "<img src=\"" + iconPath + "\" height=\"64\" data_me_id=\"" + me + "\" data_member_id=\"" + obj.ID + "\" name=\"activityMemberInfoArea\" alt=\"" + Name + "\" /></a>";

        if (unreadcount > 0) {
            str += "<em>" + unreadcount + "</em>";
        }
        str += "</dt>";
        if (obj.ID != null) {
            str += "<div style=\"float:left; cursor:pointer; width:525px;\" onclick=\"window.location.href='/Msg/Messagedetails/" + youid + "'\">";
        }
        else {
            str += "<div style=\"float:left; width:525px;\">";
        }
        str += "<dd class=\"Lertxt\">与 " + Name + "</dd>";
        str += "<dd class=\"Lerlinks\">";
        if (obj.ID != null) {
            str += "<a href=\"/Msg/Messagedetails/" + youid + "\">";
        }
        else {
            str += "<a href=\"javascript:;\">";
        }
        //渲染用户的动作标签
        var msgbox = replaceToN(SystemMsgBoxes.clearSystemMsgBox(obj.Comment));

        if (obj.ID != null) {
            str += (msgbox.length > 95 ? msgbox.substring(0, 95) + "<span class=\"ellipsis\">...</span>" : msgbox) + "</a></dd>";
        }
        else {
            str += msgbox + "</a></dd>";
        }
        str += "<dd class=\"Lertxt\"><font class=\"lertime\">" + paserJsonDate(obj.CreatedTime).format('yyyy-mm-dd') + "</font>";
        var msgCount = 0;
        $.ajaxSetup({ async: false });
        MsgProvider.getMsgCount(youid, function (data) {
            msgCount = data;
        });
        $.ajaxSetup({ async: true });
        str += "<font class=\"Lercount\">共" + msgCount + "条对话</font>";
        str += "</dd></div></dl>";
        str += "</div>";

        return str;
    },
    changemsger: function (youid) {
        openurl("/msg/msgs/" + youid);
    },
    dellastmsger: function (youid) {
        var deleteMsgConfirm = function (v, h, f) {
            if (v == 'ok') {
                MsgProvider.delLastMsger(youid, function () {
                    //                    if (youid == you) {

                    var refreshYou = "";
                    $.ajaxSetup({ async: false });
                    MsgProvider.getLastMsgers(1, 1, function (data) {
                        var objs = $.parseJSON(data);

                        $.each(objs, function (i) {
                            refreshYou = objs[0].ToMember == me ? objs[0].FromMember : objs[0].ToMember;
                        });
                    });
                    $.ajaxSetup({ async: true });
                    var str = "";
                    if (pageNo < 2)
                        str += getLastMsgers(me, refreshYou, $("div#lastMsgers"), pageSize2, 1);
                    else {

                        var msgCount = 0;
                        $.ajaxSetup({ async: false });
                        MsgProvider.getLastMsageCount(function (data) {
                            msgCount = data;
                        });
                        $.ajaxSetup({ async: true });
                        var pCount = Math.ceil(msgCount / pageSize);
                        var pNo = pageNo;
                        if (pageNo > pCount) pNo = pCount;

                        str += getLastMsgers(me, refreshYou, $("div#lastMsgers"), pageSize, pNo);

                        $("div#paging").html(profileQueryStrPaging(pNo, pCount, pageUrl).toString());
                    }

                    $("div#lastMsgers").html(str);

                    //                    }
                    //                    else
                    //                        msg_i_functions.changemsger(you);
                });
            }
            else if (v == 'cancel') {
            }
            return true;
        };
        $.jBox.confirm("确认删除和Ta的所有聊天记录，并将Ta从常用聊天列表中删除？", "确认提示", deleteMsgConfirm);
    },
    isfindyou: function (obj, you) {
        if (obj.FromMember == you || obj.ToMember == you)
            return true;
        else
            return false;
    },
    getyouid: function (obj, me) {
        var youid;
        if (obj.FromMember == me)
            youid = obj.ToMember;
        else
            youid = obj.FromMember;
        return youid;
    },
    getunredcount: function (obj, youid) {
        if (obj.UnReads != null) {
            for (i = 0; i < obj.UnReads.length; i++) {
                if (obj.UnReads[i].SenderMid == youid)
                    return obj.UnReads[i].UnReadCount;
            }
        }
    },
    buildlastmsglist: function (obj, me) {
        var str = "";
        var youid = msg_i_functions.getyouid(obj, me);

        str += "<ul class='msgerpanelT2'>"
        str += "<li class='buttonsright'>";
        str += linkbuttons.T4(youid);
        str += profilemetastr.gethot(obj);
        str += "</li>";
        str += profilemetastr.geticon(obj, youid, uploadpath);
        str += profilemetastr.getinfomationT2(obj, youid, "infomation");
        str += "<li class='msgbanner'>";
        if (obj.ToMember == youid)
            str += msg_i_functions.getrightmsgbanner(obj, "msglistbannerright", true);
        else
            str += msg_i_functions.getleftmsgbanner(obj, "msglistbannerleft", true);
        str += "</li>";
        str += profilemetastr.getmsgcount(obj);
        str += profilemetastr.getunreadmsgcount(obj, youid);
        str += "</ul>";

        return str;
    }
}

/*———————————————————————————————————————————————————*/
/* 注册相关方法
/*∨∨∨∨∨——————————————————————————————————————————————*/

//加载注册时的兴趣
var registerBusinessProvider = {
    //    getinterestforclass: function (classid, pageno) {
    //        registerprovider.getRegAddInterestForClass(classid, pageno, function (data) {
    //            var obj = $.parseJSON(data);
    //            var interests = obj.interests;
    //            $("div.box_ppt ul#interestlist").attr("data-pagenonow", obj.pageno);
    //            //            $("div.findlist").html("");
    //            var str = "";
    //            $.each(interests, function (i) {
    //                //                str += "<div class=\"finditem\" id=\"itemdiv" + interests[i].ID + "\"><img class=\"image\" data-interestid=\"" + interests[i].ID + "\" src=\"" + photofunctions.getsmallphotoname(interests[i].ICONPath) + "\"/><div class=\"title\">" + interests[i].Title + "</div><ul class=\"manager\"><li class=\"but\" id=\"isFans" + interests[i].ID + "\"><a class=\"add-btn\" onclick=\"interestCenterFunctions.addInterestFans('" + interests[i].ID + "');clossthis('" + interests[i].ID + "');\"><i></i>关注</a></li></ul></div>";

    //                str += "<li data-ifopen=\"false\"><a href=\"javascript:;\" id=\"" + interests[i].ID + "\" onclick=\"interestCenterFunctions.addInterestFans('" + interests[i].ID + "')\"><img src=\"" + photofunctions.getsmallphotoname(interests[i].ICONPath) + "\" alt=\"" + interests[i].Title + "\" width=\"120\" height=\"120\" /></a><em></em><font>" + interests[i].Title + "</font></li>";
    //            });
    //            //            $("div.reginterestdiv").css("height", ((interests.length / 4 < 0) ? 1 : ((interests.length / 4) + 1)) * 80 + 300);
    //            //            $("div.findlist").css("height", ((interests.length / 4 < 0) ? 1 : ((interests.length / 4) + 1)) * 80)
    //            //            $("div.findlist").html(str);
    //            //            interestCenter.bindinterestLabel($("div.findlist img.image"));

    //            $("div.box_ppt ul#interestlist").html(str);
    //            $("div.box_ppt ul#interestlist li:eq(3)").after("<li><span>请选择你<br/>最喜欢的东东 <br/><br/><label><label id=\"currentSelectCount\">0</label>/3</label></span></li>");
    //            registerBusinessProvider.regAddInterestClickEffect();
    //            $("label#currentSelectCount").html(obj.pageno - 1);
    //        });
    //    },
    getRegRecomInterest: function (pageNo) {
        registerprovider.getRegRecomInterest(pageNo, function (data) {
            var interests = $.parseJSON(data);
            var str = "";
            $.each(interests, function (i) {
                str += "<li data-ifopen=\"false\" onclick=\"interestCenterFunctions.addInterestFans('" + interests[i].ID + "');shareInterest('" + interests[i].ID + "');\"><s ></s><a href=\"javascript:;\" id=\"" + interests[i].ID + "\"><img src=\"" + photofunctions.getprofileiconphotoname(interests[i].ICONPath) + "\" alt=\"" + interests[i].Title + "\" width=\"120\" height=\"120\" /></a><em><img src=\"/pics/hobby_check.png\" title=\"" + interests[i].Title + "\" alt=\"" + interests[i].Title + "\" /></em><font class=\"alpha50 Trans\">" + interests[i].Title + "</font></li>";
            });
            $("div.box_ppt ul#interestlist").html(str);
            $("div.box_ppt ul#interestlist li:eq(3)").after("<li id=\"currentSelect\"></li>");
            var regAddInterestTitle = "";
            switch (pageNo) {
                case "2": regAddInterestTitle = "接着，再选一个最贴切你的兴趣喜好的图标";
                    break;
                case "3": regAddInterestTitle = "最后一步啦，选一个最贴切你的愿望的图标";
                    break;
                default:
                    break;
            }
            $("div.box_ppt ul#interestlist li#currentSelect").html("<div style=\"display:none;\"><span><label id=\"regAddInterestTitle\">" + regAddInterestTitle + "</label><br/><b><label id=\"currentSelectCount\">0</label>/3</b></span></div>");
            $("div.box_ppt ul#interestlist li#currentSelect div").show();
            $("div.box_ppt ul#interestlist").attr("data-pagenonow", Number(pageNo) + 1);
            registerBusinessProvider.regAddInterestClickEffect();
            $("label#currentSelectCount").html(Number(pageNo) - 1);
        });
    },
    regAddInterestClickEffect: function () {
        $('div.demo3 ul li').each(function () {

            //鼠标悬浮事件
            $(this).hover(function () {
                if ($(this).attr("data-ifopen") == "false") {
                    $(this).find('em').css({ "visibility": "visible" });
                    // $(this).find('font').css({ "visibility": "visible" });
                    $(this).find('s').css({ "display": "block" });
                }
                //$(this).html('<em></em>');
                //$(this).html('<font>美食控</font>');
            }, function () {
                if ($(this).attr("data-ifopen") == "false") {
                    $(this).find('em').css({ "visibility": "hidden" });
                    //  $(this).find('font').css({ "visibility": "hidden" });
                    $(this).find('s').css({ "display": "none" });
                }
            });
            //鼠标点击事件
            $(this).click(function () {
                // var imgbox=$(this).find('a img').clone();
                //var smallpic=$('.small_pic img:last');
                //  $('.small_pic img').insertAfter(imgbox);
                var pic_list = $('div.small_pic').find('img');
                var imgstr = $(this).find('a').html();
                if (pic_list.length < 3) {
                    if ($(this).attr("data-ifopen") == "false") {
                        $(this).find('em').css({ "visibility": "visible" });
                        $(this).find('font').css({ "visibility": "visible" });
                        $(this).attr("data-ifopen", "true");
                        $(this).css({ opacity: 0.4 });
                        var currentSelectCount = $("label#currentSelectCount").html();

                        $("label#currentSelectCount").html(Number(currentSelectCount) + 1);
                    }
                    else {
                        $(this).find('em').css({ "visibility": "hidden" });
                        $(this).find('font').css({ "visibility": "hidden" });
                        $(this).attr("data-ifopen", "false");
                    }
                    $('.small_pic').html($('.small_pic').html() + imgstr);
                    $('.small_pic img:first').css({ heigt: 40, width: 40 });
                }
                else {
                    return false;
                }

                //$(this).find('em').css({ "visibility" : "visible"});
                //$(this).find('font').css({"visibility" : "visible"});

            });
            $("li#currentSelect").unbind("click");
        });
    },
    getinterestforlike: function (content) {
        registerprovider.getRegAddInterestForLike(content, function (data) {
            var obj = $.parseJSON(data);
            var interests = obj.interests;
            $("div.findlist").html("");
            var str = "";
            $.each(interests, function (i) {
                str += "<div class=\"finditem\" id=\"itemdiv" + interests[i].ID + "\"><img class=\"image\" data-interestid=\"" + interests[i].ID + "\" src=\"" + photofunctions.getsmallphotoname(interests[i].ICONPath) + "\"/><div class=\"title\">" + interests[i].Title + "</div><ul class=\"manager\"><li class=\"but\" id=\"isFans" + interests[i].ID + "\"><a class=\"add-btn\" onclick=\"interestCenterFunctions.addInterestFans('" + interests[i].ID + "');clossthis('" + interests[i].ID + "');\"><i></i>关注</a></li></ul></div>";
            });
            $("div.reginterestdiv").css("height", (interests.length / 4 < 0 ? 1 : (interests.length / 4) + 1) * 80 + 300);
            $("div.findlist").css("height", (interests.length / 4 < 0 ? 1 : (interests.length / 4) + 1) * 80)
            $("div.findlist").html(str);
            interestCenter.bindinterestLabel($("div.findlist img.image"));
        });
    }
}

/*———————————————————————————————————————————————————*/
/* 话题相关方法
/*∨∨∨∨∨——————————————————————————————————————————————*/

//加载话题中的图片
function getImageToTopic(imgids, showcount) {
    if (imgids != null) {
        var imgs = imgids.split(',');
        var imgstr = "";
        var indexcount = showcount < imgs.length - 1 ? showcount : imgs.length - 1;
        for (var j = 0; j < indexcount; j++) {
            imgstr += "<img src=\"" + photofunctions.getnormalphotoname(imgs[j]) + "\" title=\"" + imgs[j] + "\" onload=\"ImageZoom(this)\" width=\"10\" heidht=\"10\" />";
        }
        return imgstr;
    }
    else {
        return imgids;
    }
}
//加载话题中的图片固定宽
function getFixedWidthImageToTopic(imgids, showcount, fixedWidth) {
    if (imgids != null) {
        var imgs = imgids.split(',');
        var imgstr = "";
        var indexcount = showcount < imgs.length - 1 ? showcount : imgs.length - 1;
        for (var j = 0; j < indexcount; j++) {
            imgstr += "<img src=\"" + photofunctions.getnormalphotoname(imgs[j]) + "\" title=\"" + imgs[j] + "\" onload=\"FixedImageWidth(this, " + fixedWidth + ")\" width=\"10\" heidht=\"10\" />";
        }
        return imgstr;
    }
    else {
        return imgids;
    }
}
//加载话题图片缩放
function bindTopicImageZoom(imgdivobj) {
    imgdivobj.click(function () {
        if ($(this).attr("data-ifopen") == null || $(this).attr("data-ifopen") == "0") {
            var inparentobj = false;
            var inobj = false;
            $(this).attr("data-ifopen", "1");
            for (var i = 0; i < $(this).children("img").length; i++)
                ImageZoomToJqueryToHTML($(this).children("img")[i], 400);
            $(this).children("img").attr("title", "点击缩小");
            $(this).children("img").bind("mouseenter", function () {
                inobj = true;
                clearTopicImageEnlargeBu();
                $("body").append("<img id=\"twoclicktoshowtopicimage\" title=\"点击查看原图\" src=\"/pics/topicimageBigbu.png\" onclick=\"window.open('" + $(this).attr("src") + "')\"/>");
                var twoclicktoshowtopicimage = $("#twoclicktoshowtopicimage");
                twoclicktoshowtopicimage.css({ "left": $(this).offset().left + 5, "top": $(this).offset().top + $(this).height() - twoclicktoshowtopicimage.height() - 5 });
                twoclicktoshowtopicimage.bind("mouseenter", function () {
                    inparentobj = true;
                });
                twoclicktoshowtopicimage.bind("mouseleave", function () {
                    inparentobj = false;
                    var close = setTimeout(function () {
                        clearTimeout(close);
                        if (!inparentobj && !inobj)
                            clearTopicImageEnlargeBu();
                    }, 500);
                });
            });
            $(this).children("img").bind("mouseleave", function () {
                inobj = false;
                var close = setTimeout(function () {
                    clearTimeout(close);
                    if (!inparentobj && !inobj)
                        clearTopicImageEnlargeBu();
                }, 500);
            });
        }
        else {
            $(this).attr("data-ifopen", "0");
            for (var i = 0; i < $(this).children("img").length; i++)
                ImageZoomToJqueryToHTML($(this).children("img")[i], 105);
            $(this).children("img").attr("title", "点击放大");
            clearTopicImageEnlargeBu();
            $(this).children("img").unbind("mouseenter");
            $(this).children("img").unbind("mouseleave");
        }
    });
}
//清除话题图片放大的按钮
function clearTopicImageEnlargeBu() {
    var twoclicktoshowtopicimage = $("#twoclicktoshowtopicimage");
    if (twoclicktoshowtopicimage.html() != null) {
        twoclicktoshowtopicimage.remove();
        twoclicktoshowtopicimage.unbind("mouseenter");
        twoclicktoshowtopicimage.unbind("mouseleave");
    }
}

/*———————————————————————————————————————————————————*/
/* 设置相关方法
/*∨∨∨∨∨——————————————————————————————————————————————*/

// 内容页 左侧菜单鼠标效果
$(function () {
    $('.Set_menu ul li').hover(function () {

        //for(var j=1; j<=9;j++)
        //{
        //$(this).find('em.mo' + j).addClass('no' + j);
        //$(this).find('em.mo' + j).css( { opacity : 1});
        //}
    }, function () {
        //	$(this).removeClass('on');	
        for (var i = 1; i <= 9; i++) {
            $(this).find('em').removeClass('no' + i);
        }
    });
});

function SetSkinShowHide() {
    var $tab01 = $('.tabnav1');
    var $tab02 = $('.tabnav2');
    $('.tabbox2').hide();
    $tab01.click(function () {
        $('.tabbox1').hide();
        $('.tabbox2').show();
    });
    $tab02.click(function () {
        $('.tabbox1').show();
        $('.tabbox2').hide();
    });
}
