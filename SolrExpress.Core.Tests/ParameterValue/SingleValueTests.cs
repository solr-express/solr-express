using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.ParameterValue;

namespace SolrExpress.Core.Tests.ParameterValue
{
    [TestClass]
    public class SingleValueTests
    {
        /// <summary>
        /// Where   Using a SingleValue instance
        /// When    Create the instance with a string value
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void SingleValue001()
        {
            // Arrange
            var expected = "Id:xpto";
            string actual;
            var parameter = new SingleValue<TestDocument>(q => q.Id, "xpto");

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a SingleValue instance
        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
        /// What    Returns valid=false
        /// </summary>
        [TestMethod]
        public void SingleValue002()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new SingleValue<TestDocumentWithAttribute>(q => q.NotIndexed, "xpto");

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.IsFalse(actual);
        }

        /// <summary>
        /// Where   Using a SingleValue instance
        /// When    Create the instance with an expression using a field indicated with "index=true" and invoke Validate method
        /// What    Returns valid=true
        /// </summary>
        [TestMethod]
        public void SingleValue003()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new SingleValue<TestDocumentWithAttribute>(q => q.Indexed, "xpto");

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.IsTrue(actual);
        }
    }
}
