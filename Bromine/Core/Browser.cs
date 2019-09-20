using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Bromine.Core.ElementInteraction;
using Bromine.Core.ElementLocator;
using Bromine.Logger;
using Bromine.Models;
using Bromine.Verifies;

using OpenQA.Selenium;

using Xunit.Abstractions;

namespace Bromine.Core
{
    /// <inheritdoc cref="IBrowser" />
    public class Browser : IBrowser
    {
        /// <summary>
        /// Launch a Chrome browser with the default configuration.
        /// NOTE: No logging is supported in this configuration.
        /// </summary>
        public Browser() : this(new BrowserOptions())
        {
        }

        /// <summary>
        /// Launch a Chrome browser with the default configuration and console logging for Xunit.
        /// </summary>
        /// <param name="output"><see cref="ITestOutputHelper"/></param>
        public Browser(ITestOutputHelper output) : this(new BrowserOptions(), string.Empty, output)
        {
        }

        /// <summary>
        /// Provides methods of interacting with the web browser.
        /// </summary>
        /// <param name="options">Provides advanced browser and driver options.</param>
        /// <param name="logFileName">Level of information to log.</param>
        /// <param name="output"><see cref="ITestOutputHelper"/></param>
        public Browser(BrowserOptions options, string logFileName = "", ITestOutputHelper output = null)
        {
            BrowserOptions = options;

            Log = new Log(logFileName, output);
            Verify = new Verify(Log);
            ConditionalVerify = new ConditionalVerify(Log);
            SoftVerify = new SoftVerify(Log);

            Verify.VerifyFailed += VerifyOnVerifyFailed;

            Driver = new Driver(BrowserOptions.Driver, Log);
            if (BrowserOptions.Driver.ImplicitWaitEnabled)
            {
                EnableImplicitWait(options.Driver.SecondsToWait);
            }

            Find = new Find(Driver);
            SeleniumFind = new SeleniumFind(Driver);
            Navigate = new Navigate(Driver);
            Window = new Window(Driver);
            ElementStyle = new ElementStyle(this);
            Wait = new Wait(this);

            InitializeScreenShotDirectory(options.Driver.ScreenShotPath);
        }

        /// <inheritdoc />
        public Log Log { get; }

        /// <inheritdoc />
        public string Url => Driver.WebDriver.Url;

        /// <inheritdoc />
        public string Title => Driver.WebDriver.Title;

        /// <inheritdoc />
        public string Source => Driver.WebDriver.PageSource;

        /// <inheritdoc />
        public ILogs SeleniumLogs => Driver.WebDriver.Manage().Logs;

        /// <inheritdoc />
        public ICookieJar Cookies => Driver.WebDriver.Manage().Cookies;

        /// <inheritdoc />
        public IWindow Window { get; }

        /// <inheritdoc />
        public Point Position => Driver.WebDriver.Manage().Window.Position;

        /// <inheritdoc />
        public Size Size => Driver.WebDriver.Manage().Window.Size;

        /// <inheritdoc />
        public Find Find { get; }

        /// <inheritdoc />
        public SeleniumFind SeleniumFind { get; }

        /// <inheritdoc />
        public Navigate Navigate { get; }

        /// <inheritdoc />
        public BrowserOptions BrowserOptions { get; }

        /// <inheritdoc />
        public ElementStyle ElementStyle { get; }

        /// <inheritdoc />
        public Wait Wait { get; }

        /// <inheritdoc />
        public string ScreenShotDirectory => _screenShotDirectory;

        /// <inheritdoc />
        public string ScreenShotName { get; set; }

        /// <inheritdoc />
        public string ScreenShotPath => $@"{ScreenShotDirectory}\{ScreenShotName}";

        /// <inheritdoc />
        public Image LastImage
        {
            get
            {
                try
                {
                    return Image.FromFile(ScreenShotPath);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);

                    return null;
                }
            }
        }

        /// <inheritdoc />
        public Size LastImageSize
        {
            get
            {
                var size = new Size();

                if (LastImage != null)
                {
                    size = new Size(LastImage.Size.Width, LastImage.Size.Height);
                }

                return size;
            }
        }

        /// <summary>
        /// The path where images will be stored.
        /// </summary>
        public static string DefaultImagePath => $@"{AppDomain.CurrentDomain.BaseDirectory}\{ScreenShotsDirectory}";

        /// <inheritdoc />
        public string Information => Driver.WebDriver.GetType().ToString();

        /// <inheritdoc />
        public Verify Verify { get; }

        /// <inheritdoc />
        public ConditionalVerify ConditionalVerify { get; }

        /// <inheritdoc />
        public SoftVerify SoftVerify { get; }

        internal Driver Driver { get; }

        /// <inheritdoc />
        public void TakeElementScreenShot(string name, Element element)
        {
            TakeRegionScreenShot(name, new Rectangle(element.Location, element.Size));
        }

        /// <inheritdoc />
        public void TakeRegionScreenShot(string name, Rectangle screenShotRegion)
        {
            Bitmap croppedImage;

            TakeVisibleScreenShot(name);

            using (var image = new Bitmap(ScreenShotPath))
            {
                croppedImage = image.Clone(screenShotRegion, image.PixelFormat);
            }

            using (var writer = new FileStream(ScreenShotPath, FileMode.OpenOrCreate))
            {
                croppedImage.Save(writer, ImageFormat.Png);
            }
        }

        /// <inheritdoc />
        public void TakeVisibleScreenShot(string name)
        {
            ScreenShotName = $"{name}.png";

            try
            {
                ScreenShot = Driver.ScreenShot;
                ScreenShot.SaveAsFile(ScreenShotPath, ScreenshotImageFormat.Png);
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
            var didSoftVerifyFail = SoftVerify.HasFailure;
            SoftVerify.Dispose();
            Driver?.Dispose();

            if (didSoftVerifyFail)
            {
                Verify.False(true, "One or more Verify statements failed. See the logs for more details.");
            }

            Log.Dispose();

        }

        private void EnableImplicitWait(int secondsToWait)
        {
            Driver.WebDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, secondsToWait);
        }

        private void InitializeScreenShotDirectory(string path = "")
        {
            if (string.IsNullOrWhiteSpace(path)) // Create the logs where the app is running.
            {
                path = $@"{AppDomain.CurrentDomain.BaseDirectory}\{ScreenShotsDirectory}";
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            _screenShotDirectory = path;
        }

        private void VerifyOnVerifyFailed(Exception e, VerifyFailedEvent args)
        {
            Dispose();

            if (args.Type != ConditionalVerify.Type) { throw e; }
        }

        private Screenshot ScreenShot { get; set; }

        private static string ScreenShotsDirectory => "ScreenShots";
        private string _screenShotDirectory;
    }
}
