using Xunit;
using SolrExpress.Core.Search.ParameterValue;
using System;
using SolrExpress.Core.Utility;

namespace SolrExpress.Core.UnitTests.Search.ParameterValue
{
    public class SingleTests
    {
        /// <summary>
        /// Where   Using a Single instance
        /// When    Create the instance with a string value
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void Single001()
        {
            // Arrange
            var expected = "_id_:xpto";
            string actual;
            var expressionCache = new ExpressionCache<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(expressionCache);
            var parameter = new Single<TestDocument>(q => q.Id, "xpto");
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            actual = parameter.Execute();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Where   Using a Single instance
        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
        /// What    Returns valid=false
        /// </summary>
        [Fact]
        public void Single002()
        {
            // Arrange
            bool actual;
            string dummy;
            var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
            var expressionBuilder = new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
            var parameter = new Single<TestDocumentWithAttribute>(q => q.NotIndexed, "xpto");
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.False(actual);
        }

        /// <summary>
        /// Where   Using a Single instance
        /// When    Create the instance with an expression using a field indicated with "index=true" and invoke Validate method
        /// What    Returns valid=true
        /// </summary>
        [Fact]
        public void Single003()
        {
            // Arrange
            bool actual;
            string dummy;
            var expressionCache = new ExpressionCache<TestDocumentWithAttribute>();
            var expressionBuilder = new ExpressionBuilder<TestDocumentWithAttribute>(expressionCache);
            var parameter = new Single<TestDocumentWithAttribute>(q => q.Indexed, "xpto");
            parameter.ExpressionBuilder = expressionBuilder;

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.True(actual);
        }

        /// <summary>
        /// Where   Using a Single instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void Single004()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new Single<TestDocument>(null, "x"));
        }

        /// <summary>
        /// Where   Using a Single instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void Single005()
        {
            // Arrange / Act / Assert
            Assert.Throws<ArgumentNullException>(() => new Single<TestDocument>(q => q.Id, null));
        }
    }
}
