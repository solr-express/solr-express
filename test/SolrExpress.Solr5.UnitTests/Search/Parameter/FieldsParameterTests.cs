using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Solr5.Search.Parameter;
using SolrExpress.Utility;
using System;
using System.Linq.Expressions;
using System.Reflection;
using SolrExpress.Options;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
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
            var expected = JObject.Parse(@"
            {
              ""fields"": [
                ""id"",
                ""score""
              ]
            }");
            var container = new JObject();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);
            var parameter = (IFieldsParameter<TestDocument>)new FieldsParameter<TestDocument>(expressionBuilder);
            parameter.FieldExpressions = new Expression<Func<TestDocument, object>>[] {
                q => q.Id,
                q => q.Score
            };

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
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
