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
    }
}
