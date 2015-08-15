using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Solr4.Parameter;
using System;

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
            var parameter1 = new FilterQueryParameter(new SingleValue<TestDocument>(q => q.Id, "X"));
            var parameter2 = new FilterQueryParameter(new SingleValue<TestDocument>(q => q.Score, "Y"));

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
            new FilterQueryParameter(null);
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
            var parameter1 = new FilterQueryParameter(new SingleValue<TestDocument>(q => q.Id, "X"), "tag1");

            // Act
            parameter1.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("fq={!tag=tag1}Id:X", container[0]);
        }
    }
}
