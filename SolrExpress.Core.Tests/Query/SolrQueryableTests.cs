using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolrExpress.Core.Builder;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Parameter;
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
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IParameterFactory<TestDocument>>().Object, new Mock<IBuilderFactory<TestDocument>>().Object);
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
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IParameterFactory<TestDocument>>().Object, new Mock<IBuilderFactory<TestDocument>>().Object);

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
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IParameterFactory<TestDocument>>().Object, new Mock<IBuilderFactory<TestDocument>>().Object, solrQueryConfiguration);

            bool isValid;
            string errorMessage;

            // Act
            queryable.Parameter(mockParameter.Object);

            // Assert
            mockValidate.Verify(q => q.Validate(out isValid, out errorMessage), Times.Never);
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SolrQueryable004()
        {
            // Arrange / Act / Assert
            new SolrQueryable<TestDocument>(null, null, null);
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Parameter" with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SolrQueryable005()
        {
            // Arrange
            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IParameterFactory<TestDocument>>().Object, new Mock<IBuilderFactory<TestDocument>>().Object);

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
        public void SolrQueryable006()
        {
            // Arrange
            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IParameterFactory<TestDocument>>().Object, new Mock<IBuilderFactory<TestDocument>>().Object);

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
        public void SolrQueryable007()
        {
            // Arrange
            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IParameterFactory<TestDocument>>().Object, new Mock<IBuilderFactory<TestDocument>>().Object);

            // Act / Assert
            queryable.ResultInterceptor(null);
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance configured with Handler name
        /// When    Creating SolrQueryable object
        /// What    Change provider instance property "Handler" with informad name
        /// </summary>
        [TestMethod]
        public void SolrQueryable008()
        {
            // Arrange
            string handlerName = "HANDLER-NAME";
            var solrQueryConfiguration = new SolrQueryConfiguration
            {
                Handler = handlerName
            };

            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IParameterFactory<TestDocument>>().Object, new Mock<IBuilderFactory<TestDocument>>().Object, solrQueryConfiguration);

            providerMock.Setup(q => q.Execute(It.IsAny<string>(), It.IsAny<string>())).Returns(".");

            // Act
            queryable.Execute();

            // Assert
            providerMock.Verify(q => q.Execute(It.Is<string>(s => s.Equals(handlerName)), It.IsAny<string>()), Times.Once);
        }
    }
}
