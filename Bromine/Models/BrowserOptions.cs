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
        public BrowserOptions(DriverOptions options, int secondsToWait = 0, string screenShotPath = "")
        {
            Driver = options;
            ScreenShotPath = screenShotPath;

            if (secondsToWait <= 0) { return; }

            ImplicitWaitEnabled = true;
            SecondsToWait = secondsToWait;
        }

        /// <summary>
        /// Create a BrowserOptions object to configure how the browser behaves.
        /// This constructor can be used for .NET Core applications as it sets the default driver path by default.
        /// </summary>
        /// <param name="browser"><see cref="DriverOptions.Browser"/></param>
        /// <param name="isHeadless"><see cref="DriverOptions.IsHeadless"/></param>
        /// <param name="remoteHostPath"><see cref="DriverOptions.RemoteHost"/></param>
        /// <param name="secondsToWait"><see cref="SecondsToWait"/></param>
        /// <param name="screenshotPath"><see cref="ScreenShotPath"/></param>
        public BrowserOptions(BrowserType browser = BrowserType.Chrome, bool isHeadless = false, string remoteHostPath = "", int secondsToWait = 0, string screenshotPath = "") :
            this(new DriverOptions(browser, isHeadless, remoteHostPath, true, true), secondsToWait, screenshotPath)
        {
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
    }
}
