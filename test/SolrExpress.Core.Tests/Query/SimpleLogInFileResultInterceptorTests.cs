using Xunit;
using SolrExpress.Core.Query;
using System;

namespace SolrExpress.Core.Tests.Query
{
    public class SimpleLogInFileResultInterceptorTests
    {
        /// <summary>
        /// Where   Using a SimpleLogInFileResultInterceptor instance
        /// When    Creating class with null value
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SimpleLogInFileResultInterceptor001()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new SimpleLogInFileResultInterceptor(null));
        }

        /// <summary>
        /// Where   Using a SimpleLogInFileResultInterceptor instance
        /// When    Creating class with empty value
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SimpleLogInFileResultInterceptor002()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new SimpleLogInFileResultInterceptor(""));
        }
    }
}
