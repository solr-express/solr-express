using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.ParameterValue;
using System;

namespace SolrExpress.Core.Tests.ParameterValue
{
    [TestClass]
    public class NegativeValueTests
    {
        /// <summary>
        /// Where   Using a NegativeValue instance
        /// When    Create the instance with a value
        /// What    Get the informed value
        /// </summary>
        [TestMethod]
        public void NegativeValue001()
        {
            // Arrange
            var expected = "-tst";
            string actual;
            var parameter = new NegativeValue(new FreeValue("tst"));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a NegativeValue instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NegativeValue002()
        {
            // Arrange
            var parameter = new NegativeValue(null);

            // Act / Assert
            parameter.Execute();
        }
    }
}
