using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    [FieldMustBeIndexedTrue]
    public sealed class FacetSpatialParameter<TDocument> : IFacetSpatialParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public FacetSpatialParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this.ExpressionBuilder = expressionBuilder;
        }

        public string AliasName { get; set; }
        public GeoCoordinate CenterPoint { get; set; }
        public decimal Distance { get; set; }
        public string[] Excludes { get; set; }
        public ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
        public Expression<Func<TDocument, object>> FieldExpression { get; set; }
        public SpatialFunctionType FunctionType { get; set; }
        public int? Limit { get; set; }
        public int? Minimum { get; set; }
        public FacetSortType? SortType { get; set; }

        public void AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["facet"] ?? new JObject();
            jObj.Add(this._result);
            container["facet"] = jObj;
        }

        public void Execute()
        {
            var formule = ParameterUtil.GetSpatialFormule(
                this.ExpressionBuilder.GetFieldName(this.FieldExpression),
                this.FunctionType,
                this.CenterPoint,
                this.Distance);

            var array = new List<JProperty>
            {
                new JProperty("q", formule)
            };

            if (this.Excludes?.Any() ?? false)
            {
                var excludeValue = new JObject(new JProperty("excludeTags", new JArray(this.Excludes)));
                array.Add(new JProperty("domain", excludeValue));
            }

            if (this.Minimum.HasValue)
            {
                array.Add(new JProperty("mincount", this.Minimum.Value));
            }

            if (this.SortType.HasValue)
            {
                ParameterUtil.GetFacetSort(this.SortType.Value, out string typeName, out string sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            this._result = new JProperty(this.AliasName, new JObject(new JProperty("query", new JObject(array.ToArray()))));
        }
    }
}
