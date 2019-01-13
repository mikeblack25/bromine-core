using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Bromine.Core
{
    /// <summary>
    /// Provide access to browser drivers.
    /// </summary>
    public class Driver: IDisposable
    {
        /// <summary>
        /// Initialize an IWebDriver for the given browser and desired configuration.
        /// </summary>
        /// <param name="browser">Type of browser to initialize.</param>
        /// <param name="isHeadless">Do not render a UI when true. This will run faster.</param>
        public Driver(BrowserType browser, bool isHeadless = false, bool hideDriverWindow = true)
        {
            switch(browser)
            {
                case BrowserType.Chrome:
                    {
                        _driver = InitializeChromeDriver(isHeadless, hideDriverWindow);
                        break;
                    }
                case BrowserType.Edge:
                    {
                        // TODO: If isHeadless = true Log message not supported.
                        _driver = InitializeEdgeDriver(hideDriverWindow);
                        break;
                    }
                case BrowserType.Firefox:
                    {
                        _driver = InitializeFirefoxDriver(isHeadless, hideDriverWindow);
                        break;
                    }
                default:
                    {
                        // TODO: Log not supported type.
                        break;
                    }
            }

        }

        /// <summary>
        /// Get a Chrome browser driver.
        /// </summary>
        /// <param name="hideDriverWindow">If true do not display the webdriver dialog.</param>
        /// <param name="isHeadless">If true do not render the browser UI. This is faster and takes less resources.</param>
        /// <returns></returns>
        private IWebDriver InitializeChromeDriver(bool isHeadless = true, bool hideDriverWindow = true)
        {
            var options = new ChromeOptions();
            var chromeDriverService = ChromeDriverService.CreateDefaultService();

            if (hideDriverWindow)
            {
                chromeDriverService.HideCommandPromptWindow = true;
            }

            if (isHeadless)
            {
                options.AddArgument(headlessFlagString);
            }

            chromeDriverService.HideCommandPromptWindow = true;

            return new ChromeDriver(chromeDriverService, options);
        }

        /// <summary>
        /// Get the URL of the current page.
        /// </summary>
        public string Url => _driver.Url;

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to.</param>
        public void NavigateToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        /// <summary>
        /// Get a Firefox browser driver.
        /// </summary>
        /// <param name="hideDriverWindow">If true do not display the webdriver dialog.</param>
        /// <param name="isHeadless">If true do not render the browser UI. This is faster and takes less resources.</param>
        /// <returns></returns>
        private IWebDriver InitializeFirefoxDriver(bool isHeadless = false, bool hideDriverWindow = true)
        {
            var options = new FirefoxOptions();
            var firefoxDriverService = FirefoxDriverService.CreateDefaultService();

            if (hideDriverWindow)
            {
                firefoxDriverService.HideCommandPromptWindow = true;
            }

            if (isHeadless)
            {
                options.AddArgument(headlessFlagString);
            }

            return new FirefoxDriver(firefoxDriverService, options);
        }

        /// <summary>
        /// Get a Edge browser driver.
        /// </summary>
        /// <param name="hideDriverWindow">If true do not display the webdriver dialog.</param>
        /// <returns></returns>
        private IWebDriver InitializeEdgeDriver(bool hideDriverWindow = true)
        {
            var options = new EdgeOptions();
            var edgeDriverService = EdgeDriverService.CreateDefaultService();

            if (hideDriverWindow)
            {
                edgeDriverService.HideCommandPromptWindow = true;
            }

            return new EdgeDriver(edgeDriverService, options);
        }

        private const string headlessFlagString = "--headless";

        private IWebDriver _driver { get; }
    }


    /// <summary>
    /// Supported web browser types.
    /// </summary>
    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge
    }
}
