using System;

using Bromine.Constants;
using Bromine.Core;
using Bromine.Models;

using Tests.Bromine.Common;

using Xunit;

using DriverOptions = Bromine.Models.DriverOptions;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Tests to verify browser types and advanced browser configurations.
    /// </summary>
    public class BrowserTypeTests : IDisposable
    {
        /// <summary>
        /// Test a Chrome browser can be created in both normal and headless (no UI) mode.
        /// </summary>
        /// <param name="browser">Browser to launch.</param>
        /// <param name="isHeadless">When true the UI will not be rendered. This is faster and works close to the same as a standard browser.</param>
        [Trait(Category.Browser, Category.Chrome)]
        [Theory]
        [InlineData(BrowserType.Chrome)]
        [InlineData(BrowserType.Chrome, true)]
        public void InitializeChromeBrowserTest(BrowserType browser, bool isHeadless = false)
        {
            VerifyBrowser(browser, isHeadless);
        }

        /// <summary>
        /// Test a Chrome browser can be created in both normal and headless (no UI) mode.
        /// </summary>
        /// <param name="browser">Browser to launch.</param>
        /// <param name="isHeadless">When true the UI will not be rendered. This is faster and works close to the same as a standard browser.</param>
        [Trait(Category.Browser, Category.Firefox)]
        [Theory]
        [InlineData(BrowserType.Firefox)]
        [InlineData(BrowserType.Firefox, true)]
        public void InitializeFirefoxBrowserTest(BrowserType browser, bool isHeadless = false)
        {
            VerifyBrowser(browser, isHeadless);
        }

        /// <summary>
        /// Test a Chrome browser can be created on a remote machine in both normal and headless (no UI) mode.
        /// NOTE: The Selenium Hub and Node must be launched before this test will work.
        /// TODO: Setup and teardown the Selenium Grid for this test.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="isHeadless"></param>
        [Trait(Category.Browser, Category.Chrome), Trait(Category.Browser, Category.Remote)]
        [Theory]
        [InlineData(BrowserType.Chrome)]
        [InlineData(BrowserType.Chrome, true)]
        public void InitializeRemoteChromeBrowserTest(BrowserType browser, bool isHeadless = false)
        {
            VerifyRemoteBrowser(browser, isHeadless);
        }

        /// <summary>
        /// Test a Firefox browser can be created on a remote machine in both normal and headless (no UI) mode.
        /// NOTE: The Selenium Hub and Node must be launched before this test will work.
        /// TODO: Setup and teardown the Selenium Grid for this test.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="isHeadless"></param>
        [Trait(Category.Browser, Category.Firefox), Trait(Category.Browser, Category.Remote)]
        [Theory]
        [InlineData(BrowserType.Firefox)]
        [InlineData(BrowserType.Firefox, true)]
        public void InitializeRemoteFirefoxBrowserTest(BrowserType browser, bool isHeadless = false)
        {
            VerifyRemoteBrowser(browser, isHeadless);
        }

        /// <summary>
        /// Dispose of the browser.
        /// </summary>
        public void Dispose()
        {
            Browser.Dispose();
        }

        private void VerifyBrowser(BrowserType browser, bool isHeadless)
        {
            BrowserInit(new DriverOptions(browser, isHeadless));

            Browser.Verify.Contains(browser.ToString(), Browser.Information);
        }

        private void VerifyRemoteBrowser(BrowserType browser, bool isHeadless)
        {
            var browserOptions = new BrowserOptions(browser, isHeadless, 0, RemoteAddress);

            Browser = new Browser(browserOptions);

            Browser.Navigate.ToUrl(TestSites.GoogleUrl);
        }

        private void BrowserInit(DriverOptions driverOptions)
        {
            var browserOptions = new BrowserOptions(driverOptions);

            Browser = new Browser(browserOptions);

            Browser.Navigate.ToUrl(TestSites.GoogleUrl);
        }

        private Browser Browser { get; set; }

        private const string RemoteAddress = "localhost:4444";
    }
}
