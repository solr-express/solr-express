using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.ParameterValue;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FilterQueryParameter<TDocument> : IFilterParameter<TDocument>, IParameter<List<string>>
        where TDocument : IDocument
    {
        private IQueryParameterValue _value;
        private string _tagName;

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
            var expression = this._value.Execute().GetSolrFilterWithTag(this._tagName);

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

            this._value = value;
            this._tagName = tagName;

            return this;
        }
    }
}
