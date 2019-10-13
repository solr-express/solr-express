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
            var atomic = new AtomicUpdate<SimpleTestDocument>();

            // Act / Assert
            Assert.Throws<UnsupportedFeatureException>(() => atomic.Execute());
        }
    }
}
