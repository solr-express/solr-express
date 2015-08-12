using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.ParameterValue;
using System;

namespace SolrExpress.Core.Tests.ParameterValue
{
    [TestClass]
    public class MultiValueTests
    {
        /// <summary>
        /// Where   Using a MultiValue instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MultiValue001()
        {
            // Arrange
            var parameter = new MultiValue(SolrQueryConditionType.And, null);

            // Act / Assert
            parameter.Execute();
        }

        /// <summary>
        /// Where   Using a MultiValue instance
        /// When    Create the instance with 1 query parameter
        /// What    Throws ArgumentException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MultiValue002()
        {
            // Arrange
            var parameter = new MultiValue(SolrQueryConditionType.And, new FreeValue("X"));

            // Act / Assert
            parameter.Execute();
        }

        /// <summary>
        /// Where   Using a MultiValue instance
        /// When    Create the instance with AND condition and 2 query parameters
        /// What    Throws ArgumentException
        /// </summary>
        [TestMethod]
        public void MultiValue003()
        {
            // Arrange
            var expected = "(\"value 1\" AND \"value 2\")";
            string actual;
            var parameter = new MultiValue(SolrQueryConditionType.And, new FreeValue("value 1"), new FreeValue("value 2"));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Where   Using a MultiValue instance
        /// When    Create the instance with OR condition and 2 query parameters
        /// What    Throws ArgumentException
        /// </summary>
        [TestMethod]
        public void MultiValue004()
        {
            // Arrange
            var expected = "(\"value 1\" OR \"value 2\")";
            string actual;
            var parameter = new MultiValue(SolrQueryConditionType.Or, new FreeValue("value 1"), new FreeValue("value 2"));

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
