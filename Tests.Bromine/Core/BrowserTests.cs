using System.Drawing;
using System.IO;

using Bromine.Core;

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
        /// Navigate to <see cref="CoreTestsBase.AmazonUrl"/>.
        /// Verify Browser.Source contains <see cref="CoreTestsBase.Amazon" />.
        /// Verify Browser.Title contains <see cref="CoreTestsBase.Amazon" />.
        /// </summary>
        [Fact]
        public void VerifySourceAndTitle()
        {
            Browser.Navigate.ToUrl(AmazonUrl);

            Assert.Contains(Amazon, Browser.Source);
            Assert.Contains(Amazon, Browser.Title);
        }

        /// <summary>
        /// Navigate to <see cref="CoreTestsBase.AmazonUrl"/>.
        /// Take a screenshot of the visible page.
        /// Verify the file <see cref="Browser.LastScreenshotPath"/> exists.
        /// Delete the file located <see cref="Browser.LastScreenshotPath"/>.
        /// </summary>
        [Fact]
        public void VerifyScreenshot()
        {
            Browser.Navigate.ToUrl(AmazonUrl);

            Browser.TakeVisibleScreenshot("Amazon Visible Page Screenshot");

            Assert.True(File.Exists(Browser.LastScreenshotPath), $"Unable to find the expected screenshot at {Browser.LastScreenshotPath}");

            File.Delete(Browser.LastScreenshotPath); // Delete screenshot file for the test.
        }

        /// <summary>
        /// Navigate to <see cref="CoreTestsBase.AmazonUrl"/>.
        /// Take a screenshot of the visible page.
        /// Take a screenshot of a region of the screen with a width and height of 50 pixels.
        /// Verify the saved image <see cref="Browser.LastImageSize"/> is the expected size.
        /// TODO: Investigate why Browser.LastImage is locked and can't be deleted.
        /// </summary>
        [Fact]
        public void VerifyRegionScreenshot()
        {
            var initialPosition = new Point(0, 0);
            var regionSize = new Size(50, 50);
            var region = new Rectangle(initialPosition, regionSize);

            Browser.Navigate.ToUrl(AmazonUrl);

            Browser.TakeRegionScreenshot("Amazon Region Screenshot", region);

            Assert.Equal(regionSize, Browser.LastImageSize);

            // File.Delete(Browser.LastScreenshotPath);
        }

        /// <summary>
        /// Navigate to <see cref="CoreTestsBase.AmazonUrl"/>.
        /// Maximize window to find the element in question.
        /// Take a screenshot of the element on the page.
        /// Verify the saved image <see cref="Browser.LastImageSize"/> is the expected size.
        /// TODO: Investigate why Browser.LastImage is locked and can't be deleted.
        /// </summary>
        [Fact]
        public void VerifyElementScreenshot()
        {
            var regionSize = new Size(98, 44);

            Browser.Navigate.ToUrl(AmazonUrl);

            Browser.Maximize();

            Browser.TakeElementScreenshot("Amazon Element Screenshot", CartButton);

            Assert.Equal(regionSize, Browser.LastImageSize);

            // File.Delete(Browser.LastScreenshotPath);
        }
    }
}
