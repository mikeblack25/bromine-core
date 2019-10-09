using System;

namespace Bromine.Verifies
{
    /// <summary>
    /// Event with information about a verify failure.
    /// </summary>
    public class VerifyFailedEvent : EventArgs
    {
        /// <inheritdoc />
        public VerifyFailedEvent(string verifyType, string message = "")
        {
            Type = verifyType;
            Message = message;
        }

        /// <summary>
        /// Type of verify statement.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Message about the failure.
        /// </summary>
        public string Message { get; }
    }
}
