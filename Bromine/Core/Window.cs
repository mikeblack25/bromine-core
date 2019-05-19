using System;
using System.Collections.Generic;
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
            _driver = driver;
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
                    Exceptions.Add(e);
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
                    Exceptions.Add(e);
                }
            }
        }

        /// <summary>
        /// <see cref="Driver.Exceptions"/>
        /// </summary>
        public List<Exception> Exceptions => _driver.Exceptions;

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
                Exceptions.Add(e);
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
                Exceptions.Add(e);
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
                Exceptions.Add(e);
            }
        }

        private readonly Driver _driver;     
        private IWindow BrowserWindow => _driver.Window;
    }
}
