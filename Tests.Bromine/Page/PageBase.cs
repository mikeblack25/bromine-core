using System;

using Bromine.Constants;
using Bromine.Core;
using Bromine.Models;

using Tests.Bromine.Page.Google;

using Xunit.Abstractions;

namespace Tests.Bromine.Page
{
    /// <summary>
    /// Base for all test classes.
    /// Provides test classes direct access to all defined Page Objects when <see cref="InitializePages(BrowserType, bool, ITestOutputHelper)"/>, a Browser and a standard teardown via <see cref="Dispose"/>.
    /// </summary>
    public class PageBase : IDisposable
    {
        /// <summary>
        /// Initialize all page objects and inject the desired Browser into all derived Page Objects.
        /// In this example we only have the <see cref="Home"/> Page Object.
        /// Other pages can be added in the same way.
        /// </summary>
        /// <param name="browser"><see cref="BrowserType"/></param>
        /// <param name="isHeadless"><see cref="DriverOptions.IsHeadless"/></param>
        /// <param name="output">Output object when using xUnit as the test runner.</param>
        public void InitializePages(BrowserType browser, bool isHeadless = false, ITestOutputHelper output = null)
        {
            var browserOptions = new BrowserOptions(browser, isHeadless);
            Browser = new Browser(browserOptions, output);

            InitializePages();
        }

        private void InitializePages()
        {
            Home = new Home(Browser);
        }

        /// <summary>
        /// <see cref="Browser"/>
        /// </summary>
        public Browser Browser { get; private set; }

        /// <summary>
        /// <see cref="Google.Home"/>
        /// </summary>
        public Home Home { get; private set; }

        /// <summary>
        /// Dispose of Browser resources.
        /// </summary>
        public void Dispose()
        {
            Browser.Dispose();
        }
    }
}
