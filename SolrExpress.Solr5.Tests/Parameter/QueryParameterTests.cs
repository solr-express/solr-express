using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Solr5.Tests.Parameter
{
    [TestClass]
    public class QueryParameterTests
    {
        /// <summary>
        /// Where   Using a QueryParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void QueryParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""query"": ""Id:ITEM01""
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new QueryParameter(new SingleValue<TestDocument>(q => q.Id, "ITEM01"));

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
