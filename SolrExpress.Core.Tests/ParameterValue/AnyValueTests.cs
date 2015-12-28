using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.ParameterValue;
using System;

namespace SolrExpress.Core.Tests.ParameterValue
{
    [TestClass]
    public class AnyValueTests
    {
        /// <summary>
        /// Where   Using a AnyValue instance
        /// When    Create the instance with a value
        /// What    Get the informed value
        /// </summary>
        [TestMethod]
        public void AnyValue001()
        {
            // Arrange
            var expected = "tst";
            string actual;
            var parameter = new AnyValue("tst");

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a AnyValue instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AnyValue002()
        {
            // Arrange
            var parameter = new AnyValue(null);

            // Act / Assert
            parameter.Execute();
        }
    }
}
