using System.Drawing;

using OpenQA.Selenium;

namespace Bromine.Core
{
    /// <summary>
    /// Configure and get size and location information for the browser window.
    /// </summary>
    public class Window : IWindow
    {
        /// <summary>
        /// Construct a window object for the given driver.
        /// </summary>
        /// <param name="browser">Browser used to navigate.</param>
        public Window(IBrowser browser)
        {
            Browser = browser;
        }

        /// <summary>
        /// Is the window minimized?
        /// </summary>
        public bool IsMinimized
        {
            get => _isMinimized;
            private set
            {
                _isMinimized = value;
                _isCustom = false;
                _isMaximized = false;
                _isFullScreen = false;
                Log.Framework(WindowIsMinimizedMessage);
                LogUpdateMessage();
            }
        }

        /// <summary>
        /// Has the window been set to a custom size and / or position?
        /// Not Minimized, Maximized, or FullScreen.
        /// </summary>
        public bool IsCustom
        {
            get => _isCustom;
            private set
            {
                _isCustom = value;
                _isMinimized = false;
                _isMaximized = false;
                _isFullScreen = false;
                LogUpdateMessage();
            }
        }

        /// <summary>
        /// Is the window maximized?
        /// </summary>
        public bool IsMaximized
        {
            get => _isMaximized;
            private set
            {
                _isMaximized = value;
                _isMinimized = false;
                _isCustom = false;
                _isFullScreen = false;
                Log.Framework(WindowIsMaximizedMessage);
                LogUpdateMessage();
            }
        }

        /// <summary>
        /// Is the window full screen?
        /// </summary>
        public bool IsFullScreen
        {
            get => _isFullScreen;
            private set
            {
                _isFullScreen = value;
                _isMinimized = false;
                _isCustom = false;
                _isMaximized = false;
                Log.Framework(WindowIsFullScreenMessage);
                LogUpdateMessage();
            }
        }

        /// <summary>
        /// Position of the browser window.
        /// </summary>
        public Point Position
        {
            get => BrowserWindow.Position;
            set
            {
                BrowserWindow.Position = value;
                IsCustom = true;
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
                BrowserWindow.Size = value;
                IsCustom = true;
            }
        }

        /// <summary>
        /// Minimize the window.
        /// </summary>
        public void Minimize()
        {
            BrowserWindow.Minimize();
            IsMinimized = true;
        }

        /// <summary>
        /// Maximize the window.
        /// </summary>
        public void Maximize()
        {
            BrowserWindow.Maximize();
            IsMaximized = true;
        }

        /// <summary>
        /// Maximize the size of the browser window.
        /// </summary>
        public void FullScreen()
        {
            BrowserWindow.FullScreen();
            IsFullScreen = true;
        }

        /// <summary>
        /// Window is minimized
        /// </summary>
        public const string WindowIsMinimizedMessage = "Window is minimized";

        /// <summary>
        /// Window is maximized
        /// </summary>
        public const string WindowIsMaximizedMessage = "Window is maximized";

        /// <summary>
        /// Window is full screen.
        /// </summary>
        public const string WindowIsFullScreenMessage = "Window is full screen";

        private void LogUpdateMessage()
        {
            Log.Framework($"Window Size: height = {Size.Height} width = {Size.Width} Position: x = {Position.X} y = {Position.Y}");
        }

        private IWindow BrowserWindow => Driver.WebDriver.Manage().Window;

        private bool _isMinimized;
        private bool _isCustom;
        private bool _isMaximized;
        private bool _isFullScreen;

        private IBrowser Browser { get; }
        private Driver Driver => Browser.Driver;
        private Log Log => Browser.Log;
    }
}
