using DaleNewman;
using System;
using System.Globalization;
using System.Linq;

namespace SolrExpress.Search.Result
{
    public abstract class BaseFacetsResult
    {
        protected static Type[] DateTypes = new[] { typeof(DateTime), typeof(DateTime?) };
        protected static Type[] NotIntTypes = new[] { typeof(decimal), typeof(float), typeof(double), typeof(decimal?), typeof(float?), typeof(double?) };

        /// <summary>
        /// Get prepared facet gap to use in DateMath library
        /// </summary>
        /// <param name="value">Original value</param>
        /// <returns>Value prepared to use in DateMath library</returns>
        protected static string GetDateTimeFacetGap(string value)
        {
            return value
                .Replace("YEARS", "y")
                .Replace("YEAR", "y")
                .Replace("MONTHS", "M")
                .Replace("MONTH", "M")
                .Replace("DAYS", "d")
                .Replace("DAY", "d")
                .Replace("WEEKS", "w")
                .Replace("WEEK", "w")
                .Replace("HOURS", "h")
                .Replace("HOUR", "h")
                .Replace("MINUTES", "m")
                .Replace("MINUTE", "m")
                .Replace("SECONDS", "s")
                .Replace("SECOND", "s");
        }

        protected static object GetMaximumValue(Type fieldType, IFacetItemRangeValue item, string facetGap)
        {
            if (string.IsNullOrWhiteSpace(facetGap))
            {
                return null;
            }

            if (DateTypes.Contains(fieldType))
            {
                facetGap = GetDateTimeFacetGap(facetGap);

                var minimumValue = ((FacetItemRangeValue<DateTime>)item).MinimumValue;
                return DateMath.Apply(minimumValue ?? DateTime.MinValue, facetGap);
            }

            if (NotIntTypes.Contains(fieldType))
            {
                var minimumValue = ((FacetItemRangeValue<decimal>)item).MinimumValue;
                return (minimumValue ?? decimal.MinValue) + decimal.Parse(facetGap, CultureInfo.InvariantCulture);
            }

            {
                var minimumValue = ((FacetItemRangeValue<int>)item).MinimumValue;
                return (minimumValue ?? int.MinValue) + int.Parse(facetGap, CultureInfo.InvariantCulture);
            }
        }
    }
}
