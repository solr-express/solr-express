using Newtonsoft.Json.Linq;
using SolrExpress.Options;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Solr5.Search.Parameter;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
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
            var expected = JObject.Parse(@"
            {
                ""params"":
                {
                    ""wt"": ""json""
                }
            }");
            var container = new JObject();
            var parameter = (IWriteTypeParameter<TestDocument>)new WriteTypeParameter<TestDocument>();
            var solrExpressOptions = new SolrExpressOptions();
            parameter.Value = WriteType.Json;

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }
    }
}
