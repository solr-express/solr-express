using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class FieldListParameter<TDocument> : BaseFieldsParameter<TDocument>, ISearchParameter<List<string>>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the creation of the parameter "fl"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            foreach (var expression in this.Expressions)
            {
                var fieldName = expression.GetFieldNameFromExpression();

                var fieldList = container.FirstOrDefault(q => q.StartsWith("fl="));

                if (!string.IsNullOrWhiteSpace(fieldList))
                {
                    container.Remove(fieldList);

                    fieldList = $"{fieldList},{fieldName}";
                }
                else
                {
                    fieldList = $"fl={fieldName}";
                }

                container.Add(fieldList);
            }
        }
    }
}
