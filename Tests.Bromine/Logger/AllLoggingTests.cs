using Bromine.Logger;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Logger
{
    /// <summary>
    /// Tests to verify the rolling log file works as expected.
    /// </summary>
    public class AllLoggingTests : LogBase
    {
        /// <inheritdoc />
        public AllLoggingTests(ITestOutputHelper output)
        {
            LogManager = new LogManager(output, string.Empty, LogType.XunitConsole, LogType.Text);
        }

        /// <summary>
        /// Verify Log.Message logs to a rolling log file.
        /// </summary>
        [Fact]
        public void LogMessageTest()
        {
            Message = InfoMessageString;
            LogManager.Message(Message);
        }

        /// <summary>
        /// Verify Log.Error logs to a rolling log file.
        /// </summary>
        [Fact]
        public void LogErrorTest()
        {
            Message = ErrorMessageString;
            LogManager.Error(Message);
        }
    }
}
