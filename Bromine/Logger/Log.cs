using System;
using System.Reflection;

using Xunit.Abstractions;

namespace Bromine.Logger
{
    /// <summary>
    /// Manages session loggers and provides common access for common logging methods.
    /// </summary>
    public class Log : IDisposable
    {
        /// <inheritdoc />
        public Log(string fileName = "", params LogType[] loggers)
        {
            Initialize(fileName, null, loggers);
        }

        /// <inheritdoc />
        public Log(ITestOutputHelper output, string fileName = "", params LogType[] loggers)
        {
            Initialize(fileName, output, loggers);
        }

        /// <summary>
        /// <see cref="Logger.TextLog"/>
        /// </summary>
        public TextLog TextLog { get; private set; }

        /// <summary>
        /// <see cref="Logger.XunitConsoleLog"/>
        /// </summary>
        public XunitConsoleLog XunitConsoleLog { get; private set; }

        /// <summary>
        /// Add an info message to the log.
        /// </summary>
        /// <param name="message">Info message to log.</param>
        public void Message(string message)
        {
            TextLog?.Message(message);
            XunitConsoleLog?.Message(message);
        }

        /// <summary>
        /// Add an error message to the log.
        /// </summary>
        /// <param name="message">Error message to log.</param>
        public void Error(string message)
        {
            TextLog?.Error(message);
            XunitConsoleLog?.Error(message);
        }

        /// <summary>
        /// Dispose of loggers.
        /// </summary>
        public void Dispose()
        {
            TextLog?.Dispose();
            XunitConsoleLog?.Dispose();
        }

        private void Initialize(string fileName, ITestOutputHelper output, params LogType[] loggers)
        {
            if (string.IsNullOrWhiteSpace(fileName) && output != null)
            {
                // ReSharper disable once PossibleNullReferenceException
                fileName = (output.GetType().GetField("test", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(output) as ITest)?.DisplayName;
            }

            foreach (var logType in loggers)
            {
                switch (logType)
                {
                    case LogType.Text:
                    {
                        TextLog = new TextLog(fileName);
                        break;
                    }
                    case LogType.XunitConsole:
                    {
                        XunitConsoleLog = new XunitConsoleLog(output);
                        break;
                    }
                }
            }
        }
    }


    /// <summary>
    /// Supported Log types.
    /// </summary>
    public enum LogType
    {
#pragma warning disable 1591
        Text,
        XunitConsole
#pragma warning restore 1591
    }
}
