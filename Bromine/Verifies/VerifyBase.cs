using System;
using System.Collections.Generic;

using Bromine.Logger;

using Xunit;
using Xunit.Sdk;

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
        protected VerifyBase(Log log)
        {
            Log = log;
        }

        /// <summary>
        /// Type of Verify.
        /// </summary>
        public abstract string Type { get; }

        /// <summary>
        /// Verifies that all items in the collection pass when executed against
        /// action.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action to test each item against.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void All(IEnumerable<object> collection, Action<object> action, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.All");
                Assert.All(collection, action);
            }
            catch (AllException e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that a collection contains exactly a given number of elements, which meet
        /// the criteria provided by the element inspectors.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action to test each item against.</param>
        public void Collection(IEnumerable<object> collection, params Action<object>[] action)
        {
            try
            {
                Log.Message($"Expected {Type}.Collection");
                Assert.Collection(collection, action);
            }
            catch (CollectionException e)
            {
                HandleException(e);
            }
        }

        /// <summary>
        /// Verifies that a string contains a given sub-string, using the current culture.
        /// </summary>
        /// <param name="expectedSubString">The sub-string which is expected not to be in the string.</param>
        /// <param name="actualString">The string to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void Contains(string expectedSubString, string actualString, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.Contains");
                Assert.Contains(expectedSubString, actualString);
            }
            catch (ContainsException e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that a string does not contain a given sub-string, using the current culture.
        /// </summary>
        /// <param name="expectedSubString">The sub-string which is expected not to be in the string.</param>
        /// <param name="actualString">The string to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void DoesNotContain(string expectedSubString, string actualString, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.DoesNotContain");
                Assert.DoesNotContain(expectedSubString, actualString);
            }
            catch (DoesNotContainException e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that a string does not match a regular expression.
        /// </summary>
        /// <param name="expectedRegexPattern">The regex pattern expected not to match.</param>
        /// <param name="actualString">The string to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void DoesNotMatch(string expectedRegexPattern, string actualString, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.DoesNotMatch");
                Assert.DoesNotMatch(expectedRegexPattern, actualString);
            }
            catch (DoesNotMatchException e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that a collection is empty.
        /// </summary>
        /// <param name="collection">The collection to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void Empty(IEnumerable<object> collection, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.Empty");
                Assert.Empty(collection);
            }
            catch (EmptyException e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that a string ends with a given string, using the current culture.
        /// </summary>
        /// <param name="expectedEndString">The string expected to be at the end of the string.</param>
        /// <param name="actualString">The string to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void EndsWith(string expectedEndString, string actualString, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.EndsWith");
                Assert.EndsWith(expectedEndString, actualString);
            }
            catch (EndsWithException e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that two objects are equal, using a default comparer.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The value to be compared against.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void Equal(object expected, object actual, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.Equal");
                Assert.Equal(expected, actual);
            }
            catch (EqualException e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that two double values are equal, using a default comparer.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The value to be compared against.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void Equal(double expected, double actual, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.Equal");
                Assert.Equal(expected, actual);
            }
            catch (EqualException e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that the condition is false.
        /// </summary>
        /// <param name="condition">The condition to be tested.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void False(bool condition, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.False");
                Assert.False(condition);
            }
            catch (FalseException e)
            {
                HandleException(e, message);
            }
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
            try
            {
                Log.Message($"Expected {Type}.InRange");
                Assert.InRange(actual, low, high);
            }
            catch (InRangeException e)
            {
                HandleException(e, message);
            }
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
            try
            {
                Log.Message($"Expected {Type}.InRange");
                Assert.InRange(actual, low, high);
            }
            catch (InRangeException e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that an object reference is not null.
        /// </summary>
        /// <param name="condition">The object to be validated.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void NotNull(object condition, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.NotNull");
                Assert.NotNull(condition);
            }
            catch (NotNullException e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that an object reference is null.
        /// </summary>
        /// <param name="condition">The object to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void Null(object condition, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.Null");
                Assert.Null(condition);
            }
            catch (NullException e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that two objects are not equal, using a default comparer.
        /// </summary>
        /// <param name="expected">The expected object.</param>
        /// <param name="actual">The actual object.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void NotEqual(object expected, object actual, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.NotEqual");
                Assert.NotEqual(expected, actual);
            }
            catch (NotEqualException e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that a collection is not empty.
        /// </summary>
        /// <param name="collection">The collection to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void NotEmpty(IEnumerable<object> collection, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.NotEmpty");
                Assert.NotEmpty(collection);
            }
            catch (NotEmptyException e)
            {
                HandleException(e, message);
            }
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
            try
            {
                Log.Message($"Expected {Type}.NotInRange");
                Assert.NotInRange(actual, low, high);
            }
            catch (NotInRangeException e)
            {
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that an expression is true.
        /// </summary>
        /// <param name="condition">The condition to be inspected.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void True(bool condition, string message = "")
        {
            try
            {
                Log.Message($"Expected {Type}.True");
                Assert.True(condition);
            }
            catch (TrueException e)
            {
                HandleException(e, message);
            }
        }

        internal abstract void HandleException(Exception exception, string message = "");

        internal virtual void LogErrorMessage(Exception exception, string message = "")
        {
            if (message != string.Empty)
            {
                Log.Error(message);
            }
            Log.Error(exception.Message);
        }

        internal Log Log { get; }
    }
}
