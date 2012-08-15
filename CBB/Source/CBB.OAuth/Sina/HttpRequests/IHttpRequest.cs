using CBB.OAuth.Sina.Common;

namespace CBB.OAuth.Sina.HttpRequests
{
    /// <summary>
    /// Defines methods to perform HTTP request.
    /// </summary>
    public interface IHttpRequest
    {
        /// <summary>
        /// Performs the HTTP request.
        /// </summary>
        /// <returns>The HTTP response.</returns>
        string Request();

        /// <summary>
        /// Performs the HTTP request asynchronously.
        /// </summary>
        void RequestAsync(AsyncCallback<string> callback);
    }
}
