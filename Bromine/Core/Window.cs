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
        /// <param name="driver">Driver used to navigate.</param>
        public Window(Driver driver)
        {
            Driver = driver;
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
                    Driver.LogManager.Error(e.Message);
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
                    Driver.LogManager.Error(e.Message);
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
                Driver.LogManager.Error(e.Message);
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
                Driver.LogManager.Error(e.Message);
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
                Driver.LogManager.Error(e.Message);
            }
        }

        private Driver Driver { get; }
        private IWindow BrowserWindow => Driver.WebDriver.Manage().Window;
    }
}
