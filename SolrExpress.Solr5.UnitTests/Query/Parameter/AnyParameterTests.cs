using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Query.Parameter;

namespace SolrExpress.Solr5.UnitTests.Query.Parameter
{
    [TestClass]
    public class AnyParameterTests
    {
        /// <summary>
        /// Where   Using a AnyParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void AnyParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
                ""params"":{
                    ""x"": ""y""
                }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new AnyParameter();
            parameter.Configure("x", "y");

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
