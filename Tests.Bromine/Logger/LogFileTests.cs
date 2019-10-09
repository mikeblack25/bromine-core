using System.Reflection;

using Bromine.Logger;

using Xunit;

namespace Tests.Bromine.Logger
{
    /// <summary>
    /// Tests to verify the rolling log file works as expected.
    /// </summary>
    public class LogFileTests : LogBase
    {
        /// <summary>
        /// Verify Log.Message logs to a rolling log file.
        /// </summary>
        [Fact]
        public void LogMessageTest()
        {
            Message = "This is an INFO message";

            Log = new Log(MethodBase.GetCurrentMethod().Name, Log.MdExtension);

            Log.Message(Message);
        }

        /// <summary>
        /// Verify Log.Error logs to a rolling log file.
        /// </summary>
        [Fact]
        public void LogErrorTest()
        {
            Message = "This is an ERROR message.";

            Log = new Log(MethodBase.GetCurrentMethod().Name, Log.MdExtension);

            Log.Error(Message);
        }

        /// <summary>
        /// Verify Log.Error logs to a rolling log file.
        /// </summary>
        [Fact]
        public void LogDebugTest()
        {
            Message = "This is a DEBUG message.";

            Log = new Log(MethodBase.GetCurrentMethod().Name, Log.MdExtension);

            Log.Debug(Message);
        }
    }
}
