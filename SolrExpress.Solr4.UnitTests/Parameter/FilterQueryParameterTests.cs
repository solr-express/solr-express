using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Solr4.Parameter;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.UnitTests.Parameter
{
    [TestClass]
    public class FilterQueryParameterTests
    {
        /// <summary>
        /// Where   Using a FilterQueryParameter instance
        /// When    Invoking the method "Execute" using 2 instances
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FilterQueryParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter1 = new FilterQueryParameter<TestDocument>();
            var parameter2 = new FilterQueryParameter<TestDocument>();
            parameter1.Configure(new Single<TestDocument>(q => q.Id, "X"));
            parameter2.Configure(new Single<TestDocument>(q => q.Score, "Y"));

            // Act
            parameter1.Execute(container);
            parameter2.Execute(container);

            // Assert
            Assert.AreEqual(2, container.Count);
            Assert.AreEqual("fq=Id:X", container[0]);
            Assert.AreEqual("fq=Score:Y", container[1]);
        }

        /// <summary>
        /// Where   Using a FilterQueryParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FilterQueryParameter002()
        {
            // Arrange / Act / Assert
            var parameter = new FilterQueryParameter<TestDocument>();
            parameter.Configure(null);
        }

        /// <summary>
        /// Where   Using a FilterQueryParameter instance
        /// When    Invoking the method "Execute" using tag name
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FilterQueryParameter003()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FilterQueryParameter<TestDocument>();
            parameter.Configure(new Single<TestDocument>(q => q.Id, "X"), "tag1");

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("fq={!tag=tag1}Id:X", container[0]);
        }
    }
}
