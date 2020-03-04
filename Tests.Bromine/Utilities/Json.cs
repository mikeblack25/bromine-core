using System;

using Bromine.Core;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Utilities
{
    /// <summary>
    /// Tests the behavior of Verifies.Verify.
    /// </summary>
    public class Json : Framework
    {
        /// <summary>
        /// Launch a browser in headless mode.
        /// </summary>
        public Json(ITestOutputHelper output) : base(output, true, LogLevels.Framework)
        {
            TestObject = new global::Bromine.Utilities.Json();
            HeadlessInit(logElementHistory: true);
        }

        /// <summary>
        /// Does the expectedSubString exist in the actualString?
        /// </summary>
        [Fact]
        public void Save()
        {
            var element = CommonPage.EnableButtonId;
            element = CommonPage.EnableButtonCss;
            
            CommonPage.NextButton.Click();

            TestObject.SaveObject($@"{AppDomain.CurrentDomain.BaseDirectory}\test.json", Browser.Session);
        }

        /// <summary>
        /// Does the expectedSubString exist in the actualString?
        /// </summary>
        [Fact]
        public void Read()
        {
            var obj = TestObject.ReadObject($@"{AppDomain.CurrentDomain.BaseDirectory}\test.json", new Session());
        }

        private global::Bromine.Utilities.Json TestObject { get; }
    }
}
