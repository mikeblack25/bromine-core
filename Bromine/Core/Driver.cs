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
        public Driver(Models.DriverOptions options)
        {
            switch(options.Browser)
            {
                case BrowserType.Chrome:
                    {
                        WebDriver = InitializeChromeDriver(options.IsHeadless, options.HideDriverWindow);
                        break;
                    }
                case BrowserType.Edge:
                    {
                        // TODO: If isHeadless = true Log message not supported.
                        WebDriver = InitializeEdgeDriver(options.HideDriverWindow);
                        break;
                    }
                case BrowserType.Firefox:
                    {
                        WebDriver = InitializeFirefoxDriver(options.IsHeadless, options.HideDriverWindow);
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
        public string Url => WebDriver.Url;

        /// <summary>
        /// Get the HTML source.
        /// </summary>
        public string Source => WebDriver.PageSource;

        /// <summary>
        /// Get the HTML title.
        /// </summary>
        public string Title => WebDriver.Title;

        /// <summary>
        /// Get the driver logs.
        /// </summary>
        public ILogs Logs => WebDriver.Manage().Logs;

        /// <summary>
        /// Manipulate cookies.
        /// </summary>
        public ICookieJar Cookies => WebDriver.Manage().Cookies;

        /// <summary>
        /// Manipulate currently focused window.
        /// </summary>
        public IWindow Window => WebDriver.Manage().Window;

        /// <summary>
        /// Position of the browser window.
        /// </summary>
        public Point Position => WebDriver.Manage().Window.Position;

        /// <summary>
        /// Size ofthe browser window.
        /// </summary>
        public Size Size => WebDriver.Manage().Window.Size;

        internal IWebDriver WebDriver { get; }

        #region Navigate
        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to.</param>
        public void NavigateToUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Navigate back to the previous page.
        /// </summary>
        public void NavigateBack()
        {
            WebDriver.Navigate().Back();
        }

        /// <summary>
        /// Navigate forward.
        /// </summary>
        public void NavigateForward()
        {
            WebDriver.Navigate().Forward();
        }

        /// <summary>
        /// Refresh the current page.
        /// </summary>
        public void Refresh()
        {
            WebDriver.Navigate().Refresh();
        }
        #endregion

        #region Window
        /// <summary>
        /// Maxamize the window.
        /// </summary>
        public void Maxamize()
        {
            WebDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Minimize the window.
        /// </summary>
        public void Minimize()
        {
            WebDriver.Manage().Window.Minimize();
        }

        /// <summary>
        /// Maximize the size of the browser window.
        /// </summary>
        public void FullScreen()
        {
            WebDriver.Manage().Window.FullScreen();
        }
        #endregion

        /// <summary>
        /// Close the Browser and WebDriver.
        /// </summary>
        public void Dispose()
        {
            WebDriver.Quit();
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
