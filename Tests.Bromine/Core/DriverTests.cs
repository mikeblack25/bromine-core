using System;
using System.Collections.Generic;

using Bromine.Constants;
using Bromine.Core;
using Bromine.Models;

using Tests.Bromine.Common;

using Xunit;

using static Xunit.Assert;

namespace Tests.Bromine.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Test the behavior of the Driver class.
    /// </summary>
    public class DriverTests: IDisposable
    {
        /// <summary>
        /// Test default Driver constructor.
        /// </summary>
        /// <param name="browser">Browser to launch.</param>
        [Theory]
        [InlineData(BrowserType.Chrome), Trait(Category.Browser, Category.Chrome)]
        [InlineData(BrowserType.Firefox), Trait(Category.Browser, Category.Firefox)]
        public void InitializeBrowserDefaultsTest(BrowserType browser)
        {
            var driverOptions = new BrowserConfiguration(browser);
            Driver = new Driver(driverOptions, new List<Exception>());

            NotNull(Driver.WebDriver);         
        }

        /// <summary>
        /// Test _driver constructor with headless mode.
        /// </summary>
        /// <param name="browser">Browser to launch.</param>
        [Theory]
        [InlineData(BrowserType.Chrome), Trait(Category.Browser, Category.Chrome)]
        [InlineData(BrowserType.Firefox), Trait(Category.Browser, Category.Firefox)]
        public void InitializeBrowserIsHeadlessTest(BrowserType browser)
        {
            var driverOptions = new BrowserConfiguration(browser, true);
            Driver = new Driver(driverOptions, new List<Exception>());

            NotNull(Driver.WebDriver);
        }

        /// <inheritdoc />
        /// <summary>
        /// Dispose of the _driver resource.
        /// </summary>
        public void Dispose()
        {
            Driver?.Dispose();
        }

        protected Driver Driver;
    }
}
