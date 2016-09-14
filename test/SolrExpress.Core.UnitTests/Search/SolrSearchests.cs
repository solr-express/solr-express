using Moq;
using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using System;
using Xunit;

namespace SolrExpress.Core.UnitTests.Search
{
    public class SolrSearchests
    {
        private Mock<IEngine> _engine;

        public SolrSearchests()
        {
            this._engine = new Mock<IEngine>();
            this._engine.Setup(q => q.GetService<ISolrConnection>()).Returns(new Mock<ISolrConnection>().Object);
        }

        /// <summary>
        /// Where   Using a SolrSearch instance
        /// When    Invoking the method "Parameter" using a concret class than implement IQueryParameter, configured to do not allow multiple instances
        /// What    Throws AllowMultipleInstanceOfParameterTypeException
        /// </summary>
        [Fact]
        public void SolrSearch001()
        {
            // Arrange
            var parameter = new Mock<ISearchParameter>();
            parameter.Setup(q => q.AllowMultipleInstances).Returns(false);
            var queryable = new SolrSearch<TestDocument>(new DocumentCollectionOptions<TestDocument>(), this._engine.Object);
            queryable.Add(parameter.Object);

            // Act / Assert
            Assert.Throws<AllowMultipleInstanceOfParameterTypeException>(() => queryable.Add(parameter.Object));
        }

        /// <summary>
        /// Where   Using a SolrSearch instance configured with FailFast = true
        /// When    Invoking the method "Parameter" with a invalid value
        /// What    Throws InvalidParameterTypeException
        /// </summary>
        [Fact]
        public void SolrSearch002()
        {
            // Arrange
            var validate = new Mock<IValidation>();
            var parameter = validate.As<ISearchParameter>();

            var providerMock = new Mock<ISolrConnection>();
            var queryable = new SolrSearch<TestDocument>(new DocumentCollectionOptions<TestDocument>(), this._engine.Object);

            bool isValid;
            var errorMessage = "test";

            validate.Setup(q => q.Validate(out isValid, out errorMessage));

            // Act / Assert
            Assert.Throws<InvalidParameterTypeException>(() => queryable.Add(parameter.Object));
        }

        /// <summary>
        /// Where   Using a SolrSearch instance configured with FailFast = false
        /// When    Invoking the method "Parameter"
        /// What    Do not invoke Validate method of parameter class
        /// </summary>
        [Fact]
        public void SolrSearch003()
        {
            // Arrange
            var validate = new Mock<IValidation>();
            var parameter = validate.As<ISearchParameter>();
            var documentCollectionOptions = new DocumentCollectionOptions<TestDocument>
            {
                FailFast = false
            };

            var queryable = new SolrSearch<TestDocument>(documentCollectionOptions, this._engine.Object);

            bool isValid;
            string errorMessage;

            // Act
            queryable.Add(parameter.Object);

            // Assert
            validate.Verify(q => q.Validate(out isValid, out errorMessage), Times.Never);
        }

        /// <summary>
        /// Where   Using a SolrSearch instance
        /// When    Invoking the method "Parameter" with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SolrSearch004()
        {
            // Arrange
            var queryable = new SolrSearch<TestDocument>(new DocumentCollectionOptions<TestDocument>(), this._engine.Object);

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => queryable.Add((ISearchParameter)null));
        }

        /// <summary>
        /// Where   Using a SolrSearch instance
        /// When    Invoking the method "QueryInterceptor" with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SolrSearch005()
        {
            // Arrange
            var queryable = new SolrSearch<TestDocument>(new DocumentCollectionOptions<TestDocument>(), this._engine.Object);

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => queryable.Add((ISearchInterceptor)null));
        }

        /// <summary>
        /// Where   Using a SolrSearch instance
        /// When    Invoking the method "QueryInterceptor" with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SolrSearch006()
        {
            // Arrange
            var queryable = new SolrSearch<TestDocument>(new DocumentCollectionOptions<TestDocument>(), this._engine.Object);

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => queryable.Add((IResultInterceptor)null));
        }

        /// <summary>
        /// Where   Using a SolrSearch instance configured with FailFast = true and CheckAnyParameter = true
        /// When    Invoking the method "Parameter" with a invalid value
        /// What    Throws InvalidParameterTypeException
        /// </summary>
        [Fact]
        public void SolrSearch008()
        {
            // Arrange
            var validate = new Mock<IValidation>();
            var parameter = validate.As<ISearchParameter>();
            var anyParameter = parameter.As<IAnyParameter>();

            var providerMock = new Mock<ISolrConnection>();
            var queryable = new SolrSearch<TestDocument>(new DocumentCollectionOptions<TestDocument>(), this._engine.Object);

            bool isValid;
            var errorMessage = "test";

            validate.Setup(q => q.Validate(out isValid, out errorMessage));

            // Act / Assert
            Assert.Throws<InvalidParameterTypeException>(() => queryable.Add(anyParameter.Object));
        }

        /// <summary>
        /// Where   Using a SolrSearch instance configured with FailFast = false and CheckAnyParameter = true
        /// When    Invoking the method "Parameter" with a invalid value
        /// What    Do not invoke Validate method of parameter class
        /// </summary>
        [Fact]
        public void SolrSearch009()
        {
            // Arrange
            var validate = new Mock<IValidation>();
            var parameter = validate.As<ISearchParameter>();
            var anyParameter = parameter.As<IAnyParameter>();
            var documentCollectionOptions = new DocumentCollectionOptions<TestDocument>
            {
                FailFast = false
            };

            var queryable = new SolrSearch<TestDocument>(documentCollectionOptions, this._engine.Object);

            bool isValid;
            string errorMessage;

            validate.Setup(q => q.Validate(out isValid, out errorMessage));

            // Act
            queryable.Add(anyParameter.Object);

            // Assert
            validate.Verify(q => q.Validate(out isValid, out errorMessage), Times.Never);
        }

        /// <summary>
        /// Where   Using a SolrSearch instance configured with FailFast = true and CheckAnyParameter = false
        /// When    Invoking the method "Parameter" with a invalid value
        /// What    Do not invoke Validate method of parameter class
        /// </summary>
        [Fact]
        public void SolrSearch010()
        {
            // Arrange
            var validate = new Mock<IValidation>();
            var parameter = validate.As<ISearchParameter>();
            var anyParameter = parameter.As<IAnyParameter>();
            var documentCollectionOptions = new DocumentCollectionOptions<TestDocument>
            {
                CheckAnyParameter = false
            };

            var queryable = new SolrSearch<TestDocument>(documentCollectionOptions, this._engine.Object);

            bool isValid;
            string errorMessage;

            validate.Setup(q => q.Validate(out isValid, out errorMessage));

            // Act
            queryable.Add(anyParameter.Object);

            // Assert
            validate.Verify(q => q.Validate(out isValid, out errorMessage), Times.Never);
        }

        /// <summary>
        /// Where   Using a SolrSearch instance
        /// When    Invoking the method "Handler" with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void SolrSearch011()
        {
            // Arrange
            var queryable = new SolrSearch<TestDocument>(new DocumentCollectionOptions<TestDocument>(), this._engine.Object);

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => queryable.SetHandler(null));
        }
    }
}
