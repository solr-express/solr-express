using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class QueryParameterTests
    {
        [TestMethod]
        public void WhenExecute_CreateJson()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""query"": ""Id:ITEM01""
            }");
            string actual;
            var jObject = new JObject();
            var paramer = new QueryParameter<TestDocument>(q => q.Id, "ITEM01");
            
            // Act
            paramer.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
