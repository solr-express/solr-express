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
            ((ISearchItemFieldExpression<TDocument>)this).ExpressionBuilder = expressionBuilder;
        }

        string IChangeDynamicFieldBehaviour<TDocument>.DynamicFieldPrefix { get; set; }

        string IChangeDynamicFieldBehaviour<TDocument>.DynamicFieldSuffix { get; set; }

        ExpressionBuilder<TDocument> ISearchItemFieldExpression<TDocument>.ExpressionBuilder { get; set; }

        Expression<Func<TDocument, object>> ISearchItemFieldExpression<TDocument>.FieldExpression { get; set; }

        void IChangeBehaviour.Execute()
        {
            var parameter = ((ISearchItemFieldExpression<TDocument>)this);
            var parameterBehaviour = ((IChangeDynamicFieldBehaviour<TDocument>)this);

            parameter.ExpressionBuilder.SetDynamicFieldPrefixName(parameter.FieldExpression, parameterBehaviour.DynamicFieldPrefix);
            parameter.ExpressionBuilder.SetDynamicFieldSuffixName(parameter.FieldExpression, parameterBehaviour.DynamicFieldSuffix);
        }
    }
}
