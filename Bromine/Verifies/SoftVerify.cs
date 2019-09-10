using System;

using Bromine.Logger;

namespace Bromine.Verifies
{
    /// <summary>
    /// 
    /// </summary>
    public class SoftVerify : VerifyBase, IDisposable
    {
        /// <inheritdoc />
        public SoftVerify(Log log) : base(log)
        {
        }

        /// <summary>
        /// True if a SoftVerify statement has failed.
        /// </summary>
        public bool HasFailure { get; private set; }

        internal override void HandleException(Exception exception, string message = "")
        {
            HasFailure = true;
            Log.Error(BuildException(exception, message).Message);
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
