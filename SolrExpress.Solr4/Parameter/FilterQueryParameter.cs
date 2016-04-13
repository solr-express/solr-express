using SolrExpress.Core;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.ParameterValue;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FilterQueryParameter<TDocument> : IFilterParameter<TDocument>, IParameter<List<string>>
        where TDocument : IDocument
    {
        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="value">Parameter value used to create the query</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public FilterQueryParameter(IQueryParameterValue value, string tagName = null)
        {
            this.Value = value;
            this.TagName = tagName;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Execute the creation of the parameter "fq"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            Checker.IsNull(this.Value);

            //TODO
            //container.Add($"fq={UtilHelper.GetSolrFilterWithTag(this.TagName, this.Value.Execute())}");
        }

        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        public Expression<Func<TDocument, object>> Expression { get; set; }

        /// <summary>
        /// Value of the filter
        /// </summary>
        public IQueryParameterValue Value { get; set; }

        /// <summary>
        /// Tag name to use in facet excluding list
        /// </summary>
        public string TagName { get; set; }
    }
}
