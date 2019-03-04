using System;
using System.IO;

using Bromine.Core;

using Xunit;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Test Browser behavior.
    /// </summary>
    public class BrowserTests: IDisposable
    {
        /// <summary>
        /// Construct a Browser and test Browser behavior.
        /// </summary>
        public BrowserTests()
        {
            Browser = new Browser(BrowserType.Chrome);
        }

        /// <summary>
        /// Verify navigation to a URL works.
        /// </summary>
        [Fact]
        public void VerifyNavigate()
        {
            Browser.Navigate.ToUrl(AmazonUrl);

            Assert.Equal(AmazonUrl, Browser.Url);
        }

        /// <summary>
        /// Verify page source and title properties return the expected values.
        /// </summary>
        [Fact]
        public void VerifySourceAndTitle()
        {
            Browser.Navigate.ToUrl(AmazonUrl);

            Assert.Contains(Amazon, Browser.Source);
            Assert.Contains(Amazon, Browser.Title);
        }

        /// <summary>
        /// Verify page source and title properties return the expected values.
        /// </summary>
        [Fact]
        public void VerifyScreenshot()
        {
            Browser.Navigate.ToUrl(AmazonUrl);

            Browser.TakeScreenshot("Amazon Test");

            Assert.True(File.Exists(Browser.LastScreenshotPath), $"Unable to find the expected screenshot at {Browser.LastScreenshotPath}");

            File.Delete(Browser.LastScreenshotPath); // Delete screenshot file for the test.
        }

        /// <summary>
        /// Dispose of the Browser resource.
        /// </summary>
        public void Dispose()
        {
            Browser.Dispose();
        }

        private const string Amazon = "Amazon";
        private const string AmazonUrl = "https://www.amazon.com/";
        private Browser Browser { get; }
    }
}
