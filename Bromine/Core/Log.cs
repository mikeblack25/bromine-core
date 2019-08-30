using System;

using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;

namespace Bromine.Core
{
    /// <summary>
    /// Provides log support functionality using the log4net library. 
    /// </summary>
    public class Log
    {
        /// <inheritdoc />
        public Log(string logLevel)
        {
            LogLevel = GetLevel(logLevel);
        }

        /// <summary>
        /// Add a message to the log.
        /// </summary>
        /// <param name="message"></param>
        public void Message(string message)
        {
            Logger.Info($"{GetFormattedDateTime} {message}");

            var stuff = Logger.Logger.Repository;
        }

        /// <summary>
        /// Log level used for logging.
        /// </summary>
        public Level LogLevel { get; }

        /// <summary>
        /// Get the current time in the following format MM/dd/yyyy hh:mm:ss:ffff.
        /// </summary>
        public string GetFormattedDateTime => DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss:ffff");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public static Level GetLevel(string logLevel)
        {
            switch (logLevel)
            {
                case "Alert":
                {
                    return Level.Alert;
                }
                case "All":
                {
                    return Level.All;
                }
                case "Critical":
                {
                    return Level.Critical;
                }
                case "Debug":
                {
                    return Level.Debug;
                }
            }

            return Level.All;
        }

        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
