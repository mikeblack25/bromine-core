using System;
using System.Collections.Generic;
using System.IO;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Bromine.Core
{
    /// <summary>
    /// Provides ability to interact with a web browser.
    /// </summary>
    public class Browser : IDisposable
    {
        /// <summary>
        /// Url of the current page.
        /// </summary>
        public string Url => Driver.Url;

        /// <summary>
        /// Title of the current page.
        /// </summary>
        public string Title => Driver.Title;

        /// <summary>
        /// Get the HTML source (DOM).
        /// </summary>
        public string Source => Driver.Source;

        /// <summary>
        /// Helpers to find elements.
        /// </summary>
        public Find Find { get; }

        /// <summary>
        /// Helpers to navigate to pages and files.
        /// </summary>
        public Navigate Navigate { get; }

        /// <summary>
        /// List of element that were called.
        /// </summary>
        public List<Element> CalledElements { get; }

        /// <summary>
        /// List of exceptions.
        /// </summary>
        public List<Exception> Exceptions { get; }

        /// <summary>
        /// Get the path to the last screenshot;
        /// </summary>
        public string LastScreenshotPath { get; private set; }

        /// <summary>
        /// Provides methods of interacting with the web browser.
        /// </summary>
        /// <param name="browser">Type of browser to use.</param>
        /// <param name="enableImplicitWait">When true, the driver will automatically wait the secondsToImplicitWait for a condition before stopping execution.</param>
        /// <param name="secondsToImplicitWait">Seconds to wait for a given condition. This is only applicable when enableImplicitWait is true.</param>
        /// <param name="stringShotDirectory">Location to store screenshots. If this is not provided screenshots will be put in a Screenshots directory in the output path.</param>
        public Browser(BrowserType browser, bool enableImplicitWait = true, int secondsToImplicitWait = 5, string stringShotDirectory = "")
            : this(new Models.BrowserOptions(browser, enableImplicitWait, secondsToImplicitWait))
        {
            InitializeScreenshotDirectory(stringShotDirectory);
        }

        /// <summary>
        /// Provides methods of interacting with the web browser.
        /// </summary>
        /// <param name="options">Provides advanced browser and driver configuration.</param>
        public Browser(Models.BrowserOptions options)
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

        /// <summary>
        /// Wait for a given condition to be true.
        /// </summary>
        /// <param name="condition">Condition to check every 250 ms for the specified wait time.</param>
        /// <param name="timeToWait">Time in seconds to wait for the condition to be satasfied.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Take a screenshot of the visible page.
        /// </summary>
        /// <param name="name">Name of the file of the screenshot.</param>
        public void TakeScreenshot(string name)
        {
            LastScreenshotPath = $@"{ScreenshotPath}\{name}.jpg";

            try
            {
                var screenshot = Driver.Screenshot;
                screenshot.SaveAsFile(LastScreenshotPath, ScreenshotImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
        }

        /// <summary>
        /// Dispose of the Selenium WebDriver.
        /// </summary>
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

        private string _screenshotsDirectory => "Screenshots";
        private string ScreenshotPath { get; set; }
    }
}
