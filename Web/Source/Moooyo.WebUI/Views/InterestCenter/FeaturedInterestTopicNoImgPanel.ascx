﻿<%@ Import Namespace="Moooyo.BiZ.WenWen"  %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.BiZ.WenWen.WenWen>" %>
<%var topic = Model;
  string content = Server.UrlDecode(topic.Content);
  content = Comm.getExpression(content);
  %>
                       <div class="content5">
                    <div class="left4">
                        <a href="/WenWen/ShowWenWen?wwid=<%=topic.ID %>" target="_blank" class="none-12-666666"><%=content.Length > 89 ? content.Substring(0, 89) + "<span class=\"letspa--3\">...</span>" : content%></a>
                    </div>
                    <div class="right3">
                    <% Moooyo.BiZ.InterestCenter.Interest interest = Moooyo.BiZ.InterestCenter.InterestFactory.GetInterest(topic.InterestID); %>
                        <a href="/InterestCenter/InterestFans?iid=<%=interest.ID %>" target="_blank" title="<%=interest.Title %>" name="interestA" data-interestid="<%=interest.ID %>"><img src="<%=Comm.getImagePath(interest.ICONPath, ImageType.Icon) %>" width="30" height="30" border="0" /></a>
                    </div>
                    <div class="bottom2"><span class="fl fc-666666">by <a href="/Member/Ta/<%=topic.Creater.MemberID%>" target="_blank" title="<%=topic.Creater.NickName%>"><%=topic.Creater.NickName%></a> &nbsp;</span><span class="mt5 fl"><a href="/Member/Ta/<%=topic.Creater.MemberID%>" target="_blank" title="<%=topic.Creater.NickName%>" name="memberInfoLabel" data_me_id="<%=ViewData["userId"] %>" data_member_id="<%=topic.Creater.MemberID %>"><img src="<%=Comm.getImagePath(topic.Creater.ICONPath, ImageType.Icon) %>" width="22" height="22" border="0" style="margin-top:4px;" /></a> &nbsp;</span><span class="mt8 manadiv fl" style="float:left;"><span class="like3" style="float:left;">&nbsp;</span><span class="like2" id="like<%=topic.ID %>" onclick="WenWenControl.likeclick('<%=topic.ID %>')" style="float:left;"><%=topic.Likecount%>&nbsp;♥</span><span class="like1" style="float:left;">&nbsp;</span></span><span class="fl"></span><span class="fl cgray ml10"><%=(topic.AnswerCount == 0 ? "" : ("" + topic.AnswerCount + "回应"))%></span></div>
                </div>
