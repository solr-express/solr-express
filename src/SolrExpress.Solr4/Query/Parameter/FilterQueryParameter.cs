using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.ParameterValue;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Query.Parameter
{
    public sealed class FilterQueryParameter<TDocument> : IFilterParameter<TDocument>, IParameter<List<string>>
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Value of the filter
        /// </summary>
        public IQueryParameterValue Value { get; private set; }

        /// <summary>
        /// Tag name to use in facet excluding list
        /// </summary>
        public string TagName { get; private set; }

        /// <summary>
        /// Execute the creation of the parameter "fq"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            var expression = this.Value.Execute().GetSolrFilterWithTag(this.TagName);

            container.Add($"fq={expression}");
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of the filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public IFilterParameter<TDocument> Configure(IQueryParameterValue value, string tagName = null)
        {
            Checker.IsNull(value);

            this.Value = value;
            this.TagName = tagName;

            return this;
        }
    }
}
