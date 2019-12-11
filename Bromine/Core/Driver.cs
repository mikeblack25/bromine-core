using System;
using System.IO;
using System.Reflection;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
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
        /// <param name="browser"><see cref="Browser"/></param>
        public Driver(Browser browser)
        {
            Browser = browser;

            WebDriver = InitializeDriver();
        }

        /// <summary>
        /// Service the Chrome driver is running on provided it has been initialized at object construction.
        /// NOTE: only one DriverService is valid at a time.
        /// </summary>
        public DriverService DriverService { get; private set; }

        /// <summary>
        /// Take a Image for the given visible page.
        /// </summary>
        public Screenshot Image => WebDriver.TakeScreenshot();

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
                case BrowserType.InternetExplorer:
                {
                    return Options.IsRemoteDriver ? InitializeRemoteDriver(GetInternetExplorerOptions()) : InitializeInternetExplorerDriver();
                    }
                default:
                {
                    var exception = new Exception($"{Options.Browser} is not a supported Browser type");
                    Log.Error(exception.Message);

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
                SessionId sessionId = null;

                switch (Options.Browser)
                {
                    case BrowserType.Chrome:
                    {
                        sessionId = (WebDriver as ChromeDriver)?.SessionId;
                        break;
                    }
                    case BrowserType.Edge:
                    {
                        sessionId = (WebDriver as EdgeDriver)?.SessionId;
                        break;
                    }
                    case BrowserType.Firefox:
                    {
                        sessionId = (WebDriver as FirefoxDriver)?.SessionId;
                        break;
                    }
                    case BrowserType.InternetExplorer:
                    {
                        sessionId = (WebDriver as InternetExplorerDriver)?.SessionId;
                        break;
                    }
                }

                if (sessionId != null)
                {
                    WebDriver.Close();
                }

            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
            finally
            {
                WebDriver?.Quit();
                DriverService?.Dispose();
                Log?.Dispose();
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
                Log.Error(e.Message);
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
            Log.Error(exception.Message);

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
                Log.Error(e.Message);
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
                Log.Error(e.Message);
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

        #region Internet Explorer
        /// <summary>
        /// Get a Firefox browser driver.
        /// </summary>
        /// <returns></returns>
        private IWebDriver InitializeInternetExplorerDriver()
        {
            try
            {
                return new InternetExplorerDriver(GetInternetExplorerDriverService(), GetInternetExplorerOptions());
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Dispose();

                throw;
            }
        }

        private InternetExplorerOptions GetInternetExplorerOptions() => new InternetExplorerOptions();

        private InternetExplorerDriverService GetInternetExplorerDriverService()
        {
            DriverService = InternetExplorerDriverService.CreateDefaultService();

            if (Options.HideDriverWindow)
            {
                DriverService.HideCommandPromptWindow = true;
            }

            return (InternetExplorerDriverService)DriverService;
        }
        #endregion

        private IWebDriver InitializeRemoteDriver(OpenQA.Selenium.DriverOptions options) => new RemoteWebDriver(options);

        private string DefaultPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private const string HeadlessFlagString = "--headless";

        private Browser Browser { get; }
        private Log Log => Browser.Log;
        private DriverOptions Options => Browser.BrowserOptions.Driver;
    }
}
