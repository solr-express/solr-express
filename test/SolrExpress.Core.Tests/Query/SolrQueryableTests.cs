using Xunit;
using Moq;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query;
using System;

namespace SolrExpress.Core.Tests.Query
{
    public class SolrQueryableTests
    {
        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Invoking the method "Parameter" using a concret class than implement IQueryParameter, configured to do not allow multiple instances
        /// What    Throws AllowMultipleInstanceOfParameterTypeException
        /// </summary>
        [Fact]
        public void SolrQueryable001()
        {
            // Arrange
            var providerMock = new Mock<IProvider>();
            var mockParameter = new Mock<IParameter>();
            mockParameter.Setup(q => q.AllowMultipleInstances).Returns(false);
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, new Configuration());
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

            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, new Configuration());

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
        [Fact]
        public void SolrQueryable004()
        {
            // Arrange
            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, new Configuration());

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
            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, new Configuration());

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
            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, new Configuration());

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => queryable.ResultInterceptor((IResultInterceptor)null));
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Create the instance with null in provider parameter
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SolrQueryable007()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new SolrQueryable<TestDocument>(null, new Mock<IResolver>().Object, new Configuration()));
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Create the instance with null in resolver parameter
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SolrQueryable008()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new SolrQueryable<TestDocument>(new Mock<IProvider>().Object, null, new Configuration()));
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SolrQueryable009()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new SolrQueryable<TestDocument>(new Mock<IProvider>().Object, new Mock<IResolver>().Object, null));
        }

        /// <summary>
        /// Where   Using a SolrQueryable instance configured with FailFast = true and CheckAnyParameter = true
        /// When    Invoking the method "Parameter" with a invalid value
        /// What    Throws InvalidParameterTypeException
        /// </summary>
        [Fact]
        public void SolrQueryable010()
        {
            // Arrange
            var mockValidate = new Mock<IValidation>();
            var mockParameter = mockValidate.As<IParameter>();
            var mockAnyParameter = mockParameter.As<IAnyParameter>();

            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, new Configuration());

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
        public void SolrQueryable011()
        {
            // Arrange
            var mockValidate = new Mock<IValidation>();
            var mockParameter = mockValidate.As<IParameter>();
            var mockAnyParameter = mockParameter.As<IAnyParameter>();
            var solrQueryConfiguration = new Configuration
            {
                FailFast = false
            };

            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, solrQueryConfiguration);

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
        public void SolrQueryable012()
        {
            // Arrange
            var mockValidate = new Mock<IValidation>();
            var mockParameter = mockValidate.As<IParameter>();
            var mockAnyParameter = mockParameter.As<IAnyParameter>();
            var solrQueryConfiguration = new Configuration
            {
                CheckAnyParameter = false
            };

            var providerMock = new Mock<IProvider>();
            var queryable = new SolrQueryable<TestDocument>(providerMock.Object, new Mock<IResolver>().Object, solrQueryConfiguration);

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
        public void SolrQueryable013()
        {
            // Arrange
            var queryable = new SolrQueryable<TestDocument>(new Mock<IProvider>().Object, new Mock<IResolver>().Object, new Configuration());

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => queryable.Handler(null));
        }
    }
}
