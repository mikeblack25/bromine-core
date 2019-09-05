using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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

            WebDriver = InitializeDriver();
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
        /// Take a ScreenShot for the given visible page.
        /// </summary>
        public Screenshot ScreenShot => WebDriver.TakeScreenshot();

        /// <summary>
        /// List of un expected runtime behavior.
        /// </summary>
        public List<Exception> Exceptions { get; }

        /// <summary>
        /// Selenium WebDriver. <see cref="IWebDriver"/>
        /// </summary>
        internal IWebDriver WebDriver { get; }

        /// <summary>
        /// Initialize the browser from the class instance of <see cref="Options"/>.
        /// </summary>
        public IWebDriver InitializeDriver()
        {
            switch (Options.Browser)
            {
                case BrowserType.Chrome:
                {
                    return Options.IsRemoteDriver ? InitializeRemoteDriver(GetChromeOptions()) : InitializeChromeDriver();
                }
                case BrowserType.Edge:
                {
                    return Options.IsRemoteDriver ? InitializeRemoteDriver(GetEdgeOptions()) : InitializeEdgeDriver();
                }
                case BrowserType.Firefox:
                {
                    return Options.IsRemoteDriver ? InitializeRemoteDriver(GetFirefoxOptions()) : InitializeFirefoxDriver();
                }
                default:
                {
                    var exception = new Exception($"{Options.Browser} is not a supported Browser type");
                    Exceptions.Add(exception);

                    throw exception;
                }
            }
        }

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

        #region Chrome
        /// <summary>
        /// Get a Chrome browser driver.
        /// </summary>
        /// <returns></returns>
        private IWebDriver InitializeChromeDriver()
        {
            var options = GetChromeOptions();

            try
            {
                return !Options.UseDefaultDriverPath ? new ChromeDriver(StartChromeDriverService(), options) : new ChromeDriver(DefaultPath, options);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                Dispose();

                throw;
            }
        }

        private ChromeOptions GetChromeOptions()
        {
            var options = new ChromeOptions();
            options.AddArgument("--allow-file-access-from-files");

            if (Options.IsHeadless)
            {
                options.AddArgument(HeadlessFlagString);
            }

            return options;
        }

        /// <summary>
        /// TODO: This is coupling the Driver service with the default driver path. This should be split.
        /// </summary>
        /// <returns></returns>
        private ChromeDriverService StartChromeDriverService()
        {
            if (!Options.UseDefaultDriverPath)
            {
                DriverService = ChromeDriverService.CreateDefaultService();

                if (Options.HideDriverWindow)
                {
                    DriverService.HideCommandPromptWindow = true;
                }

                return (ChromeDriverService) DriverService;
            }

            var exception = new Exception("StartChromeDriverService should only be called when !Options.UseDefaultDriverPath");
            Exceptions.Add(exception);

            throw exception;
        }
        #endregion

        #region Firefox
        /// <summary>
        /// Get a Firefox browser driver.
        /// </summary>
        /// <returns></returns>
        private IWebDriver InitializeFirefoxDriver()
        {
            try
            {
                return new FirefoxDriver(GetFirefoxDriverService(), GetFirefoxOptions());
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                Dispose();

                throw;
            }
        }

        private FirefoxOptions GetFirefoxOptions()
        {
            var options = new FirefoxOptions();

            if (Options.IsHeadless)
            {
                options.AddArgument(HeadlessFlagString);
            }

            return options;
        }

        private FirefoxDriverService GetFirefoxDriverService()
        {
            DriverService = FirefoxDriverService.CreateDefaultService();

            if (Options.HideDriverWindow)
            {
                DriverService.HideCommandPromptWindow = true;
            }

            return (FirefoxDriverService) DriverService;
        }
        #endregion

        #region Edge
        /// <summary>
        /// Get a Edge browser driver.
        /// </summary>
        /// <returns></returns>
        private IWebDriver InitializeEdgeDriver()
        {
            try
            {
                return new EdgeDriver(GetEdgeDriverService(), GetEdgeOptions());
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                Dispose();

                throw;
            }
        }

        private EdgeOptions GetEdgeOptions() => new EdgeOptions();

        private EdgeDriverService GetEdgeDriverService()
        {
            DriverService = EdgeDriverService.CreateDefaultService();

            if (Options.HideDriverWindow)
            {
                DriverService.HideCommandPromptWindow = true;
            }

            return (EdgeDriverService) DriverService;
        }
        #endregion

        private IWebDriver InitializeRemoteDriver(OpenQA.Selenium.DriverOptions options) => new RemoteWebDriver(options);

        private string DefaultPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private const string HeadlessFlagString = "--headless";
    }
}
