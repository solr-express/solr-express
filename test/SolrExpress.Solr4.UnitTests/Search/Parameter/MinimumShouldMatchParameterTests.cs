using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class MinimumShouldMatchParameterTests
    {
        /// <summary>
        /// Where   Using a MinimumShouldMatchParameter instance
        /// When    Invoking method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void MinimumShouldMatchParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = (IMinimumShouldMatchParameter<TestDocument>)new MinimumShouldMatchParameter<TestDocument>();
            parameter.Value = "75%";

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Single(container);
            Assert.Equal("mm=75%", container[0]);
        }
    }
}
