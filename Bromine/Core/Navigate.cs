using System;

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
            Driver = driver;
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
                Driver.LogManager.Message($"Navigate to {url}");

            }
            catch (Exception e)
            {
                Driver.LogManager.Error(e.Message);
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
            catch (Exception e)
            {
                Driver.LogManager.Error(e.Message);
            }
        }

        /// <summary>
        /// Navigate back to the previous page.
        /// </summary>
        public void Back()
        {
            try
            {
                Driver.WebDriver.Navigate().Back();
            }
            catch (Exception e)
            {
                Driver.LogManager.Error(e.Message);
            }
        }

        /// <summary>
        /// Navigate forward.
        /// </summary>
        public void Forward()
        {
            try
            {
                Driver.WebDriver.Navigate().Forward();
            }
            catch (Exception e)
            {
                Driver.LogManager.Error(e.Message);
            }
        }

        /// <summary>
        /// Refresh the current page.
        /// </summary>
        public void Refresh()
        {
            try
            {
                Driver.WebDriver.Navigate().Refresh();
            }
            catch (Exception e)
            {
                Driver.LogManager.Error(e.Message);
            }
        }

        private Driver Driver;
    }
}
