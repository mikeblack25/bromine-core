using System;

namespace Bromine.Core
{
    /// <summary>
    /// Interact with a browser via a Selenium driver.
    /// </summary>
    public class Browser: IDisposable
    {
        /// <summary>
        /// Construct a Browser object of the given browser type.
        /// </summary>
        /// <param name="browser"></param>
        public Browser(BrowserType browser)
        {
            _driver = new Driver(browser);
        }

        public Navigate Navigate => new Navigate(_driver);

        /// <summary>
        /// Get the URL of the current page.
        /// </summary>
        public string Url => _driver.Url;

        /// <summary>
        /// Get the HTML source.
        /// </summary>
        public string Source => _driver.Source;

        /// <summary>
        /// Get the HTML title.
        /// </summary>
        public string Title => _driver.Title;

        /// <summary>
        /// Dispose of the driver resources.
        /// </summary>
        public void Dispose()
        {
            _driver.Dispose();
        }

        private Driver _driver { get; }

        private Navigate _nagigate { get; }
    }
}
