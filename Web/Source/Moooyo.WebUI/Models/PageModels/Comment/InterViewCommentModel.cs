using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels.Comment
{
    public class InterViewCommentModel
    {
        public BiZ.InterView.InterView interviewObject;
        public IList<BiZ.Comment.Comment> commentList;
        public InterViewCommentModel() { }
    }
}