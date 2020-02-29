using System.Drawing;
using Bromine.Core;

using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Tests the behavior of Core.Window.
    /// </summary>
    public class Window : Framework
    {
        /// <summary>
        /// Launch a browser and set the initial windows size to width = 200, height = 200.
        /// </summary>
        public Window(ITestOutputHelper output) : base(output, false, LogLevels.Framework)
        {
            WindowObject.Size = new Size(200, 200);
        }

        /// <summary>
        /// Can the browser window be minimized?
        /// </summary>
        [Fact]
        public void MinimizeWindowTest()
        {
            Browser.ConditionalVerify.False(WindowObject.IsMinimized, global::Bromine.Core.Window.WindowIsMinimizedMessage);

            Browser.Window.Minimize();

            Browser.Verify.True(WindowObject.IsMinimized, global::Bromine.Core.Window.WindowIsMinimizedMessage);
        }

        /// <summary>
        /// Can the browser window be resized?
        /// </summary>
        [Fact]
        public void CustomWindowSizeTest()
        {
            var expectedSize = new Size(600, 500);

            WindowObject.Minimize();

            Browser.Verify.False(WindowObject.IsCustom, CustomMessage);
            Browser.ConditionalVerify.NotEqual(expectedSize, WindowObject.Size);

            WindowObject.Size = expectedSize;

            Browser.Verify.True(WindowObject.IsCustom, CustomMessage);
            Browser.Verify.Equal(expectedSize, WindowObject.Size);
        }

        /// <summary>
        /// Widths below 516 are not returned correctly from the call to Window.Size.
        /// </summary>
        [Fact]
        public void MinimumCustomWindowSizeTest()
        {
            var expectedSize = new Size(515, 500);

            WindowObject.Size = expectedSize;

            Assert.Throws<EqualException>(() => Browser.Verify.Equal(expectedSize, WindowObject.Size, "Expected to fail to prove size lower than 516 is not supported"));
        }

        /// <summary>
        /// Can the browser window position be changed?
        /// </summary>
        [Fact]
        public void CustomWindowPositionTest()
        {
            var initialPosition = WindowObject.Position;
            var expectedPosition = new Point(initialPosition.X + 50, initialPosition.Y + 50);

            WindowObject.Position = expectedPosition;

            Browser.Verify.True(WindowObject.IsCustom, CustomMessage);
            Browser.Verify.Equal(expectedPosition, WindowObject.Position);
        }

        /// <summary>
        /// Can the browser window be maximized?
        /// </summary>
        [Fact]
        public void MaximizeWindowTest()
        {
            Browser.ConditionalVerify.False(WindowObject.IsMaximized, global::Bromine.Core.Window.WindowIsMaximizedMessage);

            Browser.Window.Maximize();

            Browser.Verify.True(WindowObject.IsMaximized, global::Bromine.Core.Window.WindowIsMaximizedMessage);
        }

        /// <summary>
        /// Can the browser window be in full screen?
        /// </summary>
        [Fact]
        public void FullScreenWindowTest()
        {
            Browser.ConditionalVerify.False(WindowObject.IsFullScreen, global::Bromine.Core.Window.WindowIsFullScreenMessage);

            Browser.Window.FullScreen();

            Browser.Verify.True(WindowObject.IsFullScreen, global::Bromine.Core.Window.WindowIsFullScreenMessage);
        }

        private const string CustomMessage = "Window.IsCustom";

        private global::Bromine.Core.Window WindowObject => Browser.Window;
    }
}
