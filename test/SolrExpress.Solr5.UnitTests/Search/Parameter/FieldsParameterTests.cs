using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Solr5.Search.Parameter;
using System;
using System.Linq.Expressions;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class FieldsParameterTests
    {
        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Invoking the method "Execute" using 2 instances
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void FieldsParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""fields"": [
                ""_id_"",
                ""_score_""
              ]
            }");
            string actual;
            var jObject = new JObject();
            var parameter1 = new FieldsParameter<TestDocument>();
            var parameter2 = new FieldsParameter<TestDocument>();
            parameter1.Configure(q => q.Id);
            parameter2.Configure(q => q.Score);

            // Act
            parameter1.Execute(jObject);
            parameter2.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Invoking the method "Execute" using 1 instance and 2 expressions
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FieldsParameter002()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""fields"": [
                ""_id_"",
                ""_score_""
              ]
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FieldsParameter<TestDocument>();
            parameter.Configure(q => q.Id, q => q.Score);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
        /// What    Returns valid=false
        /// </summary>
        [Fact]
        public void FieldsParameter003()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new FieldsParameter<TestDocumentWithAttribute>();
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
        public void FieldsParameter004()
        {
            // Arrange
            var parameter = new FieldsParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }

        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Create the instance with empty collection
        /// What    Throws ArgumentException
        /// </summary>
        [Fact]
        public void FieldsParameter005()
        {
            // Arrange
            var parameter = new FieldsParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentException>(() => parameter.Configure(new Expression<Func<TestDocument, object>>[] { }));
        }

        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Invoking the method "Validate" using field Store=true
        /// What    Valid is true
        /// </summary>
        [Fact]
        public void FieldsParameter006()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FieldsParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.Stored);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
        }

        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Invoking the method "Validate" using field Store=false
        /// What    Valid is true
        /// </summary>
        [Fact]
        public void FieldsParameter007()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FieldsParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.NotStored);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.False(isValid);
            Assert.Equal(Resource.FieldMustBeStoredTrueToBeUsedInFieldsException, errorMessage);
        }
    }
}
