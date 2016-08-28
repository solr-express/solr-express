using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Solr5.Search.Parameter;
using System;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class FacetRangeParameterTests
    {
        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Execute" using the default arguments
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void FacetRangeParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""X"": {
                  ""range"": {
                    ""field"": ""_id_"",
                    ""mincount"": 1,
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
            Assert.Equal(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Execute" using the sort type and direction parameters
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void FacetRangeParameter002()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""X"": {
                  ""range"": {
                    ""field"": ""_id_"",
                    ""mincount"": 1,
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
            parameter.Configure("X", q => q.Id, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type integer and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter003()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropInteger, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type long and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter004()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropLong, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type float and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter005()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropFloat, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type double and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter006()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropDouble, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type decimal and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter007()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropDecimal, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type DateTime and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter008()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropDateTime, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
            Assert.True(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Validate" using a field type string and with fail fast actived
        /// What    Is valid should be true
        /// </summary>
        [Fact]
        public void FacetRangeParameter009()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>();
            parameter.Configure("X", q => q.PropString, "1", "10", "20", FacetSortType.CountDesc);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.False(isValid);
            Assert.False(string.IsNullOrWhiteSpace(errorMessage));
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetRangeParameter010()
        {
            // Arrange
            var parameter = new FacetRangeParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null, q => q.Id, "", "", ""));
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetRangeParameter011()
        {
            // Arrange
            var parameter = new FacetRangeParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure("x", null, "", "", ""));
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetRangeParameter012()
        {
            // Arrange
            var parameter = new FacetRangeParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure("x", q => q.Id, null, "", ""));
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetRangeParameter013()
        {
            // Arrange
            var parameter = new FacetRangeParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure("x", q => q.Id, "", null, ""));
        }

        /// <summary>
        /// Where   Using a FacetRangeParamete instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetRangeParameter014()
        {
            // Arrange
            var parameter = new FacetRangeParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure("x", q => q.Id, "", "", null));
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Execute" using the default arguments and an excluding list
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetRangeParameter015()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""X"": {
                  ""range"": {
                    ""field"": ""{!ex=tag1,tag2}_id_"",
                    ""mincount"": 1,
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
            Assert.Equal(expected, actual);
        }
    }
}
