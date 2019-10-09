using System;

using Bromine.Logger;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Logger
{
    /// <summary>
    /// Tests to verify the rolling log file works as expected.
    /// </summary>
    public class XunitLogTests : IDisposable
    {
        /// <inheritdoc />
        /// <param name="output"></param>
        public XunitLogTests(ITestOutputHelper output)
        {
            Output = output;
        }

        /// <summary>
        /// Verify Log.Message logs to the console when using Xunit.
        /// </summary>
        [Fact]
        public void LogMessageTest()
        {
            Message = "This is an INFO message";

            Log = new Log(Output, string.Empty);

            Log.Message(Message);
        }

        /// <summary>
        /// Verify Log.Error logs to the console when using Xunit.
        /// </summary>
        [Fact]
        public void LogErrorTest()
        {
            Message = "This is an ERROR message.";

            Log = new Log(Output, string.Empty);

            Log.Error(Message);
        }

        /// <summary>
        /// Verify Log.Error logs to the console when using Xunit.
        /// </summary>
        [Fact]
        public void LogDebugTest()
        {
            Message = "This is a DEBUG message.";

            Log = new Log(Output, string.Empty);

            Log.Debug(Message);
        }

        /// <summary>
        /// Stop logging and ensure one log message exists in the XunitAppender.
        /// </summary>
        public void Dispose()
        {
            Log.Dispose();
        }

        private string Message { get; set; }
        private Log Log { get; set; }
        private ITestOutputHelper Output { get; }
    }
}
