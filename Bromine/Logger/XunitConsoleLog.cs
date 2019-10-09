using System;

using Xunit.Abstractions;

namespace Bromine.Logger
{
    /// <summary>
    /// Log session messages console when using xUnit.net.
    /// </summary>
    public class XunitConsoleLog : LogBase
    {
        /// <summary>
        /// Provides log support functionality using the log4net library with xUnit.net.
        /// </summary>
        /// <param name="output">Console log utility for xUnit.net.</param>
        public XunitConsoleLog(ITestOutputHelper output) : base(string.Empty)
        {
            Output = output ?? throw new Exception("A valid output object is required");

            Start();
        }

        /// <summary>
        /// This is logged to the console, there is no extension.
        /// </summary>
        public override string Extension => string.Empty;

        /// <inheritdoc />
        public override void Message(string message)
        {
            WriteMessage(message);
        }

        /// <inheritdoc />
        public override void Error(string message)
        {
            WriteMessage(message, ErrorString, true);
        }

        /// <inheritdoc />
        internal override void WriteMessage(string message, string type = "", bool isError = false)
        {
            try
            {
                if (IsLoggingEnabled)
                {
                    Output.WriteLine(FormattedLogMessage(message, type));

                    if (isError)
                    {
                        ErrorCount++;
                    }
                }
            }
            catch
            {
                Dispose();
            }
        }

        private ITestOutputHelper Output { get; }
    }
}
