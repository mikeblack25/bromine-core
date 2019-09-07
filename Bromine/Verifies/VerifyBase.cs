using System;
using System.Collections.Generic;

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
        protected VerifyBase(List<Exception> exceptions)
        {
            Exceptions = exceptions;
        }

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
                Assert.All(collection, action);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                HandleException(e, message);
            }
        }

        /// <summary>
        /// Verifies that a collection contains exactly a given number of elements, which meet
        /// the criteria provided by the element inspectors.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action to test each item against.</param>
        /// <param name="message">Message to display if the expectation fails.</param>
        public void Collection(IEnumerable<object> collection, Action<object> action, string message = "")
        {
            try
            {
                Assert.Collection(collection, action);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                HandleException(e, message);
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
                Assert.Contains(expectedSubString, actualString);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.DoesNotContain(expectedSubString, actualString);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.DoesNotMatch(expectedRegexPattern, actualString);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.Empty(collection);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.EndsWith(expectedEndString, actualString);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.Equal(expected, actual);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.Equal(expected, actual);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.False(condition);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.InRange(actual, low, high);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.InRange(actual, low, high);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.NotNull(condition);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.Null(condition);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.NotEqual(expected, actual);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.NotEmpty(collection);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.NotInRange(actual, low, high);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
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
                Assert.True(condition);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                HandleException(e, message);
            }
        }

        internal virtual void HandleException(Exception exception, string message = "")
        {
            throw BuildException(exception, message);
        }

        internal Exception BuildException(Exception exception, string message = "")
        {
            if (string.IsNullOrEmpty(message)) { message = exception.Message; }

            var errorMessage = $"{message} {Environment.NewLine} {exception.Message}";
            var e = new Exception(errorMessage.Trim(), exception.InnerException);

            return e;
        }

        internal List<Exception> Exceptions { get; }
    }
}
