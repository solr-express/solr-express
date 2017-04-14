using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search.Parameter;
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
    public class FacetSpatialParameter<TDocument> : IFacetSpatialParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        private readonly ExpressionBuilder<TDocument> _expressionBuilder;
        private JProperty _result;

        public FacetSpatialParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this._expressionBuilder = expressionBuilder;
        }

        string IFacetSpatialParameter<TDocument>.AliasName { get; set; }

        GeoCoordinate IFacetSpatialParameter<TDocument>.CenterPoint { get; set; }

        decimal IFacetSpatialParameter<TDocument>.Distance { get; set; }

        string[] IFacetSpatialParameter<TDocument>.Excludes { get; set; }

        Expression<Func<TDocument, object>> ISearchParameterFieldExpression<TDocument>.FieldExpression { get; set; }

        SpatialFunctionType IFacetSpatialParameter<TDocument>.FunctionType { get; set; }

        int? IFacetSpatialParameter<TDocument>.Limit { get; set; }

        int? IFacetSpatialParameter<TDocument>.Minimum { get; set; }

        FacetSortType? IFacetSpatialParameter<TDocument>.SortType { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["facet"] ?? new JObject();
            jObj.Add(this._result);
            container["facet"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IFacetSpatialParameter<TDocument>)this;

            var formule = ParameterUtil.GetSpatialFormule(
                this._expressionBuilder.GetFieldNameFromExpression(parameter.FieldExpression),
                parameter.FunctionType,
                parameter.CenterPoint,
                parameter.Distance);

            var array = new List<JProperty>
            {
                new JProperty("q", formule)
            };

            if (parameter.Excludes?.Any() ?? false)
            {
                var excludeValue = new JObject(new JProperty("excludeTags", new JArray(parameter.Excludes)));
                array.Add(new JProperty("domain", excludeValue));
            }

            if (parameter.Minimum.HasValue)
            {
                array.Add(new JProperty("mincount", parameter.Minimum.Value));
            }

            if (parameter.SortType.HasValue)
            {
                string typeName;
                string sortName;

                ParameterUtil.GetFacetSort(parameter.SortType.Value, out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            this._result = new JProperty(parameter.AliasName, new JObject(new JProperty("query", new JObject(array.ToArray()))));
        }
    }
}
