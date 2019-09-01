using System;
using System.Collections.Generic;

using Bromine.Core.ElementInteraction;

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
        }

        /// <summary>
        /// Wait for the given element to be displayed.
        /// </summary>
        /// <param name="element">Element to wait for.</param>
        /// <param name="timeToWait">Time in seconds to wait for the condition to be true.</param>
        public void DisplayedElement(Element element, int timeToWait) => Condition(() => element.Displayed, timeToWait);

        /// <summary>
        /// Wait for the given element to be visible.
        /// </summary>
        /// <param name="element">Element to wait for.</param>
        /// <param name="timeToWait">Time in seconds to wait for the condition to be true.</param>
        public void VisibleElement(Element element, int timeToWait) => Condition(() => element.Enabled, timeToWait);

        /// <summary>
        /// Wait for the given URL to be loaded.
        /// </summary>
        /// <param name="expectedUrl">Expected URL to wait for.</param>
        /// <param name="timeToWait">Time in seconds to wait for the condition to be true.</param>
        public void Navigation(string expectedUrl, int timeToWait) => Condition(() => Browser.Url == expectedUrl, timeToWait);

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
            var result = false;

            try
            {
                var wait = new DefaultWait<IWebDriver>(Driver.WebDriver)
                {
                    Timeout = TimeSpan.FromSeconds(timeToWait),
                    PollingInterval = TimeSpan.FromMilliseconds(250)
                };

                wait.Until(x => condition());

                result = true;
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }

            return result;
        }

        private const string PageLoadedScript = "\"return document.readyState\").Equals(\"complete\")";

        private Browser Browser { get; }
        private Driver Driver => Browser.Driver;
        private List<Exception> Exceptions => Browser.Exceptions;
    }
}
