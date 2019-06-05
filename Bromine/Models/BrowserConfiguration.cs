using Bromine.Constants;

namespace Bromine.Models
{
    /// <summary>
    /// Driver configuration options for browser setup.
    /// </summary>
    public class BrowserConfiguration
    {
        /// <summary>
        /// Create a BrowserConfiguration object to configure how the driver will work.
        /// </summary>
        /// <param name="browser"><see cref="Browser"/>.</param>
        /// <param name="isHeadless"><see cref="IsHeadless"/></param>
        /// <param name="hideDriverWindow"><see cref="HideDriverWindow"/></param>
        /// <param name="enableImplicitWait"><see cref="EnableImplicitWait"/></param>
        /// <param name="secondsToImplicitWait"><see cref="SecondsToImplicitWait"/></param>
        public BrowserConfiguration(BrowserType browser, bool isHeadless = false, bool hideDriverWindow = true, bool enableImplicitWait = true, int secondsToImplicitWait = 5)
        {
            Browser = browser;
            IsHeadless = isHeadless;
            HideDriverWindow = hideDriverWindow;
            EnableImplicitWait = enableImplicitWait;
            SecondsToImplicitWait = secondsToImplicitWait;
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

        /// <summary>
        /// When true calls to Selenium will automatically wait the time specified in <see cref="SecondsToImplicitWait"/>.
        /// </summary>
        public bool EnableImplicitWait { get; set; }

        /// <summary>
        /// Number of seconds to automatically wait for Selenium calls before timing out. This is only applicable if <see cref="EnableImplicitWait"/> is true.
        /// </summary>
        public int SecondsToImplicitWait { get; set; }
    }
}
