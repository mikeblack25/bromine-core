using Bromine.Core;

namespace Bromine.Models
{
    /// <summary>
    /// Driver configuration options for browser setup.
    /// </summary>
    public class BrowserOptions
    {
        /// <summary>
        /// Create a BrowserOptions object to configure how the browser will behave.
        /// </summary>
        /// <param name="browser">Type of browser to use.</param>
        /// <param name="enableImplicitWait">When true, the driver will automatically wait the secondsToImplicitWait for a condition before stopping execution.</param>
        /// <param name="secondsToImplicitWait">Number of seconds to wait for an condition. This is only applicable when enabmeImplicitWait is true.</param>
        public BrowserOptions(BrowserType browser, bool enableImplicitWait = true, int secondsToImplicitWait = 5)
        {
            Driver = new DriverOptions(browser);
            EnableImplicitWait = enableImplicitWait;
            SecondsToImplicitWait = secondsToImplicitWait;
        }

        /// <summary>
        /// Create a BrowserOptions object to configure how the browser will behave.
        /// </summary>
        /// <param name="options">Advanced browser driver configuration options.</param>
        /// <param name="enableImplicitWait">When true, the driver will automatically wait the secondsToImplicitWait for a condition before stopping execution.</param>
        /// <param name="secondsToImplicitWait">Number of seconds to wait for an condition. This is only applicable when enabmeImplicitWait is true.</param>
        public BrowserOptions(DriverOptions options, bool enableImplicitWait = true, int secondsToImplicitWait = 5)
        {
            Driver = options;
            EnableImplicitWait = enableImplicitWait;
            SecondsToImplicitWait = secondsToImplicitWait;
        }

        /// <summary>
        /// Type of browser the driver is configured for.
        /// </summary>
        public BrowserType Browser => Driver.Browser;

        /// <summary>
        /// Type of browser to use.
        /// </summary>
        public DriverOptions Driver { get; set; }

        /// <summary>
        /// The amount of time for a condition to be satasfied before failing.
        /// </summary>
        public bool EnableImplicitWait { get; set; }

        /// <summary>
        /// When EnableImplicitWait is true, the condition will periodically checked until the amount of seconds specified here.
        /// </summary>
        public int SecondsToImplicitWait { get; set; }
    }
}
