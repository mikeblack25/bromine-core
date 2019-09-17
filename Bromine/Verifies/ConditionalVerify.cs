using System;

using Bromine.Logger;

using Xunit;

namespace Bromine.Verifies
{
    /// <summary>
    /// 
    /// </summary>
    public class ConditionalVerify : VerifyBase
    {
        /// <inheritdoc />
        public ConditionalVerify(Log log) : base(log) { }

        internal override void HandleException(Exception exception, string message = "")
        {
            var ex = BuildException(exception, message);

            Skip.If(true, ex.Message);

            Log.Error(ex.Message);
        }
    }
}
