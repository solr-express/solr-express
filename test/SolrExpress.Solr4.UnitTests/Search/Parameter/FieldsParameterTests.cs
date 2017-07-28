using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Solr4.Search.Parameter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class FieldsParameterTests
    {
        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Invoking method "Execute" using 2 instances
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FieldsParameter001()
        {
            // Arrange
            var container = new List<string>();
            var solrOptions = new SolrExpressOptions();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);
            var parameter = (IFieldsParameter<TestDocument>)new FieldsParameter<TestDocument>(expressionBuilder);
            parameter.FieldExpressions = new Expression<Func<TestDocument, object>>[] {
                q => q.Id,
                q => q.Score
            };

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("fl=id,score", container[0]);
        }

        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Checking custom attributes of class
        /// What    Has FieldMustBeStoredTrueAttribute
        /// </summary>
        [Fact]
        public void FieldsParameter002()
        {
            // Arrange / Act
            var fieldMustBeIndexedTrueAttribute = typeof(FieldsParameter<TestDocument>)
                .GetTypeInfo()
                .GetCustomAttribute<FieldMustBeStoredTrueAttribute>(true);

            // Assert
            Assert.NotNull(fieldMustBeIndexedTrueAttribute);
        }
    }
}
