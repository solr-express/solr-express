using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter
{
    public interface ISearchParameterFieldExpressions<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Expressions used to find fields name
        /// </summary>
        Expression<Func<TDocument, object>>[] FieldExpressions { get; set; }
    }
}
