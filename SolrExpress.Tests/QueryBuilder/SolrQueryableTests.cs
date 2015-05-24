using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolrExpress.Exception;
using SolrExpress.QueryBuilder;

namespace SolrExpress.Tests.QueryBuilder
{
    [TestClass]
    public class SolrQueryableTests
    {
        [TestMethod]
        [ExpectedException(typeof(AllowMultipleInstanceOfParameterType))]
        public void WhenAddAParamaterThenNotAllowMultipleInstanceForASecondTime_ThrowsException()
        {
            // Arrange
            var providerMock = new Mock<IProvider>();
            var mockParameter = new Mock<IQueryParameter>();
            mockParameter.Setup(q => q.AllowMultipleInstance).Returns(false);
            mockParameter.Setup(q => q.ParameterName).Returns("mock");
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object);
            queryable.Add(mockParameter.Object);

            // Act / Assert
            queryable.Add(mockParameter.Object);
        }
    }
}
