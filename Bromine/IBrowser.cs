using System;
using System.Collections.Generic;
using System.Drawing;

using Bromine.Core;
using Bromine.Models;

using OpenQA.Selenium;

namespace Bromine
{
    /// <summary>
    /// Provides ability to interact with a web browser.
    /// </summary>
    public interface IBrowser : IDisposable
    {
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
        /// Directory where ScreenShots are saved.
        /// </summary>
        string ScreenShotDirectory { get; }

        /// <summary>
        /// Name of the last ScreenShot file.
        /// </summary>
        string ScreenShotName { get; set; }

        /// <summary>
        /// String combining the ScreenShotDirectory and the ScreenShotName.
        /// </summary>
        string ScreenShotPath { get; }

        /// <summary>
        /// Get the driver logs.
        /// </summary>
        ILogs Logs { get; }

        /// <summary>
        /// Manipulate cookies.
        /// </summary>
        ICookieJar Cookies { get; }

        /// <summary>
        /// Manipulate currently focused window.
        /// </summary>
        IWindow Window { get; }

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
        /// Helpers to navigate to pages and files.
        /// </summary>
        Navigate Navigate { get; }

        /// <summary>
        /// List of exceptions.
        /// </summary>
        List<Exception> Exceptions { get; }

        /// <summary>
        /// Browser configuration used to initialize the web driver.
        /// </summary>
        BrowserOptions BrowserOptions { get; }

        /// <summary>
        /// Last image saved at the <see cref="ScreenShotDirectory"/>.
        /// </summary>
        Image LastImage { get; }

        /// <summary>
        /// Size of the last image saved at the <see cref="ScreenShotDirectory"/>.
        /// </summary>
        Size LastImageSize { get; }

        /// <summary>
        /// Namespace of the Selenium driver being used by the browser.
        /// </summary>
        string Information { get; }

        /// <summary>
        /// Wait for a given condition to be true.
        /// </summary>
        /// <param name="condition">Condition to check every 250 ms for the specified wait time.</param>
        /// <param name="timeToWait">Time in seconds to wait for the condition to be satisfied.</param>
        /// <returns></returns>
        bool Wait(Func<bool> condition, int timeToWait = 1);

        /// <summary>
        /// Take a ScreenShot of requested element.
        /// </summary>
        /// <param name="name">Name of the file of the ScreenShot.</param>
        /// <param name="element">Element to take a ScreenShot of.</param>
        void TakeElementScreenShot(string name, Element element);

        /// <summary>
        /// Take a ScreenShot of requested region on the screen.
        /// </summary>
        /// <param name="name">Name of the file of the ScreenShot.</param>
        /// <param name="screenShotRegion">Region to take a ScreenShot of.</param>
        void TakeRegionScreenShot(string name, Rectangle screenShotRegion);

        /// <summary>
        /// Take a ScreenShot of the visible page.
        /// </summary>
        /// <param name="name">Name of the file of the ScreenShot.</param>
        void TakeVisibleScreenShot(string name);

        /// <summary>
        /// Execute the jS script.
        /// </summary>
        /// <param name="script">JavaScript to run on the given page context.</param>
        /// <returns>Response from the JS request.</returns>
        object ExecuteJs(string script);
    }
}
