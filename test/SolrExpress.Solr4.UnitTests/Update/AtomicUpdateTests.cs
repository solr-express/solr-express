using Newtonsoft.Json.Linq;
using SolrExpress.Configuration;
using SolrExpress.Solr4.Update;
using Xunit;

namespace SolrExpress.Solr4.UnitTests.Update
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
	            ""add"": {
		            ""doc"": {
			            ""dummy"": ""ymmud"",
			            ""id"": ""123456""
		            },
		            ""overwrite"": true
	            },
                ""commit"": {}
            }");
            var document = new SimpleTestDocument
            {
                Id = "123456",
                Dummy = "ymmud"
            };
            var configuration = new SolrDocumentConfiguration<SimpleTestDocument>();
            configuration.Field(q => q.Id).HasName("id");
            configuration.Field(q => q.Dummy).HasName("dummy");
            var atomic = new AtomicUpdate<SimpleTestDocument>(configuration);

            // Act
            var actual = atomic.Execute(document);

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking method "Execute" without any document
        /// What    Create a string.empty
        /// </summary>
        [Fact]
        public void AtomicUpdate002()
        {
            // Arrange
            var configuration = new SolrDocumentConfiguration<SimpleTestDocument>();
            configuration.Field(q => q.Id).HasName("id");
            configuration.Field(q => q.Dummy).HasName("dummy");
            var atomic = new AtomicUpdate<SimpleTestDocument>(configuration);

            // Act
            var actual = atomic.Execute();

            // Assert
            Assert.Null(actual);
        }
    }
}
