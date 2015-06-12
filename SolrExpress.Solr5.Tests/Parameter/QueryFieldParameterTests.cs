using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Solr5.Tests.Parameter
{
    [TestClass]
    public class QueryFieldParameterTests
    {
        /// <summary>
        /// Where   Using a QueryFieldParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void QueryFieldParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              params:{
                qf:""id^10 score~2^20""
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new QueryFieldParameter("id^10 score~2^20");

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
