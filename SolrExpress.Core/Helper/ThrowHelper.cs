using System;

namespace SolrExpress.Core.Helper
{
    /// <summary>
    /// Helper class use to process validations and throws exceptions
    /// </summary>
    public static class ThrowHelper<TException>
        where TException : System.Exception
    {
        /// <summary>
        /// Throws exception if the condition is true
        /// </summary>
        /// <param name="condition">Condition to throws exception</param>
        /// <param name="message">Message in the excpetion (optional)</param>
        public static void If(bool condition, string message = null)
        {
            If(condition, new[] { message });
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
}
