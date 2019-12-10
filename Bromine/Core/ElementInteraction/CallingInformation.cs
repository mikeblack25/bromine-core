using System;

using Bromine.Core.ElementLocator;

namespace Bromine.Core.ElementInteraction
{
    /// <summary>
    /// Provide details about how and when the element was requested.
    /// </summary>
    public class CallingInformation
    {
        /// <summary>
        /// Timestamp the element was requested.
        /// </summary>
        public DateTime CalledTimestamp { get; set; }

        /// <summary>
        /// String used to find the requested element.
        /// </summary>
        public string LocatorString { get; set; }

        /// <summary>
        /// Locator strategy used to find the requested element.
        /// </summary>
        public LocatorStrategy LocatorStrategy { get; set; }
    }
}
