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
        /// Configure expressions used to find fields name
        /// </summary>
        /// <param name="fieldExpressions">Expressions used to find fields name</param>
        IFieldsParameter<TDocument> FieldExpressions(params Expression<Func<TDocument, object>>[] fieldExpressions);
    }
}
