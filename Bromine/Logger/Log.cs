using System;
using System.Collections.Generic;
using System.IO;

using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Bromine.Logger
{
    /// <summary>
    /// Provides log support functionality using the log4net library. 
    /// </summary>
    public class Log
    {
        /// <inheritdoc />
        public Log(string filePath = "", bool appendToFile = true) : this(new List < LogAppenders > { LogAppenders.RolllingFile }, filePath, appendToFile)
        {
        }

        /// <inheritdoc />
        public Log(List<LogAppenders> appenders, string filePath = "", bool appendToFile = true)
        {
            Appenders = appenders;
            FilePath = filePath.Contains(":") ? filePath : $@"{GetResultsPath()}\{filePath}";
            AppendToFile = appendToFile;

            CreateResultsDirectory();
            InitializeAppenders();

            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// Log appenders used by the current session.
        /// </summary>
        public List<LogAppenders> Appenders { get; }

        /// <summary>
        /// Path to log files.
        /// NOTE: FileLoggers must be initialized on construction.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Should logs be appended to the current file (if it exists).
        /// </summary>
        public bool AppendToFile { get; }

        /// <summary>
        /// File appender to add logs to an existing file.
        /// </summary>
        public RollingFileAppender RollingFileAppender { get; private set; }

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
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout
            {
                ConversionPattern = $"%date %-5level %message%newline"
            };
            patternLayout.ActivateOptions();

            RollingFileAppender = new RollingFileAppender
            {
                AppendToFile = AppendToFile,
                File = FilePath,
                Layout = patternLayout,
                MaxSizeRollBackups = 5,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true
            };

            RollingFileAppender.ActivateOptions();

            hierarchy.Root.AddAppender(RollingFileAppender);
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
        }

        private void InitializeAppenders()
        {
            foreach (var appender in Appenders)
            {
                switch (appender)
                {
                    case LogAppenders.RolllingFile:
                    {
                        InitializeRollingFileAppender();
                        break;
                    }
                }
            }
        }

        private void CreateResultsDirectory()
        {
            if (!Directory.Exists(GetResultsPath()))
            {
                Directory.CreateDirectory(GetResultsPath());
            }
        }

        private string GetResultsPath(string directory = Results) => $@"{AppDomain.CurrentDomain.BaseDirectory}\{directory}";

        private const string Results =  "Results";

        /// <summary>
        /// Supported approaches to log.
        /// </summary>
        public enum LogAppenders
        {
#pragma warning disable 1591
            RolllingFile,
#pragma warning restore 1591
        }

        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
