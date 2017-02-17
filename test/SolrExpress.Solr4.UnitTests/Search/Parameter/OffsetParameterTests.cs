using Xunit;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class OffsetParameterTests
    {
        /// <summary>
        /// Where   Using a OffsetParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create correct SOLR instructions
        /// </summary>
        [Fact]
        public void OffsetParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = (IOffsetParameter<TestDocument>)new OffsetParameter<TestDocument>();
            parameter.Value = 10;

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("start=10", container[0]);
        }
    }
}
