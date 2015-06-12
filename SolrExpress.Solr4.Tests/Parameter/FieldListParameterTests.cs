using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Solr4.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Tests.Parameter
{
    [TestClass]
    public class FieldListParameterTests
    {
        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Invoking the method "Execute" using 2 instances
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FieldsParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter1 = new FieldListParameter<TestDocument>(q => q.Id);
            var parameter2 = new FieldListParameter<TestDocument>(q => q.Score);

            // Act
            parameter1.Execute(container);
            parameter2.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("fl=Id,Score", container[0]);
        }
    }
}
