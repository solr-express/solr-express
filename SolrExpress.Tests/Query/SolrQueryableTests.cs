using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Query;

namespace SolrExpress.Tests.Query
{
    [TestClass]
    public class SolrQueryableTests
    {
        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Parameter" using a concret class than implement IQueryParameter, configured to do not allow multiple instances
        /// What    Throws AllowMultipleInstanceOfParameterTypeException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AllowMultipleInstanceOfParameterTypeException))]
        public void SolrQueryable001()
        {
            // Arrange
            var providerMock = new Mock<IProvider>();
            var mockParameter = new Mock<IQueryParameter>();
            mockParameter.Setup(q => q.AllowMultipleInstances).Returns(false);
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object);
            queryable.Parameter(mockParameter.Object);

            // Act / Assert
            queryable.Parameter(mockParameter.Object);
        }
    }
}
