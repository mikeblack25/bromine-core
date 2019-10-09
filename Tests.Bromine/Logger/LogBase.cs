using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using Bromine.Logger;

using Xunit;

namespace Tests.Bromine.Logger
{
    /// <summary>
    /// Tests to verify the rolling log file works as expected.
    /// </summary>
    public class LogBase : IDisposable
    {
        /// <summary>
        /// Message to log.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Log instance.
        /// </summary>
        public LogManager LogManager { get; set; }

        /// <summary>
        /// Release the RollingFileLock and assert Message is found in the log 1 time.
        /// </summary>
        public void Dispose()
        {
            LogManager.Dispose();

            if (LogManager.TextLog != null)
            {
                VerifyLogMessages(LogManager.TextLog);
            }
        }

        /// <summary>
        /// Read the log for the current test.
        /// </summary>
        /// <returns></returns>
        public string ReadLogFromFile(global::Bromine.Logger.LogBase log)
        {
            MessageCount = 0;
            var builder = new StringBuilder();

            if (File.Exists(log.LogPath))
            {
                using (var reader = new StreamReader(log.LogPath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        MessageCount++;
                        builder.AppendLine(line);
                    }
                }
            }

            return builder.ToString();
        }

        internal const string InfoMessageString = "This is an INFO message";
        internal const string ErrorMessageString = "This is an ERROR message";

        private void VerifyLogMessages(global::Bromine.Logger.LogBase log)
        {
            var uniqueMessageCount = Regex.Matches(ReadLogFromFile(log), Message).Count;

            Assert.Equal(1, uniqueMessageCount);
            Assert.Equal(1, MessageCount);
        }

        private int MessageCount { get; set; }
    }
}
