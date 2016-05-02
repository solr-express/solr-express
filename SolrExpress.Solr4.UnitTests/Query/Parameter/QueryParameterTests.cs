using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Solr4.Query.Parameter;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Query.Parameter
{
    [TestClass]
    public class QueryParameterTests
    {
        /// <summary>
        /// Where   Using a QueryParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void QueryParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new QueryParameter<TestDocument>();
            parameter.Configure(new Single<TestDocument>(q => q.Id, "ITEM01"));

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("q=Id:ITEM01", container[0]);
        }

        /// <summary>
        /// Where   Using a QueryParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QueryParameter002()
        {
            // Arrange / Act / Assert
            var parameter = new QueryParameter<TestDocument>();
            parameter.Configure(null);
        }
    }
}
