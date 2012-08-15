using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    public class ApplicationModel
    {
        public BiZ.Sys.Applications.Application appmodel;
        public ApplicationModel(BiZ.Sys.Applications.Application appmodel)
        {
            this.appmodel = appmodel;
        }
    }
}