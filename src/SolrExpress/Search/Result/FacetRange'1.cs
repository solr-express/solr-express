using System;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a Facet Range with knowledge of the type of the minumum and maximum values
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class FacetRange<T> : FacetRange
        where T : struct, IComparable
    {
        /// <summary>
        /// Minimum value of the facet
        /// </summary>
        public T? MinimumValue
        {
            get
            {
                return (T?)this.InternalMinimumValue;
            }
            set
            {
                this.InternalMinimumValue = value;
            }
        }

        /// <summary>
        /// Maximum value of the facet
        /// </summary>
        public T? MaximumValue
        {
            get
            {
                return (T?)this.InternalMaximumValue;
            }
            set
            {
                this.InternalMaximumValue = value;
            }
        }
    }
}
