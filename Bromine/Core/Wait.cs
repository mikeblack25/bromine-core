using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Bromine.Core
{
    /// <summary>
    /// Provides ability to wait for conditions using Selenium's DefaultWait class.
    /// </summary>
    public class Wait
    {
        /// <summary>
        /// Construct a class For object to provide access to wait methods.
        /// </summary>
        public Wait(Browser browser)
        {
            For = new For(browser);
        }

        /// <summary>
        /// <see cref="For"/>
        /// </summary>
        public For For { get; }
    }


    /// <summary>
    /// Provides specific wait methods to wait for conditions.
    /// </summary>
    public class For
    {
        /// <summary>
        /// Construct a For object with the provided browser.
        /// </summary>
        public For(Browser browser)
        {
            Browser = browser;

            DefaultWait = new DefaultWait<IWebDriver>(Driver)
            {
                PollingInterval = TimeSpan.FromMilliseconds(250)
            };
        }

        /// <summary>
        /// Wait for the given element to be displayed.
        /// </summary>
        /// <param name="element">Element to wait for.</param>
        /// <param name="timeToWait">Time in seconds to wait for the condition to be true.</param>
        public void DisplayedElement(Element.Element element, int timeToWait = 1) => Condition(() => element.Displayed, timeToWait);

        /// <summary>
        /// Wait for the given URL to be loaded.
        /// </summary>
        /// <param name="expectedUrl">Expected URL to wait for.</param>
        /// <param name="timeToWait">Time in seconds to wait for the condition to be true.</param>
        public void Navigation(string expectedUrl, int timeToWait = 1) => Condition(() => Browser.Url == expectedUrl, timeToWait);

        /// <summary>
        /// Wait for the document to be in a "complete" state.
        /// </summary>
        public void PageLoaded() => Browser.ExecuteJs(PageLoadedScript);

        /// <summary>
        /// Wait for the given condition to be true.
        /// </summary>
        /// <param name="condition">Condition to wait for.</param>
        /// <param name="timeToWait">Time in seconds to wait for the condition to be true.</param>
        /// <returns></returns>
        public bool Condition(Func<bool> condition, int timeToWait = 1)
        {
            try
            {
                DefaultWait.Timeout = TimeSpan.FromSeconds(timeToWait);

                DefaultWait.Until(x => condition());

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);

                return false;
            }
        }

        private const string PageLoadedScript = "\"return document.readyState\").Equals(\"complete\")";

        private Browser Browser { get; }
        private IWebDriver Driver => Browser.Driver.WebDriver;
        private Log Log => Browser.Log;
        private DefaultWait<IWebDriver> DefaultWait { get; }
    }
}
