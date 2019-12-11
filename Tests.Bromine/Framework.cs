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
        protected Framework(ITestOutputHelper output = null, bool ignoreBrowserInit = false)
        {
            Output = output;

            if (!ignoreBrowserInit)
            {
                Browser = new Browser(output: Output);
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
