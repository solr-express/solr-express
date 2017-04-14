using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class AnyParameterTests
    {
        /// <summary>
        /// Where   Using a AnyParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create correct SOLR instructions
        /// </summary>
        [Fact]
        public void AnyParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = (IAnyParameter)new AnyParameter();
            parameter.Name = "x";
            parameter.Value = "y";

            // Act
            ((ISearchItemExecution<List<string>>)parameter).Execute();
            ((ISearchItemExecution<List<string>>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("x=y", container[0]);
        }
    }
}
