using Bromine.Constants;

namespace Bromine.Models
{
    /// <summary>
    /// Driver configuration options for browser setup.
    /// <see cref="DriverOptions"/> for advanced driver setup.
    /// <see cref="ImplicitWaitEnabled" /> to determine if implicit waits are enabled.
    /// <see cref="SecondsToWait"/> Amount of time to wait for Selenium actions.
    /// </summary>
    public class BrowserOptions
    {
        /// <inheritdoc />
        /// <summary>
        /// Create default BrowserOptions:
        /// Browser: Chrome
        /// Implicit Wait: False
        /// Screenshot Path: Default
        /// These are the only settings that can be changed for advanced options use a different constructor."/>
        /// </summary>
        public BrowserOptions(BrowserType browser = BrowserType.Chrome, int secondsToWait = 0, string screenShotPath = "")
            : this(new DriverOptions(browser), secondsToWait, screenShotPath)
        {
        }

        /// <summary>
        /// Create a BrowserOptions object to configure how the browser will behave.
        /// </summary>
        /// <param name="options">Advanced browser driver configuration options.</param>
        /// <param name="secondsToWait">Number of seconds to wait for a condition to be satisfied. Sets ImplicitWaitEnabled = true when greater than 0.</param>
        /// <param name="screenShotPath">Path to save screenshots. Default is a Screenshots folder where the app is launched.</param>
        public BrowserOptions(DriverOptions options, int secondsToWait = 0, string screenShotPath = "", bool hideDriverWindow = true)
        {
            Driver = options;

            if (secondsToWait <= 0) { return; }

            ImplicitWaitEnabled = true;
            SecondsToWait = secondsToWait;
            ScreenShotPath = screenShotPath;
            HideDriverWindow = hideDriverWindow;
        }

        /// <summary>
        /// Type of browser to use.
        /// </summary>
        public DriverOptions Driver { get; set; }

        /// <summary>
        /// The amount of time for a Selenium action to be satisfied before failing.
        /// </summary>
        public bool ImplicitWaitEnabled { get; }

        /// <summary>
        /// When ImplicitWaitEnabled is true, the condition will periodically checked until the amount of seconds specified here.
        /// </summary>
        public int SecondsToWait { get; set; }

        /// <summary>
        /// Path to store screenshots.
        /// </summary>
        public string ScreenShotPath { get; set; }

        /// <summary>
        /// When true the command window will not show when the browser driver is active.
        /// </summary>
        public bool HideDriverWindow { get; set; }
    }
}
