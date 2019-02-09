using System;

using Xunit;

namespace Bromine.Core
{
    /// <summary>
    /// Test Browser behavior.
    /// </summary>
    public class BrowserTests: IDisposable
    {
        public BrowserTests()
        {
            _browser = new Browser(BrowserType.Chrome);
        }

        /// <summary>
        /// Verify navigation works.
        /// </summary>
        [Fact]
        public void VerifyNavigate()
        {
            _browser.Navigate.ToUrl(amazonUrl);

            Assert.Equal(amazonUrl, _browser.Url);
        }

        /// <summary>
        /// Verify page source works.
        /// </summary>
        [Fact]
        public void VerifySourceAndTitle()
        {
            _browser.Navigate.ToUrl(amazonUrl);

            Assert.Contains(amazon, _browser.Source);
            Assert.Contains(amazon, _browser.Title);
        }

        void IDisposable.Dispose()
        {
            _browser.Dispose();
        }

        private const string amazon = "Amazon";
        private const string amazonUrl = "https://www.amazon.com/";
        private Browser _browser;
    }
}
