using Bromine.Constants;

using Xunit;
using static Xunit.Assert;

namespace Tests.Bromine.Page.Google
{
    /// <summary>
    /// Home tests to show how tests can be structured to leverage common setup and teardown to simplify the test flow.
    /// </summary>
    public class HomeTests : PageBase
    {
        /// <summary>
        /// Navigate to <see cref="Home.Url"/>
        /// </summary>
        public HomeTests()
        {
            InitializePages(BrowserType.Chrome);
            Home.Navigate();
        }

        /// <summary>
        /// Click the About link.
        /// </summary>
        [Fact]
        public void ClickAboutLink()
        {
            Home.AboutLink.Click();

            Contains("https://about.google", Browser.Url);
        }

        /// <summary>
        /// Click the Store link.
        /// </summary>
        [Fact]
        public void ClickStoreLink()
        {
            Home.StoreLink.Click();

            Contains("https://store.google", Browser.Url);
        }

        /// <summary>
        /// Click the Gmail link.
        /// </summary>
        [Fact]
        public void ClickGmailLink()
        {
            Home.GmailLink.Click();

            Contains("https://www.google.com/gmail", Browser.Url);
        }

        /// <summary>
        /// Click the Images link.
        /// </summary>
        [Fact]
        public void ClickImagesLink()
        {
            Home.ImagesLink.Click();

            Contains("https://www.google.com/imghp", Browser.Url);
        }

        /// <summary>
        /// Check the alt tag on the Google image.
        /// </summary>
        [Fact]
        public void GoogleImageAttribute()
        {
            var googleImage = Home.GoogleImage;

            Contains("Google", googleImage.GetAttribute("alt"));
        }

        /// <summary>
        /// Search for 'cat' on Google.
        /// </summary>
        [Fact]
        public void GoogleSearch()
        {
            Home.Search("cat");

            Contains("q=cat", Browser.Url);
        }
    }
}
