using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using SolrExpress.Search.Query.Extension;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Utility;
using System.Collections.Generic;
using SolrExpress.Options;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class BoostParameterTests
    {
        /// <summary>
        /// Where   Using a BoostParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Create correct SOLR instructions
        /// </summary>
        [Theory]
        [InlineData(BoostFunctionType.Bf, "bf=id")]
        [InlineData(BoostFunctionType.Boost, "boost=id")]
        public void BoostParameterTheory001(BoostFunctionType boostFunctionType, string expected)
        {
            // Arrange
            var container = new List<string>();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);
            var parameter = (IBoostParameter<TestDocument>)new BoostParameter<TestDocument>();
            parameter.BoostFunctionType = boostFunctionType;
            parameter.Query = searchQuery.Field(q => q.Id);

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal(expected, container[0]);
        }
    }
}
