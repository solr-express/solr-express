using SolrExpress.Core.ParameterValue;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Parameter
{
    /// <summary>
    /// Signatures to use in filter parameter
    /// </summary>
    public interface IFilterParameter<TDocument> : IParameter
        where TDocument : IDocument
    {
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        IFilterParameter<TDocument> Configure(Expression<Func<TDocument, object>> expression, IQueryParameterValue value, string tagName);
    }
}
