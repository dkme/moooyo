using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;

namespace CBB.OAuth.Sina.Common
{
    /// <summary>
    /// Represents the configuration section of CBB.OAuth.Sina.
    /// </summary>
    internal class AMicroblogAPIConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the response error handling configuration.
        /// </summary>
        [ConfigurationProperty("responseErrorHandling")]
        public ResponseErrorHandlingConfiguration ResponseErrorHandlingConfig
        {
            get
            {
                return (ResponseErrorHandlingConfiguration)this["responseErrorHandling"];
            }
            set
            {
                this["responseErrorHandling"] = value;
            }
        }
    }

    /// <summary>
    /// Represents the response error handling configuration.
    /// </summary>
    [ConfigurationCollection(typeof(HandlerConfigurationElement), AddItemName = "handler")]
    internal class ResponseErrorHandlingConfiguration : ConfigurationElementCollection
    {
        /// <remarks/>
        protected override ConfigurationElement CreateNewElement()
        {
            return new HandlerConfigurationElement();
        }

        /// <remarks/>
        protected override object GetElementKey(ConfigurationElement element)
        {
            var handlerConfig = element as HandlerConfigurationElement;

            return handlerConfig.ErrorCode;
        }

        /// <summary>
        /// Gets or sets a boolean value indicating whether the response error handling is enabled.
        /// </summary>
        [ConfigurationProperty("enabled")]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }
            set
            {
                this["enabled"] = value;
            }
        }
    }

    /// <summary>
    /// Represents the response error handler configuration.
    /// </summary>
    internal class HandlerConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the type property.
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
            set
            {
                this["type"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the error code pattern property.
        /// </summary>
        [ConfigurationProperty("errorCode", IsKey=true)]
        public string ErrorCode
        {
            get
            {
                return (string)this["errorCode"];
            }
            set
            {
                this["errorCode"] = value;
            }
        }
    }

    /// <summary>
    /// Represents the response analyzed error handler configuration.
    /// </summary>
    internal class HandlerConfiguration
    {
        public Type Type { get; set; }
        public string ErrorCode { get; set; }
    }
}
