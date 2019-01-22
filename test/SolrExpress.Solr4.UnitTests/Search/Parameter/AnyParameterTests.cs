using SolrExpress.Solr4.Search.Parameter;
using System.Collections.Generic;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter
{
    public class AnyParameterTests
    {
        /// <summary>
        /// Where   Using a AnyParameter instance
        /// When    Invoking method "Execute"
        /// What    Create correct SOLR instructions
        /// </summary>
        [Fact]
        public void AnyParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new AnyParameter
            {
                Name = "x",
                Value = "y"
            };

            // Act
            parameter.Execute();
            parameter.AddResultInContainer(container);

            // Assert
            Assert.Single(container);
            Assert.Equal("x=y", container[0]);
        }
    }
}
