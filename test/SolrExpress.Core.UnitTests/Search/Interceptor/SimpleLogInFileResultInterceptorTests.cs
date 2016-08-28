using SolrExpress.Core.Search.Interceptor;
using System;
using Xunit;

namespace SolrExpress.Core.UnitTests.Search.Interceptor
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
