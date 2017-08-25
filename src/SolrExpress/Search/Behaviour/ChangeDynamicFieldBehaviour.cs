using SolrExpress.Builder;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Behaviour
{
    /// <summary>
    /// Change behaviour about dynamic field
    /// </summary>
    public class ChangeDynamicFieldBehaviour<TDocument> : IChangeDynamicFieldBehaviour<TDocument>
        where TDocument : Document
    {
        public ChangeDynamicFieldBehaviour(ExpressionBuilder<TDocument> expressionBuilder)
        {
            this.ExpressionBuilder = expressionBuilder;
        }

        public string DynamicFieldPrefix { get; set; }
        public string DynamicFieldSuffix { get; set; }
        public ExpressionBuilder<TDocument> ExpressionBuilder { get; set; }
        public Expression<Func<TDocument, object>> FieldExpression { get; set; }

        public void Execute()
        {
            var parameter = (ISearchItemFieldExpression<TDocument>)this;
            var parameterBehaviour = (IChangeDynamicFieldBehaviour<TDocument>)this;

            parameter.ExpressionBuilder.SetDynamicFieldPrefixName(parameter.FieldExpression, parameterBehaviour.DynamicFieldPrefix);
            parameter.ExpressionBuilder.SetDynamicFieldSuffixName(parameter.FieldExpression, parameterBehaviour.DynamicFieldSuffix);
        }
    }
}
