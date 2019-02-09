using Xunit;

namespace Bromine.Core
{
    /// <summary>
    /// Provide access to browser drivers.
    /// </summary>
    public class DriverTests
    {
        [Fact]
        public void InitializeChromeTest()
        {
            _driver = new Driver(BrowserType.Chrome);

            _driver.NavigateToUrl(googleUrl);

            Assert.Equal(googleUrl, _driver.Url);
        }

        private void Dispose()
        {
            _driver.Dispose();
        }

        private const string googleUrl = "http://www.google.com";

        private Driver _driver;
    }
}
