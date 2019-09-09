using System.Drawing;
using System.IO;

using Bromine.Constants;
using Bromine.Core;
using Bromine.Models;

using Tests.Bromine.Common;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Tests to verify the Browser class is working as expected.
    /// </summary>
    public class BrowserTests : CoreTestsBase
    {
        /// <inheritdoc />
        public BrowserTests(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// Navigate to <see cref="TestSites.AmazonUrl"/>.
        /// VerifyBase Browser.Source contains <see cref="CoreTestsBase.Amazon" />.
        /// VerifyBase Browser.Title contains <see cref="CoreTestsBase.Amazon" />.
        /// </summary>
        [Fact]
        public void VerifySourceAndTitle()
        {
            Browser.Navigate.ToUrl(TestSites.AmazonUrl);

            Browser.Verify.Contains(Amazon, Browser.Source);
            Browser.Verify.Contains(Amazon, Browser.Title);
        }

        /// <summary>
        /// Dispose of the default browser and create a new Browser in the test to verify A browser can be created with a reference to another image save path.
        /// Navigate to <see cref="TestSites.AmazonUrl"/>.
        /// Take a ScreenShot of the visible page.
        /// VerifyBase the file <see cref="Browser.ScreenShotDirectory"/> exists.
        /// Delete the file located <see cref="Browser.ScreenShotDirectory"/>.
        /// </summary>
        [Fact]
        public void VerifyScreenShot()
        {
            var path = global::Bromine.Core.Browser.DefaultImagePath;
            var testDirectory = $@"{path}\Directory Test";
            var name = "Amazon Visible Page ScreenShot";
            var browserOptions = new BrowserOptions(BrowserType.Chrome, false, 0, string.Empty, testDirectory);

            Dispose(); // Close the default driver created on startup.
            DeleteDirectory(testDirectory); // Clear the initial directory to create one during the Browser init.

            var browser = new Browser(browserOptions);

            DeleteInitialImage(name);

            browser.Navigate.ToUrl(TestSites.AmazonUrl);
            browser.TakeVisibleScreenShot(name);

            Browser.Verify.True(File.Exists(browser.ScreenShotPath), $"Unable to find the expected ScreenShot at {browser.ScreenShotPath}");

            browser.Dispose();
        }

        /// <summary>
        /// Navigate to <see cref="TestSites.AmazonUrl"/>.
        /// Take a ScreenShot of the visible page.
        /// Take a ScreenShot of a region of the screen with a width and height of 50 pixels.
        /// VerifyBase the saved image <see cref="Browser.LastImageSize"/> is the expected size.
        /// TODO: Investigate why Browser.LastImage is locked and can't be deleted.
        /// </summary>
        [Fact]
        public void VerifyRegionScreenShot()
        {
            var initialPosition = new Point(0, 0);
            var regionSize = new Size(50, 50);
            var region = new Rectangle(initialPosition, regionSize);

            const string name = "Amazon Region ScreenShot";
            DeleteInitialImage(name);

            Browser.Navigate.ToUrl(TestSites.AmazonUrl);

            Browser.TakeRegionScreenShot(name, region);

            Browser.Verify.Equal(regionSize, Browser.LastImageSize);
        }

        /// <summary>
        /// Navigate to <see cref="TestSites.AmazonUrl"/>.
        /// Maximize window to find the element in question.
        /// Take a ScreenShot of the element on the page.
        /// VerifyBase the saved image <see cref="Browser.LastImageSize"/> is the expected size.
        /// TODO: Investigate why Browser.LastImage is locked and can't be deleted.
        /// </summary>
        [Fact]
        public void VerifyElementScreenShot()
        {
            var regionSize = new Size(98, 44);

            const string name = "Amazon Element ScreenShot";
            DeleteInitialImage(name);

            Browser.Navigate.ToUrl(TestSites.AmazonUrl);

            Browser.Window.Maximize();

            Browser.TakeElementScreenShot(name, CartButton);

            Browser.Verify.Equal(regionSize, Browser.LastImageSize);
        }

        /// <summary>
        /// Navigate to <see cref="TestSites.AmazonUrl"/>.
        /// Try to save a ScreenShot to an invalid name.
        /// VerifyBase <see cref="Browser.LastImage"/> returns null and an exception is logged when an invalid path is selected.
        /// </summary>
        [Fact]
        public void VerifyTakeVisibleScreenShotError()
        {
            Browser.Verify.Empty(Browser.Log.XunitAppender.Logs);

            Browser.Navigate.ToUrl(TestSites.AmazonUrl);
            Browser.TakeVisibleScreenShot(@"-\\\\--");

            Browser.Verify.NotEmpty(Browser.Log.XunitAppender.Logs);

            var exceptionCount = Browser.Log.ErrorCount;

            Browser.Verify.Null(Browser.LastImage);
            Browser.Verify.Equal(++exceptionCount, Browser.Log.ErrorCount);
        }

        /// <summary>
        /// VerifyBase the behavior of <see cref="Browser.Window"/>.
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

            Browser.Verify.NotEqual(windowSize, windowSize = Browser.Window.Size);
            Browser.Verify.NotEqual(position, position = Browser.Window.Position);

            Browser.Window.Minimize();

            Browser.Verify.NotEqual(windowSize, windowSize = Browser.Window.Size);
            Browser.Verify.NotEqual(position, position = Browser.Window.Position);

            Browser.Window.FullScreen();

            Browser.Verify.NotEqual(windowSize, Browser.Window.Size);
            Browser.Verify.NotEqual(position, Browser.Window.Position);

            windowSize = new Size(100, 100);
            Browser.Window.Size = windowSize;

            Browser.Verify.InRange(windowSize.Width, windowSize.Width, 600);
            Browser.Verify.InRange(windowSize.Height, windowSize.Height, 150);

            position = new Point(25, 75);
            Browser.Window.Position = position;

            Browser.Verify.Equal(position, Browser.Window.Position);
        }

        private void DeleteInitialImage(string name)
        {
            var path = $@"{Browser.ScreenShotDirectory}\{name}.png";

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
