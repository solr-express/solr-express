using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Utility;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.Search.Parameter
{
    [FieldMustBeIndexedTrue]
    public class SpatialFilterParameter<TDocument> : ISpatialFilterParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public SpatialFilterParameter(ExpressionBuilder<TDocument> expressionBuilder)
        {
            ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        GeoCoordinate ISpatialFilterParameter<TDocument>.CenterPoint { get; set; }

        decimal ISpatialFilterParameter<TDocument>.Distance { get; set; }

        ExpressionBuilder<TDocument> ISearchItemFieldExpression<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>> ISearchItemFieldExpression<TDocument>.FieldExpression { get; set; }

        SpatialFunctionType ISpatialFilterParameter<TDocument>.FunctionType { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (ISpatialFilterParameter<TDocument>)this;
            var fieldName = ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder.GetFieldName(parameter.FieldExpression);

            var formule = ParameterUtil.GetSpatialFormule(
                fieldName,
                parameter.FunctionType,
                parameter.CenterPoint,
                parameter.Distance);

            this._result = new JProperty("fq", formule);
        }
    }
}