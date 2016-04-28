using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Update;

namespace SolrExpress.Solr5.UnitTests.Update
{
    [TestClass]
    public class AtomicDeleteTests
    {
        /// <summary>
        /// Where   Using a AtomicDelete instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void AtomicDelete001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
                ""delete"":{
                    ""id"": ""123456""
                }
            }");
            string actual;
            var jObject = new JObject();
            var atomic = new AtomicDelete<TestDocument>();
            atomic.Configure("123456");

            // Act
            atomic.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a AtomicDelete instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void AtomicDelete002()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
                ""delete"":{
                    ""id"": ""(123456 OR 987654)""
                }
            }");
            string actual;
            var jObject = new JObject();
            var atomic = new AtomicDelete<TestDocument>();
            atomic.Configure("123456", "987654");

            // Act
            atomic.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
