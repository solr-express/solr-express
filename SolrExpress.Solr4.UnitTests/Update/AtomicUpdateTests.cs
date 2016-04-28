using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
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
            var expected = JObject.Parse(@"{
                ""add"": {
                    ""doc"": [
                        {
                        ""Id"": ""123456"",
                        ""Text"": ""ymmud""
                        }
                    ]
                }
            }");
            string actual;
            var jObject = new JObject();
            var document = new TestDocumentSimple
            {
                Id = "123456",
                Text = "ymmud"
            };
            var atomic = new AtomicUpdate<TestDocumentSimple>();
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
            var expected = JObject.Parse(@"{
                ""add"": {
                    ""doc"": [
                        {
                        ""Id"": ""123456"",
                        ""Text"": ""ymmud""
                        },
                        {
                        ""Id"": ""654321"",
                        ""Text"": ""ymmud2""
                        }
                    ]
                }
            }");
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

        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void AtomicUpdate003()
        {
            // Arrange
            var expected = JObject.Parse(@"{
                ""add"": {
                    ""doc"": [
                        {
                        ""Id"": ""123456"",
                        ""Text"": ""ymmud""
                        },
                        {
                        ""Id"": ""654321"",
                        ""Text"": ""ymmud2""
                        }
                    ]
                }
            }");
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
