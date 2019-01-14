using System;
using System.Drawing;

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
        /// Get the URL of the current page.
        /// </summary>
        public string Url => _driver.Url;

        /// <summary>
        /// Get the HTML source.
        /// </summary>
        public string Source => _driver.PageSource;

        /// <summary>
        /// Get the HTML title.
        /// </summary>
        public string Title => _driver.Title;

        /// <summary>
        /// Get the driver logs.
        /// </summary>
        public ILogs Logs => _driver.Manage().Logs;

        /// <summary>
        /// Manipulate cookies.
        /// </summary>
        public ICookieJar Cookies => _driver.Manage().Cookies;

        /// <summary>
        /// Manipulate currently focused window.
        /// </summary>
        public IWindow Window => _driver.Manage().Window;

        /// <summary>
        /// Position of the browser window.
        /// </summary>
        public Point Position => _driver.Manage().Window.Position;

        /// <summary>
        /// Size ofthe browser window.
        /// </summary>
        public Size Size => _driver.Manage().Window.Size;

        #region Navigate
        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to.</param>
        public void NavigateToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Navigate back to the previous page.
        /// </summary>
        public void NavigateBack()
        {
            _driver.Navigate().Back();
        }

        /// <summary>
        /// Navigate forward.
        /// </summary>
        public void NavigateForward()
        {
            _driver.Navigate().Forward();
        }

        /// <summary>
        /// Refresh the current page.
        /// </summary>
        public void Refresh()
        {
            _driver.Navigate().Refresh();
        }
        #endregion

        #region Window
        /// <summary>
        /// Maxamize the window.
        /// </summary>
        public void Maxamize()
        {
            _driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Minimize the window.
        /// </summary>
        public void Minimize()
        {
            _driver.Manage().Window.Minimize();
        }

        /// <summary>
        /// Maximize the size of the browser window.
        /// </summary>
        public void FullScreen()
        {
            _driver.Manage().Window.FullScreen();
        }
        #endregion

        /// <summary>
        /// Close the Browser and WebDriver.
        /// </summary>
        public void Dispose()
        {
            _driver.Quit();
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
