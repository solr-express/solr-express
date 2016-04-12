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
        /// Expression used to find the property name
        /// </summary>
        Expression<Func<TDocument, object>> Expression { get; set; }

        /// <summary>
        /// Value of the filter
        /// </summary>
        IQueryParameterValue Value { get; set; }
    }
}
