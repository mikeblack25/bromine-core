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
        public Log Log { get; set; }

        /// <summary>
        /// Release the RollingFileLock and assert Message is found in the log 1 time.
        /// </summary>
        public void Dispose()
        {
            Log.Dispose();
            var uniqueMessageCount = Regex.Matches(ReadLogFromFile(), Message).Count;

            Assert.Equal(1, uniqueMessageCount);
            Assert.Equal(1, MessageCount);
        }

        /// <summary>
        /// Read the log for the current test.
        /// </summary>
        /// <returns></returns>
        public string ReadLogFromFile()
        {
            MessageCount = 0;
            var builder = new StringBuilder();

            if (File.Exists(Log.LogName))
            {
                using (var reader = new StreamReader(Log.LogName))
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

        private int MessageCount { get; set; }
    }
}
