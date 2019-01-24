namespace SolrExpress.Utility
{
    /// <summary>
    /// Helper class used to verify equality between value
    /// </summary>
    internal static class ValueEqualityUtil
    {
        /// <summary>
        /// Verify equality between value
        /// </summary>
        /// <param name="value1">First value to check</param>
        /// <param name="value2">Second value to check</param>
        /// <returns>True if values is equal, false otherwise</returns>
        internal static bool IsEquals<T>(this T value1, T value2)
        {
            if (object.Equals(value1, default(T)) && object.Equals(value2, default(T)))
            {
                return true;
            }

            return value1?.Equals(value2) ?? false;
        }
    }
}
