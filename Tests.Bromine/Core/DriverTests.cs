using System;

using Bromine.Constants;
using Bromine.Core;
using Bromine.Models;
using Tests.Bromine.Common;

using OpenQA.Selenium;

using Xunit;

namespace Tests.Bromine.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Test the behavior of the Driver class.
    /// </summary>
    public class DriverTests: IDisposable
    {
        /// <summary>
        /// Test default Driver constructor.
        /// </summary>
        /// <param name="browser">Browser to launch.</param>
        [Theory]
        [InlineData(BrowserType.Chrome), Trait(Category.Browser, Category.Chrome)]
        [InlineData(BrowserType.Edge), Trait(Category.Browser, Category.Edge)]
        [InlineData(BrowserType.Firefox), Trait(Category.Browser, Category.Firefox)]
        public void InitializeBrowserDefaultsTest(BrowserType browser)
        {
            var driverOptions = new BrowserConfiguration(browser);

            BrowserInit(driverOptions);
        }

        /// <summary>
        /// Test Driver constructor with headless mode.
        /// </summary>
        /// <param name="browser">Browser to launch.</param>
        [Theory]
        [InlineData(BrowserType.Chrome), Trait(Category.Browser, Category.Chrome)]
        [InlineData(BrowserType.Firefox), Trait(Category.Browser, Category.Firefox)]
        public void InitializeBrowserIsHeadlessTest(BrowserType browser)
        {
            var driverOptions = new BrowserConfiguration(browser, true);

            BrowserInit(driverOptions);
        }

        /// <inheritdoc />
        /// <summary>
        /// Dispose of the Driver resource.
        /// </summary>
        public void Dispose()
        {
            Driver?.Dispose();
        }

        private void BrowserInit(BrowserConfiguration browserConfiguration)
        {
            try
            {
                Driver = new Driver(browserConfiguration);

                Driver.NavigateToUrl(GoogleUrl);

                Assert.Equal(GoogleUrl, Driver.Url);
            }
            catch (WebDriverException e)
            {
                // The driver is not loaded on the computer.
                if (e.Message.Contains("Cannot start the driver service on"))
                {

                }
                else
                {
                    throw;
                }
            }
            catch (InvalidOperationException e)
            {
                // The driver is not loaded on the computer.
                if (e.Message.Contains("Expected browser binary location, but unable to find binary in default location"))
                {

                }
                else
                {
                    throw;
                }
            }
        }

        private const string GoogleUrl = "https://www.google.com/?gws_rd=ssl";
        private Driver Driver { get; set; }
    }
}
