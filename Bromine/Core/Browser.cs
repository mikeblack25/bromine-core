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
        public string Url => _driver.Url;

        /// <summary>
        /// Title of the current page.
        /// </summary>
        public string Title => _driver.Title;

        /// <summary>
        /// Get the HTML source (DOM).
        /// </summary>
        public string Source => _driver.Source;

        /// <summary>
        /// Helpers to find elements.
        /// </summary>
        public Find Find { get; }

        /// <summary>
        /// List of element that were called.
        /// </summary>
        public List<Element> CalledElements { get; private set; }

        /// <summary>
        /// List of exceptions.
        /// </summary>
        public List<Exception> Exceptions { get; private set; }

        /// <summary>
        /// Get the path to the last screenshot;
        /// </summary>
        public string LastScreenshotPath { get; private set; }

        /// <summary>
        /// Provides methods of interacting with the web browser.
        /// </summary>
        /// <param name="browser">Type of browser to use.</param>
        /// <param name="enableImplicitWait">When true, the driver will automatically wait the secondsToImplicitWait for a condition before stopping execution.</param>
        /// <param name="secondsToImplicitWait">Seconds to wait for a given condition. This is only applicable when enabmeImplicitWait is true.</param>
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

            _driver = new Driver(options.Driver);

            if (options.EnableImplicitWait)
            {
                EnableImplicitWait(options.SecondsToImplicitWait);
            }

            Find = new Find(_driver.WebDriver);
        }

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to.</param>
        public void NavigateToUrl(string url)
        {
            try
            {
                _driver.WebDriver.Navigate().GoToUrl(url);
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
        }

        /// <summary>
        /// Navigate to the given file.
        /// </summary>
        /// <param name="path"></param>
        public void NavigateToFile(string path)
        {
            try
            {
                _driver.WebDriver.Navigate().GoToUrl($"file://{path}");
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
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
                var wait = new DefaultWait<IWebDriver>(_driver.WebDriver)
                {
                    Timeout = TimeSpan.FromSeconds(timeToWait),
                    PollingInterval = TimeSpan.FromMilliseconds(250)
                };

                wait.Until(x => condition() == true);

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
            LastScreenshotPath = $@"{_screenshotPath}\{name}.jpg";

            try
            {
                var screenshot = _driver.Screenshot;
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
            _driver?.Dispose();
        }

        private void EnableImplicitWait(int secondsToWait)
        {
            _driver.WebDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, secondsToWait);
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

            _screenshotPath = path;
        }

        private Driver _driver { get; set; }

        private string _screenshotsDirectory => "Screenshots";
        private string _screenshotPath { get; set; }
    }
}
