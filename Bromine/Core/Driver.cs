using System;
using System.Collections.Generic;
using System.ComponentModel;

using Bromine.Constants;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Extensions;

namespace Bromine.Core
{
    /// <summary>
    /// Provide access to interact with browser specific drivers.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class Driver: IDisposable
    {
        /// <summary>
        /// Initialize an IWebDriver for the given browser and desired configuration.
        /// </summary>
        /// <param name="options">Object to bass desired browser driver configuration.</param>
        /// <param name="exceptions">Exception list to track unexpected results during execution.</param>
        public Driver(Models.BrowserConfiguration options, List<Exception> exceptions)
        {
            Exceptions = exceptions;

            switch (options.Browser)
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
                    Exceptions.Add(new InvalidEnumArgumentException($"{options.Browser} is not a supported Browser type"));
                    break;
                }
            }
        }

        /// <summary>
        /// Service the Chrome driver is running on provided it has been initialized at object construction.
        /// NOTE: only one DriverService is valid at a time.
        /// </summary>
        public ChromeDriverService ChromeDriverService { get; private set; }

        /// <summary>
        /// Service the Firefox driver is running on provided it has been initialized at object construction.
        /// NOTE: only one DriverService is valid at a time.
        /// </summary>
        public FirefoxDriverService FirefoxDriverService { get; private set; }

        /// <summary>
        /// Service the Edge driver is running on provided it has been initialized at object construction.
        /// NOTE: only one DriverService is valid at a time.
        /// </summary>
        public EdgeDriverService EdgeDriverService { get; private set; }

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
        /// Take a screenshot for the given visible page.
        /// </summary>
        public Screenshot Screenshot => WebDriver.TakeScreenshot();

        internal IWebDriver WebDriver { get; }

        /// <summary>
        /// Manipulate currently focused window.
        /// </summary>
        public IWindow Window => WebDriver.Manage().Window;

        /// <summary>
        /// List of un expected runtime behavior.
        /// </summary>
        public List<Exception> Exceptions { get; }

        /// <summary>
        /// Close the Browser and WebDriver.
        /// </summary>
        public void Dispose()
        {
            WebDriver?.Quit();
            ChromeDriverService?.Dispose();
            FirefoxDriverService?.Dispose();
            EdgeDriverService?.Dispose();
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
            options.AddArgument("--allow-file-access-from-files");

            ChromeDriverService = ChromeDriverService.CreateDefaultService();

            if (hideDriverWindow)
            {
                ChromeDriverService.HideCommandPromptWindow = true;
            }

            if (isHeadless)
            {
                options.AddArgument(HeadlessFlagString);
            }

            ChromeDriverService.HideCommandPromptWindow = true;

            try
            {
                return new ChromeDriver(ChromeDriverService, options);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                Dispose();

                throw;
            }
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
            FirefoxDriverService = FirefoxDriverService.CreateDefaultService();

            if (hideDriverWindow)
            {
                FirefoxDriverService.HideCommandPromptWindow = true;
            }

            if (isHeadless)
            {
                options.AddArgument(HeadlessFlagString);
            }

            try
            {
                return new FirefoxDriver(FirefoxDriverService, options);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                Dispose();

                throw;
            }
        }

        /// <summary>
        /// Get a Edge browser driver.
        /// </summary>
        /// <param name="hideDriverWindow">If true do not display the webdriver dialog.</param>
        /// <returns></returns>
        private IWebDriver InitializeEdgeDriver(bool hideDriverWindow = true)
        {
            var options = new EdgeOptions();
            EdgeDriverService = EdgeDriverService.CreateDefaultService();

            if (hideDriverWindow)
            {
                EdgeDriverService.HideCommandPromptWindow = true;
            }

            try
            {
                return new EdgeDriver(EdgeDriverService, options);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                Dispose();

                throw;
            }
        }

        private const string HeadlessFlagString = "--headless";
    }
}
