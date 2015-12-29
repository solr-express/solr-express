using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.ParameterValue;
using System;

namespace SolrExpress.Core.Tests.ParameterValue
{
    [TestClass]
    public class NegateTests
    {
        /// <summary>
        /// Where   Using a Negate instance
        /// When    Create the instance with a value
        /// What    Get the informed value
        /// </summary>
        [TestMethod]
        public void Negate001()
        {
            // Arrange
            var expected = "-tst";
            string actual;
            var parameter = new Negate(new Any("tst"));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Negate instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Negate002()
        {
            // Arrange
            var parameter = new Negate(null);

            // Act / Assert
            parameter.Execute();
        }
    }
}
