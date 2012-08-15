using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CBB.OAuth.Sina.Common;
using System.Net;

namespace CBB.OAuth.Sina.HttpRequests
{
    /// <summary>
    /// Performs a OAuth token-attached HTTP-Delete request to the resource identified by the <c>uri</c>.
    /// </summary>
    public class OAuthHttpDelete : OAuthHttpGet
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OAuthHttpDelete"/> with the specified <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri"></param>
        public OAuthHttpDelete(string uri)
            : base(uri)
        {
            base.Method = HttpMethod.Delete;
        }
    }
}
