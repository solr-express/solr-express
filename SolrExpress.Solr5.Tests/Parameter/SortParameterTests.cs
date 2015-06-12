using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Solr5.Tests.Parameter
{
    [TestClass]
    public class SortParameterTests
    {
        /// <summary>
        /// Where   Using a LimitParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void LimitParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""limit"": 10
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new LimitParameter(10);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
