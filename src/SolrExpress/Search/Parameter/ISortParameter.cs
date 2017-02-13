using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in sort parameter
    /// </summary>
    public interface ISortParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Expression used to find field name
        /// </summary>
        Expression<Func<TDocument, object>>[] FieldExpressions { get; set; }

        /// <summary>
        /// True to ascendent order, otherwise false
        /// </summary>
        bool Ascendent { get; set; }
    }
}
