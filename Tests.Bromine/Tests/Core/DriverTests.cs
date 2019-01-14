using System;

using Tests.Bromine.Common;

using Xunit;

namespace Bromine.Core
{
    /// <summary>
    /// Test Driver behavior.
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
            _driver = new Driver(browser);

            _driver.NavigateToUrl(googleUrl);

            Assert.Equal(googleUrl, _driver.Url);
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
            _driver = new Driver(browser, true);

            _driver.NavigateToUrl(googleUrl);

            Assert.Equal(googleUrl, _driver.Url);
        }

        void IDisposable.Dispose()
        {
            _driver.Dispose();
        }

        private const string googleUrl = "https://www.google.com/?gws_rd=ssl";
        private Driver _driver;
    }
}
