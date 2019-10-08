using System;
using System.IO;
using System.Reflection;

using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository;
using log4net.Repository.Hierarchy;

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

            InitializeLog4Net();
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

            InitializeLog4Net();
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
        /// File appender to write a log file.
        /// </summary>
        public FileAppender FileAppender { get; private set; }

        /// <summary>
        /// Stop / Pause logging.
        /// </summary>
        public void Stop()
        {
            Repo?.ResetConfiguration();
        }

        /// <summary>
        /// Add an info message to the log.
        /// </summary>
        /// <param name="message">Info message to log.</param>
        public void Message(string message)
        {
            lock (Lock)
            {
                _logger?.Info(message);
            }

            Output?.WriteLine(FormattedLogMessage(message, "INFO"));
        }

        /// <summary>
        /// Add an error message to the log.
        /// </summary>
        /// <param name="message">Error message to log.</param>
        public void Error(string message)
        {
            lock (Lock)
            {
                _logger?.Error(message);
            }

            Output?.WriteLine(FormattedLogMessage(message, "ERROR"));
            ErrorCount++;
        }

        /// <summary>
        /// Add a debug message to the log.
        /// </summary>
        /// <param name="message">Debug message to log.</param>
        public void Debug(string message)
        {
            lock (Lock)
            {
                _logger?.Debug(message);
            }

            Output?.WriteLine(FormattedLogMessage(message, "DEBUG"));
        }

        private void InitializeFileAppender()
        {
            Layout.ActivateOptions();
            FileAppender = new FileAppender
            {
                Name = "FileAppender",
                AppendToFile = false,
                File = LogName,
                ImmediateFlush = true,
                Layout = Layout,
                LockingModel = new FileAppender.MinimalLock(),
                Threshold = Level.All
            };

            Hierarchy.Root.AddAppender(FileAppender);
            Hierarchy.Configured = true;
        }

        /// <summary>
        /// Clear the log file.
        /// </summary>
        public void ClearFile()
        {
            if (FileAppender != null)
            {
                ReleaseFileLock();
                File.WriteAllText(LogName, string.Empty);
            }
        }

        /// <summary>
        /// Release the file log on the rolling log file.
        /// </summary>
        public void ReleaseFileLock()
        {
            if (FileAppender == null) { return; }

            FileAppender.LockingModel = new FileAppender.MinimalLock();
            FileAppender.ActivateOptions();
        }

        /// <summary>
        /// Stop logging.
        /// </summary>
        public void Dispose()
        {
            FileAppender?.Close();
        }

        private void CreateDirectories()
        {
            CreateLogsDirectory();
            CreateImagesDirectory();
            CreateDomDirectory();
        }

        private void InitializeLog4Net()
        {
            if (string.IsNullOrWhiteSpace(Extension)) { return; }

            Repo = LogManager.GetRepository();
            Hierarchy = (Hierarchy) Repo;
            Root = Hierarchy.Root;
            Root.Level = Level.All;

            Layout = new PatternLayout
            {
                ConversionPattern = LogPattern
            };

            InitializeFileAppender();
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

        private ILoggerRepository Repo { get; set; }
        private Hierarchy Hierarchy { get; set; }
        private log4net.Repository.Hierarchy.Logger Root { get; set; }
        private PatternLayout Layout { get; set; }
        private ITestOutputHelper Output { get; }

        private readonly ILog _logger = LogManager.GetLogger(typeof(Log));

        private static string LogPattern => " %date [%thread] %-5level %message%newline";
        private static string FormattedLogMessage(string message, string type) => $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff")} {type} {message}";

        private static readonly object Lock = new object();
    }
}
