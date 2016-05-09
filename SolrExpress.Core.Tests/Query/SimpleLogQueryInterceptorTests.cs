using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.Query;
using System;

namespace SolrExpress.Core.Tests.Query
{
    [TestClass]
    public class SimpleLogQueryInterceptorTests
    {
        /// <summary>
        /// Where   Using a SimpleLogQueryInterceptor instance
        /// When    Creating class with null value
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
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
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SimpleLogQueryInterceptor002()
        {
            // Arrange / Act / Assert
            new SimpleLogQueryInterceptor("");
        }
    }
}
