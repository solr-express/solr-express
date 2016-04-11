using System;

namespace SolrExpress.Core
{
    /// <summary>
    /// Helper class use to process validations and throws exceptions
    /// </summary>
    public static class ThrowHelper<TException>
        where TException : Exception
    {
        /// <summary>
        /// Throws exception if the condition is true
        /// </summary>
        /// <param name="condition">Condition to throws exception</param>
        /// <param name="message">Message in the excpetion (optional)</param>
        public static void If(bool condition, string message = null)
        {
            If(condition, string.IsNullOrWhiteSpace(message) ? null : new[] { message });
        }

        /// <summary>
        /// Throws exception if the condition is true
        /// </summary>
        /// <param name="condition">Condition to throws exception</param>
        /// <param name="args">Message in the excpetion</param>
        public static void If(bool condition, string[] args)
        {
            if (condition)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), args);
            }
        }
    }


    /// <summary>
    /// Helper class use to process validations and throws exceptions
    /// </summary>
    public static class ThrowExtension
    {
        /// <summary>
        /// Throws ArgumentNullException if specified string is null, empty, or consists only of white-space characters
        /// </summary>
        /// <param name="value">Value to check</param>
        public static void ThrowIfIsNullOrWhiteSpace(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException();
            }
        }
    }
}
