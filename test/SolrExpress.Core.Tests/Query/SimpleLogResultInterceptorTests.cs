using Xunit;
using SolrExpress.Core.Query;
using System;

namespace SolrExpress.Core.Tests.Query
{
    public class SimpleLogResultInterceptorTests
    {
        /// <summary>
        /// Where   Using a SimpleLogResultInterceptor instance
        /// When    Creating class with null value
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SimpleLogResultInterceptor001()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new SimpleLogResultInterceptor(null));
        }

        /// <summary>
        /// Where   Using a SimpleLogResultInterceptor instance
        /// When    Creating class with empty value
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SimpleLogResultInterceptor002()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new SimpleLogResultInterceptor(""));
        }
    }
}
