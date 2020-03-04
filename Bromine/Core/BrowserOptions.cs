namespace Bromine.Core
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
        /// <param name="useDefaultDriverPath"><see cref="DriverOptions.UseDefaultDriverPath"/></param>
        /// <param name="hideDriverWindow"><see cref="DriverOptions.HideDriverWindow"/></param>
        /// <param name="stopOnError"><see cref="StopOnError"/></param>
        /// <param name="logElementHistory"><see cref="LogElementHistory"/></param>
        public BrowserOptions(BrowserType browser, bool isHeadless = false, int secondsToWait = 0, string remoteAddress = "", bool useDefaultDriverPath = false, bool hideDriverWindow = true, bool stopOnError = true, bool logElementHistory = false)
        : this(new DriverOptions(browser, isHeadless, secondsToWait, remoteAddress, useDefaultDriverPath, hideDriverWindow), stopOnError: stopOnError, logElementHistory: logElementHistory)
        {
        }

        /// <summary>
        /// Provide a DriverOptions to configure the driver.
        /// NOTE: This options is best for creating reusable configurations as needed for advanced setup.
        /// </summary>
        /// <param name="options"><see cref="DriverOptions"/></param>
        /// <param name="stopOnError"><see cref="StopOnError"/></param>
        /// <param name="logElementHistory"><see cref="LogElementHistory"/></param>
        public BrowserOptions(DriverOptions options, bool stopOnError = true, bool logElementHistory = false)
        {
            Driver = options;
            StopOnError = stopOnError;
            LogElementHistory = logElementHistory;
        }

        /// <summary>
        /// Type of browser to use.
        /// </summary>
        public DriverOptions Driver { get; }

        /// <summary>
        /// When true errors locating elements will stop execution.
        /// </summary>
        public bool StopOnError { get; }

        /// <summary>
        /// Update the the session log with information about elements.
        /// </summary>
        public bool LogElementHistory { get; }
    }


    /// <summary>
    /// Supported web browser types.
    /// </summary>
    public enum BrowserType
    {
#pragma warning disable 1591
        Chrome,
        Edge,
        Firefox,
        InternetExplorer
#pragma warning restore 1591
    }
}
