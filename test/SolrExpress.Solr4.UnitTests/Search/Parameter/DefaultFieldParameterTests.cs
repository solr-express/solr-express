using SolrExpress.Builder;
using SolrExpress.Options;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Utility;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class DefaultFieldParameterTests
    {
        /// <summary>
        /// Where   Using a DefaultFieldParameter instance
        /// When    Invoking method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void DefaultFieldParameter001()
        {
            // Arrange
            var container = new List<string>();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var parameter = new DefaultFieldParameter<TestDocument>(expressionBuilder)
            {
                FieldExpression = q => q.Id
            };

            // Act
            parameter.Execute();
            parameter.AddResultInContainer(container);

            // Assert
            Assert.Single(container);
            Assert.Equal("df=id", container[0]);
        }
    }
}
