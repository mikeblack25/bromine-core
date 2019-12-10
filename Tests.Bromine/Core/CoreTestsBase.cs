using System;

using Bromine;
using Bromine.Core;
using Bromine.Core.ElementInteraction;
using Bromine.Logger;
using Bromine.Models;

using Xunit.Abstractions;

namespace Tests.Bromine.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for tests to provide reusable functionality and locators.
    /// </summary>
    public class CoreTestsBase : IDisposable
    {
        /// <summary>
        /// Construct a Chrome browser to use for tests.
        /// </summary>
        protected CoreTestsBase(ITestOutputHelper output = null, bool ignoreBrowserInit = false)
        {
            Output = output;

            if (!ignoreBrowserInit)
            {
                Browser = new Browser(new BrowserOptions(), Output, LogType.XunitConsole, LogType.Text);
            }
        }

        /// <summary>
        /// Main entry point to interact with a web browser.
        /// </summary>
        public IBrowser Browser { get; set; }

        /// <summary>
        /// Used to write log messages.
        /// </summary>
        public Log Log => Browser.Log;

        /// <summary>
        /// Base path to all static html pages sourced in the Pages folder of the project.
        /// </summary>
        internal string PagesBasePath => $@"{AppDomain.CurrentDomain.BaseDirectory}\Pages";

        /// <summary>
        /// Amazon home page portion of a URL.
        /// </summary>
        internal string AmazonHome => @"Amazon.com\Amazon.com.html";

        /// <summary>
        /// Amazon
        /// </summary>
        internal string Amazon => "Amazon";

        internal Element CartButton => Browser.SeleniumFind.ElementById("nav-cart");

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
