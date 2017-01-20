using SolrExpress.Core;
using Xunit;

namespace SolrExpress.Solr4.UnitTests
{
    public class SolrConnectionTests
    {
        /// <summary>
        /// Where   Using a SolrConnection instance
        /// When    Invoking the method "Get" using AuthenticationType.Basic
        /// What    Throw exception
        /// </summary>
        [Fact]
        public void SolrConnection001()
        {
            // Arrange
            var options = new SecurityOptions
            {
                AuthenticationType = AuthenticationType.Basic
            };
            var connection = new SolrConnection();
            
            // Act / Assert
            Assert.Throws<UnsupportedSecuritySystemException>(() => connection.Get(options, string.Empty, string.Empty));
        }

        /// <summary>
        /// Where   Using a SolrConnection instance
        /// When    Invoking the method "Post" using AuthenticationType.Basic
        /// What    Throw exception
        /// </summary>
        [Fact]
        public void SolrConnection002()
        {
            // Arrange
            var options = new SecurityOptions
            {
                AuthenticationType = AuthenticationType.Basic
            };
            var connection = new SolrConnection();
            
            // Act / Assert
            Assert.Throws<UnsupportedSecuritySystemException>(() => connection.Post(options, string.Empty, string.Empty));
        }
    }
}
