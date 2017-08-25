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
        private readonly Func<TKey, TKey, TKey> _addGenericValues;

        /// <summary>
        /// Create a new instance of FacetItemRangeValue
        /// </summary>
        /// <param name="minimumValue">Minimum value of range</param>
        /// <param name="maximumValue">Maximum value of range</param>
        public FacetItemRangeValue(TKey? minimumValue = null, TKey? maximumValue = null)
        {
            this.MinimumValue = minimumValue;
            this.MaximumValue = maximumValue;

            if (typeof(DateTime) == typeof(TKey))
            {
                return;
            }

            // Declare parameters
            var paramA = Expression.Parameter(typeof(TKey), "a");
            var paramB = Expression.Parameter(typeof(TKey), "b");

            // Add the parameters together
            var body = Expression.Add(paramA, paramB);

            // Compile it
            this._addGenericValues = Expression
                .Lambda<Func<TKey, TKey, TKey>>(body, paramA, paramB)
                .Compile();
        }

        public void CalculateMaximumValueUsingGap(object gap)
        {
            if (this.MinimumValue.HasValue)
            {
                if (typeof(DateTime) == typeof(TKey))
                {
                    var minimumDateTime = DateTime.Parse(this.MinimumValue.Value.ToString());
                    var diff = (DateTime)gap - DateTime.MinValue;

                    this.SetMaximumValue(minimumDateTime.Add(diff));
                }
                else
                {
                    this.MaximumValue = this._addGenericValues(this.MinimumValue.Value, ((TKey?)gap).Value);
                }
            }
            else if (((TKey?)gap).HasValue)
            {
                this.MaximumValue = (TKey?)gap;
            }
        }

        public void SetMaximumValue(object value)
        {
            this.MaximumValue = (TKey?)value;
        }

        public void SetMinimumValue(object value)
        {
            this.MinimumValue = (TKey?)value;
        }

        public TKey? MinimumValue { get; set; }
        public TKey? MaximumValue { get; set; }
        public long Quantity { get; set; }
        public IEnumerable<IFacetItem> Facets { get; set; }
    }
}
