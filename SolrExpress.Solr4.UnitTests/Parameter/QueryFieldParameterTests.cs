using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Solr4.Parameter;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Parameter
{
    [TestClass]
    public class QueryFieldParameterTests
    {
        /// <summary>
        /// Where   Using a QueryFieldParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void QueryFieldParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new QueryFieldParameter("id^10 score~2^20");

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("qf=id^10 score~2^20", container[0]);
        }

        /// <summary>
        /// Where   Using a QueryFieldParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QueryFieldParameter002()
        {
            // Arrange / Act / Assert
            new QueryFieldParameter(null);
        }
    }
}
