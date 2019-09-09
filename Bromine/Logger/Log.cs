using System;
using System.IO;

using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

using Xunit.Abstractions;

namespace Bromine.Logger
{
    /// <summary>
    /// Provides log support functionality using the log4net library. 
    /// </summary>
    public class Log : IDisposable
    {
        /// <inheritdoc />
        public Log(ITestOutputHelper outputHelper) : this(string.Empty, outputHelper)
        {
        }

        /// <inheritdoc />
        public Log(string filePath = "", ITestOutputHelper outputHelper = null)
        {
            Hierarchy = (Hierarchy)LogManager.GetRepository();
            Hierarchy.Root.Level = Level.All;
            OutputHelper = outputHelper;
            Layout = new PatternLayout
            {
                ConversionPattern = LogPattern
            };

            FilePath = filePath.Contains(":") ? filePath : $@"{GetResultsPath()}\{filePath}";

            CreateResultsDirectory();

            if (!string.IsNullOrWhiteSpace(filePath)) { InitializeRollingFileAppender(); }
            if (outputHelper != null) { InitializeXunitAppender(); }

            Start();
        }

        /// <summary>
        /// Path to log files.
        /// NOTE: FileLoggers must be initialized on construction.
        /// </summary>
        public string FilePath { get; }

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
            LogManager.GetRepository().ResetConfiguration();
        }

        /// <summary>
        /// Add an info message to the log.
        /// </summary>
        /// <param name="message">Info message to log.</param>
        public void Message(string message)
        {
            Logger.Info(message);
        }

        /// <summary>
        /// Add an error message to the log.
        /// </summary>
        /// <param name="message">Error message to log.</param>
        public void Error(string message)
        {
            Logger.Error(message);
            ErrorCount++;
        }

        /// <summary>
        /// Add a debug message to the log.
        /// </summary>
        /// <param name="message">Debug message to log.</param>
        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        private void InitializeRollingFileAppender()
        {
            Layout.ActivateOptions();

            RollingFileAppender = new RollingFileAppender
            {
                AppendToFile = true,
                File = FilePath,
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

            XunitAppender = new XunitAppender(OutputHelper, LogPattern)
            {
                Layout = Layout
            };

            XunitAppender.ActivateOptions();

            Hierarchy.Root.AddAppender(XunitAppender);
            Hierarchy.Configured = true;
        }

        /// <summary>
        /// Clear the rolling log file.
        /// </summary>
        public void ClearRollingFileAppender()
        {
            ReleaseRollingFileLock();
            File.WriteAllText(FilePath, string.Empty);
        }

        /// <summary>
        /// Release the file log on the rolling log file.
        /// </summary>
        public void ReleaseRollingFileLock()
        {
            RollingFileAppender.ImmediateFlush = true;
            RollingFileAppender.LockingModel = new FileAppender.MinimalLock();
            RollingFileAppender.ActivateOptions();
        }

        /// <summary>
        /// Stop logging.
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        private void CreateResultsDirectory()
        {
            if (!Directory.Exists(GetResultsPath()))
            {
                Directory.CreateDirectory(GetResultsPath());
            }
        }

        private Hierarchy Hierarchy { get; }
        private PatternLayout Layout { get; }
        private ITestOutputHelper OutputHelper { get; }
        private string GetResultsPath(string directory = Results) => $@"{AppDomain.CurrentDomain.BaseDirectory}\{directory}";

        private const string Results =  "Results";
        private string LogPattern => "%date %-5level %message%newline";
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
