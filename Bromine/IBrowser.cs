using System;
using System.Drawing;

using Bromine.Core;
using Bromine.Element;
using Bromine.Verifies;

using OpenQA.Selenium;

namespace Bromine
{
    /// <summary>
    /// Provides ability to interact with a web browser.
    /// </summary>
    public interface IBrowser : IDisposable
    {
        /// <summary>
        /// <see cref="Log"/>
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Url of the current page.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// HTML Title of the current page.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Get the HTML source (DOM).
        /// </summary>
        string Source { get; }

        /// <summary>
        /// Manipulate currently focused window.
        /// </summary>
        Window Window { get; }

        /// <summary>
        /// Position of the browser window.
        /// </summary>
        Point Position { get; }

        /// <summary>
        /// Size of the browser window.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Helpers to find elements.
        /// </summary>
        Find Find { get; }

        /// <summary>
        /// Helpers to find elements using Selenium location strategies.
        /// </summary>
        SeleniumFind SeleniumFind { get; }

        /// <summary>
        /// Helpers to navigate to pages and files.
        /// </summary>
        Navigate Navigate { get; }

        /// <summary>
        /// Browser configuration used to initialize the web driver.
        /// </summary>
        BrowserOptions BrowserOptions { get; }

        /// <summary>
        /// Provide ability change the style of Elements.
        /// </summary>
        ElementStyle ElementStyle { get; }

        /// <summary>
        /// Provides Wait behavior using Selenium's DefaultWait class.
        /// </summary>
        Wait Wait { get; }

        /// <summary>
        /// Namespace of the Selenium driver being used by the browser.
        /// </summary>
        string Information { get; }

        /// <summary>
        /// Assert expected conditions.
        /// Execution will stop when a verify condition fails.
        /// </summary>
        Verify Verify { get; }

        /// <summary>
        /// Assert expected conditions.
        /// Execution will stop when a verify condition fails and the test will be marked as skipped.
        /// </summary>
        ConditionalVerify ConditionalVerify { get; }

        /// <summary>
        /// Assert expected conditions.
        /// Execution will not stop when a verify condition fails.
        /// The test will be marked as failed if any exceptions occur during execution.
        /// </summary>
        SoftVerify SoftVerify { get; }

        /// <summary>
        /// Configure Selenium <see cref="IWebDriver"/> for the session.
        /// </summary>
        Driver Driver { get; }

        /// <summary>
        /// Take a Image of requested element.
        /// </summary>
        /// <param name="name">Name of the file of the Image.</param>
        /// <param name="element">Element to take a Image of.</param>
        void TakeElementImage(string name, Element.Element element);

        /// <summary>
        /// Take a Image of requested region on the screen.
        /// </summary>
        /// <param name="name">Name of the file of the Image.</param>
        /// <param name="imageRegion">Region to take a Image of.</param>
        void TakeRegionImage(string name, Rectangle imageRegion);

        /// <summary>
        /// Take a Image of the visible page.
        /// </summary>
        /// <param name="name">Name of the file of the Image.</param>
        void TakeVisibleImage(string name);

        /// <summary>
        /// Execute the jS script.
        /// </summary>
        /// <param name="script">JavaScript to run on the given page context.</param>
        /// <param name="arguments">Optional arguments to pass to the call.</param>
        /// <returns>Response from the JS request.</returns>
        object ExecuteJs(string script, object[] arguments);
    }
}
