/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// Ajax或通信方法
/////////////////////////////////////////////////////////////////////////////
function ajaxPost(url, dataMap, callback) {
    var urlRndCode = urlAddRandomCode(url);
    $.post(
        urlRndCode,
        dataMap,
        callback
    );
}
function ajaxGet(url, dataMap, callback) {
    var urlRndCode = urlAddRandomCode(url);
    $.get(
        urlRndCode,
        dataMap,
        callback
    );
}
function urlAddRandomCode(url) {
    var rndCode = Math.random(new Date().getTime() / 1000);
    rndCode = rndCode.toString().substring(2, 7);
    if (url.indexOf('?') >= 0) {
        url += "&_rnd=" + rndCode;
    }
    else {
        url += "?_rnd=" + rndCode;
    }
    return url;
}



/////////////////////////////////////////////////////////////////////////////
// 数据访问封装
/////////////////////////////////////////////////////////////////////////////

//系统后台相关数据方法
var systemdataprovider = {
//    getsystemmarks: function (sex, callback) {
//        ajaxPost("/SystemFunc/GetSystemMarks", { sex: sex }, callback);
//    },
//    getsystemhi: function (type, callback) {
//        ajaxPost("/SystemFunc/GetSystemHi", { type: type }, callback);
//    },
//    getsystemskills: function (type, count, callback) {
//        ajaxPost("/SystemFunc/GetSystemSkills", { type: type, count: count }, callback);
//    },
    getsystemwants: function (type, count, callback) {
        ajaxPost("/SystemFunc/GetSystemWants", { type: type, count: count }, callback);
    },
    getsysteminterview: function (type, alreadyanswered, callback) {
        ajaxPost("/SystemFunc/GetSystemInterView", { type: type, alreadyanswered: alreadyanswered }, callback);
    },
    getprovince: function (callback) {
        ajaxPost("/SystemFunc/GetProvince", {}, callback);
    }
}

//注册相关数据方法
var registerprovider = {
    getauditingmember: function (mid, callback) {
        ajaxPost("/Register/GetAuditingMember", { mid: mid }, callback);
    },
    getRegAddInterestForClass: function (classid, nowpageno, callback) {
        ajaxPost("/Register/RegGetInterestForPage", { classid: classid, nowpageno: nowpageno }, callback);
    },
    getRegAddInterestForLike: function (content, callback) {
        ajaxPost("/Register/RegGetInterestForLike", { content: content }, callback);
    },
    getRegRecomInterest: function (pageNo, callback) {
        ajaxPost("/Register/GetRegRecomInterest", { pageNo: pageNo }, callback);
    },
    selectSex: function (sex, callback) {
        ajaxPost("/Register/SelectSex", { sex: sex }, callback);
    }
}

//用户相关数据方法
var memberprovider = {
    getmemberprofile: function (mid, callback) {
        ajaxPost("/member/getmemberprofile", { mid: mid }, callback);
    },
    setLatLng: function (hiddenmyloc, lat, lng, callback) {
        ajaxPost("/Setting/SetLatLng", { hiddenmyloc: hiddenmyloc, lat: lat, lng: lng }, callback);
    },
    seticonphoto: function (id, callback) {
        ajaxPost("/member/SetMemberIconPhoto", { id: id }, callback);
    },
    setiwant: function (iwant, callback) {
        ajaxPost("/member/SetIWant", { iwant: iwant }, callback);
    },
    setemail: function (pwd, email, callback) {
        ajaxPost("/setting/SetEmailProc", { pwd: pwd, email: email }, callback);
    },
    modifyEmail: function (email, callback) {
        ajaxPost("/Setting/ModifyEmail", { email: email }, callback);
    },
    modifyEmailPasswd: function (email, password, callback) {
        ajaxPost("/Setting/ModifyEmail", { email: email, password: password }, callback);
    },
    getfavormemberactivitys: function (mid, pagesize, pageno, callback) {
        ajaxPost("/Activity/GetFavorMemberActivitys", { mid: mid, pagesize: pagesize, pageno: pageno }, callback);
    },
    getinterviews: function (mid, pagesize, pageno, callback) {
        ajaxPost("/member/GetInterViews", { mid: mid, pagesize: pagesize, pageno: pageno }, callback);
    },
    addinterview: function (mid, systemQuestionID, question, answer, callback) {
        ajaxPost("/member/AddInterView", { mid: mid, systemQuestionID: systemQuestionID, question: question, answer: answer }, callback);
    },
    updateinterview: function (id, mid, answer, callback) {
        ajaxPost("/member/UpdateInterView", { id: id, mid: mid, answer: answer }, callback);
    },
    deleteinterview: function (id, mid, callback) {
        ajaxPost("/member/DeleteInterView", { id: id, mid: mid }, callback);
    },
    getprovinceknows: function (province, skillname, pagesize, pageno, callback) {
        ajaxPost("/member/GetProvinceKnows", { province: province, skillname: skillname, pagesize: pagesize, pageno: pageno }, callback);
    },
    getprovincewantlearns: function (province, skillname, pagesize, pageno, callback) {
        ajaxPost("/member/GetProvinceWantLearns", { province: province, skillname: skillname, pagesize: pagesize, pageno: pageno }, callback);
    },
    updateMemberContact: function (mobileNo, qq, callback) {
        ajaxPost("/Setting/UpdateMemberContact", { mobileNo: mobileNo, qq: qq }, callback);
    },
    updateFansGroupName: function (memberID, name, firstName, second, theThird, callback) {
        ajaxPost("/Setting/UpdateFansGroupName", { memberID: memberID, name: name, firstName: firstName, second: second, theThird: theThird }, callback);
    },
    getFansGroupName: function (memberID, callback) {
        ajaxPost("/Setting/GetFansGroupName", { memberID: memberID }, callback);
    },
    getMemberInfo: function (id, callback) {
        ajaxPost("/Member/getMemberInfo", { membId: id }, callback);
    },
    getMemberDistance: function (meid, heid, callback) {
        ajaxPost("/Member/GetWeDistance", { meId: meid, heId: heid }, callback);
    },
    getMemberIsInFavor: function (meid, heid, callback) {
        ajaxPost("/Member/IsInFavor", { meId: meid, taId: heid }, callback);
    },
    setWeiBoUser: function (userword, password, callback) {
        ajaxPost("/Push/setWeiBoUser", { userword: userword, password: password }, callback);
    },
    getPersonalityPicture: function (pageSize, pageNo, callback) {
        ajaxPost("/Setting/GetMemberSkins", { pageSize: pageSize, pageNo: pageNo }, callback);
    },
    getPersonalityPicture2: function (pageSize, pageNo, pictureType, callback) {
        ajaxPost("/Setting/GetMemberSkins2", { pageSize: pageSize, pageNo: pageNo, pictureType: pictureType }, callback);
    },
    setMemberSkin: function (memberSkinId, setType, callback) {
        ajaxPost("/Setting/SetMemberSkin", { memberSkinId: memberSkinId, setType: setType }, callback);
    },
    //采集米果
    setPointsScheduleZero: function (points, callback) {
        ajaxPost("/Member/setPointsScheduleZero", { points: points }, callback);
    }
}

//相册相关数据方法
var photoprovider = {
    getcomments: function (photoid, callback) {
        ajaxPost("/photo/getCommentList", { id: photoid }, callback);
    },
    addphotocomment: function (photoid, comment, callback) {
        ajaxPost("/photo/addComment", { id: photoid, content: comment }, callback);
    },
    delphotocomment: function (mid, photoid, createdtime, callback) {
        ajaxPost("/photo/delComment", { mid: mid, photoid: photoid, createdtime: createdtime }, callback);
    },
    updatephoto: function (photoid, title, content, callback) {
        ajaxPost("/photo/updatePhoto", { id: photoid, title: title, content: content }, callback());
    },
    batchUpdatePhotoInfo: function (photoIDs, photoTitles, photoContents, callback) {
        ajaxPost("/photo/BatchUpdatePhotoInfo", { photoIDs: photoIDs, photoTitles: photoTitles, photoContents: photoContents }, callback);
    },
    delphoto: function delPhoto(id, callback) {
        ajaxPost("/up/delphoto", { photoID: id }, callback);
    },
    getmemberphotolist: function (mid, type, pagesize, pageno, callback) {
        ajaxPost("/photo/memberphotolist", { mid: mid, phototype: type, pagesize: pagesize, pageno: pageno }, callback);
    },
    getphoto: function (id, callback) {
        ajaxPost("/photo/getPhoto", { id: id }, callback);
    },
    getnextphoto: function (id, callback) {
        ajaxPost("/photo/getNextPhoto", { id: id }, callback);
    },
    viewphoto: function (id, callback) {
        ajaxPost("/photo/viewPhoto", { id: id }, callback);
    },
    getmembersimplephotolist: function (mid, phototype, pagesize, pageno, callback) {
        ajaxPost("/photo/GetSimplePhotos", { mid: mid, phototype: phototype, pagesize: pagesize, pageno: pageno }, callback);
    },
    buildmembercover: function (mid, callback) {
        ajaxPost("/up/buildcoverimg", { mid: mid }, callback);
    }
}

//信息相关数据方法
var MsgProvider =
{
    delLastMsger: function (you, callback) {
        ajaxPost("/Msg/DeleteLastMsger", { you: you }, callback);
    },
    getLastMsgers: function (pagesize, pageno, callback) {
        ajaxPost("/Msg/GetLastMsgers", { pagesize: pagesize, pageno: pageno }, callback);
    },
    getPrivateAndSysMesges: function (pagesize, pageno, callback) {
        ajaxGet(
            "/Msg/GetPrivateAndSysMesges",
            { pagesize: pagesize, pageno: pageno },
            callback
        );
    },
    getLastMsgerTo: function (you, callback) {
        ajaxPost(
            "/Msg/GetLastMsgerTo",
            { you: you },
            callback
        );
    },
    getMsgs: function (you, pagesize, pageno, callback) {
        ajaxPost("/Msg/GetMsgs", { you: you, pagesize: pagesize, pageno: pageno }, callback);
    },
    getMsgCount: function (you, callback) {
        ajaxGet("/Msg/GetMessageCount", { you: you }, callback);
    },
    getLastMsageCount: function (callback) {
        ajaxPost("/Msg/GetLastMsageCount", {}, callback);
    },
    getAboutMeActivity: function (pageSize, pageNo, callback) {
        ajaxPost("/Msg/GetAboutMeActivity", { pageSize: pageSize, pageNo: pageNo }, callback);
    }
}

//邀请相关数据方法
var Invert = {
    photo: function (mid, container) {
        var link = '[url_t1]{"text":"去上传照片","url":"/Content/AddImageContent"}[/url_t1]';
        var comment = "多多上传照片，我看好你！" + link;
        MemberLinkProvider.morePhoto(mid, comment, function (result) {
            if (result.ok) {
                $.jBox.tip("你的邀请成功发送给TA了。", 'success');
                container.hide();
            }
        });
    },
    iWant: function (mid, container) {
        var link = '[url_t1]{"text":"去填写想做的事","url":"/Content/IContent"}[/url_t1]';
        var comment = "填写一下你想做的事嘛，我等着看哟！" + link;
        MemberLinkProvider.finishIWant(mid, comment, function (result) {
            if (result.ok) {
                $.jBox.tip("你的邀请成功发送给TA了。", 'success');
                container.hide();
            }
        });
    },
    profile: function (mid, container) {
        var link = '[url_t1]{"text":"去更新资料","url":"/Setting/PersonInfo"}[/url_t1]';
        var comment = "资料写详细点嘛，我等着看哟！" + link;
        MemberLinkProvider.finishProfile(mid, comment, function (result) {
            if (result.ok) {
                $.jBox.tip("你的邀请成功发送给TA了。", 'success');
                container.hide();
            }
        });
    },
    updateLocation: function (mid, container) {
        var link = '[url_t1]{"text":"去标注位置","url":"/Setting/SetLocation"}[/url_t1]';
        var comment = "快去标注你的位置吧，然后就能看到和其他人的距离啦！" + link;
        MemberLinkProvider.finishUpdateLocation(mid, comment, function (result) {
            if (result.ok) {
                $.jBox.tip("你的邀请成功发送给TA了。", 'success');
                container.hide();
            }
        });
    },
    interview: function (mid, container) {
        var link = '[url_t1]{"text":"去完成专访","url":"/Content/AddInterViewContent"}[/url_t1]';
        var comment = "多写点专访吧，我等着看哟！" + link;
        MemberLinkProvider.finishInterview(mid, comment, function (result) {
            if (result.ok) {
                $.jBox.tip("你的邀请成功发送给TA了。", 'success');
                container.hide();
            }
        });
    },
    uploadAvatar: function (mid, container) {
        var link = '[url_t1]{"text":"去上传头像","url":"/Setting/UploadFace"}[/url_t1]';
        var comment = "上传一下你的头像嘛，我等着看哟！" + link;
        MemberLinkProvider.InvertUploadAvatar(mid, comment, function (result) {
            if (result.ok) {
                $.jBox.tip("消息发送成功，等着好消息吧。", 'success');
                container.hide();
            }
        });
    },
    AddInterest: function (mid, container) {
        var link = '[url_t1]{"text":"去添加兴趣","url":"/InterestCenter/AddInterestFans"}[/url_t1]';
        var comment = "多多添加一些兴趣吧，我等着看哟！" + link;
        MemberLinkProvider.InvertAddInterest(mid, comment, function (result) {
            if (result.ok) {
                $.jBox.tip("消息发送成功，等着好消息吧。", 'success');
                container.hide();
            }
        });
    },
    Authentica: function (mid, container) {
        var link = '[url_t1]{"text":"去完成视频认证","url":"/Setting/Authentica"}[/url_t1]';
        var comment = "赶快去完成一下视频认证吧，我等着看哟！" + link;
        MemberLinkProvider.SettingAuthentica(mid, comment, function (result) {
            if (result.ok) {
                $.jBox.tip("消息发送成功，等着好消息吧。", 'success');
                //                container.hide();
            }
        });
    },
    Authenticas: function (mid, container) {
        var link = '[url_t1]{"text":"去完成视频认证","url":"/Setting/Authentica"}[/url_t1]';
        var comment = "赶快去完成一下视频认证吧，我等着看哟！" + link;
        MemberLinkProvider.SettingAuthentica(mid, comment, function (result) {
            if (result.ok) {
                $.jBox.tip("消息发送成功，等着好消息吧。", 'success');
                container.hide();
            }
        });
    }
}

//用户联系相关数据方法
var MemberLinkProvider =
{
    SettingAuthentica: function (mid, comment, callback) {
        ajaxPost("/Activity/MsgToMember", { toMember: mid, comment: comment, type: 223 }, callback);
    },
    InvertUploadAvatar: function (mid, comment, callback) {
        ajaxPost("/Activity/MsgToMember", { toMember: mid, comment: comment, type: 215 }, callback);
    },
    InvertAddInterest: function (mid, comment, callback) {
        ajaxPost("/Activity/MsgToMember", { toMember: mid, comment: comment, type: 302 }, callback);
    },
    morePhoto: function (mid, comment, callback) {
        ajaxPost("/Activity/MsgToMember", { toMember: mid, comment: comment, type: 210 }, callback);
    },
    finishProfile: function (mid, comment, callback) {
        ajaxPost("/Activity/MsgToMember", { toMember: mid, comment: comment, type: 212 }, callback);
    },
    finishInterview: function (mid, comment, callback) {
        ajaxPost("/Activity/MsgToMember", { toMember: mid, comment: comment, type: 213 }, callback);
    },
    finishUpdateLocation: function (mid, comment, callback) {
        ajaxPost("/Activity/MsgToMember", { toMember: mid, comment: comment, type: 216 }, callback);
    },
    finishIWant: function (mid, comment, callback) {
        ajaxPost("/Activity/MsgToMember", { toMember: mid, comment: comment, type: 223 }, callback);
    },
    visit: function (mid, callback) {
        ajaxPost("/Activity/VisitMember", { mid: mid }, callback);
    },
    interviewcomment: function (mid, comment, callback) {
        ajaxPost("/Activity/MsgToMember", { toMember: mid, comment: comment, type: 130 }, callback);
    },
    talk: function (mid, comment, callback) {
        ajaxPost("/Activity/MsgToMember", { toMember: mid, comment: comment, type: 130 }, callback);
    },
    date: function (you, comment, callback) {
        ajaxPost("/Activity/DateToMember", { toMember: you, comment: comment }, callback);
    },
    gift: function (mid, giftID, giftName, comment, callback) {
        ajaxPost("/Activity/AddGiftor", { mid: mid, giftID: giftID, giftName: giftName, comment: comment }, callback);
    },
    presentGlamourValue: function (toMember, glamourType, glamourValue, callback) {
        ajaxPost("/Activity/PresentGlamourValue", { toMember: toMember, glamourType: glamourType, glamourValue: glamourValue }, callback);
    },
    favor: function (mid, callback) {
        ajaxPost("/Activity/FavorMember", { toMember: mid }, callback);
    },
    delfavor: function (mid, callback) {
        ajaxPost("/Activity/DeleteFavorMember", { toMember: mid }, callback);
    },
    delMemberFavor: function (mid, callback) {
        ajaxPost("/Activity/DeleteMemberFavor", { toMember: mid }, callback);
    },
    updatefavorcomment: function (you, comment, callback) {
        ajaxPost("/Activity/UpdateFavorComment", { toMember: you, comment: comment }, callback);
    },
    disable: function (mid, callback) {
        ajaxPost("/Activity/DisableMember", { toMember: mid }, callback);
    },
    deldisable: function (mid, callback) {
        ajaxPost("/Activity/DeleteDisableMember", { toMember: mid }, callback);
    },
    silent: function (mid, callback) {
        ajaxPost("/Activity/SilentToMember", { mid: mid }, callback);
    },
    getvistors: function (mid, pagesize, pageno, callback) {
        ajaxPost("/Relation/GetVistors", { pagesize: pagesize, pageno: pageno }, callback);
    },
    getFavorers: function (mid, pagesize, pageno, callback) {
        ajaxPost("/Relation/GetFavorers", { pagesize: pagesize, pageno: pageno }, callback);
    },
    getFavoredList: function (pagesize, pageno, callback) {
        ajaxPost("/Relation/GetFavoredList", { pagesize: pagesize, pageno: pageno }, callback);
    },
    getFavoredCount: function (fromMember, callback) {
        ajaxPost("/Relation/GetFavoredCount", { fromMember: fromMember }, callback);
    },
    getListWhoFavoredMe: function (pagesize, pageno, callback) {
        ajaxPost("/Relation/GetListWhoFavoredMe", { pagesize: pagesize, pageno: pageno }, callback);
    },
    getGiftors: function (mid, pagesize, pageno, callback) {
        ajaxPost("/Relation/GetGiftors", { pagesize: pagesize, pageno: pageno }, callback);
    },
    getMySendedGifts: function (mid, pagesize, pageno, callback) {
        ajaxPost("/Relation/GetMySendedGifts", { pagesize: pagesize, pageno: pageno }, callback);
    },
    getSilentors: function (mid, pagesize, pageno, callback) {
        ajaxPost("/Relation/GetSilentors", { pagesize: pagesize, pageno: pageno }, callback);
    },
    getDisablers: function (mid, pagesize, pageno, callback) {
        ajaxPost("/Relation/GetDisablers", { pagesize: pagesize, pageno: pageno }, callback);
    },
    getNewMembersToMark: function (sex, pagesize, pageno, callback) {
        ajaxPost("/Relation/GetNewMembersToMark", { sex: sex, pagesize: pagesize, pageno: pageno }, callback);
    },
    getMemberUnReadCount: function (callback) {
        ajaxPost("/Activity/GetMemberUnReadCount", {}, callback);
    },
    getUnReadCounters: function (callback) {
        ajaxPost("/Activity/GetUnReadCounters", {}, callback);
    },
    getUnReadMsgCount: function (callback) {
        ajaxPost("/Activity/GetUnReadMsgCount", {}, callback);
    },
    getUnReadSystemMsgCount: function (callback) {
        ajaxPost("/Activity/GetUnReadSystemMsgCount", {}, callback);
    },
    getUnReadBeenFavorCount: function (userID, callback) {
        ajaxPost("/Activity/GetUnReadBeenFavorCount", { userID: userID }, callback);
    },
    getUnReadActivitysAboutMeCount: function (userID, callback) {
        ajaxPost("/Activity/GetUnReadActivitysAboutMeCount", {}, callback);
    },
    setNewpwd: function (oldpwd, newpwd, callback) {
        ajaxPost("/Setting/SetNewPwd", { oldpwd: oldpwd, newpwd: newpwd }, callback);
    },
    isOldPwdRight: function (oldpwd, callback) {
        ajaxPost("/Setting/IsOldPwdRight", { oldpwd: oldpwd }, callback);
    },
    setContact: function (qq, msn, tel, other, callback) {
        ajaxPost("/Setting/SetContactInfo", { qq: qq, msn: msn, tel: tel, other: other }, callback);
    },
    setAutoAddFavor: function (autoaddfavor, callback) {
        ajaxPost("/Setting/SetAutoAddFavor", { autoaddfavor: autoaddfavor }, callback);
    },
    setPrivacy: function (flagAutoAddToFavor, flagOnlySeniorMemberTS, flagOnlyVIPMemberTS, callback) {
        ajaxPost("/Setting/SetPrivacy", { flagAutoAddToFavor: flagAutoAddToFavor, flagOnlySeniorMemberTS: flagOnlySeniorMemberTS, flagOnlyVIPMemberTS: flagOnlyVIPMemberTS }, callback);
    },
    accessSet: function (isAllowAccessMe, callback) {
        ajaxPost("/Setting/AccessSet", { isAllowAccessMe: isAllowAccessMe }, callback);
    },
    GetOnlineStr: function (memberid, callback) {
        ajaxPost("/Member/GetOnlineStr", { memberid: memberid }, callback);
    }
}

//兴趣中心相关数据方法
var interestCenterProvider =
{
    addInterest: function (title, content, classes, iconid, selfhoodPictureId, selfhoodPicture, callback) {
        ajaxPost("/InterestCenter/AddMemberInterest", { title: title, content: content, classes: classes, iconid: iconid, selfhoodPictureId: selfhoodPictureId, selfhoodPicture: selfhoodPicture }, callback);
    },
    getAllInterestClasses: function (callback) {
        ajaxPost("/InterestCenter/GetAllInterestClass", callback);
    },
    addInterestFans: function (iID, callback) {
        ajaxPost("/InterestCenter/AddInterestFans", { iID: iID }, callback);
    },
    AddInterestFanss: function (iID, callback) {
        ajaxPost("/InterestCenter/AddInterestFanss", { iID: iID }, callback);
    },
    delInterestFans: function (iID, callback) {
        ajaxPost("/InterestCenter/DelInterestFans", { iID: iID }, callback);
    },
    delMemberInterestFans: function (memberId, interestId, callback) {
        ajaxPost("/InterestCenter/DelMemberInterestFans", { memberId: memberId, interestId: interestId }, callback);
    },
    delMemberInterestFansToMsg: function (username, interestname, mid, container) {
        var comment = "你以被“" + username + "”请出兴趣群组“" + interestname + "”！";
        MemberLinkProvider.InvertAddInterest(mid, comment, function (result) {
            if (result.ok) {
                $.jBox.tip("粉丝移除成功，系统将会以私信的形式通知对方。", 'success');
                container.hide();
            }
        });
    },
    getInterest: function (iID, callback) {
        ajaxPost("/InterestCenter/GetInterest", { iID: iID }, callback);
    },
    getInterestWenwen: function (id, callback) {
        ajaxPost("/InterestCenter/GetInterestWenWen", { id: id }, callback);
    },
    modifyInterest: function (interestId, title, content, classes, iconid, selfhoodPictureId, selfhoodPicture, callback) {
        ajaxPost("/InterestCenter/ModifyMemberInterest", { interestId: interestId, title: title, content: content, classes: classes, iconid: iconid, selfhoodPictureId: selfhoodPictureId, selfhoodPicture: selfhoodPicture }, callback);
    },
    getMemberInterest: function (iId, callback) {
        ajaxPost("/InterestCenter/GetMemberInterest", { iId: iId }, callback);
    },
    isFans: function (iID, callback) {
        ajaxPost("/InterestCenter/IsFans", { iID: iID }, callback);
    },
    UpdateInterestFansAjax: function (interestid, pagesize, pageno, callback) {
        ajaxPost("/InterestCenter/UpdateInterestFansAjax", { interestid: interestid, pagesize: pagesize, pageno: pageno }, callback);
    },
//    UpdateInterestFanssAjax: function (interestid, pagesize, pageno, callback) {
//        ajaxPost("/InterestCenter/UpdateInterestFanssAjax", { interestid: interestid, pagesize: pagesize, pageno: pageno }, callback);
//    },
    getMyinterest: function (callback) {
        ajaxPost("/InterestCenter/getMyinterest", {}, callback);
    }
}

//微博连接相关方法
var MicroConn =
{
    setConnectorEnableStatusFalse: function (platformType, callback) {
        ajaxPost("/MicroConn/SaveConnectorEnableStatusFalse", { platformType: platformType }, callback);
    },
    sendInfo: function (platformType, content, PicPath, Url, callback) {
        ajaxPost("/MicroConn/SendInfo", { platformType: platformType, Content: content, PicPath: PicPath, Url: Url }, callback);
    }
}

//呼叫系统管理员相关数据方法
var CallAdmin =
{
    call: function (mid, type, content, callback) {
        ajaxPost("/admin/connectAdmin", { mid: mid, type: type, content: content }, callback);
    }
}

//问问相关数据方法
var WenWenLinkProvider = {
    addwenwenanswer: function (interestid, wenwenid, content, upordown, callback) {
        ajaxPost("/WenWen/InsertWenWenAnswer", { interestid: interestid, wenwenid: wenwenid, content: content, upordown: upordown }, callback);
    },
    addMyLike: function (id, callback) {
        ajaxPost("/WenWen/AddMyLike", { id: id }, callback);
    },
    memberLikeInterestTopics: function (topicId, callback) {
        ajaxPost("/WenWen/MemberLikeInterestTopics", { topicId: topicId }, callback);
    },
    updatewenwenanswer: function (id, content, callback) {
        ajaxPost("/WenWen/UpdateWenWenAnswer", { id: id, content: content }, callback);
    },
    getWenWen: function (pageno, id, callback) {
        ajaxPost("/InterestCenter/GetWenWen", { pageno: pageno, id: id }, callback);
    },
    getnextwenwen: function (page, callback) {
        ajaxPost("/WenWen/GetNextWenWen", { page: page }, callback);
    },
    getwenwenanswer: function (wenwenid, pagesize, pageno, createdtimeorder, callback) {
        ajaxPost("/WenWen/GetWenWenAnswer", { wenwenid: wenwenid, pagesize: pagesize, pageno: pageno, createdtimeorder: createdtimeorder }, callback);
    },
    getrandonwenwen: function (callback) {
        ajaxPost("/Push/GetQustion", {}, callback);
    },
    getwenwenfroanswercount: function (callback) {
        ajaxPost("/WenWen/GetWenWenAnswerForMemberCount", {}, callback);
    },
    getwenwenfroanswer: function (pagesize, pageno, callback) {
        ajaxPost("/WenWen/GetWenWenAnswerForMember", { pagesize: pagesize, pageno: pageno }, callback);
    },
    getwenwenfromembercount: function (callback) {
        ajaxPost("/WenWen/GetWenWenForMemberCount", {}, callback);
    },
    getwenwenfromember: function (pagesize, pageno, callback) {
        ajaxPost("/WenWen/GetWenWenForMember", { pagesize: pagesize, pageno: pageno }, callback);
    },
    getwenwenformyinterest: function (pageno, type, callback) {
        ajaxPost("/InterestCenter/getinterestformy", { pageno: pageno, type: type }, callback);
    },
    showanswerinterest: function (memberid, callback) {
        ajaxPost("/WenWen/GetAnswerInterest", { memberid: memberid }, callback);
    },
    getwenwenforinterest: function (pageno, topictoboy, topictogirl, callback) {
        ajaxPost("/Member/getmyinterest", { pageno: pageno, topictoboy: topictoboy, topictogirl: topictogirl }, callback);
    },
    getWWFilterStatus: function (id, callback) {
        ajaxPost("/WenWen/GetWWFilterStatus", { id: id }, callback);
    },
    getInterestIdTopic: function (interestId, pageSize, pageNo, callback) {
        ajaxPost("/WenWen/GetInterestIdTopic", { interestId: interestId, pageSize: pageSize, pageNo: pageNo }, callback);
    },
    getBriefMemberIdTopics: function (briefMemberId, pageSize, pageNo, callback) {
        ajaxPost("/WenWen/GetBriefMemberIdTopics", { briefMemberId: briefMemberId, pageSize: pageSize, pageNo: pageNo }, callback);
    },
    getBriefMemberIdTopicsCount: function (briefMemberId, callback) {
        ajaxPost("/WenWen/GetBriefMemberIdTopicsCount", { briefMemberId: briefMemberId }, callback);
    },
    getTopicCount: function (memberId, callback) {
        ajaxPost("/WenWen/GetTopicCount", { memberId: memberId }, callback);
    },
    getAnswerFilterStatus: function (id, callback) {
        ajaxPost("/WenWen/GetAnswerFilterStatus", { id: id }, callback);
    },
    getFeaturedInterestTopic: function (publishedTopicSex, pageSize, pageNo, callBack) {
        ajaxPost("/WenWen/GetFeaturedInterestTopic", { publishedTopicSex: publishedTopicSex, pageSize: pageSize, pageNo: pageNo }, callBack);
    },
    getTypesetFeaturedInterestTopic: function (publishedTopicSex, pageSize, pageNo, callBack) {
        ajaxPost("/WenWen/GetTypesetFeaturedInterestTopic", { publishedTopicSex: publishedTopicSex, pageSize: pageSize, pageNo: pageNo }, callBack);
    }
}

//我的活动相关数据方法
var MyActivesProvider = {
    AddGroove: function (content, callback) {
        ajaxPost("/Actives/AddGroove", { content: content }, callback);
    },
    GetGroove: function (pagesize, pageno, callback) {
        ajaxPost("/Actives/GetGroove", { pagesize: pagesize, pageno: pageno }, callback);
    },
    GetGrooveCount: function (callback) {
        ajaxPost("/Actives/GetGrooveCount", {}, callback);
    }
}

//活动相关数据方法
var ActivesProvider = {
    geteverydaysuper: function (pageno, callback) {
        ajaxPost("/Actives/ShowEverydaySuper", { pageno: pageno }, callback);
    },
    getnowsuper: function (pageno, callback) {
        ajaxPost("/Actives/ShowNowSuper", { pageno: pageno }, callback);
    },
    addactive: function (dllname, spacename, functionname, type, enable, callback) {
        ajaxPost("/Admin/AddActives", { dllname: dllname, spacename: spacename, functionname: functionname, type: type, enable: enable }, callback);
    },
    delactive: function (id, callback) {
        ajaxPost("/Admin/DelActives", { id: id }, callback);
    },
    upactive: function (id, enable, callback) {
        ajaxPost("/Admin/UpActives", { id: id, enable: enable }, callback);
    }
}

//用户信息异步验证相关数据方法
var UserInfo = {
    CheckUserNick: function (nickname, callback) {
        ajaxPost("/Register/CheckUserNick", { nickname: nickname }, callback);
    }
}

//内容
var ContentProvider = {
    getLastMemberContent: function (toObjectID, type, callback) {
        ajaxPost("/Content/getLastMemberContent", { toObjectID: toObjectID, type: type }, callback);
    },
    getContentToType: function (contenttype, pagesize, pageno, callback) {
        ajaxPost("/Content/getContentToType", { contenttype: contenttype, pagesize: pagesize, pageno: pageno }, callback);
    },
    MyContentToAjax: function (pageno, contenttype, callback) {
        ajaxPost("/Content/IContentToAjax", { pageno: pageno, contenttype: contenttype }, callback);
    },
    MyFavorerContentToAjax: function (pageno, contenttype, callback) {
        ajaxPost("/Content/IFavorerContentToAjax", { pageno: pageno, contenttype: contenttype }, callback);
    },
    TaContentToAjax: function (pageno, contenttype, memberID, callback) {
        ajaxPost("/Content/TaContentToAjax", { pageno: pageno, contenttype: contenttype, memberID: memberID }, callback);
    },
    IndexContentToAjax: function (pageno, interestID, city, sex, callback) {
        ajaxPost("/Content/IndexContentToAjax", { pageno: pageno, interestID: interestID, city: city, sex: sex }, callback);
    },
    reloadInterView: function (callback) {
        ajaxPost("/Content/reloadInterView", {}, callback);
    },
    InsertImageContent: function (permissions, interestids, Content, imageIDs, layOutType, type, contentid, callback) {
        ajaxPost("/Content/InsertImageContent", { permissions: permissions, interestids: interestids, Content: Content, imageIDs: imageIDs, layOutType: layOutType, type: type, contentid: contentid }, callback);
    },
    InsertSuiSuiNianContent: function (permissions, interestids, Content, imageIDs, layOutType, type, contentid, callback) {
        ajaxPost("/Content/InsertSuiSuiNianContent", { permissions: permissions, interestids: interestids, Content: Content, imageIDs: imageIDs, layOutType: layOutType, type: type, contentid: contentid }, callback);
    },
    InsertInterViewContent: function (permissions, interestids, interviewid, ifdelete, callback) {
        ajaxPost("/Content/InsertInterViewContent", { permissions: permissions, interestids: interestids, interviewid: interviewid, ifdelete: ifdelete }, callback);
    },
    InsertCallForContent: function (permissions, interestids, Content, imageIDs, layOutType, type, contentid, callback) {
        ajaxPost("/Content/InsertCallForContent", { permissions: permissions, interestids: interestids, Content: Content, imageIDs: imageIDs, layOutType: layOutType, type: type, contentid: contentid }, callback);
    },
    InsertInterestContent: function (permissions, interestids, content, interstid, type, callback) {
        ajaxPost("/Content/InsertInterestContent", { permissions: permissions, interestids: interestids, content: content, interstid: interstid, type: type }, callback);
    },
    InsertMemberContent: function (permissions, interestids, lat, lng, type, callback) {
        ajaxPost("/Content/InsertMemberContent", { permissions: permissions, interestids: interestids, lat: lat, lng: lng, type: type }, callback);
    },
    DeleteContent: function (contentid, callback) {
        ajaxPost("/Content/DeleteContent", { contentid: contentid }, callback);
    },
    AddContentLike: function (contentID, likeContentType, callback) {
        ajaxPost("/Content/AddContentLike", { contentID: contentID, likeContentType: likeContentType }, callback);
    },
    ShowNewMember: function (interestID, createdTime, callback) {
        ajaxPost("/Content/ShowNewMember", { interestID: interestID, createdTime: createdTime }, callback);
    }
}
//回复
var CommentProvider = {
    ShowComment: function (contentID, pagesize, pageno, callback) {
        ajaxPost("/Content/ShowComment", { contentID: contentID, pagesize: pagesize, pageno: pageno }, callback);
    },
    getMemberLabelComment: function (contentID, memberLabel, callback) {
        ajaxPost("/Content/GetMemberLabelComment", { contentID: contentID, memberLabel: memberLabel }, callback);
    },
    AddComment: function (contentID, content, toMember, commentId, callback) {
        ajaxPost("/Content/AddComment", { contentID: contentID, content: content, toMember: toMember, commentId: commentId }, callback);
    },
    addMemberLabelComment: function (contentID, content, memberLabel, callback) {
        ajaxPost("/Content/AddMemberLabelComment", { contentID: contentID, content: content, memberLabel: memberLabel }, callback);
    }
//    ShowInterViewComment: function (interviewID, pageno, callback) {
//        ajaxPost("/Content/ShowInterViewComment", { interviewID: interviewID, pageno: pageno }, callback);
//    },
//    AddInterViewComment: function (interviewID, contentID, content, callback) {
//        ajaxPost("/Content/AddInterViewComment", { interviewID: interviewID, contentID: contentID, content: content }, callback);
//    }
}
//顶部推送内容计数器的更新
var ImagePushProvider = {
    UpdatePushShowCount: function (id, callback) {
        ajaxPost("/Content/UpdatePushShowCount", { id: id }, callback);
    },
    UpdatePushClickCount: function (id, callback) {
        ajaxPost("/Content/UpdatePushClickCount", { id: id }, callback);
    }
}

//通用数据方法
var commonProvider = {
    savePathPhotoGetUploadedPath: function (photoNamePath, memberID, callBack) {
        ajaxPost("/Shared/SavePathPhotoGetUploadedPath", { photoNamePath: photoNamePath, memberID: memberID }, callBack);
    }
}
