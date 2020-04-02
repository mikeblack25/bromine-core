using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Bromine.Element
{
    /// <summary>
    /// Provide details about how and when the element was requested.
    /// </summary>
    public class Information
    {
        /// <inheritdoc />
        public Information()
        {
            Attributes = new Dictionary<string, object>();
        }

        /// <summary>
        /// Name of the session or test where the element is called.
        /// </summary>
        public string SessionName { get; set; }

        /// <summary>
        /// Timestamp the element was requested.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Time in seconds to locate the element.
        /// </summary>
        public TimeSpan FindTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan TotalTime { get; set; }

        /// <summary>
        /// Element attributes.
        /// </summary>
        public Dictionary<string, object> Attributes { get; set; }

        /// <summary>
        /// String used to find the requested element.
        /// </summary>
        public string LocatorString { get; set; }

        /// <summary>
        /// Locator strategy used to find the requested element.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Strategy Strategy { get; set; }

        /// <summary>
        /// Name of the element caller.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Path to the file of the Element caller.
        /// </summary>
        public string CallerFilePath { get; set; }

        /// <summary>
        /// Line number of the Element caller.
        /// </summary>
        public int CallerLineNumber { get; set; }

        /// <summary>
        /// Does the element have the proper context to communicate with a browser.
        /// </summary>
        public bool IsInitialized { get; set; }
    }
}
