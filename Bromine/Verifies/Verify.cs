using System;
using Bromine.Logger;

namespace Bromine.Verifies
{
    /// <summary>
    /// Provides different methods to verify test expectations.
    /// When a verify statement fails:
    /// - Test execution will stop.
    /// - Test is reported as fail.
    /// </summary>
    public class Verify : VerifyBase
    {
        /// <inheritdoc />
        public Verify(LogManager logManager) : base(logManager)
        {
        }

        /// <summary>
        /// Verify
        /// </summary>
        public override string Type => "Verify";

        internal override void HandleException(Exception exception, string message = "")
        {
            LogErrorMessage(exception, message);

            OnVerifyFailed(exception, new VerifyFailedEvent(Type, message));

            throw exception;
        }
    }
}
