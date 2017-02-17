using Xunit;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class FacetLimitParameterTests
    {
        /// <summary>
        /// Where   Using a FacetLimitParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create correct SOLR instructions
        /// </summary>
        [Fact]
        public void FacetLimitParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = (IFacetLimitParameter<TestDocument>)new FacetLimitParameter<TestDocument>();
            parameter.Value = 10;

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("facet.limit=10", container[0]);
        }
    }
}
