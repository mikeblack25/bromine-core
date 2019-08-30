using System.Drawing;
using System.IO;

using Bromine.Constants;
using Bromine.Core;
using Bromine.Models;

using Tests.Bromine.Common;

using Xunit;
using static Xunit.Assert;

namespace Tests.Bromine.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Tests to verify the Browser class is working as expected.
    /// </summary>
    public class LogTests : CoreTestsBase
    {
        /// <summary>
        /// Navigate to <see cref="TestSites.AmazonUrl"/>.
        /// Verify Browser.Source contains <see cref="CoreTestsBase.Amazon" />.
        /// Verify Browser.Title contains <see cref="CoreTestsBase.Amazon" />.
        /// </summary>
        [Fact]
        public void VerifySourceAndTitle()
        {
            Browser.Navigate.ToUrl(TestSites.AmazonUrl);


        }
    }
}
