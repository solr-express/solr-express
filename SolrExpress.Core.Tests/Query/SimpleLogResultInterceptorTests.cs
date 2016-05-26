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
        [ExpectedException(typeof(ArgumentNullException))]
        public void SimpleLogResultInterceptor001()
        {
            // Arrange / Act / Assert
            new SimpleLogResultInterceptor(null);
        }

        /// <summary>
        /// Where   Using a SimpleLogResultInterceptor instance
        /// When    Creating class with empty value
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SimpleLogResultInterceptor002()
        {
            // Arrange / Act / Assert
            new SimpleLogResultInterceptor("");
        }
    }
}
