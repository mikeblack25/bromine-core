using System;
using System.IO;

namespace Bromine.Logger
{
    /// <summary>
    /// Log session messages to a .txt file.
    /// Logs are found in the Logs folder in the executing directory.
    /// </summary>
    public class TextLog : LogBase
    {
        /// <inheritdoc />
        public TextLog(string fileName) : base(fileName)
        {
            Start();
        }

        /// <summary>
        /// .txt
        /// </summary>
        public override string Extension => ".txt";

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

        /// <summary>
        /// Check to see if a FileName was given before creating the log file.
        /// </summary>
        public override void Start()
        {
            if (string.IsNullOrWhiteSpace(FileName))
            {
                throw new Exception("FileName is required before starting the log.");
            }

            base.Start();

            if (Writer == null)
            {
                InitializeFileLogger();
            }
        }

        /// <inheritdoc />
        internal override void WriteMessage(string message, string type = "", bool isError = false)
        {
            try
            {
                if (IsLoggingEnabled)
                {
                    Writer?.WriteLineAsync(FormattedLogMessage(message, type));

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

        /// <summary>
        /// Clear the log file.
        /// </summary>
        public void ClearFile()
        {
            if (Writer != null)
            {
                File.WriteAllText(LogPath, string.Empty);
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            Writer?.Dispose();
        }

        private void InitializeFileLogger()
        {
            if (string.IsNullOrWhiteSpace(Extension)) { return; }

            Writer = new StreamWriter(LogPath, false);
        }
    }
}
