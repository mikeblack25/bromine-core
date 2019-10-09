using System;
using System.IO;

namespace Bromine.Logger
{
    /// <summary>
    /// Provides base behavior for derived log types. 
    /// </summary>
    public abstract class LogBase : IDisposable
    {
        /// <summary>
        /// Provides base for derived log types.
        /// Logs are stored in the output path of the executing application in a folder called Logs.
        /// </summary>
        /// <param name="fileName">Name of the file to log.</param>
        protected LogBase(string fileName)
        {
            FileName = fileName;

            CreateDirectories();
        }

        /// <summary>
        /// Name of the test currently being executed.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Fully qualified path to the log file.
        /// </summary>
        public string LogPath => $@"{LogsPath}\{FileName.Replace(":", string.Empty).Replace("\"", string.Empty)}{Extension}";

        /// <summary>
        /// Log extension for the current test.
        /// </summary>
        public abstract string Extension { get; }

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
        public int ErrorCount { get; internal set; }

        /// <summary>
        /// Is logging currently enabled?
        /// </summary>
        public bool IsLoggingEnabled { get; set; }

        /// <summary>
        /// Add an info message to the log.
        /// </summary>
        /// <param name="message">Info message to log.</param>
        public abstract void Message(string message);

        /// <summary>
        /// Add an error message to the log.
        /// </summary>
        /// <param name="message">Error message to log.</param>
        public abstract void Error(string message);



        /// <summary>
        /// Start logging messages.
        /// </summary>
        public virtual void Start()
        {
            IsLoggingEnabled = true;
        }

        /// <summary>
        /// Stop logging messages.
        /// </summary>
        public void Stop()
        {
            IsLoggingEnabled = false;
        }

        /// <summary>
        /// Add an error message to the log.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="isError"></param>
        internal abstract void WriteMessage(string message, string type = "", bool isError = false);

        /// <summary>
        /// Dispose of logging resources.
        /// </summary>
        public virtual void Dispose()
        {
        }

        private void CreateDirectories()
        {
            CreateLogsDirectory();
            CreateImagesDirectory();
            CreateDomDirectory();
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

        internal StreamWriter Writer { get; set; }
        internal static string FormattedLogMessage(string message, string type = "") => $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.ffff")} {type} {message}";
        internal const string ErrorString = "ERROR";
    }
}
