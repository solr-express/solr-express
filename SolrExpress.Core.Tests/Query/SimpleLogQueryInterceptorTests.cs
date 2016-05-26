using Xunit;
using SolrExpress.Core.Query;
using System;

namespace SolrExpress.Core.Tests.Query
{
    public class SimpleLogQueryInterceptorTests
    {
        /// <summary>
        /// Where   Using a SimpleLogQueryInterceptor instance
        /// When    Creating class with null value
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SimpleLogQueryInterceptor001()
        {
            // Arrange / Act / Assert
            new SimpleLogQueryInterceptor(null);
        }

        /// <summary>
        /// Where   Using a SimpleLogQueryInterceptor instance
        /// When    Creating class with empty value
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SimpleLogQueryInterceptor002()
        {
            // Arrange / Act / Assert
            new SimpleLogQueryInterceptor("");
        }
    }
}
