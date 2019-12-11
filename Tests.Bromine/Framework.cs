using System;

using Bromine;
using Bromine.Core;
using Bromine.Models;

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
        protected Framework(ITestOutputHelper output = null, bool ignoreBrowserInit = false)
        {
            Output = output;

            if (!ignoreBrowserInit)
            {
                Browser = new Browser(output: Output);
            }
        }

        /// <summary>
        /// Page with common html elements.
        /// </summary>
        public Page.Common CommonPage { get; private set; }

        /// <summary>
        /// Initialize a headless Chrome browser and navigate to CommonPage.
        /// </summary>
        public void HeadlessInit()
        {
            var options = new BrowserOptions(BrowserType.Chrome, true);
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
        public virtual void Dispose()
        {
            Browser?.Dispose();
        }
    }
}
