using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Solr5.Search.Parameter;
using System.Reflection;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class SortParameterTests
    {
        /// <summary>
        /// Where   Using a SortParameter instance
        /// When    Invoking method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void SortParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""sort"": ""_id_ desc""
            }");
            var container = new JObject();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(new SolrExpressOptions());
            expressionBuilder.LoadDocument();
            var parameter = (ISortParameter<TestDocument>)new SortParameter<TestDocument>(expressionBuilder);
            parameter.FieldExpression(q => q.Id);

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }

        /// <summary>
        /// Where   Using a SortParameter instance
        /// When    Checking custom attributes of class
        /// What    Has FieldMustBeIndexedTrueAttribute
        /// </summary>
        [Fact]
        public void SortParameter002()
        {
            // Arrange / Act
            var fieldMustBeIndexedTrueAttribute = typeof(SortParameter<TestDocument>)
                .GetTypeInfo()
                .GetCustomAttribute<FieldMustBeIndexedTrueAttribute>(true);

            // Assert
            Assert.NotNull(fieldMustBeIndexedTrueAttribute);
        }
    }
}