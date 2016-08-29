using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Search.Parameter;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class SystemParameterTests
    {
        /// <summary>
        /// Where   Using a SystemParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void SystemParameter001()
        {
            // Arrange
            var container = new JObject();
            var parameter = new SystemParameter();

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal("none", container["echoParams"]);
            Assert.Equal("json", container["wt"]);
            Assert.Equal("off", container["indent"]);
            Assert.Equal("edismax", container["defType"]);
            Assert.Equal("*,score", container["fl"]);
            Assert.Equal("*:*", container["q.alt"]);
            Assert.Equal("score asc", container["sort"]);
            Assert.Equal("id", container["df"]);
            Assert.Equal("*:*", container["q"]);
        }
    }
}
