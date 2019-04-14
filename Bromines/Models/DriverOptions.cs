using Bromine.Core;

namespace Bromine.Models
{
    /// <summary>
    /// Driver configuration options for browser setup.
    /// </summary>
    public class DriverOptions
    {
        /// <summary>
        /// Create a DriverOptions object to configure how the driver will work.
        /// </summary>
        /// <param name="browser">Type of browser to use.</param>
        /// <param name="isHeadless">When true, the UI will not be rendered for the browser.</param>
        /// <param name="hideDriverWindow">When true, the command window will not be visible for the driver.</param>
        public DriverOptions(BrowserType browser, bool isHeadless = false, bool hideDriverWindow = true)
        {
            Browser = browser;
            IsHeadless = isHeadless;
            HideDriverWindow = hideDriverWindow;
        }

        /// <summary>
        /// Type of browser to use.
        /// </summary>
        public BrowserType Browser { get; set; }

        /// <summary>
        /// When true the UI will not be rendered. This saves time and system resources.
        /// </summary>
        public bool IsHeadless { get; set; }

        /// <summary>
        /// When true the command window will not show when the browser driver is active.
        /// </summary>
        public bool HideDriverWindow { get; set; }
    }
}
