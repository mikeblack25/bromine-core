using Bromine.Constants;

namespace Bromine.Models
{
    /// <summary>
    /// Driver configuration options for browser setup.
    /// </summary>
    public class BrowserOptions
    {
        /// <summary>
        /// Launch a Chrome browser with the default configuration.
        /// <see cref="DriverOptions.Browser"/> Chrome will be launched.
        /// <see cref="DriverOptions.IsHeadless"/> false (the UI will show).
        /// <see cref="DriverOptions.SecondsToWait"/> 0 (no implicit wait).
        /// <see cref="DriverOptions.RemoteAddress"/> Browsers will be launched locally.
        /// <see cref="DriverOptions.ScreenShotPath"/> ScreenShots are stored in in a ScreenShots folder in the executing directory.
        /// <see cref="DriverOptions.UseDefaultDriverPath"/> false.
        /// <see cref="DriverOptions.HideDriverWindow"/> true (drivers may have to be closed manually if not disposed properly).
        /// </summary>
        public BrowserOptions()
            : this(new DriverOptions())
        {
        }

        /// <summary>
        /// Configure all DriverOptions properties as needed for the driver.
        /// </summary>
        /// <param name="browser"><see cref="DriverOptions.Browser"/></param>
        /// <param name="isHeadless"><see cref="DriverOptions.IsHeadless"/></param>
        /// <param name="secondsToWait"><see cref="DriverOptions.SecondsToWait"/></param>
        /// <param name="remoteAddress"><see cref="DriverOptions.RemoteAddress"/></param>
        /// <param name="screenShotPath"><see cref="DriverOptions.ScreenShotPath"/></param>
        /// <param name="useDefaultDriverPath"><see cref="DriverOptions.UseDefaultDriverPath"/></param>
        /// <param name="hideDriverWindow"><see cref="DriverOptions.HideDriverWindow"/></param>
        public BrowserOptions(BrowserType browser, bool isHeadless, int secondsToWait = 0, string remoteAddress = "", string screenShotPath = "", bool useDefaultDriverPath = false, bool hideDriverWindow = true)
        : this(new DriverOptions(browser, isHeadless, secondsToWait, remoteAddress, screenShotPath, useDefaultDriverPath, hideDriverWindow))
        {
        }

        /// <summary>
        /// Provide a DriverOptions to configure the driver.
        /// NOTE: This options is best for creating reusable configurations as needed for advanced setup.
        /// </summary>
        /// <param name="options"><see cref="DriverOptions"/></param>
        public BrowserOptions(DriverOptions options)
        {
            Driver = options;
        }

        /// <summary>
        /// Type of browser to use.
        /// </summary>
        public DriverOptions Driver { get; }
    }
}
