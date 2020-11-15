using System;

namespace Bromine.Core
{
    /// <summary>
    /// Allows configuration of the driver used to launch a browser.
    /// </summary>
    public class DriverOptions
    {
        /// <summary>
        /// Configure a driver to launch a web browser.
        /// By default a Chrome browser will be launched with a UI.
        /// </summary>
        /// <param name="browser">Browser to configure the driver for.</param>
        /// <param name="isHeadless">When true the UI will not be rendered. This will execute faster.</param>
        /// <param name="secondsToWait">When > 0, Selenium actions will wait up to the specified amount of time in seconds (Implicit Wait).</param>
        /// <param name="remoteAddress">Host address of the Selenium Grid.</param>
        /// <param name="useDefaultDriverPath">When true, the driver will be located in the default path. This is required if used with .NET Core.</param>
        /// <param name="hideDriverWindow">Hide the window of the web driver during execution. Drivers may have to be manually closed if not disposed properly.</param>
        public DriverOptions(BrowserType browser = BrowserType.Chrome,
                             bool isHeadless = false,
                             int secondsToWait = 0,
                             string remoteAddress = "",
                             bool useDefaultDriverPath = false,
                             bool hideDriverWindow = true)
        {
            Browser = browser;
            IsHeadless = isHeadless;
            SecondsToWait = secondsToWait;

            if (!string.IsNullOrWhiteSpace(remoteAddress))
            {
                IsRemoteDriver = true;
                RemoteAddress = new Uri($"http://{remoteAddress}/wd/hub");
            }

            UseDefaultDriverPath = useDefaultDriverPath;
            HideDriverWindow = hideDriverWindow;
        }

        /// <summary>
        /// Type of browser to use.
        /// <see cref="BrowserType"/>
        /// </summary>
        public BrowserType Browser { get; }

        /// <summary>
        /// When true the UI will not be rendered. This saves time and system resources.
        /// </summary>
        public bool IsHeadless { get; }

        /// <summary>
        /// Will the driver be executed on a remote machine?
        /// </summary>
        public bool IsRemoteDriver { get; }

        /// <summary>
        /// Address of the remote Selenium Grid.
        /// </summary>
        public Uri RemoteAddress { get; }

        /// <summary>
        /// The number of seconds to implicitly wait for a web action to happen.
        /// This will not wait the specified time if the requested action is successful earlier.
        /// </summary>
        public int SecondsToWait { get; set; }

        /// <summary>
        /// When true the driver will be used from the executing directory.
        /// This is required if used by .NET Core applications.
        /// </summary>
        public bool UseDefaultDriverPath { get; }

        /// <summary>
        /// When true the command window will not show when the browser driver is active.
        /// </summary>
        public bool HideDriverWindow { get; }
    }
}
