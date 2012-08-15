<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.InterestWenWenModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<%int showcount = int.Parse(ViewData["showcount"].ToString()); %>
<%if (Model.wenwenlist != null && Model.wenwenlist.Count > 0) { %>
<div class="interesttowwitemdiv">
<%
      List<Moooyo.BiZ.WenWen.WenWen> llist = new List<Moooyo.BiZ.WenWen.WenWen>();
      List<Moooyo.BiZ.WenWen.WenWen> rlist = new List<Moooyo.BiZ.WenWen.WenWen>();
      int lcount = 0, rcount = 0;
      for (int i = 0; i < Model.wenwenlist.Count; i++)
      {
          if (lcount + rcount < showcount)
          {
              var obj = Model.wenwenlist[i];
              if (obj != null && obj.Content != null && obj.InterestID != null)
              {
                  if (lcount - rcount == 0)
                  {
                      if (obj.ContentImage != null && obj.ContentImage != "")
                          lcount += 2;
                      else
                          lcount += 1;
                      llist.Add(obj);
                  }
                  else if (lcount - rcount > 0)
                  {
                      if (obj.ContentImage != null && obj.ContentImage != "")
                          rcount += 2;
                      else
                          rcount += 1;
                      rlist.Add(obj);
                  }
                  else if (lcount - rcount < 0)
                  {
                      if (obj.ContentImage != null && obj.ContentImage != "")
                          lcount += 2;
                      else
                          lcount += 1;
                      llist.Add(obj);
                  }
              }
          }
          else
              break;
      }
      if (rcount > lcount)
      {
          rlist.RemoveAt(rlist.Count - 1);
      }
      String ltopicstr = "<div class=\"leftdiv\">";
      String rtopicstr = "<div class=\"rightdiv\">";
      foreach (var obj in llist)
      {
          if (obj.ContentImage != null && obj.ContentImage != "")
          {
              string content = obj.Content.Length > 30 ? obj.Content.Substring(0, 30) + "..." : obj.Content;
              content = Comm.getExpression(content);
              string replay = obj.AnswerCount > 0 ? "<a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + obj.AnswerCount + "回应</a>" : "";
              string imagestr = obj.ContentImage;
              string imgstr = "<a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + Comm.getImageToTopic(imagestr, 1) + "</a>";
              string classname = obj.Creater.Sex == 1 ? "class=\"itemboybig\"" : "class=\"tiemgirlbig\"";
              ltopicstr += "<div " + classname + "><div class=\"imgdiv myinterestuser\"><a href=\"/Member/Ta/" + obj.Creater.MemberID + "\" target=\"_blank\"><img data_me_id=\"" + Model.UserID + "\" data_member_id=\"" + obj.Creater.MemberID + "\" src=\"" + Comm.getImagePath(obj.Creater.ICONPath, ImageType.Middle) + "\"/>" + obj.Creater.NickName + "</a></div><div class=\"contentdiv\"><span><a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + content + "</a></span>" + imgstr + "</div><div class=\"manadiv\">" + replay + "&nbsp;" + (imagestr.Split(',').Length - 1) + "图</div></div>";
          }
          else
          {
              string content = obj.Content.Length > 30 ? obj.Content.Substring(0, 30) + "..." : obj.Content;
              content = Comm.getExpression(content);
              string replay = obj.AnswerCount > 0 ? "<a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + obj.AnswerCount + "回应</a>" : "";
              string classname = obj.Creater.Sex == 1 ? "class=\"itemboysmal\"" : "class=\"itemgirlsmal\"";
              ltopicstr += "<div " + classname + "><div class=\"imgdiv myinterestuser\"><a href=\"/Member/Ta/" + obj.Creater.MemberID + "\" target=\"_blank\"><img data_me_id=\"" + Model.UserID + "\" data_member_id=\"" + obj.Creater.MemberID + "\" src=\"" + Comm.getImagePath(obj.Creater.ICONPath, ImageType.Middle) + "\"/>" + obj.Creater.NickName + "</a></div><div class=\"contentdiv\"><span><a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + content + "</a></span></div><div class=\"manadiv\">" + replay + "</div></div>";
          }
      }
      foreach (var obj in rlist)
      {
          if (obj.ContentImage != null && obj.ContentImage != "")
          {
              string content = obj.Content.Length > 30 ? obj.Content.Substring(0, 30) + "..." : obj.Content;
              content = Comm.getExpression(content);
              string replay = obj.AnswerCount > 0 ? "<a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + obj.AnswerCount + "回应</a>" : "";
              string imagestr = obj.ContentImage;
              string imgstr = "<a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + Comm.getImageToTopic(imagestr, 1) + "</a>";
              string classname = obj.Creater.Sex == 1 ? "class=\"itemboybig\"" : "class=\"tiemgirlbig\"";
              rtopicstr += "<div " + classname + "><div class=\"imgdiv myinterestuser\"><a href=\"/Member/Ta/" + obj.Creater.MemberID + "\" target=\"_blank\"><img data_me_id=\"" + Model.UserID + "\" data_member_id=\"" + obj.Creater.MemberID + "\" src=\"" + Comm.getImagePath(obj.Creater.ICONPath, ImageType.Middle) + "\"/>" + obj.Creater.NickName + "</a></div><div class=\"contentdiv\"><span><a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + content + "</a></span>" + imgstr + "</div><div class=\"manadiv\">" + replay + "&nbsp;" + (imagestr.Split(',').Length - 1) + "图</div></div>";
          }
          else
          {
              string content = obj.Content.Length > 30 ? obj.Content.Substring(0, 30) + "..." : obj.Content;
              content = Comm.getExpression(content);
              string replay = obj.AnswerCount > 0 ? "<a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + obj.AnswerCount + "回应</a>" : "";
              string classname = obj.Creater.Sex == 1 ? "class=\"itemboysmal\"" : "class=\"itemgirlsmal\"";
              rtopicstr += "<div " + classname + "><div class=\"imgdiv myinterestuser\"><a href=\"/Member/Ta/" + obj.Creater.MemberID + "\" target=\"_blank\"><img data_me_id=\"" + Model.UserID + "\" data_member_id=\"" + obj.Creater.MemberID + "\" src=\"" + Comm.getImagePath(obj.Creater.ICONPath, ImageType.Middle) + "\"/>" + obj.Creater.NickName + "</a></div><div class=\"contentdiv\"><span><a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + content + "</a></span></div><div class=\"manadiv\">" + replay + "</div></div>";
          }
      }
      ltopicstr += "</div>";
      rtopicstr += "</div>";
%>
<%=ltopicstr %>
<%=rtopicstr %>
</div>
<%} %>
