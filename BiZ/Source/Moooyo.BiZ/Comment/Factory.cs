using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Comment
{
    public class Factory
    {
        public ICanBeenComment getICanBeenComment(Comm.ContentType name)
        {
            ICanBeenComment iCanBeenComment = null;
            switch (name)
            {
                case Comm.ContentType.PublicContent: iCanBeenComment = new Content.PublicContent(); break;
                case Comm.ContentType.InterView: iCanBeenComment = new InterView.InterView(); break;
                default: break;
            }
            return iCanBeenComment;
        }
    }
}
