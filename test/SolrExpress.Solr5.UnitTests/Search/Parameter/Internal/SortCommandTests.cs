using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Search.Parameter.Internal;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter.Internal
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
            var expected = JObject.Parse(@"
            {
              ""sort"": ""id asc""
            }");
            string actual;
            var jObject = new JObject();
            var command = new SortCommand();

            // Act
            command.Execute("id", true, jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
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
            var expected = JObject.Parse(@"
            {
              ""sort"": ""field1 asc, field2 desc""
            }");
            string actual;
            var jObject = new JObject();
            var command = new SortCommand();

            // Act
            command.Execute("field1", true, jObject);
            command.Execute("field2", false, jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }
    }
}
