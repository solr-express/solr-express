using System;
using System.Collections;

namespace SolrExpress.Utility
{
    // <summary>
    // Helper class used to process validations and throws exceptions
    // </summary>
    internal static class Checker
    {
        /// <summary>
        /// Throws exception if condition is true
        /// </summary>
        /// <param name="condition">Condition to throws exception</param>
        /// <param name="args">Message in the excpetion</param>
        internal static void IsTrue<TException>(bool condition, params object[] args)
            where TException : Exception
        {
            if (condition)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), args);
            }
        }

        /// <summary>
        /// Throws exception if condition is false
        /// </summary>
        /// <param name="condition">Condition to throws exception</param>
        /// <param name="args">Message in the excpetion</param>
        internal static void IsFalse<TException>(bool condition, params object[] args)
            where TException : Exception
        {
            if (!condition)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), args);
            }
        }

        /// <summary>
        /// Throws ArgumentNullException if specified object is null
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="erroMessage">Message in exception</param>
        internal static void IsNull(object value, string erroMessage = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(erroMessage);
            }
        }

        /// <summary>
        /// Throws ArgumentNullException if specified object is null
        /// </summary>
        /// <param name="condition">Condition to throws exception</param>
        /// <param name="args">Message in the excpetion</param>
        internal static void IsNull<TException>(object value, params object[] args)
            where TException : Exception
        {
            if (value == null)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), args);
            }
        }

        /// <summary>
        /// Throws ArgumentNullException if specified string is null, empty, or consists only of white-space characters
        /// </summary>
        /// <param name="value">Value to check</param>
        internal static void IsNullOrWhiteSpace(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Throws ArgumentOutOfRangeException if specified value is out of range
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="minValue">Min value of valid range</param>
        /// <param name="maxValue">Max value of valid range</param>
        /// <param name="erroMessage">Message in exception</param>
        internal static void IsOutOfRange(decimal value, decimal minValue, decimal maxValue, string erroMessage)
        {
            if (value < minValue || value > maxValue)
            {
                throw new ArgumentOutOfRangeException(erroMessage);
            }
        }

        /// <summary>
        /// Throws ArgumentException if specified value is lower than other value
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="minValue">Min value of valid range</param>
        internal static void IsLowerThan(decimal value, decimal minValue)
        {
            if (value < minValue)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Throws ArgumentOutOfRangeException if specified collection is empty
        /// </summary>
        /// <param name="collection">Collection to check</param>
        internal static void IsEmpty(ICollection collection)
        {
            if (collection.Count == 0)
            {
                throw new ArgumentException();
            }
        }
    }
}
