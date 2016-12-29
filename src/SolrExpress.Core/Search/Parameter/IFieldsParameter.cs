using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Signatures to use in fields parameter
    /// </summary>
    public interface IFieldsParameter<TDocument> : ISearchParameter<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        IFieldsParameter<TDocument> Configure(params Expression<Func<TDocument, object>>[] expressions);

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        Expression<Func<TDocument, object>>[] Expressions { get; }
    }
}
