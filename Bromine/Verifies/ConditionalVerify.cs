using System;

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
        public ConditionalVerify(IBrowser browser) : base(browser) { }

        internal override void HandleException(Exception exception, string message = "")
        {
            Skip.If(true, message);

            LogErrorMessage(exception, message);

            throw exception;
        }
    }
}
