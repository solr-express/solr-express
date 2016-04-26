using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query;
using System;

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
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, new Configuration());
            queryable.Parameter(mockParameter.Object);

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
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, new Configuration());

            bool isValid;
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
            var solrQueryConfiguration = new Configuration
            {
                FailFast = false
            };

            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, solrQueryConfiguration);

            bool isValid;
            string errorMessage;

            // Act
            queryable.Parameter(mockParameter.Object);

            // Assert
            mockValidate.Verify(q => q.Validate(out isValid, out errorMessage), Times.Never);
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Parameter" with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SolrQueryable004()
        {
            // Arrange
            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, new Configuration());

            // Act / Assert
            queryable.Parameter(null);
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "QueryInterceptor" with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SolrQueryable005()
        {
            // Arrange
            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, new Configuration());

            // Act / Assert
            queryable.QueryInterceptor(null);
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "QueryInterceptor" with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SolrQueryable006()
        {
            // Arrange
            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, new Configuration());

            // Act / Assert
            queryable.ResultInterceptor(null);
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Create the instance with null in provider parameter
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SolrQueryable007()
        {
            // Arrange / Act / Assert
            new SolrQueryable<TestDocument>(null, new Mock<IResolver>().Object, new Configuration());
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Create the instance with null in resolver parameter
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SolrQueryable008()
        {
            // Arrange / Act / Assert
            new SolrQueryable<TestDocument>(new Mock<IProvider>().Object, null, new Configuration());
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SolrQueryable009()
        {
            // Arrange / Act / Assert
            new SolrQueryable<TestDocument>(new Mock<IProvider>().Object, new Mock<IResolver>().Object, null);
        }
    }
}
