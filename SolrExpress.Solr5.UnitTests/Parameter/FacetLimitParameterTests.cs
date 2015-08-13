using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Solr5.UnitTests.Parameter
{
    [TestClass]
    public class FacetLimitParameterTests
    {
        /// <summary>
        /// Where   Using a FacetLimitParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void FacetLimitParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              params:{
                ""facet.limit"":10
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetLimitParameter(10);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
