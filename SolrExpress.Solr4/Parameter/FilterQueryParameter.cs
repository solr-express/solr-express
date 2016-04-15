using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
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
        private Expression<Func<TDocument, object>> _expression { get; set; }
        private IQueryParameterValue _value { get; set; }
        private string _tagName { get; set; }

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
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public IFilterParameter<TDocument> Configure(Expression<Func<TDocument, object>> expression, IQueryParameterValue value, string tagName)
        {
            Checker.IsNull(expression);
            Checker.IsNull(value);

            this._expression = expression;
            this._value = value;
            this._tagName = tagName;

            return this;
        }
    }
}
