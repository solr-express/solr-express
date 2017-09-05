namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Facet range parameter
    /// </summary>
    public interface IFacetRangeParameter<TDocument> : IFacetParameter<TDocument>, ISearchItemFieldExpression<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Name of alias added in query
        /// </summary>
        string AliasName { get; set; }
        
        /// <summary>
        /// Size of each range bucket to make facet
        /// </summary>
        string Gap { get; set; }

        /// <summary>
        /// Lower bound to make facet
        /// </summary>
        string Start { get; set; }

        /// <summary>
        /// Upper bound to make facet
        /// </summary>
        string End { get; set; }

        /// <summary>
        /// Counts should also be computed for all records with field values lower then lower bound of the first range
        /// </summary>
        bool CountBefore { get; set; }

        /// <summary>
        /// Counts should also be computed for all records with field values greater then the upper bound of the last range
        /// </summary>
        bool CountAfter { get; set; }

        /// <summary>
        /// True to last bucket will end at “end” even if it is less than “gap” wide
        /// </summary>
        bool HardEnd { get; set; }
    }
}