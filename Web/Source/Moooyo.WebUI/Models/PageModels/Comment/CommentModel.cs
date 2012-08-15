using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels.Comment
{
    public class CommentModel
    {
        public BiZ.Content.PublicContent contentObject;
        public IList<BiZ.Comment.Comment> commentList;
        public int commentCount;

        public CommentModel() { }
    }
}