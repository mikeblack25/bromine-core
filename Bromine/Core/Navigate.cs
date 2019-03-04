using System;
using System.Collections.Generic;

namespace Bromine.Core
{
    /// <summary>
    /// Class to enable with browser navigation.
    /// </summary>
    public class Navigate
    {
        /// <summary>
        /// Construct a Navigate object for the given driver type.
        /// </summary>
        /// <param name="driver"></param>
        public Navigate(Driver driver, List<Exception> exceptions)
        {
            _driver = driver;
            _exceptions = exceptions;
        }

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to.</param>
        public void ToUrl(string url)
        {
            try
            {
                _driver.WebDriver.Navigate().GoToUrl(url);
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
            }
        }

        /// <summary>
        /// Navigate to the given file.
        /// </summary>
        /// <param name="path"></param>
        public void ToFile(string path)
        {
            try
            {
                _driver.WebDriver.Navigate().GoToUrl($"file://{path}");
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
            }
        }

        /// <summary>
        /// Navigate back to the previous page.
        /// </summary>
        public void Back()
        {
            _driver.NavigateBack();
        }

        /// <summary>
        /// Navigate forward.
        /// </summary>
        public void Forward()
        {
            _driver.NavigateForward();
        }

        /// <summary>
        /// Refresh the current page.
        /// </summary>
        public void Refresh()
        {
            _driver.Refresh();
        }

        private Driver _driver { get; }

        private List<Exception> _exceptions { get; }
    }
}
