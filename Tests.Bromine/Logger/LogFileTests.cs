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
            Log = new Log(MethodBase.GetCurrentMethod().Name, LogType.Text);
            Message = InfoMessageString;
            Log.Message(Message);
        }

        /// <summary>
        /// Verify Log.Error logs to a rolling log file.
        /// </summary>
        [Fact]
        public void LogErrorTest()
        {
            Log = new Log(MethodBase.GetCurrentMethod().Name, LogType.Text);
            Message = ErrorMessageString;
            Log.Error(ErrorMessageString);
        }
    }
}
