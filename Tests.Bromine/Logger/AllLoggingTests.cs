﻿using Bromine.Logger;

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
            Output = output;
        }

        /// <summary>
        /// Verify Log.Message logs to a rolling log file.
        /// </summary>
        [Fact]
        public void LogMessageTest()
        {
            Message = "This is an INFO message";

            Log = new Log(Output);

            Log.Message(Message);
        }

        /// <summary>
        /// Verify Log.Error logs to a rolling log file.
        /// </summary>
        [Fact]
        public void LogErrorTest()
        {
            Message = "This is an ERROR message.";

            Log = new Log(Output);

            Log.Error(Message);
        }

        /// <summary>
        /// Verify Log.Error logs to a rolling log file.
        /// </summary>
        [Fact]
        public void LogDebugTest()
        {
            Message = "This is a DEBUG message.";

            Log = new Log(Output);

            Log.Debug(Message);
        }

        private ITestOutputHelper Output { get; }
    }
}
