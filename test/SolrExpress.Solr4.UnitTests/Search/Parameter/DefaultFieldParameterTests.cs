using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Utility;
using System.Collections.Generic;
using SolrExpress.Options;
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
            var parameter = (IDefaultFieldParameter<TestDocument>)new DefaultFieldParameter<TestDocument>(expressionBuilder);
            parameter.ExpressionBuilder = expressionBuilder;
            parameter.FieldExpression = q => q.Id;

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("df=id", container[0]);
        }
    }
}
