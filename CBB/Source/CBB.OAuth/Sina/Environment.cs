using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Text.RegularExpressions;
using CBB.OAuth.Sina.Common;

namespace CBB.OAuth.Sina
{
    /// <summary>
    /// Provides access to environmental variables, like current Access Token.
    /// </summary>
    public static class Environment
    {
        /// <summary>
        /// Initializes the envrionment settings.
        /// </summary>
        static Environment()
        {
            var section = ConfigurationManager.GetSection("amicroblogAPI") as AMicroblogAPIConfigurationSection;
            if (null != section)
            {
                Environment.ResponseErrorHandlingEnabled = section.ResponseErrorHandlingConfig.Enabled;
                Environment.Configuration = section;

                foreach (HandlerConfigurationElement element in section.ResponseErrorHandlingConfig)
                {
                    if(string.IsNullOrEmpty(element.Type))
                        throw new AMicroblogException(LocalErrorCode.ArgumentNotProvided, "Handler type not provided in responseErrorHandling configuration section.");

                    var type = Type.GetType(element.Type, true, true);
                    var errorCode = element.ErrorCode;
                    if (string.IsNullOrEmpty(errorCode))
                        errorCode = "*";

                    var interfaceName = "IResponseErrorHandler";
                    var inter = type.GetInterface(interfaceName, false);
                    if (null == inter)
                        throw new AMicroblogException(LocalErrorCode.ArgumentInvalid, "Type '{0}' does not implement {1}.", element.Type, interfaceName);

                    ResponseErrorHandlers.Add(new HandlerConfiguration() { Type = type, ErrorCode = errorCode });
                }

                if (Environment.ResponseErrorHandlingEnabled)
                {
                    Environment.ResponseError += new EventHandler<ResponseErrorEventArgs>(HandleResponseError);
                }
            }

            AppKey = ConfigurationManager.AppSettings["SinaWeiboKey"];
            AppSecret = ConfigurationManager.AppSettings["SinaWeiboKeySecret"];
            if(string.IsNullOrEmpty(AppKey) || string.IsNullOrEmpty(AppSecret))
                throw new AMicroblogException(LocalErrorCode.AppKeyOrSecretNotProvided, "appKey or appSecret not configured in application config file.");
        }

        /// <summary>
        /// Handles the response error event.
        /// </summary>
        private static void HandleResponseError(object sender, ResponseErrorEventArgs e)
        {
            foreach (var handler in ResponseErrorHandlers)
            {
                try
                {
                    var errorCode = e.ErrorData.ErrorCode.ToString();
                    if (handler.ErrorCode == "*" || errorCode == handler.ErrorCode || Regex.IsMatch(errorCode, handler.ErrorCode, RegexOptions.IgnoreCase))
                    {
                        var realHandler = Activator.CreateInstance(handler.Type) as IResponseErrorHandler;
                        realHandler.Handle(e.ErrorData);
                        e.IsHandled = true;
                    }
                }
                catch
                {
                    // Nothing to do. 
                }
            }
        }

        /// <summary>
        /// Gets the app key configured in app.config.
        /// </summary>
        /// <remarks>In a web application, this field should be set manually each time a OAuth request is sent.</remarks>
        public static string AppKey
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the app secret configured in app.config.
        /// </summary>
        /// <remarks>In a web application, this field should be set manually each time a OAuth request is sent.</remarks>
        public static string AppSecret
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current access token generated after login.
        /// </summary>
        /// <remarks>
        /// This AccessToken is used to construct OAuth authorization header for all HTTP requests which requires OAuth authorization.
        /// By default, this token is set after a successful login.
        /// In web application, this field should be set manually each time a OAuth request is sent. In this case, consider stores the access token in session or somewhere after login.
        /// </remarks>
        public static OAuthAccessToken AccessToken
        {
            get;
            set;
        }

        /// <summary>
        /// Validates whether the current access token is null or not.
        /// </summary>
        /// <remarks><see cref="AMicroblogAPI.Common.AMicroblogException"/> is thrown if null.</remarks>
        /// <exception cref="AMicroblogAPI.Common.AMicroblogException">If the access token is null.</exception>
        public static void ValidateAccessToken()
        {
            if (null == Environment.AccessToken)
                throw new AMicroblogException(LocalErrorCode.AccessTokenNotObtained, "Access token not obtained. Make sure retrieved access token is set to Environment.AccessToken.");
        }

        /// <summary>
        /// Gets the current login user's SINA account name.
        /// </summary>
        public static string CurrentUserAccount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the OAuth signature method. Default to "HMAC-SHA1".
        /// </summary>
        public static string OAuthSignatureMethod = "HMAC-SHA1";

        /// <summary>
        /// Gets or sets the OAuth version. Default to "1.0".
        /// </summary>
        public static string OAuthVersion = "1.0";

        /// <summary>
        /// Gets or sets the OAuth callback. Default to "oob".
        /// </summary>
        /// <remarks>In a web application, consider overrides this field to a desired uri during the RequestToken call.</remarks>
        public static string OAuthCallback = "oob";

        /// <summary>
        /// Represents the event that a HTTP response comes back in error.
        /// </summary>
        /// <remarks>Exception in the event handlers of this event is not thrown.</remarks>
        public static event EventHandler<ResponseErrorEventArgs> ResponseError;

        /// <summary>
        /// Triggers the <see cref="ResponseError"/>.
        /// </summary>
        /// <remarks>To insure that each event handler has a chance to process the event, any exception is suspended.</remarks>
        internal static void NotifyResponseError(ResponseErrorEventArgs e)
        {
            if (null != ResponseError)
            {
                var invocationList = ResponseError.GetInvocationList();
                foreach (var invocation in invocationList)
                {
                    try
                    {
                        var inv = invocation as EventHandler<ResponseErrorEventArgs>;
                        inv(null, e);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Gets or sets the configuration of AMicroblogAPI.
        /// </summary>
        internal static AMicroblogAPIConfigurationSection Configuration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a boolean value indicating whether response error handling is enabled.
        /// </summary>
        internal static bool ResponseErrorHandlingEnabled
        {
            get;
            set;
        }

        private static Collection<HandlerConfiguration> handlers = new Collection<HandlerConfiguration>();
        /// <summary>
        /// Gets the eesponse error handlers.
        /// </summary>
        internal static Collection<HandlerConfiguration> ResponseErrorHandlers
        {
            get { return handlers; }
        }
    }
}
