using Xunit;
using SolrExpress.Solr4.Query.Parameter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Solr4.UnitTests.Query.Parameter
{
    public class FieldListParameterTests
    {
        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Invoking the method "Execute" using 2 instances
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FieldListParameter001()
        {
            // Arrange
            var container = new List<string>();
            var parameter1 = new FieldListParameter<TestDocument>();
            var parameter2 = new FieldListParameter<TestDocument>();
            parameter1.Configure(q => q.Id);
            parameter2.Configure(q => q.Score);

            // Act
            parameter1.Execute(container);
            parameter2.Execute(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("fl=_id_,_score_", container[0]);
        }

        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
        /// What    Returns valid=false
        /// </summary>
        [Fact]
        public void FieldListParameter002()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new FieldListParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.NotStored);

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.False(actual);
        }

        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=true" and invoke Validate method
        /// What    Returns valid=true
        /// </summary>
        [Fact]
        public void FieldListParameter003()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new FieldListParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.Stored);

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.True(actual);
        }

        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Invoking the method "Execute" using 1 instance and 2 expressions
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FieldListParameter004()
        {
            // Arrange
            var container = new List<string>();
            var parameter = new FieldListParameter<TestDocument>();
            parameter.Configure(q => q.Id, q => q.Score);

            // Act
            parameter.Execute(container);

            // Assert
            Assert.Equal(1, container.Count);
            Assert.Equal("fl=_id_,_score_", container[0]);
        }

        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
        /// What    Returns valid=false
        /// </summary>
        [Fact]
        public void FieldListParameter005()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new FieldListParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.Stored, q => q.NotStored);

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.False(actual);
        }

        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FieldListParameter006()
        {
            // Arrange
            var parameter = new FieldListParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }

        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Create the instance with empty collection
        /// What    Throws ArgumentOutOfRangeException
        /// </summary>
        [Fact]
        public void FieldListParameter007()
        {
            // Arrange
            var parameter = new FieldListParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => parameter.Configure(new Expression<Func<TestDocument, object>>[] { }));
        }
    }
}
