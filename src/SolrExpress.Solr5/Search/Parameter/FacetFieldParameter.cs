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
using System.Threading.Tasks;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    [FieldMustBeIndexedTrue]
    public class FacetFieldParameter<TDocument> : IFacetFieldParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public FacetFieldParameter(ExpressionBuilder<TDocument> expressionBuilder, ISolrExpressServiceProvider<TDocument> serviceProvider)
        {
            ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
            ((IFacetParameter<TDocument>)this).ServiceProvider = serviceProvider;
        }

        string[] IFacetFieldParameter<TDocument>.Excludes { get; set; }

        ExpressionBuilder<TDocument> ISearchItemFieldExpression<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>> ISearchItemFieldExpression<TDocument>.FieldExpression { get; set; }

        int? IFacetFieldParameter<TDocument>.Limit { get; set; }

        int? IFacetFieldParameter<TDocument>.Minimum { get; set; }

        FacetSortType? IFacetFieldParameter<TDocument>.SortType { get; set; }

        IList<IFacetParameter<TDocument>> IFacetParameter<TDocument>.Facets { get; set; }

        ISolrExpressServiceProvider<TDocument> IFacetParameter<TDocument>.ServiceProvider { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["facet"] ?? new JObject();
            jObj.Add(this._result);
            container["facet"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IFacetFieldParameter<TDocument>)this;

            var fieldName = ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder.GetFieldName(parameter.FieldExpression);
            var aliasName = ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder.GetAliasName(parameter.FieldExpression);

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

            if (parameter.Facets?.Any() ?? false)
            {
                Parallel.ForEach(parameter.Facets, item => ((ISearchItemExecution<JObject>)item).Execute());

                var subfacets = new JObject();

                foreach (var item in parameter.Facets)
                {
                    ((ISearchItemExecution<JObject>)item).AddResultInContainer(subfacets);
                }

                array.Add((JProperty)subfacets.First);
            }

            this._result = new JProperty(aliasName, new JObject(new JProperty("terms", new JObject(array.ToArray()))));
        }
    }
}
