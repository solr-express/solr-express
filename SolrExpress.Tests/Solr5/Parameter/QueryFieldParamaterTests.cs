using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class QueryFieldParamaterTests
    {
        /// <summary>
        /// Where   Using a QueryFieldParamater instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void QueryFieldParamater001()
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
            var paramer = new QueryFieldParamater("id^10 score~2^20");

            // Act
            paramer.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
