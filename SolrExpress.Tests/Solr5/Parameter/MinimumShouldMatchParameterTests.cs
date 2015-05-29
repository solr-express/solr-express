using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class MinimumShouldMatchParameterTests
    {
        /// <summary>
        /// Where   Using a MinimumShouldMatchParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void MinimumShouldMatchParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              params:{
                mm:""75%""
              }
            }");
            string actual;
            var jObject = new JObject();
            var paramer = new MinimumShouldMatchParameter("75%");

            // Act
            paramer.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
