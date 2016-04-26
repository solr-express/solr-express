using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.Query.ParameterValue;
using System;

namespace SolrExpress.Core.Tests.ParameterValue
{
    [TestClass]
    public class SingleTests
    {
        /// <summary>
        /// Where   Using a Single instance
        /// When    Create the instance with a string value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void Single001()
        {
            // Arrange
            var expected = "Id:xpto";
            string actual;
            var parameter = new Single<TestDocument>(q => q.Id, "xpto");

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Single instance
        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
        /// What    Returns valid=false
        /// </summary>
        [TestMethod]
        public void Single002()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new Single<TestDocumentWithAttribute>(q => q.NotIndexed, "xpto");

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.IsFalse(actual);
        }

        /// <summary>
        /// Where   Using a Single instance
        /// When    Create the instance with an expression using a field indicated with "index=true" and invoke Validate method
        /// What    Returns valid=true
        /// </summary>
        [TestMethod]
        public void Single003()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new Single<TestDocumentWithAttribute>(q => q.Indexed, "xpto");

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// Where   Using a Single instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Single004()
        {
            // Arrange / Act / Assert
            new Single<TestDocument>(null, "x");
        }

        /// <summary>
        /// Where   Using a Single instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Single005()
        {
            // Arrange / Act / Assert
            new Single<TestDocument>(q => q.Id, null);
        }
    }
}
