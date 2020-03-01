using System;
using System.Collections.Generic;

using Bromine.Core;

using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tests.Bromine.VerifiesTests
{
    /// <summary>
    /// Tests the behavior of Verifies.Verify failure conditions.
    /// Note: Copy Page: Summaries may not have not been updated.
    /// </summary>
    public class VerifyFailures : Framework
    {
        /// <summary>
        /// Launch a browser in headless mode.
        /// </summary>
        public VerifyFailures(ITestOutputHelper output) : base(output, true, LogLevels.Framework)
        {
            HeadlessInit();
        }

        /// <summary>
        /// Does the expectedSubString exist in the actualString?
        /// </summary>
        [Fact]
        public void Contains()
        {
            Assert.Throws<ContainsException>(() => Browser.Verify.Contains("x", "abc"));
        }

        /// <summary>
        /// Does the notExpectedSubString exist in the actualString?
        /// </summary>
        [Fact]
        public void DoesNotContain()
        {
            Assert.Throws<DoesNotContainException>(() => Browser.Verify.DoesNotContain("x", "xyz"));
        }

        /// <summary>
        /// Is a given enumerable object empty?
        /// </summary>
        [Fact]
        public void Empty()
        {
            var items = new List<string> { "1 item added" };

            Assert.Throws<EmptyException>(() => Browser.Verify.Empty(items));
        }

        /// <summary>
        /// Does the actualString end with the expectedEndString?
        /// </summary>
        [Fact]
        public void EndsWith()
        {
            Assert.Throws<EndsWithException>(() => Browser.Verify.EndsWith("I don't understand", "what is the end of the message?"));
        }

        /// <summary>
        /// Are two double values equal?
        /// </summary>
        [Fact]
        public void Equal()
        {
            Assert.Throws<EqualException>(() => Browser.Verify.Equal(2, 1));
        }

        /// <summary>
        /// Are two objects equal?
        /// </summary>
        [Fact]
        public void AreObjectsEqual()
        {
            Assert.Throws<EqualException>(() => Browser.Verify.Equal(1, string.Empty));
        }

        /// <summary>
        /// Are two objects not equal?
        /// </summary>
        [Fact]
        public void NotEqual()
        {
            Assert.Throws<NotEqualException>(() => Browser.Verify.NotEqual(1, 1));
        }

        /// <summary>
        /// Is the condition false?
        /// </summary>
        [Fact]
        public void False()
        {
            Assert.Throws<FalseException>(() => Browser.Verify.False(true));
        }

        /// <summary>
        /// Are values in range?
        /// </summary>
        [Fact]
        public void InRange()
        {
            var value = 1.00;

            // ReSharper disable once RedundantAssignment
            Assert.Throws<InRangeException>(() => Browser.Verify.InRange(value, value += 0.5, value += 1.5));
        }

        /// <summary>
        /// Are DateTimes in range?
        /// </summary>
        [Fact]
        public void InRangeDateTime()
        {
            var time = DateTime.Now;

            Assert.Throws<InRangeException>(() => Browser.Verify.InRange(time, time.AddDays(1), time.AddDays(2)));
        }

        /// <summary>
        /// Are values not in range?
        /// </summary>
        [Fact]
        public void NotInRange()
        {
            var value = 1.00;

            // ReSharper disable once RedundantAssignment
            Assert.Throws<NotInRangeException>(() => Browser.Verify.NotInRange(value, value, value));
        }

        /// <summary>
        /// Are DateTimes not in range?
        /// </summary>
        [Fact]
        public void NotInRangeDateTime()
        {
            var time = DateTime.Now;

            Assert.Throws<NotInRangeException>(() => Browser.Verify.NotInRange(time, time, time));
        }

        /// <summary>
        /// Is an object not null?
        /// </summary>
        [Fact]
        public void NotNull()
        {
            Assert.Throws<NotNullException>(() => Browser.Verify.NotNull(null));
        }

        /// <summary>
        /// Is an object null?
        /// </summary>
        [Fact]
        public void Null()
        {
            Assert.Throws<NullException>(() => Browser.Verify.Null(string.Empty));
        }

        /// <summary>
        /// Is a collection not empty?
        /// </summary>
        [Fact]
        public void NotEmpty()
        {
            Assert.Throws<NotEmptyException>(() => Browser.Verify.NotEmpty(new List<string>()));
        }

        /// <summary>
        /// Is a true condition true?
        /// </summary>
        [Fact]
        public void True()
        {
            Assert.Throws<TrueException>(() => Browser.Verify.True(false));
        }
    }
}
