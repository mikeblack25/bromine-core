using System;

using Bromine.Constants;

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
        /// <param name="browser"><see cref="Browser"/></param>
        /// <param name="isHeadless"><see cref="IsHeadless"/></param>
        /// <param name="remoteHost"><see cref="RemoteHost"/></param>
        /// <param name="hideDriverWindow"><see cref="HideDriverWindow"/></param>
        /// <param name="useDefaultDriverPath"><see cref="UseDefaultDriverPath"/></param>
        public DriverOptions(BrowserType browser, bool isHeadless = false, string remoteHost = "", bool hideDriverWindow = true, bool useDefaultDriverPath = false)
        {
            Browser = browser;
            IsHeadless = isHeadless;
            HideDriverWindow = hideDriverWindow;
            UseDefaultDriverPath = useDefaultDriverPath;

            if (!string.IsNullOrWhiteSpace(remoteHost))
            {
                IsRemoteDriver = true;
                RemoteHost = new Uri($"http://{remoteHost}/wd/hub");
            }
        }

        /// <summary>
        /// Type of browser to use.
        /// </summary>
        public BrowserType Browser { get; }

        /// <summary>
        /// Will the driver be executed on a remote machine?
        /// </summary>
        public bool IsRemoteDriver { get; }

        /// <summary>
        /// Address of the remote Selenium Grid.
        /// </summary>
        public Uri RemoteHost { get; }

        /// <summary>
        /// When true the UI will not be rendered. This saves time and system resources.
        /// </summary>
        public bool IsHeadless { get; }

        /// <summary>
        /// When true the command window will not show when the browser driver is active.
        /// </summary>
        public bool HideDriverWindow { get; }

        /// <summary>
        /// When true the driver will be used from the executing directory.
        /// This is required if used by .NET Core applications.
        /// </summary>
        public bool UseDefaultDriverPath { get; }
    }
}
