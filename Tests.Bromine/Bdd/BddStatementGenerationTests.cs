using Bromine.BehaviorGenerator;
using Bromine.Constants;
using Tests.Bromine.Page;
using Tests.Bromine.Page.Google;

using Xunit;
using static Xunit.Assert;

namespace Tests.Bromine.Bdd
{
    /// <summary>
    /// Home tests to show how tests can be structured to leverage common setup and teardown to simplify the test flow.
    /// </summary>
    public class BddStatementGenerationTests : PageBase
    {
        /// <summary>
        /// Navigate to <see cref="Home.Url"/>
        /// </summary>
        public BddStatementGenerationTests()
        {
            InitializePages(BrowserType.Chrome);
        }

        /// <summary>
        /// Click the About link.
        /// </summary>
        [Fact]
        public void GetElements()
        {
            var generator = new ActionGenerator(Home, @"C:\repos\bromine-core\Tests.Bromine\Page");
        }
    }
}
