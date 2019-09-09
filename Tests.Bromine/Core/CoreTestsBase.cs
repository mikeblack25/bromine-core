using System;
using System.IO;
using System.Reflection;

using Bromine;
using Bromine.Core;
using Bromine.Core.ElementInteraction;

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
        protected CoreTestsBase(ITestOutputHelper output = null)
        {
            Browser = new Browser(output);
        }

        /// <summary>
        /// Main entry point to interact with a web browser.
        /// </summary>
        public IBrowser Browser { get; set; }

        /// <summary>
        /// Base path to all static html pages sourced in the Pages folder of the project.
        /// </summary>
        internal string BasePath => $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)?.Substring(6)}\Pages";

        /// <summary>
        /// Amazon home page portion of a URL.
        /// </summary>
        internal string AmazonHome => @"Amazon.com\Amazon.com.html";

        /// <summary>
        /// Amazon
        /// </summary>
        internal string Amazon => "Amazon";

        internal Element CartButton => Browser.SeleniumFind.ElementById("nav-cart");

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
