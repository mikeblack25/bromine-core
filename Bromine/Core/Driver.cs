using System;
using System.Collections.Generic;
using System.ComponentModel;
using Bromine.Constants;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;

using DriverOptions = Bromine.Models.DriverOptions;

namespace Bromine.Core
{
    /// <summary>
    /// Provide access to interact with browser specific drivers.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class Driver : IDisposable
    {
        /// <summary>
        /// Initialize an IWebDriver for the given browser and desired configuration.
        /// </summary>
        /// <param name="options">Object to bass desired browser driver configuration.</param>
        /// <param name="exceptions">Exception list to track unexpected results during execution.</param>
        public Driver(DriverOptions options, List<Exception> exceptions)
        {
            Options = options;
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
        /// Advanced driver configuration options.
        /// See the following options for more details.
        /// <see cref="DriverOptions.Browser"/>
        /// <see cref="DriverOptions.IsRemoteDriver"/>
        /// <see cref="DriverOptions.IsHeadless"/>
        /// <see cref="DriverOptions.HideDriverWindow"/>
        /// </summary>
        public DriverOptions Options { get; }

        /// <summary>
        /// Service the Chrome driver is running on provided it has been initialized at object construction.
        /// NOTE: only one DriverService is valid at a time.
        /// </summary>
        public DriverService DriverService { get; private set; }

        /// <summary>
        /// Take a screenshot for the given visible page.
        /// </summary>
        public Screenshot Screenshot => WebDriver.TakeScreenshot();

        /// <summary>
        /// List of un expected runtime behavior.
        /// </summary>
        public List<Exception> Exceptions { get; }

        /// <summary>
        /// Selenium WebDriver. <see cref="IWebDriver"/>
        /// </summary>
        internal IWebDriver WebDriver { get; }

        /// <summary>
        /// Close the Browser and WebDriver.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public void Dispose()
        {
            try
            {
                WebDriver?.Close();
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
            }
            finally
            {
                WebDriver?.Quit();
                DriverService?.Dispose();
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
            options.AddArgument("--allow-file-access-from-files");

            DriverService = ChromeDriverService.CreateDefaultService();

            if (hideDriverWindow)
            {
                DriverService.HideCommandPromptWindow = true;
            }

            if (isHeadless)
            {
                options.AddArgument(HeadlessFlagString);
            }

            DriverService.HideCommandPromptWindow = true;

            try
            {
                return !Options.IsRemoteDriver ? new ChromeDriver((ChromeDriverService)DriverService, options) : new RemoteWebDriver(options);
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
            DriverService = FirefoxDriverService.CreateDefaultService();

            if (hideDriverWindow)
            {
                DriverService.HideCommandPromptWindow = true;
            }

            if (isHeadless)
            {
                options.AddArgument(HeadlessFlagString);
            }

            try
            {
                return !Options.IsRemoteDriver ? new FirefoxDriver((FirefoxDriverService)DriverService, options) : new RemoteWebDriver(options);
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
            DriverService = EdgeDriverService.CreateDefaultService();

            if (hideDriverWindow)
            {
                DriverService.HideCommandPromptWindow = true;
            }

            try
            {
                return !Options.IsRemoteDriver ? new EdgeDriver((EdgeDriverService)DriverService, options) : new RemoteWebDriver(options);
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
