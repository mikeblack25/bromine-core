using System;
using System.Collections.Generic;

using Xunit;

namespace Bromine.Verifies
{
    /// <summary>
    /// 
    /// </summary>
    public class ConditionalVerify : VerifyBase
    {
        /// <inheritdoc />
        /// <param name="exceptions"></param>
        public ConditionalVerify(List<Exception> exceptions) : base(exceptions)
        {
        }

        internal override void HandleException(Exception exception, string message = "")
        {
            var ex = BuildException(exception, message);

            Skip.If(true, ex.Message);
        }
    }
}
