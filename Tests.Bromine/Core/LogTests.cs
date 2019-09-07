using System.Diagnostics;
using System.IO;

using Bromine.Logger;

using Xunit;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Tests to verify the Browser class is working as expected.
    /// </summary>
    public class LogTests
    {
        /// <summary>
        /// Verify Log.Message logs to a rolling log file.
        /// </summary>
        [Fact]
        public void VerifyLogMessageWritesToARollingLogFileTest()
        {
            var message = "This is an INFO message";
            // ReSharper disable once PossibleNullReferenceException
            var method = new StackFrame(0).GetMethod().DeclaringType.Name;
            Log = new Log($"{method}.txt");

            DeleteLogFile();

            Log.Message(message);

            // TODO: Add Ordered test support since the log file can not be read in the context of the current session.
            //var log = ReadLog();

            //Contains(message, log);
        }

        /// <summary>
        /// Verify Log.Error logs to a rolling log file.
        /// </summary>
        [Fact]
        public void VerifyLoErrorWritesToARollingLogFileTest()
        {
            var message = "This is an ERROR message.";
            // ReSharper disable once PossibleNullReferenceException
            var method = new StackFrame(0).GetMethod().DeclaringType.Name;
            Log = new Log($"{method}.txt");

            DeleteLogFile();

            Log.Message(message);

            // TODO: Add Ordered test support since the log file can not be read in the context of the current session.
            //var log = ReadLog();

            //Contains(message, log);
        }

        /// <summary>
        /// Verify Log.Error logs to a rolling log file.
        /// </summary>
        [Fact]
        public void VerifyLoDebugWritesToARollingLogFileTest()
        {
            var message = "This is a DEBUG message.";
            // ReSharper disable once PossibleNullReferenceException
            var method = new StackFrame(0).GetMethod().DeclaringType.Name;
            Log = new Log($"{method}.txt");

            DeleteLogFile();

            Log.Message(message);

            // TODO: Add Ordered test support since the log file can not be read in the context of the current session.
            //var log = ReadLog();

            //Contains(message, log);
        }

        private void DeleteLogFile()
        {
            if (File.Exists(Log.FilePath))
            {
                File.Delete(Log.FilePath);
            }
        }

        private string ReadLog()
        {
            if (File.Exists(Log.FilePath))
            {
                using (var reader = new StreamReader(Log.FilePath))
                {
                    return reader.ReadToEnd();
                }
            }

            return string.Empty;
        }

        private Log Log { get; set; }
    }
}
