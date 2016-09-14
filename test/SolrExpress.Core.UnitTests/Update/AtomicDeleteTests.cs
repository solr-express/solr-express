using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Update;

namespace SolrExpress.Core.UnitTests.Update
{
    public class AtomicDeleteTests
    {
        /// <summary>
        /// Where   Using a AtomicDelete instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicDelete001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
                  ""delete"": ""123456""
            }");
            var atomic = new AtomicDelete<TestDocument>();
            atomic.Configure("123456");

            // Act
            var actual = JObject.Parse(atomic.Execute());

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicDelete instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicDelete002()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
                ""delete"": ""(123456 OR 987654)""
            }");
            var atomic = new AtomicDelete<TestDocument>();
            atomic.Configure("123456", "987654");

            // Act
            var actual = JObject.Parse(atomic.Execute());

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a AtomicDelete instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void AtomicDelete003()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
                ""delete"": ""(123456 OR 987654)""
            }");
            var atomic = new AtomicDelete<TestDocument>();
            atomic.Configure("123456");
            atomic.Configure("987654");

            // Act
            var actual = JObject.Parse(atomic.Execute());

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }
    }
}
