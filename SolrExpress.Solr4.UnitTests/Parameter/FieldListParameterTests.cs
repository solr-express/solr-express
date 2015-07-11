using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Solr4.Parameter;

namespace SolrExpress.Solr4.UnitTests.Parameter
{
    [TestClass]
    public class FieldListParameterTests
    {
        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Invoking the method "Execute" using 2 instances
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FieldListParameter001()
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

        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
        /// What    Returns valid=false
        /// </summary>
        [TestMethod]
        public void FieldListParameter002()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new FieldListParameter<TestDocumentWithAttribute>(q => q.NotStored);

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.IsFalse(actual);
        }

        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=true" and invoke Validate method
        /// What    Returns valid=true
        /// </summary>
        [TestMethod]
        public void FieldListParameter003()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new FieldListParameter<TestDocumentWithAttribute>(q => q.Stored);

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Invoking the method "Execute" using 1 instance and 2 expressions
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FieldListParameter004()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FieldListParameter<TestDocument>(q => q.Id, q => q.Score);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.AreEqual(1, container.Count);
            Assert.AreEqual("fl=Id,Score", container[0]);
        }

        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
        /// What    Returns valid=false
        /// </summary>
        [TestMethod]
        public void FieldListParameter005()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new FieldListParameter<TestDocumentWithAttribute>(q => q.Stored, q => q.NotStored);

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.IsFalse(actual);
        }
    }
}
