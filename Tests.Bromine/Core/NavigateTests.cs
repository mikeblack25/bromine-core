using Bromine.Core;

using OpenQA.Selenium;

using Tests.Bromine.Common;

using Xunit;

namespace Tests.Bromine.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Tests to verify the Navigate class is working as expected.
    /// </summary>
    public class NavigateTests : CoreTestsBase
    {
        /// <summary>
        /// Verify behavior in <see cref="Navigate"/> for the following.
        /// <see cref="Navigate.ToFile"/>
        /// <see cref="Navigate.Back"/>
        /// <see cref="Navigate.Forward"/>
        /// <see cref="Navigate.Refresh"/>
        /// </summary>
        [Fact]
        public void NavigateToFileVerifyBackForwardAndRefreshTest()
        {
            Browser.Navigate.ToFile($@"{BasePath}\{AmazonHome}"); // Verify Navigate.ToFile().
            var originalUrl = Browser.Url;

            CartButton.Click();
            var nextUrl = Browser.Url;

            Assert.Contains("cart", nextUrl);

            Browser.Navigate.Back();

            Assert.Equal(originalUrl, Browser.Url); // Verify Navigate.Back().

            Browser.Navigate.Forward();

            Assert.Equal(nextUrl, Browser.Url); // Verify Navigate.Forward().

            var cartButton = CartButton;

            Assert.True(cartButton.Displayed);

            Browser.Navigate.Refresh();

            Assert.Throws<StaleElementReferenceException>(() => cartButton.Displayed); // Verify Navigate.Refresh().
        }

        /// <summary>
        /// Verify the Browser can navigate to the requested URL.
        /// <see cref="Navigate.ToUrl"/>
        /// </summary>
        [Fact]
        public void NavigateToUrlTest()
        {
            Browser.Navigate.ToUrl(TestSites.AmazonUrl);

            Assert.Equal(TestSites.AmazonUrl, Browser.Url);
        }
    }
}
