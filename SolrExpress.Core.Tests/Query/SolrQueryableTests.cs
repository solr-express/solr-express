using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Query;

namespace SolrExpress.Core.Tests.Query
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
            var mockParameter = new Mock<IParameter>();
            mockParameter.Setup(q => q.AllowMultipleInstances).Returns(false);
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object);

            // Act / Assert
            queryable.Parameter(mockParameter.Object);
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance configured with FailFast = true
        /// When    Invoking the method "Parameter" with a invalid value
        /// What    Throws InvalidParameterTypeException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterTypeException))]
        public void SolrQueryable002()
        {
            // Arrange
            var mockValidate = new Mock<IValidation>();
            var mockParameter = mockValidate.As<IParameter>();

            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object);

            var isValid = false;
            string errorMessage;

            mockValidate.Setup(q => q.Validate(out isValid, out errorMessage));

            // Act / Assert
            queryable.Parameter(mockParameter.Object);
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance configured with FailFast = false
        /// When    Invoking the method "Parameter"
        /// What    Do not invoke Validate method of parameter class
        /// </summary>
        [TestMethod]
        public void SolrQueryable003()
        {
            // Arrange
            var mockValidate = new Mock<IValidation>();
            var mockParameter = mockValidate.As<IParameter>();
            var solrQueryConfiguration = new SolrQueryConfiguration
            {
                FailFast = false
            };

            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, solrQueryConfiguration);

            bool isValid;
            string errorMessage;

            // Act
            queryable.Parameter(mockParameter.Object);

            // Assert
            mockValidate.Verify(q => q.Validate(out isValid, out errorMessage), Times.Never);
        }
    }
}
