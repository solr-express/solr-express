using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to use in fields parameter
    /// </summary>
    public interface IFieldsParameter<TDocument> : ISearchParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Expression used to find property name
        /// </summary>
        Expression<Func<TDocument, object>>[] FieldExpressions { get; set;}
    }
}
