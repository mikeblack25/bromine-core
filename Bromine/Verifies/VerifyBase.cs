using System;
using System.Collections.Generic;

using Bromine.Core;

using Xunit;

namespace Bromine.Verifies
{
    /// <summary>
    /// Provides access to Xunit Assertions.
    /// <see cref="Assert"/>
    /// Note: This does not include all assertions provided by Xunit at this time.
    /// </summary>
    public abstract class VerifyBase
    {
        /// <inheritdoc />
        protected VerifyBase(IBrowser browser)
        {
            Browser = browser;
        }

        internal IBrowser Browser { get; }

        internal Log Log => Browser.Log;

        /// <summary>
        /// Verifies that a string contains a given sub-string, using the current culture.
        /// </summary>
        /// <param name="expectedSubString">The sub-string which is expected not to be in the string.</param>
        /// <param name="actualString">The string to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void Contains(string expectedSubString, string actualString, string message = "")
        {
            InvokeAssert(() => Assert.Contains(expectedSubString, actualString), message);
        }

        /// <summary>
        /// Verifies that a string does not contain a given sub-string, using the current culture.
        /// </summary>
        /// <param name="notExpectedSubString">The sub-string which is expected not to be in the string.</param>
        /// <param name="actualString">The string to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void DoesNotContain(string notExpectedSubString, string actualString, string message = "")
        {
            InvokeAssert(() => Assert.DoesNotContain(notExpectedSubString, actualString), message);
        }

        /// <summary>
        /// Verifies that a collection is empty.
        /// </summary>
        /// <param name="collection">The collection to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void Empty(IEnumerable<object> collection, string message = "")
        {
            InvokeAssert(() => Assert.Empty(collection), message);
        }

        /// <summary>
        /// Verifies that a string ends with a given string, using the current culture.
        /// </summary>
        /// <param name="expectedEndString">The string expected to be at the end of the string.</param>
        /// <param name="actualString">The string to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void EndsWith(string expectedEndString, string actualString, string message = "")
        {
            InvokeAssert(() => Assert.EndsWith(expectedEndString, actualString), message);
        }

        /// <summary>
        /// Verifies that two objects are equal, using a default comparer.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The value to be compared against.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void Equal(object expected, object actual, string message = "")
        {
            InvokeAssert(() => Assert.Equal(expected, actual), message);
        }

        /// <summary>
        /// Verifies that two double values are equal, using a default comparer.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The value to be compared against.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void Equal(double expected, double actual, string message = "")
        {
            InvokeAssert(() => Assert.Equal(expected, actual), message);
        }

        /// <summary>
        /// Verifies that an expression is false.
        /// </summary>
        /// <param name="condition">The condition to be inspected.</param>
        /// <param name="message">Intent of the verify statement.</param>
        /// <param name="errorMessage">Message to display if the expectation fails.</param>
        public void False(bool condition, string message = "", string errorMessage = "")
        {
            InvokeAssert(() => Assert.False(condition, errorMessage), message);
        }

        /// <summary>
        /// Fail with the given message.
        /// </summary>
        /// <param name="message"></param>
        public void Fail(string message = "")
        {
            InvokeAssert(() => Assert.False(true), message);
        }

        /// <summary>
        /// Verifies that a value is within a given range.
        /// </summary>
        /// <param name="actual">The actual value to be evaluated.</param>
        /// <param name="low">The (inclusive) low value of the range.</param>
        /// <param name="high">The (inclusive) high value of the range.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void InRange(double actual, double low, double high, string message = "")
        {
            InvokeAssert(() => Assert.InRange(actual, low, high), message);
        }

        /// <summary>
        /// Verifies that a value is within a given range.
        /// </summary>
        /// <param name="actual">The actual value to be evaluated.</param>
        /// <param name="low">The (inclusive) low value of the range.</param>
        /// <param name="high">The (inclusive) high value of the range.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void InRange(DateTime actual, DateTime low, DateTime high, string message = "")
        {
            InvokeAssert(() => Assert.InRange(actual, low, high), message);
        }

        /// <summary>
        /// Verifies that a value is not within a given range, using the default comparer.
        /// </summary>
        /// <param name="actual">The actual value to be evaluated.</param>
        /// <param name="low">The (inclusive) low value of the range.</param>
        /// <param name="high">The (inclusive) high value of the range.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void NotInRange(double actual, double low, double high, string message = "")
        {
            InvokeAssert(() => Assert.NotInRange(actual, low, high), message);
        }

        /// <summary>
        /// Verifies that a value is not within a given range.
        /// </summary>
        /// <param name="actual">The actual value to be evaluated.</param>
        /// <param name="low">The (inclusive) low value of the range.</param>
        /// <param name="high">The (inclusive) high value of the range.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void NotInRange(DateTime actual, DateTime low, DateTime high, string message = "")
        {
            InvokeAssert(() => Assert.NotInRange(actual, low, high), message);
        }

        /// <summary>
        /// Verifies that an object reference is not null.
        /// </summary>
        /// <param name="condition">The object to be validated.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void NotNull(object condition, string message = "")
        {
            InvokeAssert(() => Assert.NotNull(condition), message);
        }

        /// <summary>
        /// Verifies that an object reference is null.
        /// </summary>
        /// <param name="condition">The object to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void Null(object condition, string message = "")
        {
            InvokeAssert(() => Assert.Null(condition), message);
        }

        /// <summary>
        /// Verifies that two objects are not equal, using a default comparer.
        /// </summary>
        /// <param name="expected">The expected object.</param>
        /// <param name="actual">The actual object.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void NotEqual(object expected, object actual, string message = "")
        {
            InvokeAssert(() => Assert.NotEqual(expected, actual), message);
        }

        /// <summary>
        /// Verifies that a collection is not empty.
        /// </summary>
        /// <param name="collection">The collection to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void NotEmpty(IEnumerable<object> collection, string message = "")
        {
            InvokeAssert(() => Assert.NotEmpty(collection), message);
        }

        /// <summary>
        /// Verifies that an expression is true.
        /// </summary>
        /// <param name="condition">The condition to be inspected.</param>
        /// <param name="message">Intent of the verify statement.</param>
        /// <param name="errorMessage">Message to display if the expectation fails.</param>
        public void True(bool condition, string message = "", string errorMessage = "")
        {
            InvokeAssert(() => Assert.True(condition, errorMessage), message);
        }

        internal abstract void HandleException(Exception exception, string message = "");

        /// <summary>
        /// Write a provided error message and the exception.Message.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        internal virtual void LogErrorMessage(Exception exception, string message = "")
        {
            if (message != string.Empty)
            {
                Log.Error(message);
            }
            Log.Error(exception.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="message"></param>
        internal void InvokeAssert(VerifyAction action, string message)
        {
            try
            {
                Log.Message(message);
                action.Invoke();
            }
            catch (Exception e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal delegate void VerifyAction();
    }
}
