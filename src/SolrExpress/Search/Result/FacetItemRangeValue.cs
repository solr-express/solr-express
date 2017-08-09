using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Represents a value of a facet item of type Facet=Field
    /// </summary>
    public class FacetItemRangeValue<TKey> : IFacetItemRangeValue
        where TKey : struct, IComparable
    {
        private Func<TKey, TKey, TKey> _addGenericValues;

        /// <summary>
        /// Create a new instance of FacetItemRangeValue
        /// </summary>
        /// <param name="maximumValue">Minimum value of range</param>
        /// <param name="maximumValue">Maximum value of range</param>
        public FacetItemRangeValue(TKey? minimumValue = null, TKey? maximumValue = null)
        {
            this.MinimumValue = minimumValue;
            this.MaximumValue = maximumValue;

            if (!typeof(DateTime).Equals(typeof(TKey)))
            {
                // Declare the parameters
                var paramA = Expression.Parameter(typeof(TKey), "a");
                var paramB = Expression.Parameter(typeof(TKey), "b");

                // Add the parameters together
                var body = Expression.Add(paramA, paramB);

                // Compile it
                this._addGenericValues = Expression
                    .Lambda<Func<TKey, TKey, TKey>>(body, paramA, paramB)
                    .Compile();
            }
        }

        /// <summary>
        /// Minimum value of range
        /// </summary>
        public TKey? MinimumValue { get; set; }

        /// <summary>
        /// Maximum value of range
        /// </summary>
        public TKey? MaximumValue { get; set; }

        /// <summary>
        /// Quantity of item
        /// </summary>
        public long Quantity { get; set; }

        /// <summary>
        /// Subfacets of item
        /// </summary>
        public IEnumerable<IFacetItem> Facets { get; set; }

        void IFacetItemRangeValue.CalculateMaximumValueUsingGap(object gap)
        {
            if (this.MinimumValue.HasValue)
            {
                if (!typeof(DateTime).Equals(typeof(TKey)))
                {
                    this.MaximumValue = this._addGenericValues(this.MinimumValue.Value, ((TKey?)gap).Value);
                }
                else
                {
                    var minimumDateTime = DateTime.Parse(this.MinimumValue.Value.ToString());
                    var diff = ((DateTime)gap) - minimumDateTime;

                    ((IFacetItemRangeValue)this).SetMinimumValue(minimumDateTime.Add(diff));
                }
            }
            else if (((TKey?)gap).HasValue)
            {
                this.MaximumValue = ((TKey?)gap);
            }
        }

        void IFacetItemRangeValue.SetMaximumValue(object value)
        {
            this.MaximumValue = (TKey?)value;
        }

        void IFacetItemRangeValue.SetMinimumValue(object value)
        {
            this.MinimumValue = (TKey?)value;
        }
    }
}
