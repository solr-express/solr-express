using Xunit;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class LimitParameterTests
    {
        /// <summary>
        /// Where   Using a LimitParameter instance
        /// When    Invoking method "Execute"
        /// What    Create correct SOLR instructions
        /// </summary>
        [Fact]
        public void LimitParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new LimitParameter<TestDocument>();
            parameter.Value = 10;

            // Act
            parameter.Execute();
            parameter.AddResultInContainer(container);

            // Assert
            Assert.Single(container);
            Assert.Equal("rows=10", container[0]);
        }
    }
}
