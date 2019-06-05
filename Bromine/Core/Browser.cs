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
        /// <summary>
        /// Create a simple Browser object to interact with Elements.
        /// The driver will be configured based on the browser value selected.
        /// For advanced Browser configuration use Browser(BrowserConfiguration configuration) to construct a Browser object.
        /// </summary>
        /// <param name="browser"><see cref="BrowserType"/></param>
        /// <param name="stringShotDirectory">Location to store screenshots. If this is not provided screenshots will be put in a Screenshots directory in the output path.</param>
        public Browser(BrowserType browser, string stringShotDirectory = "") : this(new BrowserConfiguration(browser))
        {
            InitializeScreenshotDirectory(stringShotDirectory);
        }

        /// <summary>
        /// Provides methods of interacting with the web browser.
        /// </summary>
        /// <param name="configuration">Provides advanced browser and driver configuration.</param>
        public Browser(BrowserConfiguration configuration)
        {
            Exceptions = new List<Exception>();

            BrowserConfiguration = configuration;

            Driver = new Driver(BrowserConfiguration, Exceptions);

            if (BrowserConfiguration.EnableImplicitWait)
            {
                EnableImplicitWait(BrowserConfiguration.SecondsToImplicitWait);
            }

            Find = new Find(Driver);
            Navigate = new Navigate(Driver);
            Window = new Window(Driver);
        }

        /// <inheritdoc />
        public string Url => Driver.Url;

        /// <inheritdoc />
        public string Title => Driver.Title;

        /// <inheritdoc />
        public string Source => Driver.Source;

        /// <inheritdoc />
        public string ScreenshotPath { get; private set; }

        /// <inheritdoc />
        public Find Find { get; }

        /// <inheritdoc />
        public Navigate Navigate { get; }

        /// <inheritdoc />
        public IWindow Window { get; }

        /// <inheritdoc />
        public List<Exception> Exceptions { get; }

        /// <inheritdoc />
        public BrowserConfiguration BrowserConfiguration { get; }

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
            ScreenshotPath = $@"{ScreenshotPath}\{name}.png";

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

        private void InitializeScreenshotDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = $@"{AppDomain.CurrentDomain.BaseDirectory}\{ScreenshotsDirectory}";
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            ScreenshotPath = path;
        }

        private Driver Driver { get; }
        private Screenshot Screenshot { get; set; }

        private static string ScreenshotsDirectory => "Screenshots";
    }
}
