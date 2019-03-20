using System;
using System.Collections.Generic;
using System.Drawing;
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
        /// Create a simple Browser object to interact with Elements.
        /// The driver will be configured based on the browser value selected.
        /// For advanced Browser configuration use Browser(BrowserOptions options) to construct a Browser object.
        /// </summary>
        /// <param name="browser">Type of browser to use.</param>
        /// <param name="enableImplicitWait">When true, the driver will automatically wait the secondsToImplicitWait for a condition before stopping execution.</param>
        /// <param name="secondsToImplicitWait">Seconds to wait for a given condition. This is only applicable when enableImplicitWait is true.</param>
        /// <param name="stringShotDirectory">Location to store screenshots. If this is not provided screenshots will be put in a Screenshots directory in the output path.</param>
        public Browser(BrowserType browser, bool enableImplicitWait = true, int secondsToImplicitWait = 5, string stringShotDirectory = "")
            : this(new BrowserOptions(browser, enableImplicitWait, secondsToImplicitWait))
        {
            InitializeScreenshotDirectory(stringShotDirectory);
        }

        /// <summary>
        /// Provides methods of interacting with the web browser.
        /// </summary>
        /// <param name="options">Provides advanced browser and driver configuration.</param>
        public Browser(BrowserOptions options)
        {
            CalledElements = new List<Element>();
            Exceptions = new List<Exception>();

            Driver = new Driver(options.Driver);

            if (options.EnableImplicitWait)
            {
                EnableImplicitWait(options.SecondsToImplicitWait);
            }

            Find = new Find(Driver.WebDriver);
            Navigate = new Navigate(Driver, Exceptions);
        }

        /// <inheritdoc />
        public string Url => Driver.Url;

        /// <inheritdoc />
        public string Title => Driver.Title;

        /// <inheritdoc />
        public string Source => Driver.Source;

        /// <inheritdoc />
        public Find Find { get; }

        /// <inheritdoc />
        public Navigate Navigate { get; }

        /// <inheritdoc />
        public List<Element> CalledElements { get; }

        /// <inheritdoc />
        public List<Exception> Exceptions { get; }

        /// <inheritdoc />
        public string LastScreenshotPath { get; private set; }

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
        public void TakeScreenshot(string name, Element element)
        {
            TakeScreenshot(name, new Rectangle(element.Location, element.Size));
        }

        /// <inheritdoc />
        public void TakeScreenshot(string name, Rectangle screenShotRegion)
        {
            TakeScreenshot(name);

            var bmpImage = new Bitmap(Image.FromFile(LastScreenshotPath));
            var cropedImag = bmpImage.Clone(screenShotRegion, bmpImage.PixelFormat);

            Screenshot.SaveAsFile(LastScreenshotPath, ScreenshotImageFormat.Jpeg);
        }

        /// <inheritdoc />
        public void TakeScreenshot(string name)
        {
            LastScreenshotPath = $@"{ScreenshotPath}\{name}.jpg";

            try
            {
                Screenshot = Driver.Screenshot;
                Screenshot.SaveAsFile(LastScreenshotPath, ScreenshotImageFormat.Jpeg);
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
                path = $@"{AppDomain.CurrentDomain.BaseDirectory}\{_screenshotsDirectory}";
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            ScreenshotPath = path;
        }

        private Driver Driver { get; }
        private Screenshot Screenshot { get; set; }

        private string _screenshotsDirectory => "Screenshots";
        private string ScreenshotPath { get; set; }
    }
}
