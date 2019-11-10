using Bromine.Constants;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Page.PageTests
{
    /// <summary>
    /// Home tests to show how tests can be structured to leverage common setup and teardown to simplify the test flow.
    /// </summary>
    public class HomeTests : PageBase
    {
        /// <summary>
        /// Navigate to <see cref="GoogleHome.Url"/>
        /// </summary>
        public HomeTests(ITestOutputHelper output)
        {
            InitializePages(BrowserType.Chrome, false, output);
            GoogleHome.Navigate();
        }

        /// <summary>
        /// Click the About link.
        /// </summary>
        [Fact]
        public void ClickAboutLink()
        {
            GoogleHome.AboutLink.Click();

            Browser.Verify.Contains("https://about.google", Browser.Url);
        }

        /// <summary>
        /// Click the Store link.
        /// </summary>
        [Fact]
        public void ClickStoreLink()
        {
            GoogleHome.StoreLink.Click();

            Browser.Verify.Contains("https://store.google", Browser.Url);
        }

        /// <summary>
        /// Click the Gmail link.
        /// </summary>
        [Fact]
        public void ClickGmailLink()
        {
            GoogleHome.GmailLink.Click();

            Browser.Verify.Contains("https://www.google.com/gmail", Browser.Url);
        }

        /// <summary>
        /// Click the Images link.
        /// </summary>
        [Fact]
        public void ClickImagesLink()
        {
            GoogleHome.ImagesLink.Click();

            Browser.Verify.Contains("https://www.google.com/imghp", Browser.Url);
        }

        /// <summary>
        /// Check the alt tag on the Google image.
        /// </summary>
        [Fact]
        public void GoogleImageAttribute()
        {
            var googleImage = GoogleHome.GoogleImage;

            Browser.Verify.Contains("Google", googleImage.GetAttribute("alt"));
        }

        /// <summary>
        /// Search for 'cat' on Google.
        /// </summary>
        [Fact]
        public void GoogleSearch()
        {
            GoogleHome.Search("cat");

            Browser.Verify.Contains("q=cat", Browser.Url);
        }
    }
}
