using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class WriteTypeParameterTests
    {
        /// <summary>
        /// Where   Using a WriteTypeParameter instance
        /// When    Invoking method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void WriteTypeParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = (IWriteTypeParameter<TestDocument>)new WriteTypeParameter<TestDocument>();
            var solrExpressOptions = new SolrExpressOptions();
            parameter.Value = WriteType.Json;

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("wt=json", container[0]);
        }
    }
}
