using System;

using Bromine.Constants;
using Bromine.Core;
using Bromine.Models;

using OpenQA.Selenium;

using Tests.Bromine.Common;
using Xunit;

using DriverOptions = Bromine.Models.DriverOptions;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Tests to verify browser types and advanced browser configurations.
    /// </summary>
    public class BrowserTypeTests
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

        private void VerifyBrowser(BrowserType browser, bool isHeadless)
        {
            BrowserInit(new DriverOptions(browser, isHeadless));

            Browser.Verify.Contains(browser.ToString(), Browser.Information);
        }

        private void BrowserInit(DriverOptions driverOptions)
        {
            var browserOptions = new BrowserOptions(driverOptions);

            try
            {
                Browser = new Browser(browserOptions);

                Browser.Navigate.ToUrl(TestSites.GoogleUrl);

                Browser.Verify.Equal(TestSites.GoogleUrl, Browser.Url);
            }
            catch (WebDriverException e)
            {
                // The driver is not loaded on the computer.
                if (e.Message.Contains("Cannot start the driver service on"))
                {
                    Browser.Exceptions.Add(e);
                }
                else
                {
                    throw;
                }
            }
            catch (InvalidOperationException e)
            {
                // The driver is not loaded on the computer.
                if (e.Message.Contains(
                    "Expected browser binary location, but unable to find binary in default location"))
                {
                    Browser.Exceptions.Add(e);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                Browser.Exceptions.Add(e);

                throw;
            }
            finally
            {
                Browser?.Dispose();
            }
        }

        private Browser Browser { get; set; }
    }
}
