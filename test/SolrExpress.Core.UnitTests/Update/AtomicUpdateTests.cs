using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Update;

namespace SolrExpress.Core.UnitTests.Update
{
    public class AtomicUpdateTests
    {
        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicUpdate001()
        {
            // Arrange
            var expected = JArray.Parse(@"
            [
              {
                ""Id"": ""123456"",
                ""Text"": ""ymmud""
              }
            ]");
            var document = new TestDocumentSimple
            {
                Id = "123456",
                Text = "ymmud"
            };
            var atomic = new AtomicUpdate<TestDocumentSimple>();
            atomic.Configure(document);

            // Act
            var actual = JArray.Parse(atomic.Execute());

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicUpdate002()
        {
            // Arrange
            var expected = JArray.Parse(@"[
              {
                ""Id"": ""123456"",
                ""Text"": ""ymmud""
              },
              {
                ""Id"": ""654321"",
                ""Text"": ""ymmud2""
              }
            ]");
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
            var actual = JArray.Parse(atomic.Execute());

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicUpdate003()
        {
            // Arrange
            var expected = JArray.Parse(@"[
              {
                ""Id"": ""123456"",
                ""Text"": ""ymmud""
              },
              {
                ""Id"": ""654321"",
                ""Text"": ""ymmud2""
              }
            ]");
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
            var actual = JArray.Parse(atomic.Execute());

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }
    }
}
