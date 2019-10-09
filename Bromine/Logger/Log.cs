using System;
using System.IO;
using System.Reflection;

using Xunit.Abstractions;

namespace Bromine.Logger
{
    /// <summary>
    /// Provides log support functionality using the log4net library. 
    /// </summary>
    public class Log : IDisposable
    {
        /// <summary>
        /// Provides log support functionality using the log4net library. 
        /// </summary>
        /// <param name="testName">Name of the executing test.</param>
        /// <param name="logExtension">Extension for logs.</param>
        public Log(string testName, string logExtension)
        {
            TestName = testName;
            Extension = logExtension;

            CreateDirectories();

            InitializeFileLogger();
        }

        /// <summary>
        /// Provides log support functionality using the log4net library with xUnit.net.
        /// </summary>
        /// <param name="output">Console log utility for xUnit.net.</param>
        /// <param name="logExtension">Extension to use for log files.</param>
        public Log(ITestOutputHelper output, string logExtension = MdExtension)
        {
            Output = output;
            Extension = logExtension;

            CreateDirectories();

            if (Output != null)
            {
                // ReSharper disable once PossibleNullReferenceException
                var test = (ITest) Output.GetType().GetField("test", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Output);
                TestName = test.DisplayName;
            }

            InitializeFileLogger();
        }

        /// <summary>
        /// .md
        /// </summary>
        public const string MdExtension = ".md";

        /// <summary>
        /// .txt
        /// </summary>
        public const string TxtExtension = ".txt";

        /// <summary>
        /// .log
        /// </summary>
        public const string LogExtension = ".log";

        /// <summary>
        /// Name of the test currently being executed.
        /// </summary>
        public string TestName { get; }

        /// <summary>
        /// Fully qualified path to the log file.
        /// </summary>
        public string LogName => $@"{LogsPath}\{TestName.Replace(":", string.Empty).Replace("\"", string.Empty)}{Extension}";

        /// <summary>
        /// Log extension for the current test.
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// Path to log files.
        /// NOTE: FileLoggers must be initialized on construction.
        /// </summary>
        public string LogsPath { get; set; }

        /// <summary>
        /// Path to images.
        /// NOTE: FileLoggers must be initialized on construction.
        /// </summary>
        public string ImagesPath { get; set; }

        /// <summary>
        /// Path to DOM captures.
        /// NOTE: FileLoggers must be initialized on construction.
        /// </summary>
        public string DomPath { get; set; }

        /// <summary>
        /// Number of errors that have been logged.
        /// </summary>
        public int ErrorCount { get; private set; }

        /// <summary>
        /// Add an info message to the log.
        /// </summary>
        /// <param name="message">Info message to log.</param>
        public void Message(string message)
        {
            var formattedMessage = FormattedLogMessage(message, "INFO");
            Writer?.WriteLineAsync(formattedMessage);

            Output?.WriteLine(formattedMessage);
        }

        /// <summary>
        /// Add an error message to the log.
        /// </summary>
        /// <param name="message">Error message to log.</param>
        public void Error(string message)
        {
            var formattedMessage = FormattedLogMessage(message, "ERROR");
            Writer?.WriteLineAsync(formattedMessage);

            Output?.WriteLine(formattedMessage);
        }

        /// <summary>
        /// Add a debug message to the log.
        /// </summary>
        /// <param name="message">Debug message to log.</param>
        public void Debug(string message)
        {
            var formattedMessage = FormattedLogMessage(message, "DEBUG");
            Writer?.WriteLineAsync(formattedMessage);

            Output?.WriteLine(formattedMessage);
        }

        /// <summary>
        /// Clear the log file.
        /// </summary>
        public void ClearFile()
        {
            if (Writer != null)
            {
                File.WriteAllText(LogName, string.Empty);
            }
        }

        /// <summary>
        /// Stop logging.
        /// </summary>
        public void Dispose()
        {
            Writer?.Dispose();
        }

        private void CreateDirectories()
        {
            CreateLogsDirectory();
            CreateImagesDirectory();
            CreateDomDirectory();
        }

        private void InitializeFileLogger()
        {
            if (string.IsNullOrWhiteSpace(Extension)) { return; }

            Writer = new StreamWriter(LogName, false);
        }

        private void CreateLogsDirectory()
        {
            LogsPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Logs";
            CreateDirectory(LogsPath);
        }

        private void CreateImagesDirectory()
        {
            ImagesPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Images";
            CreateDirectory(ImagesPath);
        }

        private void CreateDomDirectory()
        {
            DomPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Dom";
            CreateDirectory(DomPath);
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private ITestOutputHelper Output { get; }
        private StreamWriter Writer { get; set; }

        private static string FormattedLogMessage(string message, string type) => $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff")} {type} {message}";
    }
}
