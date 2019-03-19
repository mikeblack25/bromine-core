using System;
using System.Collections.Generic;

namespace Bromine.Core
{
    /// <summary>
    /// Class to enable browser navigation.
    /// </summary>
    public class Navigate
    {
        /// <summary>
        /// Construct a Navigate object for the given driver type.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="exceptions"></param>
        public Navigate(Driver driver, List<Exception> exceptions)
        {
            Driver = driver;
            Exceptions = exceptions;
        }

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to.</param>
        public void ToUrl(string url)
        {
            try
            {
                Driver.WebDriver.Navigate().GoToUrl(url);
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
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
                Driver.WebDriver.Navigate().GoToUrl($"file://{path}");
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
        }

        /// <summary>
        /// Navigate back to the previous page.
        /// </summary>
        public void Back()
        {
            Driver.NavigateBack();
        }

        /// <summary>
        /// Navigate forward.
        /// </summary>
        public void Forward()
        {
            Driver.NavigateForward();
        }

        /// <summary>
        /// Refresh the current page.
        /// </summary>
        public void Refresh()
        {
            Driver.Refresh();
        }

        private Driver Driver { get; }

        private List<Exception> Exceptions { get; }
    }
}
