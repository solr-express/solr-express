using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using SolrExpress.Search.Query.Extension;
using SolrExpress.Solr5.Search.Parameter;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class BoostParameterTests
    {
        /// <summary>
        /// Where   Using a BoostParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Create correct SOLR instructions
        /// </summary>
        [Theory]
        [InlineData(BoostFunctionType.Bf, @"{""params"": {""bf"": ""_id_""}}")]
        [InlineData(BoostFunctionType.Boost, @"{""params"": {""boost"": ""_id_""}}")]
        public void BoostParameterTheory001(BoostFunctionType boostFunctionType, string expectedJson)
        {
            // Arrange
            var expected = JObject.Parse(expectedJson);
            var container = new JObject();
            var solrOptions = new SolrExpressOptions();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);
            var parameter = (IBoostParameter<TestDocument>)new BoostParameter<TestDocument>();
            parameter.BoostFunctionType = boostFunctionType;
            parameter.Query = searchQuery.Field(q => q.Id);

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal(expected.ToString(), container.ToString());
        }
    }
}
