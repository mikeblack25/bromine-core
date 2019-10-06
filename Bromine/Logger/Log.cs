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
        public Log(string testName, string logExtension) : this(null)
        {
            TestName = testName;
            Extension = logExtension;
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

            CreateLogsDirectory();
            CreateImagesDirectory();
            CreateDomDirectory();

            if (Output != null)
            {
                // ReSharper disable once PossibleNullReferenceException
                var test = (ITest) Output.GetType().GetField("test", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Output);
                TestName = test.DisplayName;
            }

            LogName = $@"{LogsPath}\{TestName}{Extension}";

            Repo = LogManager.GetRepository();

            Hierarchy = (Hierarchy)Repo;
            Root = Hierarchy.Root;
            Root.Level = Level.All;

            Layout = new PatternLayout
            {
                ConversionPattern = LogPattern
            };

            InitializeRollingFileAppender();

            if (output != null)
            {
                InitializeXunitAppender();

                Root.AddAppender(XunitAppender);
            }

            Start();
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
        public string LogName { get; }

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
        /// File appender to add logs to an existing file.
        /// </summary>
        public RollingFileAppender RollingFileAppender { get; private set; }

        /// <summary>
        /// Appender for logging messages to the console when using Xunit.
        /// </summary>
        public XunitAppender XunitAppender { get; private set; }

        /// <summary>
        /// Start / Resume logging.
        /// </summary>
        public void Start()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// Stop / Pause logging.
        /// </summary>
        public void Stop()
        {
            Repo.Shutdown();
        }

        /// <summary>
        /// Add an info message to the log.
        /// </summary>
        /// <param name="message">Info message to log.</param>
        public void Message(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Add an error message to the log.
        /// </summary>
        /// <param name="message">Error message to log.</param>
        public void Error(string message)
        {
            _logger.Error(message);
            ErrorCount++;
        }

        /// <summary>
        /// Add a debug message to the log.
        /// </summary>
        /// <param name="message">Debug message to log.</param>
        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        private void InitializeRollingFileAppender()
        {
            Layout.ActivateOptions();

            RollingFileAppender = new RollingFileAppender
            {
                AppendToFile = true,
                File = LogName,
                Layout = Layout,
                MaxSizeRollBackups = 5,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true
            };

            RollingFileAppender.ActivateOptions();

            Hierarchy.Root.AddAppender(RollingFileAppender);
            Hierarchy.Configured = true;
        }

        private void InitializeXunitAppender()
        {
            Layout.ActivateOptions();

            XunitAppender = new XunitAppender(Output, LogPattern);

            XunitAppender.ActivateOptions();

            Hierarchy.Configured = true;
        }

        /// <summary>
        /// Clear the rolling log file.
        /// </summary>
        public void ClearRollingFileAppender()
        {
            if (RollingFileAppender != null)
            {
                ReleaseRollingFileLock();
                File.WriteAllText(LogName, string.Empty);
            }
        }

        /// <summary>
        /// Release the file log on the rolling log file.
        /// </summary>
        public void ReleaseRollingFileLock()
        {
            if (RollingFileAppender != null)
            {
                RollingFileAppender.ImmediateFlush = true;
                RollingFileAppender.LockingModel = new FileAppender.MinimalLock();
                RollingFileAppender.ActivateOptions();
            }
        }

        /// <summary>
        /// Stop logging.
        /// </summary>
        public void Dispose()
        {
            Stop();
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

        private ILoggerRepository Repo { get; }
        private Hierarchy Hierarchy { get; }
        private log4net.Repository.Hierarchy.Logger Root { get; }
        private PatternLayout Layout { get; }
        private ITestOutputHelper Output { get; }

        private readonly ILog _logger = LogManager.GetLogger(typeof(Log));

        private string LogPattern => " %date %-5level %message%newline";
    }
}
