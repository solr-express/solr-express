using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    [FieldMustBeIndexedTrue]
    public sealed class FacetFieldParameter<TDocument> : IFacetFieldParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public FacetFieldParameter(ExpressionBuilder<TDocument> expressionBuilder, ISolrExpressServiceProvider<TDocument> serviceProvider)
        {
            this.ExpressionBuilder = expressionBuilder;
            this.ServiceProvider = serviceProvider;
        }

        public string[] Excludes { get; set; }
        public ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
        public Expression<Func<TDocument, object>> FieldExpression { get; set; }
        public int? Limit { get; set; }
        public int? Minimum { get; set; }
        public FacetSortType? SortType { get; set; }
        public IList<IFacetParameter<TDocument>> Facets { get; set; }
        public ISolrExpressServiceProvider<TDocument> ServiceProvider { get; set; }
        public SearchQuery<TDocument> Filter { get; set; }
        public FacetMethodType? MethodType { get; set; }
        public string Prefix { get; set; }

        public void AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["facet"] ?? new JObject();
            jObj.Add(this._result);
            container["facet"] = jObj;
        }

        public void Execute()
        {
            var fieldName = this.ExpressionBuilder.GetFieldName(this.FieldExpression);
            var aliasName = this.ExpressionBuilder.GetAliasName(this.FieldExpression);

            var array = new List<JProperty>
            {
                new JProperty("field", fieldName)
            };

            if (this.Minimum.HasValue)
            {
                array.Add(new JProperty("mincount", this.Minimum.Value));
            }

            JProperty domain = null;
            if (this.Excludes?.Any() ?? false)
            {
                var excludeValue = new JObject(new JProperty("excludeTags", new JArray(this.Excludes)));
                domain = new JProperty("domain", excludeValue);
            }
            if (this.Filter != null)
            {
                var filter = new JProperty("filter", this.Filter.Execute());
                domain = domain ?? new JProperty("domain", new JObject());
                ((JObject)domain.Value).Add(filter);
            }
            if (this.MethodType.HasValue)
            {
                var methodName = string.Empty;
                switch (this.MethodType.Value)
                {
                    case FacetMethodType.UninvertedField:
                        methodName = "method:uif";
                        break;
                    case FacetMethodType.DocValues:
                        methodName = "method:dv";
                        break;
                    case FacetMethodType.Stream:
                        methodName = "method:stream";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(this.MethodType));
                }

                var method = new JProperty("method", methodName);
                domain = domain ?? new JProperty("domain", new JObject());
                ((JObject)domain.Value).Add(method);
            }
            if (!string.IsNullOrWhiteSpace(this.Prefix))
            {
                var filter = new JProperty("prefix", this.Prefix);
                domain = domain ?? new JProperty("domain", new JObject());
                ((JObject)domain.Value).Add(filter);
            }
            if (domain != null)
            {
                array.Add(domain);
            }

            if (this.SortType.HasValue)
            {
                ParameterUtil.GetFacetSort(this.SortType.Value, out string typeName, out string sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            if (this.Limit.HasValue)
            {
                array.Add(new JProperty("limit", this.Limit));
            }

            if (this.Facets?.Any() ?? false)
            {
                Parallel.ForEach(this.Facets, item => ((ISearchItemExecution<JObject>)item).Execute());

                var subfacets = new JObject();

                foreach (var item in this.Facets)
                {
                    ((ISearchItemExecution<JObject>)item).AddResultInContainer(subfacets);
                }

                array.Add((JProperty)subfacets.First);
            }

            this._result = new JProperty(aliasName, new JObject(new JProperty("terms", new JObject(array.ToArray()))));
        }
    }
}
