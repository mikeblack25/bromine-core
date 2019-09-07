using System;
using System.Collections.Generic;

using Xunit;

namespace Bromine.Verifies
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class VerifyBase
    {
        /// <inheritdoc />
        protected VerifyBase(List<Exception> exceptions)
        {
            Exceptions = exceptions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        /// <param name="message"></param>
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
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        /// <param name="message"></param>
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
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="collection"></param>
        /// <param name="message"></param>
        public void Contains(object expected, IEnumerable<object> collection, string message = "")
        {
            try
            {
                Assert.Contains(expected, collection);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                HandleException(e, message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="collection"></param>
        /// <param name="message"></param>
        public void Contains(string expected, string collection, string message = "")
        {
            try
            {
                Assert.Contains(expected, collection);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                HandleException(e, message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="collection"></param>
        /// <param name="message"></param>
        public void DoesNotContain(object expected, IEnumerable<object> collection, string message = "")
        {
            try
            {
                Assert.DoesNotContain(expected, collection);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                HandleException(e, message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedRegxPattern"></param>
        /// <param name="expectedString"></param>
        /// <param name="message"></param>
        public void DoesNotMatch(string expectedRegxPattern, string expectedString, string message = "")
        {
            try
            {
                Assert.DoesNotMatch(expectedRegxPattern, expectedString);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                HandleException(e, message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="message"></param>
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
        /// 
        /// </summary>
        /// <param name="expectedEndString"></param>
        /// <param name="expectedString"></param>
        /// <param name="message"></param>
        public void EndsWith(string expectedEndString, string expectedString, string message = "")
        {
            try
            {
                Assert.EndsWith(expectedEndString, expectedString);
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                HandleException(e, message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
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
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
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
        /// 
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <param name="message"></param>
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
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
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
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
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
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
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
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="message"></param>
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
        /// 
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <param name="message"></param>
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
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
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

            return exception;
        }

        internal List<Exception> Exceptions { get; }
    }
}
