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
    [FacetRangeType]
    [FieldMustBeIndexedTrue]
    public class FacetRangeParameter<TDocument> : IFacetRangeParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public FacetRangeParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        string IFacetRangeParameter<TDocument>.AliasName { get; set; }

        bool IFacetRangeParameter<TDocument>.CountAfter { get; set; }

        bool IFacetRangeParameter<TDocument>.CountBefore { get; set; }

        string IFacetRangeParameter<TDocument>.End { get; set; }

        string[] IFacetRangeParameter<TDocument>.Excludes { get; set; }

        ExpressionBuilder<TDocument> ISearchItemFieldExpression<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>> ISearchItemFieldExpression<TDocument>.FieldExpression { get; set; }

        string IFacetRangeParameter<TDocument>.Gap { get; set; }

        int? IFacetRangeParameter<TDocument>.Limit { get; set; }

        int? IFacetRangeParameter<TDocument>.Minimum { get; set; }

        FacetSortType? IFacetRangeParameter<TDocument>.SortType { get; set; }

        string IFacetRangeParameter<TDocument>.Start { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["facet"] ?? new JObject();
            jObj.Add(this._result);
            container["facet"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IFacetRangeParameter<TDocument>)this;

            var array = new List<JProperty>
            {
                new JProperty("field", ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder.GetFieldName(parameter.FieldExpression))
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

            if (!string.IsNullOrWhiteSpace(parameter.Gap))
            {
                array.Add(new JProperty("gap", parameter.Gap));
            }
            if (!string.IsNullOrWhiteSpace(parameter.Start))
            {
                array.Add(new JProperty("start", parameter.Start));
            }
            if (!string.IsNullOrWhiteSpace(parameter.End))
            {
                array.Add(new JProperty("end", parameter.End));
            }

            if (parameter.CountBefore || parameter.CountAfter)
            {
                var content = new List<string>();
                if (parameter.CountBefore)
                {
                    content.Add("before");
                }

                if (parameter.CountAfter)
                {
                    content.Add("after");
                }

                array.Add(new JProperty("other", new JArray(content.ToArray())));
            }

            if (parameter.SortType.HasValue)
            {
                string typeName;
                string sortName;

                ParameterUtil.GetFacetSort(parameter.SortType.Value, out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            this._result = new JProperty(parameter.AliasName, new JObject(new JProperty("range", new JObject(array.ToArray()))));
        }
    }
}