using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Update;
using SolrExpress.Update;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Update
{
    public class AtomicUpdateTests
    {
        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicUpdate001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
	            ""add"": [{

                        ""dummy"": ""ymmud"",
			            ""id"": ""123456""

                    }
	            ],
	            ""commit"": {}
            }");
            var document = new SimpleTestDocument
            {
                Id = "123456",
                Dummy = "ymmud"
            };
            var atomic = (IAtomicUpdate<SimpleTestDocument>)new AtomicUpdate<SimpleTestDocument>();

            // Act
            var actual = JObject.Parse(atomic.Execute(document));

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicUpdate002()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
	            ""add"": [{
                        ""dummy"": ""ymmud"",
			            ""id"": ""123456""
                    },
		            {
                        ""dummy"": ""ymmud2"",
			            ""id"": ""654321""
		            }
	            ],
	            ""commit"": {}
            }");
            var document1 = new SimpleTestDocument
            {
                Id = "123456",
                Dummy = "ymmud"
            };
            var document2 = new SimpleTestDocument
            {
                Id = "654321",
                Dummy = "ymmud2"
            };
            var atomic = (IAtomicUpdate<SimpleTestDocument>)new AtomicUpdate<SimpleTestDocument>();

            // Act
            var actual = JObject.Parse(atomic.Execute(document1, document2));

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking method "Execute" without any document
        /// What    Create a string.empty
        /// </summary>
        [Fact]
        public void AtomicUpdate003()
        {
            // Arrange
            var expected = string.Empty;
            var atomic = (IAtomicUpdate<SimpleTestDocument>)new AtomicUpdate<SimpleTestDocument>();

            // Act
            var actual = atomic.Execute();

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }
    }
}
