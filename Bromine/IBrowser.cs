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
        /// Directory where screenshots are saved.
        /// </summary>
        string ScreenshotPath { get; }

        /// <summary>
        /// Helpers to find elements.
        /// </summary>
        Find Find { get; }

        /// <summary>
        /// Helpers to navigate to pages and files.
        /// </summary>
        Navigate Navigate { get; }

        /// <summary>
        /// Helpers to interact with the browser window.
        /// </summary>
        IWindow Window { get; }

        /// <summary>
        /// List of exceptions.
        /// </summary>
        List<Exception> Exceptions { get; }

        /// <summary>
        /// Browser configuration used to initialize the web driver.
        /// </summary>
        BrowserConfiguration BrowserConfiguration { get; }

        /// <summary>
        /// Last image saved at the <see cref="ScreenshotPath"/>.
        /// </summary>
        Image LastImage { get; }

        /// <summary>
        /// Size of the last image saved at the <see cref="ScreenshotPath"/>.
        /// </summary>
        Size LastImageSize { get; }

        /// <summary>
        /// Wait for a given condition to be true.
        /// </summary>
        /// <param name="condition">Condition to check every 250 ms for the specified wait time.</param>
        /// <param name="timeToWait">Time in seconds to wait for the condition to be satisfied.</param>
        /// <returns></returns>
        bool Wait(Func<bool> condition, int timeToWait = 1);

        /// <summary>
        /// Take a screenshot of requested element.
        /// </summary>
        /// <param name="name">Name of the file of the screenshot.</param>
        /// <param name="element">Element to take a screenshot of.</param>
        void TakeElementScreenshot(string name, Element element);

        /// <summary>
        /// Take a screenshot of requested region on the screen.
        /// </summary>
        /// <param name="name">Name of the file of the screenshot.</param>
        /// <param name="screenShotRegion">Region to take a screenshot of.</param>
        void TakeRegionScreenshot(string name, Rectangle screenShotRegion);

        /// <summary>
        /// Take a screenshot of the visible page.
        /// </summary>
        /// <param name="name">Name of the file of the screenshot.</param>
        void TakeVisibleScreenshot(string name);
    }
}
