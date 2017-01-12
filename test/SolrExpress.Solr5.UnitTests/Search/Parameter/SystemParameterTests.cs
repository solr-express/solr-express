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
            var parameter = new SystemParameter<TestDocument>();
            parameter.Configure();

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal("none", container["params"]["echoParams"]);
            Assert.Equal("json", container["params"]["wt"]);
            Assert.Equal("off", container["params"]["indent"]);
            Assert.Equal("edismax", container["params"]["defType"]);
            Assert.Equal("*:*", container["params"]["q.alt"]);
            Assert.Equal("id", container["params"]["df"]);
            Assert.Equal("*,score", container["fields"][0]);
            Assert.Equal("score desc", container["sort"]);
            Assert.Equal("*:*", container["query"]);
        }
    }
}
