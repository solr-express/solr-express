using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Query.Parameter;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Solr5.UnitTests.Query.Parameter
{
    [TestClass]
    public class FieldsParameterTests
    {
        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Invoking the method "Execute" using 2 instances
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void FieldsParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""fields"": [
                ""Id"",
                ""Score""
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
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
        /// What    Returns valid=false
        /// </summary>
        [TestMethod]
        public void FieldsParameter002()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new FieldsParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.NotStored);

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.IsFalse(actual);
        }

        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=true" and invoke Validate method
        /// What    Returns valid=true
        /// </summary>
        [TestMethod]
        public void FieldsParameter003()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new FieldsParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.Stored);

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Invoking the method "Execute" using 1 instance and 2 expressions
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FieldsParameter004()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""fields"": [
                ""Id"",
                ""Score""
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
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FieldsParameter instance
        /// When    Create the instance with an expression using a field indicated with "index=false" and invoke Validate method
        /// What    Returns valid=false
        /// </summary>
        [TestMethod]
        public void FieldsParameter005()
        {
            // Arrange
            bool actual;
            string dummy;
            var parameter = new FieldsParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.Stored, q => q.NotStored);

            // Act
            parameter.Validate(out actual, out dummy);

            // Assert
            Assert.IsFalse(actual);
        }

        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FieldListParameter006()
        {
            // Arrange / Act / Assert
            var parameter = new FieldsParameter<TestDocument>();
            parameter.Configure(null);
        }

        /// <summary>
        /// Where   Using a FieldListParameter instance
        /// When    Create the instance with empty collection
        /// What    Throws ArgumentOutOfRangeException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FieldListParameter007()
        {
            // Arrange / Act / Assert
            var parameter = new FieldsParameter<TestDocument>();
            parameter.Configure(new Expression<Func<TestDocument, object>>[] { });
        }
    }
}
