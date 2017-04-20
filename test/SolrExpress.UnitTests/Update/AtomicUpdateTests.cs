using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Update;

namespace SolrExpress.UnitTests.Update
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
            var atomic = (IAtomicUpdate<TestDocumentSimple>)new AtomicUpdate<TestDocumentSimple>();

            // Act
            var actual = JArray.Parse(atomic.Execute(document));

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
            var atomic = (IAtomicUpdate<TestDocumentSimple>)new AtomicUpdate<TestDocumentSimple>();

            // Act
            var actual = JArray.Parse(atomic.Execute(document1, document2));

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking method "Execute"
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
            var atomic = (IAtomicUpdate<TestDocumentSimple>)new AtomicUpdate<TestDocumentSimple>();

            // Act
            var actual = JArray.Parse(atomic.Execute(document1, document2));

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicUpdate instance
        /// When    Invoking method "Execute" without any document
        /// What    Create a string.empty
        /// </summary>
        [Fact]
        public void AtomicUpdate004()
        {
            // Arrange
            var expected = string.Empty;
            var atomic = (IAtomicUpdate<TestDocumentSimple>)new AtomicUpdate<TestDocumentSimple>();

            // Act
            var actual = atomic.Execute();

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }
    }
}
