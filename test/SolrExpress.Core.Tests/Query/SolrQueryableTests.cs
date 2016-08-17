using Moq;
using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using System;
using Xunit;

namespace SolrExpress.Core.Tests.Query
{
    public class SolrQueryableTests
    {
        public SolrQueryableTests()
        {
            var mockEngine = new MockEngine();
            mockEngine.Setup(q => q.GetService<ISolrConnection>()).Returns(new Mock<ISolrConnection>().Object);

            ApplicationServices.Current = mockEngine;
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Parameter" using a concret class than implement IQueryParameter, configured to do not allow multiple instances
        /// What    Throws AllowMultipleInstanceOfParameterTypeException
        /// </summary>
        [Fact]
        public void SolrQueryable001()
        {
            // Arrange
            var mockParameter = new Mock<IParameter>();
            mockParameter.Setup(q => q.AllowMultipleInstances).Returns(false);
            var queryable = new SolrQueryable<TestDocument>(new DocumentCollectionOptions<TestDocument>());
            queryable.Parameter(mockParameter.Object);

            // Act / Assert
            Assert.Throws<AllowMultipleInstanceOfParameterTypeException>(() => queryable.Parameter(mockParameter.Object));
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance configured with FailFast = true
        /// When    Invoking the method "Parameter" with a invalid value
        /// What    Throws InvalidParameterTypeException
        /// </summary>
        [Fact]
        public void SolrQueryable002()
        {
            // Arrange
            var mockValidate = new Mock<IValidation>();
            var mockParameter = mockValidate.As<IParameter>();

            var providerMock = new Mock<ISolrConnection>();
            var queryable = new SolrQueryable<TestDocument>(new DocumentCollectionOptions<TestDocument>());

            bool isValid;
            var errorMessage = "test";

            mockValidate.Setup(q => q.Validate(out isValid, out errorMessage));

            // Act / Assert
            Assert.Throws<InvalidParameterTypeException>(() => queryable.Parameter(mockParameter.Object));
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance configured with FailFast = false
        /// When    Invoking the method "Parameter"
        /// What    Do not invoke Validate method of parameter class
        /// </summary>
        [Fact]
        public void SolrQueryable003()
        {
            // Arrange
            var mockValidate = new Mock<IValidation>();
            var mockParameter = mockValidate.As<IParameter>();
            var documentCollectionOptions = new DocumentCollectionOptions<TestDocument>
            {
                FailFast = false
            };

            var queryable = new SolrQueryable<TestDocument>(documentCollectionOptions);

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
        [Fact]
        public void SolrQueryable004()
        {
            // Arrange
            var providerMock = new Mock<ISolrConnection>();
            var queryable = new SolrQueryable<TestDocument>(new DocumentCollectionOptions<TestDocument>());

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => queryable.Parameter((IParameter)null));
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "QueryInterceptor" with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SolrQueryable005()
        {
            // Arrange
            var providerMock = new Mock<ISolrConnection>();
            var queryable = new SolrQueryable<TestDocument>(new DocumentCollectionOptions<TestDocument>());

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => queryable.QueryInterceptor((IQueryInterceptor)null));
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "QueryInterceptor" with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SolrQueryable006()
        {
            // Arrange
            var providerMock = new Mock<ISolrConnection>();
            var queryable = new SolrQueryable<TestDocument>(new DocumentCollectionOptions<TestDocument>());

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => queryable.ResultInterceptor((IResultInterceptor)null));
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Create the instance with null in options
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SolrQueryable007()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new SolrQueryable<TestDocument>(null));
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance configured with FailFast = true and CheckAnyParameter = true
        /// When    Invoking the method "Parameter" with a invalid value
        /// What    Throws InvalidParameterTypeException
        /// </summary>
        [Fact]
        public void SolrQueryable008()
        {
            // Arrange
            var mockValidate = new Mock<IValidation>();
            var mockParameter = mockValidate.As<IParameter>();
            var mockAnyParameter = mockParameter.As<IAnyParameter>();

            var providerMock = new Mock<ISolrConnection>();
            var queryable = new SolrQueryable<TestDocument>(new DocumentCollectionOptions<TestDocument>());

            bool isValid;
            var errorMessage = "test";

            mockValidate.Setup(q => q.Validate(out isValid, out errorMessage));

            // Act / Assert
            Assert.Throws<InvalidParameterTypeException>(() => queryable.Parameter(mockAnyParameter.Object));
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance configured with FailFast = false and CheckAnyParameter = true
        /// When    Invoking the method "Parameter" with a invalid value
        /// What    Do not invoke Validate method of parameter class
        /// </summary>
        [Fact]
        public void SolrQueryable009()
        {
            // Arrange
            var mockValidate = new Mock<IValidation>();
            var mockParameter = mockValidate.As<IParameter>();
            var mockAnyParameter = mockParameter.As<IAnyParameter>();
            var documentCollectionOptions = new DocumentCollectionOptions<TestDocument>
            {
                FailFast = false
            };

            var queryable = new SolrQueryable<TestDocument>(documentCollectionOptions);

            bool isValid;
            string errorMessage;

            mockValidate.Setup(q => q.Validate(out isValid, out errorMessage));

            // Act
            queryable.Parameter(mockAnyParameter.Object);

            // Assert
            mockValidate.Verify(q => q.Validate(out isValid, out errorMessage), Times.Never);
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance configured with FailFast = true and CheckAnyParameter = false
        /// When    Invoking the method "Parameter" with a invalid value
        /// What    Do not invoke Validate method of parameter class
        /// </summary>
        [Fact]
        public void SolrQueryable010()
        {
            // Arrange
            var mockValidate = new Mock<IValidation>();
            var mockParameter = mockValidate.As<IParameter>();
            var mockAnyParameter = mockParameter.As<IAnyParameter>();
            var documentCollectionOptions = new DocumentCollectionOptions<TestDocument>
            {
                CheckAnyParameter = false
            };

            var queryable = new SolrQueryable<TestDocument>(documentCollectionOptions);

            bool isValid;
            string errorMessage;

            mockValidate.Setup(q => q.Validate(out isValid, out errorMessage));

            // Act
            queryable.Parameter(mockAnyParameter.Object);

            // Assert
            mockValidate.Verify(q => q.Validate(out isValid, out errorMessage), Times.Never);
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Handler" with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SolrQueryable011()
        {
            // Arrange
            var queryable = new SolrQueryable<TestDocument>(new DocumentCollectionOptions<TestDocument>());

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => queryable.Handler(null));
        }
    }
}
