using System;
using System.Collections.Generic;

namespace Bromine.Verifies
{
    /// <summary>
    /// 
    /// </summary>
    public class SoftVerify : VerifyBase, IDisposable
    {
        /// <inheritdoc />
        /// <param name="exceptions"></param>
        public SoftVerify(List<Exception> exceptions) : base(exceptions)
        {
        }

        /// <summary>
        /// True if a SoftVerify statement has failed.
        /// </summary>
        public bool HasFailure { get; private set; }

        internal override void HandleException(Exception exception, string message = "")
        {
            HasFailure = true;
            Exceptions.Add(BuildException(exception, message));
        }

        /// <summary>
        /// Check if there are any exceptions to log out for results.
        /// </summary>
        public void Dispose()
        {
            var message = string.Empty;

            if (Exceptions.Count <= 0) { return; }


            foreach (var exception in Exceptions)
            {
                message += $"{exception.Message} {Environment.NewLine}";
            }

            Exceptions.Add(new Exception(message));
        }
    }
}
