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





//后台管理相关方法
var AdminProvider =
{
    GetAllSystemWants: function (callback) {
        ajaxPost("/SystemFunc/GetAllSystemWants", {}, callback);
    },
    AddSystemWant: function (type, isaudited, witter, iwantstr, content, callback) {
        ajaxPost("/SystemFunc/AddSystemWants", { type: type, isaudited: isaudited, witter: witter, iwantstr: iwantstr, content: content }, callback);
    },
    GetSystemManager: function (managerId, callback) {
        ajaxPost("/SystemFunc/GetSystemManager", { managerId: managerId }, callback);
    },
    UpdateSystemManager: function (id, name, pwd, level, allowlogin, callback) {
        ajaxPost("/SystemFunc/UpdateSystemManager", { id: id, name: name, pwd: pwd, level: level, allowlogin: allowlogin }, callback);
    },
    DelSystemWant: function (id, callback) {
        ajaxPost("/SystemFunc/DelSystemWants", { id: id }, callback);
    },
    AddSystemFilterWord: function (wordname, word_is_enable, wordtype, callback) {
        ajaxPost("/SystemFunc/AddSystemWord", { wordname: wordname, word_is_enable: word_is_enable, wordtype: wordtype }, callback);
    },
    GetAllSystemFilterWord: function (type, callback) {
        ajaxPost("/SystemFunc/GetAllSystemFilterWord", { type: type }, callback);
    },
    DisableFilterWord: function (idList, callback) {
        ajaxPost("/SystemFunc/DisableFilterWords", { idlist: idList }, callback);
    },
    EnableFilterWord: function (idList, callback) {
        ajaxPost("/SystemFunc/EnableFilterWords", { idlist: idList }, callback);
    },
    DeleteFilterWord: function (idList, callback) {
        ajaxPost("/SystemFunc/DeleteFilterWords", { idlist: idList }, callback);
    },
    UploadFilterWord: function (wfpath, type, callback) {
        ajaxPost("/SystemFunc/UploadFilterWords", { path: wfpath, type: type }, callback);
    },
    GetAllVerifyText: function (type, pageno, pagesize, callback) {
        ajaxPost("/SystemFunc/GetFilterText", { verifyStatus: type, pageno: pageno, pagesize: pagesize }, callback);
    },
    UpdateFilterText: function (id, verifyStatus, adminid, text, callback) {
        ajaxPost("/SystemFunc/UpdateFilterText", { id: id, verifyStatus: verifyStatus, adminid: adminid, text: text }, callback);
    },
    UpdateFilterTexts: function (idlist, verifyStatus, adminid, texts, callback) {
        ajaxPost("/SystemFunc/UpdateFilterTexts", { idlist: idlist, verifyStatus: verifyStatus, adminid: adminid, texts: texts }, callback);
    },
    RemoveFilterText: function (id, callback) {
        ajaxPost("/SystemFunc/DeleteFilterText", { id: id }, callback);
    },
    RemoveFilterTexts: function (idList, callback) {
        ajaxPost("/SystemFunc/DeleteFilterTexts", { idList: idList }, callback);
    },
    GetFilterTextcont: function (type, callback) {
        ajaxPost("/SystemFunc/GetFilterTextCount", { type: type }, callback);
    },
    GetAllCheckPhoto: function (type, pageno, pagesize, callback) {
        ajaxPost("/SystemFunc/GetCheckPhotos", { type: type, pageno: pageno, pagesize: pagesize }, callback);
    },
    GetCheckPhotocount: function (type, callback) {
        ajaxPost("/SystemFunc/GetCheckPhotocount", { type: type }, callback);
    },
    UpdateCheckPhotos: function (idlis, type, adminid, useridlist, callback) {
        ajaxPost("/SystemFunc/UpdateCheckPhotos", { idlis: idlis, type: type, adminid: adminid, useridlist: useridlist }, callback);
    },
    UpdateUserPhotoisReal: function (type, useridlist, callback) {
        ajaxPost("/SystemFunc/UpdateUserPhotoisReal", { type: type, useridlist: useridlist }, callback);
    },
    UpdateCheckPhoto: function (id, type, adminid, userid, callback) {
        ajaxPost("/SystemFunc/UpdateCheckPhoto", { id: id, type: type, adminid: adminid, userid: userid }, callback);
    },
    RemoveCheckPhotos: function (idlist, imglist, callback) {
        ajaxPost("/SystemFunc/RemoveCheckPhotos", { idlist: idlist, imglist: imglist }, callback);
    },
    UpdateAllowLogin: function (ids, type, callback) {
        ajaxPost("/SystemFunc/UpdateAllowLogin", { ids: ids, type: type }, callback);
    },
    GetAllAccount: function (type, usersel, ssel, scontent, pageno, pagesize, callback) {
        ajaxPost("/SystemFunc/GetAllAccount", { type: type, usersel: usersel, ssel: ssel, scontent: scontent, pageno: pageno, pagesize: pagesize }, callback);
    },
    GetAllAcountCount: function (type, usersel, ssel, scontent, callback) {
        ajaxPost("/SystemFunc/GetAllAcountCount", { type: type, usersel: usersel, ssel: ssel, scontent: scontent }, callback);
    },
    GetAllSystemInterView: function (callback) {
        ajaxPost("/SystemFunc/GetAllSystemInterView", {}, callback);
    },
    AddSystemInterView: function (type, isaudited, witter, question, answer, callback) {
        ajaxPost("/SystemFunc/AddSystemInterView", { type: type, isaudited: isaudited, witter: witter, question: question, answer: answer }, callback);
    },
    DelSystemInterView: function (id, callback) {
        ajaxPost("/SystemFunc/DelSystemInterView", { id: id }, callback);
    },
//    GetAllSystemHi: function (callback) {
//        ajaxPost("/SystemFunc/GetAllSystemHi", {}, callback);
//    },
//    AddSystemHi: function (type, isaudited, witter, comment, callback) {
//        ajaxPost("/SystemFunc/AddSystemHi", { type: type, isaudited: isaudited, witter: witter, comment: comment }, callback);
//    },
//    DelSystemHi: function (id, callback) {
//        ajaxPost("/SystemFunc/DelSystemHi", { id: id }, callback);
//    },
//    GetAllSystemMarks: function (callback) {
//        ajaxPost("/SystemFunc/GetAllSystemMarks", {}, callback);
//    },
//    AddSystemMark: function (sex, isaudited, witter, content, contentsend, contentcove, callback) {
//        ajaxPost("/SystemFunc/AddSystemMark", { sex: sex, isaudited: isaudited, witter: witter, content: content, contentsend: contentsend, contentcove: contentcove }, callback);
//    },
//    DelSystemMark: function (id, callback) {
//        ajaxPost("/SystemFunc/DelSystemMark", { id: id }, callback);
//    },
//    GetAllSystemSkills: function (callback) {
//        ajaxPost("/SystemFunc/GetAllSystemSkills", {}, callback);
//    },
//    AddSystemSkill: function (type, isaudited, witter, skillname, contentsend, callback) {
//        ajaxPost("/SystemFunc/AddSystemSkill", { type: type, isaudited: isaudited, witter: witter, skillname: skillname, contentsend: contentsend }, callback);
//    },
//    DelSystemSkill: function (id, callback) {
//        ajaxPost("/SystemFunc/DelSystemSkill", { id: id }, callback);
//    },
//    GetAllSystemBBMs: function (callback) {
//        ajaxPost("/SystemFunc/GetAllSystemBBMs", {}, callback);
//    },
//    AddSystemBBM: function (content, contentsend, iconpath, callback) {
//        ajaxPost("/SystemFunc/AddSystemBBM", { content: content, contentsend: contentsend, iconpath: iconpath }, callback);
//    },
//    DelSystemBBM: function (id, callback) {
//        ajaxPost("/SystemFunc/DelSystemBBM", { id: id }, callback);
//    },
    AddInterestClass: function (title, icon, order, callback) {
        ajaxPost("/SystemFunc/AddInterestClass", { title: title, icon: icon, order: order }, callback);
    },
    GetAllInterestClass: function (callback) {
        ajaxPost("/SystemFunc/GetAllInterestClass", callback);
    },
    DelInterestClass: function (id, callback) {
        ajaxPost("/SystemFunc/DelInterestClass", { id: id }, callback);
    },
    getauditnewusers: function (pagesize, pageno, callback) {
        ajaxPost("/Admin/getauditnewusers", { pagesize: pagesize, pageno: pageno }, callback);
    },
    setmembersauditresult: function (mids, auditresult, marks, score, sex, callback) {
        ajaxPost("/Admin/setmembersauditresult", { mids: mids, auditresult: auditresult, marks: marks, score: score, sex: sex }, callback);
    },
    getJb: function (isaudited, pagesize, pageno, callback) {
        ajaxPost("/Admin/getSystemCall", { type: "jb", isAudited: isaudited, pagesize: pagesize, pageno: pageno }, callback);
    },
    getJy: function (isaudited, pagesize, pageno, callback) {
        ajaxPost("/Admin/getSystemCall", { type: "jy", isAudited: isaudited, pagesize: pagesize, pageno: pageno }, callback);
    },
    getYj: function (isaudited, pagesize, pageno, callback) {
        ajaxPost("/Admin/getSystemCall", { type: "yj", isAudited: isaudited, pagesize: pagesize, pageno: pageno }, callback);
    },
    setYj: function (id,uid, result, resulttxt, originaltext, callback) {
        ajaxPost("/Admin/setSystemCall", { id: id,uid:uid, result: result, resulttxt: resulttxt, originaltext: originaltext }, callback);
    },
    getCallCount: function (type, isaudited, callback) {
        ajaxPost("/Admin/GetCallCount", { type: type, isaudited: isaudited }, callback);
    },
    setAllowLogin: function (mid, allowlogin, callback) {
        ajaxPost("/Admin/setallowlogin", { mid: mid, allowlogin: allowlogin }, callback);
    },
    setmembertype: function (mid, membertype, callback) {
        ajaxPost("/Admin/setmembertype", { mid: mid, membertype: membertype }, callback);
    },
    getapplication: function (callback) {
        ajaxPost("/SystemFunc/GetApplication", {}, callback);
    },
    addapplication: function (imgpath, des, linkurl, callback) {
        ajaxPost("/SystemFunc/AddApplication", { imgpath: imgpath, des: des, linkurl: linkurl }, callback);
    },
    delapplication: function (id, imgname, callback) {
        ajaxPost("/SystemFunc/DelApplication", { id: id, imgname: imgname }, callback);
    },
    checkActivationCode: function (ActivationCode, callback) {
        ajaxPost("/Account/CheckActivationCode", { ActivationCode: ActivationCode }, callback);
    },
    getUserMin_Id: function (memberid, callback) {
        ajaxPost("/Admin/getUserMin_Id", { memberid: memberid }, callback);
    }
}

//推荐数据管理相关方法
var AdminRecommendedDataProvider =
{
    addAdminLikeTopic: function (userId, topicId, callBack) {
        ajaxPost("/SystemFunc/AddAdminLikeTopic", { userId: userId, topicId: topicId }, callBack);
    },
    deleteAdminLikeTopic: function (userId, topicId, callBack) {
        ajaxPost("/SystemFunc/DeleteAdminLikeTopic", { userId: userId, topicId: topicId }, callBack);
    },
    addAdminLikeTopicContent: function (userId, topicId, callBack) {
        ajaxPost("/SystemFunc/AddAdminLikeTopicContent", { userId: userId, topicId: topicId }, callBack);
    },
    deleteAdminLikeTopicContent: function (userId, topicId, callBack) {
        ajaxPost("/SystemFunc/DeleteAdminLikeTopicContent", { userId: userId, topicId: topicId }, callBack);
    },
    addTopImagePush: function (contentid, callback) {
        ajaxPost("/SystemFunc/AddTopImagePush", { contentid: contentid }, callback);
    },
    deleteTopImagePush: function (pushImgId, callback) {
        ajaxPost("/SystemFunc/DeleteTopImagePush", { pushImgId: pushImgId }, callback);
    },
    getAllTopImagesPush: function (pageSize, pageNo, callback) {
        ajaxPost("/SystemFunc/GetAllTopImagesPush", { pageSize: pageSize, pageNo: pageNo }, callback);
    },
    getTopImagesPush: function (deleteFlag, pageSize, pageNo, callback) {
        ajaxPost("/SystemFunc/GetTopImagesPush", { deleteFlag: deleteFlag, pageSize: pageSize, pageNo: pageNo }, callback);
    },
    getPushImageCount: function (deleteFlag, callback) {
        ajaxPost("/SystemFunc/GetPushImageCount", { deleteFlag: deleteFlag }, callback);
    },
    getAllPushImageCount: function (callback) {
        ajaxPost("/SystemFunc/GetAllPushImageCount", {  }, callback);
    },
    ifImagePush: function (contentid, callback) {
        ajaxPost("/SystemFunc/IfImagePush", { contentid: contentid }, callback);
    },
    ifAdminLikedTopic: function (userId, topicId, callBack) {
        ajaxPost("/SystemFunc/IfAdminLikedTopic", { userId: userId, topicId: topicId }, callBack);
    },
    ifAdminLikedTopicContent: function (userId, topicId, callBack) {
        ajaxPost("/SystemFunc/IfAdminLikedTopicContent", { userId: userId, topicId: topicId }, callBack);
    },
    getAdminLikeOrNotTopics: function (interestID, pageSize, pageNo, likeOrNot, callBack) {
        ajaxPost("/SystemFunc/GetAdminLikeOrNotTopics", { interestID: interestID, pageSize: pageSize, pageNo: pageNo, likeOrNot: likeOrNot }, callBack);
    },
    getAdminLikeOrNotTopicsCount: function (interestID, likeOrNot, callBack) {
        ajaxPost("/SystemFunc/GetAdminLikeOrNotTopicsCount", { interestID: interestID, likeOrNot: likeOrNot }, callBack);
    },
    updateAllTopicsAdminLikeCount: function (userId, callBack) {
        ajaxPost("/SystemFunc/UpdateAllTopicsAdminLikeCount", { userId: userId }, callBack);
    },
    setPushImageShowStatus: function (pushImageId, showStatus, callBack) {
        ajaxPost("/SystemFunc/SetPushImageShowStatus", { pushImageId: pushImageId, showStatus: showStatus }, callBack);
    },
    addTopPushImage: function (contentId, photoPath, callBack) {
        ajaxPost("/SystemFunc/AddTopPushImage", { contentId: contentId, photoPath: photoPath }, callBack);
    },
    getFeaturedContents: function (usedFlag, pageSize, pageNo, callBack) {
        ajaxPost("/SystemFunc/GetFeaturedContents", { usedFlag: usedFlag, pageSize: pageSize, pageNo: pageNo }, callBack);
    },
    getFeaturedContentsCount: function (usedFlag, callBack) {
        ajaxPost("/SystemFunc/GetFeaturedContentsCount", { usedFlag: usedFlag }, callBack);
    },
    addFeaturedContent: function (image, content, usedFlag, callBack) {
        ajaxPost("/SystemFunc/AddFeaturedContent", { image: image, content: content, usedFlag: usedFlag }, callBack);
    },
    DeleteFeaturedContent: function (featContentId, callBack) {
        ajaxPost("/SystemFunc/DeleteFeaturedContent", { featContentId: featContentId }, callBack);
    },
    ajaxSetFeaturedContentHasUsed: function (featCtentId, usedFlag, callBack) {
        ajaxPost("/SystemFunc/AjaxSetFeaturedContentHasUsed", { featCtentId: featCtentId, usedFlag: usedFlag }, callBack);
    }
}

//字典项目相关数据方法
var AdminDictionaryDataProvider =
{
    addMemberSkin: function (personalityPicture, personalityBackgroundPicture, callBack) {
        ajaxPost("/SystemFunc/AddMemberSkin", { personalityPicture: personalityPicture, personalityBackgroundPicture: personalityBackgroundPicture }, callBack);
    },
    deleteMemberSkin: function (memberSkinId, callBack) {
        ajaxPost("/SystemFunc/DeleteMemberSkin", { memberSkinId: memberSkinId }, callBack);
    },
    getMemberSkins: function (pageSize, pageNo, callBack) {
        ajaxPost("/SystemFunc/GetMemberSkins", { pageSize: pageSize, pageNo: pageNo }, callBack);
    }
}

//活动相关数据方法
var adminActivityDataProvider =
{
    getInviteCodes: function (usedFlag, pageSize, pageNo, callBack) {
        ajaxPost("/SystemFunc/GetInviteCodes", { usedFlag: usedFlag, pageSize: pageSize, pageNo: pageNo }, callBack);
    },
    getInviteCodesCount: function (usedFlag, callBack) {
        ajaxPost("/SystemFunc/GetInviteCodesCount", { usedFlag: usedFlag }, callBack);
    },
    getInviteCodesToMember: function (callBack) {
        ajaxPost("/SystemFunc/GetInviteCodeToMember", {}, callBack);
    },
    generateInviteCodes: function (count, generatedMemberId, callBack) {
        ajaxPost("/SystemFunc/GenerateInviteCodes", { count: count, generatedMemberId: generatedMemberId }, callBack);
    },
    generateInviteCodeToMember: function (callBack) {
        ajaxPost("/SystemFunc/GenerateInviteCodeToMember", {}, callBack);
    },
    ajaxSetInviteCodeHasUsed: function (inviteCode, callBack) {
        ajaxPost("/SystemFunc/AjaxSetInviteCodeHasUsed", { inviteCode: inviteCode }, callBack);
    }
}
//用户状态相关数据方法
var adminMemberInfoDataProvider =
{
    getMemberInfoCount: function (callBack) {
        ajaxGet("/SystemFunc/GetMemberInfoCount", {  }, callBack);
    },
    getTypeMemberInfoCount: function (activityType, callBack) {
        ajaxGet("/SystemFunc/GetTypeMemberInfoCount", { activityType: activityType }, callBack);
    },
    getFromMemberInfoCount: function (fromMember, callBack) {
        ajaxPost("/SystemFunc/GetFromMemberInfoCount", { fromMember: fromMember }, callBack);
    },
    getTimeMemberInfoCount: function (startTime, endTime, callBack) {
        ajaxPost("/SystemFunc/GetTimeMemberInfoCount", { startTime: startTime, endTime: endTime }, callBack);
    },
    getMemberInfos: function (pageSize, pageNo, callBack) {
        ajaxGet("/SystemFunc/GetMemberInfos", { pageSize: pageSize, pageNo: pageNo }, callBack);
    },
    getTypeMemberInfos: function (activityType, pageSize, pageNo, callBack) {
        ajaxGet("/SystemFunc/GetTypeMemberInfos", { activityType: activityType, pageSize: pageSize, pageNo: pageNo }, callBack);
    },
    getFromMemberInfos: function (fromMember, pageSize, pageNo, callBack) {
        ajaxPost("/SystemFunc/GetFromMemberInfos", { fromMember: fromMember, pageSize: pageSize, pageNo: pageNo }, callBack);
    },
    getTimeMemberInfos: function (startTime, endTime, pageSize, pageNo, callBack) {
        ajaxPost("/SystemFunc/GetTimeMemberInfos", { startTime: startTime, endTime: endTime, pageSize: pageSize, pageNo: pageNo }, callBack);
    },
    createMemberActivity: function (fromMember, toMember, activType, operateUrl, callBack) {
        ajaxGet("/SystemFunc/CreateMemberActivity", 
            { fromMember: fromMember, toMember: toMember, activType: activType, operateUrl: operateUrl }, callBack);
    }
}
//图片审核相关数据方法
var adminImageAuditDataProvider =
{
    beautyphoto: function (id, auditresult, callback) {
        ajaxPost("/Admin/beautyphoto", { id: id, auditresult: auditresult }, callback);
    },
    getauditphotos: function (photoType, isaudited, pagesize, pageno, callback) {
        ajaxPost("/Admin/getauditphotos", { photoType: photoType, isaudited: isaudited, pagesize: pagesize, pageno: pageno }, callback);
    },
    auditphoto: function (id, auditresult, pictureType, callback) {
        ajaxPost("/Admin/auditphoto", { id: id, auditresult: auditresult, pictureType: pictureType }, callback);
    },
    auditphotos: function (ids, auditresult, pictureType, callback) {
        ajaxPost("/Admin/auditphotos", { ids: ids, auditresult: auditresult, pictureType: pictureType }, callback);
    },
    getTypeAuditPhotoCount: function (photoType, isAudited, callback) {
        ajaxPost("/SystemFunc/GetTypeAuditPhotoCount", { photoType: photoType, isAudited: isAudited }, callback);
    }
}
