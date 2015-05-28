using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class LimitParameterTests
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
              ""offset"": 10
            }");
            string actual;
            var jObject = new JObject();
            var paramer = new OffsetParameter(10);

            // Act
            paramer.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
