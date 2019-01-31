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

        protected BaseFacetsResult()
        {
        }

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

            var appliedFacetGap = facetGap;

            if (DateTypes.Contains(fieldType))
            {
                appliedFacetGap = GetDateTimeFacetGap(appliedFacetGap);

                var minimumValue = ((FacetItemRangeValue<DateTime>)item).MinimumValue;
                return DateMath.Apply(minimumValue ?? DateTime.MinValue, appliedFacetGap);
            }

            if (NotIntTypes.Contains(fieldType))
            {
                var minimumValueDecimal = ((FacetItemRangeValue<decimal>)item).MinimumValue;
                return (minimumValueDecimal ?? decimal.MinValue) + decimal.Parse(appliedFacetGap, CultureInfo.InvariantCulture);
            }

            var minimumValueINt = ((FacetItemRangeValue<int>)item).MinimumValue;
            return (minimumValueINt ?? int.MinValue) + int.Parse(appliedFacetGap, CultureInfo.InvariantCulture);
        }
    }
}
