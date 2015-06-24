using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Solr4.Parameter;

namespace SolrExpress.Solr4.UnitTests.Parameter
{
    [TestClass]
    public class FilterQueryParameterTests
    {
        /// <summary>
        /// Where   Using a FilterParameter instance
        /// When    Invoking the method "Execute" using 2 instances
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FilterParameter001()
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
    }
}
