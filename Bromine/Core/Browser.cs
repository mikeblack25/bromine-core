using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Bromine.Constants;
using Bromine.Models;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Bromine.Core
{
    /// <inheritdoc cref="IBrowser" />
    public class Browser : IBrowser
    {
        public Browser(BrowserType browser = BrowserType.Chrome, int secondsToImplicitWait = 0)
            : this(new BrowserOptions(browser, secondsToImplicitWait))
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

            if (options.ImplicitWaitEnabled)
            {
                EnableImplicitWait(options.SecondsToWait);
            }

            Find = new Find(Driver);
            Navigate = new Navigate(Driver);
            Window = new Window(Driver);

            InitializeScreenshotDirectory(options.ScreenShotPath);
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
        public string ScreenshotDirectory => _screenshotDirectory;

        /// <inheritdoc />
        public string ScreenshotName { get; set; }

        /// <inheritdoc />
        public string ScreenshotPath => $@"{ScreenshotDirectory}\{ScreenshotName}";

        /// <inheritdoc />
        public Image LastImage
        {
            get
            {
                try
                {
                    return Image.FromFile(ScreenshotPath);
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

        public static string DefaultImagePath => $@"{AppDomain.CurrentDomain.BaseDirectory}\{ScreenshotsDirectory}";

        /// <inheritdoc />
        public string Information => Driver.WebDriver.GetType().ToString();

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
        public void TakeElementScreenshot(string name, Element element)
        {
            TakeRegionScreenshot(name, new Rectangle(element.Location, element.Size));
        }

        /// <inheritdoc />
        public void TakeRegionScreenshot(string name, Rectangle screenShotRegion)
        {
            Bitmap croppedImage;

            TakeVisibleScreenshot(name);

            using (var image = new Bitmap(ScreenshotPath))
            {
                croppedImage = image.Clone(screenShotRegion, image.PixelFormat);
            }

            using (var writer = new FileStream(ScreenshotPath, FileMode.OpenOrCreate))
            {
                croppedImage.Save(writer, ImageFormat.Png);
            }
        }

        /// <inheritdoc />
        public void TakeVisibleScreenshot(string name)
        {
            ScreenshotName = $"{name}.png";

            try
            {
                Screenshot = Driver.Screenshot;
                Screenshot.SaveAsFile(ScreenshotPath, ScreenshotImageFormat.Png);
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
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

        private void InitializeScreenshotDirectory(string path = "")
        {
            if (string.IsNullOrWhiteSpace(path)) // Create the logs where the app is running.
            {
                path = $@"{AppDomain.CurrentDomain.BaseDirectory}\{ScreenshotsDirectory}";
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            _screenshotDirectory = path;
        }

        private Driver Driver { get; }
        private Screenshot Screenshot { get; set; }

        private static string ScreenshotsDirectory => "Screenshots";
        private string _screenshotDirectory;
    }
}
