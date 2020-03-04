using System;

using Bromine;
using Bromine.Core;
using Xunit.Abstractions;

namespace Tests.Bromine
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for tests to provide reusable functionality and locators.
    /// </summary>
    public class Framework : IDisposable
    {
        /// <summary>
        /// Construct a Chrome browser to use for tests.
        /// </summary>
        protected Framework(ITestOutputHelper output = null, bool ignoreBrowserInit = false, LogLevels logLevel = LogLevels.Message)
        {
            Output = output;

            if (!ignoreBrowserInit)
            {
                Browser = new Browser(output: Output, logLevel: logLevel);
            }
        }

        /// <summary>
        /// Page with common html elements.
        /// </summary>
        public Page.Common CommonPage { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stopOnError"></param>
        /// <param name="logElementHistory"></param>
        public void HeadlessInit(bool stopOnError = true, bool logElementHistory = false)
        {
            var options = new BrowserOptions(BrowserType.Chrome, isHeadless: true, stopOnError: stopOnError, logElementHistory: logElementHistory);
            Browser = new Browser(options: options, logLevel: LogLevels.Framework, output: Output);

            CommonPage = new Page.Common(Browser);
            CommonPage.Navigate();
        }

        /// <summary>
        /// Main entry point to interact with a web browser.
        /// </summary>
        public IBrowser Browser { get; set; }

        /// <summary>
        /// Used to write log messages.
        /// </summary>
        public Log Log => Browser.Log;

        internal ITestOutputHelper Output { get; }

        /// <summary>
        /// Dispose of the Browser.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public void Dispose()
        {
            Browser?.Dispose();
        }
    }
}
