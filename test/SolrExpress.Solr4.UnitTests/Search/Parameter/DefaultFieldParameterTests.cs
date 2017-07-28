using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr4.Search.Parameter;
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
            var parameter = (IDefaultFieldParameter<TestDocument>)new DefaultFieldParameter<TestDocument>();
            var solrExpressOptions = new SolrExpressOptions();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrExpressOptions);
            expressionBuilder.LoadDocument();
            parameter.ExpressionBuilder = expressionBuilder;
            parameter.FieldExpression = (q) => q.Id;

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("df=id", container[0]);
        }
    }
}
