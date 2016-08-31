using System.Collections.Generic;
using SolrExpress.Solr4.Search.Parameter.Internal;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Search.Parameter.Internal
{
    public class SortCommandTests
    {
        /// <summary>
        /// Where   Using a SortCommand instance
        /// When    Invoking the method "Execute" with id|asc
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void SortCommand001()
        {
            // Arrange
            var container = new List<string>();
            var command = new BaseSortSolr4Parameter();

            // Act
            command.Execute("id", true, container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("sort=id asc", container[0]);
        }

        /// <summary>
        /// Where   Using a SortCommand instance
        /// When    Invoking the method "Execute" with id|asc
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void SortCommand002()
        {
            // Arrange
            var container = new List<string>();
            var command = new BaseSortSolr4Parameter();

            // Act
            command.Execute("field1", true, container);
            command.Execute("field2", false, container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("sort=field1 asc,field2 desc", container[0]);
        }
    }
}
