using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrExpress.Core.ParameterValue;

namespace SolrExpress.Core.Tests.ParameterValue
{
    [TestClass]
    public class FreeValueTests
    {
        /// <summary>
        /// Where   Using a FreeValue instance
        /// When    Create the instance with a value
        /// What    Get the informed value
        /// </summary>
        [TestMethod]
        public void FreeValue001()
        {
            // Arrange
            var expected = "tst";
            string actual;
            var parameter = new FreeValue("tst");

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
