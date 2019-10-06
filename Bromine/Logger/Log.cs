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
        /// <inheritdoc />
        public Log(ITestOutputHelper output = null)
        {
            Output = output;

            var test = (ITest)Output.GetType().GetField("test", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Output);
            TestName = test.DisplayName;
            LogName = $@"{LogsPath}\{TestName}{LogExtension}";

            Repo = LogManager.GetRepository();    
            Root.Level = Level.All;

            Layout = new PatternLayout
            {
                ConversionPattern = LogPattern
            };

            CreateLogsDirectory();
            CreateImagesDirectory();
            CreateDomDirectory();

            InitializeRollingFileAppender();

            if (output != null)
            {
                InitializeXunitAppender();

                Root.AddAppender(XunitAppender);
            }

            Start();
        }

        /// <summary>
        /// Name of the test currently being executed.
        /// </summary>
        public string TestName { get; }

        /// <summary>
        /// Fully qualified path to the log file.
        /// </summary>
        public string LogName { get; }

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
            Repo.ResetConfiguration();
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

            XunitAppender = new XunitAppender(Output, LogPattern)
            {
                Layout = Layout
            };

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
                File.WriteAllText($@"{LogsPath}\{LogName}", string.Empty);
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
        private Hierarchy Hierarchy => (Hierarchy)Repo;
        private log4net.Repository.Hierarchy.Logger Root => Hierarchy.Root;
        private PatternLayout Layout { get; }
        private ITestOutputHelper Output { get; }

        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string LogPattern => " %date %-5level %message%newline";
        private const string LogExtension = ".md";
    }
}
