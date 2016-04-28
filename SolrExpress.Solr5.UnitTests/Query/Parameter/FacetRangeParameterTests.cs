using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr5.Query.Parameter;
using System;

namespace SolrExpress.Solr5.UnitTests.Query.Parameter
{
    [TestClass]
    public class FacetRangeParameterTests
    {
        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Execute" using the default arguments
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""X"": {
                  ""range"": {
                    ""field"": ""Id"",
                    ""gap"": ""1"",
                    ""start"": ""10"",
                    ""end"": ""20"",
                    ""other"": [
                      ""before"",
                      ""after""
                    ]
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure("X", q => q.Id, "1", "10", "20");

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Execute" using the sort type and direction parameters
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter002()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""X"": {
                  ""range"": {
                    ""field"": ""Id"",
                    ""gap"": ""1"",
                    ""start"": ""10"",
                    ""end"": ""20"",
                    ""other"": [
                      ""before"",
                      ""after""
                    ],
                    ""sort"": {
                      ""count"": ""desc""
                    }
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure("X", q => q.Id, "1", "10", "20", SolrFacetSortType.CountDesc);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type integer and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter003()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropInteger, "1", "10", "20", SolrFacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsTrue(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type long and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter004()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropLong, "1", "10", "20", SolrFacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsTrue(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type float and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter005()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropFloat, "1", "10", "20", SolrFacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsTrue(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type double and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter006()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropDouble, "1", "10", "20", SolrFacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsTrue(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type decimal and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter007()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropDecimal, "1", "10", "20", SolrFacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsTrue(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type DateTime and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter008()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropDateTime, "1", "10", "20", SolrFacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsTrue(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type string and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter009()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropString, "1", "10", "20", SolrFacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FacetRangeParameter010()
        {
            // Arrange / Act / Assert
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure(null, q => q.Id, "", "", "");
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FacetRangeParameter011()
        {
            // Arrange / Act / Assert
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure("x", null, "", "", "");
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FacetRangeParameter012()
        {
            // Arrange / Act / Assert
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure("x", q => q.Id, null, "", "");
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FacetRangeParameter013()
        {
            // Arrange / Act / Assert
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure("x", q => q.Id, "", null, "");
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FacetRangeParameter014()
        {
            // Arrange / Act / Assert
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure("x", q => q.Id, "", "", null);
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Execute" using the default arguments and an excluding list
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter015()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""X"": {
                  ""range"": {
                    ""field"": ""{!ex=tag1,tag2}Id"",
                    ""gap"": ""1"",
                    ""start"": ""10"",
                    ""end"": ""20"",
                    ""other"": [
                      ""before"",
                      ""after""
                    ]
                  }
                }
              }
            }").ToString();
            var jObject = new JObject();
            var parameter = new FacetRangeParameter<TestDocument>();
            parameter.Configure("X", q => q.Id, "1", "10", "20", excludes: new[] { "tag1", "tag2" });

            // Act
            parameter.Execute(jObject);
            var actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
