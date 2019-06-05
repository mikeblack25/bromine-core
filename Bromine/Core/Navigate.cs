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
        /// <param name="driver">Driver used to navigate.</param>
        public Navigate(Driver driver)
        {
            _driver = driver;
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
                _driver.WebDriver.Navigate().GoToUrl($"file://{path}");
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
            try
            {
                _driver.WebDriver.Navigate().Back();
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
        }

        /// <summary>
        /// Navigate forward.
        /// </summary>
        public void Forward()
        {
            try
            {
                _driver.WebDriver.Navigate().Forward();
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
        }

        /// <summary>
        /// Refresh the current page.
        /// </summary>
        public void Refresh()
        {
            try
            {
                _driver.WebDriver.Navigate().Refresh();
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
        }

        private readonly Driver _driver;
        private List<Exception> Exceptions => _driver.Exceptions;
    }
}
