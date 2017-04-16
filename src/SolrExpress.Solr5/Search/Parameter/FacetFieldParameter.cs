using Newtonsoft.Json.Linq;
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
    public class FacetFieldParameter<TDocument> : IFacetFieldParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        private JProperty _result;

        public FacetFieldParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            ((ISearchParameterFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        string[] IFacetFieldParameter<TDocument>.Excludes { get; set; }

        ExpressionBuilder<TDocument> ISearchParameterFieldExpression<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>> ISearchParameterFieldExpression<TDocument>.FieldExpression { get; set; }

        int? IFacetFieldParameter<TDocument>.Limit { get; set; }

        int? IFacetFieldParameter<TDocument>.Minimum { get; set; }

        FacetSortType? IFacetFieldParameter<TDocument>.SortType { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["facet"] ?? new JObject();
            jObj.Add(this._result);
            container["facet"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IFacetFieldParameter<TDocument>)this;

            var fieldName = ((ISearchParameterFieldExpression<TDocument>)this).ExpressionBuilder.GetFieldName(parameter.FieldExpression);
            var aliasName = ((ISearchParameterFieldExpression<TDocument>)this).ExpressionBuilder.GetPropertyName(parameter.FieldExpression);

            var array = new List<JProperty>
            {
                new JProperty("field", fieldName)
            };

            if (parameter.Minimum.HasValue)
            {
                array.Add(new JProperty("mincount", parameter.Minimum.Value));
            }

            if (parameter.Excludes?.Any() ?? false)
            {
                var excludeValue = new JObject(new JProperty("excludeTags", new JArray(parameter.Excludes)));
                array.Add(new JProperty("domain", excludeValue));
            }

            if (parameter.SortType.HasValue)
            {
                string typeName;
                string sortName;

                ParameterUtil.GetFacetSort(parameter.SortType.Value, out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            if (parameter.Limit.HasValue)
            {
                array.Add(new JProperty("limit", parameter.Limit));
            }

            this._result = new JProperty(aliasName, new JObject(new JProperty("terms", new JObject(array.ToArray()))));
        }
    }
}
