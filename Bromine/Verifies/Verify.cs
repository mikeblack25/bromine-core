using System;

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
        public Verify(IBrowser browser) : base(browser)
        {
        }

        internal override void HandleException(Exception exception, string message = "")
        {
            LogErrorMessage(exception, message);

            throw exception;
        }
    }
}
