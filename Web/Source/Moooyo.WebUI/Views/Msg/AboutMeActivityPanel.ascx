<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.MyActivityListPanelModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%@ Import Namespace="Moooyo.WebUI.Controllers" %>
<%@ Import Namespace="Moooyo.BiZ.Member.Activity" %>
<% 
    int aboutMeCount = Model.activityHolderList.Count;
    int relatCount = Model.relationObjList.Count;
    //bool flag1 = false;
    //int num = 0;
    //Moooyo.BiZ.Member.Activity.ActivityHolderRelatedToMe aObj = null;
    for (int i = 0; i < aboutMeCount; i++)
    {
        Moooyo.WebUI.Models.RelationDisplayObj rObj = Model.relationObjList[i];
        //for (int j = 0; j < aboutMeCount; j++) 
        //{
        //    if (rObj.FromMember == Model.activityHolderList[j].MemberID)
        //    {
        //        flag1 = true;
        //        num = j;
        //    }
        //    else
        //    {
        //        flag1 = false;
        //    }
        //    if (flag1) break;
        //}
         %>

    <div class="fans_box">
        <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4"></b></b>
        <div class="with_me">
            <div class="fans_info">
                <dl class="clearfix">
                    <dt><a href="/Content/TaContent/<%=rObj.ID %>" target="_blank" id="activityMemberInfo1"><img src="<%=rObj.MinICON %>" data_me_id="<%=Model.UserID %>" data_member_id="<%=rObj.ID %>" name="activityMemberInfoArea" height="30" width="30" title="<%=rObj.Name%>" /></a></dt>
                    <dd><span class="blue02"><a href="/Content/TaContent/<%=rObj.ID %>" target="_blank"><%=rObj.Name%></a></span>
                    <% if (rObj.IsRealPhotoIdentification)
                      { %><em><img src="/pics/video_pic.png" height="14" width="14" /></em><% } %>
                      </dd>
                    <dd></dd>
                </dl>
            </div>
            <div class="fans_detail">
                <% 
                //if (flag1)
                //{
                string str = Moooyo.WebUI.Controllers.ActivityController.GetActivityStr(Model.activityHolderList[i], rObj);
                    Response.Write(str);
                //}
                 %>
                         
            </div>
        </div>
            <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1"></b></b>
    </div>  
    <% } %>