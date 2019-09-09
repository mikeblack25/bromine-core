using Bromine.Core;

using OpenQA.Selenium;

using Tests.Bromine.Common;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Tests to verify the Navigate class is working as expected.
    /// </summary>
    public class NavigateTests : CoreTestsBase
    {
        /// <inheritdoc />
        public NavigateTests(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// VerifyBase behavior in <see cref="Navigate"/> for the following.
        /// <see cref="Navigate.ToFile"/>
        /// <see cref="Navigate.Back"/>
        /// <see cref="Navigate.Forward"/>
        /// <see cref="Navigate.Refresh"/>
        /// </summary>
        [Fact]
        public void NavigateToFileVerifyBackForwardAndRefreshTest()
        {
            Browser.Navigate.ToFile($@"{BasePath}\{AmazonHome}"); // VerifyBase Navigate.ToFile().
            var originalUrl = Browser.Url;

            CartButton.Click();
            var nextUrl = Browser.Url;

            Browser.Verify.Contains("cart", nextUrl);

            Browser.Navigate.Back();

            Browser.Verify.Equal(originalUrl, Browser.Url); // VerifyBase Navigate.Back().

            Browser.Navigate.Forward();

            Browser.Verify.Equal(nextUrl, Browser.Url); // VerifyBase Navigate.Forward().

            var cartButton = CartButton;

            Browser.Verify.True(cartButton.Displayed);

            Browser.Navigate.Refresh();

            Assert.Throws<StaleElementReferenceException>(() => cartButton.Displayed); // VerifyBase Navigate.Refresh().
        }

        /// <summary>
        /// VerifyBase the Browser can navigate to the requested URL.
        /// <see cref="Navigate.ToUrl"/>
        /// </summary>
        [Fact]
        public void NavigateToUrlTest()
        {
            Browser.Navigate.ToUrl(TestSites.AmazonUrl);

            Browser.Verify.Equal(TestSites.AmazonUrl, Browser.Url);
        }
    }
}
