﻿using System;
using System.IO;
using System.Text;

namespace Bromine.Core
{
    /// <summary>
    /// Log messages.
    /// <see cref="LogLevels"/> for details on usage.
    /// DOM snapshots are saved to the DOM folder of the executing directory.
    /// Images are saved to the Images folder of the executing directory.
    /// Logs are written to the Logs folder of the executing directory.
    /// </summary>
    public class Log : IDisposable
    {
        /// <summary>
        /// Create an instance of Log for writing log messages.
        /// </summary>
        /// <param name="sessionName"></param>
        /// <param name="logLevel"></param>
        /// <param name="startLogging"></param>
        public Log(string sessionName, LogLevels logLevel = LogLevels.Message, bool startLogging = true)
        {
            InitializeLog(logLevel, sessionName, startLogging);
            Logs = new StringBuilder();

            Message($"'{SessionName}' started");
        }

        /// <summary>
        /// Formatted name of the test or file being executed.
        /// </summary>
        public string SessionName { get; private set; }

        /// <summary>
        /// Number of errors that have been logged.
        /// </summary>
        public int ErrorCount { get; private set; }

        /// <summary>
        /// Messages will be logged at the selected when true.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Directory where DOM snapshots are saved.
        /// </summary>
        public string DomDirectory { get; set; }

        /// <summary>
        /// Directory where images are saved.
        /// </summary>
        public string ImagesDirectory { get; set; }

        /// <summary>
        /// Directory where images are saved.
        /// </summary>
        public string LogsDirectory { get; set; }

        /// <summary>
        /// Is the log initialized?
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Determine what kind of messages will be logged.
        /// <see cref="LogLevels"/> for details on usage.
        /// </summary>
        public LogLevels LogLevel { get; private set; }

        /// <summary>
        /// Add an info message to the log.
        /// </summary>
        /// <param name="message">Info message to log.</param>
        public void Message(string message)
        {
            WriteMessage(message);
        }

        /// <summary>
        /// Add an error message to the log.
        /// </summary>
        /// <param name="message">Error message to log.</param>
        public void Error(string message)
        {
            if (LogLevel >= LogLevels.Message) { WriteMessage(message); }

            ErrorCount++;
        }

        /// <summary>
        /// Add a debug message to the log.
        /// </summary>
        /// <param name="message">Error message to log.</param>
        public void Debug(string message)
        {
            if (LogLevel >= LogLevels.Debug) { WriteMessage(message); }
        }

        /// <summary>
        /// Add a debug message to the log.
        /// </summary>
        /// <param name="message">Error message to log.</param>
        public void Framework(string message)
        {
            if (LogLevel >= LogLevels.Framework) { WriteMessage(message); }
        }

        /// <summary>
        /// Start logging messages.
        /// </summary>
        public void Start()
        {
            IsEnabled = true;
        }

        /// <summary>
        /// Stop logging messages.
        /// </summary>
        public void Stop()
        {
            IsEnabled = false;
        }

        /// <summary>
        /// Most recent DOM snapshot path.
        /// </summary>
        public string LastDomSnapshotFilePath { get; private set; }

        /// <summary>
        /// Most recent image path.
        /// </summary>
        public string LastImagePath { get; private set; }

        /// <summary>
        /// Path to the session log file.
        /// </summary>
        public string LogPath => $@"{LogsDirectory}/{FormattedTimeString(DateTime.Now)} {SessionName}{LogExtension}";


        /// <summary>
        /// Save a capture of the DOM.
        /// </summary>
        /// <param name="source">DOM source text to write.</param>
        /// <param name="message">Optional message to add to the file name.</param>
        public void SaveDom(string source, string message = "")
        {
            LastDomSnapshotFilePath = $@"{DomDirectory}/{FormattedTimeString(DateTime.Now)} {SessionName} {message}".Trim() + LogExtension;

            using (var writer = new StreamWriter(LastDomSnapshotFilePath))
            {
                writer.WriteLine(source);
            }
        }

        /// <summary>
        /// Returns a formatted path string.
        /// {ImagesDirectory}/{FormattedTimeString(DateTime.Now)}{TestName} {message}{ImageExtension}
        /// </summary>
        /// <param name="message">Optional message to add to the file name.</param>
        /// <returns></returns>
        public string ImagePath(string message = "")
        {
            return LastImagePath = $@"{ImagesDirectory}/{FormattedTimeString(DateTime.Now)} {SessionName} {message}".Trim() + ImageExtension;
        }

        /// <summary>
        /// Time formatted string to use in file names.
        /// </summary>
        /// <param name="time">Time to format.</param>
        /// <returns></returns>
        public static string FormattedTimeString(DateTime time) => time.ToString("yyyy-MM-dd H_mm_ss");

        /// <summary>
        /// Get the current log output.
        /// </summary>
        public string LogOutput => Logs.ToString();

        /// <summary>
        /// Dispose of loggers.
        /// </summary>
        public void Dispose()
        {
            if (IsInitialized)
            {
                using (var writer = new StreamWriter(LogPath))
                {
                    writer.Write(Logs.ToString());
                }

                IsInitialized = false;
            }
        }

        internal string FormattedLogMessage(string message) => $"{DateTime.Now:dd/MM/yyyy HH:mm:ss.ffff} {LogLevel.ToString()} {message}";

        private void WriteMessage(string message)
        {
            if (!IsEnabled)
            {
                return;
            }

            var formattedMessage = FormattedLogMessage(message);

            Logs.AppendLine(formattedMessage);
        }

        private void InitializeLog(LogLevels logLevel, string sessionName, bool startLogging)
        {
            IsInitialized = true;
            LogLevel = logLevel;
            SessionName = sessionName;

            InitializeDirectories();

            if (startLogging)
            {
                Start();
            }
        }

        private void InitializeDirectories()
        {
            DomDirectory = $@"{AppDomain.CurrentDomain.BaseDirectory}\{DomDirectoryText}";
            ImagesDirectory = $@"{AppDomain.CurrentDomain.BaseDirectory}\{ImagesDirectoryText}";
            LogsDirectory = $@"{AppDomain.CurrentDomain.BaseDirectory}\{LogsDirectoryText}";

            CreateDirectory(DomDirectory);
            CreateDirectory(ImagesDirectory);
            CreateDirectory(LogsDirectory);
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private StringBuilder Logs { get; }

        private string DomDirectoryText => "DOM";
        private string ImagesDirectoryText => "Images";
        private string LogsDirectoryText => "Logs";
        private string ImageExtension => ".png";
        private string LogExtension => ".txt";
    }


    /// <summary>
    /// Supported web browser types.
    /// </summary>
    public enum LogLevels
    {
        /// <summary>
        /// Log only Messages.
        /// </summary>
        Message = 0,

        /// <summary>
        /// Log Messages and Errors.
        /// </summary>
        Error,

        /// <summary>
        /// Log Messages, Errors, and Debug messages.
        /// </summary>
        Debug,

        /// <summary>
        /// Log Messages, Errors, Debug messages, and Framework messages.
        /// </summary>
        Framework
    }
}
