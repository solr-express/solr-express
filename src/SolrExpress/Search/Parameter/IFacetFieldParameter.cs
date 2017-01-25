using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use facet field parameter
    /// </summary>
    public interface IFacetFieldParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure expression used to find field name
        /// </summary>
        /// <param name="fieldExpression">Expression used to find field name</param>
        IFacetFieldParameter<TDocument> FieldExpression(Expression<Func<TDocument, object>> fieldExpression);

        /// <summary>
        /// Configure sort type of result of facet
        /// </summary>
        /// <param name="sortType">Sort type of result of facet</param>
        IFacetFieldParameter<TDocument> SortType(FacetSortType sortType);

        /// <summary>
        /// Configure minimum count of itens in facet's result
        /// </summary>
        /// <param name="minimum">Minimum count of itens in facet's result</param>
        IFacetFieldParameter<TDocument> Minimum(int minimum);

        /// <summary>
        /// Configure limit of itens in facet's result
        /// </summary>
        /// <param name="limit">Limit of itens in facet's result</param>
        IFacetFieldParameter<TDocument> Limit(int limit);

        /// <summary>
        /// Configure list of tags to exclude in facet calculation
        /// </summary>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetFieldParameter<TDocument> Excludes(params string[] excludes);
    }
}
