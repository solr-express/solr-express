using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr5.Search.Parameter;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
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
            var expected = JObject.Parse(@"
            {
              ""params"": {
                ""mm"": ""75%""
              }
            }");
            var container = new JObject();
            var parameter = (IMinimumShouldMatchParameter<TestDocument>)new MinimumShouldMatchParameter<TestDocument>();
            parameter.Value = "75%";

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }
    }
}
