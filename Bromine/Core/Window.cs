using System;
using System.Drawing;
using OpenQA.Selenium;

namespace Bromine.Core
{
    /// <summary>
    /// Interact with the Browser Window.
    /// </summary>
    public class Window : IWindow
    {
        /// <summary>
        /// Construct a window object for the given driver.
        /// </summary>
        /// <param name="browser">Browser used to navigate.</param>
        public Window(Browser browser)
        {
            Browser = browser;
        }

        /// <summary>
        /// Position of the browser window.
        /// </summary>
        public Point Position
        {
            get => BrowserWindow.Position;
            set
            {
                try
                {
                    BrowserWindow.Position = value;
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }            
            }
        } 

        /// <summary>
        /// Size of the browser window.
        /// </summary>
        public Size Size
        {
            get => BrowserWindow.Size;
            set
            {
                try
                {
                    BrowserWindow.Size = value;
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }
            }
        }

        /// <summary>
        /// Maximize the window.
        /// </summary>
        public void Maximize()
        {
            try
            {
                BrowserWindow.Maximize();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        /// <summary>
        /// Minimize the window.
        /// </summary>
        public void Minimize()
        {
            try
            {
                BrowserWindow.Minimize();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        /// <summary>
        /// Maximize the size of the browser window.
        /// </summary>
        public void FullScreen()
        {
            try
            {
                BrowserWindow.FullScreen();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        private Browser Browser { get; }
        private IWebDriver Driver => Browser.Driver.WebDriver;
        private Log Log => Browser.Log;
        private IWindow BrowserWindow => Driver.Manage().Window;
    }
}
