using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a value of a facet item of type Facet=Field
    /// </summary>
    public interface IFacetItemRangeValue
    {
        /// <summary>
        /// Quantity of item
        /// </summary>
        long Quantity { get; set; }

        /// <summary>
        /// Subfacets of item
        /// </summary>
        IEnumerable<IFacetItem> Facets { get; set; }

        /// <summary>
        /// Set minimum value of range
        /// </summary>
        void SetMinimumValue(object value);

        /// <summary>
        /// Set maximum value of range
        /// </summary>
        void SetMaximumValue(object value);

        /// <summary>
        /// Get minimum value of range
        /// </summary>
        object GetMinimumValue();

        /// <summary>
        /// Get maximum value of range
        /// </summary>
        object GetMaximumValue();

        /// <summary>
        /// Calculate maximum value of range using informed gap
        /// </summary>
        void CalculateMaximumValueUsingGap(object gap);
    }
}
