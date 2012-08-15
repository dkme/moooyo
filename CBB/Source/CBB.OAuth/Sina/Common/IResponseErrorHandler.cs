using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.OAuth.Sina.Common
{
    /// <summary>
    /// Defines methods to handle HTTP response error. 
    /// </summary>
    public interface IResponseErrorHandler
    {
        /// <summary>
        /// Handles the response error with the <paramref name="errorData"/>.
        /// </summary>
        /// <param name="errorData">The contextual data of the response error.</param>
        void Handle(ResponseErrorData errorData);
    }
}
