using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using System;
using System.Collections.Generic;

namespace Bromine.Core
{
    public class Browser : IDisposable
    {
        /// <summary>
        /// Url of the current page.
        /// </summary>
        public string Url => _driver.Url;

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
        /// Provides methods of interacting with the web browser.
        /// </summary>
        /// <param name="browser">Type of browser to use.</param>
        /// <param name="enableImplicitWait">When true, the driver will automatically wait the secondsToImplicitWait for a condition before stopping execution.</param>
        /// <param name="secondsToImplicitWait">Number of seconds to wait for an condition. This is only applicable when enabmeImplicitWait is true.</param>
        public Browser(BrowserType browser, bool enableImplicitWait = true, int secondsToImplicitWait = 5)
            : this(new Models.BrowserOptions(browser, enableImplicitWait, secondsToImplicitWait))
        {
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

        private Driver _driver;
    }
}
