using Xunit;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr5.Query.Parameter;
using System;
using SolrExpress.Core;

namespace SolrExpress.Solr5.UnitTests.Query.Parameter
{
    public class FacetFieldParameterTests
    {
        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the default arguments
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void FacetFieldParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""Id"": {
                  ""terms"": {
                    ""field"": ""_id_"",
                    ""mincount"": 1
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(actual, expected.ToString());
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the sort type and direction parameters
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void FacetFieldParameter002()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""Id"": {
                  ""terms"": {
                    ""field"": ""_id_"",
                    ""mincount"": 1,
                    ""sort"": {
                      ""count"": ""desc""
                    }
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id, FacetSortType.CountDesc);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(actual, expected.ToString());
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void FacetFieldParameter003()
        {
            // Arrange
            var parameter = new FacetFieldParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the limit parameter
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetFieldParameter004()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""Id"": {
                  ""terms"": {
                    ""field"": ""_id_"",
                    ""mincount"": 1,
                    ""limit"": 10
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id, limit: 10);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(actual, expected.ToString());
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the default arguments and an excluding list
        /// What    Create a valid string
        /// </summary>
        [Fact]
        public void FacetFieldParameter005()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""Id"": {
                  ""terms"": {
                    ""field"": ""{!ex=tag1,tag2}_id_"",
                    ""mincount"": 1
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id, excludes: new[] { "tag1", "tag2" });

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(actual, expected.ToString());
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Validate" using field Indexed=true
        /// What    Valid is true
        /// </summary>
        [Fact]
        public void FacetFieldParameter006()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetFieldParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.Indexed);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.True(isValid);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Validate" using field Indexed=false
        /// What    Valid is true
        /// </summary>
        [Fact]
        public void FacetFieldParameter007()
        {
            // Arrange
            bool isValid;
            string errorMessage;
            var parameter = new FacetFieldParameter<TestDocumentWithAttribute>();
            parameter.Configure(q => q.NotIndexed);

            // Act
            parameter.Validate(out isValid, out errorMessage);

            // Assert
            Assert.False(isValid);
            Assert.Equal(Resource.FieldMustBeIndexedTrueToBeUsedInAFacetException, errorMessage);
        }
    }
}
