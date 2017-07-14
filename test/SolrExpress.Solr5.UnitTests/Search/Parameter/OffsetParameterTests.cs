using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr5.Search.Parameter;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class OffsetParameterTests
    {
        /// <summary>
        /// Where   Using a OffsetParameter instance
        /// When    Invoking method "Execute"
        /// What    Create correct SOLR instructions
        /// </summary>
        [Fact]
        public void OffsetParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
                ""offset"": 10
            }");
            var container = new JObject();
            var parameter = (IOffsetParameter<TestDocument>)new OffsetParameter<TestDocument>();
            parameter.Value = 10;

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }
    }
}
