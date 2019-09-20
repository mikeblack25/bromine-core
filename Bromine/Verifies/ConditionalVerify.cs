using System;

using Bromine.Logger;

using Xunit;

namespace Bromine.Verifies
{
    /// <summary>
    /// Provides different methods to conditional verify test expectations.
    /// When a verify statement fails:
    /// - Test execution will stop.
    /// - Test is reported as skip.
    /// NOTE: This is useful for preconditions of a test.
    /// </summary>
    public class ConditionalVerify : VerifyBase
    {
        /// <inheritdoc />
        public ConditionalVerify(Log log) : base(log) { }

        /// <summary>
        /// ConditionalVerify
        /// </summary>
        public override string Type => "ConditionalVerify";

        internal override void HandleException(Exception exception, string message = "")
        {
            Skip.If(true, message);

            LogErrorMessage(exception, message);

            OnVerifyFailed(exception, new VerifyFailedEvent(Type, message));

            throw exception;
        }
    }
}
