using System;
using System.Collections.Generic;

using Bromine.Constants;
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
            Appenders = new List<LogAppenders> { LogAppenders.Xunit };
        }

        /// <summary>
        /// Verify Log.Message logs to the console when using Xunit.
        /// </summary>
        [Fact]
        public void LogMessageTest()
        {
            Message = "This is an INFO message";
            // ReSharper disable once PossibleNullReferenceException
            TestName = $"{System.Reflection.MethodBase.GetCurrentMethod().Name}.txt";

            Log = new Log(Appenders, string.Empty, false, Output);

            Log.Message(Message);
        }

        /// <summary>
        /// Verify Log.Error logs to the console when using Xunit.
        /// </summary>
        [Fact]
        public void LogErrorTest()
        {
            Message = "This is an ERROR message.";
            // ReSharper disable once PossibleNullReferenceException
            TestName = $"{System.Reflection.MethodBase.GetCurrentMethod().Name}.txt";

            Log = new Log(Appenders, string.Empty, false, Output);

            Log.Error(Message);
        }

        /// <summary>
        /// Verify Log.Error logs to the console when using Xunit.
        /// </summary>
        [Fact]
        public void LogDebugTest()
        {
            Message = "This is a DEBUG message.";
            // ReSharper disable once PossibleNullReferenceException
            TestName = $"{System.Reflection.MethodBase.GetCurrentMethod().Name}.txt";

            Log = new Log(Appenders, string.Empty, false, Output);

            Log.Debug(Message);
        }

        /// <summary>
        /// Stop logging and ensure one log message exists in the XunitAppender.
        /// </summary>
        public void Dispose()
        {
            Log.Stop();

            Assert.Single(Log.XunitAppender.Logs);
        }

        private string Message { get; set; }
        private string TestName { get; set; }
        private Log Log { get; set; }
        private List<LogAppenders> Appenders { get; }
        private ITestOutputHelper Output { get; }
    }
}
