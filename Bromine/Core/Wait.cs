using System;
using System.Threading;

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
            Browser = browser;
            DefaultWait = new DefaultWait<IWebDriver>(Driver)
            {
                PollingInterval = TimeSpan.FromMilliseconds(250)

                    
            };
        }

        /// <summary>
        /// Wait for the specified time. To use milliseconds set timeInMilliseconds to true.
        /// </summary>
        /// <param name="time">Time to wait, if timeInMilliseconds is false this will be time in seconds.</param>
        /// <param name="timeInMilliseconds">When true the time will be in milliseconds, seconds are used otherwise.</param>
        /// <param name="message">Optional information to add to the log message.</param>
        public void ForTime(int time, bool timeInMilliseconds = false, string message = "")
        {
            var timeUnit = !timeInMilliseconds ? "seconds" : "milliseconds";
            Log.Framework($"Wait {time} {timeUnit} {message}".Trim());

            if (!timeInMilliseconds)
            {
                time *= 1000;
            }
            Thread.Sleep(time);
        }

        /// <summary>
        /// Wait for the given element to be displayed.
        /// </summary>
        /// <param name="element">Element to wait for.</param>
        /// <param name="timeToWait">Time in seconds to wait for the condition to be true.</param>
        public void ForDisplayedElement(IElement element, int timeToWait) => ForCondition(() => element.Displayed, timeToWait);

        /// <summary>
        /// Wait for the given URL to be loaded.
        /// </summary>
        /// <param name="expectedUrl">Expected URL to wait for.</param>
        /// <param name="timeToWait">Time in seconds to wait for the condition to be true.</param>
        public void ForNavigation(string expectedUrl, int timeToWait) => ForCondition(() => Browser.Url == expectedUrl, timeToWait);

        /// <summary>
        /// Wait for the given condition to be true.
        /// </summary>
        /// <param name="condition">Condition to wait for.</param>
        /// <param name="timeToWait">Time in seconds to wait for the condition to be true.</param>
        /// <returns></returns>
        public bool ForCondition(Func<bool> condition, int timeToWait)
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

        private Browser Browser { get; }
        private IWebDriver Driver => Browser.Driver.WebDriver;
        private Log Log => Browser.Log;
        private DefaultWait<IWebDriver> DefaultWait { get; }
    }
}
