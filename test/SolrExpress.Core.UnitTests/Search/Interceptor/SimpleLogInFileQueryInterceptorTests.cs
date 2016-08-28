using SolrExpress.Core.Search.Interceptor;
using System;
using Xunit;

namespace SolrExpress.Core.UnitTests.Search.Interceptor
{
    public class SimpleLogInFileQueryInterceptorTests
    {
        /// <summary>
        /// Where   Using a SimpleLogInFileQueryInterceptor instance
        /// When    Creating class with null value
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SimpleLogInFileQueryInterceptor001()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new SimpleLogInFileQueryInterceptor(null));
        }

        /// <summary>
        /// Where   Using a SimpleLogInFileQueryInterceptor instance
        /// When    Creating class with empty value
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SimpleLogInFileQueryInterceptor002()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new SimpleLogInFileQueryInterceptor(""));
        }
    }
}
