using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Bromine.Core.Element;
using Bromine.Logger;
using Bromine.Models;
using Bromine.Verifies;

using OpenQA.Selenium;

using Xunit.Abstractions;

using LogType = Bromine.Logger.LogType;

namespace Bromine.Core
{
    /// <inheritdoc cref="IBrowser" />
    public class Browser : IBrowser
    {
        /// <summary>
        /// Launch a Chrome browser with the default configuration and the requested loggers.
        /// </summary>
        public Browser(params LogType[] loggers) : this(new BrowserOptions(), string.Empty, loggers)
        {
        }

        /// <summary>
        /// Launch a requested browser and configuration and log with the provided loggers.
        /// </summary>
        /// <param name="options">Provides advanced browser and driver options.</param>
        /// <param name="fileName">File name for the log file.</param>
        /// <param name="loggers"></param>
        public Browser(BrowserOptions options, string fileName = "", params LogType[] loggers)
        {
            BrowserOptions = options;

            Initialize(fileName, null, loggers);
        }

        /// <summary>
        /// Launch a requested browser and configuration and log with the provided loggers.
        /// Xunit console logging is provided through this constructor.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="output"></param>
        /// <param name="loggers"></param>
        public Browser(BrowserOptions options, ITestOutputHelper output, params LogType[] loggers)
        {
            BrowserOptions = options;

            Initialize(string.Empty, output, loggers);
        }

        /// <inheritdoc />
        public Log Log { get; private set; }

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
        public IWindow Window { get; private set; }

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
        public string ScreenShotDirectory { get; private set; }

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
        public Verify Verify { get; private set; }

        /// <inheritdoc />
        public ConditionalVerify ConditionalVerify { get; private set; }

        /// <inheritdoc />
        public SoftVerify SoftVerify { get; private set; }

        internal Driver Driver { get; private set; }

        /// <inheritdoc />
        public void TakeElementScreenShot(string name, Element.Element element)
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
            try
            {
                var didSoftVerifyFail = SoftVerify.HasFailure;

                if (didSoftVerifyFail)
                {
                    Verify.Fail("One or more SoftVerify statements FAILED");
                }
                if (Log.XunitConsoleLog.ErrorCount > 0)
                {
                    Log.Message("REVIEW: One or more errors occured during execution");
                }
            }
            finally
            {
                Driver?.Dispose();
                Log?.Dispose();
            }
        }

        private void Initialize(string fileName, ITestOutputHelper output, params LogType[] loggers)
        {
            Log = new Log(output, fileName, loggers);

            Verify = new Verify(Log);
            ConditionalVerify = new ConditionalVerify(Log);
            SoftVerify = new SoftVerify(Log);

            Verify.VerifyFailed += VerifyOnVerifyFailed;

            Driver = new Driver(BrowserOptions.Driver, Log);
            if (BrowserOptions.Driver.ImplicitWaitEnabled)
            {
                EnableImplicitWait(BrowserOptions.Driver.SecondsToWait);
            }

            Find = new Find(Driver, Log);
            SeleniumFind = new SeleniumFind(Driver, Log);
            Navigate = new Navigate(Driver);
            Window = new Window(Driver);
            ElementStyle = new ElementStyle(this);
            Wait = new Wait(this);

            InitializeScreenShotDirectory(BrowserOptions.Driver.ScreenShotPath);
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

            ScreenShotDirectory = path;
        }

        private void VerifyOnVerifyFailed(Exception e, VerifyFailedEvent args)
        {
            Dispose();

            if (args.Type != ConditionalVerify.Type) { throw e; }
        }

        private Screenshot ScreenShot { get; set; }

        private static string ScreenShotsDirectory => "ScreenShots";
    }
}
