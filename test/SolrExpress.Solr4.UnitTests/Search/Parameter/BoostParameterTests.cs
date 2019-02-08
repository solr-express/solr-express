using SolrExpress.Builder;
using SolrExpress.Configuration;
using SolrExpress.Options;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Utility;
using System.Collections.Generic;
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
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrDocumentConfiguration, solrConnection);
            expressionBuilder.LoadDocument();
            var searchQuery = new SearchQuery<TestDocument>(expressionBuilder);
            var parameter = new BoostParameter<TestDocument>
            {
                BoostFunctionType = boostFunctionType,
                Query = searchQuery.Field(q => q.Id)
            };

            // Act
            parameter.Execute();
            parameter.AddResultInContainer(container);

            // Assert
            Assert.Single(container);
            Assert.Equal(expected, container[0]);
        }
    }
}
