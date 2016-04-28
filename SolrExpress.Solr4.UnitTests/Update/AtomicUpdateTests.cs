using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Solr4.Update;

namespace SolrExpress.Solr4.UnitTests.Update
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
            var expected = JArray.Parse(@"
            [
                {
                    ""id"": ""123456""
                },
                {
                    ""id"": ""654321""
                }
            ]");
            string actual;
            var jObject = new JObject();
            var document1 = new TestDocumentSimple
            {
                Id = "123456",
                Text = "ymmud"
            };
            var document2 = new TestDocumentSimple
            {
                Id = "654321",
                Text = "ymmud2"
            };
            var atomic = new AtomicUpdate<TestDocumentSimple>();
            atomic.Configure(document1, document2);

            // Act
            atomic.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
