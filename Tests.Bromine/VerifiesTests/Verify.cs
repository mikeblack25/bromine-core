using System;
using System.Collections.Generic;

using Bromine.Core;

using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tests.Bromine.VerifiesTests
{
    /// <summary>
    /// Tests the behavior of Verifies.Verify.
    /// </summary>
    public class Verify : Framework
    {
        /// <summary>
        /// Launch a browser in headless mode.
        /// </summary>
        public Verify(ITestOutputHelper output) : base(output, true, LogLevels.Framework)
        {
            HeadlessInit();
        }

        /// <summary>
        /// Does the expectedSubString exist in the actualString?
        /// </summary>
        [Fact]
        public void Contains()
        {
            Browser.Verify.Contains("x", "xyz");
        }

        /// <summary>
        /// Does the notExpectedSubString exist in the actualString?
        /// </summary>
        [Fact]
        public void DoesNotContain()
        {
            Browser.Verify.DoesNotContain("x", "abc");
        }

        /// <summary>
        /// Is a given enumerable object empty?
        /// </summary>
        [Fact]
        public void Empty()
        {
            Browser.Verify.Empty(new List<string>());
        }

        /// <summary>
        /// Does the actualString end with the expectedEndString?
        /// </summary>
        [Fact]
        public void EndsWith()
        {
            Browser.Verify.EndsWith("the message?", "what is the end of the message?");
        }

        /// <summary>
        /// Are two double values equal?
        /// </summary>
        [Fact]
        public void Equal()
        {
            Browser.Verify.Equal(1, 1);
        }

        /// <summary>
        /// Are two objects equal?
        /// </summary>
        [Fact]
        public void AreObjectsEqual()
        {
            Browser.Verify.Equal(string.Empty, string.Empty);
        }

        /// <summary>
        /// Are two objects not equal?
        /// </summary>
        [Fact]
        public void NotEqual()
        {
            Browser.Verify.NotEqual(1, 2);
        }

        /// <summary>
        /// Is the condition false?
        /// </summary>
        [Fact]
        public void False()
        {
            Browser.Verify.False(false);
        }

        /// <summary>
        /// Can Fail be called?
        /// </summary>
        [Fact]
        public void Fail()
        {
            Assert.Throws<FalseException>(() => Browser.Verify.Fail());
        }

        /// <summary>
        /// Are values in range?
        /// </summary>
        [Fact]
        public void InRange()
        {
            var value = 1.00;

            // ReSharper disable once RedundantAssignment
            Browser.Verify.InRange(value, value -= 0.5, value += 0.5);
        }

        /// <summary>
        /// Are DateTimes in range?
        /// </summary>
        [Fact]
        public void InRangeDateTime()
        {
            var time = DateTime.Now;

            Browser.Verify.InRange(time, time.Subtract(new TimeSpan(1000)), time.AddDays(1));
        }

        /// <summary>
        /// Are values not in range?
        /// </summary>
        [Fact]
        public void NotInRange()
        {
            var value = 1.00;

            // ReSharper disable once RedundantAssignment
            Browser.Verify.NotInRange(value, value += 0.5, value += 1.5);
        }

        /// <summary>
        /// Are DateTimes not in range?
        /// </summary>
        [Fact]
        public void NotInRangeDateTime()
        {
            var time = DateTime.Now;

            Browser.Verify.NotInRange(time, time.AddDays(1), time.AddDays(2));
        }

        /// <summary>
        /// Is an object not null?
        /// </summary>
        [Fact]
        public void NotNull()
        {
            Browser.Verify.NotNull(string.Empty);
        }

        /// <summary>
        /// Is an object null?
        /// </summary>
        [Fact]
        public void Null()
        {
            Browser.Verify.Null(null);
        }

        /// <summary>
        /// Is a collection not empty?
        /// </summary>
        [Fact]
        public void NotEmpty()
        {
            var items = new List<string> {"1 item added"};

            Browser.Verify.NotEmpty(items);
        }

        /// <summary>
        /// Is a true condition true?
        /// </summary>
        [Fact]
        public void True()
        {
            Browser.Verify.True(true, "Does a true condition return true?");
        }
    }
}
