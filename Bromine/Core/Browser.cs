using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Bromine.Models;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Bromine.Core
{
    /// <inheritdoc cref="IBrowser" />
    public class Browser : IBrowser
    {
        /// <summary>
        /// Launch a Chrome browser with the default configuration.
        /// </summary>
        public Browser() : this(new BrowserOptions())
        {
        }

        /// <summary>
        /// Provides methods of interacting with the web browser.
        /// </summary>
        /// <param name="options">Provides advanced browser and driver options.</param>
        public Browser(BrowserOptions options)
        {
            Exceptions = new List<Exception>();
            BrowserOptions = options;

            Driver = new Driver(BrowserOptions.Driver, Exceptions);

            if (options.Driver.ImplicitWaitEnabled)
            {
                EnableImplicitWait(options.Driver.SecondsToWait);
            }

            Find = new Find(Driver);
            Navigate = new Navigate(Driver);
            Window = new Window(Driver);
            ElementStyle = new ElementStyle(this);

            InitializeScreenShotDirectory(options.Driver.ScreenShotPath);
        }

        /// <inheritdoc />
        public string Url => Driver.WebDriver.Url;

        /// <inheritdoc />
        public string Title => Driver.WebDriver.Title;

        /// <inheritdoc />
        public string Source => Driver.WebDriver.PageSource;

        /// <inheritdoc />
        public ILogs Logs => Driver.WebDriver.Manage().Logs;

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
        public Navigate Navigate { get; }

        /// <inheritdoc />
        public List<Exception> Exceptions { get; }

        /// <inheritdoc />
        public BrowserOptions BrowserOptions { get; }

        /// <inheritdoc />
        public ElementStyle ElementStyle { get; }

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
                    Exceptions.Add(e);
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

        internal Driver Driver { get; }

        /// <inheritdoc />
        public bool Wait(Func<bool> condition, int timeToWait = 1)
        {
            var result = false;

            try
            {
                var wait = new DefaultWait<IWebDriver>(Driver.WebDriver)
                {
                    Timeout = TimeSpan.FromSeconds(timeToWait),
                    PollingInterval = TimeSpan.FromMilliseconds(250)
                };

                wait.Until(x => condition());

                result = true;
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }

            return result;
        }

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
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
        }

        /// <inheritdoc />
        public object ExecuteJs(string script, object[] arguments)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var js = Driver as IJavaScriptExecutor;

            return js?.ExecuteAsyncScript(script, arguments);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Driver?.Dispose();
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

        private Screenshot ScreenShot { get; set; }

        private static string ScreenShotsDirectory => "ScreenShots";
        private string _screenShotDirectory;
    }
}
