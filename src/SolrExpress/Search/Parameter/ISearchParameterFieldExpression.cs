using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Signatures to indicate use of property FieldExpression
    /// </summary>
    public interface ISearchParameterFieldExpression<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Expression used to find field name
        /// </summary>
        Expression<Func<TDocument, object>> FieldExpression { get; set; }
    }
}
