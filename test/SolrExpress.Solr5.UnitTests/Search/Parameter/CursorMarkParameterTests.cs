using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Solr5.Search.Parameter;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class CursorMarkParameterTests
    {
        /// <summary>
        /// Where   Using a CursorMarkParameter instance
        /// When    Invoking method "Execute"
        /// What    Create correct SOLR instructions
        /// </summary>
        [Fact]
        public void CursorMarkParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
                ""params"":{
                    ""cursorMark"": ""xpto""
                }
            }");
            var container = new JObject();
            var parameter = new CursorMarkParameter();
            parameter.CursorMark = "xpto";

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expected.ToString(), container.ToString());
        }
    }
}
