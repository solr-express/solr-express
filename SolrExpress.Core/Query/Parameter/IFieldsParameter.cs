using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Query.Parameter
{
    /// <summary>
    /// Signatures to use in fields parameter
    /// </summary>
    public interface IFieldsParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        IFieldsParameter<TDocument> Configure(params Expression<Func<TDocument, object>>[] expressions);
    }
}
