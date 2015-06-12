using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.ParameterValue;

namespace SolrExpress.Tests.ParameterValue
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
    }
}
