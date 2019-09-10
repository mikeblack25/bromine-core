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
    public class RollingLogFileTests : IDisposable
    {
        /// <summary>
        /// Verify Log.Message logs to a rolling log file.
        /// </summary>
        [Fact]
        public void LogMessageTest()
        {
            Message = "This is an INFO message";
            // ReSharper disable once PossibleNullReferenceException
            TestName = $"{System.Reflection.MethodBase.GetCurrentMethod().Name}.txt";

            Log = new Log(TestName);
            Log.ClearRollingFileAppender();

            Log.Message(Message);
        }

        /// <summary>
        /// Verify Log.Error logs to a rolling log file.
        /// </summary>
        [Fact]
        public void LogErrorTest()
        {
            Message = "This is an ERROR message.";
            // ReSharper disable once PossibleNullReferenceException
            TestName = $"{System.Reflection.MethodBase.GetCurrentMethod().Name}.txt";

            Log = new Log(TestName);
            Log.ClearRollingFileAppender();

            Log.Error(Message);
        }

        /// <summary>
        /// Verify Log.Error logs to a rolling log file.
        /// </summary>
        [Fact]
        public void LogDebugTest()
        {
            Message = "This is a DEBUG message.";
            // ReSharper disable once PossibleNullReferenceException
            TestName = $"{System.Reflection.MethodBase.GetCurrentMethod().Name}.txt";

            Log = new Log(TestName);
            Log.ClearRollingFileAppender();

            Log.Debug(Message);
        }

        /// <summary>
        /// Release the RollingFileLock and assert Message is found in the log 1 time.
        /// </summary>
        public void Dispose()
        {
            Log.ReleaseRollingFileLock();
            var uniqueMessageCount = Regex.Matches(ReadLogFromFile(), Message).Count;

            Log.Stop();

            Assert.Equal(1, uniqueMessageCount);
            Assert.Equal(1, MessageCount);
        }

        private string ReadLogFromFile()
        {
            MessageCount = 0;
            var builder = new StringBuilder();

            if (File.Exists(Log.FilePath))
            {
                using (var reader = new StreamReader(Log.FilePath))
                {
                    var line = string.Empty;

                    while ((line = reader.ReadLine()) != null)
                    {
                        MessageCount++;
                        builder.AppendLine(line);
                    }
                }
            }

            return builder.ToString();
        }

        private int MessageCount { get; set; }
        private string Message { get; set; }
        private string TestName { get; set; }
        private Log Log { get; set; }
    }
}
