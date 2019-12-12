using System;

namespace Bromine.Element
{
    /// <summary>
    /// Provide details about how and when the element was requested.
    /// </summary>
    public class Information
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
        public Strategy Strategy { get; set; }
    }
}
