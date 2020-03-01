using System;

using Bromine.Core;

namespace Bromine.Verifies
{
    /// <summary>
    /// Provides different methods to "soft" verify test expectations.
    /// When a verify statement fails:
    /// - Test execution will continue.
    /// - Test is reported as fail.
    /// </summary>
    public class SoftVerify : VerifyBase
    {
        /// <inheritdoc />
        public SoftVerify(IBrowser browser) : base(browser)
        {
        }

        /// <summary>
        /// True if a SoftVerify statement has failed.
        /// </summary>
        public bool HasFailure { get; private set; }

        internal override void HandleException(Exception exception, string message = "")
        {
            HasFailure = true;
            LogErrorMessage(exception, message);
        }
    }
}
