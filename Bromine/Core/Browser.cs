using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Bromine.Element;
using Bromine.Verifies;

using OpenQA.Selenium;

using Xunit.Abstractions;

namespace Bromine.Core
{
    /// <inheritdoc cref="IBrowser" />
    public class Browser : IBrowser
    {
        /// <summary>
        /// Launch a requested browser and configuration and log with the provided loggers.
        /// </summary>
        /// <param name="options">Provides advanced browser and driver options.</param>
        /// <param name="logLevel"></param>
        /// <param name="output"></param>
        public Browser(BrowserOptions options = null, LogLevels logLevel = LogLevels.Message, ITestOutputHelper output = null)
        {
            BrowserOptions = options ?? new BrowserOptions(BrowserType.Chrome);

            if (output != null)
            {
                Log = new Log(logLevel, output);
            }

            if (BrowserOptions.LogElementHistory)
            {
                Session = new Session();
            }

            Initialize();
        }

        /// <inheritdoc />
        public Log Log { get; }

        /// <inheritdoc />
        public Session Session { get; }

        /// <inheritdoc />
        public string Url => !Driver.WebDriver.Url.StartsWith("file://") ? Driver.WebDriver.Url : Driver.WebDriver.Url.Replace('/', '\\');

        /// <inheritdoc />
        public string Title => Driver.WebDriver.Title;

        /// <inheritdoc />
        public string Source => Driver.WebDriver.PageSource;

        /// <inheritdoc />
        public Window Window { get; private set; }

        /// <inheritdoc />
        public Point Position => Driver.WebDriver.Manage().Window.Position;

        /// <inheritdoc />
        public Size Size => Driver.WebDriver.Manage().Window.Size;

        /// <inheritdoc />
        public Find Find { get; private set; }

        /// <inheritdoc />
        public SeleniumFind SeleniumFind { get; private set; }

        /// <inheritdoc />
        public Navigate Navigate { get; private set; }

        /// <inheritdoc />
        public BrowserOptions BrowserOptions { get; }

        /// <inheritdoc />
        public ElementStyle ElementStyle { get; private set; }

        /// <inheritdoc />
        public Wait Wait { get; private set; }

        /// <inheritdoc />
        public string Information => Driver.WebDriver.GetType().ToString();

        /// <inheritdoc />
        public Verify Verify { get; private set; }

        /// <inheritdoc />
        public ConditionalVerify ConditionalVerify { get; private set; }

        /// <inheritdoc />
        public SoftVerify SoftVerify { get; private set; }

        /// <inheritdoc />
        public Driver Driver { get; private set; }

        /// <inheritdoc />
        public void TakeElementImage(string name, Element.Element element)
        {
            TakeRegionImage(name, new Rectangle(element.Location, element.Size));
        }

        /// <inheritdoc />
        public void TakeRegionImage(string name, Rectangle imageRegion)
        {
            Bitmap croppedImage;

            TakeVisibleImage(name);

            using (var image = new Bitmap($"{Log.ImagesDirectory}"))
            {
                croppedImage = image.Clone(imageRegion, image.PixelFormat);
            }

            using (var writer = new FileStream(Log.ImagePath(name), FileMode.OpenOrCreate))
            {
                croppedImage.Save(writer, ImageFormat.Png);
            }
        }

        /// <inheritdoc />
        public void TakeVisibleImage(string name)
        {
            try
            {
                Image = Driver.Image;
                Image.SaveAsFile(Log.ImagePath(name), ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        /// <inheritdoc />
        public object ExecuteJs(string script, params object[] arguments)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var js = Driver as IJavaScriptExecutor;

            return js?.ExecuteAsyncScript(script, arguments);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            try
            {
                var didSoftVerifyFail = SoftVerify.HasFailure;
                TakeVisibleImage("Test Complete");

                if (didSoftVerifyFail)
                {
                    Verify.Fail("One or more SoftVerify statements FAILED");
                }

                if (Log.ErrorCount > 0)
                {
                    Log.Debug("REVIEW: One or more errors occured during execution");
                }
            }
            finally
            {
                Driver?.Dispose();
                Log?.Dispose();
            }
        }

        private void Initialize()
        {
            Verify = new Verify(this);
            ConditionalVerify = new ConditionalVerify(this);
            SoftVerify = new SoftVerify(this);

            Driver = new Driver(this);
            if (BrowserOptions.Driver.ImplicitWaitEnabled)
            {
                EnableImplicitWait(BrowserOptions.Driver.SecondsToWait);
            }

            Find = new Find(this);
            SeleniumFind = new SeleniumFind(this);
            Navigate = new Navigate(this);
            Window = new Window(this);
            ElementStyle = new ElementStyle(this);
            Wait = new Wait(this);
        }

        private void EnableImplicitWait(int secondsToWait)
        {
            Driver.WebDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, secondsToWait);
        }

        private Screenshot Image { get; set; }
    }
}
