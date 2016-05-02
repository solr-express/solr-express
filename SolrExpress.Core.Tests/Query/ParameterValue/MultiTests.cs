using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.Query.ParameterValue;
using System;

namespace SolrExpress.Core.Tests.Query.ParameterValue
{
    [TestClass]
    public class MultiTests
    {
        /// <summary>
        /// Where   Using a Multi instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Multi001()
        {
            // Arrange
            var parameter = new Multi(SolrQueryConditionType.And, null);

            // Act / Assert
            parameter.Execute();
        }

        /// <summary>
        /// Where   Using a Multi instance
        /// When    Create the instance with 1 query parameter
        /// What    Throws ArgumentException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Multi002()
        {
            // Arrange
            var parameter = new Multi(SolrQueryConditionType.And, new Any("X"));

            // Act / Assert
            parameter.Execute();
        }

        /// <summary>
        /// Where   Using a Multi instance
        /// When    Create the instance with AND condition and 2 query parameters
        /// What    Throws ArgumentException
        /// </summary>
        [TestMethod]
        public void Multi003()
        {
            // Arrange
            var expected = "(value 1 AND value 2)";
            string actual;
            var parameter = new Multi(SolrQueryConditionType.And, new Any("value 1"), new Any("value 2"));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a Multi instance
        /// When    Create the instance with OR condition and 2 query parameters
        /// What    Throws ArgumentException
        /// </summary>
        [TestMethod]
        public void Multi004()
        {
            // Arrange
            var expected = "(value 1 OR value 2)";
            string actual;
            var parameter = new Multi(SolrQueryConditionType.Or, new Any("value 1"), new Any("value 2"));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
