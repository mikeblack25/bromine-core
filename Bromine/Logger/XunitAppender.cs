﻿using System.Collections.Generic;

using log4net.Appender;
using log4net.Core;
using log4net.Layout;

using Xunit.Abstractions;

namespace Bromine.Logger
{
    /// <summary>
    /// Appender for logging to the console when using Xunit.
    /// </summary>
    public class XunitAppender : AppenderSkeleton
    {
        /// <inheritdoc />
        public XunitAppender(ITestOutputHelper outputHelper, string pattern)
        {
            _outputHelper = outputHelper;
            Name = "XunitAppender";
            // ReSharper disable once VirtualMemberCallInConstructor
            Layout = new PatternLayout(pattern);
            Logs = new List<string>();
        }

        /// <summary>
        /// Messages that have been logged to this appender.
        /// </summary>
        public List<string> Logs { get; }

        /// <summary>
        /// Add a log message to the console.
        /// </summary>
        /// <param name="loggingEvent"></param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            Logs.Add(RenderLoggingEvent(loggingEvent));
            _outputHelper.WriteLine(RenderLoggingEvent(loggingEvent));
        }

        private readonly ITestOutputHelper _outputHelper;
    }
}
