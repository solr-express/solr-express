using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class SystemParameterTests
    {
        /// <summary>
        /// Where   Using a SystemParameter instance
        /// When    Invoking method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void SystemParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = (ISystemParameter<TestDocument>)new SystemParameter<TestDocument>();

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(2, container.Count);
            Assert.Equal("echoParams=none", container[0]);
            Assert.Equal("indent=off", container[1]);
        }
    }
}
