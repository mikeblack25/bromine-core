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
        /// <param name="browser">Type of browser to use.</param>
        /// <param name="isHeadless">When true, the UI will not be rendered for the browser.</param>
        /// <param name="remoteHost">Provide a host in the form IP:Port or HostName:Port </param>
        /// <param name="hideDriverWindow">When true the command window will not show for the driver.</param>
        public DriverOptions(BrowserType browser, bool isHeadless = false, string remoteHost = "", bool hideDriverWindow = true)
        {
            Browser = browser;
            IsHeadless = isHeadless;
            HideDriverWindow = hideDriverWindow;

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
    }
}
