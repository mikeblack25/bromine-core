using System;

using Bromine.Logger;

namespace Bromine.Verifies
{
    /// <summary>
    /// Provides different methods to "soft" verify test expectations.
    /// When a verify statement fails:
    /// - Test execution will continue.
    /// - Test is reported as fail.
    /// </summary>
    public class SoftVerify : VerifyBase, IDisposable
    {
        /// <inheritdoc />
        public SoftVerify(Log log) : base(log)
        {
        }

        /// <summary>
        /// SoftVerify
        /// </summary>
        public override string Type => "SoftVerify";

        /// <summary>
        /// True if a SoftVerify statement has failed.
        /// </summary>
        public bool HasFailure { get; private set; }

        internal override void HandleException(Exception exception, string message = "")
        {
            HasFailure = true;
            LogErrorMessage(exception, message);

            OnVerifyFailed(exception, new VerifyFailedEvent(Type, message));
        }

        /// <summary>
        /// Check if there are any exceptions to log out for results.
        /// </summary>
        public void Dispose()
        {
            if (Log.ErrorCount == 0) { return; }

            Log.Error("One or more verify statements failed. See above logs.");
        }
    }
}
