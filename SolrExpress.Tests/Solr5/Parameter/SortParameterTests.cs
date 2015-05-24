using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class SortParameterTests
    {
        [TestMethod]
        public void WhenExecute_CreateJson()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""limit"": 10
            }");
            string actual;
            var jObject = new JObject();
            var paramer = new LimitParameter(10);

            // Act
            paramer.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
