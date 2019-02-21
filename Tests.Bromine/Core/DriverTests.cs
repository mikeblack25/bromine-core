using System;

using Bromine.Core;
using Bromine.Models;

using Tests.Bromine.Common;
using Xunit;

namespace Tests.Bromine.Core
{
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
            var driverOptions = new DriverOptions(browser);
            Driver = new Driver(driverOptions);

            Driver.NavigateToUrl(GoogleUrl);

            Assert.Equal(GoogleUrl, Driver.Url);
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
            var driverOptions = new DriverOptions(browser, true);
            Driver = new Driver(driverOptions);

            Driver.NavigateToUrl(GoogleUrl);

            Assert.Equal(GoogleUrl, Driver.Url);
        }

        /// <summary>
        /// Dispose of the Driver resource.
        /// </summary>
        public void Dispose()
        {
            Driver?.Dispose();
        }

        private const string GoogleUrl = "https://www.google.com/?gws_rd=ssl";
        private Driver Driver { get; set; }
    }
}
