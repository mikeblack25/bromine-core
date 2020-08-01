using System;

namespace Bromine.Verifies
{
    /// <summary>
    /// NOTE: This does not currently do anything!!!
    /// Provides different methods to conditional verify test expectations.
    /// When a verify statement fails:
    /// - Test execution will stop.
    /// - Test is reported as skip.
    /// NOTE: This is useful for preconditions of a test.
    /// TODO: Implement an event that can be subscribed to. This will allow the caller to handle this in their context for all test runners.
    /// </summary>
    public class ConditionalVerify : VerifyBase
    {
        /// <inheritdoc />
        public ConditionalVerify(IBrowser browser) : base(browser) { }

        internal override void HandleException(Exception exception, string message = "")
        {
            LogErrorMessage(exception, message);

            throw exception;
        }
    }
}
