using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in facet range parameter
    /// </summary>
    public interface IFacetRangeParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure name of alias added in query
        /// </summary>
        /// <param name="aliasName"Name of alias added in query></param>
        IFacetRangeParameter<TDocument> AliasName(string aliasName);

        /// <summary>
        /// Configure expression used to find field name
        /// </summary>
        /// <param name="fieldExpression">Expression used to find field name</param>
        IFacetRangeParameter<TDocument> FieldExpression(Expression<Func<TDocument, object>> fieldExpression);

        /// <summary>
        /// Configure size of each range bucket to make facet
        /// </summary>
        /// <param name="gap">Size of each range bucket to make facet</param>
        IFacetRangeParameter<TDocument> Gap(string gap);

        /// <summary>
        /// Configure lower bound to make facet
        /// </summary>
        /// <param name="start">Lower bound to make facet</param>
        IFacetRangeParameter<TDocument> Start(string start);

        /// <summary>
        /// Configure upper bound to make facet
        /// </summary>
        /// <param name="end">Upper bound to make facet</param>
        IFacetRangeParameter<TDocument> End(string end);

        /// <summary>
        /// Configure sort type of result of facet
        /// </summary>
        /// <param name="sortType">Sort type of result of facet</param>
        IFacetRangeParameter<TDocument> SortType(FacetSortType sortType);

        /// <summary>
        /// Configure list of tags to exclude in facet calculation
        /// </summary>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetRangeParameter<TDocument> Excludes(params string[] excludes);

        /// <summary>
        /// Configure counts should also be computed for all records with field values lower then lower bound of the first range
        /// </summary>
        /// <param name="countBefore">Counts should also be computed for all records with field values lower then lower bound of the first range</param>
        IFacetRangeParameter<TDocument> CountBefore(bool countBefore);

        /// <summary>
        /// Configure counts should also be computed for all records with field values greater then the upper bound of the last range
        /// </summary>
        /// <param name="countAfter">Counts should also be computed for all records with field values greater then the upper bound of the last range</param>
        IFacetRangeParameter<TDocument> CountAfter(bool countAfter);
    }
}