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
            parameter.Configure();

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal("none", container["params"]["echoParams"]);
            Assert.Equal("json", container["params"]["wt"]);
            Assert.Equal("off", container["params"]["indent"]);
            Assert.Equal("edismax", container["params"]["defType"]);
            Assert.Equal("*,score", container["params"]["fl"]);
            Assert.Equal("*:*", container["params"]["q.alt"]);
            Assert.Equal("score asc", container["params"]["sort"]);
            Assert.Equal("id", container["params"]["df"]);
            Assert.Equal("*:*", container["params"]["q"]);
        }
    }
}
