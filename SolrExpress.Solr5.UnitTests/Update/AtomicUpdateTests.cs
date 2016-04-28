using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Solr5.Update;

namespace SolrExpress.Solr5.UnitTests.Update
{
    [TestClass]
    public class AtomicUpdateTests
    {
        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void AtomicUpdate001()
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
            var document = new TestDocument
            {
                Id = "123456",
                Dummy = "ymmud",
                Spatial = new GeoCoordinate(10, 20)
            };
            var atomic = new AtomicUpdate<TestDocument>();
            atomic.Configure(document);

            // Act
            atomic.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void AtomicUpdate002()
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
            var document1 = new TestDocument
            {
                Id = "123456",
                Dummy = "ymmud",
                Spatial = new GeoCoordinate(10, 20)
            };
            var document2 = new TestDocument
            {
                Id = "654321",
                Dummy = "ymmud2",
                Spatial = new GeoCoordinate(20, 30)
            };
            var atomic = new AtomicUpdate<TestDocument>();
            atomic.Configure(document1);
            atomic.Configure(document2);

            // Act
            atomic.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
