using System;
using System.Drawing;
using System.IO;

using Bromine.Constants;
using Bromine.Core;
using Bromine.Models;

using Xunit;

using static Xunit.Assert;

namespace Tests.Bromine.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Tests to verify the functionality of the Browser class is working as expected.
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

            Contains(Amazon, Browser.Source);
            Contains(Amazon, Browser.Title);
        }

        /// <summary>
        /// Dispose of the default browser and create a new Browser in the test to verify A browser can be created with a reference to another image save path.
        /// Navigate to <see cref="CoreTestsBase.AmazonUrl"/>.
        /// Take a screenshot of the visible page.
        /// Verify the file <see cref="Browser.ScreenshotPath"/> exists.
        /// Delete the file located <see cref="Browser.ScreenshotPath"/>.
        /// </summary>
        [Fact]
        public void VerifyScreenshot()
        {
            var path = global::Bromine.Core.Browser.DefaultImagePath;
            var testDirectory = $@"{path}\Directory Test";
            var name = "Amazon Visible Page Screenshot";
            var browserOptions = new BrowserOptions(BrowserType.Chrome, 0, testDirectory);

            Dispose(); // Close the default driver created on startup.
            DeleteDirectory(testDirectory); // Clear the initial directory to create one during the Browser init.

            var browser = new Browser(browserOptions);

            DeleteInitialImage(name);

            browser.Navigate.ToUrl(AmazonUrl);
            browser.TakeVisibleScreenshot(name);

            True(File.Exists(browser.ScreenshotPath), $"Unable to find the expected screenshot at {browser.ScreenshotPath}");

            browser.Dispose();
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

            const string name = "Amazon Region Screenshot";
            DeleteInitialImage(name);

            Browser.Navigate.ToUrl(AmazonUrl);

            Browser.TakeRegionScreenshot(name, region);

            Equal(regionSize, Browser.LastImageSize);
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

            const string name = "Amazon Element Screenshot";
            DeleteInitialImage(name);

            Browser.Navigate.ToUrl(AmazonUrl);

            Browser.Window.Maximize();

            Browser.TakeElementScreenshot(name, CartButton);

            Equal(regionSize, Browser.LastImageSize);
        }

        /// <summary>
        /// Navigate to <see cref="CoreTestsBase.AmazonUrl"/>.
        /// Try to save a screenshot to an invalid name.
        /// Verify <see cref="Browser.Exceptions"/> is not empty trying to save an invalid file name.>
        /// Verify <see cref="Browser.LastImage"/> returns null and an exception is logged when an invalid path is selected.
        /// </summary>
        [Fact]
        public void VerifyTakeVisibleScreenshotError()
        {
            Empty(Browser.Exceptions);

            Browser.Navigate.ToUrl(AmazonUrl);
            Browser.TakeVisibleScreenshot(@"-\\\\--");

            NotEmpty(Browser.Exceptions);

            var exceptionCount = Browser.Exceptions.Count;

            Null(Browser.LastImage);
            Equal(++exceptionCount, Browser.Exceptions.Count);
        }

        /// <summary>
        /// Verify the behavior of <see cref="Browser.Window"/>.
        /// <see cref="Window.Maximize"/>
        /// <see cref="Window.Minimize"/>
        /// <see cref="Window.FullScreen"/>
        /// <see cref="Window.Size"/>
        /// <see cref="Window.Position"/>
        /// </summary>
        [Fact]
        public void VerifyBrowserResize()
        {
            var windowSize = Browser.Window.Size;
            var position = Browser.Window.Position;

            Browser.Window.Maximize();

            NotEqual(windowSize, windowSize = Browser.Window.Size);
            NotEqual(position, position = Browser.Window.Position);

            Browser.Window.Minimize();

            NotEqual(windowSize, windowSize = Browser.Window.Size);
            NotEqual(position, position = Browser.Window.Position);

            Browser.Window.FullScreen();

            NotEqual(windowSize, Browser.Window.Size);
            NotEqual(position, Browser.Window.Position);

            windowSize = new Size(100, 100);
            Browser.Window.Size = windowSize;

            InRange(windowSize.Width, windowSize.Width, 600);
            InRange(windowSize.Height, windowSize.Height, 150);

            position = new Point(25, 75);
            Browser.Window.Position = position;

            Equal(position, Browser.Window.Position);
        }

        /// <summary>
        /// Verify the browser can wait for conditions for the time specified and that Exceptions are logged when the expected conditions are not met.
        /// </summary>
        [Fact]
        public void VerifyBrowserWait()
        {
            Empty(Browser.Exceptions);
            False(Browser.Wait(() => false));
            NotEmpty(Browser.Exceptions);

            var startTime = DateTime.Now;
            const int timeToWait = 5;

            Browser.Wait(() => false, timeToWait);

            InRange(DateTime.Now, startTime.AddSeconds(timeToWait - 1), startTime.AddSeconds(timeToWait + 1));

            var exceptionCount = Browser.Exceptions.Count;

            True(Browser.Wait(() => true));
            Equal(exceptionCount, Browser.Exceptions.Count);
        }

        private void DeleteInitialImage(string name)
        {
            var path = $@"{Browser.ScreenshotPath}\{name}.png";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
    }
}
