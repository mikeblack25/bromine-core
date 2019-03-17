using System.IO;

using Xunit;

namespace Tests.Bromine.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Test Browser behavior.
    /// </summary>
    public class BrowserTests : CoreTestsBase
    {
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
    }
}
